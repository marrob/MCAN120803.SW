using System;
using Konvolucio.MCAN120803.API;

namespace Example_0005.Read
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Adapter példányosítása*/
            CanAdapterDevice adapter = new CanAdapterDevice();
            /*Kapcsolódás egy létező adapterhez.*/
            adapter.Connect();
            /*Busz lezárás engedélyezése. */
            adapter.Attributes.Termination = true;
            /*Megnyitás az átviteli sebesség paraméterrel.*/
            adapter.Open(CanBaudRateCollection.Speed500kBaud);
            /*10 elemü üzenet tömb létrehozása.*/
            var rxMsgArray = new CanMessage[10];
            /*Bérkezett üzenet beolvasása a tömbe */
            adapter.Read(rxMsgArray, 0, adapter.Attributes.PendingRxMessages);
            /*Kapcsolat zárása*/
            adapter.Close();
            /*Kapcsolat bontása*/
            adapter.Disconnect();
            Console.Read();
        }
    }
}
