
namespace Konvolucio.MCAN120803.GUI.DataStorage
{
    using System.Collections.Generic;
    using AppModules.CanTx.Model;

    /// <summary>
    /// Tool CanTx kollekcióját tárolja.
    /// </summary>
    public class StorageCanTxMessageCollection : List<StorageCanTxMessageItem>
    {
        /// <summary>
        /// Visszatöltés
        /// </summary>
        /// <param name="target"></param>
        public void CopyTo(CanTxMessageCollection target)
        {
            target.Clear();
            target.RaiseListChangedEvents = false;
            foreach (var item in this)
            {
                var targetItem = new CanTxMessageItem();
                item.CopyTo(targetItem);
                ((CanTxMessageCollection) target).Add(targetItem);
            }
            target.RaiseListChangedEvents = true;
            target.ResetBindings();
        }

        /// <summary>
        /// Új Project
        /// </summary>
        public void New()
        {
            Clear();
        }
    }
}
