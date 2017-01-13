
namespace Konvolucio.MCAN120803.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AppModules.Adapters;

    class PlayAppEvent : IApplicationEvent
    {
        public IAdapterService Adapter;

        public PlayAppEvent(IAdapterService adapter)
        {
            Adapter = adapter;
        }

        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}
