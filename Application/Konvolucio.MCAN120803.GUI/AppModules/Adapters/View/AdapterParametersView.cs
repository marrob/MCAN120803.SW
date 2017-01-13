
namespace Konvolucio.MCAN120803.GUI.AppModules.Adapters.View
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    using Commands;

    public partial class AdapterParametersView : ToolStrip
    {
        public AdapterParametersView()
        {
            InitializeComponent();
  
            this.Items.AddRange(
                                new ToolStripItem[]
                                {
                                    new AdapterComboBox() {},
                                    new BaudrateComboBox(){},
                                    new LoopbackCommand(),
                                    new TerminationCommand(),
                                    new ListenOnlyCommand(),
                                    new NonAutoReTxCommand(),
                                });
        }
    }
}
