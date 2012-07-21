//-----------------------------------------------------------------------
// <copyright file="StorageService.cs" company="GEP13">
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

namespace Gep13.WindowsPhone.Core.Storage
{
    using System;
    using System.IO.IsolatedStorage;

    /// <summary>
    /// Concrete implemenation of the IStorageService to provide ability to access IsolatedStorage
    /// </summary>
    public class StorageService : IStorageService
    {
        /// <summary>
        /// A Reference to the Phones IsolatedStorageSettings.
        /// </summary>
        private static readonly IsolatedStorageSettings StorageSettings = IsolatedStorageSettings.ApplicationSettings;

        /// <summary>
        /// Adds an entity to the phones isolated
        /// storage using the key supplied for reference.
        /// If an entity with the same key exists it will
        /// be overriden.
        /// </summary>
        /// <typeparam name="TEntity">The type to be used.</typeparam>
        /// <param name="key">
        /// The key to refrence the entity by on get or remove
        /// </param>
        /// <param name="entity">
        /// The entity to store.
        /// </param>
        public void Add<TEntity>(string key, TEntity entity)
        {
            if (StorageSettings.Contains(key))
            {
                StorageSettings[key] = entity;
                return;
            }

            StorageSettings.Add(key, entity);
        }

        /// <summary>
        /// Gets the entity stored against
        /// the provided key if one exists.
        /// </summary>
        /// <typeparam name="TEntity">The type to be used.</typeparam>
        /// <param name="key">
        /// The key of the entity to get.
        /// </param>
        /// <returns>
        /// The entity if found otherwise new, 0 or false.
        /// </returns>
        public TEntity Get<TEntity>(string key)
        {
            if (StorageSettings.Contains(key))
            {
                return (TEntity)StorageSettings[key];
            }

            if (typeof(TEntity) == typeof(string))
            {
                return (TEntity)(object)string.Empty;
            }

            if (typeof(TEntity).IsValueType)
            {
                return default(TEntity);
            }

            return Activator.CreateInstance<TEntity>();
        }

        /// <summary>
        /// Removes the entity stored against
        /// the provided key if it exists. If
        /// not entity exists method will exit
        /// safely.
        /// </summary>
        /// <param name="key">
        /// The key of the entity to remove.
        /// </param>
        public void Remove(string key)
        {
            if (StorageSettings.Contains(key))
            {
                StorageSettings.Remove(key);
            }
        }
    }
}