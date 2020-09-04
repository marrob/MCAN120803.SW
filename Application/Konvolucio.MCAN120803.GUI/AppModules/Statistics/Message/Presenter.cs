// -----------------------------------------------------------------------
// <copyright file="Presenter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Message
{
    using System.Windows.Forms;
    using Commands;
    using DataStorage;
    using Model;
    using Properties;
    using Services;
    using View;
    using WinForms.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Presenter
    {

        public Presenter(
                            IStatisticsGridView gridView,
                            MessageStatistics statistics,
                            Storage project)
        {


            gridView.Source = statistics.Messages;

            gridView.Menu.Items.AddRange(
                new ToolStripItem[]
                {

                   new ClearCommand(statistics), 
                   new DefaultCommand(statistics, gridView), 
                   new EnabledCommand(project), 
                   new ExportCommand(project, gridView),


                });
           
            #region Project események
            EventAggregator.Instance.Subscribe<StorageAppEvent>(e1 =>
            {
                switch (e1.ChangingType)
                {
                    case FileChangingType.Loading:
                        {
   
                            break;
                        }

                    case FileChangingType.LoadComplete:
                        {
                            project = e1.Storage;

                            gridView.AllowClick = true;

                            gridView.GridLayout = e1.Storage.StatisticsGridLayout;

                            gridView.TimestampFormat = e1.Storage.Parameters.TimestampFormat;

                            if (e1.Storage.Parameters.MessageStatisticsEnabled)
                            {
                                gridView.BackgroundText = CultureService.Instance.GetString(CultureText.text_STATISTICS);
                            }
                            else
                            {
                                gridView.BackgroundText = CultureService.Instance.GetString(CultureText.text_STATISTICS);
                                gridView.BackgroundText += " ";
                                gridView.BackgroundText += CultureService.Instance.GetString(CultureText.text_DISABLED);
                            }

                            gridView.Refresh();

                            break;
                        }
                    case FileChangingType.Saving:
                        {
                            /*GridLayout hozza létre a default értéket és nem a project File.. ezért
                             mentés előtt átt kell adni a projectnek a layout listákat.*/
                            gridView.GridLayout.CopyTo(e1.Storage.StatisticsGridLayout);
                            break;
                        }

                    case FileChangingType.ContentChanged:
                        {
                            /*Célzott frssítések*/
                            if (e1.Details.DataObjects == DataObjects.ParameterProperty)
                            {

                                if (e1.Details.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(() => e1.Storage.Parameters.ArbitrationIdFormat) ||
                                    e1.Details.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(() => e1.Storage.Parameters.DataFormat))
                                {
                                    /*Converterek miatt újra kell rajzolni a Grid-et*/
                                    /*Ez kritikus! ha egy sor törlésekor jön egy esemény ami frssíti a DGV-az DGV hibhoz vezet!*/
                                    gridView.Refresh();
                                }

                                else if (e1.Details.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(() => e1.Storage.Parameters.TimestampFormat))
                                {
                                    gridView.TimestampFormat = e1.Storage.Parameters.TimestampFormat;
                                }

                                else if (e1.Details.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(() => e1.Storage.Parameters.MessageStatisticsEnabled))
                                {
                                    if (e1.Storage.Parameters.MessageStatisticsEnabled)
                                    {
                                        gridView.BackgroundText = CultureService.Instance.GetString(CultureText.text_STATISTICS);
                                    }
                                    else
                                    {
                                        gridView.BackgroundText = CultureService.Instance.GetString(CultureText.text_STATISTICS);
                                        gridView.BackgroundText += " ";
                                        gridView.BackgroundText += CultureService.Instance.GetString(CultureText.text_DISABLED);
                                    }
                                }
                            }
                            break;
                        }
                }
            });
            #endregion 

            #region Adapter események

            EventAggregator.Instance.Subscribe<PlayAppEvent>(e =>
            {
  

                /*Periodikus frsstés itt indul.*/
                gridView.Start();
                gridView.RefreshRate = Settings.Default.dataGridViewStatisticsRefreshRateMs;

            });

            EventAggregator.Instance.Subscribe<StopAppEvent>(e =>
            {
                /*Periodikus frsstés itt leáll.*/
                gridView.Stop();
            });

            #endregion
        }
    }
}
