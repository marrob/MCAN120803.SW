using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvolucio.Sequence
{
    public interface IAction
    {
        bool IsSetUpDone { get; set; }
        bool SettingsVisible { get; set; }
        bool HaveOpi { get; set; }
        bool ShowModuleTime { get; set; }
        StepCollection Sequence { get; set; }
        string SettingsDescription();
        void SetUpMethod();
        void MainMethod();
        void CleanUpMethod();
        void AbortMethod();
    }
}
