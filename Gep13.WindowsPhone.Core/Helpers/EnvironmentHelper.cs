//-----------------------------------------------------------------------
// <copyright file="EnvironmentHelper.cs" company="GEP13">
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
    using System.ComponentModel;
    using Microsoft.Devices;

    /// <summary>
    /// The environment helper.
    /// </summary>
    public static class EnvironmentHelper
    {
        /// <summary>
        /// The design time.
        /// </summary>
        private static bool? designTime;

        /// <summary>
        /// The using emulator.
        /// </summary>
        private static bool? usingEmulator;

        /// <summary>
        /// Gets a value indicating whether design time.
        /// </summary>
        public static bool DesignTime 
        {
            get
            {
                if (!designTime.HasValue)
                {
                    designTime = (bool)DesignerProperties.IsInDesignTool;
                }

                return designTime.Value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether using emulator.
        /// </summary>
        public static bool UsingEmulator
        {
            get
            {
                if (!usingEmulator.HasValue)
                {
                    usingEmulator = Environment.DeviceType == DeviceType.Emulator;
                }

                return usingEmulator.Value;
            }
        }
    }
}