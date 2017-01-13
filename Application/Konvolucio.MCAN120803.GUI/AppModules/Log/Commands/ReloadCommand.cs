
namespace Konvolucio.MCAN120803.GUI.AppModules.Log.Commands
{
    using System;
    using System.Windows.Forms; 

    using Properties;
    using Services;
    using Model;
    using TreeNodes;
    using DataStorage;


    internal sealed class ReloadCommand : ToolStripMenuItem
    {
        readonly ILogFileCollection _logs;
        readonly Storage _storage;

        public ReloadCommand(ILogFileCollection logs, Storage storage)
        {
            Text = CultureService.Instance.GetString(CultureText.menuItem_Reload_Text);
            Image = Resources.refresh16;
            _logs = logs;
            _storage = storage;
 
            EventAggregator.Instance.Subscribe<TreeViewSelectionChangedAppEvent>(e =>
            {
                if (e.SelectedNode is LogTopTreeNode)                        
                    Visible = true;
                else
                    Visible = false;
            });
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
#if TRACE
    AppDiagService.WriteLine(this.GetType().Namespace + "." + this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
#endif
            _logs.Load(_storage.Loaction, _storage.FileName);
        }
    }
}
