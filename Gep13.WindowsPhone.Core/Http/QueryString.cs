//-----------------------------------------------------------------------
// <copyright file="QueryString.cs" company="GEP13">
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

namespace Gep13.WindowsPhone.Core.Http
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The query string.
    /// </summary>
    public class QueryString : IEnumerable<KeyValuePair<string, string>>
    {
        /// <summary>
        /// The key value pairs.
        /// </summary>
        private readonly List<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// Provide useful information about this object by overriding the ToString method
        /// </summary>
        /// <returns>The formatted string with lots of information.</returns>
        public override string ToString()
        {
            return "?" + string.Join("&", this.keyValuePairs.Select(param => string.Format("{0}={1}", param.Key, param.Value)).ToArray());
        }

        /// <summary>
        /// Add a new KeyValuePair to the collection
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(string key, string value)
        {
            this.keyValuePairs.Add(new KeyValuePair<string, string>(key, value));
        }

        /// <summary>
        /// Add many KeyValuePairs to the collection
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="items">The items.</param>
        public void AddMany(string key, IEnumerable<string> items)
        {
            foreach (var item in items)
            {
                this.Add(key, item);
            }
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>The System.Collections.IEnumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>The System.Collections.Generic.IEnumerator`1[T -&gt; System.Collections.Generic.KeyValuePair`2[TKey -&gt; System.String, TValue -&gt; System.String]].</returns>
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return this.keyValuePairs.GetEnumerator();
        }
    }
}
