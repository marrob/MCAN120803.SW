namespace Konvolucio.MCAN120803.GUI
{
    using System;

    public class AppConstants
    {
        public const string XmlNamespace = @"http://www.konvolucio.hu/mcanx/2016/project/content";
        public const string XmlRootElement = "mcanxProject";
        public const int MaxProductNameLenght = 20;
        public const int MaxProductVersionLenght = 20;
        public const int MaxProductCodeLenght = 20;
        public const int MaxCustomerNameLenght = 20;
        public const int MaxCustomerCodeLenght = 20;
        public const string NewFileName = "Untitled";
        public const string FileExtension = ".mcanx";
        public const string ValueNotAvailable2 = "n/a";
        public const string InvalidFlieNameChar = "A file name can't contain any of flowing characters:";


        [Obsolete("Használd helyette a Application.CompanyName")]
        public const string SoftwareCustomer = "Konvolúció Bt.";

        [Obsolete("Használd helyette a Application.ProductName ")]
        public const string SoftwareTitle = "MCAN120803 CAN Bus Tool";
        
        public const string FileFilter = "CAN Bus Tool Project File - (*.mcanx)|*.mcanx";


        public const string GenericTimestampFormat = "yyyy.MM.dd HH:mm:ss.fff";
        public static readonly string DefaultTimestampFormat = TemplateTimestampFormats[0];
        
        public const string FileNameTimestampFormat = "yyMMdd HHmmss";
        public static string[] TemplateTimestampFormats
        {
            get
            {
                return new string[]
                {
                    "HH:mm:ss.fff",
                    "yyyy.MM.dd HH:mm:ss.fff",
                    @"MM/dd/yyyy HH:mm:ss",
                    @"MM-dd-yyyy HH:mm:ss",
                    @"MM/dddd/yy H:mm:s",
                    "yyyy.MM.dd. HH:mm:ss",
                    "MM/dd/yy H:mm:ss",
                    "MM/dd/yy H:mm:ss",
                    @"MM-dd-yyyy_HH:mm:ss"
                };
            }
        }

        public static readonly string DefaultDataFormat = TemplateDataFormats[0];
        public static string[] TemplateDataFormats
        {
            get
            {
                return new string[]
                {
                    "{0:X2} ",         /*Sample:"A1 B2 C3" */
                    "{0:X2},",         /*Sample:"A1,B2,C3"*/
                    "0x{0:X2}, ",      /*Sample:"0xA1, 0xB2, 0xC3"*/
                };
            }
        }

        public static readonly string DefaultArbitrationIdFormat = Converters.ArbitrationIdConverter.TemplateFormats[0];

        public const string PleaseAdapterSelecte = "Please Select Adapter";


    }
}
