// -----------------------------------------------------------------------
// <copyright file="Class1.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class IFooTestClass
    {
        public interface IFooTest
        {
            ///<summary>
            /// Foo method
            /// <code>
            ///   IFooTest t = new FooTest();
            ///   t.Foo();
            /// </code>
            ///</summary>
            void Foo();

            ///<summary>
            /// Bar method
            ///</summary>
            void Bar();

            ///<summary>
            /// Situation normal
            ///</summary>
            void Snafu();
        }

        public class FooTest : IFooTest
        {
            /// <inheritdoc />
            public void Foo() { /*...*/ }
            /// <inheritdoc />
            public void Bar() { /*...*/ }
            /// <inheritdoc />
            public void Snafu() {/*...*/ }

            public FooTest()
            {
                Foo();

                IFooTest t = new FooTest();
                t.Foo();
            }
        }
    }
}
