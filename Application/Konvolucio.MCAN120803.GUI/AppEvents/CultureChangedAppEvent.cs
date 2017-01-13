
namespace Konvolucio.MCAN120803.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using Services; /*Culture Service*/


    /// <summary>
    /// 
    /// </summary>
    [Obsolete("MegszÜnt nincs futásidejű nyelválasztás.")]
    class CultureChangedAppEventx : IApplicationEvent
    {
        public ICultureService Current;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="language"></param>
        public CultureChangedAppEventx(ICultureService service)
        {
            Current = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.GetType().Name + " " + (Current != null ? Current.CurrentCultureName : "null->Clear");
        }
    }
}
