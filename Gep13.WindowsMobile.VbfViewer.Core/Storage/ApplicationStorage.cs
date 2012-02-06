using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;

namespace Gep13.WindowsMobile.VbfViewer.Core.Storage
{
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
                //Publish to ExceptionService once it is built.
            }

            return new T();
        }

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
        public static bool RetriveFirstRunFlag()
        {
            try
            {
                bool storedObject;
                var storage = IsolatedStorageSettings.ApplicationSettings;

                if (storage.TryGetValue("FirstRunFlag", out storedObject))

                    return storedObject;

                else
                {
                    Commit<bool>(false, "FirstRunFlag");
                    return true;
                }

            }
            catch (Exception)
            {
                //Publish to ExceptionService once it is built.
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
                //Publish to ExceptionService once it is built.
            }
        }
    }
}