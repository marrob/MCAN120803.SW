// -----------------------------------------------------------------------
// <copyright file="ColumnLayout.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Annotations;

    /// <summary>
    /// Egy DataGird egy oszloponának Layout leírója.
    /// Ezzel egy oszlop megjelnése elmethető és vissza állítható.
    /// </summary>
    [Serializable]
    public class ColumnLayoutItem : INotifyPropertyChanged
    {
        private string _name;
        private int _displayIndex;
        private int _width;
        private bool _visible;

        /// <summary>
        /// Oszlop neve.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Ezzen az indexen kell megjelníteni az oszlopot.
        /// </summary>
        public int DisplayIndex
        {
            get { return _displayIndex; }
            set
            {
                if (value == _displayIndex) return;
                _displayIndex = value;
                OnPropertyChanged("DisplayIndex");
            }
        }

        /// <summary>
        /// Oszlop szélessége.
        /// </summary>
        public int Width
        {
            get { return _width; }
            set
            {
                if (value == _width) return;
                _width = value;
                OnPropertyChanged("Width");
            }
        }

        /// <summary>
        /// Látható éppen vagy sem.
        /// </summary>
        public bool Visible
        {
            get { return _visible; }
            set
            {
                if (value.Equals(_visible)) return;
                _visible = value;
                OnPropertyChanged("Visible");
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ColumnLayoutItem() { }


        public ColumnLayoutItem(string name, int displayIndex, int width, bool visible)
        {
            Name = name;
            DisplayIndex = displayIndex;
            Width = width;
            Visible = visible;
        }

        public override string ToString()
        {
            string str = string.Empty;
            str += ("\tName: " + Name + "; ").PadRight(25);
            str += ("\tDisplayIndex:" + DisplayIndex + "; ").PadRight(25);
            str += ("\tWidth:" + Width + "; ").PadRight(25);
            str += ("\tVisible:" + Visible + "; ").PadRight(25);
            return str;
        }

        public void CopyTo(ColumnLayoutItem target)
        {
            target.Name = Name;
            target.DisplayIndex = DisplayIndex;
            target.Width = Width;
            target.Visible = Visible;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
