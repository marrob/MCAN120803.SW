using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvolucio.MUDS150628
{
    class Codes
    {
        public const byte NRC_requestCorrectlyReceived_ResponsePending = 0x78;

        /********************************************************************************/
        /// <summary>
        /// Negative Response Code To String
        /// </summary>
        /// <param name="nrc">Negative Response Code</param>
        /// <returns></returns>
        internal static string NegativeResponseCodeToString(byte nrc)
        {
            switch (nrc)
            {
                case 0x10: { return "generalReject"; } //0
                case 0x11: { return "serviceNotSupported"; }//1
                case 0x12: { return "subFunctionNotSupported"; }//2
                case 0x13: { return "incorrectMessageLengthOrInvalidFormat"; }//3
                case 0x14: { return "responseTooLong"; }//4
                case 0x21: { return "busyRepeatRequest"; }//5
                case 0x22: { return "conditionsNotCorrect"; }//6
                case 0x24: { return "requestSequenceError"; }//7
                case 0x25: { return "noResponseFromSubnetComponent"; }//8
                case 0x26: { return "failurePreventsExecutionOfRequestedAction"; }//9
                case 0x31: { return "requestOutOfRange"; }//10
                case 0x33: { return "securityAccessDenied"; }//11
                case 0x35: { return "invalidKey"; }//12
                case 0x36: { return "exceedNumberOfAttempts"; }//13
                case 0x37: { return "requiredTimeDelayNotExpired"; }//14
                case 0x70: { return "uploadDownloadNotAccepted"; }//15
                case 0x71: { return "transferDataSuspended"; }//16
                case 0x72: { return "generalProgrammingFailure"; }//17
                case 0x73: { return "wrongBlockSequenceCounter"; }//18
                //the request is received well and allowed, but the VU needs more time and
                //"ResponsePending" Messages will be send by the VU until final "PositiveResponse" or
                //"NegativeResponse"
                case 0x78: { return "requestCorrectlyReceived_ResponsePending"; }//19
                case 0x7E: { return "subFunctionNotSupportedInActiveSession"; }//20
                case 0x7F: { return "serviceNotSupportedInActiveSession"; }//21
                case 0x81: { return "rpmTooHigh"; }//22
                case 0x82: { return "rpmTooLow"; }//23
                case 0x83: { return "engineIsRunning"; }//24
                case 0x84: { return "engineIsNotRunning"; }//25
                case 0x85: { return "engineRunTimeTooLow"; }//26
                case 0x86: { return "temperatureTooHigh"; }//27
                case 0x87: { return "temperatureTooLow"; }//28
                case 0x88: { return "vehicleSpeedTooHigh"; }//29
                case 0x89: { return "vehicleSpeedTooLow"; }//30
                case 0x8A: { return "throttle/PedalTooHigh"; }//31
                case 0x8B: { return "throttle/PedalTooLow"; }//32
                case 0x8C: { return "transmissionRangeNotInNeutral"; }//33
                case 0x8D: { return "transmissionRangeNotInGear"; }//34
                case 0x8F: { return "brakeSwitch(es)NotClosed"; }//35
                case 0x90: { return "shifterLeverNotInPark"; }//36
                case 0x91: { return "torqueConverterClutchLocked"; }//37
                case 0x92: { return "voltageTooHigh"; }//38
                case 0x93: { return "voltageTooLow"; }//39
                default: { return "undefined";}
            }
        }

        /********************************************************************************/
        /// <summary>
        /// SID to Diagnostics Serivce Name
        /// (ISO 14229-1)
        /// </summary>
        /// <param name="sid">Serivce Id</param>
        /// <returns></returns>
        internal static string ServiceIdToString(byte sid)
        {
            switch (sid)
            { 
                //Diagnostics and Communication Managment Functional Unit
                case 0x10: { return "DiagnosticsSessionControl";}
                case 0x11: { return "ECUReset"; }
                case 0x27: { return "SecurityAcess"; }
                case 0x28: { return "CommunicationControl"; }
                case 0x3E: { return "TesterPresent"; }
                case 0x84: { return "SecuredDataTransmission"; }
                case 0x85: { return "ControlDTCSetting"; }
                case 0x86: { return "ResponseOnEvent"; }
                case 0x87: { return "LinkContorl"; }
                //Data Transmission Functionl Unit
                case 0x22: { return "ReadDataByIdentifier"; }
                case 0x23: { return "ReadMemoryByAddress"; }
                case 0x24: { return "ReadScalingDataByIdentifier"; }
                case 0x2A: { return "ReadDataByPeriodicalIdentifer"; }
                case 0x2C: { return "DynamicallyDefineDataIdentifer"; }
                case 0x2E: { return "WriteDataByIdentifer"; }
                case 0x3D: { return "WriteMemoryByAddress"; }
                //Stored Data Transmission Functional Unit
                case 0x19: { return "ReadDTCInformation"; }
                case 0x14: { return "ClearDiagnosticsInformation"; }
                //Input/Output Control Functional Unit
                case 0x2F: { return "InputOutputControlByIdentifier"; }
                //Remote Activation Of Routine Functional Unit
                case 0x31: { return "RoutineControl"; }
                //Upload/Download Functional Unit
                case 0x34: { return "RequestDwonload"; }
                case 0x35: { return "RequestUpload"; }
                case 0x36: { return "TransferData";}
                default: { return "undefined"; }            
            }
        
        }
        /********************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        internal static string RoutineControlTypeToString(byte rct)
        {
            switch (rct)
            {
                case 0x01: { return "startRoutine"; }

                default: { return "undefined"; }
            }
        }
        /********************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        internal static string RoutineIdToString(UInt16 rid)
        {
            switch (rid)
            {
                case 0x0180: { return "RemoteTachographCardDataTransfer"; }

                default: { return "undefined"; }
            }
        }
    }

    internal class FmsRoutine
    {
        const byte ServiceId = 0x31;
        const byte RoutineControlType = 0x01; //StartRutine
        readonly byte[] RoutineIdentifier = new byte[]  { 0x01, 0x80 };
        byte[] _routineControlOptions;
        /********************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public FmsRoutine(byte[] routineControlOptions)
        {

        }
        /********************************************************************************/
        public byte[] GetBytes()
        { 
            byte[] data = new byte[1 + 1 + RoutineIdentifier.Length + _routineControlOptions.Length];
            int index = 0;
            data[index] = ServiceId;
            index++;
            data[index] = RoutineControlType;
            index++;
            Buffer.BlockCopy(RoutineIdentifier, 0, data, index, RoutineIdentifier.Length);
            index += RoutineIdentifier.Length;
            Buffer.BlockCopy(_routineControlOptions, 0, data, index, _routineControlOptions.Length);
            index += _routineControlOptions.Length;
            return data;
        }

        internal class RoutineControlOption
        {
            /// <summary>
            /// 0x01
            /// </summary>
            public const byte RemoteCompanyCardReady = 0x01;
            /// <summary>
            /// 0x03
            /// </summary>
            public const byte CompanyCardToVUData = 0x03;
            /// <summary>
            /// 0x07
            /// </summary>
            public const byte RemoteDownloadDataRequest = 0x07;
            /// <summary>
            /// 0x09
            /// </summary>
            public const byte CloseRemoteAuthentication = 0x09;
            /********************************************************************************/
            /// <summary>
            /// FMS routineControlOptionRecord
            /// The user optional routineStatusRecord (251 bytes max) is defined by this document (see section V.1.3.)
            /// </summary>
            /// <param name="rid"></param>
            /// <returns></returns>
            internal static string FmsRoutinOptionRecrod(byte[] fror)
            {
                string retval = string.Empty;

                switch (fror[0])
                {
                    case 0x01:
                        {
                            retval = "RemoteCompanyCardReady, ATR:";
                            break;
                        }
                    case 0x03:
                        {
                            retval = "CompanyCardToVUData";
                            break;
                        }
                    case 0x07:
                        {
                            retval = "RemoteDownloadDataRequest";
                            break;
                        }
                    case 0x09:
                        {
                            retval = "CloseRemoteAuthentication";
                            break;
                        }
                    default: { return "undefined"; }
                }
                return retval;
            }
        
        }
        internal class StatusRecord
        {
            /// <summary>
            /// 0x02
            /// </summary>
            public const byte VUReady = 0x02;
            /// <summary>
            /// 0x04
            /// </summary>
            public const byte VUToCompanyCardData = 0x04;
            /// <summary>
            /// 0x06
            /// </summary>
            public const byte RemoteAuthenticationSucceeded = 0x06;
            /// <summary>
            /// 0x08
            /// </summary>
            public const byte RemoteDownloadAccessGranted = 0x08;
            /// <summary>
            /// 0x0A
            /// </summary>
            public const byte RemoteAuthenticationIsClosed = 0x0A;
            /// <summary>
            /// 0x0C
            /// </summary>
            public const byte APDUError = 0x0C;
            /// <summary>
            /// 0x0E
            /// </summary>
            public const byte AuthenticationError = 0x0E;
            /// <summary>
            /// 0x10
            /// </summary>
            public const byte TooManyAuthenticationErrors = 0x10;

            /********************************************************************************/
            /// <summary>
            /// FleetManagmentSystem
            /// RoutineControl positive response messages shall be used to transmit data from the VU to the remote company
            /// card, with subfunction set to 0x01 (StartRoutine) and routineIdentifier set to 0x0180
            /// (RemoteTachographCardDataTransfer).
            /// </summary>
            /// <param name="fror"></param>
            /// <returns></returns>
            internal static string FmsRoutinStatusRecrod(byte[] fror)
            {
                string retval = string.Empty;

                switch (fror[0])
                {
                    case 0x02:
                        {
                            retval = "VUReady";
                            break;
                        }
                    case 0x04:
                        {
                            retval = "VUToCompanyCardData";
                            break;
                        }
                    case 0x06:
                        {
                            retval = "RemoteAuthenticationSucceeded";
                            break;
                        }
                    case 0x08:
                        {
                            retval = "RemoteDownloadAccessGranted";
                            break;
                        }
                    case 0x0A:
                        {
                            retval = "RemoteAuthenticationIsClosed";
                            break;
                        }
                    case 0x0C:
                        {
                            retval = "APDUError";
                            break;
                        }
                    case 0x0E:
                        {
                            retval = "AuthenticationError";
                            break;
                        }
                    case 0x10:
                        {
                            retval = "TooManyAuthenticationErrors";
                            break;
                        }
                    default: { return "undefined"; }
                }
                return retval;
            }
        }
        
    }
}
