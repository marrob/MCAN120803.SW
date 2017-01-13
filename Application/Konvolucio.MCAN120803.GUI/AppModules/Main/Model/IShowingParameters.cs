// -----------------------------------------------------------------------
// <copyright file="Interface1.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Main.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IShowingParameters
    {
        string Path { get; }
        string ProudctName { get; set; }
        string ProductVersion { get; set; }
        string ProcutCode { get; set; }
        string CustomerName { get; set; }
        string CustomerCode { get; set; }
        bool Show();
    }
}
