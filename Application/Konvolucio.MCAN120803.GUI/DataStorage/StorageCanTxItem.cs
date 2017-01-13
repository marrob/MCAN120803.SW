
namespace Konvolucio.MCAN120803.GUI.DataStorage
{
    using AppModules.CanTx.Model;
    using Common;
    using Converters;


    public class StorageCanTxMessageItem
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public bool IsPeriod { get; set; }
        public int PeriodTime { get; set; }
        public ArbitrationIdType Type { get; set; }
        public bool Remote { get; set; }
        public string ArbitrationId { get; set; }
        public string Data { get; set; }
        public string Documentation { get; set; }
        public string Description { get; set; }

        public StorageCanTxMessageItem()
        {

        }

        public void CopyTo(CanTxMessageItem target)
        {
            target.Name = Name;
            target.Key = Key;
            target.IsPeriod = IsPeriod;
            target.PeriodTime = PeriodTime;
            target.Type = Type;
            target.Remote = Remote;
            target.ArbitrationId = CustomDataConversion.StringToUInt32HighSpeed(ArbitrationId);
            target.Data = CustomDataConversion.StringToByteArrayHighSpeed(Data);
            target.Documentation = Documentation;
            target.Description = Description;
        }
    }
}
