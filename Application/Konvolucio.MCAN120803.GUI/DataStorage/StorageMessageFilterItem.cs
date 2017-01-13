
namespace Konvolucio.MCAN120803.GUI.DataStorage
{
    using Common;

    public class StorageMessageFilterItem
    {
        public string Name { get; set; }
        public MaskOrArbId MaskOrArbId { get; set; }
        public bool Enabled { get; set; }
        public uint MaskOrArbIdValue { get; set; }
        public ArbitrationIdType Type { get; set; }
        public bool Remote { get; set; }
        public MessageDirection Direction { get; set; }
        public MessageFilterMode Mode { get; set; }


        public StorageMessageFilterItem()
        {
        }

        public void CopyTo(AppModules.Filter.Model.MessageFilterItem target)
        {
            target.Name = Name;
            target.Enabled = Enabled;
            target.MaskOrArbId = MaskOrArbId;
            target.MaskOrArbIdValue = MaskOrArbIdValue;
            target.Type = Type;
            target.Remote = Remote;
            target.Direction = Direction;
            target.Mode = Mode;
        }
    }
}
