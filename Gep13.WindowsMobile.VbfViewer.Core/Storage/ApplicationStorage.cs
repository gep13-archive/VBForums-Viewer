//-----------------------------------------------------------------------
// <copyright file="ApplicationStorage.cs" company="deanvmc">
//      Copyright (c) deanvmc, 2012. All rights reserved.
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

namespace Gep13.WindowsMobile.VbfViewer.Core.Storage
{
    using System;
    using System.IO.IsolatedStorage;

    /// <summary>
    /// The core class for dealing with storing items in Isolated Storage
    /// </summary>
    public class ApplicationStorage
    {
        /// <summary>
        /// Retrieves and object of Type T from Isolated storage using the
        /// objectId parameter for reference. Return Type matches the calling
        /// type.
        /// </summary>
        /// <typeparam name="T">The type to be used.</typeparam>
        /// <param name="objectId">The object's ID.</param>
        /// <returns>
        /// An Object of T, or the types default value on exception.
        /// </returns>
        public static T Retrive<T>(string objectId) where T : class, new()
        {
            try
            {
                T storedObject;
                var storage = IsolatedStorageSettings.ApplicationSettings;

                if (storage.TryGetValue<T>(objectId, out storedObject))
                {
                    return storedObject;
                }
            }
            catch (Exception)
            {
                // Publish to ExceptionService once it is built.
            }

            return new T();
        }

        /// <summary>
        /// Retrieves and object of Type T from Isolated storage using the
        /// objectId parameter for reference. Return Type matches the calling
        /// type.
        /// </summary>
        /// <typeparam name="T">The type to be used.</typeparam>
        /// <returns>
        /// An Object of T, or the types default value on exception.
        /// </returns>
        public static bool RetriveFirstRunFlag()
        {
            try
            {
                bool storedObject;
                var storage = IsolatedStorageSettings.ApplicationSettings;

                if (storage.TryGetValue("FirstRunFlag", out storedObject))
                {
                    return storedObject;
                }
                else
                {
                    Commit<bool>(false, "FirstRunFlag");
                    return true;
                }
            }
            catch (Exception)
            {
                // Publish to ExceptionService once it is built.
            }

            return true;
        }

        /// <summary>
        /// Commits an object of type T to Isolated storage using the
        /// objectId parameter for reference.
        /// </summary>
        /// <typeparam name="T">The type to be used.</typeparam>
        /// <param name="objectToStore">The object to be stored.</param>
        /// <param name="objectId">The object's ID</param>
        public static void Commit<T>(T objectToStore, string objectId)
        {
            try
            {
                var storage = IsolatedStorageSettings.ApplicationSettings;
                storage.Remove(objectId);
                storage.Add(objectId, objectToStore);
                storage.Save();
            }
            catch (Exception)
            {
                // Publish to ExceptionService once it is built.
            }
        }
    }
}