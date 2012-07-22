//-----------------------------------------------------------------------
// <copyright file="HttpException.cs" company="GEP13">
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
    using System;
    using System.Collections.Generic;
    using System.Net;

    /// <summary>
    /// The http exception.
    /// </summary>
    public class HttpException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException"/> class.
        /// </summary>
        public HttpException()
        {
            this.Errors = new List<string>
                         {
                             "We had a problem connecting to the web service, please try again in a few moments."
                         };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public HttpException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner exception.</param>
        public HttpException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException"/> class.
        /// </summary>
        /// <param name="ex">The exception</param>
        public HttpException(WebException ex)
            : this()
        {
            ////// TODO: Get friendly errors
            ////if (ex.Status == WebExceptionStatus.UnknownError)
            ////{
            ////    using (var reader = new StreamReader(ex.Response.GetResponseStream()))
            ////    {
            ////        var json = reader.ReadToEnd();
            ////        var friendlyErrors = SerializationHelper.Deserialize<List<string>>(json);
            ////        if(friendlyErrors != null)
            ////            Errors.RepopulateList(friendlyErrors);
            ////    }
            ////}
        }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        public List<string> Errors { get; set; }
    }
}
