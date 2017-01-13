// -----------------------------------------------------------------------
// <copyright file="NodeObject.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.VectorCANdb.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 6 Node Definitions
    /// Keyword: BU_
    /// </summary>
    public class NodeObject : DbcObject<NodeObject>
    {
        public List<string> Names;

        public NodeObject()
        {
            Names = new List<string>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input">
        /// BU_: New_Node_11New_Node_11 1New_Node_10New_Node_101 New_Node_8New_Node_8New_Node_8 New_Node_6_New_ DGT Omron PC"
        /// </param>
        /// <returns></returns>
        public NodeObject Parse(string input)
        {
            int offset = 0;
            string[] array = input.Split(' ');
            Names = new List<string>();
            if (array[offset++] == "BU_:")
                for (offset = 1; offset < array.Length; offset++)
                    Names.Add(array[offset]);
            return this;
        }


        public string ToDbcFormat()
        {
            throw new NotImplementedException();
        }
    }
}
