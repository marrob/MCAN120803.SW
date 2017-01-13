

namespace Konvolucio.MCAN120803.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AppModules.Adapters;

    class AdapterChangedAppEvent : IApplicationEvent
    {
        public IAdapterService Adapter;

        public AdapterChangedAppEvent(IAdapterService adapter)
        {
            Adapter = adapter;
        }
        
        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}
