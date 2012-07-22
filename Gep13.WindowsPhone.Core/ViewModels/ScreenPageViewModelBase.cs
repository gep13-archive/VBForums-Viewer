//-----------------------------------------------------------------------
// <copyright file="ScreenPageViewModelBase.cs" company="GEP13">
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

namespace Gep13.WindowsPhone.Core.ViewModels
{
    using Caliburn.Micro;
    using Gep13.WindowsPhone.Core.Workers;

    /// <summary>
    /// The base class for all screens of the application
    /// </summary>
    public class ScreenPageViewModelBase : Screen
    {
        /// <summary>
        /// Local instance of the shared ViewModelWorker class
        /// </summary>
        private readonly ViewModelWorker viewModelWorker;

        /// <summary>
        /// Initializes a new instance of the ScreenPageViewModelBase class
        /// </summary>
        /// <param name="viewModelWorker">Incoming ViewModelWorker provided by Caliburn.Micro</param>
        public ScreenPageViewModelBase(ViewModelWorker viewModelWorker)
        {
            this.viewModelWorker = viewModelWorker;
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not to remove the source page from the back stack
        /// </summary>
        public bool BackNavSkipOne { get; set; }

        /// <summary>
        /// Gets a utility class to provide access to common workers to view models
        /// </summary>
        protected ViewModelWorker VMWorker
        {
            get { return this.viewModelWorker; }
        }
    }
}