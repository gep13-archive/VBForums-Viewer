//-----------------------------------------------------------------------
// <copyright file="ViewModelWorker.cs" company="GEP13">
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

namespace Gep13.WindowsPhone.Core.Workers
{
    using Caliburn.Micro;
    using Gep13.WindowsPhone.Core.Navigation;
    using Gep13.WindowsPhone.Core.Progress;
    using Gep13.WindowsPhone.Core.Rating;
    using Gep13.WindowsPhone.Core.Storage;

    /// <summary>
    /// Injected into every view model, access to most required objects
    /// </summary>
    public class ViewModelWorker
    {
        /// <summary>
        /// Local instance of the Shared INavigationService
        /// </summary>
        private INavigationService navigationService;

        /// <summary>
        /// Local instance of the Shared IProgressService
        /// </summary>
        private IProgressService progressService;

        /// <summary>
        /// Local instance of the Shared IEventAggregator
        /// </summary>
        private IEventAggregator eventAggregator;

        /// <summary>
        /// Local instance of the Shared IStorageService
        /// </summary>
        private IStorageService storageService;

        /// <summary>
        /// Local instance of the Shared INavigationHelperService
        /// </summary>
        private INavigationHelperService navigationHelperService;

        /// <summary>
        /// Local instnace of the Shared IRatingService
        /// </summary>
        private IRatingService ratingService;

        /// <summary>
        /// Initializes a new instance of the ViewModelWorker class
        /// </summary>
        /// <param name="navigationService">NavigationService provided by Caliburn.Micro</param>
        /// <param name="progressService">ProgressService provided by Caliburn.Micro</param>
        /// <param name="eventAggregator">EventAggregator provided by Caliburn.Micro</param>
        /// <param name="storageService">StorageService provided by Caliburn.Micro</param>
        /// <param name="navigationHelperService">NavigationHelperService provided by Caliburn.Micro</param>
        /// <param name="ratingService">RatingService provided by Caliburn.Micro</param>
        public ViewModelWorker(INavigationService navigationService, IProgressService progressService, IEventAggregator eventAggregator, IStorageService storageService, INavigationHelperService navigationHelperService, IRatingService ratingService)
        {
            this.navigationService = navigationService;
            this.progressService = progressService;
            this.eventAggregator = eventAggregator;
            this.storageService = storageService;
            this.navigationHelperService = navigationHelperService;
            this.ratingService = ratingService;
        }

        /// <summary>
        /// Gets the shared NavigationService
        /// </summary>
        public INavigationService NavigationService
        {
            get { return this.navigationService; }
        }

        /// <summary>
        /// Gets the shared ProgressService
        /// </summary>
        public IProgressService ProgressService
        {
            get { return this.progressService; }
        }

        /// <summary>
        /// Gets the shared EventAggregator
        /// </summary>
        public IEventAggregator EventAggregator
        {
            get { return this.eventAggregator; }
        }

        /// <summary>
        /// Gets the shared StorageService
        /// </summary>
        public IStorageService StorageService
        {
            get { return this.storageService; }
        }

        /// <summary>
        /// Gets the shared NavigationHelperService
        /// </summary>
        public INavigationHelperService NavigationHelperService
        {
            get { return this.navigationHelperService; }
        }

        /// <summary>
        /// Gets the shard RatingService
        /// </summary>
        public IRatingService RatingService
        {
            get { return this.ratingService; }
        }
    }
}