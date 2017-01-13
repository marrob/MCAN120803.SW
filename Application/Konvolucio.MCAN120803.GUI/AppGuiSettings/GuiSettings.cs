

namespace Konvolucio.MCAN120803.GuiSettings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;
    using System.Xml;
    using System.Xml.Serialization;
    using System.IO;
    using Konvolucio.MCAN120803.GUI.Properties;

    public partial class AppGuiSettings
    {
      
        public const string XmlRootElement = "mcanxguicfg";
        public const string FileExtension = ".mcanxguicfg";
        public const string XmlNamespace = @"http://www.konvolucio.hu/mcanxguicfg/2016/project/content";

        [XmlIgnore]
        public string Path { get; private set; }

        public event EventHandler Loaded;
        public event EventHandler Loading;
        public event EventHandler Saving;
        public event EventHandler Saved;

        public static readonly AppGuiSettings instance = new AppGuiSettings();
        public static AppGuiSettings Instance { get { return instance; } }


        [XmlElement("Log")]
        public LogGuiSettings Log { get; set; }

        [XmlIgnore]
        public Type[] SupportedTypes
        {
            get
            {
                return new Type[]
                    {
                        typeof(LogGuiSettings),
                    };
            }
        }

        public AppGuiSettings()
        {
            Log = new LogGuiSettings();
        }

        public void Load(string path)
        {
            string filename = System.IO.Path.GetFileNameWithoutExtension(path);
            string directory = System.IO.Directory.GetDirectoryRoot(path);
            string cfpath = directory + filename + FileExtension;

            if (!System.IO.File.Exists(cfpath))
            {
                SaveToFile(cfpath);
                Log.SetDefault();
                Path = cfpath;
            }
        }

        public void Save()
        {
            if (Path == null)
            {
                throw new Exception("Please Load First.");
            }

            if (!System.IO.File.Exists(Path))
            {
                SaveToFile(Path);
            }
        }

        void SaveToFile(string path)
        {
            var xmlFormat = new XmlSerializer(typeof(AppGuiSettings), null, SupportedTypes, new XmlRootAttribute(XmlRootElement), XmlNamespace);
            using (Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlFormat.Serialize(fStream, this);
            }
        }

        AppGuiSettings LoadFromFile(string path)
        {
            var xmlFormat = new XmlSerializer(typeof(AppGuiSettings), null, SupportedTypes, new XmlRootAttribute(XmlRootElement), XmlNamespace);
            AppGuiSettings instance;
            using (Stream fStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                instance = (AppGuiSettings)xmlFormat.Deserialize(fStream);
            }

            return instance;
        }

        protected void OnSaving()
        {
            if (Saving != null)
                Saving(this, EventArgs.Empty);
        }

        protected void OnSaved()
        {
            if (Saved != null)
                Saved(this, EventArgs.Empty);
        }

        protected void OnLoaded()
        {
            if (Loaded != null)
                Loaded(this, EventArgs.Empty);
        }

        protected void OnLoading()
        {
            if (Loading != null)
                Loading(this, EventArgs.Empty);
        }

    }
}
