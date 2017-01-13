namespace Konvolucio.MCAN120803.GUI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Resources;
    using System.Threading;
    using System.Globalization;
    using System.ComponentModel;

    public interface ICultureService
    {
        /// <summary>
        /// Az erőforrásbó string megszerzésee név alapján.
        /// </summary>
        string[] SupportedCulutreNames { get; }
        
        /// <summary>
        /// Aktuális szálon Culutre nének lekérdzése beállíása
        /// </summary>
        string CurrentCultureName { get; set; }
        
        /// <summary>
        /// Használt Text erőforrás megszerzése
        /// </summary>
        ResourceManager TextResource { get; }

        /// <summary>
        /// Az erőforrásbó string megszerzésee név alapján.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string GetString(string name);

    }

    class CultureService : ICultureService
    {
        private static readonly ICultureService _instance = new CultureService();
        public static ICultureService Instance { get { return _instance; } }

        /// <summary>
        /// Az erőforrásbó string megszerzésee név alapján.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string this[string name]
        {
            get
            {
                return GetString(name);
            }
        }
        
        /// <summary>
        /// Támogatott Culturák
        /// </summary>
        public string[] SupportedCulutreNames 
        {
            get
            {
                return new string[] 
                {
                    "en-US",
                    "hu-HU"
                };
            }
        }
        /// <summary>
        /// Aktuális szálon Culutre nének lekérdzése beállíása
        /// </summary>
        public string CurrentCultureName
        {
            get
            {
                return _cultureName;
            }
            set
            {
                _cultureName = value;
                _currentCulture = new CultureInfo(_cultureName);
                Thread.CurrentThread.CurrentCulture = _currentCulture;
                Thread.CurrentThread.CurrentUICulture = _currentCulture;
            }
        }
        /// <summary>
        /// Aktuális szálon a Culture megszerzése
        /// </summary>
        public CultureInfo CurrentCulutre
        {
            get
            {
                return _currentCulture;
            }
        }

        /// <summary>
        /// Használt Text erőforrás megszerzése
        /// </summary>
        public ResourceManager TextResource
        {
            get
            {
                return _resource;
            }
        }

        CultureInfo _currentCulture;
        string _cultureName;
        readonly ResourceManager _resource;

        /// <summary>
        /// Konstruktor
        /// </summary>
        private CultureService()
        {
            _resource = new ResourceManager("Konvolucio.MCAN120803.GUI.TextResource", typeof(Program).Assembly);
            CurrentCultureName = "en-US";
        }
        
        /// <summary>
        /// Az erőforrásbó string megszerzésee név alapján.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetString(string name)
        {
            var str = string.Empty;
            try
            {
                str = _resource.GetString(name);
            }
            catch
            {
                AppDiagService.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + "=>" + "ERROR" + " " + "Culture Not found Resource for: [" + name + "]");
                str = name;
            }
            return str;
        }
    }
}
