
namespace Konvolucio.MCAN120803.GuiSettings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;
    using Konvolucio.MCAN120803.GUI;
    using WinForms.Framework;

    public interface IAllowDefault
    {
        void SetDefault();
    }

    public partial class AppGuiSettings 
    {
        public class LogGuiSettings : IAllowDefault
        {
            [DefaultValue(0.23)]
            public double SplitContainerLog_SplitterDistance { get; set; }
            
            public ColumnLayoutCollection DataGridViewLog_ColumnLayout { get; set; }
            
            [DefaultValue(true)]
            public bool ColumnAutoSize { get; set; }



            public void SetDefault()
            {
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(this))
                {
                    DefaultValueAttribute myAttribute = (DefaultValueAttribute)property.Attributes[typeof(DefaultValueAttribute)];

                    if (myAttribute != null)
                    {
                        property.SetValue(this, myAttribute.Value);
                    }
                }
            }
        }


    }
}
