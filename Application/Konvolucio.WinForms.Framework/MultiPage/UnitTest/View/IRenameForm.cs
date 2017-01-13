// -----------------------------------------------------------------------
// <copyright file="IRenameForm.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework.MultiPage.UnitTest.View
{
    using System.Windows.Forms;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IRenameForm
    {
        string NewName { get; set; }
        DialogResult ShowDialog();
    }
}
