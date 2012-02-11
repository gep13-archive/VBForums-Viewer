//-----------------------------------------------------------------------
// <copyright file="IoC.cs" company="GEP13">
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

namespace Gep13.WindowsMobile.VbfViewer.Core
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The main IoC Class
    /// </summary>
    public static partial class IoC
    {
        /// <summary>
        /// Dictionary to hold all the registrations
        /// </summary>
        private static readonly Dictionary<Type, Type> registration = new Dictionary<Type, Type>();

        /// <summary>
        /// Need to check with Paul what this actually is
        /// </summary>
        private static readonly Dictionary<Type, object> rot = new Dictionary<Type, object>();

        /// <summary>
        /// Array of empty Arguments
        /// </summary>
        private static readonly object[] emptyArguments = new object[0];

        /// <summary>
        /// Thread lock object
        /// </summary>
        private static readonly object syncLock = new object();

        /// <summary>
        /// Initializes static members of the IoC class
        /// </summary>
        static IoC()
        {
            RegisterAll();
        }

        /// <summary>
        /// Worker method to Resolve the Type for the container
        /// </summary>
        /// <param name="type">The Type which needs to be resolved</param>
        /// <returns>An object of the resolved type</returns>
        public static object Resolve(Type type)
        {
            lock (syncLock)
            {
                if (!rot.ContainsKey(type))
                {
                    if (!registration.ContainsKey(type))
                    {
                        throw new Exception("Type not registered.");
                    }

                    var resolveTo = registration[type] ?? type;
                    var constructorInfos = resolveTo.GetConstructors();

                    if (constructorInfos.Length > 1)
                    {
                        throw new Exception("Cannot resolve a type that has more than one constructor.");
                    }

                    var constructor = constructorInfos[0];
                    var parameterInfos = constructor.GetParameters();
                    
                    if (parameterInfos.Length == 0)
                    {
                        rot[type] = constructor.Invoke(emptyArguments);
                    }
                    else
                    {
                        var parameters = new object[parameterInfos.Length];
                    
                        foreach (var parameterInfo in parameterInfos)
                        {
                            parameters[parameterInfo.Position] = Resolve(parameterInfo.ParameterType);
                        }

                        rot[type] = constructor.Invoke(parameters);
                    }
                }

                return rot[type];
            }
        }

        /// <summary>
        /// Generic version of Resolve
        /// </summary>
        /// <typeparam name="T">The generic Entity</typeparam>
        /// <returns>The generic instance of the Resolved Type</returns>
        public static T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        /// <summary>
        /// Register a Type in the Container
        /// </summary>
        /// <typeparam name="I">The Interface derived from</typeparam>
        /// <typeparam name="C">The concrete implementation of the Interface</typeparam>
        public static void Register<I, C>() where C : class, I
        {
            lock (syncLock)
            {
                registration.Add(typeof(I), typeof(C));
            }
        }

        /// <summary>
        /// Register a Type in the Container
        /// </summary>
        /// <typeparam name="C">The concrete implementation of the Interface</typeparam>
        public static void Register<C>() where C : class
        {
            lock (syncLock)
            {
                registration.Add(typeof(C), null);
            }
        }

        /// <summary>
        /// Helper method to register all Types which the IoC Container
        /// </summary>
        static partial void RegisterAll();
    }
}