// -----------------------------------------------------------------------
// <copyright file="CommentsObject.cs" company="">
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
    /// 11 Comment Definitions
    /// Keyword: CM_
    /// </summary>
    public class CommentsObject : DbcObject<CommentsObject>
    {
        public string Network;
        public List<DbcMessageCommentItem> Messages;
        public List<DbcSignalCommentItem> Signals;
        public List<DbcNodeCommentItem> Nodes;


        /// <summary>
        /// Message Comment
        /// Keyword: CM_ BO_
        /// </summary>
        /// 
        public class DbcMessageCommentItem : DbcObject<DbcMessageCommentItem>
        {
            public int ArbitrationId;
            public string Comment;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="input">CM_ BO_ 2 "Single Line Comment";</param>
            /// <returns></returns>
            public DbcMessageCommentItem Parse(string input)
            {
                string remain = input.Substring("CM_ BO_".Length).Trim(' ');
                ArbitrationId = DbcDatabase.ParseStringToInt32(remain.Substring(0, remain.IndexOf('"')).Trim(' '));
                Comment = remain.Substring(remain.IndexOf('"')).Trim(new char[] { '"', ';' });
                return this;
            }


            public string ToDbcFormat()
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Signal Comment
        /// Keyword: CM_ SG_
        /// </summary>
        public class DbcSignalCommentItem : DbcObject<DbcSignalCommentItem>
        {
            public int ArbitrationId;
            public string Name;
            public string Comment;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="input">"CM_ SG_ 4 EM_State_Signal \"Jel megjegyzés\";"</param>
            /// <returns></returns>
            public DbcSignalCommentItem Parse(string input)
            {
                string[] array = input.Split(' ');
                ArbitrationId = DbcDatabase.ParseStringToInt32(array[2]);
                Name = array[3];
                Comment = input.Substring(input.IndexOf('"')).Trim(new char[] { ';', ' ', '"' });
                return this;
            }

            public string ToDbcFormat()
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Node Comment
        /// Keyword: CM_ BU_ 
        /// </summary>
        public class DbcNodeCommentItem : DbcObject<DbcNodeCommentItem>
        {
            public string Name;
            public string Comment;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="input">"CM_ BU_ Omron \"Node Comment\";"</param>
            /// <returns></returns>
            public DbcNodeCommentItem Parse(string input)
            {
                string[] array = input.Split(' ');
                Name = array[2];
                Comment = input.Substring(input.IndexOf('"')).Trim(new char[] { ';', ' ', '"' });
                return this;
            }


            public string ToDbcFormat()
            {
                throw new NotImplementedException();
            }
        }


        public CommentsObject()
        {
            Messages = new List<DbcMessageCommentItem>();
            Signals = new List<DbcSignalCommentItem>();
            Nodes = new List<DbcNodeCommentItem>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CommentsObject Parse(string input)
        {
            string remain = string.Empty;
            string[] array = input.Split(' ');

            switch (array[1])
            {
                case "BU_": /*Node*/
                    {
                        Nodes.Add(new DbcNodeCommentItem().Parse(input));
                        break;
                    }
                case "BO_": /*Messages*/
                    {
                        Messages.Add(new DbcMessageCommentItem().Parse(input));
                        break;
                    }
                case "SG_": /*Signals*/
                    {
                        Signals.Add(new DbcSignalCommentItem().Parse(input));
                        break;
                    }
                default: /*Networks*/
                    {
                        Network = input.Substring("CM_".Length).Trim(new char[] { ';', ' ', '"' });
                        break;
                    }
            }
            return this;
        }


        public string ToDbcFormat()
        {
            throw new NotImplementedException();
        }
    }
}
