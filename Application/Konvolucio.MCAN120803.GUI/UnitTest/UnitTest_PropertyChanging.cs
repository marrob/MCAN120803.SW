// -----------------------------------------------------------------------
// <copyright file="UnitTest_PropertyChanging.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UnitTest_PropertyChanging
    {
        private class PropertyChangingCancelEventArgs : PropertyChangingEventArgs
        {
            public bool Cancel { get; set; }

            public PropertyChangingCancelEventArgs(string propertyName)
                : base(propertyName)
            {
            }
        }

        private class PropertyChangingCancelEventArgs<T> : PropertyChangingCancelEventArgs
        {
            public T OriginalValue { get; private set; }

            public T NewValue { get; private set; }

            public PropertyChangingCancelEventArgs(string propertyName, T originalValue, T newValue)
                : base(propertyName)
            {
                this.OriginalValue = originalValue;
                this.NewValue = newValue;
            }
        }

        private class PropertyChangedEventArgs<T> : PropertyChangedEventArgs
        {
            public T PreviousValue { get; private set; }

            public T CurrentValue { get; private set; }

            public PropertyChangedEventArgs(string propertyName, T previousValue, T currentValue)
                : base(propertyName)
            {
                this.PreviousValue = previousValue;
                this.CurrentValue = currentValue;
            }
        }

        private class Example : INotifyPropertyChanging, INotifyPropertyChanged
        {
            public event PropertyChangingEventHandler PropertyChanging;

            public event PropertyChangedEventHandler PropertyChanged;

            protected bool OnPropertyChanging<T>(string propertyName, T originalValue, T newValue)
            {
                var handler = this.PropertyChanging;
                if (handler != null)
                {
                    var args = new PropertyChangingCancelEventArgs<T>(propertyName, originalValue, newValue);
                    handler(this, args);
                    return !args.Cancel;
                }
                return true;
            }

            protected void OnPropertyChanged<T>(string propertyName, T previousValue, T currentValue)
            {
                var handler = this.PropertyChanged;
                if (handler != null)
                    handler(this, new PropertyChangedEventArgs<T>(propertyName, previousValue, currentValue));
            }

            private int _ExampleValue;

            public int ExampleValue
            {
                get { return _ExampleValue; }
                set
                {
                    if (_ExampleValue != value)
                    {
                        if (this.OnPropertyChanging("ExampleValue", _ExampleValue, value))
                        {
                            var previousValue = _ExampleValue;
                            _ExampleValue = value;
                            this.OnPropertyChanged("ExampleValue", previousValue, value);
                        }
                    }
                }
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                var exampleObject = new Example();

                exampleObject.PropertyChanging += new PropertyChangingEventHandler(exampleObject_PropertyChanging);
                exampleObject.PropertyChanged += new PropertyChangedEventHandler(exampleObject_PropertyChanged);

                exampleObject.ExampleValue = 123;
                exampleObject.ExampleValue = 100;
            }

            static void exampleObject_PropertyChanging(object sender, PropertyChangingEventArgs e)
            {
                if (e.PropertyName == "ExampleValue")
                {
                    int originalValue = ((PropertyChangingCancelEventArgs<int>)e).OriginalValue;
                    int newValue = ((PropertyChangingCancelEventArgs<int>)e).NewValue;

                    // do not allow the property to be changed if the new value is less than the original value
                    if (newValue < originalValue)
                        ((PropertyChangingCancelEventArgs)e).Cancel = true;
                }

            }

            static void exampleObject_PropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "ExampleValue")
                {
                    int previousValue = ((PropertyChangedEventArgs<int>)e).PreviousValue;
                    int currentValue = ((PropertyChangedEventArgs<int>)e).CurrentValue;
                }
            }
        }
    }
}
