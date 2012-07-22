//-----------------------------------------------------------------------
// <copyright file="IsolatedStorageHelper.cs" company="GEP13">
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

namespace Gep13.WindowsPhone.Core.Helpers
{
    using System.IO.IsolatedStorage;

    /// <summary>
    /// The isolated storage helper.
    /// </summary>
    public static class IsolatedStorageHelper
    {
        /// <summary>
        /// Safely get out a setting value, proving a default if it does not exist
        /// </summary>
        /// <param name="settings">The instance of IsolatedStorageSettings that is being inspected</param>
        /// <param name="name">The name of the object being requested</param>
        /// <param name="defaultValue">The default value.</param>
        /// <typeparam name="T">The type of the object that is being requested</typeparam>
        /// <returns>The requested object from Isolated Storage</returns>
        public static T GetSetting<T>(this IsolatedStorageSettings settings, string name, T defaultValue)
        {
            if (!settings.Contains(name))
            {
                return defaultValue;
            }

            return (T)settings[name];
        }
    }
}