
namespace Konvolucio.MCAN120803.GUI.DataStorage
{
    using System.Collections.Generic;


    public class StorageMessageFilterCollection : List<StorageMessageFilterItem>
    {
        public void CopyTo(AppModules.Filter.Model.MessageFilterCollection target)
        {
            target.Clear();
            foreach (var item in this)
            {
                var targetItem = new AppModules.Filter.Model.MessageFilterItem();
                item.CopyTo(targetItem);
                ((AppModules.Filter.Model.MessageFilterCollection) target).Add(targetItem);
            }
        }

        public void New()
        {
            this.Clear();
        }
    }

}
