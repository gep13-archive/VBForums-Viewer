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
    using Gep13.WindowsPhone.VBForumsMetro.Client.Workers;

    /// <summary>
    /// The ViewModel class for the AddAccount page
    /// </summary>
    public class AddAccountViewModel : ScreenPageViewModelBase
    {
        /// <summary>
        /// Local variable for the userName
        /// </summary>
        private string userName;

        /// <summary>
        /// Local variale for the password
        /// </summary>
        private string password;

        /// <summary>
        /// Initializes a new instance of the AddAccountViewModel class
        /// </summary>
        /// <param name="viewModelWorker">The View Model Worker from common access properties</param>
        public AddAccountViewModel(ViewModelWorker viewModelWorker)
            : base(viewModelWorker)
        {
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
                return this.userName;
            }

            set
            {
                this.userName = value;
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
                return this.password;
            }

            set
            {
                this.password = value;
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
            ////this.CommitAccountToStorage();
            ////this.SetFirstRunFlag();
        }

        /// <summary>
        /// Worker method to set the FirstRunFlag to indicate that the user have set the account details
        /// </summary>
        private void SetFirstRunFlag()
        {
            ////this.storageService.Add("FirstRunFlag", true);
        }
    }
}