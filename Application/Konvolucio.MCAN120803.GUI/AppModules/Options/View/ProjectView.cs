

namespace Konvolucio.MCAN120803.GUI.AppModules.Options.View
{

    using System.Windows.Forms;
    using DataStorage;

    public partial class ProjectView : UserControl, IOptionPage
    {
        public bool RequiedDefault
        {
            get { return false; }
        }

        public bool RequiedRestart
        {
            get { return false; }
        }
     
        private Storage _storage;
        
        /// <summary>
        /// Konstructor
        /// </summary>
        public ProjectView()
        {
            InitializeComponent();

            /*Referencia átvétel, akkor mÜkökd, ha van betöltve project, de látszik rajta...*/
            /*Akkor is kell frissülni ha másho változtak a paraméterek, Content changed nem használhatod, mert akkor saját változtatásokra is frissül*/
            EventAggregator.Instance.Subscribe<StorageAppEvent>(e1 =>
            {
                if (e1.ChangingType == FileChangingType.LoadComplete)
                {
                    _storage = e1.Storage;
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateValues()
        {
    
            textBoxProductName.Text = _storage.Parameters.ProductName;
            textBoxProductVersion.Text = _storage.Parameters.ProductVersion;
            textBoxProductCode.Text = _storage.Parameters.ProductCode;
            textBoxCustomerName.Text = _storage.Parameters.CustomerName;
            textBoxCustomerCode.Text = _storage.Parameters.CustomerCode;
            textBoxComment.Text = _storage.Parameters.Comment;
        }

        public void Save()
        {
            _storage.Parameters.ProductName = textBoxProductName.Text;
            _storage.Parameters.ProductVersion =textBoxProductVersion.Text;
            _storage.Parameters.ProductCode =textBoxProductCode.Text;
            _storage.Parameters.CustomerName =textBoxCustomerName.Text;
            _storage.Parameters.CustomerCode = textBoxCustomerCode.Text;
            _storage.Parameters.Comment =textBoxComment.Text;

        }

        public void Defualt()
        {

        }
    }
}
