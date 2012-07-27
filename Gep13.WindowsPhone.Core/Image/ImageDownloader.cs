//-----------------------------------------------------------------------
// <copyright file="ImageDownloader.cs" company="GEP13">
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

namespace Gep13.WindowsPhone.Core.Image
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.Windows;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// The image downloader.
    /// </summary>
    public class ImageDownloader
    {
        /// <summary>
        /// The expiration days.
        /// </summary>
        private static double expirationDays = 1.0;

        /// <summary>
        /// Gets or sets the expiration days.
        /// </summary>
        public static double ExpirationDays
        {
            get
            {
                return expirationDays;
            }

            set
            {
                expirationDays = value;
            }
        }

        /// <summary>
        /// The download image.
        /// </summary>
        /// <param name="imageUrl">The image url.</param>
        /// <param name="image">The image.</param>
        /// <param name="imageCacheItem">The image cache item.</param>
        public static void DownloadImage(string imageUrl, BitmapImage image, ImageCacheItem imageCacheItem)
        {
            var filename = CreateUniqueFilename(imageUrl);
            imageCacheItem.LocalFilename = filename;

            var asyncDataTransfer = new AsyncDataTransfer
            {
                ImageCacheItem = imageCacheItem,
                Image = image
            };

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(imageUrl);
            if (imageCacheItem.ImageId != null)
            {
                httpWebRequest.Headers["If-None-Match"] = imageCacheItem.ImageId;
            }

            asyncDataTransfer.Request = httpWebRequest;

            httpWebRequest.BeginGetResponse(RequestCallback, asyncDataTransfer);
        }

        /// <summary>
        /// The request callback.
        /// </summary>
        /// <param name="asyncResult">The async result.</param>
        private static void RequestCallback(IAsyncResult asyncResult)
        {
            if (!asyncResult.IsCompleted)
            {
                return;
            }

            var transfer = (AsyncDataTransfer)asyncResult.AsyncState;

            try
            {
                var response = (HttpWebResponse)transfer.Request.EndGetResponse(asyncResult);

                var storageFile = IsolatedStorageFile.GetUserStoreForApplication();

                if (response.StatusCode == HttpStatusCode.NotModified)
                {
                    Deployment.Current.Dispatcher.BeginInvoke(
                        () =>
                        {
                            var fileName = transfer.ImageCacheItem.LocalFilename;
                            transfer.Image.SetSource(storageFile.OpenFile(fileName, FileMode.Open));
                        });

                    return;
                }

                transfer.ImageCacheItem.ImageId = response.Headers["ETag"];

                transfer.ImageCacheItem.Expiration = response.Headers["Expires"] != null ? DateTime.Parse(response.Headers["Expires"]) : DateTime.Now.AddDays(expirationDays);

                var responseStream = response.GetResponseStream();

                using (var writer = new BinaryWriter(storageFile.CreateFile(transfer.ImageCacheItem.LocalFilename)))
                {
                    var b = new byte[4096];
                    int read;

                    while ((read = responseStream.Read(b, 0, b.Length)) > 0)
                    {
                        writer.Write(b, 0, read);
                    }

                    writer.Flush();
                }

                Deployment.Current.Dispatcher.BeginInvoke(
                    () =>
                    {
                        var fileName = transfer.ImageCacheItem.LocalFilename;
                        transfer.Image.SetSource(storageFile.OpenFile(fileName, FileMode.Open));
                    });

                ImageCache.AddCacheItem(transfer.Request.RequestUri.ToString(), transfer.ImageCacheItem);

                ImageCache.Save();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to cache image. " + ex);
            }
        }

        /// <summary>
        /// The create unique filename.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <returns>The System.String.</returns>
        private static string CreateUniqueFilename(string url)
        {
            var extension = Path.GetExtension(url);
            var textToHash = Encoding.UTF8.GetBytes(url);
            var sha1Managed = new SHA1Managed();
            var hash = sha1Managed.ComputeHash(textToHash);
            return BitConverter.ToString(hash) + extension;
        }
    }
}