// -----------------------------------------------------------------------
// <copyright file="VersionObject.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.VectorCANdb.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// 4 Version
    /// Keyword: VERSION
    /// </summary>
    public class VersionObject : DbcObject<VersionObject>
    {
        public string Value;

        /// <summary>
        /// "VERSION \"12\""
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public VersionObject Parse(string input)
        {
            string pattern = "\"(.*?)\"";
            Value = Regex.Match(input, pattern).Groups[1].Value;
            return this;
        }


        public string ToDbcFormat()
        {
            throw new NotImplementedException();
        }
    }
}
