

namespace Konvolucio.MCAN120803.GUI.AppModules.Adapters.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms; 

    using API;       
    using Baudrate;
    using Services;        
    using Common;

    internal sealed class BaudrateComboBox : ToolStripComboBox
    {
        ProjectParameters _parmeters = null;
        string _comboItemCustomBaudRate_Text = "Custom Baud Rate Editor";
        string _lastSelectedBaud = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public BaudrateComboBox()
        {

            DropDownStyle = ComboBoxStyle.DropDownList;
            AutoToolTip = true;
            ToolTipText = CultureService.Instance.GetString(CultureText.menuItem_Baudrate_ToolTip);
            Size = new System.Drawing.Size(150, 25);

            List<string> bauds = CanBaudRateCollection.GetBaudRates().Select(n => n.Name).ToList<string>();
            var baudWithCustom = new string[bauds.Count + 1];
            bauds.Add(_comboItemCustomBaudRate_Text);
            bauds.CopyTo(baudWithCustom, 0);
            Items.AddRange(baudWithCustom);
           

            EventAggregator.Instance.Subscribe<StorageAppEvent>(e =>
            {
                if (e.ChangingType == FileChangingType.LoadComplete)
                {
                    _parmeters = e.Storage.Parameters;

                    /*Ha olyan BaudRate-et töltött be, ami egyedi, azt először hozzá kell adni a ComboBox listájához, mert különben nem jelenik meg...*/
                    if (!_parmeters.Baudrate.Contains("kBaud") && !_parmeters.Baudrate.Contains("MBaud"))
                    {
                        bauds = CanBaudRateCollection.GetBaudRates().Select(n => n.Name).ToList<string>();
                        bauds.Add(_parmeters.Baudrate);
                        string[] temp = new string[bauds.Count];
                        bauds.CopyTo(temp, 0);
                        Items.Clear();
                        Items.AddRange(temp);
                    }

                    SelectedItem = _lastSelectedBaud = _parmeters.Baudrate;
                }
            });
            EventAggregator.Instance.Subscribe<StopAppEvent>(e => Enabled = true);
            EventAggregator.Instance.Subscribe<PlayAppEvent>(e => Enabled = false);
        }

        /// <summary>
        /// Válaszott érték mentése ami az előzmény is.
        /// </summary>
        protected override void OnSelectedIndexChanged(System.EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            _parmeters.Baudrate = SelectedItem as string;
            _lastSelectedBaud = SelectedItem as string;
        }

        /// <summary>
        /// Egy elemre klikkelt a legördülő listában, amihez lehet meg kell jeleníteni a BaudRate szerkesztőt.
        /// </summary>
        protected override void OnSelectionChangeCommitted(System.EventArgs e)
        {

           if (!Text.Contains("kBaud") && !Text.Contains("MBaud"))
           {/*Ez egy új vagy a meglévő érték létrehozás/szerkesztése */

               IBaurateEditorForm cbf = new BaurateEditorForm();

               if (Text[0] == 'B')
               { /*meglévő érték szereksztése*/
                   cbf.CustomBaudRateValue = Text.Remove(Text.IndexOf("Custom", 0)).Trim();
               }
               else
               {
                   /*először adott meg egyedi értéket, alaphelyzetet lát.*/
                   cbf.Default();
               }

               if (cbf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
               {
                   /*Egyedi értéket választott a ComoboLista elemeiben a Custom szót az új értkere kell módosítani.*/
                   string customValue = cbf.CustomBaudRateValue + " Custom Baud";
                   List<string> bauds = CanBaudRateCollection.GetBaudRates().Select(n => n.Name).ToList<string>();
                   bauds.Add(customValue);
                   /*tömbnek új referencia kell, csak akkor frissül a box...*/
                   string[] temp = new string[bauds.Count];
                   bauds.CopyTo(temp, 0);
                   Items.Clear();
                   Items.AddRange(temp);
                   /*A kijelölt elem az új egyedi érték*/
                   SelectedItem = customValue;
               }
               else
               {
                   /*Mégsem nyutgázta az új értéket */
                   /*Vissza kell tenni az előző kijelőlést*/
                   SelectedItem = _lastSelectedBaud;
               }
           }
        }
    }
}
