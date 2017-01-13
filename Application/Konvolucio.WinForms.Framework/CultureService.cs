namespace Konvolucio.WinForms.Framework
{
    using System.Resources;

    static class CultureService
    {
        
        static readonly ResourceManager _resource;

        /// <summary>
        /// Konstruktor
        /// </summary>
        static CultureService()
        {
            _resource = new ResourceManager("Konvolucio.WinForms.Framework.TextResource", typeof(Konvolucio.WinForms.Framework.KnvDataGridView).Assembly);
        }
        
        /// <summary>
        /// Az erőforrásbó string megszerzésee név alapján.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetString(string name)
        {
            var str = string.Empty;
            try
            {
                str = _resource.GetString(name);
            }
            catch
            {
                str = name;
            }
            return str;
        }
    }
}
