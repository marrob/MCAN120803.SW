// -----------------------------------------------------------------------
// <copyright file="LogFileNameTreeView.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Log.TreeNodes
{
    using System;
    using System.Windows.Forms; /*TreeNode*/

    using Model;
    using View;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LogFileNameTreeNode : TreeNode
    {
        public string Guid
        {
            get { return Log.Guid; }
        }

        public ILogFileItem Log { get; private set; }
        public ILogFileCollection Logs { get; private set; }

        public LogFileNameTreeNode(ILogFileCollection logs, ILogFileItem log, ILogDescriptionView description, TreeNode[] subNodes)
        {
            Text = log.Name;
            Nodes.AddRange(subNodes);
            Logs = logs;
            Log = log;
            SelectedImageKey = ImageKey = @"database16";
            Name = log.Guid; /* Figyelem ez kell a az elem törléséhez. */
            log.PropertyChanged += (s, e) => { Text = log.Name; };
            log.Info.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == "Description") description.Content = log.Info.Description;
            };

        }

        public void Load()
        {
            try
            {
                EventAggregator.Instance.Publish<LogFileAppEvent>(new LogFileAppEvent(Log, FileChangingType.Loading));
                Log.Load();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

                EventAggregator.Instance.Publish<LogFileAppEvent>(new LogFileAppEvent(Log, FileChangingType.LoadComplete));
            }
        }
    }
}
