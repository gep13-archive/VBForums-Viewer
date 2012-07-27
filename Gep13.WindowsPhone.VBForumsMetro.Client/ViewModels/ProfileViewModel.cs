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
    using System.Threading.Tasks;

    using Gep13.WindowsPhone.VBForumsMetro.Core.Workers;
    using Gep13.WindowsPhone.VBForumsMetro.Models;

    /// <summary>
    /// The ViewModel class for the Profile page
    /// </summary>
    public class ProfileViewModel : PivotItemViewModelBase
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
        /// The join date.
        /// </summary>
        private DateTime joinDate;

        /// <summary>
        /// The posts.
        /// </summary>
        private int posts;

        /// <summary>
        /// The posts per day.
        /// </summary>
        private double postsPerDay;

        /// <summary>
        /// The profile picture url.
        /// </summary>
        private Uri profilePictureUrl;

        /// <summary>
        /// Initializes a new instance of the ProfileViewModel class
        /// </summary>
        /// <param name="viewModelWorker">The View Model Worker from common access properties</param>
        public ProfileViewModel(VBForumsMetroViewModelWorker viewModelWorker)
            : base(viewModelWorker)
        {
            this.DisplayName = "profile";
        }

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
            }
        }

        /// <summary>
        /// Gets or sets the join date.
        /// </summary>
        public DateTime JoinDate
        {
            get
            {
                return this.joinDate;
            }

            set
            {
                this.joinDate = value;
                this.NotifyOfPropertyChange(() => this.JoinDate);
            }
        }

        /// <summary>
        /// Gets or sets the posts.
        /// </summary>
        public int Posts
        {
            get
            {
                return this.posts;
            }

            set
            {
                this.posts = value;
                this.NotifyOfPropertyChange(() => this.Posts);
            }
        }

        /// <summary>
        /// Gets or sets the posts per day.
        /// </summary>
        public double PostsPerDay
        {
            get
            {
                return this.postsPerDay;
            }

            set
            {
                this.postsPerDay = value;
                this.NotifyOfPropertyChange(() => this.PostsPerDay);
            }
        }

        /// <summary>
        /// Gets or sets the profile picture url.
        /// </summary>
        public Uri ProfilePictureUrl
        {
            get
            {
                return this.profilePictureUrl;
            }

            set
            {
                this.profilePictureUrl = value;
                this.NotifyOfPropertyChange(() => this.ProfilePictureUrl);
            }
        }

        /// <summary>
        /// An overridden implemenation of the OnViewLoaded method to do specific functionality within this view
        /// </summary>
        /// <param name="view">The current view</param>
        protected override void OnViewLoaded(object view)
        {
            this.GetProfile();

            base.OnViewLoaded(view);
        }

        /// <summary>
        /// The get profile.
        /// </summary>
        private async void GetProfile()
        {
            var viewModelWorker = (VBForumsMetroViewModelWorker)this.VMWorker;

            var id = await this.GetMemberId();

            var profile =
                await
                viewModelWorker.VBForumsWebService.GetProfileForUser(
                    id, new LoginCredentialModel() { UserName = "gep31", Password = "qwerty" });
            this.MemberId = profile.MemberId;
            this.UserName = profile.UserName;
            this.JoinDate = profile.JoinDate;
            this.Posts = profile.Posts;
            this.PostsPerDay = profile.PostsPerDay;
            this.ProfilePictureUrl = profile.ProfilePictureUrl;
        }

        /// <summary>
        /// The get member id.
        /// </summary>
        /// <returns>
        /// The System.Threading.Tasks.Task`1[TResult -&gt; System.Int32].
        /// </returns>
        private async Task<int> GetMemberId()
        {
         var viewModelWorker = (VBForumsMetroViewModelWorker)this.VMWorker;
            var id =
                await
                viewModelWorker.VBForumsWebService.GetMemberIdForUser(
                    new LoginCredentialModel() { UserName = "gep31", Password = "qwerty" });

            return id;
        }
    }
}