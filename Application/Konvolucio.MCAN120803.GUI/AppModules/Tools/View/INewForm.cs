// -----------------------------------------------------------------------
// <copyright file="Class1.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Tools.View
{
    using System.Windows.Forms;
    using Common;
    using Tools.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface INewForm
    {
        string NewName { get; set; }
        ToolTypes SelectedType { get; set; }
        string[] Types { get; set; }
        DialogResult ShowDialog();
    }
}
