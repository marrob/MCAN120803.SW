// -----------------------------------------------------------------------
// <copyright file="MessageFilterItemTemplate.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Filter.Model
{
    using System;
    using System.ComponentModel;
    using Common;
    using Converters;
    using DataStorage;
    using WinForms.Framework;   /*PropertyPlus*/
    using Services;

/*IProjectService*/

    [Serializable]
    public class MessageFilterItem : ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler DefaultStateComplete;
            
        private string _name;
        private bool _enabled;
        private MaskOrArbId _maskOrArbId;
        private uint _maskOrArbIdValue;
        private ArbitrationIdType _type;
        private bool _remote;
        private MessageDirection _direction;
        private MessageFilterMode _mode;
        
        public string Guid { get; set; }

        /// <summary>
        /// Sorszáma 1.-es bázisú
        /// </summary>
        public int Index { get; internal set; }

        /// <summary>
        /// Szürő neve.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => Name));
                }
            }
        }

        /// <summary>
        /// Szürő engedélyezése vagy tiltása
        /// </summary>
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => Enabled));
                }
            }
        }

        /// <summary>
        /// Itt döntheted el, hogy a Value-ban meagodd érték ArbId vagy Mask
        /// </summary>
        public MaskOrArbId MaskOrArbId
        {
            get { return _maskOrArbId; }
            set
            {
                if (_maskOrArbId != value)
                {
                    _maskOrArbId = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => MaskOrArbId));
                }
            }
        }

        /// <summary>
        /// ArbId vagy Mask
        /// </summary>
        [TypeConverter(typeof(ArbitrationIdConverter))]
        public uint MaskOrArbIdValue 
        {
            get { return _maskOrArbIdValue; }
            set 
            {
                if (_maskOrArbIdValue != value)
                {
                    _maskOrArbIdValue = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => MaskOrArbIdValue));
                }
            }
        }

        /// <summary>
        /// Arbitácitós azonosító típsua.
        /// </summary>
        public ArbitrationIdType Type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => Type));
                }
            }
        }

        /// <summary>
        /// Üzenet távoli adatkérős vagy sem.
        /// </summary>
        public bool Remote
        {
            get { return _remote; }
            set
            {
                if (_remote != value)
                {
                    _remote = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => Remote));
                }
            }
        }

        /// <summary>
        /// Üzenet iránya.
        /// </summary>
        public MessageDirection Direction
        {
            get { return _direction; }
            set
            {
                if (_direction != value)
                {
                    _direction = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => Direction));
                }
            }
        }
        
        /// <summary>
        /// Filter módja.
        /// </summary>
        public MessageFilterMode Mode
        {
            get { return _mode; }
            set
            {
                if (_mode != value)
                {
                    _mode = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => Direction));
                }
            }
        }

        /// <summary>
        /// Ennyi üzenet felelt meg a szürő feltételének.
        /// </summary>
        public int? AcceptanceCount { get; set; }

        public MessageFilterItem()
        {
            
        }

        public MessageFilterItem(string name, bool enabled, MaskOrArbId maskOrArbId, uint maskOrArbIdValue, ArbitrationIdType type, bool remote, MessageDirection direction, MessageFilterMode mode)
        {
            _name = name;
            _enabled = enabled;
            _maskOrArbId = maskOrArbId;
            _maskOrArbIdValue = maskOrArbIdValue;
            _type = type;
            _remote = remote;
            _direction = direction;
            _mode = mode;
        }

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Üzenethez tartozó statisztika alpahelyzetbe hozása.
        /// </summary>
        public void Default()
        {
            AcceptanceCount = null;

            if (DefaultStateComplete != null)
                DefaultStateComplete(this, EventArgs.Empty);
        }

        /// <summary>
        /// Másol a tárolóba.
        /// </summary>
        /// <param name="target"></param>
        public void CopyTo(StorageMessageFilterItem target)
        {
            target.MaskOrArbIdValue = _maskOrArbIdValue;
            target.Name = Name;
            target.Enabled = Enabled;
            target.MaskOrArbId = MaskOrArbId;
            target.MaskOrArbIdValue = MaskOrArbIdValue;
            target.Type = Type;
            target.Remote = Remote;
            target.Direction = Direction;
            target.Mode = Mode;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
           return new MessageFilterItem(_name, _enabled, _maskOrArbId, _maskOrArbIdValue, _type, _remote, _direction, _mode);
        }
    }
}
