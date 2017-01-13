

namespace Konvolucio.MCAN120803.GUI.AppModules.Tracer.Model
{
    using System.Collections.ObjectModel;
    using WinForms.Framework;

    public sealed class MessageTraceCollection : Collection<MessageTraceItem>
    {

        public event ListChangingEventHandler<MessageTraceItem> ListChanging;

        public new void Clear()
        {
            OnReseting();
            base.Clear();
        }

        protected override void InsertItem(int index, MessageTraceItem item)
        {
            var messageTraceItem = item as MessageTraceItem;
            if (messageTraceItem != null) 
                messageTraceItem.Index = index + 1;
            base.InsertItem(index, item);
        }

        private void OnReseting()
        {
            ListChanging?.Invoke(this, new ListChangingEventArgs<MessageTraceItem>(ListChangingType.Clearing, null));
        }
    }
}
