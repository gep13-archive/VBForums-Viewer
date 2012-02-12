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

namespace Gep13.WindowsMobile.VbfViewer.Client.ViewModels
{
    using System;
    using System.Windows;
    using Caliburn.Micro;
    using Gep13.WindowsMobile.VbfViewer.Client.Workers;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;

    /// <summary>
    /// The base class for all screens of the application
    /// </summary>
    public class ScreenPageViewModelBase : Screen
    {
        /// <summary>
        /// Local instance of the shared ViewModelWorker class
        /// </summary>
        private ViewModelWorker viewModelWorker;

        /// <summary>
        /// Initializes a new instance of the ScreenPageViewModelBase class
        /// </summary>
        /// <param name="viewModelWorker">Incoming ViewModelWorker provided by Caliburn.Micro</param>
        public ScreenPageViewModelBase(ViewModelWorker viewModelWorker)
        {
            this.viewModelWorker = viewModelWorker;
        }

        /// <summary>
        /// Gets a utility class to provide access to common workers to view models
        /// </summary>
        protected ViewModelWorker VMWorker
        {
            get { return this.viewModelWorker; }
        }

        /// <summary>
        /// Helper Method to navigate to the MainPage
        /// </summary>
        public void GoToMainPage()
        {
            this.VMWorker.NavigationService.UriFor<MainPageViewModel>().Navigate();
        }

        /// <summary>
        /// Helper Method to navigatr to the Settings Page
        /// </summary>
        public void GoToSettingsPage()
        {
            MessageBox.Show("Not quite there yet :)");
            ////VMWorker.NavigationService.UriFor<SettingsViewModel>().Navigate();
        }

        /// <summary>
        /// Helper Method to navigate to the ProfilePage
        /// </summary>
        public void GoToProfilePage()
        {
            this.VMWorker.NavigationService.UriFor<ProfileViewModel>().Navigate();
        }

        /// <summary>
        /// Helper method to navigate to the Edit Account Page
        /// </summary>
        public void GoToEditAccountPage()
        {
            this.VMWorker.NavigationService.UriFor<AddAccountViewModel>()
                .WithParam(x => x.IsEditMode, true)
                .Navigate();
        }

        /// <summary>
        /// Helper method to navigate to the Add Account Page
        /// </summary>
        public void GoToAddAccountPage()
        {
            this.VMWorker.NavigationService.UriFor<AddAccountViewModel>()
                .WithParam(x => x.IsEditMode, false)
                .Navigate();
        }

        /// <summary>
        /// Initialises the application bar based on the view 
        /// </summary>
        /// <param name="view">The incoming View</param>
        /// <remarks>
        /// We can't databind so we have to do this programmatically
        /// </remarks>
        public void InitialiseAppBar(object view)
        {
            // sets up the app bar, use CM Buttons so we can have Action Messages included
            var pageView = view as PhoneApplicationPage;
            if (pageView != null && pageView.ApplicationBar == null)
            {
                if (this is ProfileViewModel)
                {
                    var appBar = new ApplicationBar();
                    var button1 = new AppBarButton() { Message = "GoToSettingsPage", Text = "settings", IconUri = new Uri("/Resources/Iconography/appbar.feature.settings.rest.png", UriKind.Relative) };
                    appBar.Buttons.Add(button1);
                    var menuitem1 = new AppBarMenuItem() { Message = "GoToEditAccountPage", Text = "update account" };
                    appBar.MenuItems.Add(menuitem1);
                    pageView.ApplicationBar = appBar;
                }
                else if (this is AddAccountViewModel)
                {
                    var appBar = new ApplicationBar();
                    var button1 = new AppBarButton() { Message = "GoToProfilePage", Text = "save", IconUri = new Uri("/Resources/Iconography/appbar.save.rest.png", UriKind.Relative) };
                    appBar.Buttons.Add(button1);
                    pageView.ApplicationBar = appBar;
                }
            }
        }

        /// <summary>
        /// Fired after the view gets attached to the view model
        /// </summary>
        /// <param name="view">The incoming View</param>
        /// <param name="context">The incoming Context</param>
        protected override void OnViewAttached(object view, object context)
        {
            base.OnViewAttached(view, context);

            // ensure the app bar is initialised
            this.InitialiseAppBar(view);
        }
    }
}