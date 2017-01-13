// -----------------------------------------------------------------------
// <copyright file="MessageFilterCollection.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Filter.Model
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    using WinForms.Framework;
    using Common;
    using DataStorage;
    using Services;

    public class MessageFilterCollection : BindingList<MessageFilterItem>, IRowReOredable
    {
        public event ListChangingEventHandler<MessageFilterItem> ListChanging;
       
        public event EventHandler DefaultStateComplete;

        private readonly object _lockObj = new object();


        public new void Add(MessageFilterItem item)
        {
            var messageFilterItem = item as MessageFilterItem;
            if (messageFilterItem != null) 
                messageFilterItem.Guid = Guid.NewGuid().ToString();
            base.Add(item);
        }

        public new void Clear()
        {
            OnListChanging(ListChangingType.Clearing, null);
            base.Clear();
        }

        public new void ResetBindings()
        {
            OnListChanging(ListChangingType.Clearing, null);   
            base.ResetBindings();
        }

        protected override void RemoveItem(int index)
        {
            OnListChanging(ListChangingType.ItemRemoving, this[index]);   
            base.RemoveItem(index);
        }

        protected void OnListChanging(ListChangingType type, MessageFilterItem item)
        {
            if (ListChanging != null)
                ListChanging(this, new ListChangingEventArgs<MessageFilterItem>(type, item));
        }

        /// <summary>
        /// Project tárolóba másolja.
        /// </summary>
        /// <param name="target"></param>
        public void CopyTo(StorageMessageFilterCollection target)
        {
            target.Clear();
            foreach (var sourceItem in this)
            {
                var targetItem = new StorageMessageFilterItem();
                sourceItem.CopyTo(targetItem);
                target.Add(targetItem);
            }
        }

        /// <summary>
        /// Elem hozzáadása a listához a felületről
        /// </summary>
        public void GuiAdd(MessageFilterItem item)
        {
            item.Name = CollectionTools.GetExtendedUniqueItemName(item.Name, this.Select(n => n.Name).ToArray<string>());
            Add(item);
        }

        /// <summary>
        /// Elem beszurása a felületről.
        /// Az elem lehet létező vagy új.
        /// </summary>
        /// <param name="index">Index. 0-ás bázisíú</param>
        /// <param name="item">elem.</param>
        public void GuiInsert(int index, MessageFilterItem item)
        {
            item.Name = CollectionTools.GetExtendedUniqueItemName(item.Name, this.Select(n => n.Name).ToArray<string>());
            InsertItem(index, item);
        }

        public MessageFilterItem GuiGetNew()
        {
            var name = CollectionTools.GetNewName(this.Select(n => n.Name).ToArray<string>(), "New_Filter");
            var newItem = new MessageFilterItem()
            {
                Name = name,
                Direction = MessageDirection.Transmitted,
                Enabled = true,
                MaskOrArbId = MaskOrArbId.ArbId,
                MaskOrArbIdValue = 0x0,
                Type = ArbitrationIdType.Standard,
            };
            return newItem;
        }

        protected override void InsertItem(int index, MessageFilterItem item)
        {
            var messageSenderItem = item as MessageFilterItem;
            if (messageSenderItem != null)
                messageSenderItem.Index = index;

            base.InsertItem(index, item);

            for (var i = 1; i < this.Count + 1; i++)
            {
                var senderItem = this[i - 1] as MessageFilterItem;
                if (senderItem != null)
                    senderItem.Index = i;
            }
        }

        public void ItemMoveTo(object item, int index)
        {
            lock (_lockObj) 
            {
                RaiseListChangedEvents = false;
                Remove(item as MessageFilterItem);
                Insert(index, item as MessageFilterItem);
                RaiseListChangedEvents = true;
                ResetBindings();
            }
        }

        /// <summary>
        /// Üzenethez tartozó statisztika alpahelyzetbe hozása.
        /// </summary>
        public void Default()
        {
            foreach (var item in this)
                item.Default();

            if (DefaultStateComplete != null)
                DefaultStateComplete(this, EventArgs.Empty);
        }

        public bool DoAddToLog(uint arbitrationId, ArbitrationIdType type, bool remote, MessageDirection direction)
        {
            foreach (var filter in this)
            {
                if (filter.Enabled)
                {
                    if (filter.MaskOrArbId == MaskOrArbId.ArbId)
                    {
                        if (filter.MaskOrArbIdValue == arbitrationId && filter.Type == type && filter.Remote == remote && filter.Direction == direction)
                        {
                            if (filter.Mode == MessageFilterMode.InsertToLog || filter.Mode == MessageFilterMode.InsertToTraceAndLog)
                            {
                                if (filter.AcceptanceCount == null) filter.AcceptanceCount = 1;
                                else filter.AcceptanceCount++;
                                return true;
                            }
                        }
                    }

                    if (filter.MaskOrArbId == MaskOrArbId.Mask)
                    {
                        if ((arbitrationId & filter.MaskOrArbIdValue) == arbitrationId)
                        {
                            if (filter.Type == type && filter.Remote == remote && filter.Direction == direction)
                            {
                                if (filter.Mode == MessageFilterMode.InsertToLog || filter.Mode == MessageFilterMode.InsertToTraceAndLog)
                                {
                                    if (filter.AcceptanceCount == null) filter.AcceptanceCount = 1;
                                    else filter.AcceptanceCount++;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool DoAddToTrace(uint arbitrationId, ArbitrationIdType type, bool remote, MessageDirection direction)
        {
            foreach (var filter in this)
            {
                if (filter.Enabled)
                {
                    if (filter.MaskOrArbId == MaskOrArbId.ArbId)
                    {
                        if (filter.MaskOrArbIdValue == arbitrationId && filter.Type == type && filter.Remote == remote && filter.Direction == direction)
                        {
                            if (filter.Mode == MessageFilterMode.Drop)
                            {
                                if (filter.AcceptanceCount == null) filter.AcceptanceCount = 1;
                                else filter.AcceptanceCount++;
                                return false;
                            }

                            if (filter.Mode == MessageFilterMode.InsertToTrace || filter.Mode == MessageFilterMode.InsertToTraceAndLog)
                            {
                                if (filter.AcceptanceCount == null) filter.AcceptanceCount = 1;
                                else filter.AcceptanceCount++;
                                return true;
                            }
                        }
                    }

                    if (filter.MaskOrArbId == MaskOrArbId.Mask)
                    {
                        if ((arbitrationId & filter.MaskOrArbIdValue) == arbitrationId)
                        {
                            if (filter.Type == type && filter.Remote == remote && filter.Direction == direction)
                            {
                                if (filter.Mode == MessageFilterMode.Drop)
                                {
                                    if (filter.AcceptanceCount == null) filter.AcceptanceCount = 1;
                                    else filter.AcceptanceCount++;
                                    return false;
                                }

                                if (filter.Mode == MessageFilterMode.InsertToTrace || filter.Mode == MessageFilterMode.InsertToTraceAndLog)
                                {
                                    if (filter.AcceptanceCount == null) filter.AcceptanceCount = 1;
                                    else filter.AcceptanceCount++;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}
