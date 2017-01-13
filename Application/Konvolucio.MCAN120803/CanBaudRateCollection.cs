
namespace Konvolucio.MCAN120803.API
{
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// 
    /// </summary>
    public class CanBaudRateCollection
    {
        public class BaudRateItem
        {
            public string Name;
            public uint Value;
            public BaudRateItem(string name, uint value)
            {
                Name = name;
                Value = value;
            }
        }
        /// <summary>
        /// { 5000,      400, CAN_SJW_3TQ, CAN_BS1_16TQ, CAN_BS2_4TQ, }, //1
        /// </summary>
        public static BaudRateItem Speed5000Baud
        {
            get { return new BaudRateItem("5.000kBaud", 5000); }
        }
        /// <summary>
        /// { 5555,      360, CAN_SJW_3TQ, CAN_BS1_16TQ, CAN_BS2_4TQ, }, //2
        /// </summary>
        public static BaudRateItem Speed5555Buad
        {
            get { return new BaudRateItem("5.555kBaud", 5555); }
        }
        /// <summary>
        /// { 6250,      320, CAN_SJW_3TQ, CAN_BS1_16TQ, CAN_BS2_4TQ, }, //3
        /// </summary>
        public static BaudRateItem Speed6250Baud
        {
            get { return new BaudRateItem("6.250kBaud", 6250); }
        }
        /// <summary>
        /// { 8000,      250, CAN_SJW_3TQ, CAN_BS1_16TQ, CAN_BS2_4TQ, }, //4
        /// </summary>
        public static BaudRateItem Speed8000Baud
        {
            get { return new BaudRateItem("8.000kBaud", 8000); }
        }
        /// <summary>
        /// { 10000,     200, CAN_SJW_3TQ, CAN_BS1_16TQ, CAN_BS2_4TQ, }, //5
        /// </summary>
        public static BaudRateItem Speed10000Baud
        {
            get { return new BaudRateItem("10.000kBaud", 10000); }
        }
        /// <summary>
        /// { 12500,     160, CAN_SJW_3TQ, CAN_BS1_16TQ, CAN_BS2_4TQ, }, //6
        /// </summary>
        public static BaudRateItem Speed12500Baud
        {
            get { return new BaudRateItem("12.500kBaud", 12500); }
        }
        /// <summary>
        /// { 15625,     128, CAN_SJW_3TQ, CAN_BS1_16TQ, CAN_BS2_4TQ, }, //7
        /// </summary>
        public static BaudRateItem Speed16525Baud
        {
            get { return new BaudRateItem("15.625kBaud", 15625); }
        }
        /// <summary>
        /// { 16000,     125, CAN_SJW_3TQ, CAN_BS1_16TQ, CAN_BS2_4TQ, }, //8
        /// </summary>
        public static BaudRateItem Speed16000Baud
        {
            get { return new BaudRateItem("16.000kBaud", 16000); }
        }
        /// <summary>
        /// { 20000,     100, CAN_SJW_3TQ, CAN_BS1_16TQ, CAN_BS2_4TQ, }, //9
        /// </summary>
        public static BaudRateItem Speed20000Baud
        {
            get { return new BaudRateItem("20.000kBaud", 20000); }
        }
        /// <summary>
        /// { 25000,     80,  CAN_SJW_3TQ, CAN_BS1_16TQ, CAN_BS2_4TQ, }, //10
        /// </summary>
        public static BaudRateItem Speed25000Baud
        {
            get { return new BaudRateItem("25.000kBaud", 25000); }
        }
        /// <summary>
        /// { 31250,     64,  CAN_SJW_3TQ, CAN_BS1_16TQ, CAN_BS2_4TQ, }, //11
        /// </summary>
        public static BaudRateItem Speed31250Baud
        {
            get { return new BaudRateItem("31.250kBaud", 31250); }
        }
        /// <summary>
        /// { 33333,     60,  CAN_SJW_3TQ, CAN_BS1_16TQ, CAN_BS2_4TQ, }, //12
        /// </summary>
        public static BaudRateItem Speed33333Baud
        {
            get { return new BaudRateItem("33.333kBaud", 33333); }
        }
        /// <summary>
        /// { 40000,     50,  CAN_SJW_3TQ, CAN_BS1_16TQ, CAN_BS2_4TQ, }, //13
        /// </summary>
        public static BaudRateItem Speed40000Baud
        {
            get { return new BaudRateItem("40.000kBaud", 40000); }
        }
        /// <summary>
        /// { 50000,     40,  CAN_SJW_3TQ, CAN_BS1_16TQ, CAN_BS2_4TQ, }, //14
        /// </summary>
        public static BaudRateItem Speed50000Baud
        {
            get { return new BaudRateItem("50.000kBaud", 50000); }
        }
        /// <summary>
        /// { 62500,     32,  CAN_SJW_3TQ, CAN_BS1_16TQ, CAN_BS2_4TQ, }, //15
        /// </summary>
        public static BaudRateItem Speed62500Baud
        {
            get { return new BaudRateItem("62.250kBaud", 62500); }
        }
        /// <summary>
        /// { 80000,     25,  CAN_SJW_3TQ, CAN_BS1_16TQ, CAN_BS2_4TQ, }, //16
        /// </summary>
        public static BaudRateItem Speed80000Baud
        {
            get { return new BaudRateItem("80.000kBaud", 80000); }
        }
        /// <summary>
        /// { 83333,     24,  CAN_SJW_3TQ, CAN_BS1_16TQ, CAN_BS2_4TQ, }, //17
        /// </summary>
        public static BaudRateItem Speed83333Baud
        {
            get { return new BaudRateItem("83.333kBaud", 83333); }
        }
        /// <summary>
        /// { 100000,    20,  CAN_SJW_3TQ, CAN_BS1_16TQ, CAN_BS2_4TQ, }, //18
        /// </summary>
        public static BaudRateItem Speed100000Baud
        {
            get { return new BaudRateItem("100.000kBaud", 100000); }
        }
        /// <summary>
        /// { 125000,    16,  CAN_SJW_3TQ, CAN_BS1_16TQ, CAN_BS2_4TQ, }, //19
        /// </summary>
        public static BaudRateItem Speed125000Baud
        {
            get { return new BaudRateItem("125.000kBaud", 125000); }
        }
        /// <summary>
        /// { 166666,    14,  CAN_SJW_3TQ, CAN_BS1_14TQ, CAN_BS2_3TQ, }, //20
        /// </summary>
        public static BaudRateItem Speed166666Baud
        {
            get { return new BaudRateItem("166.666kBaud", 166666); }
        }
        /// <summary>
        /// { 200000,    10,  CAN_SJW_3TQ, CAN_BS1_16TQ, CAN_BS2_4TQ, }, //21
        /// </summary>
        public static BaudRateItem Speed200kBaud
        {
            get { return new BaudRateItem("200.000kBaud", 200000); }
        }
        /// <summary>
        /// { 250000,    8,   CAN_SJW_3TQ, CAN_BS1_16TQ, CAN_BS2_4TQ, }, //22
        /// </summary>
        public static BaudRateItem Speed250kBaud
        {
            get { return new BaudRateItem("250.000kBaud", 250000); }
        }
        /// <summary>
        /// { 400000,    5,   CAN_SJW_3TQ, CAN_BS1_16TQ, CAN_BS2_4TQ, }, //23
        /// </summary>
        public static BaudRateItem Speed400kBaud
        {
            get { return new BaudRateItem("400.000kBaud", 400000); }
        }
        /// <summary>
        /// { 500000,    4,   CAN_SJW_3TQ, CAN_BS1_16TQ, CAN_BS2_4TQ, }, //24
        /// </summary>
        public static BaudRateItem Speed500kBaud
        {
            get { return new BaudRateItem("500.000kBaud", 500000); }
        }
        /// <summary>
        /// { 800000,    4,   CAN_SJW_3TQ, CAN_BS1_10TQ, CAN_BS2_2TQ, }, //25
        /// </summary>
        public static BaudRateItem Speed840kBaud
        {
            get { return new BaudRateItem("800.000kBaud", 840000); }
        }
        /// <summary>
        /// { 1000000,   2,   CAN_SJW_3TQ, CAN_BS1_16TQ, CAN_BS2_4TQ, }, //26
        /// </summary>
        public static BaudRateItem Speed1000kBaud
        {
            get { return new BaudRateItem("1.000MBaud", 1000000); }
        }
        /********************************************************************************/
        public static List<BaudRateItem> GetBaudRates()
        {
            List<BaudRateItem>  items = new List<BaudRateItem> ();
            PropertyInfo[] thisObjectProperties = typeof(CanBaudRateCollection).GetProperties();
            foreach (PropertyInfo property in thisObjectProperties)
            {
                BaudRateItem item = (BaudRateItem)property.GetValue(null, null);
                items.Add(new BaudRateItem(item.Name, item.Value));
            }
            return items;
        }
    }
}
