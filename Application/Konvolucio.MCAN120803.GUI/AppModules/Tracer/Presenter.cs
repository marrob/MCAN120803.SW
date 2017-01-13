// -----------------------------------------------------------------------
// <copyright file="Presenter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Tracer
{
    using System.Windows.Forms;
    using Common;
    using DataStorage;
    using Properties;
 
    using Services;
    using WinForms.Framework;
    using View;
    using Model;
 
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Presenter
    {
        public Presenter(ITraceGridView gridView, MessageTraceCollection collection, ProjectParameters parameters)
        {

            gridView.Source = collection;

            gridView.ContextMenuStrip.Items.AddRange(
                new ToolStripItem[]
                {
                    new Commands.ClearTraceCommand(collection),
                    new Commands.AutoSizeAllCommand(gridView),
                    new Commands.AutoScrolllCommand(gridView), 
                    new Commands.EnabledCommand(parameters),
                    new Commands.FullscreenCommand(gridView),
                });


            EventAggregator.Instance.Subscribe<StorageAppEvent>(e1 =>
            {
                switch (e1.ChangingType)
                {
                    case FileChangingType.Loading:
                    {
                        gridView.DefaultLayout();
                        gridView.Source.Clear();
                        break;
                    }

                    case FileChangingType.LoadComplete:
                    {
                        gridView.CustomArbIdColumns = e1.Storage.CustomArbIdColumns;
                        gridView.GridLayout = e1.Storage.TraceGridLayout;

                        gridView.TimestampFormat = e1.Storage.Parameters.TimestampFormat;

                        if (e1.Storage.Parameters.TraceEnabled)
                        {
                            gridView.BackgroundText = CultureService.Instance.GetString(CultureText.text_TRACE);
                        }
                        else
                        {
                            gridView.BackgroundText = CultureService.Instance.GetString(CultureText.text_TRACE);
                            gridView.BackgroundText += " ";
                            gridView.BackgroundText += CultureService.Instance.GetString(CultureText.text_DISABLED);
                        }

                        break;
                    }
                    case FileChangingType.Saving:
                    {
                        /*GridLayout hozza létre a default értéket és nem a project File.. ezért
                         mentés előtt átt kell adni a projectnek a layout listákat.*/
                        gridView.GridLayout.CopyTo(e1.Storage.TraceGridLayout);
                        break;
                    }

                    case FileChangingType.ContentChanged:
                    {
                        /*Célzott frssítések*/
                        if (e1.Details.DataObjects == DataObjects.ParameterProperty)
                        {
                            if (e1.Details.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(() => e1.Storage.Parameters.TimestampFormat))
                            {
                                gridView.TimestampFormat = e1.Storage.Parameters.TimestampFormat;
                            }

                            else if (e1.Details.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(() => e1.Storage.Parameters.TraceEnabled))
                            {
                                if (e1.Storage.Parameters.TraceEnabled)
                                {
                                    gridView.BackgroundText = CultureService.Instance.GetString(CultureText.text_TRACE);
                                }
                                else
                                {
                                    gridView.BackgroundText = CultureService.Instance.GetString(CultureText.text_TRACE);
                                    gridView.BackgroundText += " ";
                                    gridView.BackgroundText += CultureService.Instance.GetString(CultureText.text_DISABLED);
                                }
                            }
                        }
                        break;
                    }
                }
            });

            EventAggregator.Instance.Subscribe<PlayAppEvent>(e =>
            {
                /*Periodikus frsstés itt indul.*/
                gridView.Start();
                gridView.RefreshRate = Settings.Default.dataGridViewTraceRefreshRateMs;

            });

            EventAggregator.Instance.Subscribe<StopAppEvent>(e =>
            {
                /*Periodikus frsstés itt leáll.*/
                gridView.Stop();
            });
        }

    }
}

