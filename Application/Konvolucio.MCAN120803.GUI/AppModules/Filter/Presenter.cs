// -----------------------------------------------------------------------
// <copyright file="Presenter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Filter
{
    using System.Windows.Forms;
    using Commands;
    using Common;
    using DataStorage;
    using Model;
    using Services;
    using View;
    using WinForms.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Presenter
    {
        public Presenter( IFiltersGridView gridView, MessageFilterCollection collection, Storage storage, ProjectParameters parameters)
        {
            gridView.Menu.Items.AddRange(
                new ToolStripItem[]
                {
                    new NewCommand(gridView.DataGridViewBase,collection),    
                    new CopyRowsCommand(gridView.DataGridViewBase, collection), 
                    new CutRowsCommand(gridView.DataGridViewBase, collection), 
                    new PasteRowsCommand(gridView.DataGridViewBase,collection), 
                    new DeleteRowsCommand(gridView.DataGridViewBase, collection),
                    new DefaultCommand(collection), 
                    new EnabledCommand(parameters),
                    new ExportCommand(storage, gridView),
                });

            gridView.Source = collection;

            EventAggregator.Instance.Subscribe<StorageAppEvent>(e1 =>
            {
                switch (e1.ChangingType)
                {
                    case FileChangingType.Loading:
                        {
                            gridView.DefaultLayout();
                            break;
                        }

                    case FileChangingType.LoadComplete:
                        {

                            gridView.GridLayout = e1.Storage.FilterGridLayout;

                            if (e1.Storage.Parameters.FiltersEnabled)
                            {
                                gridView.BackgroundText = CultureService.Instance.GetString(CultureText.text_FILTERS);
                                gridView.BackgroundText += " ";
                                gridView.BackgroundText += CultureService.Instance.GetString(CultureText.text_ENABLED);
                            }
                            else
                            {
                                gridView.BackgroundText = CultureService.Instance.GetString(CultureText.text_FILTERS);
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
                            gridView.GridLayout.CopyTo(e1.Storage.FilterGridLayout);
                            break;
                        }

                    case FileChangingType.ContentChanged:
                        {
                            /*Célzott frssítések*/
                            if (e1.Details.DataObjects == DataObjects.ParameterProperty)
                            {

                                if (e1.Details.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(() => e1.Storage.Parameters.ArbitrationIdFormat))
                                {
                                    /*Converterek miatt újra kell rajzolni a Grid-et*/
                                    /*Ez kritikus! ha egy sor törlésekor jön egy esemény ami frssíti a DGV-az DGV hibhoz vezet!*/
                                    gridView.Refresh();
                                }

                                if (e1.Details.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(() => e1.Storage.Parameters.FiltersEnabled))
                                {
                                    if (e1.Storage.Parameters.FiltersEnabled)
                                    {
                                        gridView.BackgroundText = CultureService.Instance.GetString(CultureText.text_FILTERS);
                                        gridView.BackgroundText += " ";
                                        gridView.BackgroundText += CultureService.Instance.GetString(CultureText.text_ENABLED);
                                    }
                                    else
                                    {
                                        gridView.BackgroundText = CultureService.Instance.GetString(CultureText.text_FILTERS);
                                        gridView.BackgroundText += " ";
                                        gridView.BackgroundText += CultureService.Instance.GetString(CultureText.text_DISABLED);
                                    }
                                }
                            }
                            break;
                        }
                }
            });
        }
    }
}
