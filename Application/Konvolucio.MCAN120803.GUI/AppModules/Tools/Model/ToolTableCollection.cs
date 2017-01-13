// -----------------------------------------------------------------------
// <copyright file="ToolTableCollection.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Tools.Model
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Reflection;
    using CanTx.Model;
    using Common;
    using DataStorage;
    using WinForms.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class ToolTableCollection : BindingList<ToolTableItem>, IDisposable, IListChanging<ToolTableItem>
    {

        /// <summary>
        /// Egy tábla változott a listában.
        /// </summary>
        public event ListChangedEventHandler TableChanged;

        /// <summary>
        /// Tábálák listája változott.
        /// </summary>
        public event ListChangingEventHandler<ToolTableItem> ListChanging;

        private bool _disposed;

        /// <summary>
        /// Tx Queue beállítása
        /// </summary>
        /// <param name="txQueue"></param>
        public void SetTxQueue(SafeQueue<CommonCanMessage> txQueue)
        {
            foreach (var item in this)
            {
                foreach (var table in this)
                {
                    var canTxTable = table.TableObject as CanTxMessageCollection;
                    if (canTxTable != null)
                        canTxTable.TxQueue = txQueue;
                }
            }
        }
    
        /// <summary>
        /// Indítás
        /// </summary>
        public void Start()
        {
            foreach (var table in this)
            {
                var canTxTable = table.TableObject as CanTxMessageCollection;
                if(canTxTable != null)
                    canTxTable.Start();
            }
        }

        /// <summary>
        /// Leállítás
        /// </summary>
        public void Stop()
        {
            foreach (var table in this)
            {
                var canTxTable = table.TableObject as CanTxMessageCollection;
                if(canTxTable != null)
                    canTxTable.Stop();
            }
        }

        /// <summary>
        /// Egy Tool tábla beszúrása a listába.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void InsertItem(int index, ToolTableItem item)
        {
            item.TableChanged += TableChanged;
            base.InsertItem(index, item);
        }

        /// <summary>
        ///  Public implementation of Dispose pattern callable by consumers. 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected implementation of Dispose pattern. 
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                foreach (var table in this)
                {
                    var canTxTable = table.TableObject as CanTxMessageCollection;
                    if (canTxTable != null)
                        canTxTable.Dispose();
                }
                /*szabályos leállítás*/
                Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "(): Running-> Stop()");
            }
            _disposed = true;
        }

        /// <summary>
        /// Táblák mentése a project tárolóba.
        /// </summary>
        /// <param name="target"></param>
        public void CopyTo(StorageToolCollection target)
        {
            target.Clear();
            foreach (var item in this)
            {
                var targetItem = new StorageToolItem
                {
                    Name = item.Name,
                    ToolType = item.ToolType,
                };

                if (item.ToolType == ToolTypes.CAN)
                {
                    var castedItem = item.TableObject as CanTxMessageCollection;
                    if (castedItem != null)
                    {
                        targetItem.ToolObject = new StorageCanTxMessageCollection();
                        castedItem.CopyTo((StorageCanTxMessageCollection) targetItem.ToolObject);
                    }
                    target.Add(targetItem);
                }
            }
        }

        /// <summary>
        /// Egy tábla törlése
        /// </summary>
        /// <param name="item"></param>
        public new void Remove(ToolTableItem item)
        {
            if (ListChanging != null)
              ListChanging(this, new ListChangingEventArgs<ToolTableItem>(ListChangingType.ItemRemoving, item));
            base.Remove(item);
        }
    }
}
