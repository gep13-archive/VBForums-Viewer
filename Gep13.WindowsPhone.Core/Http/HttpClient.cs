//-----------------------------------------------------------------------
// <copyright file="HttpClient.cs" company="GEP13">
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
    using System.Diagnostics;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Threading;
    using System.Windows;

    using Gep13.WindowsPhone.Core.Helpers;

    using SharpGIS;

    /// <summary>
    /// The http client.
    /// </summary>
    public static class HttpClient
    {
        /// <summary>
        /// The timeout period.
        /// </summary>
        /// <remarks>Default value of 30 seconds</remarks>
        private static TimeSpan timeout = TimeSpan.FromSeconds(30);

        /// <summary>
        /// The begin request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="callback">The callback.</param>
        /// <typeparam name="T">The type that the request is being made for</typeparam>
        public static void BeginRequest<T>(HttpRequest<T> request, Action<HttpResponse<T>> callback)
        {
            BeginRequest(request.Url, callback);
        }

        /// <summary>
        /// The begin request.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <param name="callback">The callback.</param>
        /// <typeparam name="T">The type that the request is being made for</typeparam>
        public static void BeginRequest<T>(string url, Action<HttpResponse<T>> callback)
        {
            var client = new GZipWebClient();

            var timer = new Timer(state => client.CancelAsync(), null, timeout, TimeSpan.FromMilliseconds(-1));

            Debug.WriteLine("HTTP Request: {0}", url);
            client.DownloadStringCompleted += (s, e) => ProcessResponse(callback, e);
            client.DownloadStringAsync(new Uri(url, UriKind.Absolute), timer);
        }

        /// <summary>
        /// The process response.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <param name="e">The e.</param>
        /// <typeparam name="T">The type that the response is being made for</typeparam>
        /// <exception cref="WebException">Occurs when error is recieved.</exception>
        private static void ProcessResponse<T>(Action<HttpResponse<T>> callback, DownloadStringCompletedEventArgs e)
        {
            var timer = (Timer)e.UserState;
            if (timer != null)
            {
                timer.Dispose();
            }

            try
            {
                if (e.Error != null)
                {
                    throw new WebException("Error getting the web service data", e.Error);
                }

                var json = e.Result.Replace("&amp;", "&");
                Debug.WriteLine("HTTP Response: {0}\r\n", json);
                var model = SerializationHelper.Deserialize<T>(json);

                Deployment.Current.Dispatcher.BeginInvoke(() => callback(new HttpResponse<T>(model)));
            }
            catch (SerializationException ex)
            {
                var httpException = new HttpException("Unable to deserialize the model", ex);
                Debug.WriteLine(ex);
                Deployment.Current.Dispatcher.BeginInvoke(() => callback(new HttpResponse<T>(httpException)));
            }
            catch (WebException ex)
            {
                var httpException = new HttpException(ex);
                Debug.WriteLine(ex);
                Deployment.Current.Dispatcher.BeginInvoke(() => callback(new HttpResponse<T>(httpException)));
            }
        }
    }
}