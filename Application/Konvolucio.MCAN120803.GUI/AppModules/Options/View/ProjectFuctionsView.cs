
namespace Konvolucio.MCAN120803.GUI.AppModules.Options.View
{

    using System.Windows.Forms;

    using DataStorage;
    
    public partial class ProjectFunctionsView : UserControl, IOptionPage
    {
        public bool RequiedDefault
        {
            get { return false; }
        }

        private Storage _storage;


        /// <summary>
        /// Konstructor
        /// </summary>
        public ProjectFunctionsView()
        {
            InitializeComponent();

            EventAggregator.Instance.Subscribe<StorageAppEvent>(e1 =>
            {
                if (e1.ChangingType == FileChangingType.LoadComplete)
                {
                    _storage = e1.Storage;
                }
            });
        }

        public bool RequiedRestart
        {
            get { return false; }
        }

        public void UpdateValues()
        {
            checkBoxLogEnable.Checked = _storage.Parameters.LogEnabled;
            checkBoxTraceEnabled.Checked = _storage.Parameters.TraceEnabled;
            checkBoxPlayHistoryClearEnabled.Checked = _storage.Parameters.PlayHistoryClearEnabled;
            checkBoxAdapterStatisticsEnabled.Checked = _storage.Parameters.AdapterStatisticsEnabled;
            checkBoxMessageStatisticsEnabled.Checked = _storage.Parameters.MessageStatisticsEnabled;
            checkBoxRxMsgResolvingBySenderEnabled.Checked = _storage.Parameters.RxMsgResolvingBySenderEnabled;
        }

        public void Save()
        {
            _storage.Parameters.LogEnabled = checkBoxLogEnable.Checked;
            _storage.Parameters.TraceEnabled = checkBoxTraceEnabled.Checked;
            _storage.Parameters.PlayHistoryClearEnabled = checkBoxPlayHistoryClearEnabled.Checked;
            _storage.Parameters.AdapterStatisticsEnabled = checkBoxAdapterStatisticsEnabled.Checked;
            _storage.Parameters.MessageStatisticsEnabled = checkBoxMessageStatisticsEnabled.Checked;
            _storage.Parameters.RxMsgResolvingBySenderEnabled = checkBoxRxMsgResolvingBySenderEnabled.Checked;
        }


        public void Defualt()
        {

        }



    }
}
