
namespace Konvolucio.MCAN120803.GUI.AppModules.Filter.Commands
{
    using System;
    using System.Windows.Forms;
    using Model;
    using Properties;
    using Services;

    internal sealed class DefaultCommand : ToolStripMenuItem
    {
        private readonly MessageFilterCollection _collection;
        private readonly object _lockObj;

        public DefaultCommand(MessageFilterCollection collection)
        {
            Image = Resources.Synchronize_16x16;
            Text = CultureService.Instance.GetString(CultureText.menuItem_Default_Text);
            //ShortcutKeys = Keys.Alt | Keys.D;
            _collection = collection;
            _lockObj = new object();

        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (Enabled)
            {
                lock (_lockObj)
                {
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        _collection.Default(); 
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
        }
    }
}
