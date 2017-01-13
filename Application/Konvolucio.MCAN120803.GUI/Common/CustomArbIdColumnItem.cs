// -----------------------------------------------------------------------
// <copyright file="CustomArbIdEditorItem.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------



namespace Konvolucio.MCAN120803.GUI.Common
{
    using System.Linq;
    using System.ComponentModel;
    using DataStorage;
    using WinForms.Framework;
    using Services;

    /// <summary>
    /// Egyedi mező leírója.
    /// </summary>
    public sealed class CustomArbIdColumnItem : INotifyPropertyChanged, IDataErrorInfo
    {
        public string this[string columnName] { get { return OnValidate(columnName); } }

        private string _lastError;
        public string Error { get { return _lastError; } }
              
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Az oszlop neve.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnProppertyChanged(PropertyPlus.GetPropertyName(()=>Name));
                }
            }
        }
        private string _name;

        /// <summary>
        /// Az arbitráció azonosító típusa.
        /// </summary>
        public ArbitrationIdType Type
        {
            get { return _type; }
            set
            {
                if (value != _type)
                {
                    _type = value;
                    OnProppertyChanged(PropertyPlus.GetPropertyName(() => Type));
                }
            }
        }
        private ArbitrationIdType _type;

        /// <summary>
        /// Az egyedi Arbi Az. ettől a bittől kezdődik. 
        /// </summary>
        public int StartBit
        {
            get
            {
                return _startBit;
            }

            set
            {
                if (value != _startBit)
                {
                    _startBit = value;
                    OnProppertyChanged(PropertyPlus.GetPropertyName(() => StartBit));
                }
            }
        }
        private int _startBit;

        /// <summary>
        /// Az egyedi érték ilyen hosszú.
        /// </summary>
        public int LengthBit
        {
            get { return _lengthBit; }
            set
            {
                if (value != _lengthBit)
                {
                    _lengthBit = value;
                    OnProppertyChanged(PropertyPlus.GetPropertyName(() => LengthBit));
                }
            }
        }
        private int _lengthBit;

        /// <summary>
        /// Az egyedi érték megjelnítése előtt ennyit kell shiftelni balra.
        /// íráskor pedig ennyit jobbra.
        /// </summary>
        public int Shift
        {
            get { return _shift; }
            set 
            {
                if (value != _shift)
                {
                    _shift = value;
                    OnProppertyChanged(PropertyPlus.GetPropertyName(() => Shift));
                }
            }
        }
        private int _shift;

        /// <summary>
        /// Az egyedi éréket ezzel lehet formázni.
        /// pl:X2
        /// </summary>
        public string Format
        {
            get { return _fromat; }
            set 
            {
                if (value != _fromat)
                {
                    _fromat = value;
                    OnProppertyChanged(PropertyPlus.GetPropertyName(() => Format));
                }
            }
        }
        private string _fromat;

        /// <summary>
        /// Leírás az egyedi mezőhöz, ez a ToolTip-ben jelenik meg.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnProppertyChanged(PropertyPlus.GetPropertyName(() => Description));
                }
            }
        }
        private string _description;


        [Browsable(false)]
        public CustomArbIdColumnCollection Columns { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public CustomArbIdColumnItem()
        {
            Name = "No_Name_Column";
            Type =  ArbitrationIdType.Extended;
            Shift = 0;
            StartBit =  0;
            LengthBit = 1;
            Format = "X2";
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="name">Az oszlop neve</param>
        /// <param name="type">Üzenet típusa</param>
        /// <param name="shift">Sift értéke</param>
        /// <param name="startBit">StartBit értéke</param>
        /// <param name="lengthBit"></param>
        /// <param name="format">Fomázó karakterek: "X2"</param>
        /// <param name="description">leírás</param>
        public CustomArbIdColumnItem( string name,
                                                 ArbitrationIdType type,
                                                 int startBit,
                                                 int lengthBit,
                                                 int shift,
                                                 string format,
                                                  string description)
        {
            Name = name;
            Type = type;
            StartBit = startBit;
            LengthBit = lengthBit;
            Shift = shift;
            Format = format;
            Description = description;
        }

        /// <summary>
        /// Startbittől kezdve lengtbthBit hosszan egyesek vannak. 
        /// </summary>
        /// <param name="startBit">Nullás bázisú.</param>
        /// <param name="lengthBit">Ennyi "1" van egymás után.</param>
        /// <returns></returns>
        public uint SetMask(int startBit, int lengthBit)
        {
            uint retval = 0;
            for (var i = startBit; i < startBit + lengthBit; i++)
                retval |= (uint)(1 << i);
            return retval;
        }

        /// <summary>
        /// StartBit-től kezdve nullák vannak lengthBit hosszan.
        /// </summary>
        /// <param name="startBit">Nullás bázisú.</param>
        /// <param name="lengthBit">Ennyi "0" van egymás után</param>
        /// <returns></returns>
        public uint ClrMask(int startBit, int lengthBit)
        {
            return ~SetMask(startBit, lengthBit);
        }

        /// <summary>
        /// Az arbitrationId-ból kivágja a megfelelő részt és jelenti meg,
        /// igény esetén shiftel.
        /// </summary>
        /// <param name="arbitrationId"></param>
        /// <returns></returns>
        public uint GetValue(uint arbitrationId)
        {
            uint normArbId = (arbitrationId & SetMask(StartBit, LengthBit)) >> StartBit;

            if (Shift != 0)
                return normArbId << Shift;

            return normArbId;
        }

        /// <summary>
        /// Az értéket módosítja az ArbId-ben ott ahol kell, startBit és bit length alapján.
        /// </summary>
        /// <param name="customValue">Érétk ami módosítja maszk és shift alapján az arbitrationId-t</param>
        /// <param name="arbitrationId">A modósítandó arbitrationId</param>
        /// <returns>A startBit és shift alapján módosított arbitratinId </returns>
        public uint SetValue(uint customValue, uint arbitrationId)
        {
            customValue >>= Shift;

            return arbitrationId & ClrMask(StartBit, LengthBit) | ((customValue << StartBit) & SetMask(StartBit, LengthBit));
        }

        private void OnProppertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private string OnValidate(string columnName)
        {
            #region Name
            if (columnName == PropertyPlus.GetPropertyName(() => Name))
            {
                const int ruleMaxNameLength = 32;

                var value = typeof(CustomArbIdColumnItem).GetProperty(PropertyPlus.GetPropertyName(() => Name)).GetValue(this, null);
                if (string.IsNullOrWhiteSpace(value as string) || string.IsNullOrEmpty(value as string))
                {
                    _lastError = CultureText.text_TheFieldCantBeEmtpy;
                    return _lastError;
                }
                
                if ((value as string).Length > ruleMaxNameLength)
                {
                    _lastError = string.Format(CultureService.Instance.GetString(CultureText.text_SymbolNameMaxLength), ruleMaxNameLength);
                    return _lastError;
                }

                var item = Columns.Count(n => n.Name == (string) value);
                if (item != 1)
                {
                    _lastError = CultureService.Instance.GetString(CultureText.text_AlreadyExists);
                    return _lastError;
                }

                _lastError = null;
                return null;
            }
            #endregion

            #region Shift
            else if (columnName == PropertyPlus.GetPropertyName(() => Shift))
            {
                const int ruleMaxShiftValue = 31;

                var value = (int)typeof(CustomArbIdColumnItem).GetProperty(PropertyPlus.GetPropertyName(() => Shift)).GetValue(this, null);
                if (value < 0)
                {
                    _lastError = CultureService.Instance.GetString(CultureText.text_ItCanNotBeANegativeNumber);
                    return _lastError;
                }
                else if (value > ruleMaxShiftValue)
                {
                    _lastError = string.Format(CultureService.Instance.GetString(CultureText.text_TheShiftIsTooLargeUpTo), ruleMaxShiftValue);
                    return _lastError;
                }
            }
            #endregion

            #region StartBit
            else if (columnName == PropertyPlus.GetPropertyName(() => StartBit))
            {
                const int ruleMaxStartBitValue = 31;

                var value = (int)typeof(CustomArbIdColumnItem).GetProperty(PropertyPlus.GetPropertyName(() => StartBit)).GetValue(this, null);
                if (value < 0)
                {
                    _lastError = CultureService.Instance.GetString(CultureText.text_ItCanNotBeANegativeNumber);
                    return _lastError;
                }
                else if (value > ruleMaxStartBitValue)
                {
                    _lastError = string.Format(CultureService.Instance.GetString(CultureText.text_ItCanNotBeMoreThan), ruleMaxStartBitValue);
                    return _lastError;
                }
            }
            #endregion

            #region LengthBit
            else if (columnName == PropertyPlus.GetPropertyName(() => LengthBit))
            {
                const int ruleMaxLengthValue = 32;

                var value = (int)typeof(CustomArbIdColumnItem).GetProperty(PropertyPlus.GetPropertyName(() => LengthBit)).GetValue(this, null);
                if (value < 0)
                {
                    _lastError = CultureService.Instance.GetString(CultureText.text_ItCanNotBeANegativeNumber);
                    return _lastError;
                }
                else if (value > ruleMaxLengthValue)
                {
                    _lastError = string.Format(CultureService.Instance.GetString(CultureText.text_ItCanNotBeMoreThan), ruleMaxLengthValue);
                    return _lastError;
                }
                else if (value == 0)
                {
                    _lastError = CultureService.Instance.GetString(CultureText.text_TheValueCanNotBeNull);
                    return _lastError;
                }
            }
            #endregion

            #region Description
            else if (columnName == PropertyPlus.GetPropertyName(() => Description))
            {
                const int ruleMaxDescriptionLength = 255;

                var value = (string)typeof(CustomArbIdColumnItem).GetProperty(PropertyPlus.GetPropertyName(() => Description)).GetValue(this, null);
                if (value != null && value.Length > ruleMaxDescriptionLength)
                {
                    _lastError = CultureService.Instance.GetString(CultureText.text_TheTextIsTooLong) + string.Format(CultureService.Instance.GetString(CultureText.text_ItCanNotBeMoreThan), ruleMaxDescriptionLength);
                    return _lastError;
                }
            }
            #endregion
            _lastError = null;
            return null;
        }

        /// <summary>
        /// Ezt az oszlop leírót a Project elembe másolja.
        /// </summary>
        /// <param name="target"></param>
        public void CopyTo(StorageCustomArbIdColumnItem target)
        {
            target.Name = Name;
            target.Type = Type;
            target.StartBit = StartBit;
            target.LengthBit = LengthBit;
            target.Shift = Shift;
            target.Format = Format;
            target.Description = Description;
        }

        /// <summary>
        /// Egy másik ugyanilyen elmebe másolja.
        /// </summary>
        /// <param name="target"></param>
        public void CopyTo(CustomArbIdColumnItem target)
        {
            target.Name = Name;
            target.Type = Type;
            target.StartBit = StartBit;
            target.LengthBit = LengthBit;
            target.Shift = Shift;
            target.Format = Format;
            target.Description = Description;
        }
    }
}
