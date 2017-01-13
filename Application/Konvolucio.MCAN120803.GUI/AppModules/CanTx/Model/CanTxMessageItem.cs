
namespace Konvolucio.MCAN120803.GUI.AppModules.CanTx.Model
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using Common;
    using Converters;
    using DataStorage;
    using Services;
    using WinForms.Framework;

    /// <summary>
    /// CAN Tx táblázat egy rekordját írja le.
    /// </summary>
    [Serializable]
    public class CanTxMessageItem: ICloneable, INotifyPropertyChanged, IDataErrorInfo
    {
        /// <summary>
        /// Tulajdonság változott.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        
        /// <summary>
        /// IDataErrorInfo interfészhez.
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string this[string columnName] { get { return OnValidate(columnName); } }

        /// <summary>
        /// IDataErrorInfo interfészhez.
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        private string _lastError;

        /// <summary>
        /// IDataErrorInfo interfészhez.
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string Error { get { return _lastError; } }
        
        /// <summary>
        /// Sor indexe.
        /// </summary>
        public int Index { get; internal set; }

        /// <summary>
        /// Üzenet neve.
        /// </summary>
        [TypeConverter(typeof(MessageNameConverter))]
        public string Name 
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(()=>Name));
                }
            }
        }
        private string _name;

        /// <summary>
        /// Üzenethez tartozó billtyüzt nyomógomb.
        /// </summary>
        public string Key
        {
            get { return _key; }
            set
            {
                if (_key != value)
                {
                    if (value != null)
                    {
                        _key = value.ToUpper();
                        if (value.Length > 1)
                            _key = value[0].ToString();
                    }
                    else
                    {
                        _key = null;
                    }
                    OnPropertyChanged(PropertyPlus.GetPropertyName(()=>Key));
                }
            }
        }
        private string _key;

        /// <summary>
        /// Periodikus legyen az üzenet vagy sem.
        /// </summary>
        public bool IsPeriod
        {
            get { return _isPeriod; }
            set
            {
                if (value != _isPeriod)
                {
                    _isPeriod = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(()=>IsPeriod));
                }
            }
        }
        private bool _isPeriod;

        /// <summary>
        /// Ha periodikus az üzenet, akkor a periodus idő ms-ben.
        /// </summary>
        public int PeriodTime
        {
            get { return _periodTime; }
            set 
            {
                if (_periodTime != value)
                {
                    _periodTime = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(()=>PeriodTime));
                    
                }
            }
        }
        private int _periodTime;

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
                    OnPropertyChanged(PropertyPlus.GetPropertyName(()=>Type));
                }
            }
        }
        private ArbitrationIdType _type;

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
                    OnPropertyChanged(PropertyPlus.GetPropertyName(()=>Remote));
                }
            }
        }
        private bool _remote;

        /// <summary>
        /// Üzenet hossza.
        /// </summary>
        public int Length 
        { 
            get 
            {
                if (Data == null)
                    return 0;
                else
                    return Data.Length;
            } 
        }

        /// <summary>
        /// Üzenet arbitrációs azonosítója. 
        /// </summary>
        [TypeConverter(typeof(ArbitrationIdConverter))]
        public uint ArbitrationId 
        {
            get { return _arbitrationId; }
            set
            {
                if (_arbitrationId != value)
                {
                    _arbitrationId = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(()=>ArbitrationId));
                }
            }
        }
        private uint _arbitrationId;

        /// <summary>
        /// Üzenet adat kerete.
        /// </summary>
        [TypeConverter(typeof(DataFrameConverter))]
        public byte[] Data 
        {
            get { return _data; }
            set
            {
                if (value == null)
                {
                    _data = new byte[0];
                    OnPropertyChanged(PropertyPlus.GetPropertyName(()=>Data));
                    OnPropertyChanged(PropertyPlus.GetPropertyName(()=>Length));
                }
                else if (_data == null)
                {
                    _data = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => Data));
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => Length));
                }
                else if (!value.SequenceEqual(_data))
                {
                    _data = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => Data));
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => Length));
                }
            }
        }
        private byte[] _data;

        /// <summary>
        /// Üzenethez tartozó dokumentáció.
        /// </summary>
        public string Documentation
        {
            get { return _documentation; }
            set
            {
                if (_documentation != value)
                {
                    _documentation = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() =>Documentation));
                }
            }
        
        }
        private string _documentation;

        /// <summary>
        /// Üzenethez tartozó leírás.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(PropertyPlus.GetPropertyName(() => Description));
                }
            }
        }
        private string _description;

        /// <summary>
        /// Küldő szál utoljára ekkor nézett rá utoljára.
        /// </summary>
        public long LastUpdateDateTimeTicks { get; set; }
        
        /// <summary>
        /// Konstructor
        /// </summary>
        public CanTxMessageItem()
        {

        }

        /// <summary>
        /// Konstructor
        /// </summary>
        public CanTxMessageItem(string name,
                            string key,
                            int periodTime,
                            ArbitrationIdType type,
                            bool remote,
                            uint arbitrationId,
                            byte[] data,
                            string documentation,
                            string description
                            
                         ):this()
        {
            Name = name;
            Key = key;
            PeriodTime = periodTime;
            Type = type;
            Remote = remote;
            ArbitrationId = arbitrationId;
            Data = data;
            Description = description;
            Documentation = documentation;
        }

        /// <summary>
        /// Mezők validálása
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        private string OnValidate(string columnName)
        {
            #region Name
            if (columnName == PropertyPlus.GetPropertyName(() => Name))
            {
                object obj = typeof(CanTxMessageItem).GetProperty(PropertyPlus.GetPropertyName(() => Name)).GetValue(this, null);
                if (!string.IsNullOrWhiteSpace(obj as string))
                {
                    /*Ez itt kell*/
                    _lastError = ConsistencyCheck.Symbol(obj as string);
                    return _lastError;
                }
                else
                {
                    return null;
                }
            }
            #endregion

            #region Data
            if (columnName == PropertyPlus.GetPropertyName(() => Data))
            {
                object obj = typeof(CanTxMessageItem).GetProperty(PropertyPlus.GetPropertyName(() => Data)).GetValue(this, null);
                if (string.IsNullOrWhiteSpace(obj as string))
                {
                    /*Ennek itt nincs értelme*/
                    //try { new DataFrameConverter().ConvertFrom(obj as string); }
                    //catch (Exception ex) { LastError = ex.Message; return LastError; }
                }

                return null;
            }
            else if (columnName == PropertyPlus.GetPropertyName(() => Length))
            {
                object obj = typeof(CanTxMessageItem).GetProperty(PropertyPlus.GetPropertyName(() => Length)).GetValue(this, null);

                if ((int)obj > 8)
                {
                    _lastError = CultureService.Instance.GetString(CultureText.text_DataTooLong);
                    return _lastError;
                }
            }
            else if (columnName == PropertyPlus.GetPropertyName(() => ArbitrationId))
            {
               
            }

            _lastError = null;
            return null;


            #endregion
        }

        /// <summary>
        /// Tulajdonság változott.
        /// </summary>
        /// <param name="name"></param>
        private void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Lista másolása a projectbe.
        /// </summary>
        /// <param name="target">Project cél.</param>
        public void CopyTo(StorageCanTxMessageItem target)
        { 
            target.Name = Name;
            target.Key  = Key;
            target.IsPeriod = IsPeriod;
            target.PeriodTime  = PeriodTime;
            target.Type = Type;
            target.Remote = Remote;
            target.ArbitrationId = CustomDataConversion.UInt32ToStringHighSpeed(ArbitrationId);
            target.Data = CustomDataConversion.ByteArrayToStringHighSpeed(Data);
            target.Documentation = _documentation;
            target.Description = Description;
        }

        /// <summary>
        /// Klónozás másoláshoz.
        /// </summary>
        public object Clone()
        {
           return new CanTxMessageItem(_name, _key, _periodTime, _type, _remote, _arbitrationId, _data, _documentation, _description);
        }
    }
}