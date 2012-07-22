//-----------------------------------------------------------------------
// <copyright file="HttpResponse.cs" company="GEP13">
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
    /// <summary>
    /// The http response.
    /// </summary>
    /// <typeparam name="T">The type that the response is being created for.</typeparam>
    public class HttpResponse<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponse{T}"/> class.
        /// </summary>
        /// <param name="ex">The exception.</param>
        public HttpResponse(HttpException ex)
        {
            this.Error = ex;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponse{T}"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        public HttpResponse(T response)
        {
            this.Model = response;
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        public T Model { get; private set; }

        /// <summary>
        /// Gets the error.
        /// </summary>
        public HttpException Error { get; private set; }

        /// <summary>
        /// Gets a value indicating whether has error.
        /// </summary>
        public bool HasError
        {
            get { return this.Error != null; }
        }
    }
}
