
namespace Konvolucio.MCAN120803.GUI.AppModules.CanTx.Model
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using Common;
    using DataStorage;
    using Properties;
    using Tools.Model;
    using WinForms.Framework;

    /// <summary>
    /// A küldendő üzenetek kolleciója, adatkötésssel kapcsolódik a View-hoz
    /// </summary>
    [Serializable]
    public class CanTxMessageCollection : BindingList<CanTxMessageItem>, IRowReOredable, IToolItem
    {
        /// <summary>
        /// Néha rá kell nézni, hogy a szálon futó dolgok rendben vannak-e.
        /// </summary>
        public Exception LastException { get; private set; }

        /// <summary>
        /// Küldendő üzenetek várakozási sora.
        /// </summary>
        public SafeQueue<CommonCanMessage> TxQueue { get; set; }

        /// <summary>
        /// Kisérleti megoldás.
        /// Kötelességed, az olyan esetekben ehhez Lock-olni, amikor fut a Sender szál!
        /// </summary>
        public object SyncRoot => ((ICollection) this).SyncRoot;

        private bool _disposed;
        private bool _isRunning;
        private AutoResetEvent _shutdownEvent;
        private readonly object _lockObj = new object();
        private int _threadSleepMs;

        public CanTxMessageCollection()
        {
            LastException = null;
            _threadSleepMs = 0;
        }

        /// <summary>
        /// Periodikus üzenetek küldésének idítása.
        /// </summary>
        public void Start()
        {
            _isRunning = true;
            LastException = null;
            _shutdownEvent = new AutoResetEvent(false);
            _threadSleepMs = Settings.Default.SenderThreadSleepMs;
            var thread = new Thread(new ThreadStart(DoWork))
            {
                Priority = Settings.Default.SenderThreadPriority,
                Name = "Sender",
            };
            thread.Start();
        }

        /// <summary>
        /// Periodikus üzenet küldésének leállítása.
        /// </summary>
        public void Stop()
        {
            Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");

            if (_shutdownEvent != null)
            {
                Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "(): Stop is pending.");
                _shutdownEvent.Set();
            }
        }

        /// <summary>
        /// Billentyű megnyoásakor kell hívnod, ehhez tartozik egy vagy több üzenet, ami küldésre kerül.
        /// </summary>
        public void KeyPressed(string key)
        {
            var items = (this.Where(n => n.Key == key));
            foreach (var item in items)
                Send(item);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void ItemMoveTo(object item, int index)
        {
            lock (_lockObj) 
            /*Tipikus vershenyhelzet akkor alakulhat ki, 
            * amikor a Thread járja be lista elemeit, ekkor nem törölhet elemet a MoveTo*/
            {
                /*
                * Meg kell akadályozni, hogy Binding értesüljön a sor törlésréről és a beszurásról, 
                * elég neki a végeredmény, így ez nem számít sor törlésének és beszurásnak.
                */
                RaiseListChangedEvents = false;
                Remove(item as CanTxMessageItem);
                Insert(index, item as CanTxMessageItem);
                RaiseListChangedEvents = true;
                ResetBindings();
            }
        }

        /// <summary>
        /// Lista elem küldése, azonnal teszi a Queue-ba.
        /// </summary>
        public void Send(object item)
        {
             
            var senderItem = item as CanTxMessageItem;
            if (senderItem != null)
            {
                if (_isRunning)
                {
                    var msg = new CommonCanMessage
                    {
                        Source = "Message Sender Collection By Click Trigger",
                        Name = senderItem.Name,
                        Data = senderItem.Data,
                        Remote = senderItem.Remote,
                        ArbitrationId = senderItem.ArbitrationId,
                        Type = senderItem.Type,
                        Description = senderItem.Description,
                        Documentation = senderItem.Documentation,
                    };
                    TxQueue.Enqueue(msg);
                }
            }
            else
            {
                throw new InvalidCastException();

            }
        }

        /// <summary>
        /// Futtatja a periodikus küldést.
        /// </summary>
        private void DoWork()
        {
            foreach (var item in this)
                item.LastUpdateDateTimeTicks = DateTime.Now.Ticks;
            do
            {
                try
                {
                    lock (((ICollection)this).SyncRoot) /*Bejáráskor nem válozhat a lista!*/
                    {
                        var dateTimeNow = DateTime.Now.Ticks;
                        foreach (var item in this)
                        {
                            if (item.IsPeriod && (dateTimeNow - item.LastUpdateDateTimeTicks >= item.PeriodTime*10000))
                            {
                                Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "(): Last Period:" +
                                                ((dateTimeNow - item.LastUpdateDateTimeTicks)/(double) 10000).ToString("N3"));
                                item.LastUpdateDateTimeTicks = dateTimeNow;

                                var msg = new CommonCanMessage
                                {
                                    Source = "Periodic Sender",
                                    Name = item.Name,
                                    Data = item.Data,
                                    Remote = item.Remote,
                                    ArbitrationId = item.ArbitrationId,
                                    Type = item.Type,
                                    Documentation =  item.Documentation,
                                    Description = item.Description,
                                };
                                TxQueue.Enqueue(msg);
                            }
                        }
                        Thread.Sleep(_threadSleepMs);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "(): DoWork is now shutdown by Loop Fuction Exception.");
                    LastException = ex;
                    break;
                }

                if (_shutdownEvent.WaitOne(0))
                {
                    Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "(): DoWork is now shutdown!");
                    break;
                }

            } while (_isRunning);

            _isRunning = false;

            TxQueue?.Clear();

            Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "(): Loop is colsed...");
        }

        /// <summary>
        /// Lista másolása a projectbe.
        /// </summary>
        /// <param name="target">Project cél.</param>
        public void CopyTo(StorageCanTxMessageCollection target)
        {
            target.Clear();
            foreach (var item in this)
            {
                var targetItem = new StorageCanTxMessageItem();
                item.CopyTo(targetItem);
                target.Add(targetItem);
            }
        }

        /// <summary>
        /// Felszabadítás és a futó szál leállítása.
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
            }
            _disposed = true;
        }


        /// <summary>
        /// Elem beszúrása
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void InsertItem(int index, CanTxMessageItem item)
        {
            var messageSenderItem = item as CanTxMessageItem;
            if (messageSenderItem != null)
                messageSenderItem.Index = index;

            base.InsertItem(index, item);

            for (var i = 1; i < this.Count + 1; i++)
            {
                var senderItem = this[i-1] as CanTxMessageItem;
                if (senderItem != null) 
                    senderItem.Index = i;
            }
        }

        /// <summary>
        /// Üzenet keresése paraméterek alapján.
        /// </summary>
        /// <param name="arbitrationId"></param>
        /// <param name="type"></param>
        /// <param name="isRemote"></param>
        /// <returns></returns>
        public CanTxMessageItem GetMsg(uint arbitrationId, ArbitrationIdType type, bool isRemote)
        {
            var item = this.FirstOrDefault(n =>
            {
                return (n.ArbitrationId == arbitrationId &&
                        n.Type == type &&
                        n.Remote == isRemote);
            });
            return item;
        }

    }
}
