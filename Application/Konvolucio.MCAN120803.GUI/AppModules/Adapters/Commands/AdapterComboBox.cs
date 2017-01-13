
namespace Konvolucio.MCAN120803.GUI.AppModules.Adapters.Commands
{
    using System;
    using System.Windows.Forms; 

    using WinForms.Framework;
    using Common;
    using DataStorage;

    internal sealed class AdapterComboBox : ToolStripComboBox
    {
        /// <summary>
        /// 
        /// </summary>
        ProjectParameters _parmeters;
        
        /// <summary>
        /// 
        /// </summary>
        public AdapterComboBox()
        {
            DropDownStyle = ComboBoxStyle.DropDownList;
            AutoToolTip = true;
            Items.AddRange(AdapterService.GetAdapters());
            Size = new System.Drawing.Size(200, 25);

            EventAggregator.Instance.Subscribe<StorageAppEvent>(e =>
            {
                if (e.Details.DataObjects == DataObjects.ParameterProperty &&
                    e.Details.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(() => e.Storage.Parameters.DeviceName))
                {
                    _parmeters = e.Storage.Parameters;
                    SelectedItem = e.Storage.Parameters.DeviceName;
                }
            });

            EventAggregator.Instance.Subscribe<StopAppEvent>(e => Enabled = true);
            EventAggregator.Instance.Subscribe<PlayAppEvent>(e => Enabled = false);
        }
       
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);

            if (!string.IsNullOrEmpty(SelectedItem as string))
                _parmeters.DeviceName = SelectedItem as string;
        }

        protected override void OnDropDown(EventArgs e)
        {
            base.OnDropDown(e);
            
            Items.Clear();
            Items.AddRange(AdapterService.GetAdapters());
        }
    }
}
