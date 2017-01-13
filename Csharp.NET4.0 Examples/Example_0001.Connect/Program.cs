using System;
using Konvolucio.MCAN120803.API;

namespace Example_0001.Connect
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

            /*Kapcsolódás egy létező adapterhez.*/
            adapter.Connect();

            Console.WriteLine("Connected to: " + adapter.Attributes.SerialNumber);

            /*Kapcsolat bontása*/
            adapter.Disconnect();

            Console.Read();

            /*Konzol kimenete:
             *CAN Bus Adapter - MCAN120803 3869366E3133
             *CAN Bus Adapter - MCAN120803 3873366E3133
             *CAN Bus Adapter - MCAN120803 387536633133
             *Connected to: 3869366E3133
             */
        }
    }
}