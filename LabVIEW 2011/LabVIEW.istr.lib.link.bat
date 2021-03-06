SET log=^> "%TEMP%\log.txt"
set appSrcDir="D:\@@@!ProjectS\MCAN120803\Software.Setup\Resource\Application"
set srcDir="D:\@@@!ProjectS\MCAN120803\Software\LabVIEW 2011\Konvolucio MCAN120803"
set destDir="c:\Program Files\National Instruments\LabVIEW 2011\instr.lib\Konvolucio MCAN120803"

mkdir  %destDir%"\Konvolucio MCAN120803"
mklink %destDir%"\Konvolucio MCAN120803.lvproj"							%srcDir%"\Konvolucio MCAN120803.lvproj"
mklink %destDir%"\Konvolucio MCAN120803.lvlib"							%srcDir%"\Konvolucio MCAN120803.lvlib"
mklink %destDir%"\dir.mnu"												%srcDir%"\dir.mnu"

mkdir  %destDir%"\Examples"
mklink %destDir%"\Examples\Konvolucio MCAN120803 Example Tool.ini"							%srcDir%"\Examples\Konvolucio MCAN120803 Example Tool.ini"
mklink %destDir%"\Examples\Konvolucio MCAN120803 Example Tool.vi"							%srcDir%"\Examples\Konvolucio MCAN120803 Example Tool.vi"
mklink %destDir%"\Examples\Konvolucio MCAN120803 Example_0001 Connect.vi"					%srcDir%"\Examples\Konvolucio MCAN120803 Example_0001 Connect.vi"
mklink %destDir%"\Examples\Konvolucio MCAN120803 Example_0002 ConnectTo.vi"					%srcDir%"\Examples\Konvolucio MCAN120803 Example_0002 ConnectTo.vi"
mklink %destDir%"\Examples\Konvolucio MCAN120803 Example_0003 ConnectBySerialNumber.vi"		%srcDir%"\Examples\Konvolucio MCAN120803 Example_0003 ConnectBySerialNumber.vi"
mklink %destDir%"\Examples\Konvolucio MCAN120803 Example_0004 Write.vi"						%srcDir%"\Examples\Konvolucio MCAN120803 Example_0004 Write.vi"
mklink %destDir%"\Examples\Konvolucio MCAN120803 Example_0005 WriteSingleFrame.vi"			%srcDir%"\Examples\Konvolucio MCAN120803 Example_0005 WriteSingleFrame.vi"
mklink %destDir%"\Examples\Konvolucio MCAN120803 Example_0006 Read.vi"						%srcDir%"\Examples\Konvolucio MCAN120803 Example_0006 Read.vi"

mkdir  %destDir%"\Private"
mklink %destDir%"\Private\dotNetExceptionStringWrapper.vi"									%srcDir%"\Private\dotNetExceptionStringWrapper.vi"
mklink %destDir%"\Private\dotNetExceptionWrapper.vi"										%srcDir%"\Private\dotNetExceptionWrapper.vi"
mklink %destDir%"\Private\dotNetTick2LvTimestamp.vi"										%srcDir%"\Private\dotNetTick2LvTimestamp.vi"
mklink %destDir%"\Private\ErrorHandler.vi"													%srcDir%"\Private\ErrorHandler.vi"
mklink %destDir%"\Private\ErrorHelper.vi"													%srcDir%"\Private\ErrorHelper.vi"
mklink %destDir%"\Private\IsNullReferenceCheck.vi"											%srcDir%"\Private\IsNullReferenceCheck.vi"
mklink %destDir%"\Private\Konvolucio.MCAN120803.API.dll"									%appSrcDir%"\Konvolucio.MCAN120803.API.dll"
mklink %destDir%"\Private\Konvolucio.WinUSB.dll"											%appSrcDir%"\Konvolucio.WinUSB.dll"

mkdir  %destDir%"\Public"

mkdir  %destDir%"\Public\Attributes"
mklink %destDir%"\Public\Attributes\AttrGet.vi"												%srcDir%"\Public\Attributes\AttrGet.vi"	
mklink %destDir%"\Public\Attributes\AttrGetAssemblyVersion.vi"								%srcDir%"\Public\Attributes\AttrGetAssemblyVersion.vi"
mklink %destDir%"\Public\Attributes\AttrGetBaudrate.vi"										%srcDir%"\Public\Attributes\AttrGetBaudrate.vi"	
mklink %destDir%"\Public\Attributes\AttrGetFilter.vi"										%srcDir%"\Public\Attributes\AttrGetFilter.vi"	
mklink %destDir%"\Public\Attributes\AttrGetFirmwareRev.vi"									%srcDir%"\Public\Attributes\AttrGetFirmwareRev.vi"	
mklink %destDir%"\Public\Attributes\AttrGetIsConnected.vi"									%srcDir%"\Public\Attributes\AttrGetIsConnected.vi"	
mklink %destDir%"\Public\Attributes\AttrGetIsOpen.vi"										%srcDir%"\Public\Attributes\AttrGetIsOpen.vi"	
mklink %destDir%"\Public\Attributes\AttrGetListenOnly.vi"									%srcDir%"\Public\Attributes\AttrGetListenOnly.vi"	
mklink %destDir%"\Public\Attributes\AttrGetLoopback.vi"										%srcDir%"\Public\Attributes\AttrGetLoopback.vi"	
mklink %destDir%"\Public\Attributes\AttrGetMask.vi"											%srcDir%"\Public\Attributes\AttrGetMask.vi"	
mklink %destDir%"\Public\Attributes\AttrGetNonAutoRetransmission.vi"						%srcDir%"\Public\Attributes\AttrGetNonAutoRetransmission.vi"	
mklink %destDir%"\Public\Attributes\AttrGetPcbRev.vi"										%srcDir%"\Public\Attributes\AttrGetPcbRev.vi"	
mklink %destDir%"\Public\Attributes\AttrGetRxDrop.vi"										%srcDir%"\Public\Attributes\AttrGetRxDrop.vi"	
mklink %destDir%"\Public\Attributes\AttrGetRxErrorCounter.vi"								%srcDir%"\Public\Attributes\AttrGetRxErrorCounter.vi"	
mklink %destDir%"\Public\Attributes\AttrGetRxPendingMsg.vi"									%srcDir%"\Public\Attributes\AttrGetRxPendingMsg.vi"	
mklink %destDir%"\Public\Attributes\AttrGetRxTotal.vi"										%srcDir%"\Public\Attributes\AttrGetRxTotal.vi"	
mklink %destDir%"\Public\Attributes\AttrGetSerialNumber.vi"									%srcDir%"\Public\Attributes\AttrGetSerialNumber.vi"	
mklink %destDir%"\Public\Attributes\AttrGetState.vi"										%srcDir%"\Public\Attributes\AttrGetState.vi"	
mklink %destDir%"\Public\Attributes\AttrGetTermination.vi"									%srcDir%"\Public\Attributes\AttrGetTermination.vi"	
mklink %destDir%"\Public\Attributes\AttrGetTxDrop.vi"										%srcDir%"\Public\Attributes\AttrGetTxDrop.vi"	
mklink %destDir%"\Public\Attributes\AttrGetTxErrorCounter.vi"								%srcDir%"\Public\Attributes\AttrGetTxErrorCounter.vi"	
mklink %destDir%"\Public\Attributes\AttrGetTxPendingMsg.vi"									%srcDir%"\Public\Attributes\AttrGetTxPendingMsg.vi"	
mklink %destDir%"\Public\Attributes\AttrGetTxTotal.vi"										%srcDir%"\Public\Attributes\AttrGetTxTotal.vi"	
mklink %destDir%"\Public\Attributes\attributes.mnu"											%srcDir%"\Public\Attributes\attributes.mnu"	
mklink %destDir%"\Public\Attributes\AttrSet.vi"												%srcDir%"\Public\Attributes\AttrSet.vi"	
mklink %destDir%"\Public\Attributes\AttrSetBaudrate.vi"										%srcDir%"\Public\Attributes\AttrSetBaudrate.vi"	
mklink %destDir%"\Public\Attributes\AttrSetFilter.vi"										%srcDir%"\Public\Attributes\AttrSetFilter.vi"	
mklink %destDir%"\Public\Attributes\AttrSetListenOnly.vi"									%srcDir%"\Public\Attributes\AttrSetListenOnly.vi"	
mklink %destDir%"\Public\Attributes\AttrSetLoopback.vi"										%srcDir%"\Public\Attributes\AttrSetLoopback.vi"	
mklink %destDir%"\Public\Attributes\AttrSetMask.vi"											%srcDir%"\Public\Attributes\AttrSetMask.vi"	
mklink %destDir%"\Public\Attributes\AttrSetNonAutoRetransmission.vi"						%srcDir%"\Public\Attributes\AttrSetNonAutoRetransmission.vi"	
mklink %destDir%"\Public\Attributes\AttrSetTermination.vi"									%srcDir%"\Public\Attributes\AttrSetTermination.vi"	

mkdir  %destDir%"\Public\Configure"
mklink %destDir%"\Public\Configure\Close.vi"												%srcDir%"\Public\Configure\Close.vi"	
mklink %destDir%"\Public\Configure\configure.mnu"											%srcDir%"\Public\Configure\configure.mnu"
mklink %destDir%"\Public\Configure\Connect.vi"												%srcDir%"\Public\Configure\Connect.vi"
mklink %destDir%"\Public\Configure\ConnectBySerialNumber.vi"								%srcDir%"\Public\Configure\ConnectBySerialNumber.vi"
mklink %destDir%"\Public\Configure\ConnectTo.vi"											%srcDir%"\Public\Configure\ConnectTo.vi"
mklink %destDir%"\Public\Configure\Disconnect.vi"											%srcDir%"\Public\Configure\Disconnect.vi"
mklink %destDir%"\Public\Configure\Flush.vi"												%srcDir%"\Public\Configure\Flush.vi"
mklink %destDir%"\Public\Configure\GetAdapters.vi"											%srcDir%"\Public\Configure\GetAdapters.vi"
mklink %destDir%"\Public\Configure\GetBaudrates.vi"											%srcDir%"\Public\Configure\GetBaudrates.vi"
mklink %destDir%"\Public\Configure\Open.vi"													%srcDir%"\Public\Configure\Open.vi"

mkdir  %destDir%"\Public\Data"
mklink %destDir%"\Public\Data\Data.mnu"														%srcDir%"\Public\Data\Data.mnu"	
mklink %destDir%"\Public\Data\Read.vi"														%srcDir%"\Public\Data\Read.vi"	
mklink %destDir%"\Public\Data\Write.vi"														%srcDir%"\Public\Data\Write.vi"	
mklink %destDir%"\Public\Data\WriteSingleFrame.vi"											%srcDir%"\Public\Data\WriteSingleFrame.vi"	

mkdir  %destDir%"\Public\Services"
mklink %destDir%"\Public\Services\Reset.vi"													%srcDir%"\Public\Services\Reset.vi"
mklink %destDir%"\Public\Services\services.mnu"												%srcDir%"\Public\Services\services.mnu"
mklink %destDir%"\Public\Services\Start.vi"													%srcDir%"\Public\Services\Start.vi"
mklink %destDir%"\Public\Services\Stop.vi"													%srcDir%"\Public\Services\Stop.vi"

mkdir  %destDir%"\Public\Tools"
mklink %destDir%"\Public\Tools\Tools.mnu"													%srcDir%"\Public\Tools\Tools.mnu"
mklink %destDir%"\Public\Tools\HexStringArrayToByteArray.vi"								%srcDir%"\Public\Tools\HexStringArrayToByteArray.vi"
mklink %destDir%"\Public\Tools\ByteArrayToHexStringArray.vi"								%srcDir%"\Public\Tools\ByteArrayToHexStringArray.vi"

mkdir  %destDir%"\Types"
mklink %destDir%"\Types\StateType.ctl"														%srcDir%"\Types\StateType.ctl"
mklink %destDir%"\Types\MessageType.ctl"													%srcDir%"\Types\MessageType.ctl"
mklink %destDir%"\Types\MCanHandleType.ctl"													%srcDir%"\Types\MCanHandleType.ctl"
mklink %destDir%"\Types\BaudRateItemType.ctl"												%srcDir%"\Types\BaudRateItemType.ctl"
mklink %destDir%"\Types\AdapterItemType.ctl"												%srcDir%"\Types\AdapterItemType.ctl"

set /p DUMMY=Hit ENTER to continue...