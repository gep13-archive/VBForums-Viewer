//-----------------------------------------------------------------------
// <copyright file="VBForumsMetroSterlingService.cs" company="GEP13">
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

namespace Gep13.WindowsPhone.VBForumsMetro.Core.Database
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using Gep13.WindowsPhone.Core.Database;
    using Gep13.WindowsPhone.VBForumsMetro.Database;
    using Wintellect.Sterling;
    using Wintellect.Sterling.IsolatedStorage;

    /// <summary>
    /// A concrete implementation of the ISterlingService specifically for the VBForumsMetro application
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "This is handled in the Deactivate method")]
    public class VBForumsMetroSterlingService : ISterlingService
    {
        /// <summary>
        /// Maintain an internal reference to the Sterling internal engine
        /// </summary>
        private SterlingEngine sterlingEngine;

        /// <summary>
        /// Maintain a refernce to the default logger for Sterling
        /// </summary>
        private SterlingDefaultLogger sterlingDefaultLogger;

        /// <summary>
        /// Gets the Database for the VBForumsMetro application
        /// </summary>
        public ISterlingDatabaseInstance Database { get; private set; }

        /// <summary>
        /// Activate the database instance
        /// </summary>
        public void Activate()
        {
            if (DesignerProperties.IsInDesignTool)
            {
                return;
            }

            if (Debugger.IsAttached)
            {
                this.sterlingDefaultLogger = new SterlingDefaultLogger(SterlingLogLevel.Verbose);
            }

            this.sterlingEngine = new SterlingEngine();

            // custom serializer for types
            this.sterlingEngine.SterlingDatabase.RegisterSerializer<TypeSerializer>();

            // change this for more verbose messages
            this.sterlingDefaultLogger = new SterlingDefaultLogger(SterlingLogLevel.Information);

            this.sterlingEngine.Activate();

            this.Database = this.sterlingEngine.SterlingDatabase.RegisterDatabase<VBForumsMetroDatabase>(new IsolatedStorageDriver());

            // see if we need to load it
            VBForumsMetroDatabase.CheckAndCreate(this.Database);
        }

        /// <summary>
        /// Deactivate the database instance
        /// </summary>
        public void Deactivate()
        {
            if (Debugger.IsAttached && this.sterlingDefaultLogger != null)
            {
                this.sterlingDefaultLogger.Detach();
            }

            this.sterlingEngine.Dispose();

            this.Database = null;
            this.sterlingEngine = null;
        }
    }
}