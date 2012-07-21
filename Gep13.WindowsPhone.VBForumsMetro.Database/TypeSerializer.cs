//-----------------------------------------------------------------------
// <copyright file="TypeSerializer.cs" company="GEP13">
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

namespace Gep13.WindowsPhone.VBForumsMetro.Database
{
    using System;
    using System.IO;
    using Wintellect.Sterling.Exceptions;
    using Wintellect.Sterling.Serialization;

    /// <summary>
    /// Serialier that handles types and their derivatives
    /// </summary>
    public class TypeSerializer : BaseSerializer
    {
        /// <summary>
        /// Return true if this serializer can handle the object, i.e. if it
        /// can be cast to type
        /// </summary>
        /// <param name="targetType">The target</param>
        /// <returns>True if it can be serialized</returns>
        public override bool CanSerialize(Type targetType)
        {
            return typeof(Type).IsAssignableFrom(targetType);
        }

        /// <summary>
        /// Serialize the object
        /// </summary>
        /// <param name="target">The target</param>
        /// <param name="writer">The writer</param>
        public override void Serialize(object target, BinaryWriter writer)
        {
            var type = target as Type;

            // this is important
            if (type == null)
            {
                throw new SterlingSerializerException(this, target.GetType());
            }

            // we need this 
            if (type.AssemblyQualifiedName == null)
            {
                throw new SterlingSerializerException(this, target.GetType());
            }

            writer.Write(type.AssemblyQualifiedName);
        }

        /// <summary>
        /// Deserialize the object
        /// </summary>
        /// <param name="type">The type of the object</param>
        /// <param name="reader">A reader to deserialize from</param>
        /// <returns>The deserialized object</returns>
        public override object Deserialize(Type type, BinaryReader reader)
        {
            return Type.GetType(reader.ReadString());
        }
    }
}