// -----------------------------------------------------------------------
// <copyright file="MultiPageBaseItem.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Annotations;

    /// <summary>
    /// Egy MultiPage lapot ez osztály ír le.
    /// 
    /// Egy Page-hez trtozik:
    /// 
    /// - UserControl : Szabadon módosítható, ez maga egy lap
    /// - PageButton: lap-hoz tartozó nyomógomb.
    /// - TreeNode: 
    /// - Tag: Szabadon felhasználható
    /// </summary>
    public sealed class MultiPageItem : INotifyPropertyChanged, IDisposable
    {

        public event EventHandler Disposing;
        public event PropertyChangedEventHandler PropertyChanged;


        public MultiPageItem(string name, UserControl userControl, string imageKey)
        {
            _name = name;
            PageControl = userControl;
            
            if(PageControl != null)
                PageControl.Dock = DockStyle.Fill;

            PageButton = new PageButton() { Text = name, Tag = this };
            PageNode = new TreeNode {Text = name, ImageKey = imageKey, SelectedImageKey = imageKey, Tag = this};
            ImageKey = imageKey;
        }

        /// <summary>
        /// A hozzáadott elem neve.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;

                    PageNode.Text = Name;
                    PageButton.Text = Name;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => Name));
                }
            }
        }
        private string _name;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Szabadon felhasználható..
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Ez egy Page amit felhasználó lát.
        /// </summary>
        public UserControl PageControl { get; private set; }

        public PageButton PageButton { get; private set; }

        public TreeNode PageNode { get; private set; }

        public string ImageKey { get;  private set; }

        public override bool Equals(object obj)
        {
            var other = obj as MultiPageItem;
            if (other != null)
                if (_name == other.Name)
                    return true;
            return false;
        }

        public override int GetHashCode()
        {
            return (_name != null ? _name.GetHashCode() : 0);
        }

        public void Dispose()
        {
            OnDisposing();
            if(PageControl!=null)
               PageControl.Dispose();
        }

        public override string ToString()
        {
            return Name;
        }
        
        public static bool operator !=(MultiPageItem x, MultiPageItem y)
        {
            return !(x == y);
        }

        public static bool operator ==(MultiPageItem x, MultiPageItem y)
        {

            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            if (object.ReferenceEquals(null, y))
                return false;

            if(x.Equals(y))
                return true;
            
            return false;
        }

        private void OnDisposing()
        {
            var handler = Disposing;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
