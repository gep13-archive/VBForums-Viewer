//-----------------------------------------------------------------------
// <copyright file="ImageCacheConverter.cs" company="GEP13">
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
    using System.IO.IsolatedStorage;
    using System.Windows.Data;
    using System.Windows.Media.Imaging;

    using Gep13.WindowsPhone.Core.Helpers;
    using Gep13.WindowsPhone.Core.Image;

    /// <summary>
    /// The image cache convertercs.
    /// </summary>
    public class ImageCacheConverter : IValueConverter
    {
        /// <summary>
        /// Provides the ability to convert a string version of a url to an image
        /// </summary>
        /// <param name="value">The input value which is to be converted.</param>
        /// <param name="targetType">The type to which the input value should be converted.</param>
        /// <param name="parameter">I don't know what this is?!?</param>
        /// <param name="culture">The culture of the application requesting the conversion.</param>
        /// <returns>A value which can be bound to the Source Property of an image</returns>
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (EnvironmentHelper.DesignTime)
            {
                return value;
            }

            var imageUri = value as Uri;
            if (imageUri != null)
            {
                try
                {
                    return ImageCache.GetImage(new BitmapImage(imageUri));
                }
                catch (IsolatedStorageException e)
                {
                    Console.WriteLine(e);
                    return value;
                }
            }
            
            var bitmapImage = value as BitmapImage;
            
            return bitmapImage != null ? ImageCache.GetImage(bitmapImage) : value;
        }

        /// <summary>
        /// Provides the ability to convert from a cached image to a string representation of a Url
        /// </summary>
        /// <param name="value">The input value which is to be converted.</param>
        /// <param name="targetType">The type to which the input value should be converted.</param>
        /// <param name="parameter">I don't know what this is?!?</param>
        /// <param name="culture">The culture of the application requesting the conversion.</param>
        /// <returns>A string represenation of a url</returns>
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}