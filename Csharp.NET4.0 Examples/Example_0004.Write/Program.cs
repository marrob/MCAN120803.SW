using System;
using Konvolucio.MCAN120803.API;

namespace Example_0004.Write
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Adapter példányosítása*/
            CanAdapterDevice adapter = new CanAdapterDevice();
            /*Kapcsolódás egy létező adapterhez.*/
            adapter.Connect();
            /*Kapcsolódott ezzel a SerialNumber-el rendlekező adapterhez.*/
            Console.WriteLine("Connected to: " + adapter.Attributes.SerialNumber);
            /*Busz lezárás engedélyezése. */
            adapter.Attributes.Termination = true;
            /*Megnyitás az átviteli sebesség paraméterrel.*/
            adapter.Open(CanBaudRateCollection.Speed500kBaud);
            /*Üzenet tömb létrehozása.*/
            var txMsgArray = new CanMessage[]
            {
                new CanMessage(0x01FF, new byte[]{0x01,0x02,0x03,0x04})
            };
            /*Üzenet tömb küldése.*/
            adapter.Write(txMsgArray);
            /*Kapcsolat zárása*/
            adapter.Close();
            /*Kapcsolat bontása*/
            adapter.Disconnect();
            Console.Read();
        }
    }
}