//-----------------------------------------------------------------------
// <copyright file="ProfileViewModel.cs" company="GEP13">
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

namespace Gep13.WindowsPhone.VBForumsMetro.Client.ViewModels
{
    using System;
    using Gep13.WindowsPhone.VBForumsMetro.Core.Workers;

    using Microsoft.Phone.Controls;

    /// <summary>
    /// The ViewModel class for the Profile page
    /// </summary>
    public class ProfileViewModel : VBForumsMetroScreenPageViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the ProfileViewModel class
        /// </summary>
        /// <param name="viewModelWorker">The View Model Worker from common access properties</param>
        public ProfileViewModel(VBForumsMetroViewModelWorker viewModelWorker)
            : base(viewModelWorker)
        {
        }

        /// <summary>
        /// An overridden implemenation of the OnViewLoaded method to do specific functionality within this view
        /// </summary>
        /// <param name="view">The current view</param>
        protected override void OnViewLoaded(object view)
        {
            this.VMWorker.RatingService.CheckWhetherUserWantsToRateApplication();
            this.VMWorker.NavigationHelperService.PurgeNavigationalBackStack(App.Current.RootVisual as PhoneApplicationFrame); 

            this.VMWorker.DiagnosticsService.ReportException(new Exception("test"), "this is something extra");
            this.VMWorker.DiagnosticsService.CheckForPreviousException();
            
            base.OnViewLoaded(view);
        }
    }
}
