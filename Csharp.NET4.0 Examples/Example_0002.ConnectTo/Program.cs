using System;
using Konvolucio.MCAN120803.API;

namespace Example_0002.ConnectTo
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Elérhető adapterek megjelnítése*/
            foreach(CanAdapterItem item in  CanAdapterDevice.GetAdapters())
                Console.WriteLine(item);

            /*Adapter példányosítása*/
            CanAdapterDevice adapter = new CanAdapterDevice();

            /*Kapcsolódás a tömb 0. indexén lévő adapterhez*/
            adapter.ConnectTo(CanAdapterDevice.GetAdapters()[0]);

            /*Kapcsolat bontása*/
            adapter.Disconnect();

            Console.Read();

            /*Konzol kimenete:
             *CAN Bus Adapter - MCAN120803 3869366E3133
             *CAN Bus Adapter - MCAN120803 3873366E3133
             *CAN Bus Adapter - MCAN120803 387536633133
             */
        }
    }
}