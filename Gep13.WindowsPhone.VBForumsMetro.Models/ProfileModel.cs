//-----------------------------------------------------------------------
// <copyright file="ProfileModel.cs" company="GEP13">
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

namespace Gep13.WindowsPhone.VBForumsMetro.Models
{
    using System;

    /// <summary>
    /// The profile model.
    /// </summary>
    public class ProfileModel : BaseModel<ProfileModel>
    {
        /// <summary>
        /// The user name.
        /// </summary>
        private string userName;

        /// <summary>
        /// The custom user title.
        /// </summary>
        private string customUserTitle;

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
        /// Gets or sets the custom user title.
        /// </summary>
        public string CustomUserTitle
        {
            get
            {
                return this.customUserTitle;
            }

            set
            {
                this.customUserTitle = value;
                this.NotifyOfPropertyChange(() => this.CustomUserTitle);
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
    }
}