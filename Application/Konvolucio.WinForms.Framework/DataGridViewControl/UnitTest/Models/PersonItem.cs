// -----------------------------------------------------------------------
// <copyright file="PersonItem.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework.DataGridViewControl.UnitTest.Models
{
    using System;
    using System.ComponentModel;

    [Serializable]
    public class PersonItem : ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int Index { get; private set; }

        public string FirstName
        {
            get { return _firstNam; }
            set
            {
                if (_firstNam != value)
                {
                    _firstNam = value;
                    OnPorpretyChanged(PropertyPlus.GetPropertyName(()=>FirstName));
                }
            }
        }
        private string _firstNam;

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPorpretyChanged(PropertyPlus.GetPropertyName(()=>LastName));
                }
            }
        }
        private string _lastName;

        public int Age
        {
            get { return _age; }
            set
            {
                if (_age != value)
                {
                    _age = value;
                    OnPorpretyChanged(PropertyPlus.GetPropertyName(()=>Age));
                }
            }
        }

        private int _age;
        
        public PersonItem()
        {
            FirstName = "Please write here youre name";
            LastName = "Plesase write here youre name";
            Age = 0;
        }

        public PersonItem(string first, string last, int age)
        {
            _firstNam = first;
            _lastName = last;
            _age = age;
        }
        protected void OnPorpretyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public override string ToString()
        {
            return FirstName + " " + LastName;
        }

        public object Clone()
        {
           return new PersonItem(FirstName, LastName, Age);
        }
    }
}
