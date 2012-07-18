//-----------------------------------------------------------------------
// <copyright file="BooleanToVisibilityConverter.cs" company="GEP13">
//      Copyright (c) GEP13, 2012. All rights reserved.
//      Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation 
//      files (the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy, 
//      modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software 
//      is furnished to do so, subject to the following conditions:
//
//      The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
//      THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES 
//      OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE 
//      LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN 
//      CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
//-----------------------------------------------------------------------

namespace Gep13.WindowsPhone.Core.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// A common Converter to allow the conversion of a boolean value to a visbility property. Also allows the inversion of the value.
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Gets or sets a value indicating whether to invert the conversion
        /// </summary>
        public bool InvertValue { get; set; }

        /// <summary>
        /// Provides the ability to convert a boolean value to a visibility property
        /// </summary>
        /// <param name="value">The input value which is to be converted.</param>
        /// <param name="targetType">The type to which the input value should be converted.</param>
        /// <param name="parameter">I don't know what this is?!?</param>
        /// <param name="culture">The culture of the application requesting the conversion.</param>
        /// <returns>A value which can be bound to the Visibility property on an object.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool booleanValue = (bool)value;

            if (this.InvertValue)
            {
                booleanValue = !booleanValue;
            }

            return booleanValue ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Provides the ability to convert from a visibility property to a boolean
        /// </summary>
        /// <param name="value">The input value which is to be converted.</param>
        /// <param name="targetType">The type to which the input value should be converted.</param>
        /// <param name="parameter">I don't know what this is?!?</param>
        /// <param name="culture">The culture of the application requesting the conversion.</param>
        /// <returns>A boolean value which is the result of the reverse conversion</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool booleanValue = ((Visibility)value) == Visibility.Visible;

            return this.InvertValue ? !booleanValue : booleanValue;
        }
    }
}