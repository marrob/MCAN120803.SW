using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Konvolucio.MUDS150628
{
    //Layer 3
    /// <summary>
    /// Road vehicles - Diagnostics on 
    /// Controller Area Networks (CAN)
    /// Network layer Services
    /// </summary>
    public class Iso15765NetwrorkLayer
    {
        public int ReadTimeoutMs;
        internal string MessageFrameLogContent;
        internal bool ParserLog;

        /// <summary>
        /// Block Size (BS)
        /// Ha 0, akkor megszakítás nélkül fogadhat.
        /// </summary>
        byte BlockSize = 0x00;
        /// <summary>
        /// Minimum várakoziási idő 2db Consecutive frame között.
        /// </summary>
        byte SeparationTime = 0;

        const int MAX_DATA_SIZE = 4096;

        const byte N_PCItypeSingleFrame = 0x00; 
        const byte N_PCItypeFirstFrame = 0x10;
        const byte N_PCItypeConsecutiveFrame = 0x20;
        const byte N_PCItypeFlowControl = 0x30;

        const int MAX_RETRY_COUNT = 3;

        IPhysicalLink _link;

        /********************************************************************************/
        public Iso15765NetwrorkLayer(IPhysicalLink physicalLink)
        { 
            _link = physicalLink;
            ReadTimeoutMs = 6000;
            ParserLog = true;
        }
        /********************************************************************************/
        public void Open( UInt32 transmittId, UInt32 receiveId, UInt32 baudRate)
        {
           
            //TransmittId = transmittId;
            //ReceiveId = receiveId;
            //_adapter = adapter;
            //if (!adapter.IsConnected)
            //    throw new Iso15765Exception("Adapter not Connected!");
            //adapter.Open(baudRate);
        }
        /********************************************************************************/
        /// <summary>
        /// Kérérs mjad az ECU válasza.
        /// Example:
        /// Network.Transport(new byte[] { 0x3E, 0x00 }, out response);
        /// </summary>
        /// <param name="request">Kérés: SID, parameters,</param>
        /// <param name="response">Válasz: Positive/Negative SID, response params.</param>
        public void Transport(byte[] request, out byte[] response)
        {
            byte requestServiceId = request[0];
            int retryCount = 0;
            response = new byte[0];
            bool isDone = false;
            do
            {
                try
                {
                    TransmittRequest(request, ReadTimeoutMs, ref MessageFrameLogContent);
                    ReceiveResponse(out response, SeparationTime, BlockSize, ReadTimeoutMs, ref MessageFrameLogContent);
                    do
                    {
                        if (response[0] == 0x7F)
                        {//Negative Response 
                            //0x7F | ServiceId | Negative Response Code 
                            //ismételd az olvasást, ha kell
                            if (response[2] == Codes.NRC_requestCorrectlyReceived_ResponsePending)
                            {   //Negaitve Response, de ezt követően küldhet még: ismét Response Pendinget, vagy pozitiv/negatív választ.
                                //Többször is küldheti egymás után a vevő...
                                ReceiveResponse(out response, SeparationTime, BlockSize, ReadTimeoutMs, ref MessageFrameLogContent);
                                isDone = false;
                            }
                            else
                            {
                                //Negative Response...
                                throw new Exception("Negative Response: Service Id:" + string.Format("0x{0:X2}", response[1]) + " " + Codes.ServiceIdToString(response[1]) +
                                                    ", " +
                                                    "NRC: " + string.Format("0x{0:X2}", response[2]) + " " + Codes.NegativeResponseCodeToString(response[2]));
                            }
                        }
                        else
                        {
                            isDone = true;
                        }
                    } while (!isDone);
                }
                catch (Iso15765TimeoutException e)
                {
                    if (retryCount != MAX_RETRY_COUNT-1)
                    {
                        retryCount++;
                        if (ParserLog) Console.WriteLine("Retry, count:" + retryCount.ToString());
                    }
                    else
                    {
                        throw e;
                    }
                }
            } while (!isDone);
        }   
        /********************************************************************************/
        public void TransmittRequest(byte[] request, int flowControlTimeout, ref string log)
        {
            if (request.Length >= 4096)
                throw new ArgumentOutOfRangeException("requestData", "Data length is too long.");

            int length = request.Length;
            byte[] frame = new byte[8];
            int offset;
           
            if (length <= 7)
            {
                //SingleFrame(SF) 
                offset = 0;
                for (int i = 0; i < 8; i++)
                    frame[i] = 0xFF;
                //single frame data length
                byte SF_DL = (byte)request.Length;
                frame[offset] = (byte)(N_PCItypeSingleFrame | SF_DL);
                offset++;
                Buffer.BlockCopy(request, 0, frame, offset, request.Length);
                _link.Write(frame, ref log);
                if(ParserLog) Console.WriteLine("TransmittRequest.N_PCItypeSingleFrame->Done");
            }
            else
            {
                //Ha küldő FirstFramet küld, akkor a következő lépésben FlowControllt, kell fogadnia.
                //FirstFrame(FF)
                offset = 0;
                //first frame data length
                UInt16 FF_DL = (UInt16)request.Length;
                frame[0] = (byte)(N_PCItypeFirstFrame | (FF_DL & 0x0F) >> 4);
                frame[1] = (byte)FF_DL;

                Buffer.BlockCopy(request, offset, frame, 2, 6);
                offset += 6;
                _link.Write(frame, ref log);
                if (ParserLog) Console.WriteLine("TransmittRequest.N_PCItypeFirstFrame->Read_N_PCItypeFlowControl.");

                //Read FlowControl
                _link.Read(out frame, flowControlTimeout, ref MessageFrameLogContent);
                if (ParserLog) Console.WriteLine("TransmittRequest.Read_N_PCItypeFlowControl->N_PCItypeConsecutiveFrame.");
                if ((frame[0] & 0xF0) != N_PCItypeFlowControl)
                {
                    throw new Exception("Sequence error, expected flow control frame.");
                }
                else
                {
                    byte blockCnt = 0;
                    byte blockSize = frame[1];
                    byte separationTime = frame[2];
                    byte frameCnt = 0;
                    Console.WriteLine("BS:" + blockSize.ToString() + " frame, STmin:" + separationTime.ToString() + "ms.");
                    do
                    {
                        //separationTime: 00-7F ->> 0-125ms
                        if(separationTime <= 0x7F)
                            System.Threading.Thread.Sleep(separationTime);

                        //Fill 0xFF...
                        for (int i = 0; i < 8; i++)
                            frame[i] = 0xFF;

                        frameCnt++;
                        frame[0] = (byte)(N_PCItypeConsecutiveFrame | (frameCnt & 0x0F));
                        if ((request.Length - offset) >= 7)
                        {
                            Buffer.BlockCopy(request, offset, frame, 1, 7);
                            offset += 7;
                            _link.Write(frame, ref log);
                            if (ParserLog) Console.WriteLine("TransmittRequest.N_PCItypeConsecutiveFrame->N_PCItypeConsecutiveFrame. Full Frame.");
                        }
                        else
                        {
                            Buffer.BlockCopy(request, offset, frame, 1, request.Length - offset);
                            offset += request.Length - offset;
                            _link.Write(frame, ref log);
                            if (ParserLog) Console.WriteLine("TransmittRequest.N_PCItypeConsecutiveFrame->N_PCItypeConsecutiveFrame.");
                        }

                        if (blockSize != 0)
                        {
                            blockCnt++;
                            if (blockCnt >= blockSize)
                            {
                                if (ParserLog) Console.WriteLine("TransmittRequest.N_PCItypeFirstFrame->Read_N_PCItypeFlowControl.");
                                _link.Read(out frame, flowControlTimeout, ref log);
                                if ((frame[0] & 0xF0) != N_PCItypeFlowControl)
                                    throw new Exception("Sequence error, expected flow control frame.");

                                blockCnt = 0;
                                blockSize = frame[1];
                                separationTime = frame[2];
                            }
                        }

                    } while (request.Length - offset != 0);                    
                }
            }
        }
        /********************************************************************************/
        public void ReceiveResponse(out byte[] response, byte separationTime, byte maxBlockSize, int readTimeout, ref string log)
        {
            //Ha a veveő FirsFrame framet kap(ez itt most), akkor a következő lépésben küldenie kell kézéfogást a maxBlockSize-al.
            //maxBlockSize után ismét kell kézfogást küldeni.
            //Vagyis ha fogadok, akkor ez itt mondja meg, hogy hogyan szeretné (maximum framek száma a blockban, framek közötti idő)
            byte[] buffer;
            int offset = 0;
            byte frameCnt = 0;
            byte blockCnt = 0;
            UInt16 length = 0;
            response = new byte[0];
            bool isDone = false;

            do
            {

                _link.Read(out buffer, readTimeout, ref log);

                switch (buffer[0] & 0xF0u)
                {
                    case N_PCItypeSingleFrame:
                        {
                            length = buffer[0];
                            response = new byte[length];
                            Buffer.BlockCopy(buffer, 1, response, 0, length);
                            if (ParserLog) Console.WriteLine("ReceiveResponse.N_PCItypeSingleFrame -> Done.");
                            isDone = true;
                            break;
                        }
                    case N_PCItypeFirstFrame:
                        {
                            length = (UInt16)(((buffer[0] & 0x0F) << 4) | buffer[1]);
                            response = new byte[length];
                            frameCnt = 0;
                            offset = 0;
                            Buffer.BlockCopy(buffer, 2, response, offset, 6);
                            offset += 6;
                            if (ParserLog) Console.WriteLine("ReceiveResponse.N_PCItypeFirstFrame -> SendFlowControl.");
                            _link.Write(new byte[] { (N_PCItypeFlowControl), maxBlockSize, separationTime, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, ref log);
                            if (ParserLog) Console.WriteLine("ReceiveResponse.N_PCItypeFirstFrame -> WaitFor:N_PCItypeConsecutiveFrame.");
                            isDone = false;
                            break;
                        }
                    case N_PCItypeConsecutiveFrame:
                        {
                            frameCnt++;
                            //21 82 00 00 80 5A BF 35
                            //22 85 52 D3 74 42 0F 09
                            //23 A5 B7 4F B8 55 03 09
                            //24 23 F7 0B D3 4C 56 D8
                            //...
                            //2F 3E 8F C7 F6 81 61 D4
                            //20 64 E3 9F 94 9F 62 A4
                            //...
                            if ((frameCnt & 0x0F) != (buffer[0] & 0x0F))
                                throw new Exception("Frame Sequence Error.");

                            if ((length - offset) >= 7)
                            {
                                Buffer.BlockCopy(buffer, 1, response, offset, 7);
                                offset += 7;
                            }
                            else
                            {
                                Buffer.BlockCopy(buffer, 1, response, offset, length - offset);
                                offset += length - offset;
                            }

                            if ((length - offset) == 0)
                            {
                                isDone = true;
                                if (ParserLog) Console.WriteLine("ReceiveResponse.N_PCItypeConsecutiveFrame -> Done");
                            }
                            else
                            {
                                if (maxBlockSize != 0)
                                {
                                    blockCnt++;
                                    if (blockCnt >= maxBlockSize)
                                    {  //Az adott blokkban már több frame nem lehet, most küldök egy FlowControl-t
                                        blockCnt = 0;
                                        if (ParserLog) Console.WriteLine("ReceiveResponse.N_PCItypeFirstFrame -> SendFlowControl.");
                                        _link.Write(new byte[] { (N_PCItypeFlowControl), maxBlockSize, separationTime, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, ref log);
                                        if (ParserLog) Console.WriteLine("ReceiveResponse.N_PCItypeConsecutiveFrame -> WaitFor:N_PCItypeConsecutiveFrame.");
                                    }
                                    else
                                    {
                                        isDone = false;
                                        if (ParserLog) Console.WriteLine("ReceiveResponse.N_PCItypeConsecutiveFrame -> WaitFor:N_PCItypeConsecutiveFrame.");
                                    }
                                }
                                else
                                {
                                    isDone = false;
                                    if (ParserLog) Console.WriteLine("ReceiveResponse.N_PCItypeConsecutiveFrame -> WaitFor:N_PCItypeConsecutiveFrame.");
                                }
                            }

                            break;
                        }
                    default:
                        {
                            throw new Exception("N_PCI not excepted type error.");
                        }
                }

            } while (!isDone);
        }
        /********************************************************************************/
        public void SaveFrameLog(string path)
        {
            WriteFileFromString(path, MessageFrameLogContent);
        }
        /********************************************************************************/
        static void WriteFileFromString(string path, string content)
        {
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                sw.Write(content);
            }
        }

    }
}