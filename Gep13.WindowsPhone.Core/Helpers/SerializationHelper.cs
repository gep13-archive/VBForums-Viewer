//-----------------------------------------------------------------------
// <copyright file="SerializationHelper.cs" company="GEP13">
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

namespace Gep13.WindowsPhone.Core.Helpers
{
    using System.IO;
    using System.Runtime.Serialization.Json;

    /// <summary>
    /// The serialization helper.
    /// </summary>
    public static class SerializationHelper
    {
        ////// TODO: Convert back to JSON.NET when upgraded to CM 1.3 - to fix a method access exception
        ////public static T Deserialize<T>(string serialized)
        ////{
        ////    if (string.IsNullOrEmpty(serialized))
        ////        return default(T);

        ////    //return JsonConvert.DeserializeObject<T>(serialized);
        ////}

        ////public static string Serialize<T>(T obj)
        ////{
        ////    return JsonConvert.SerializeObject(obj);
        ////}

        /// <summary>
        /// The serialize method
        /// </summary>
        /// <param name="obj">The object to be serialized.</param>
        /// <typeparam name="T">The type of object that requires serialized.</typeparam>
        /// <returns>The serialized System.String.</returns>
        public static string Serialize<T>(T obj)
        {
            using (var stream = new MemoryStream())
            using (var sr = new StreamReader(stream))
            {
                var ser = new DataContractJsonSerializer(typeof(T));
                ser.WriteObject(stream, obj);
                sr.BaseStream.Position = 0;
                var s = sr.ReadToEnd();
                return s;
            }
        }

        /// <summary>
        /// The deserialize method.
        /// </summary>
        /// <param name="json">The json string which is to be deserialized.</param>
        /// <typeparam name="T">The type of th eobject that he JSON should be converted to.</typeparam>
        /// <returns>An instance of the requested type, populated from the JSON string.</returns>
        public static T Deserialize<T>(string json)
        {
            using (var stream = new MemoryStream())
            using (var sr = new StreamWriter(stream))
            {
                sr.Write(json);
                sr.Flush();
                sr.BaseStream.Position = 0;

                var ser = new DataContractJsonSerializer(typeof(T));
                return (T)ser.ReadObject(stream);
            }
        }
    }
}
