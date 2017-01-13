
namespace Konvolucio.MCAN120803.GUI.DataStorage
{
    using Common;

    public class StroageParameters
    {
        public string ProductName { get; set; }
        public string ProductVersion { get; set; }
        public string ProductCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string Comment { get; set; }

        public string DeviceName { get; set; }
        public string Baudrate { get; set; }
        public bool Loopback { get; set; }
        public bool Termination { get; set; }
        public bool ListenOnly { get; set; }
        public bool NonAutoReTx { get; set; }

        public bool FiltersEnabled { get; set; }
        public bool LogEnabled { get; set; }
        public bool TraceEnabled { get; set; }
        public bool PlayHistoryClearEnabled { get; set; }
        public bool AdapterStatisticsEnabled { get; set; }
        public bool MessageStatisticsEnabled { get; set; }
        public bool RxMsgResolvingBySenderEnabled { get; set; }

        public string TimestampFormat { get; set; }
        public string ArbitrationIdFormat { get; set; }
        public string DataFormat { get; set; }


        public StroageParameters()
        {
            ProductName = AppConstants.ValueNotAvailable2;
            ProductVersion = AppConstants.ValueNotAvailable2;
            ProductCode = AppConstants.ValueNotAvailable2;
            CustomerName = AppConstants.ValueNotAvailable2;
            CustomerCode = AppConstants.ValueNotAvailable2;
            Comment = AppConstants.ValueNotAvailable2;

            DeviceName = AppConstants.ValueNotAvailable2;
            Baudrate = AppConstants.ValueNotAvailable2;
            Loopback = false;
            Termination = false;
            ListenOnly = false;
            NonAutoReTx = false;

            FiltersEnabled = false;
            LogEnabled = true;
            TraceEnabled = true;
            PlayHistoryClearEnabled = true;
            AdapterStatisticsEnabled = true;
            MessageStatisticsEnabled = true;
            RxMsgResolvingBySenderEnabled = true;

            TimestampFormat = AppConstants.DefaultTimestampFormat;
            ArbitrationIdFormat = AppConstants.DefaultArbitrationIdFormat;
            DataFormat = AppConstants.DefaultDataFormat;
        }

        public void CopyTo(ProjectParameters target)
        {
            /*ha valami nem jelenik meg a projectfájlból akkr itt a probléma...*/
            target.ProductName = ProductName;
            target.ProductVersion = ProductVersion;
            target.ProductCode = ProductCode;
            target.CustomerName = CustomerName;
            target.CustomerCode = CustomerCode;
            target.Comment = Comment;

            target.DeviceName = DeviceName;
            target.Baudrate = Baudrate;
            target.Loopback = Loopback;
            target.Termination = Termination;
            target.ListenOnly = ListenOnly;
            target.NonAutoReTx = NonAutoReTx;

            target.FiltersEnabled = FiltersEnabled;
            target.LogEnabled = LogEnabled;
            target.TraceEnabled = TraceEnabled;
            target.PlayHistoryClearEnabled = PlayHistoryClearEnabled;
            target.AdapterStatisticsEnabled = AdapterStatisticsEnabled;
            target.MessageStatisticsEnabled = MessageStatisticsEnabled;
            target.RxMsgResolvingBySenderEnabled = RxMsgResolvingBySenderEnabled;

            target.TimestampFormat = TimestampFormat;
            target.ArbitrationIdFormat = ArbitrationIdFormat;
            target.DataFormat = DataFormat;
        }

        public void New()
        {
            ProductName = AppConstants.ValueNotAvailable2;
            ProductVersion = AppConstants.ValueNotAvailable2;
            ProductCode = AppConstants.ValueNotAvailable2;
            CustomerName = AppConstants.ValueNotAvailable2;
            CustomerCode = AppConstants.ValueNotAvailable2;

            Comment = AppConstants.ValueNotAvailable2;
            DeviceName = AppConstants.ValueNotAvailable2;
            Baudrate = AppConstants.ValueNotAvailable2;
            Loopback = false;
            Termination = false;
            ListenOnly = false;
            NonAutoReTx = false;

            FiltersEnabled = false;
            LogEnabled = true;
            TraceEnabled = true;
            PlayHistoryClearEnabled = true;
            AdapterStatisticsEnabled = true;
            MessageStatisticsEnabled = true;
            RxMsgResolvingBySenderEnabled = true;

            TimestampFormat = AppConstants.DefaultTimestampFormat;
            ArbitrationIdFormat = AppConstants.DefaultArbitrationIdFormat;
            DataFormat = AppConstants.DefaultDataFormat;
        }
    }
}
