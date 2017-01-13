using System;
using Konvolucio.MCAN120803.API;

namespace Example_0003.ConnectBySerialNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Elérhető adapterek megjelnítése*/
            foreach (CanAdapterItem item in CanAdapterDevice.GetAdapters())
                Console.WriteLine(item);

            /*Adapter példányosítása*/
            CanAdapterDevice adapter = new CanAdapterDevice();

            /*Kapcsolódás a 3873366E3133 azonosítójú adapterhez*/
            adapter.ConnectTo("3873366E3133");

            Console.WriteLine("Connected to: " + adapter.Attributes.SerialNumber);

            /*Kapcsolat bontása*/
            adapter.Disconnect();

            Console.Read();

            /*Konzol kimenete:
             *CAN Bus Adapter - MCAN120803 3869366E3133
             *CAN Bus Adapter - MCAN120803 3873366E3133
             *CAN Bus Adapter - MCAN120803 387536633133
             *Connected to: 3873366E3133
             */
        }
    }
}