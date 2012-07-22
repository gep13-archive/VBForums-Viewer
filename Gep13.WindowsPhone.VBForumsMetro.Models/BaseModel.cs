//-----------------------------------------------------------------------
// <copyright file="BaseModel.cs" company="GEP13">
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

namespace Gep13.WindowsPhone.VBForumsMetro.Models
{
    using Caliburn.Micro;

    /// <summary>
    /// The Base Class definition that all Models will derive from
    /// </summary>
    /// <typeparam name="T">The type that derives from base model</typeparam>
    public class BaseModel<T> : PropertyChangedBase, IBaseModel
        where T : BaseModel<T>
    {
        /// <summary>
        /// Gets or sets the Standard Key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Override the base Equals method and test for equality based on the objects Id property.
        /// </summary>
        /// <param name="obj">The object being tested for equality</param>
        /// <returns>True if the objects are equal, otherwise false.</returns>
        public override bool Equals(object obj)
        {
            return obj is T && ((T)obj).Id.Equals(this.Id);
        }

        /// <summary>
        /// Override the base GetHashCode method and return the Id for the current object
        /// </summary>
        /// <returns>The Id of the current object which will always be unique</returns>
        public override int GetHashCode()
        {
            return this.Id;
        }

        /// <summary>
        /// Override the base ToString method to return sensible information about the current object.
        /// </summary>
        /// <returns>A formatted string with information about the current object</returns>
        public override string ToString()
        {
            return string.Format("{0} with Id {1}", typeof(T).FullName, this.Id);
        }
    }
}