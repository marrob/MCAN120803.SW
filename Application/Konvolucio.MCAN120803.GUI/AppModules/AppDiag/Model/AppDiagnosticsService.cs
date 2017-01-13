// -----------------------------------------------------------------------
// <copyright file="AppDiagnostics.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class AppDiagService
    {
        public static event EventHandler<AppDiagnosticsEventArgs> ContentAdded;

        public static void WriteLine(string content)
        {
            Console.WriteLine(content);
            if (ContentAdded != null)
                ContentAdded(null, new AppDiagnosticsEventArgs(content));
        }
    }
}
