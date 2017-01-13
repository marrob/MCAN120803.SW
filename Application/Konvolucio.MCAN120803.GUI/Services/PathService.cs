

namespace Konvolucio.MCAN120803.GUI.Services
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Configuration;
    using System.IO;
    using Properties;

    public interface IPathService
    {
        bool FirstStart { get; }
        string WorkspacePath { get; }
        string BrowseLocation { get; }
        string CreateNewProjectPath(string location, string productName, string fileName, bool subFolderEnabled);
    }

    public class PathService : IPathService
    {
        public const string DefaultMyDocumentsSubdirectory = "\\Konvolucio";
        public const string DefaultBrowseFolder = "\\MCAN120803";
        public const string DefaultAppSamplesFolder = "\\Sample Projects";
        public const string DefaultWorkspaceDirectory = "\\Wokrspace";


        public static IPathService Instance { get { return _pathService; } }
        static IPathService _pathService = new PathService();

        public bool FirstStart
        {
            get { return Settings.Default.IsFirstStart; }
            set { Settings.Default.IsFirstStart = value; }
        }

        public string WorkspacePath { get { return _workspacePath; } }
        string _workspacePath;

        public string BrowseLocation { get { return _browseLoaction; } }
        string _browseLoaction;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PathService()
        {
            var settingsFilePath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
            var settignsLocation = System.IO.Path.GetDirectoryName(settingsFilePath);
            // C:\Users\Margit Róbert\AppData\Local\Konvolucio\Konvolucio.CanMonitor.exe_Url_bximlhn52hlghyphytgjcscr4syqy4gl\Wokrspace
            _workspacePath = settignsLocation + DefaultWorkspaceDirectory;
            if (Directory.Exists(Path.GetDirectoryName(_workspacePath)) == false)
                Directory.CreateDirectory(Path.GetDirectoryName(_workspacePath));

            var documentsLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            
            if (FirstStart)
            {
                // C:\Users\Margit Róbert\Documents\Konvolucio\MCAN120803\Samples\
                _browseLoaction = documentsLocation + DefaultMyDocumentsSubdirectory + DefaultBrowseFolder + DefaultAppSamplesFolder;
                Settings.Default.BrowseLocation = BrowseLocation;
                FirstStart = false;
                Settings.Default.Save();
            }
            else
            {
                _browseLoaction = Settings.Default.BrowseLocation;
            }

        }

        /// <summary>
        /// Két utvonalat hasonlít össze, ha a felhasználó definál egy sajátot az lesz a nyerő.
        /// TODO: PathService-ben a helye
        /// </summary>
        /// <param name="location"></param>
        /// <param name="productName"></param>
        /// <param name="fileName"></param>
        /// <param name="subFolderEnabled"></param>
        /// <returns></returns>
        public string CreateNewProjectPath(string location, string productName, string fileName, bool subFolderEnabled)
        {
            productName = productName.Trim();
            fileName = fileName.Trim();

            string autoPath;
            string userPath;

            if (subFolderEnabled)
            {
                string temp = location + "\\" + productName + "\\" + productName + AppConstants.FileExtension;
                autoPath = temp;
            }
            else
            {
                string temp = location + "\\" + productName + AppConstants.FileExtension;
                autoPath = temp;
            }

            if (subFolderEnabled)
            {
                string temp = location + "\\" + fileName + "\\" + fileName + AppConstants.FileExtension;
                userPath = temp;
            }
            else
            {
                string temp = location + "\\" + fileName + AppConstants.FileExtension;
                userPath = temp;
            }

            if (userPath != autoPath)
            {
                return userPath;
            }
            else
                return autoPath;
        }
    }
}
