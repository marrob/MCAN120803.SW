
namespace Konvolucio.MCAN120803.GUI.DataStorage
{
    using System.ComponentModel;
    using Common;
    using AppModules.CanTx.Model;
    using AppModules.Tools.Model;

    public class StorageToolCollection : BindingList<StorageToolItem>
    {
        /// <summary>
        /// A project tárolóbol visszatölti a Tool táblákat.
        /// </summary>
        /// <param name="target">Ide tölti vissza.</param>
        public void CopyTo(ToolTableCollection target)
        {

            target.Clear();

            foreach (var item in this)
            {
                var targetToolTableItem = new ToolTableItem()
                {
                    Name = item.Name,
                    ToolType = item.ToolType,
                };

                if (item.ToolType == ToolTypes.CAN)
                {
                    var castedItem = item.ToolObject as StorageCanTxMessageCollection;
                    if (castedItem != null)
                    {
                        targetToolTableItem.TableObject = new CanTxMessageCollection();
                        castedItem.CopyTo((CanTxMessageCollection) targetToolTableItem.TableObject);
                    }
                }
                target.Add(targetToolTableItem);
            }
        }

        /// <summary>
        /// Új Project
        /// </summary>
        public void New()
        {
            var toolTemplate = new StorageToolItem();

            toolTemplate.ToolType = ToolTypes.CAN;
            toolTemplate.Name = "Template CAN Tx";

            var canTxTable = new StorageCanTxMessageCollection();
            var canTxRecord = new StorageCanTxMessageItem();
            canTxRecord.ArbitrationId = "1";
            canTxRecord.Data = "A1 B2 C3 D4 E5 F6 A7 B8";
            canTxRecord.Key = "A";

            canTxTable.Add(canTxRecord);

            toolTemplate.ToolObject = canTxTable;

            this.Add(toolTemplate);
        }
    }
}
