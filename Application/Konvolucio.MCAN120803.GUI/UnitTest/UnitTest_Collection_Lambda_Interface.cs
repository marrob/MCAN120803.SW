// -----------------------------------------------------------------------
// <copyright file="UnitTest_Collection_Lambda_Interface.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.UnitTest
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UnitTest_Collection_Lambda_Interface
    {
        interface IPersonItem
        {
            string First { get; }
            string Last { get; }
        }
        class PersonItem : IPersonItem
        {
            public string First { get; private set; }
            public string Last { get; private set; }
            public PersonItem(string first, string last)
            {
                First = first;
                Last = last;
            }
        }
        /*IEnumerable Csak jelzi hogy sorolható, de nem jelzi, hogy mi az... Ezt Cast-lássla lehet megoldani*/
        interface IPersonCollection : IEnumerable 
        {
            int Count { get; }
            IPersonItem this[int index] { get; set; }
            void Add(IPersonItem item);
        } 

        class PersonCollection : Collection<IPersonItem>, IPersonCollection
        {
        }

        [Test]
        public void _0001_CastItemToEnumerable()
        {
            IPersonCollection pc = new PersonCollection();
            pc.Add(new PersonItem("Simpson", "Homer"));
            pc.Add(new PersonItem("Simpson", "Bart"));
            pc.Cast<IPersonItem>().FirstOrDefault(n => n.First == "Simpson");
        }

        [Test]
        public void _0002_CastItemToEnumerable()
        {
            IPersonCollection pc = new PersonCollection();
            pc.Add(new PersonItem("Simpson", "Homer"));
            pc.Add(new PersonItem("Simpson", "Bart"));

            IEnumerable<IPersonItem> x;

            x = new List<IPersonItem>();

            foreach (var item in x)
            {
             
            }



            pc.Cast<IPersonItem>().FirstOrDefault(n => n.First == "Simpson");
        }
    }
}
