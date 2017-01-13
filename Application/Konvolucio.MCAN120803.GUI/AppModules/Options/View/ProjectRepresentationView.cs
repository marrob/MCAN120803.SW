
namespace Konvolucio.MCAN120803.GUI.AppModules.Options.View
{
    using System;
    using System.Windows.Forms;

    using System.Globalization; /*InvariantCulture*/
    using Converters;           /*ArbitrationIdConverter*/
    using DataStorage;

    public partial class ProjectRepresentationView : UserControl, IOptionPage
    {
        public bool RequiedDefault
        {
            get { return false; }
        }

        private Storage  _storage;


        /// <summary>
        /// Konstructor
        /// </summary>
        public ProjectRepresentationView()
        {
            InitializeComponent();

            comboBoxTimestampFormat.Items.AddRange(AppConstants.TemplateTimestampFormats);
            comboBoxDataFormat.Items.AddRange(AppConstants.TemplateDataFormats);
            comboBoxArbitrationIdFormat.Items.AddRange(ArbitrationIdConverter.TemplateFormats);
           

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
            comboBoxTimestampFormat.SelectedItem = _storage.Parameters.TimestampFormat;
            comboBoxDataFormat.SelectedItem = _storage.Parameters.DataFormat;
            comboBoxArbitrationIdFormat.SelectedItem = _storage.Parameters.ArbitrationIdFormat;

            UpdateSampleTimestamp(_storage.Parameters.TimestampFormat);
            UpdateSampleDataFormat(_storage.Parameters.DataFormat);
            UpdateSampleArbitrationIdFormat(_storage.Parameters.ArbitrationIdFormat);
        }

        public void Save()
        {
            _storage.Parameters.TimestampFormat = comboBoxTimestampFormat.SelectedItem.ToString();
            _storage.Parameters.DataFormat = comboBoxDataFormat.SelectedItem.ToString();
            _storage.Parameters.ArbitrationIdFormat = comboBoxArbitrationIdFormat.SelectedItem.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        private void comboBoxTimestampFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSampleTimestamp(comboBoxTimestampFormat.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        private void comboBoxDataFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSampleDataFormat(comboBoxDataFormat.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        private void comboBoxArbitrationIdFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSampleArbitrationIdFormat(comboBoxArbitrationIdFormat.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        void UpdateSampleTimestamp(string fomrat)
        {
            DateTime stamp = DateTime.Now;
            try
            {
                textBoxSampleTimestamp.Text = DateTime.Now.ToString(fomrat, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        void UpdateSampleDataFormat(string format)
        {
            textBoxSampleDataFormat.Text = Converters.CustomDataConversion.ByteArrayToString(new byte[] { 0xA0, 0xB1, 0xC2, 0xD3 }, format);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        void UpdateSampleArbitrationIdFormat(string format)
        {
           textBoxSampleArbitrationIdFormat.Text = Converters.CustomDataConversion.UInt32ToString(0xA0B1C2D3, format);
        }


        public void Defualt()
        {

        }
    }
}
