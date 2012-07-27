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

namespace Gep13.WindowsPhone.VBForumsMetro.Client.ViewModels
{
    using System.Linq;

    using Gep13.WindowsPhone.VBForumsMetro.Core.Workers;
    using Gep13.WindowsPhone.VBForumsMetro.Models;

    /// <summary>
    /// The ViewModel class for the AddAccount page
    /// </summary>
    public class AddAccountViewModel : VBForumsMetroScreenPageViewModelBase
    {
        /// <summary>
        /// The member id.
        /// </summary>
        private int memberId;

        /// <summary>
        /// The user name.
        /// </summary>
        private string userName;

        /// <summary>
        /// The password.
        /// </summary>
        private string password;

        /// <summary>
        /// Local variable for whether the user is authenticated
        /// </summary>
        private bool isAuthenticated;

        /// <summary>
        /// Local variable for displaying the status of the user authentication to the user
        /// </summary>
        private string statusLabel;

        /// <summary>
        /// Initializes a new instance of the AddAccountViewModel class
        /// </summary>
        /// <param name="viewModelWorker">The View Model Worker from common access properties</param>
        public AddAccountViewModel(VBForumsMetroViewModelWorker viewModelWorker)
            : base(viewModelWorker)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether an account is being added or edited.
        /// </summary>
        public bool IsEditMode { get; set; }

        /// <summary>
        /// Gets or sets the member id.
        /// </summary>
        public int MemberId
        {
            get
            {
                return this.memberId;
            }

            set
            {
                this.memberId = value;
                this.NotifyOfPropertyChange(() => this.MemberId);
            }
        }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName
        {
            get
            {
                return this.userName;
            }

            set
            {
                this.userName = value;
                this.NotifyOfPropertyChange(() => this.UserName);
                this.NotifyOfPropertyChange(() => this.CanAuthenticateUser);
            }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                this.password = value;
                this.NotifyOfPropertyChange(() => this.Password);
                this.NotifyOfPropertyChange(() => this.CanAuthenticateUser);
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
        /// Gets or sets the contents of the StatusLabel displayed on the page
        /// </summary>
        public string StatusLabel
        {
            get
            {
                return this.statusLabel;
            }

            set
            {
                this.statusLabel = value;
                this.NotifyOfPropertyChange(() => this.StatusLabel);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the specified user credentials are authenticated.
        /// </summary>
        public bool IsUserAuthenticated
        {
            get
            {
                return this.isAuthenticated;
            }

            set
            {
                this.isAuthenticated = value;
                this.NotifyOfPropertyChange(() => this.IsUserAuthenticated);
                this.NotifyOfPropertyChange(() => this.CanPersistUserCredentials);
                this.NotifyOfPropertyChange(() => this.CanDeleteAccount);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current account can be deleted.
        /// </summary>
        public bool CanDeleteAccount
        {
            get { return this.IsUserAuthenticated || this.IsEditMode; }
        }

        /// <summary>
        /// Gets a value indicating whether it is possible to Authenticate a user
        /// </summary>
        public bool CanAuthenticateUser
        {
            get
            {
                return !string.IsNullOrEmpty(this.UserName) && !string.IsNullOrEmpty(this.Password);
            }
        }

        /// <summary>
        /// Gets a value indicating whether it is possible to navigate to the ProfilePage
        /// </summary>
        public bool CanPersistUserCredentials
        {
            get
            {
                if (this.IsUserAuthenticated)
                {
                    this.VMWorker.StorageService.Add("firstrunflag", true);
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// The persist user credentials.
        /// </summary>
        public void PersistUserCredentials()
        {
            if (this.IsEditMode)
            {
                this.UpdateUserCredentials();
            }
            else
            {
                this.SaveUserCredentials();
            }

            this.ResetViewModel();
            this.GoToMainPage();
        }

        /// <summary>
        /// Helper method to do the work when the ActionIcon is tapped on the Username TextBox
        /// </summary>
        public void UsernameActionIconTapped()
        {
            this.UserName = string.Empty;
        }

        /// <summary>
        /// Helper method to do the work when the ActionIcon is tapped on the Password Textbox
        /// </summary>
        public void PasswordActionIconTapped()
        {
            this.Password = string.Empty;
        }

        /// <summary>
        /// Helper method to populate some demo credentials
        /// </summary>
        public void PopulateDemoCredentials()
        {
            this.UserName = "gep31";
            this.Password = "qwerty";
        }

        /// <summary>
        /// A helper method to go off to the VBForums site and Authenticate the current user credentials
        /// </summary>
        public void AuthenticateUser()
        {
            this.VMWorker.ProgressService.Show();
            this.AuthenticateUserResponse();
        }

        /// <summary>
        /// A helper method to delete the current credentials
        /// </summary>
        public void DeleteAccount()
        {
            this.VMWorker.StorageService.Add("firstrunflag", false);
            this.ResetViewModel();
            this.DeleteUserCredentials(1);
        }

        /// <summary>
        /// An overridden implemenation of the OnViewAttahced method to do specific functionality within this view
        /// </summary>
        /// <param name="view">The current view</param>
        /// <param name="context">The incoming context</param>
        protected override void OnViewAttached(object view, object context)
        {
            this.StatusLabel = string.Empty;
            this.IsUserAuthenticated = false;

            base.OnViewAttached(view, context);
        }

        /// <summary>
        /// An overridden implemenation of the OnViewLoaded method to do specific functionality within this view
        /// </summary>
        /// <param name="view">The current view</param>
        protected override void OnViewLoaded(object view)
        {
            if (this.IsEditMode)
            {
                if (string.IsNullOrEmpty(this.UserName))
                {
                    this.UserName = this.VMWorker.StorageService.Get<string>("username");
                }

                if (string.IsNullOrEmpty(this.Password))
                {
                    this.Password = this.VMWorker.StorageService.Get<string>("password");
                }
            }

            base.OnViewLoaded(view);
        }

        /// <summary>
        /// The authenticate user response.
        /// </summary>
        private async void AuthenticateUserResponse()
        {
            var viewModelWorker = (VBForumsMetroViewModelWorker)this.VMWorker;
            var result =
                await
                viewModelWorker.VBForumsWebService.IsValidLoginCredential(
                    new LoginCredentialModel() { UserName = this.UserName, Password = this.Password });

            viewModelWorker.ProgressService.Hide();

            if (result)
            {
                this.StatusLabel = "user authenticated successfully";
                this.IsUserAuthenticated = true;
            }
            else
            {
                this.StatusLabel = "unable to authenticate user, please check username and password";
                this.IsUserAuthenticated = false;
            }
        }

        /// <summary>
        /// The reset view model.
        /// </summary>
        private void ResetViewModel()
        {
            // clear the values so that Tombstoning doesn't kick in
            this.UserName = string.Empty;
            this.Password = string.Empty;

            this.IsEditMode = false;
            this.IsUserAuthenticated = false;
        }

        /// <summary>
        /// The save user credentials.
        /// </summary>
        private void SaveUserCredentials()
        {
            var viewModelWorker = (VBForumsMetroViewModelWorker)this.VMWorker;
            var loginCredentials = new LoginCredentialModel() { UserName = this.UserName, Password = this.Password };

            viewModelWorker.VBForumsMetroSterlingService.Database.Save(loginCredentials);

            viewModelWorker.VBForumsMetroSterlingService.Database.Flush();

            this.MemberId = loginCredentials.Id;
        }

        /// <summary>
        /// The update user credentials.
        /// </summary>
        private void UpdateUserCredentials()
        {
            var viewModelWorker = (VBForumsMetroViewModelWorker)this.VMWorker;
            viewModelWorker.VBForumsMetroSterlingService.Database.Save(
                new LoginCredentialModel
                {
                    Id = this.MemberId,
                    UserName = this.UserName,
                    Password = this.Password,
                });

            viewModelWorker.VBForumsMetroSterlingService.Database.Flush();
        }

        /// <summary>
        /// The delete user credentials.
        /// </summary>
        /// <param name="id">The member id.</param>
        private void DeleteUserCredentials(int id)
        {
            var viewModelWorker = (VBForumsMetroViewModelWorker)this.VMWorker;

            var deleteUserCredentials =
                (from o in viewModelWorker.VBForumsMetroSterlingService.Database.Query<LoginCredentialModel, int>()
                 where o.LazyValue.Value.Id == id
                 select o.LazyValue.Value).FirstOrDefault();

            if (deleteUserCredentials == null)
            {
                return;
            }

            viewModelWorker.VBForumsMetroSterlingService.Database.Delete(deleteUserCredentials);
            viewModelWorker.VBForumsMetroSterlingService.Database.Flush();
        }
    }
}