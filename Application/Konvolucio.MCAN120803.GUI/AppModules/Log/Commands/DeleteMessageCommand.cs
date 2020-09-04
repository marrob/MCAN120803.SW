// -----------------------------------------------------------------------
// <copyright file="DeleteLogMessageCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Log.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms; /*ToolStripMenuItem*/

    using Properties;
    using Services;             /*CultureService*/
    using WinForms.Framework;
    using Model;
    using View;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class DeleteMessageCommand : ToolStripMenuItem
    {
        readonly ILogView _view;

        public DeleteMessageCommand(ILogView view)
        {
            _view = view;
            Image = Resources.Email_Delete24;
            Text = CultureService.Instance.GetString(CultureText.menuItem_Delete_Text);
            EventAggregator.Instance.Subscribe<LogSelectionChangedAppEvent>(e =>
            {
                Enabled = e.SelectedItems.Count > 0;
            });

        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (Enabled)
            {
                try
                {
                    _view.LogGrid.AllowClick = false;
                    (_view.LogGrid.Source as ILogFileMessageCollection).RemoveSelectedMessage();
                }
                catch (Exception ex)
                {
#if TRACE
                    AppDiagService.WriteLine(this.GetType().Namespace + "." + this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()" + "=>" + "ERROR" + " " + ex.Message);
#endif

                    throw ex;
                }
                finally
                {
                    _view.LogGrid.AllowClick = true;                
                }
            }
        }
    }
}
