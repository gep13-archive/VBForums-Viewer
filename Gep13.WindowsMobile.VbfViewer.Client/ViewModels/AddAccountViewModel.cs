//-----------------------------------------------------------------------
// <copyright file="AddAccountViewModel.cs" company="GEP13">
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
    using Caliburn.Micro;
    using Gep13.WindowsMobile.VbfViewer.Core.Containers;
    using Microsoft.Phone.Controls;
    using WinPhoneKit.Storage;

    /// <summary>
    /// The ViewModel class for the AddAccount page
    /// </summary>
    public class AddAccountViewModel : PropertyChangedBase
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
        /// Local instance of the Account class which is persisted into Isolated Storage
        /// </summary>
        private Account account;

        /// <summary>
        /// Initializes a new instance of the AddAccountViewModel class
        /// </summary>
        /// <param name="navigationService">The Navigation Interface used by the Application</param>
        public AddAccountViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.PurgeNavigationalBackStack();
            this.RetriveAccountFromStorage();

#if DEBUG
            this.account.Username = "gep13";
            this.account.Password = "testtest";
#endif
        }

        /// <summary>
        /// Gets or sets a value indicating whether an account is being added or edited.
        /// </summary>
        public bool IsEditMode { get; set; }

        /// <summary>
        /// Gets or sets the username of the person logging into VBForums
        /// </summary>
        public string Username
        {
            get
            {
                return this.account.Username;
            }

            set
            {
                this.account.Username = value;
                NotifyOfPropertyChange(() => this.Username);
                NotifyOfPropertyChange(() => this.CanSaveAndNavigateToProfileView);
            }
        }

        /// <summary>
        /// Gets or sets the password for the person logging into VBForums
        /// </summary>
        public string Password
        {
            get
            {
                return this.account.Password;
            }

            set
            {
                this.account.Password = value;
                NotifyOfPropertyChange(() => this.Password);
                NotifyOfPropertyChange(() => this.CanSaveAndNavigateToProfileView);
            }
        }

        /// <summary>
        /// Gets the title for the page
        /// </summary>
        public string PageTitle
        {
            get
            {
                return this.IsEditMode ? "edit account" : "add account";
            }
        }

        /// <summary>
        /// Gets a value indicating whether all details have been entered. Used by Caliburn.Micro
        /// </summary>
        public bool CanSaveAndNavigateToProfileView
        {
            get
            {
                if (string.IsNullOrEmpty(this.Username) == true ||
                    string.IsNullOrEmpty(this.Password))
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Store the account details and move to the ProfileView
        /// </summary>
        public void SaveAndNavigateToProfileView()
        {
            this.CommitAccountToStorage();
            this.SetFirstRunFlag();
            this.navigationService.UriFor<ProfileViewModel>().Navigate();
        }

        /// <summary>
        /// Worker method to go and grab the Account details from Isolated Storage
        /// </summary>
        private void RetriveAccountFromStorage()
        {
            this.account = IsolatedStorage.Get<Account>("UserAccount");
        }

        /// <summary>
        /// Worker method to save the Account details to Isolated Storage
        /// </summary>
        private void CommitAccountToStorage()
        {
            IsolatedStorage.Add("UserAccount", this.account);
        }

        /// <summary>
        /// Worker method to set the FirstRunFlag to indicate that the user have set the account details
        /// </summary>
        private void SetFirstRunFlag()
        {
            IsolatedStorage.Add("FirstRunFlag", true);
        }

        /// <summary>
        /// In order to adhere to Marketplace rules, clear the back stack so that application will exit correctly
        /// </summary>
        private void PurgeNavigationalBackStack()
        {
            var navEntry = (App.Current.RootVisual as PhoneApplicationFrame).BackStack.ElementAt(0);

            if (navEntry.Source.ToString().Contains("WelcomeView"))
            {
                (App.Current.RootVisual as PhoneApplicationFrame).RemoveBackEntry();
            }
        }
    }
}