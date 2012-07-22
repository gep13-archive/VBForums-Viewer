//-----------------------------------------------------------------------
// <copyright file="HttpService.cs" company="GEP13">
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

    using Gep13.WindowsPhone.Core.Models;

    /// <summary>
    /// The http service.
    /// </summary>
    public abstract class HttpService
    {
        /// <summary>
        /// Gets or sets the base url.
        /// </summary>
        protected string BaseUrl { get; set; }

        /// <summary>
        /// The create http request.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <param name="queryString">The query string.</param>
        /// <typeparam name="TResult">The type that the request is being made for</typeparam>
        /// <returns> The Gep13.WindowsPhone.Core.Http.HttpRequest`1[TModel -&gt; TResult].</returns>
        /// <exception cref="InvalidOperationException">If the base url is not specified.</exception>
        protected virtual HttpRequest<TResult> CreateHttpRequest<TResult>(string url, QueryString queryString)
        {
            if (string.IsNullOrEmpty(this.BaseUrl))
            {
                throw new InvalidOperationException("The BaseUrl has not been set for this service");
            }

            // IN EVERY OUTGOING WEB REQUEST:
            // Automatically send the current Client Version 
            // Whether the app is running in Trial Mode 
            // The utcOffset of the user's timezone
            queryString = queryString ?? new QueryString();
            queryString.Add("isTrial", ApplicationSettings.Current.IsTrial.ToString());
            queryString.Add("clientVersion", ApplicationSettings.Current.ClientVersion);
            queryString.Add("utcOffset", DateTimeOffset.Now.Offset.Hours.ToString());

            ////// This app doesn't use the Location service but this shows how it's possible
            ////// to automatically append myLat and myLong to every outgoing request.
            ////var myLocation = GlobalData.Current.MyLocation;
            ////if (!myLocation.IsUnknown)
            ////{
            ////    queryString.Add("myLat", myLocation.Latitude.ToString());
            ////    queryString.Add("myLong", myLocation.Longitude.ToString());
            ////}

            var fullUrl = this.BaseUrl + url + queryString;
            return new HttpRequest<TResult>(fullUrl);
        }
    }
}
