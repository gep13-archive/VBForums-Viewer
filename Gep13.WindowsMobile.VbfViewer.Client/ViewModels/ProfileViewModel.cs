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

namespace Gep13.WindowsMobile.VbfViewer.Client.ViewModels
{
    using System.Linq;
    using System.Windows;
    using Caliburn.Micro;
    using Gep13.WindowsMobile.VbfViewer.Core.Progress;
    using Microsoft.Phone.Controls;

    /// <summary>
    /// The ViewModel class for the Profile page
    /// </summary>
    public class ProfileViewModel
    {
        /// <summary>
        /// The NavigationService is an object built into
        /// Caliburn.Micro to enable ViewModel to ViewModel
        /// navigation. We are going to get this via IOC
        /// from the container we made in the bootstrapper. I
        /// think this is added to the container from register
        /// phone services. Either way we get it for free.
        /// </summary>
        private readonly INavigationService navigationService;

        /// <summary>
        /// Local instance of the ProgressService class to handle showing and hiding of the ProgressIndicator
        /// </summary>
        private readonly IProgressService progressService;

        /// <summary>
        /// Initializes a new instance of the ProfileViewModel class
        /// </summary>
        /// <param name="navigationService">The Navigation Interface used by the Application</param>
        /// <param name="progressService">The Progress Interface used by the Application</param>
        public ProfileViewModel(INavigationService navigationService, IProgressService progressService)
        {
            this.navigationService = navigationService;
            this.progressService = progressService;
            this.PurgeNavigationalBackStack();
        }

        /// <summary>
        /// Temporary method to prove the use of the ProgressIndicator
        /// </summary>
        public void ToggleProgressBar()
        {
            if (this.progressService.IsEnabled)
            {
                this.progressService.Hide();
            }
            else
            {
                this.progressService.Show();
            }
        }

        /// <summary>
        /// Allow the user to edit his/her account details
        /// </summary>
        public void NavigateToAddAccountView()
        {
            this.navigationService.UriFor<AddAccountViewModel>()
                .WithParam(x => x.IsEditMode, true)
                .Navigate();
        }

        /// <summary>
        /// Navigate to the Settings Page
        /// </summary>
        public void SaveAndNavigateToSettingsView()
        {
            MessageBox.Show("Not quite there yet :)");
        }

        /// <summary>
        /// Worker method to handle the correct use of the back button
        /// </summary>
        private void PurgeNavigationalBackStack()
        {
            var navEntry = (App.Current.RootVisual as PhoneApplicationFrame).BackStack.ElementAt(0);

            if (navEntry.Source.ToString().Contains("InitialView"))
            {
                (App.Current.RootVisual as PhoneApplicationFrame).RemoveBackEntry();
            }
        }
    }
}
