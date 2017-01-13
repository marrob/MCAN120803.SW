using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvolucio.MCAN120803.GUI
{
    public class AppDiagnosticsEventArgs : EventArgs
    {
        public string Content { get; private set; }

        public AppDiagnosticsEventArgs(string content)
        {
            Content = content;
        }
    }
}
