// -----------------------------------------------------------------------
// <copyright file="Class1.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework.DataGridViewControl.UnitTest.View
{
    using System.Windows.Forms;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface INewPersonForm
    {
        string FirstName { get; }
        string LastName { get; }
        int Age { get; }

        DialogResult ShowDialog();
    }
}
