using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SpringCardPCSC;

namespace Konvolucio.MUDS150628.UnitTest
{

    /********************************************************************************/
    [TestFixture]
    class UnitTest_UV_SC
    {
        public readonly byte[] ATRDriverCard_Krista2 = { 0x3B, 0x9A, 0x96, 0xC0, 0x10, 0x31, 0xFE, 0x5D, 0x00, 0x64, 0x05, 0x7B, 0x01, 0x02, 0x31, 0x80, 0x90, 0x00, 0x76 };
        public readonly byte[] ATRCompanyCard_Krista2 = { 0x3B, 0x9A, 0x96, 0xC0, 0x10, 0x31, 0xFE, 0x5D, 0x00, 0x64, 0x05, 0x7B, 0x01, 0x02, 0x31, 0x80, 0x90, 0x00, 0x76 };
        
        SCardChannel SmartCard;
        CanBusLink CanLink;
        Iso15765NetwrorkLayer Network;
        /********************************************************************************/
        [TestFixtureSetUp]
        public void Setup()
        {
            SmartCard = new SCardChannel("OMNIKEY CardMan 3x21 0");
            Assert.IsTrue(SmartCard.CardPresent, "Nincs kártya bedugva.");
            SmartCard.ShareMode = SCARD.SHARE_SHARED;
            SmartCard.Protocol = (uint)(SCARD.PROTOCOL_T1);
            SmartCard.Connect();

            UInt32 txId = 0x38DAEEFB;
            UInt32 rxId = 0x38DAFBEE;
            UInt32 baudRate = 250000;
            CanLink = new CanBusLink(txId,rxId,baudRate);
            CanLink.Connect();
            CanLink.BusTerminationEnable = true;
            CanLink.Open();

            Network = new Iso15765NetwrorkLayer(CanLink);
        }
        /********************************************************************************/
        [TestFixtureTearDown]
        public void ClenUp()
        { 
            if (SmartCard != null)
            {
                SmartCard.Disconnect(1);
                SmartCard = null;
            }

            if (Network != null)
            {
                Network.SaveFrameLog("D:\\" + this.GetType().FullName + ".csv");
                CanLink.Close();
                Network = null;
                CanLink.Disconnect();
                CanLink.Dispose();
            }
        }

        /********************************************************************************/
        [Test]
        public void _0001_Tachograph_Present()
        {
            byte[] response;
            Network.Transport(new byte[] { 0x3E, 0x00 }, out response);
            Assert.AreEqual(new byte[] { 0x7E, 0x00 }, response);
        }
        /********************************************************************************/
        [Test]
        public void _0001_DiagSession()
        {
            byte[] response;
            Network.Transport(new byte[] { 0x10, 0x7E }, out response);
            Assert.AreEqual(new byte[] { 0x50, 0x7E }, response);
        }
        /********************************************************************************/
        [Test]
        public void _0002_Authenticationd()
        {
            byte[] request;
            byte[] response;

            byte[] routineRequest;

            byte[] smartCardCommand;
            byte[] smartCardResponse;

            int fmsRoutineStatusHeaderSize = 5;


            Console.WriteLine("Request:TesterPresent.");
            //Erre VU-na kötelessége válaszolni, ha nincs válasz nincs VU
            Network.Transport(new byte[] { 0x3E, 0x00 }, out response);
            Assert.AreEqual(new byte[] { 0x7E, 0x00 }, response);
            Console.WriteLine("VU Response:TesterPresent Done.");

            Console.WriteLine("Request:DiagnosticSession,Session:0x7E");
            Network.Transport(new byte[] { 0x10, 0x7E }, out response);
            Assert.AreEqual(new byte[] { 0x50, 0x7E }, response);
            Console.WriteLine("VU Response:Session Done.");

            Console.WriteLine("Request:RoutinContol,StartRoutine,id:0x0180,Option:CloseRemoteAuthentication.");
            //Ez az üzenet a valós forgalom alapján került bele...
            Network.Transport(new byte[] { 0x31, 0x01, 0x01, 0x80, 0x09 }, out response);
            Assert.AreEqual(new byte[] { 0x71, 0x01, 0x01, 0x80, 0x0A }, response);
            Console.WriteLine("VU Response:RoutinContol,StartRoutine,id:0x0180,Status:CloseRemoteAuthentication Done.");

            Console.WriteLine("Request:RoutinContol,StartRoutine,id:0x0180,Option:RemoteCompanyCardReady + ATR:" + Tools.ByteArrayToCStyleString(SmartCard.CardAtr.GetBytes()));
            routineRequest = new byte[] { 0x31, 0x01, 0x01, 0x80, 0x01 };
            request = new byte[routineRequest.Length + SmartCard.CardAtr.GetBytes().Length];
            Buffer.BlockCopy(routineRequest, 0, request, 0, routineRequest.Length);
            Buffer.BlockCopy(SmartCard.CardAtr.GetBytes(), 0, request, routineRequest.Length, SmartCard.CardAtr.GetBytes().Length);
            Network.Transport(request, out response);
            Assert.AreEqual(new byte[] { 0x71, 0x01, 0x01, 0x80, 0x02 }, response, "Tachograph válasza nem VUReady.");
            Console.WriteLine("VU Response:RoutinContol,StartRoutine,id:0x0180,Status:VUReady.");


            bool isDone = false;
            smartCardResponse = new byte[] { };
            do
            {
                Console.WriteLine("Request:RoutinContol,StartRoutine,id:0x0180,Option:CompanyCardToVUData + Data:" + Tools.ByteArrayToCStyleString(smartCardResponse));
                routineRequest = new byte[] { 0x31, 0x01, 0x01, 0x80, 0x03 };
                request = new byte[routineRequest.Length + smartCardResponse.Length];
                Buffer.BlockCopy(routineRequest, 0, request, 0, routineRequest.Length);
                Buffer.BlockCopy(smartCardResponse, 0, request, routineRequest.Length, smartCardResponse.Length);
                Network.Transport(request, out response);

                if (response[4] == 0x04)
                {
                    Console.WriteLine("VU Response:RoutinContol,StartRoutine,id:0x0180,Status:VUToCompanyCardData. + Data:" + Tools.ByteArrayToCStyleString(response, fmsRoutineStatusHeaderSize, response.Length - fmsRoutineStatusHeaderSize));
                    smartCardCommand = new byte[response.Length - fmsRoutineStatusHeaderSize];
                    Buffer.BlockCopy(response, fmsRoutineStatusHeaderSize, smartCardCommand, 0, response.Length - fmsRoutineStatusHeaderSize);
                    Console.WriteLine("SmartCard Command:" + Tools.ByteArrayToCStyleString(smartCardCommand));
                    SmartCard.Transmit(new CAPDU(smartCardCommand));
                    smartCardResponse = SmartCard.Response.GetBytes();
                    Console.WriteLine("SmartCard Response:" + Tools.ByteArrayToCStyleString(smartCardResponse) + "->" + SmartCard.Response.SWString);                  
                    isDone = false;
                }
                else if (response[4] == FmsRoutine.StatusRecord.RemoteAuthenticationSucceeded)
                {
                    Console.WriteLine("VU Response:RoutinContol,StartRoutine,id:0x0180,Status:RemoteAuthenticationSucceeded.");
                    Assert.AreEqual(new byte[] { 0x71, 0x01, 0x01, 0x80, 0x06 }, response);
                    isDone = true;
                }
                else if (response[4] == FmsRoutine.StatusRecord.RemoteDownloadAccessGranted)
                {
                    Console.WriteLine("VU Response:RoutinContol,StartRoutine,id:0x0180,Status:RemoteDownloadAccessGranted.");
                    Assert.AreEqual(new byte[] { 0x71, 0x01, 0x01, 0x80, 0x08 }, response);
                    isDone = true;
                }
                else if (response[4] == FmsRoutine.StatusRecord.AuthenticationError)
                {
                    Console.WriteLine("VU Response:RoutinContol,StartRoutine,id:0x0180,Status:AuthenticationError.");
                    Assert.AreEqual(new byte[] { 0x71, 0x01, 0x01, 0x80, 0x0E }, response);
                    isDone = true;
                }
                else if (response[4] == FmsRoutine.StatusRecord.RemoteAuthenticationIsClosed)
                {
                    Console.WriteLine("VU Response:RoutinContol,StartRoutine,id:0x0180,Status:RemoteAuthenticationIsClosed.");
                    Assert.AreEqual(new byte[] { 0x71, 0x01, 0x01, 0x80, 0x0A }, response);
                    isDone = true;
                }
                else if (response[4] == FmsRoutine.StatusRecord.TooManyAuthenticationErrors)
                {
                    Console.WriteLine("VU Response:RoutinContol,StartRoutine,id:0x0180,Status:TooManyAuthenticationErrors.");
                    Assert.AreEqual(new byte[] { 0x71, 0x01, 0x01, 0x80, 0x10 }, response);
                    isDone = true;
                }

            } while (!isDone);
           
        }
        /********************************************************************************/
        [Test]
        public void _0003_Authentication_WithDelay()
        {
            byte[] request;
            byte[] response;

            byte[] routineRequest;

            byte[] smartCardCommand;
            byte[] smartCardResponse;

            int fmsRoutineStatusHeaderSize = 5;
            int delayMs = 5000;
            long timestamp = 0;


            Console.WriteLine("Request:TesterPresent.");
            //Erre VU-na kötelessége válaszolni, ha nincs válasz nincs VU
            Network.Transport(new byte[] { 0x3E, 0x00 }, out response);
            Assert.AreEqual(new byte[] { 0x7E, 0x00 }, response);
            Console.WriteLine("VU Response:TesterPresent Done.");

            Console.WriteLine("Request:DiagnosticSession,Session:0x7E");
            Network.Transport(new byte[] { 0x10, 0x7E }, out response);
            Assert.AreEqual(new byte[] { 0x50, 0x7E }, response);
            Console.WriteLine("VU Response:Session Done.");

            Console.WriteLine("Request:RoutinContol,StartRoutine,id:0x0180,Option:CloseRemoteAuthentication.");
            //Ez az üzenet a valós forgalom alapján került bele...
            Network.Transport(new byte[] { 0x31, 0x01, 0x01, 0x80, 0x09 }, out response);
            Assert.AreEqual(new byte[] { 0x71, 0x01, 0x01, 0x80, 0x0A }, response);
            Console.WriteLine("VU Response:RoutinContol,StartRoutine,id:0x0180,Status:CloseRemoteAuthentication Done.");

            Console.WriteLine("Request:RoutinContol,StartRoutine,id:0x0180,Option:RemoteCompanyCardReady + ATR:" + Tools.ByteArrayToCStyleString(SmartCard.CardAtr.GetBytes()));
            routineRequest = new byte[] { 0x31, 0x01, 0x01, 0x80, 0x01 };
            request = new byte[routineRequest.Length + SmartCard.CardAtr.GetBytes().Length];
            Buffer.BlockCopy(routineRequest, 0, request, 0, routineRequest.Length);
            Buffer.BlockCopy(SmartCard.CardAtr.GetBytes(), 0, request, routineRequest.Length, SmartCard.CardAtr.GetBytes().Length);
            Network.Transport(request, out response);
            Assert.AreEqual(new byte[] { 0x71, 0x01, 0x01, 0x80, 0x02 }, response, "Tachograph válasza nem VUReady.");
            Console.WriteLine("VU Response:RoutinContol,StartRoutine,id:0x0180,Status:VUReady.");

            
            timestamp  = DateTime.Now.Ticks;
            do
            {
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("Request:DiagnosticSession,Session:0x7E");
                Network.Transport(new byte[] { 0x10, 0x7E }, out response);
                Assert.AreEqual(new byte[] { 0x50, 0x7E }, response);
                Console.WriteLine("VU Response:Session Done.");

            } while ((DateTime.Now.Ticks - timestamp) < delayMs * 10000);

            bool isDone = false;
            smartCardResponse = new byte[] { };
            do
            {
                Console.WriteLine("Request:RoutinContol,StartRoutine,id:0x0180,Option:CompanyCardToVUData + Data:" + Tools.ByteArrayToCStyleString(smartCardResponse));
                routineRequest = new byte[] { 0x31, 0x01, 0x01, 0x80, 0x03 };
                request = new byte[routineRequest.Length + smartCardResponse.Length];
                Buffer.BlockCopy(routineRequest, 0, request, 0, routineRequest.Length);
                Buffer.BlockCopy(smartCardResponse, 0, request, routineRequest.Length, smartCardResponse.Length);
                Network.Transport(request, out response);

                if (response[4] == 0x04)
                {
                    Console.WriteLine("VU Response:RoutinContol,StartRoutine,id:0x0180,Status:VUToCompanyCardData. + Data:" + Tools.ByteArrayToCStyleString(response, fmsRoutineStatusHeaderSize, response.Length - fmsRoutineStatusHeaderSize));
                    smartCardCommand = new byte[response.Length - fmsRoutineStatusHeaderSize];
                    Buffer.BlockCopy(response, fmsRoutineStatusHeaderSize, smartCardCommand, 0, response.Length - fmsRoutineStatusHeaderSize);
                    Console.WriteLine("SmartCard Command:" + Tools.ByteArrayToCStyleString(smartCardCommand));
                    SmartCard.Transmit(new CAPDU(smartCardCommand));
                    smartCardResponse = SmartCard.Response.GetBytes();
                    Console.WriteLine("SmartCard Response:" + Tools.ByteArrayToCStyleString(smartCardResponse) + "->" + SmartCard.Response.SWString);
                    isDone = false;
                }
                else if (response[4] == FmsRoutine.StatusRecord.RemoteAuthenticationSucceeded)
                {
                    Console.WriteLine("VU Response:RoutinContol,StartRoutine,id:0x0180,Status:RemoteAuthenticationSucceeded.");
                    Assert.AreEqual(new byte[] { 0x71, 0x01, 0x01, 0x80, 0x06 }, response);
                    isDone = true;
                }
                else if (response[4] == FmsRoutine.StatusRecord.RemoteDownloadAccessGranted)
                {
                    Console.WriteLine("VU Response:RoutinContol,StartRoutine,id:0x0180,Status:RemoteDownloadAccessGranted.");
                    Assert.AreEqual(new byte[] { 0x71, 0x01, 0x01, 0x80, 0x08 }, response);
                    isDone = true;
                }
                else if (response[4] == FmsRoutine.StatusRecord.AuthenticationError)
                {
                    Console.WriteLine("VU Response:RoutinContol,StartRoutine,id:0x0180,Status:AuthenticationError.");
                    Assert.AreEqual(new byte[] { 0x71, 0x01, 0x01, 0x80, 0x0E }, response);
                    isDone = true;
                }
                else if (response[4] == FmsRoutine.StatusRecord.RemoteAuthenticationIsClosed)
                {
                    Console.WriteLine("VU Response:RoutinContol,StartRoutine,id:0x0180,Status:RemoteAuthenticationIsClosed.");
                    Assert.AreEqual(new byte[] { 0x71, 0x01, 0x01, 0x80, 0x0A }, response);
                    isDone = true;
                }
                else if (response[4] == FmsRoutine.StatusRecord.TooManyAuthenticationErrors)
                {
                    Console.WriteLine("VU Response:RoutinContol,StartRoutine,id:0x0180,Status:TooManyAuthenticationErrors.");
                    Assert.AreEqual(new byte[] { 0x71, 0x01, 0x01, 0x80, 0x10 }, response);
                    isDone = true;
                }

                timestamp = DateTime.Now.Ticks;
                do
                {
                    //System.Threading.Thread.Sleep(1000);
                    //Console.WriteLine("Request:DiagnosticSession,Session:0x7E");
                    //Network.Transport(new byte[] { 0x10, 0x7E }, out response);
                    //Assert.AreEqual(new byte[] { 0x50, 0x7E }, response);
                    //Console.WriteLine("VU Response:Session Done.");

                } while ((DateTime.Now.Ticks - timestamp) < delayMs * 10000);

            } while (!isDone);

        }
         
         
        /********************************************************************************/
        [Test]
        public void _0003_Download_Overview()
        {
            byte[] response;
            byte[] downloadData;
            int downloadOffset;

            Console.WriteLine("Request:DiagnosticSession,Session:0x7E");
            Network.Transport(new byte[] { 0x10, 0x7E }, out response);
            Assert.AreEqual(new byte[] { 0x50, 0x7E }, response);
            Console.WriteLine("VU Response:Session Done.");

            Console.WriteLine("Request:RoutinContol,StartRoutine,id:0x0180,Option:RemoteDownloadDataRequest");
            Console.WriteLine("\tDownloadRequestList:");
            Console.WriteLine("\t\tDataTransferRequest#1: 01 00 (Overview, parameter length: 0x00)");
            Network.Transport(new byte[] { 0x31, 0x01, 0x01, 0x80, 0x07, 0x01, 0x00,}, out response);
            Assert.AreEqual(new byte[] {  0x71, 0x01, 0x01, 0x80, 0x08 }, response);
            Console.WriteLine("VU Response:RoutinContol,StartRoutine,id:0x0180,Status:RemoteDownloadAccessGranted.");

            Console.WriteLine("Request:RequestUpload(szervertől a kliens felé vagyis az ECU-tól a teszter felé).");
            Console.WriteLine("\tDataFormatIdentifier: 0x00 - manufacture specific");
            Console.WriteLine("\taddressAndLengthFormatIdentifier::0x44-addressLength: 4byte, memoryLength:4byte");
            Console.WriteLine("\t\tMemory Address: 0x00 0x00 0x00 0x00");
            Console.WriteLine("\t\tMemory Size: 0xFF 0xFF 0xFF 0xFF");
            Network.Transport(new byte[] { 0x35, 0x00, 0x44, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF}, out response);
            Assert.AreEqual(new byte[] { 0x75, 0x10, 0xFF }, response);
            Console.WriteLine("VU Response:");
            Console.WriteLine("\tLengthFormatIdentifier: 0x10 - maxNumberOfBlockLength: 1byte");
            Console.WriteLine("\t\tmaxNumberOfBlockLength:0xFF");


            bool isDone = false;
            int blockSequenceCounter = 1;
            downloadData = new byte[0xFFFF];
            downloadOffset = 0;
            do
            {
                Console.WriteLine("Request:TransferData");
                Console.WriteLine("\tblockSequenceCounter:" + blockSequenceCounter.ToString());
                Console.WriteLine("\t\ttransferRequestParameterRecord#1: 0x00 - FMS wrapAroundCounter");
                Console.WriteLine("\t\ttransferRequestParameterRecord#2: 0x01 - FMS Request Overview");
                Network.Transport(new byte[] { 0x36, (byte)blockSequenceCounter, 0x00, 0x01 }, out response);
                Assert.AreEqual(response[0], 0x76, "Negatív nyugta vagy valami más...");
                Console.WriteLine("VU Response:");
                Console.WriteLine("\tblockSequenceCounter:" + response[1].ToString());
                Console.WriteLine("\twrapAroundCounter:" + response[2].ToString());
                Console.WriteLine("\tTREP#2=echo of the TRTP#2 byte from the request message:" + response[3].ToString());
                Buffer.BlockCopy(response, 4, downloadData, downloadOffset, response.Length - 4);
                downloadOffset += response.Length - 4;
                Console.WriteLine("\tData:" + Tools.ByteArrayToCStyleString(response,4,response.Length - 4));

                blockSequenceCounter++;

                if (response.Length < 255)
                {
                    isDone = true;
                    Console.WriteLine("Request:RequestTransferExit");
                    Network.Transport(new byte[] { 0x37, 0x00 }, out response);
                    Assert.AreEqual(new byte[] { 0x77, 0x00}, response);
                    Console.WriteLine("VU Response:RequestTransferExit Positive Response.");
                    Array.Resize(ref downloadData, downloadOffset);
                }

            } while (!isDone);


            Console.WriteLine("***Summary Overview****");
            Console.WriteLine("Downloaded data length is " + downloadData.Length.ToString() + " byte(s).");
            Console.WriteLine("Data:");
            string str = Tools.ArrayToStringFormat(downloadData, 0, "0x", "{0:X2}", ',', 16);
            Console.WriteLine(str);
        }
        /********************************************************************************/
        [Test]
        public void _0004_Download()
        {
            byte[] response;
            byte[] downloadData;
            int downloadOffset;

            Console.WriteLine("Request:DiagnosticSession,Session:0x7E");
            Network.Transport(new byte[] { 0x10, 0x7E }, out response);
            Assert.AreEqual(new byte[] { 0x50, 0x7E }, response);
            Console.WriteLine("VU Response:Session Done.");

            Console.WriteLine("Request:RoutinContol,StartRoutine,id:0x0180,Option:RemoteDownloadDataRequest");
            Console.WriteLine("\tDownloadRequestList:");
            Console.WriteLine("\t\tDataTransferRequest#1: 01 00 (Overview, parameter length: 0x00)");
            Console.WriteLine("\t\tDataTransferRequest#2: 02 0A 0x02 >4E EA 8A 00< 03 >56 75 EF 80<(Request Activites,Period, parameter length: 0x0A)");
            Console.WriteLine("\t\tDataTransferRequest#2: 03 00 (Request Events and faults, parameter length: 0x00)");
            Console.WriteLine("\t\tDataTransferRequest#2: 04 00 (Request Events and faults, parameter length: 0x00)");
            Console.WriteLine("\t\tDataTransferRequest#3: 05 00 (Technical data, parameter length: 0x00)");
            Network.Transport(new byte[] { 0x31, 0x01, 0x01, 0x80, 0x07, 
                                                             0x01, 0x00, //Overview
                                                             0x02, 0x0A, //Activites Period
                                                                        //MSB              LSB
                                                                   0x02, 0x4E, 0xEA, 0x8A, 0x00, //Period Start 
                                                                   0x03, 0x56, 0x85, 0xC1, 0x80, //Period End
                                                             0x03, 0x00,  //Request Events and Faults
                                                             0x04, 0x00,  //Detailed  speed
                                                             0x05, 0x00,  //Technical data
                                                          
                                                            }, out response);
            Assert.AreEqual(new byte[] { 0x71, 0x01, 0x01, 0x80, 0x08 }, response);
            Console.WriteLine("VU Response:RoutinContol,StartRoutine,id:0x0180,Status:RemoteDownloadAccessGranted.");

            Console.WriteLine("Request:RequestUpload(szervertől a kliens felé vagyis az ECU-tól a teszter felé).");
            Console.WriteLine("\tDataFormatIdentifier: 0x00 - manufacture specific");
            Console.WriteLine("\taddressAndLengthFormatIdentifier::0x44-addressLength: 4byte, memoryLength:4byte");
            Console.WriteLine("\t\tMemory Address: 0x00 0x00 0x00 0x00");
            Console.WriteLine("\t\tMemory Size: 0xFF 0xFF 0xFF 0xFF");
            Network.Transport(new byte[] { 0x35, 0x00, 0x44, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF }, out response);
            Assert.AreEqual(new byte[] { 0x75, 0x10, 0xFF }, response);
            Console.WriteLine("VU Response:");
            Console.WriteLine("\tLengthFormatIdentifier: 0x10 - maxNumberOfBlockLength: 1byte");
            Console.WriteLine("\t\tmaxNumberOfBlockLength:0xFF");

            bool isDone = false;
            int blockSequenceCounter = 1;
            

            #region FMS Request Overview
            if (false)
            {
            isDone = false;
            blockSequenceCounter = 1;
            downloadData = new byte[0xFFFF];
            downloadOffset = 0;

            downloadData[downloadOffset] = 0x76;
            downloadOffset += 1;
            downloadData[downloadOffset] = 0x01;
            downloadOffset += 1;

    
                do
                {
                    Console.WriteLine("Request:TransferData");
                    Console.WriteLine("\tblockSequenceCounter:" + blockSequenceCounter.ToString());
                    Console.WriteLine("\t\ttransferRequestParameterRecord#1: 0x00 - FMS wrapAroundCounter");
                    Console.WriteLine("\t\ttransferRequestParameterRecord#2: 0x01 - FMS Request Overview");
                    Network.Transport(new byte[] { 0x36, (byte)blockSequenceCounter, 0x00, 0x01 }, out response);
                    Assert.AreEqual(response[0], 0x76, "Negatív nyugta vagy valami más...");
                    Console.WriteLine("VU Response:");
                    Console.WriteLine("\tblockSequenceCounter:" + response[1].ToString());
                    Console.WriteLine("\twrapAroundCounter:" + response[2].ToString());
                    Console.WriteLine("\tTREP#2=echo of the TRTP#2 byte from the request message:" + response[3].ToString());
                    Buffer.BlockCopy(response, 4, downloadData, downloadOffset, response.Length - 4);



                    ////SID + TREP + DATA
                    //Buffer.BlockCopy(response, 4, downloadData, downloadOffset, response.Length - 4);
                    downloadOffset += response.Length;


                    Console.WriteLine("\tData:" + Tools.ByteArrayToCStyleString(response, 4, response.Length - 4));

                    blockSequenceCounter++;

                    if (response.Length < 255) //Ez ennyi!
                    {
                        isDone = true;
                        Array.Resize(ref downloadData, downloadOffset);
                    }

                } while (!isDone);

                Console.WriteLine("***Summary Overview****");
                Console.WriteLine("Downloaded data length is " + downloadData.Length.ToString() + " byte(s).");
                Console.WriteLine("Data:");
                Console.WriteLine(Tools.ArrayToStringFormat(downloadData, 0, "0x", "{0:X2}", ',', 16));
            #endregion
            }

            if (false)
            {
                //--------------------------------------------
                #region Summary FMS Activities
                isDone = false;
                blockSequenceCounter = 1;
                downloadData = new byte[0xFFFF];
                downloadOffset = 0;
                do
                {
                    Console.WriteLine("Request:TransferData");
                    Console.WriteLine("\tblockSequenceCounter:" + blockSequenceCounter.ToString());
                    Console.WriteLine("\t\ttransferRequestParameterRecord#1: 0x00 - FMS wrapAroundCounter");
                    Console.WriteLine("\t\ttransferRequestParameterRecord#2: 0x02 - FMS Activites");
                    Network.Transport(new byte[] { 0x36, (byte)blockSequenceCounter, 0x00, 0x02, 0x56, 0x73, 0x4C, 0x80 }, out response);
                    Assert.AreEqual(response[0], 0x76, "Negatív nyugta vagy valami más...");
                    Console.WriteLine("VU Response:");
                    Console.WriteLine("\tblockSequenceCounter:" + response[1].ToString());
                    Console.WriteLine("\twrapAroundCounter:" + response[2].ToString());
                    Console.WriteLine("\tTREP#2=echo of the TRTP#2 byte from the request message:" + response[3].ToString());
                    Buffer.BlockCopy(response, 4, downloadData, downloadOffset, response.Length - 4);
                    downloadOffset += response.Length - 4;
                    Console.WriteLine("\tData:" + Tools.ByteArrayToCStyleString(response, 4, response.Length - 4));

                    blockSequenceCounter++;

                    if (response.Length < 255) //Ez ennyi!
                    {
                        isDone = true;
                        Array.Resize(ref downloadData, downloadOffset);
                    }

                } while (!isDone);

                Console.WriteLine("***Summary FMS Activities****");
                Console.WriteLine("Downloaded data length is " + downloadData.Length.ToString() + " byte(s).");
                Console.WriteLine("Data:");
                Console.WriteLine(Tools.ArrayToStringFormat(downloadData, 0, "0x", "{0:X2}", ',', 16));
                #endregion
                //--------------------------------------------
                #region Summary FMS Events and faults
                isDone = false;
                blockSequenceCounter = 1;
                downloadData = new byte[0xFFFF];
                downloadOffset = 0;
                do
                {
                    Console.WriteLine("Request:TransferData");
                    Console.WriteLine("\tblockSequenceCounter:" + blockSequenceCounter.ToString());
                    Console.WriteLine("\t\ttransferRequestParameterRecord#1: 0x00 - FMS wrapAroundCounter");
                    Console.WriteLine("\t\ttransferRequestParameterRecord#2: 0x03 - FMS Events and faults");
                    Network.Transport(new byte[] { 0x36, (byte)blockSequenceCounter, 0x00, 0x03 }, out response);
                    Assert.AreEqual(response[0], 0x76, "Negatív nyugta vagy valami más...");
                    Console.WriteLine("VU Response:");
                    Console.WriteLine("\tblockSequenceCounter:" + response[1].ToString());
                    Console.WriteLine("\twrapAroundCounter:" + response[2].ToString());
                    Console.WriteLine("\tTREP#2=echo of the TRTP#2 byte from the request message:" + response[3].ToString());
                    Buffer.BlockCopy(response, 4, downloadData, downloadOffset, response.Length - 4);
                    downloadOffset += response.Length - 4;
                    Console.WriteLine("\tData:" + Tools.ByteArrayToCStyleString(response, 4, response.Length - 4));

                    blockSequenceCounter++;

                    if (response.Length < 255) //Ez ennyi!
                    {
                        isDone = true;
                        Array.Resize(ref downloadData, downloadOffset);
                    }

                } while (!isDone);

                Console.WriteLine("***Summary FMS Events and faults****");
                Console.WriteLine("Downloaded data length is " + downloadData.Length.ToString() + " byte(s).");
                Console.WriteLine("Data:");
                Console.WriteLine(Tools.ArrayToStringFormat(downloadData, 0, "0x", "{0:X2}", ',', 16));
                #endregion    
           
                //--------------------------------------------
                #region Summary FMS Detailed speed
                isDone = false;
                blockSequenceCounter = 1;
                downloadData = new byte[0xFFFF];
                downloadOffset = 0;

                downloadData[downloadOffset] = 0x76;
                downloadOffset += 1;
                downloadData[downloadOffset] = 0x04;
                downloadOffset += 1;
                do
                {
                    Console.WriteLine("Request:TransferData");
                    Console.WriteLine("\tblockSequenceCounter:" + blockSequenceCounter.ToString());
                    Console.WriteLine("\t\ttransferRequestParameterRecord#1: 0x00 - FMS wrapAroundCounter");
                    Console.WriteLine("\t\ttransferRequestParameterRecord#2: 0x04 - FMS Detailed speed");
                    Network.Transport(new byte[] { 0x36, (byte)blockSequenceCounter, 0x00, 0x04 }, out response);
                    Assert.AreEqual(response[0], 0x76, "Negatív nyugta vagy valami más...");
                    Console.WriteLine("VU Response:");
                    Console.WriteLine("\tblockSequenceCounter:" + response[1].ToString());
                    Console.WriteLine("\twrapAroundCounter:" + response[2].ToString());
                    Console.WriteLine("\tTREP#2=echo of the TRTP#2 byte from the request message:" + response[3].ToString());
                    Buffer.BlockCopy(response, 4, downloadData, downloadOffset, response.Length - 4);
                    downloadOffset += response.Length - 4;
                    Console.WriteLine("\tData:" + Tools.ByteArrayToCStyleString(response, 4, response.Length - 4));

                    blockSequenceCounter++;

                    if (response.Length < 255) //Ez ennyi!
                    {
                        isDone = true;
                        Array.Resize(ref downloadData, downloadOffset);
                    }

                } while (!isDone);

                Console.WriteLine("***Summary FMS Detailed speed****");
                Console.WriteLine("Downloaded data length is " + downloadData.Length.ToString() + " byte(s).");
                Console.WriteLine("Data:");
                Console.WriteLine(Tools.ArrayToStringFormat(downloadData, 0, "0x", "{0:X2}", ',', 16));
                #endregion 
            }

            if(true)
            {
                //--------------------------------------------
                #region Summary FMS Technical data
                isDone = false;
                blockSequenceCounter = 1;
                downloadData = new byte[0xFFFF];
                downloadOffset = 0;

                downloadData[downloadOffset] = 0x76;
                downloadOffset += 1;
                downloadData[downloadOffset] = 0x05;
                downloadOffset += 1;
                do
                {
                    Console.WriteLine("Request:TransferData");
                    Console.WriteLine("\tblockSequenceCounter:" + blockSequenceCounter.ToString());
                    Console.WriteLine("\t\ttransferRequestParameterRecord#1: 0x00 - FMS wrapAroundCounter");
                    Console.WriteLine("\t\ttransferRequestParameterRecord#2: 0x05 - FMS Technical data");
                    Network.Transport(new byte[] { 0x36, (byte)blockSequenceCounter, 0x00, 0x05 }, out response);
                    Assert.AreEqual(response[0], 0x76, "Negatív nyugta vagy valami más...");
                    Console.WriteLine("VU Response:");
                    Console.WriteLine("\tblockSequenceCounter:" + response[1].ToString());
                    Console.WriteLine("\twrapAroundCounter:" + response[2].ToString());
                    Console.WriteLine("\tTREP#2=echo of the TRTP#2 byte from the request message:" + response[3].ToString());
                    Buffer.BlockCopy(response, 4, downloadData, downloadOffset, response.Length - 4);
                    downloadOffset += response.Length - 4;
                    Console.WriteLine("\tData:" + Tools.ByteArrayToCStyleString(response, 4, response.Length - 4));

                    blockSequenceCounter++;

                    if (response.Length < 255) //Ez ennyi!
                    {
                        isDone = true;
                        Console.WriteLine("Request:RequestTransferExit");
                        Network.Transport(new byte[] { 0x37, 0x00 }, out response);
                        Assert.AreEqual(new byte[] { 0x77, 0x00 }, response);
                        Console.WriteLine("VU Response:RequestTransferExit Positive Response.");
                        Array.Resize(ref downloadData, downloadOffset);
                    }

                } while (!isDone);
                Console.WriteLine("***Summary FMS Technical data****");
                Console.WriteLine("Downloaded data length is " + downloadData.Length.ToString() + " byte(s).");
                Console.WriteLine("Data:");
                Console.WriteLine(Tools.ArrayToStringFormat(downloadData, 0, "0x", "{0:X2}", ',', 16));
                #endregion
            }

        }
    }
}
