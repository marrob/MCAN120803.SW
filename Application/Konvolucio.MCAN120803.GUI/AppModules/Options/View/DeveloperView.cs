
namespace Konvolucio.MCAN120803.GUI.AppModules.Options.View
{
    using System;
    using System.Windows.Forms;

    using Properties; /*Sttings*/

    public partial class DeveloperView : UserControl, IOptionPage
    {

        /// <summary>
        /// 
        /// </summary>
        public bool RequiedRestart
        {
            get { return _requiedRestart; }
        }
        bool _requiedRestart = false;

        public bool RequiedDefault
        {
            get { return _requiedDefault; }
        }
        bool _requiedDefault;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public DeveloperView()
        {
            InitializeComponent();
         
            comboBoxAdapterThreadPriority.DataSource = Enum.GetValues(typeof(System.Threading.ThreadPriority));
            comboBoxSenderThreadPriority.DataSource = Enum.GetValues(typeof(System.Threading.ThreadPriority));
        }

        /// <summary>
        /// Alaphelyzet
        /// </summary>
        public void UpdateValues()
        {
            comboBoxSenderThreadPriority.SelectedItem = Settings.Default.SenderThreadPriority;
            comboBoxAdapterThreadPriority.SelectedItem = Settings.Default.AdapterThreadPriority;

            textBoxAppSettingsSaveCounter.Text = Settings.Default.ApplictionSettingsSaveCounter.ToString();
            textBoxAppSettingsUpgradeCounter.Text = Settings.Default.ApplictionSettingsUpgradeCounter.ToString();
            numericUpDownGuiRefreshRateMs.Value = Settings.Default.GuiRefreshRateMs;
            numericUpDownTraceDataGridRefresRateMs.Value = Settings.Default.dataGridViewTraceRefreshRateMs;
            numericUpDownSenderThreadSleepMs.Value = Settings.Default.SenderThreadSleepMs;
            numericUpDownStatisticsDataGridRefresRateMs.Value = Settings.Default.dataGridViewStatisticsRefreshRateMs;
        }

        /// <summary>
        /// Clear
        /// </summary>
        private void buttonReset_Click(object sender, EventArgs e)
        {
            Settings.Default.Reset();
            Settings.Default.Save();
            _requiedRestart = true;
            _requiedDefault = true;
        }

       /// <summary>
       /// 
       /// </summary>
        public void Save()
        {
            Settings.Default.GuiRefreshRateMs = (int)numericUpDownGuiRefreshRateMs.Value;
            Settings.Default.dataGridViewTraceRefreshRateMs = (int) numericUpDownTraceDataGridRefresRateMs.Value;
            Settings.Default.SenderThreadPriority = (System.Threading.ThreadPriority)comboBoxSenderThreadPriority.SelectedItem;
            Settings.Default.AdapterThreadPriority = (System.Threading.ThreadPriority) comboBoxAdapterThreadPriority.SelectedItem;
            Settings.Default.SenderThreadSleepMs = (int) numericUpDownSenderThreadSleepMs.Value;
            Settings.Default.dataGridViewStatisticsRefreshRateMs = (int) numericUpDownStatisticsDataGridRefresRateMs.Value;
        }

        /// <summary>
        /// Default
        /// </summary>
        public void Defualt()
        {

        }
    }
}
