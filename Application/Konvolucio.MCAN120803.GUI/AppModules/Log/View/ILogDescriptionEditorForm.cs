// -----------------------------------------------------------------------
// <copyright file="ILogDescriptionView.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Log.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ILogDescriptionEditorForm
    {
        string Content { get; set; }
        DialogResult ShowDialog();
    }
}
