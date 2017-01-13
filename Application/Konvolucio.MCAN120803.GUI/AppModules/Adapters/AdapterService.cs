
namespace Konvolucio.MCAN120803.GUI.AppModules.Adapters
{
    using System;
    using System.Linq;
    using System.Diagnostics;
    using System.Reflection;
    using System.Threading;

    using API;
    using Common;
    using DataStorage;
    using Log.Model;
    using Statistics.Message.Model;
    using Statistics.Adapter.Model;
    using Filter.Model;
    using Tracer.Model;
    using Properties; 
    using Services;
    using Tools.Model;
    using WinForms.Framework;

    public interface IAdapterService : IDisposable
    {
        event EventHandler Stopped;
        event EventHandler Started;
        string GetDefaultDeviceName { get; }
        string GetDefaultBaudrate { get; }
        void Play();
        void Stop();
    }

    public sealed class AdapterService : IAdapterService
    {
        private const string VirtualDeviceName = "Virtual Adapater (Transmitt Only)";

        public event EventHandler Stopped;
        public event EventHandler Started;

        public string GetDefaultDeviceName
        {
            get
            {
                var items = CanAdapterDevice.GetAdapters().Select(n => n.SerialNumber).ToArray();
                if (items.Length != 0)
                    return items[0];
                else
                    return VirtualDeviceName;
            }
        }

        public string GetDefaultBaudrate
        {
            get { return GetBaudrates()[0]; }
        }

        public bool IsRunning
        {
            get { return _isRunning; }
        }

        private readonly MessageTraceCollection _messageTrace;
        private ICanAdapterDevice _adapter;
        private readonly ProjectParameters _paramters;
        private readonly IAdapterStatistics _adapterStat;
        private readonly MessageStatistics _messageStat;
        private readonly MessageFilterCollection _filters;
        private readonly Storage _project;
        private readonly ILogFileCollection _logFiles;
        private readonly ToolTableCollection _toolTables;
        private readonly SafeQueue<CommonCanMessage> _txQueue; 
       

        private AutoResetEvent _shutdownEvent = new AutoResetEvent(false);
        private AutoResetEvent _readyToDisposeEvent = new AutoResetEvent(false);
        private long _statusUpdateTimestamp = 0;
        private ILogFileItem _log;
        private bool _disposed;
        private bool _isRunning;
        private int _virtualAdapterFrameCounter;

        public AdapterService(
            MessageTraceCollection tracer,
            ProjectParameters paramters,
            IAdapterStatistics adapterStat,
            MessageStatistics messageStat,
            MessageFilterCollection filters,
            Storage project,
            ILogFileCollection logFiles,
            ToolTableCollection toolTables)
        {
            _messageTrace = tracer;
            _adapterStat = adapterStat;
            _messageStat = messageStat;
            _paramters = paramters;
            _filters = filters;
            _project = project;
            _logFiles = logFiles;
            _toolTables = toolTables;
            _txQueue = new SafeQueue<CommonCanMessage>();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string[] GetBaudrates()
        {
            return CanBaudRateCollection.GetBaudRates().Select(n => (n.Name)).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string[] GetAdapters()
        {
            var items = CanAdapterDevice.GetAdapters().Select(n => n.SerialNumber).ToArray();
            string[] adp = new string[items.Length + 1];
            items.CopyTo(adp, 0);
            adp[items.Length] = VirtualDeviceName;
            return adp;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Play()
        {

            /*Ha engdélyezve van a törlés, akkor indulás előtt törli az előzményeket.*/
            if (_paramters.PlayHistoryClearEnabled)
            {
                _adapterStat.Reset();
                _messageStat.Default();
                _messageTrace.Clear();
                _filters.Default();
            }

            Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");

            _shutdownEvent = new AutoResetEvent(false);
            _readyToDisposeEvent = new AutoResetEvent(false);




            uint baudrate = 0;
            if (_paramters.Baudrate.Contains("Custom"))
            {
                /*Baudrate = "B003D007 Custom Baud"*/
                baudrate = UInt32.Parse(_paramters.Baudrate.Remove(_paramters.Baudrate.IndexOf(@"Custom")).Trim(), System.Globalization.NumberStyles.HexNumber);
            }
            else
            {
                /*Baudrate = 5.000kBaud*/
                baudrate = CanBaudRateCollection.GetBaudRates().First(n => n.Name == _paramters.Baudrate).Value;
            }

            if (_paramters.DeviceName != VirtualDeviceName)
            {
                try
                {
                    _adapter = new CanAdapterDevice();
                    _adapter.ConnectTo(_paramters.DeviceName);
                    _adapter.Services.Reset();
                    _adapter.Attributes.ListenOnly = _paramters.ListenOnly;
                    _adapter.Attributes.Loopback = _paramters.Loopback;
                    _adapter.Attributes.NonAutoRetransmission = _paramters.NonAutoReTx;
                    _adapter.Attributes.Termination = _paramters.Termination;
                    _adapter.Open(baudrate);
                    _statusUpdateTimestamp = 0;
                }
                catch (Exception)
                {
                    _adapter.Dispose();
                    OnStopped();
                    throw;
                }
            }
            else
            {
                _virtualAdapterFrameCounter = 0;
            }

            try
            {
                if (_project.Parameters.LogEnabled)
                {
                    _log = new LogFileItem(_project.Loaction, _project.FileName, DateTime.Now.ToString(AppConstants.FileNameTimestampFormat));
                    _log.Messages.AddToStorageBegin();
                }

                var th = new Thread(new ThreadStart(DoWork))
                {
                    Name = "Adapter",
                    Priority = Settings.Default.AdapterThreadPriority
                };
                th.Start();
                _toolTables.SetTxQueue(_txQueue);
                _toolTables.Start();
            }
            catch
            {
                OnStopped();
                throw;
            }



        }

        /// <summary>
        /// Adapter leállítása
        /// </summary>
        public void Stop()
        {
            Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");

            if (_shutdownEvent != null)
            {
                Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "(): Stop is pending.");
                _shutdownEvent.Set();
            }

            if (_readyToDisposeEvent.WaitOne(5000))
            {
                Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "(): Stop is ready.");
            }

           /* OnStopped(); */
        }

        /// <summary>
        /// Ütemesen nézzük mi a helyzet az adapterrel
        /// </summary>
        private void DoWork()
        {
            _isRunning = true;
            Exception loopException = null;

            {
                /*SynContext*/
                Action doMehtod = () => OnStarted();
                if (App.SyncContext != null)
                    App.SyncContext.Post((e1) => { doMehtod(); }, null);
                else
                    doMehtod();
            }

            do
            {
                if (_adapter != null && _adapter.IsConnected)
                {
                    if (_adapter.LasException != null)
                    {
                        Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "(): DoWork is now shutdown by Adapter LastException is not null.");
                        loopException = _adapter.LasException;
                        break;
                    }
                    /*
                    TODO: Ezt megcsinálni a ToolTable-re is
                    if (_messageSenders.LastException != null)
                    {
                        Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "(): DoWork is now shutdown by MessageSender LastException is not null.");
                        loopException = _messageSenders.LastException;
                        break;
                    }
                    */
                    try
                    {
                        #region CAN Adapter Funcitons

                        if (_txQueue.Count > 0)
                        {
                            var tx = _txQueue.Dequeue();
                            var txTimestamp = DateTime.Now;
                            var txTrace = new MessageTraceItem(
                              name:           tx.Name,
                              timestamp:      txTimestamp,
                              direction:      MessageDirection.Transmitted,
                              type:           tx.Type,
                              remote:         tx.Remote,
                              arbitrationId:  tx.ArbitrationId,
                              data:           tx.Data,
                              documentation:  tx.Documentation,
                              description:    tx.Description);
                          
                            _adapter.Write(new[]
                            {
                                new CanMessage(
                                    isRemote:       tx.Remote ? (byte) 0x01 : (byte) 0x00b,
                                    arbitrationId:  (tx.Type == ArbitrationIdType.Standard) ? tx.ArbitrationId : tx.ArbitrationId | 0x20000000,
                                    data:           tx.Data)
                            });
                            if (!_paramters.FiltersEnabled || _filters.DoAddToTrace(
                                                                arbitrationId: tx.ArbitrationId,
                                                                type:          tx.Type,
                                                                remote:        tx.Remote,
                                                                direction:     MessageDirection.Transmitted))
                            {     
                                if (_project.Parameters.TraceEnabled)
                                    _messageTrace.Add(txTrace);
                            }

                            if(!_paramters.FiltersEnabled || _filters.DoAddToLog(
                                                                arbitrationId: tx.ArbitrationId,
                                                                type:          tx.Type,
                                                                remote:        tx.Remote,
                                                                direction:     MessageDirection.Transmitted))
                            {
                                if (_project.Parameters.LogEnabled)
                                    _log.Messages.AddToStorage(
                                        name:          tx.Name,
                                        timestamp:     txTimestamp,
                                        direction:     MessageDirection.Transmitted,
                                        type:          tx.Type,
                                        arbitrationId: tx.ArbitrationId,
                                        isRemote:      tx.Remote,
                                        data:          tx.Data,
                                        documentation: tx.Documentation,
                                        description:   tx.Description);
                            }

                            if (_project.Parameters.MessageStatisticsEnabled)
                            {
                                Action doMehtod = () =>/*SynContext*/
                                {
                                    _messageStat.InsertMessage(
                                        name:          tx.Name,
                                        direction:     MessageDirection.Transmitted,
                                        type:          tx.Type,
                                        arbitrationId: tx.ArbitrationId,
                                        data:          tx.Data,
                                        remote:        tx.Remote,
                                        timestamp:     txTimestamp);
                                };
                                if (App.SyncContext != null)
                                    App.SyncContext.Post((e1) => { doMehtod(); }, null);
                                else
                                    doMehtod();
                            }
                        }

                        if (_adapter.Attributes.PendingRxMessages > 0)
                        {
                            var rxItems = new CanMessage[1];
                            _adapter.Read(rxItems, 0, 1);

                            var rxType = ((rxItems[0].ArbitrationId & 0x20000000) == 0) ? ArbitrationIdType.Standard : ArbitrationIdType.Extended;
                            var rxArbId = unchecked(rxItems[0].ArbitrationId & (uint) (~0x20000000));
                            var rxIsRmote = rxItems[0].IsRemote != 0 ? true : false;
                            var rxTimestamp = new DateTime(rxItems[0].TimestampTick);
                            var rxData = rxItems[0].Data;
                            var rxName = string.Empty;
                            var rxDocumentation = string.Empty;
                            var rxDescription = string.Empty;

                            /*
                            TODO: Ezt megcsinálni
                            if (_paramters.RxMsgResolvingBySenderEnabled)
                            {
                                var resolvedRxMsg = _messageSenders.GetMsg(rxArbId, rxType, rxIsRmote);
                                if (resolvedRxMsg != null)
                                {
                                    rxName = resolvedRxMsg.Name;
                                    rxDocumentation = resolvedRxMsg.Documentation;
                                    rxDescription = resolvedRxMsg.Description;
                                }
                            }
                            */

                            var rxTrace = new MessageTraceItem(
                                name:          rxName,
                                timestamp:     rxTimestamp,
                                direction:     MessageDirection.Received,
                                type:          rxType,
                                remote:        rxIsRmote,
                                arbitrationId: rxArbId,
                                data:          rxData,
                                documentation: rxDocumentation,
                                description:   rxDescription); 

                            if (!_paramters.FiltersEnabled || _filters.DoAddToTrace(
                                                                   arbitrationId: rxArbId,
                                                                   type:          rxType,
                                                                   remote:        rxIsRmote,
                                                                   direction:     MessageDirection.Received))
                            {
                                if (_paramters.TraceEnabled)
                                    _messageTrace.Add(rxTrace);
                            }

                            if (!_paramters.FiltersEnabled || _filters.DoAddToLog(
                                        arbitrationId: rxArbId,
                                        type:          rxType,
                                        remote:        rxIsRmote,
                                        direction:     MessageDirection.Received))
                            {
                                if (_paramters.LogEnabled)
                                    _log.Messages.AddToStorage(
                                        name:           rxName,
                                        timestamp:      rxTimestamp,
                                        direction:      MessageDirection.Received,
                                        type:           rxType,
                                        arbitrationId:  rxArbId,
                                        isRemote:       rxIsRmote,
                                        data:           rxData,
                                        documentation:  rxDocumentation,
                                        description:    rxDescription);
                            }
                            if (_paramters.MessageStatisticsEnabled)
                            {
                                Action doMehtod = () =>/*SynContext*/
                                {
                                    _messageStat.InsertMessage(
                                        name:          rxName,
                                        direction:     MessageDirection.Received,
                                        type:          rxType,
                                        arbitrationId: rxArbId,
                                        data:          rxData,
                                        remote:        rxIsRmote,
                                        timestamp:     rxTimestamp);
                                };
                                if (App.SyncContext != null)
                                    App.SyncContext.Post((e1) => { doMehtod(); }, null);
                                else
                                    doMehtod();
                            }
                        }

                        if (_paramters.AdapterStatisticsEnabled)
                        {
                            /*Statiszitikai az adapter követi. Az értkékeket időnként le kell kérdezni tőle.*/
                            /*Az adapter leállítását követően még egyszer LE KELL KÉRDEZNI, hogy az UI-n az UTOLSÓ érték látszódjon.*/
                            long nowDateTimeTicks = DateTime.Now.Ticks;
                            if ((nowDateTimeTicks - _statusUpdateTimestamp) > 10000 * 1000)
                            {
                                AppDiagService.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "(): Adapter Staistics Poll Now... DiffTime: " + ((DateTime.Now.Ticks - _statusUpdateTimestamp) / 10000).ToString("N4") + "ms");

                                _statusUpdateTimestamp = nowDateTimeTicks;
                                _adapterStat.UpdateReceived(
                                    total:   _adapter.Attributes.RxTotal,
                                    drop:    _adapter.Attributes.RxDrop,
                                    error:   _adapter.Attributes.RxErrorCounter,
                                    pending: _adapter.Attributes.PendingRxMessages);
                                _adapterStat.UpdateTransmitted(
                                    total:   _adapter.Attributes.TxTotal,
                                    drop:    _adapter.Attributes.TxDrop,
                                    error:   _adapter.Attributes.TxErrorCounter,
                                    pending: _txQueue.Count);
                            }
                        }

                        #endregion
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "(): DoWork is now shutdown by Loop Fuction Exception.");
                        loopException = ex;
                        break;
                    }
                }
                else
                {
                    try
                    {
                        #region Virtual CAN Adapter Funcitions

                        /*Virtuaális adapter*/
                        if (_txQueue.Count > 0)
                        {
                            _virtualAdapterFrameCounter++;

                            var tx = _txQueue.Dequeue();
                            var txTrace = new MessageTraceItem(
                                name:          tx.Name,
                                timestamp:     DateTime.Now,
                                direction:     MessageDirection.Transmitted,
                                type:          tx.Type,
                                remote:        tx.Remote,
                                arbitrationId: tx.ArbitrationId,
                                data:          tx.Data,
                                documentation: tx.Documentation,
                                description:   tx.Description);
                            if (!_paramters.FiltersEnabled || _filters.DoAddToTrace(
                                                                  arbitrationId: tx.ArbitrationId,
                                                                  type:          tx.Type,
                                                                  remote:        tx.Remote,
                                                                  direction:     MessageDirection.Transmitted))
                            {
                                if (_paramters.TraceEnabled)
                                    _messageTrace.Add(txTrace);
                            }

                            if (!_paramters.FiltersEnabled || _filters.DoAddToLog(
                                        arbitrationId: tx.ArbitrationId,
                                        type:          tx.Type,
                                        remote:        tx.Remote,
                                        direction:     MessageDirection.Transmitted))
                            {
                                if (_project.Parameters.LogEnabled)
                                    _log.Messages.AddToStorage(
                                       name:          tx.Name,
                                       timestamp:     DateTime.Now,
                                       direction:     MessageDirection.Transmitted,
                                       type:          tx.Type,
                                       arbitrationId: tx.ArbitrationId,
                                       isRemote:      tx.Remote,
                                       data:          tx.Data,
                                       documentation: tx.Documentation,
                                       description:   tx.Description);
                            }

                            if (_paramters.MessageStatisticsEnabled)
                            {
                                Action doMehtod = () => 
                                {
                                    _messageStat.InsertMessage(
                                      name:          tx.Name,
                                       direction:    MessageDirection.Transmitted,
                                      type:          tx.Type,
                                      arbitrationId: tx.ArbitrationId,
                                      data:          tx.Data,
                                      remote:        tx.Remote,
                                      timestamp:     DateTime.Now);
                                };
                                if (App.SyncContext != null)
                                    App.SyncContext.Post((e1) => { doMehtod(); }, null);
                                else
                                    doMehtod();
                            }
                        }
                        if (_paramters.AdapterStatisticsEnabled)
                        {
                            /*Statiszitikai az adapter követi. Az értkékeket időnként le kell kérdezni tőle.*/
                            /*Az adapter leállítását követően még egyszer LE KELL KÉRDEZNI, hogy az UI-n az UTOLSÓ érték látszódjon.*/
                            long nowDateTimeTicks = DateTime.Now.Ticks;
                            if ((nowDateTimeTicks - _statusUpdateTimestamp) > 10000 * 1000)
                            {

                                AppDiagService.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "(): Virtual Adapter Staistics Poll Now... DiffTime: " + ((DateTime.Now.Ticks - _statusUpdateTimestamp)/10000).ToString("N4") + "ms");
                                _statusUpdateTimestamp = nowDateTimeTicks;
                                _adapterStat.UpdateTransmitted(
                                    total:   _virtualAdapterFrameCounter,
                                    drop:    0,
                                    error:   0, 
                                    pending: _txQueue.Count);

                            }
                        }

                        #endregion
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "(): DoWork is now shutdown by Loop Fuction Exception.");
                        loopException = ex;
                        break;
                    }

                }
                if (_shutdownEvent.WaitOne(0))
                {
                    Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "(): DoWork is now shutdown!");
                    break;
                }

            } while (_isRunning);
            /*Probléma megjelnítése*/
            if(loopException != null)
                new ErrorHandlerService().Show(loopException);

            #region Update Last State

            if (loopException == null)
            { /*Csak ha nem volt gebasz az adapterrel...*/
                if (_paramters.DeviceName != VirtualDeviceName && _adapter.IsConnected)
                {
                    /*Statisztika lefrissítése az UI-nak*/
                    _adapterStat.UpdateReceived(
                       total:   _adapter.Attributes.RxTotal,
                       drop:    _adapter.Attributes.RxDrop,
                       error:   _adapter.Attributes.RxErrorCounter,
                       pending: _adapter.Attributes.PendingRxMessages);
                    _adapterStat.UpdateTransmitted(
                       total:   _adapter.Attributes.TxTotal,
                       drop:    _adapter.Attributes.TxDrop,
                       error:   _adapter.Attributes.TxErrorCounter,
                       pending: _txQueue.Count);
                    _adapter.Disconnect();

                }
                else
                {
                    _adapterStat.UpdateTransmitted(
                      total:   _virtualAdapterFrameCounter,
                      drop:    0, 
                      error:   0,
                      pending: _txQueue.Count);
                }
            }
            #endregion

            #region Resource Freeing
            if (_adapter != null)
            {
                _adapter.Dispose();
                _adapter = null;
            }

           // _messageSenders.Stop();
            _readyToDisposeEvent.Set();
            _isRunning = false;
            _toolTables.Stop();
            #endregion

            #region Loop Terminate
            {
                /*SynContext*/
                Action doMehtod = () =>
                {
                    try
                    {
                        if (_project.Parameters.LogEnabled)
                        {
                            _log.Messages.AddToStorageEnd();
                            _logFiles.Add(_log);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        /*Minden áron jelezni kell! hogy megállt.*/
                        OnStopped();
                        Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "(): Loop is colsed...");
                    }
                };
                if (App.SyncContext != null)
                    App.SyncContext.Post((e1) => { doMehtod(); }, null);
                else
                    doMehtod();
            }
            #endregion

        }

        private void OnStopped()
        {
            if (Stopped != null)
            {
                Stopped(this, EventArgs.Empty);
            }
        }

        private void OnStarted()
        {          
            if (Started != null)
            {
                Started(this, EventArgs.Empty);
            }
        }

        /// <summary>
        ///  Public implementation of Dispose pattern callable by consumers. 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected implementation of Dispose pattern. 
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                if (_isRunning)
                {
                    /*szabályos leállítás*/
                    Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "(): Running-> Stop()");
                    Stop();
                }

                if (_shutdownEvent != null)
                {
                    _shutdownEvent.Dispose();
                    _shutdownEvent = null;
                }

                if (_readyToDisposeEvent != null)
                {
                    _readyToDisposeEvent.Dispose();
                    _readyToDisposeEvent = null;
                }
            } 
            _disposed = true;
        }
    }
}
