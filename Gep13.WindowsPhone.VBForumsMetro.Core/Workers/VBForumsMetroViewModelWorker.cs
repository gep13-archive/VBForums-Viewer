//-----------------------------------------------------------------------
// <copyright file="VBForumsMetroViewModelWorker.cs" company="GEP13">
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

namespace Gep13.WindowsPhone.VBForumsMetro.Core.Workers
{
    using Caliburn.Micro;

    using Gep13.WindowsPhone.Core.Database;
    using Gep13.WindowsPhone.Core.Diagnostics;
    using Gep13.WindowsPhone.Core.Navigation;
    using Gep13.WindowsPhone.Core.Progress;
    using Gep13.WindowsPhone.Core.Rating;
    using Gep13.WindowsPhone.Core.Storage;
    using Gep13.WindowsPhone.Core.Workers;
    using Gep13.WindowsPhone.VBForumsMetro.Core.Web;

    /// <summary>
    /// The vb forums metro view model worker.
    /// </summary>
    public class VBForumsMetroViewModelWorker : ViewModelWorker
    {
        /// <summary>
        /// Local instance of the Shared IVBForumsWebService
        /// </summary>
        private IVBForumsWebService vbforumsWebService;

        /// <summary>
        /// Local instance of the Shared ISterlingService
        /// </summary>
        private ISterlingService vbforumsSterlingService;

        /// <summary>
        /// Initializes a new instance of the VBForumsMetroViewModelWorker class
        /// </summary>
        /// <param name="vbforumsWebService">VBForumsWebService provided by Caliburn.Micro</param>
        /// <param name="vbforumsSterlingService">VBForumsMetroSterlingService provided by Caliburn.Micro</param>
        /// <param name="navigationService">NavigationService provided by Caliburn.Micro</param>
        /// <param name="progressService">ProgressService provided by Caliburn.Micro</param>
        /// <param name="eventAggregator">EventAggregator provided by Caliburn.Micro</param>
        /// <param name="storageService">StorageService provided by Caliburn.Micro</param>
        /// <param name="navigationHelperService">NavigationHelperService provided by Caliburn.Micro</param>
        /// <param name="ratingService">RatingService provided by Caliburn.Micro</param>
        /// <param name="diagnosticsService">DiagnosticsService provided by Caliburn.Micro</param>
        public VBForumsMetroViewModelWorker(IVBForumsWebService vbforumsWebService, ISterlingService vbforumsSterlingService, INavigationService navigationService, IProgressService progressService, IEventAggregator eventAggregator, IStorageService storageService, INavigationHelperService navigationHelperService, IRatingService ratingService, IDiagnosticsService diagnosticsService)
            : base(navigationService, progressService, eventAggregator, storageService, navigationHelperService, ratingService, diagnosticsService)
        {
            this.vbforumsWebService = vbforumsWebService;
            this.vbforumsSterlingService = vbforumsSterlingService;
        }

        /// <summary>
        /// Gets the shared VBForumsWebService
        /// </summary>
        public IVBForumsWebService VBForumsWebService
        {
            get { return this.vbforumsWebService; }
        }

        /// <summary>
        /// Gets the shared VBForumsWebService
        /// </summary>
        public ISterlingService VBForumsMetroSterlingService
        {
            get { return this.vbforumsSterlingService; }
        }
    }
}