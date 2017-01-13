
namespace Konvolucio.MCAN120803.GUI.Common
{
    using System.ComponentModel;
    using DataStorage;
    using WinForms.Framework;

    public class ProjectParameters : INotifyPropertyChanged
    {
        #region Public Event
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        private string _productName;
        public string ProductName
        {
            get { return _productName; }
            set 
            {
                if (_productName != value)
                {
                    _productName = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(()=>ProductName));
                }
            }
        }

        private string _productVersion;
        public string ProductVersion
        {
            get { return _productVersion; }
            set 
            {
                if (_productVersion != value)
                {
                    _productVersion = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => ProductVersion));
                }
            }
        }

        private string _productCode;
        public string ProductCode
        {
            get { return _productCode; }
            set
            {
                if (_productCode != value)
                {
                    _productCode = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => ProductCode));
                }
            }
        }

        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                if (_customerName != value)
                {
                    _customerName = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => CustomerName));
                }
            }
        }

        private string _customerCode;
        public string CustomerCode
        {
            get { return _customerCode; }
            set
            {
                if (_customerCode != value)
                {
                    _customerCode = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => CustomerCode));
                }
            }
        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => Comment));
                }
            }
        }

        private string _deviceName;
        public string DeviceName
        {
            get { return _deviceName; }
            set
            {
                if (_deviceName != value)
                {
                    _deviceName = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => DeviceName));
                }
            }
        }

        private string _baudrate;
        public string Baudrate 
        {
            get { return _baudrate; }
            set 
            {
                if (_baudrate != value)
                {
                    _baudrate = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => Baudrate));
                }
            }
        }

        private bool _loopback;
        public bool Loopback
        {
            get { return _loopback; }
            set
            {
                if (_loopback != value)
                {
                    _loopback = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => Loopback));
                }
            }
        }

        private bool _termination;
        public bool Termination
        {
            get { return _termination; }
            set
            {
                if (_termination != value)
                {
                    _termination = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => Termination));
                }
            }
        }

        private bool _listenOnly;
        public bool ListenOnly
        {
            get { return _listenOnly; }
            set
            {
                if (_listenOnly != value)
                {
                    _listenOnly = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => ListenOnly));
                }
            }
        }

        private bool _nonAutoReTx;
        public bool NonAutoReTx
        {
            get { return _nonAutoReTx; }
            set
            {
                if (_nonAutoReTx != value)
                {
                    _nonAutoReTx = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => NonAutoReTx));
                }
            }
        }

        private bool _filtersEnabled;
        public bool FiltersEnabled 
        {
            get { return _filtersEnabled; }
            set
            {
                if (_filtersEnabled != value)
                {
                    _filtersEnabled = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => FiltersEnabled));
                }
            }
        }

        private bool _logEnabled;
        public bool LogEnabled
        {
            get { return _logEnabled; }
            set
            {
                if (_logEnabled != value)
                {
                    _logEnabled = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => LogEnabled));
                }
            }
        }

        private bool _traceEnabled;
        public bool TraceEnabled
        {
            get { return _traceEnabled; }
            set
            {
                if (_traceEnabled != value)
                {
                    _traceEnabled = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => TraceEnabled));
                }
            }
        }

        private bool _playHistoryClearEnabled;
        public bool PlayHistoryClearEnabled
        {
            get { return _playHistoryClearEnabled; }
            set
            {
                if (_playHistoryClearEnabled != value)
                {
                    _playHistoryClearEnabled = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => PlayHistoryClearEnabled));
                }
            }
        }

        private bool _adapterStatisticsEnabled;     
        public bool AdapterStatisticsEnabled
        {
            get
            {
                return _adapterStatisticsEnabled;
            }
            set
            {
                if (_adapterStatisticsEnabled != value)
                {
                    _adapterStatisticsEnabled = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => AdapterStatisticsEnabled));
                }
            }
        }

        private bool _messageStatisticsEnabled;
        public bool MessageStatisticsEnabled
        {
            get
            {
                return _messageStatisticsEnabled;
            }
            set
            {
                if (_messageStatisticsEnabled != value)
                {
                    _messageStatisticsEnabled = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => MessageStatisticsEnabled));
                }
            }
        }

        private bool _rxMsgResolvingBySenderEnabled;
        public bool RxMsgResolvingBySenderEnabled
        {
            get
            {
                return _rxMsgResolvingBySenderEnabled;
            }
            set
            {
                if (_rxMsgResolvingBySenderEnabled != value)
                {
                    _rxMsgResolvingBySenderEnabled = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => RxMsgResolvingBySenderEnabled));
                }
            }
        }

        string _timestampFormat;
        public string TimestampFormat
        {
            get { return _timestampFormat; }
            set
            {
                if (_timestampFormat != value)
                {
                    _timestampFormat = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => TimestampFormat));
                }
            }
        }

        private string _arbitrationIdFormat;
        public string ArbitrationIdFormat
        {
            get { return _arbitrationIdFormat; }
            set
            {
                if (_arbitrationIdFormat != value)
                {
                    _arbitrationIdFormat = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => ArbitrationIdFormat));
                }
            }
        }

        private string _dataFormat;
        public string DataFormat
        {
            get { return _dataFormat; }
            set
            {
                if (_dataFormat != value)
                {
                    _dataFormat = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => DataFormat));
                }
            }
        }


        public void CopyTo(StroageParameters target)
        {
            target.ProductName = _productName;
            target.ProductVersion = _productVersion;
            target.ProductCode = _productCode;
            target.CustomerName = _customerName;
            target.CustomerCode = _customerCode;
            target.Comment = _comment;
            
            target.DeviceName = _deviceName;
            target.Baudrate = _baudrate;
            target.Loopback = _loopback;
            target.Termination = _termination;
            target.ListenOnly = _listenOnly;
            target.NonAutoReTx = _nonAutoReTx;
            
            target.FiltersEnabled = _filtersEnabled;
            target.LogEnabled = _logEnabled;
            target.TraceEnabled = _traceEnabled;
            target.PlayHistoryClearEnabled = _playHistoryClearEnabled;
            target.AdapterStatisticsEnabled = _adapterStatisticsEnabled;
            target.MessageStatisticsEnabled = _messageStatisticsEnabled;
            target.RxMsgResolvingBySenderEnabled = _rxMsgResolvingBySenderEnabled;

            target.TimestampFormat = _timestampFormat;
            target.ArbitrationIdFormat = _arbitrationIdFormat;
            target.DataFormat = _dataFormat;
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
