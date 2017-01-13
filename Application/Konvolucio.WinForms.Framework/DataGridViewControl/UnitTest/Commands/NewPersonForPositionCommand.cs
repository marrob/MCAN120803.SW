// -----------------------------------------------------------------------
// <copyright file="AddMessageFilter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework.DataGridViewControl.UnitTest.Commands
{
    using System;
    using System.ComponentModel;
    using System.Runtime.Remoting;
    using System.Windows.Forms;
    using Framework;
    using Models;
    using Properties;
    using View;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class NewPersonForPositionCommand : KnvDataGridViewNewRowBaseCommand
    {

        private readonly IBindingList _collection;
        private readonly DataGridView _dataGridView;
        private readonly INewPersonForm _personForm;
        public NewPersonForPositionCommand(DataGridView dataGridView, IBindingList collection):base(dataGridView, collection)
        {
            Text = @"New Person for position";

            _collection = collection;
            _dataGridView = dataGridView;
            _personForm = new NewPersonForm();
        }

        protected override object MakeNewItem(Type type)
        {
            PersonItem newItem = null;

            if (_personForm.ShowDialog() == DialogResult.OK)
            {
                newItem = new PersonItem
                {
                    FirstName = _personForm.FirstName,
                    LastName = _personForm.LastName,
                    Age = _personForm.Age
                };

                var selectedCell = _dataGridView.SelectedCells;
                if (selectedCell.Count > 0)
                    _collection.Insert(selectedCell[0].RowIndex + 1, newItem);
                else
                    _collection.Add(newItem);
            }

            return newItem;
        }
    }
}
