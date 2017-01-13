// -----------------------------------------------------------------------
// <copyright file="UnitTest_Class.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class UnitTest_Class
    {
        /// <summary>
        /// - Az abstract osztályt példányosítani nem lehet.
        /// - Tartalmazhat sima tulajodonságot.
        /// - Abstract metódusnak nem lehet implementációja.
        /// - Virtális metódusnak kell implementáció.
        /// </summary>
        public abstract class AbstaractClass{
            public string Name { get; set; }
            public int FirstValue { get; set; }
            public int SecondValue { get; set; }
            public abstract int Operation();
            public virtual int VirtalSumMethod() {
                return FirstValue + SecondValue;
            }
        }

        /// <summary>
        /// -Abstract metódust csak override kell implementálni.
        /// </summary>
        public class First : AbstaractClass
        {
            public override int Operation()
            {
                return FirstValue + SecondValue;
            }
        }

        public class Second : First
        {
            public override int Operation()
            {
                return FirstValue + SecondValue;
            }
        }





        [Test]
        public void _0001()
        {

        }


    }
}
