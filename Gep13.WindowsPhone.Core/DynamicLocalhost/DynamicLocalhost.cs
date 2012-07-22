//-----------------------------------------------------------------------
// <copyright file="DynamicLocalhost.cs" company="GEP13">
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

namespace Gep13.WindowsPhone.Core.DynamicLocalhost
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Windows;

    /// <summary>
    /// The dynamic localhost.
    /// </summary>
    public class DynamicLocalhost
    {
        /// <summary>
        /// Get the hostname of the current development machine
        /// </summary>
        /// <returns>ASystem.String with the name of the current development machine.</returns>
        /// <exception cref="FileNotFoundException">Thrown if file not found.</exception>
        public static string GetLocalHostname()
        {
            var asm = new AssemblyName(Assembly.GetExecutingAssembly().FullName);
            var file = Application.GetResourceStream(new Uri(string.Format("/{0};component/DynamicLocalhost\\LocalHostname.txt", asm.Name), UriKind.Relative));
            if (file == null)
            {
                throw new FileNotFoundException("Unable to locate the DynamicLocalhost\\LocalHostname.txt file in the XAP. Make sure it exists and the Build Action is Resource");
            }

            using (var sr = new StreamReader(file.Stream))
            {
                return sr.ReadLine();
            }
        }

        /// <summary>
        /// Replace the string "localhost" in the URL with the actual hostname of the development machine
        /// </summary>
        /// <param name="url">The url.</param>
        /// <returns>A System.String with the token value replaced.</returns>
        public static string ReplaceLocalhost(string url)
        {
            var hostname = GetLocalHostname();
            return string.IsNullOrEmpty(hostname) ? url : Regex.Replace(url, "localhost", hostname, RegexOptions.IgnoreCase);
        }
    }
}