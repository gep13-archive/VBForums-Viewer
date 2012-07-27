//-----------------------------------------------------------------------
// <copyright file="ImageCache.cs" company="GEP13">
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
    using System.Collections.Generic;
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Runtime.Serialization;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// The image cache.
    /// </summary>
    public class ImageCache
    {
        /// <summary>
        /// The image cache lock.
        /// </summary>
        private static readonly object ImageCacheLock = new object();

        /// <summary>
        /// The image cache file.
        /// </summary>
        private static string imageCacheFile = "ImageCache.data";

        /// <summary>
        /// The image cache dictionary.
        /// </summary>
        private static Dictionary<string, ImageCacheItem> imageCacheDictionary;

        /// <summary>
        /// Gets or sets the image cache file.
        /// </summary>
        public static string ImageCacheFile
        {
            get
            {
                return imageCacheFile;
            }

            set
            {
                imageCacheFile = value;
            }
        }

        /// <summary>
        /// The get image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>The System.Windows.Media.Imaging.BitmapImage.</returns>
        public static BitmapImage GetImage(BitmapImage image)
        {
            var url = image.UriSource.ToString();

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                image.UriSource = new Uri(url);
                return image;
            }

            if (imageCacheDictionary == null)
            {
                lock (ImageCacheLock)
                {
                    if (imageCacheDictionary == null)
                    {
                        LoadCachedImageInfo();
                    }
                }
            }

            ImageCacheItem cacheItem;
            if (imageCacheDictionary != null && imageCacheDictionary.TryGetValue(url, out cacheItem))
            {
                if (DateTime.Compare(DateTime.Now, cacheItem.Expiration) >= 0)
                {
                    /* Item has expired. */
                    ImageDownloader.DownloadImage(url, image, cacheItem);
                }
                else
                {
                    string imageFileName = cacheItem.LocalFilename;
                    var storageFile = IsolatedStorageFile.GetUserStoreForApplication();
                    var imageFile = storageFile.OpenFile(imageFileName, FileMode.Open);

                    image.SetSource(imageFile);
                }
            }
            else
            {
                var item = new ImageCacheItem();
                ImageDownloader.DownloadImage(url, image, item);
            }

            return image;
        }

        /// <summary>
        /// The load cached image info.
        /// </summary>
        public static void LoadCachedImageInfo()
        {
            var storageFile = IsolatedStorageFile.GetUserStoreForApplication();

            if (!storageFile.FileExists(imageCacheFile))
            {
                imageCacheDictionary = new Dictionary<string, ImageCacheItem>();
                return;
            }

            bool success = false;

            using (var fileStream = storageFile.OpenFile(imageCacheFile, FileMode.Open))
            {
                var serializer = new DataContractSerializer(typeof(Dictionary<string, ImageCacheItem>));

                try
                {
                    imageCacheDictionary = (Dictionary<string, ImageCacheItem>)serializer.ReadObject(fileStream);
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to read image cache file. " + ex);
                }
            }

            if (success)
            {
                return;
            }

            /* An error occured, so delete the existing cached item. */
            storageFile.DeleteFile(imageCacheFile);
            imageCacheDictionary = new Dictionary<string, ImageCacheItem>();
        }

        /// <summary>
        /// The add cache item.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <param name="imageCacheItem">The image cache item.</param>
        internal static void AddCacheItem(string url, ImageCacheItem imageCacheItem)
        {
            lock (ImageCacheLock)
            {
                imageCacheDictionary.Add(url, imageCacheItem);
            }
        }

        /// <summary>
        /// The save.
        /// </summary>
        internal static void Save()
        {
            var storageFile = IsolatedStorageFile.GetUserStoreForApplication();

            using (var fileStream = storageFile.CreateFile(imageCacheFile))
            {
                var serializer = new DataContractSerializer(typeof(Dictionary<string, ImageCacheItem>));
                serializer.WriteObject(fileStream, imageCacheDictionary);
                fileStream.Flush();
            }
        }
    }
}