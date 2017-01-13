using System;
using Konvolucio.MCAN120803.API;
namespace Example_0006.Loopback
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
            /*Loopback moód engedélyezése*/
            adapter.Attributes.Loopback = true;
            /*Megnyitás az átviteli sebesség paraméterrel.*/
            adapter.Open(CanBaudRateCollection.Speed125000Baud);
            /*2 CAN üzenet tartalmazó tömb létrehozása a kimenő üzenetknek.*/
            CanMessage[] txMsgArray = new CanMessage[] 
            {
               /*              ArbId     Data                       */
               new CanMessage(0x000001, new byte[] {0x01,0x02, 0x03}),
               new CanMessage(0x000002, new byte[] {0x04,0x05, 0x06}),
            };

            /*Üzenetek küldése*/
             adapter.Write(txMsgArray);

            /*2 elemü üzenet tömb létrehozása a bejövő üzeneteknek.*/
            var rxMsgArray = new CanMessage[2];
            /*Timeout figyeléshez megjegyezzük az beolvasás indításának időpontját.*/
            long timestampTicks = DateTime.Now.Ticks;
            bool isTimeout = false;
            do
            {
                /*Ha 2db CAN üzenet várakozik a Bufferben, akkor kiolvassuk.*/
                if (adapter.Attributes.PendingRxMessages == 2)
                {
                    /*Bérkezett üzenet beolvasása a tömbe */
                    adapter.Read(rxMsgArray, 0, adapter.Attributes.PendingRxMessages);
                    /*Kilépés a do-while-ból*/
                    break;
                }
                /*Inditás óta eltelt már 5000ms?*/
                isTimeout = (DateTime.Now.Ticks - timestampTicks) > (5000 * 10000);
                /*Timeout-ig ismétli a ciklust...*/
            } while (!isTimeout);

            if (isTimeout)
            {
                Console.WriteLine("Timeout...");
            }
            else
            { 
                 foreach (CanMessage msg in rxMsgArray)
                        Console.WriteLine("Incoming Msg:" + msg.ToString());

                 Console.WriteLine("Complete.");
            }
            /*Kapcsolat zárása*/
            adapter.Close();
            /*Kapcsolat bontása*/
            adapter.Disconnect();
            Console.Read();
        }
    }
}
