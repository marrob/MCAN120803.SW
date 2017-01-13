// -----------------------------------------------------------------------
// <copyright file="CommentItem.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.Database.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CommentItem
    {
       public string Object { get; private set; }

       public string ObjectName { get; set; }

       public string Content { get; set; }

       public CommentItem(string objectName, string foreginName, string content)
       {
           Object = objectName;
           ObjectName = foreginName;
           Content = content;
       }
    }
}
