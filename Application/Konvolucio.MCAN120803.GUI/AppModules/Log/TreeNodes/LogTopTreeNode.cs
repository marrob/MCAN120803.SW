// -----------------------------------------------------------------------
// <copyright file="LogTopTreeView.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Log.TreeNodes
{
    using System.Linq;
 
    using System.ComponentModel;
    using WinForms.Framework;
    using System.Windows.Forms;
    using DataStorage;
    using Model;
    using View;
    using Services;        
    using DataStorage;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LogTopTreeNode : TreeNode
    {
        readonly ILogDescriptionView _description;
        public ILogFileCollection Logs { get; private set; }
      
        
        /// <summary>
        /// TreeView Top eleme
        /// </summary>
        /// <param name="logs"></param>
        /// <param name="description"></param>
        /// <param name="view"></param>
        public LogTopTreeNode(  IApp appx,
                                ILogFileCollection logs,
                                ILogDescriptionView description)
        {
            Text = CultureService.Instance.GetString(CultureText.node_Logs_Text);
            SelectedImageKey = ImageKey = "log16";
            _description = description;
            Logs = logs;
            Storage storage = null;


            EventAggregator.Instance.Subscribe<StorageAppEvent>(e1 => 
            {
                storage = e1.Storage;

                switch (e1.ChangingType)
                {

                    case FileChangingType.LoadComplete:
                    {
                        /*Itt kezdi listázni a log fájlokat project betötlését követően.*/
                        Logs.Load(storage.Loaction, storage.FileName);

                        if (e1.Storage.Parameters.LogEnabled)
                        {
                            Text = CultureService.Instance.GetString(CultureText.node_Logs_Text);
                        }
                        else
                        {
                            Text = CultureService.Instance.GetString(CultureText.node_Logs_Text);
                            Text += string.Format(" [{0}] ", CultureService.Instance.GetString(CultureText.text_DISABLED));
                        }
                        break;
                    }
                    case FileChangingType.ContentChanged:
                    {
                        if (e1.Details.DataObjects == DataObjects.ParameterProperty &&
                            e1.Details.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(() => e1.Storage.Parameters.LogEnabled))
                        {
                            if (e1.Storage.Parameters.LogEnabled)
                            {
                                Text = CultureService.Instance.GetString(CultureText.node_Logs_Text);
                            }
                            else
                            {
                                Text = CultureService.Instance.GetString(CultureText.node_Logs_Text);
                                Text += string.Format(" [{0}] ", CultureService.Instance.GetString(CultureText.text_DISABLED));
                            }
                        }
                        break;
                    };
                }
            });

            logs.ListChanged += new ListChangedEventHandler(LogFiles_ListChanged);
            logs.ListChanging += new ListChangingEventHandler<ILogFileItem>(LogFiles_ListChanging);
        }

        /// <summary>
        /// Mielött végrehajtásra keürlülne a váltzás a felület frissíti.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LogFiles_ListChanging(object sender, ListChangingEventArgs<ILogFileItem> e)
        {
            if (e.ListChangedType == ListChangingType.ItemRemoving)
            {
                var node = Nodes.Cast<LogFileNameTreeNode>().FirstOrDefault(n => n.Guid == e.Item.Guid);
                if(node != null)
                    Nodes.Remove(node);
            }

            if (e.ListChangedType == ListChangingType.Clearing)
            {
                /*Figyelem abban az esetben, ha TreeView-ban több Node-ot törölsz egymás után, akkor a Node Selection 
                 * fejebb lép egy ellemme (letílhatatlan funkció), így ha a Node-hoz tartozik valmilyen betöltés funkció, 
                 * akkor a Node-ok törlésekor is betöltődik… A megoldás az, hogy az szülő Node összes gyerekét kell törölni
                 * SzulőnNode.Nodes.Clear().*/
                Nodes.Clear();
                EventAggregator.Instance.Publish<LogFileAppEvent>(new LogFileAppEvent(null, FileChangingType.UnLoadComplete));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LogFiles_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                /*Egy Node beszurása a fájl listába*/
                var bindingList = sender as IBindingList;
                if (bindingList != null)
                {
                    var log = bindingList[e.NewIndex] as ILogFileItem;
                    Nodes.Add(new LogFileNameTreeNode(Logs, log, _description,
                        new TreeNode[]
                        {
                            new LogTransmittedTreeNode(log),
                            new LogReceivedTreeNode(log),
                            new LogMessagesTreeNode(log),
                        }));
                }
            }
        }
    }
}
