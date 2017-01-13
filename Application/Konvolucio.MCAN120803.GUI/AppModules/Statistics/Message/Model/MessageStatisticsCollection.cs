
namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Message.Model
{
    using System.Collections;
    using System.ComponentModel;
    using WinForms.Framework;

    public class MessageStatisticsCollection : BindingList<MessageStatisticsItem>
    {

        /// <summary>
        /// Müvelet fog következni, a GUI-t értesítit mielött bekövetkezne pl a Clear..
        /// </summary>
        public event ListChangingEventHandler<MessageStatisticsItem> ListChanging;

        public MessageStatisticsCollection()
        {
   
        }

        /// <summary>
        /// Stataisztika lista teljes törlése.
        /// </summary>
        public new void Clear()
        {
            lock ((this as ICollection).SyncRoot)
            {
                OnClearing();
                base.Clear();
            }
        }

        /// <summary>
        /// Üzenetek Gyakoriságának számításához periodikusan meg kell hívni.
        /// </summary>
        public void UpdateRate()
        {
            lock ((this as ICollection).SyncRoot)
            {
                foreach (var message in this)
                    message.UpdateRate();
            }
        }

        private void OnClearing()
        {
            if (ListChanging != null)
                ListChanging(this, new ListChangingEventArgs<MessageStatisticsItem>(ListChangingType.Clearing, null));
        }

        protected override void InsertItem(int index, MessageStatisticsItem item)
        {
            lock ((this as ICollection).SyncRoot)
            {
                var messageSenderItem = item as MessageStatisticsItem;
                if (messageSenderItem != null)
                    messageSenderItem.Index = index;

                base.InsertItem(index, item);

                for (var i = 1; i < this.Count + 1; i++)
                {
                    var senderItem = this[i - 1] as MessageStatisticsItem;
                    if (senderItem != null)
                        senderItem.Index = i;
                }
            }
        }
    }
}
