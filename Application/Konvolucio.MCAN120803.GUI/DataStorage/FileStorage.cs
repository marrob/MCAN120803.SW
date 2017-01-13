
namespace Konvolucio.MCAN120803.GUI.DataStorage
{
    using System;
    using System.IO;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    using WinForms.Framework;

    public partial class Storage
    {
        public class FileStorage
        {
            [XmlElement("Version")]
            public string FileVersion { get; set; }

            [XmlElement("ProjectComment")]
            public string ProjectComment { get; set; }

            [XmlElement("Settings")]
            public StroageParameters Parameters { get; set; }

            [XmlArray("Filters")]
            [XmlArrayItem("Filter", typeof(StorageMessageFilterItem))]
            public StorageMessageFilterCollection Filters { get; set; }

            [XmlArray("CustomArbIdColumns")]
            [XmlArrayItem("CustomArbIdColumn", typeof(StorageCustomArbIdColumnItem))]
            public StorageCustomArbIdColumnCollection CustomArbIdColumns { get; set; }

            [XmlArray("TraceGridLayout")]
            [XmlArrayItem("ColumnLayout", typeof(ColumnLayoutItem))]
            public ColumnLayoutCollection TraceGridLayout { get; set; }

            [XmlArray("LogGridLayout")]
            [XmlArrayItem("ColumnLayout", typeof(ColumnLayoutItem))]
            public ColumnLayoutCollection LogGridLayout { get; set; }

            [XmlArray("StatisticsGridLayout")]
            [XmlArrayItem("ColumnLayout", typeof(ColumnLayoutItem))]
            public ColumnLayoutCollection StatisticsGridLayout { get; set; }

            [XmlArray("FilterGridLayout")]
            [XmlArrayItem("ColumnLayout", typeof(ColumnLayoutItem))]
            public ColumnLayoutCollection FilterGridLayout { get; set; }

            [XmlArray("Tools")]
            public StorageToolCollection Tools { get; set; }

            [XmlArray("ToolsLayouts")]
            [XmlArrayItem("ColumnLayoutCollection", typeof(ColumnLayoutCollection))]
            public List<ColumnLayoutCollection> ToolsLayouts { get; set; }

            private static Type[] SupportedTypes
            {
                get
                {
                    return new Type[]
                    {
                        typeof(StroageParameters),
                        typeof(StorageCanTxMessageItem),
                        typeof(StorageCanTxMessageCollection),
                        typeof(StorageMessageFilterCollection),
                        typeof(StorageMessageFilterItem),
                        typeof(ColumnLayoutCollection),
                        typeof(ColumnLayoutItem),
                        typeof(StorageCustomArbIdColumnItem),
                        typeof(StorageCustomArbIdColumnCollection),
                        typeof(StorageToolCollection),
                    };
                }
            }

            /// <summary>
            /// Konstruktor
            /// </summary>
            public FileStorage()
            {
                Parameters = new StroageParameters();
                Filters = new StorageMessageFilterCollection();
                CustomArbIdColumns = new StorageCustomArbIdColumnCollection();
                TraceGridLayout = new ColumnLayoutCollection();
                LogGridLayout = new ColumnLayoutCollection();
                StatisticsGridLayout = new ColumnLayoutCollection();
                FilterGridLayout = new ColumnLayoutCollection();
                Tools = new StorageToolCollection();
                ToolsLayouts = new List<ColumnLayoutCollection>();
            }

            /// <summary>
            /// Új project
            /// </summary>
            public void New()
            {
                Filters.Clear();
                CustomArbIdColumns.Clear();
                TraceGridLayout.Clear();
                LogGridLayout.Clear();
                StatisticsGridLayout.Clear();
                FilterGridLayout.Clear();
                Tools.Clear();
                ToolsLayouts.Clear();

                FileVersion = "1.0.0.0";
                Parameters.New();
                Filters.New();
                Tools.New();
            }

            /// <summary>
            /// Fájlba mentés.
            /// </summary>
            /// <param name="path"></param>
            public void SaveToFile(string path)
            {
                var xmlFormat = new XmlSerializer(typeof(FileStorage), null, SupportedTypes, new XmlRootAttribute(AppConstants.XmlRootElement), AppConstants.XmlNamespace);
                using (Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    xmlFormat.Serialize(fStream, this);
                }
            }

            /// <summary>
            /// Betöltés fájlból.
            /// </summary>
            /// <param name="path"></param>
            public void LoadFromFile(string path)
            {
                var xmlFormat = new XmlSerializer(typeof(FileStorage), null, SupportedTypes, new XmlRootAttribute(AppConstants.XmlRootElement), AppConstants.XmlNamespace);
                FileStorage instance;
                using (Stream fStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    instance = (FileStorage)xmlFormat.Deserialize(fStream);
                }
                FileVersion = instance.FileVersion;
                Parameters = instance.Parameters;
                Filters = instance.Filters;
                CustomArbIdColumns = instance.CustomArbIdColumns;
                TraceGridLayout = instance.TraceGridLayout;
                LogGridLayout = instance.LogGridLayout;
                StatisticsGridLayout = instance.StatisticsGridLayout;
                FilterGridLayout = instance.FilterGridLayout;
                Tools = instance.Tools;
                ToolsLayouts = instance.ToolsLayouts;
            }
        }
    }
}
