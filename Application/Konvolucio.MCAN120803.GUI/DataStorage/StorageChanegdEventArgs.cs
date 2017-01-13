// -----------------------------------------------------------------------
// <copyright file="ProjectChanegdEventArgs.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.DataStorage
{
    using System;
    using System.ComponentModel;
    using Services;

    /// <summary>
    /// Storage változását követi.
    /// </summary>
    public class StorageChanegdEventArgs : EventArgs
    {
        /// <summary>
        /// Projectnek ez az Objektuma változot*/
        /// </summary>
        public DataObjects DataObjects { get; private set; }
        public object SenderObject { get; private set; }
        public ListChangedEventArgs ListChangedEventArgs { get; private set; }
        public PropertyDescriptor PropertyDescriptor { get; private set; }

        public StorageChanegdEventArgs(DataObjects dataObjects, object senderObject, PropertyDescriptor propertyDesciriptor)
        {
            DataObjects = dataObjects;
            PropertyDescriptor = propertyDesciriptor;
            SenderObject = senderObject;
        }

        public StorageChanegdEventArgs(DataObjects dataObjects, object senderObject, ListChangedEventArgs listChangedEventArgs)
        {
            DataObjects = dataObjects;
            ListChangedEventArgs = listChangedEventArgs;
            SenderObject = senderObject;
        }

        public StorageChanegdEventArgs(DataObjects dataObjects)
        {
            ListChangedEventArgs = null;
            PropertyDescriptor = null;
            SenderObject = null;
        }

        public override string ToString()
        {
            string str = "Object: " + DataObjects + ", ";

            if (ListChangedEventArgs != null)
            {
                str += "ListChangedType: " + ListChangedEventArgs.ListChangedType + ", ";
                str += "NewIndex:" + ListChangedEventArgs.NewIndex + ", ";
                str += "OldIndex:" + ListChangedEventArgs.OldIndex + ", ";

                var property = ListChangedEventArgs.PropertyDescriptor;
                if (property != null)
                {
                    str += "PropertyName:" + property.Name + ", ";
                    //str += "Value: " + property.GetValue(((IBindingList)SenderObject)[ListChangedEventArgs.NewIndex]);
                }
            }

            if (PropertyDescriptor != null)
            {
                str += "PropertyName:" + PropertyDescriptor.Name + ", ";
                str += "Value: " + PropertyDescriptor.GetValue(SenderObject);
            }
            return str;
        }
    }
}
