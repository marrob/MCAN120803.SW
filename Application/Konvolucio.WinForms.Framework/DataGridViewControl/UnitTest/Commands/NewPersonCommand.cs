// -----------------------------------------------------------------------
// <copyright file="AddMessageFilter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework.DataGridViewControl.UnitTest.Commands
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Framework;
    using Models;
    using Properties;
    using View;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class NewPersonCommand : KnvDataGridViewNewRowBaseCommand
    {
        private readonly IBindingList _collection;
        private readonly INewPersonForm _personForm;
        public NewPersonCommand(DataGridView dataGridView, IBindingList collection): base(dataGridView, collection)
        {
            Text = @"New Person";
            _collection = collection;
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
                _collection.Add(newItem);
            }
            return newItem;
        }
    }
}
