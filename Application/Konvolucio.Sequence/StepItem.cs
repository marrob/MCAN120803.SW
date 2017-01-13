using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Konvolucio.Sequence
{
    public class StepItem
    {
        #region Id
        [Category("Step")]
        [Description("Step-hez tartozó egyedi azonosító.")]
        [XmlElement("Id")]
        public string StepId { get; set; }
        #endregion 
        #region Index
        [Category("Step")]
        [Description("Step aktuális poziciója a listában. O-ás indexeelésű")]
        [XmlElement("Index")]
        public int Index { get; set; }
        #endregion 
        #region Name
        [Category("Step")]
        [Description("A Step neve, a User által szabadon változtatható.")]
        [XmlElement("Name")]
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    //OnPropertyChanged("Name");
                }
            }
        }
        private string _Name;
        #endregion 
        #region Action
        [XmlElement("Action")]
        public IAction Action
        {
            get { return _Action; }
            set
            {
                if (_Action != value)
                {
                    _Action = value;
                }
            }
        }
        IAction _Action;
        #endregion 
    }
}
