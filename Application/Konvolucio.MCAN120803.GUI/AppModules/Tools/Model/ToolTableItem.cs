// -----------------------------------------------------------------------
// <copyright file="ToolTableItem.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Tools.Model
{
    using System.ComponentModel;
    using Common;
    using WinForms.Framework;

    public class ToolTableItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event ListChangedEventHandler TableChanged;

        /// <summary>
        /// Egy tábla erős neve.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnProppertyChanged(PropertyPlus.GetPropertyName(() => Name));
                }
            }
        }
        private string _name; 

        /// <summary>
        /// Egy tábla típusa. 
        /// </summary>
        public ToolTypes ToolType { get; set; }

        /// <summary>
        /// Egy tábla referenciája
        /// </summary>
        public IToolItem TableObject
        {
            get { return _tableObject; }
            set
            {

                var bindingList = value as IBindingList;
                if(bindingList!= null)
                    bindingList.ListChanged += BindingList_ListChanged;
                  
                _tableObject = value;
            }
        }
        private IToolItem _tableObject;

        /// <summary>
        /// Tovább dobja TableObject változást
        /// </summary>
        private void BindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if(TableChanged != null)
                TableChanged(this, e);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ToolTableItem()
        {
            
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ToolTableItem(string name, ToolTypes toolType) : this()
        {
            _name = name;
            ToolType = toolType;
        }

        /// <summary>
        /// Tulajdonság változott
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnProppertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
