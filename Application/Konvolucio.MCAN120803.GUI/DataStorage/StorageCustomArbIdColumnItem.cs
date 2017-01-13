// -----------------------------------------------------------------------
// <copyright file="CustomArbIdColumnItem.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.DataStorage
{
    using Common;


    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class StorageCustomArbIdColumnItem
    {
        public string Name { get; set; }
        public ArbitrationIdType Type { get; set; }
        public int StartBit { get; set; }
        public int LengthBit { get; set; }
        public int Shift { get; set; }
        public string Format { get; set; }
        public string Description { get; set; }

        public StorageCustomArbIdColumnItem()
        {
        }

        public void CopyTo(CustomArbIdColumnItem target)
        {
            target.Name = Name;
            target.Type = Type;
            target.StartBit = StartBit;
            target.LengthBit = LengthBit;
            target.Shift = Shift;
            target.Format = Format;
            target.Description = Description;
        }

        public override string ToString()
        {
            string str = string.Empty;
            str += ("\tName: " + Name + "; ").PadRight(12);
            str += ("\tType:" + Type + "; ").PadRight(12);
            str += ("\tStartBit:" + StartBit + "; ").PadRight(12);
            str += ("\tLengthBit:" + LengthBit + "; ").PadRight(12);
            str += ("\tShift:" + LengthBit + "; ").PadRight(12);
            str += ("\tFormat:" + Format + ";").PadRight(12);
            return str;
        }
    }
}