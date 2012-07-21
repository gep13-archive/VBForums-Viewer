//-----------------------------------------------------------------------
// <copyright file="InitialViewModel.cs" company="GEP13">
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
    using Caliburn.Micro;
    using Gep13.WindowsPhone.Core.Workers;

    /// <summary>
    /// The ViewModel class for the Initial page
    /// </summary>
    public class InitialViewModel : VBForumsMetroScreenPageViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the InitialViewModel class
        /// </summary>
        /// <param name="viewModelWorker">The View Model Worker from common access properties</param>
        public InitialViewModel(ViewModelWorker viewModelWorker) : base(viewModelWorker) 
        { 
        }

        /// <summary>
        /// Bound to the loaded event of the view. Caliburn.Micro
        /// has a concept called Event Triggers that allow you
        /// to fire methods in response to events.
        /// </summary>
        public void DetermineNavigationPath()
        {
            switch (this.ObtainFirstRunFlag())
            {
                case false:
                    this.GoToWelcomePage();
                    break;

                default:
                    this.GoToProfilePage();
                    break;
            }
        }

        /// <summary>
        /// ApplicationStorage is a wrapper around Isolated Storage
        /// that allows us to get certain values in the context of
        /// the application. Here we are asking if the app has
        /// been ran before on this unit.
        /// </summary>
        /// <returns>A bool, indicating whether the app have been run before.</returns>
        private bool ObtainFirstRunFlag()
        {
            return this.VMWorker.StorageService.Get<bool>("firstrunflag");
        }
    }
}