//-----------------------------------------------------------------------
// <copyright file="ApplicationSettings.cs" company="GEP13">
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

namespace Gep13.WindowsPhone.Core.Models
{
    using System.IO.IsolatedStorage;
    using System.Reflection;

    using Caliburn.Micro;

    using Gep13.WindowsPhone.Core.Helpers;

    /// <summary>
    /// The application settings.
    /// </summary>
    public class ApplicationSettings : PropertyChangedBase
    {
        /// <summary>
        /// The current set of ApplicationSettings
        /// </summary>
        private static ApplicationSettings current;

        /// <summary>
        /// Backing variable indicating whether the application is currently in trial mode
        /// </summary>
        private bool isTrial;

        /// <summary>
        /// The client version.
        /// </summary>
        private string clientVersion;

        /// <summary>
        /// Prevents a default instance of the <see cref="ApplicationSettings"/> class from being created.
        /// </summary>
        private ApplicationSettings()
        {
            string name = Assembly.GetExecutingAssembly().FullName;
            var version = new AssemblyName(name).Version;
            this.ClientVersion = version.ToString();
        }

        /// <summary>
        /// Gets or sets the current set of ApplicationSettings.
        /// </summary>
        public static ApplicationSettings Current
        {
            get
            {
                return current ?? (current = new ApplicationSettings());
            }

            set
            {
                current = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether is trial.
        /// </summary>
        public bool IsTrial
        {
            get
            {
                return this.isTrial;
            }

            set
            {
                this.isTrial = value;
                this.NotifyOfPropertyChange(() => this.IsTrial);
            }
        }

        /// <summary>
        /// Gets or sets the client version number of the application - used to compare against their SettingsVersion to handle settings upgrades
        /// </summary>
        public string ClientVersion
        {
            get
            {
                return this.clientVersion;
            }

            set
            {
                this.clientVersion = value;
                this.NotifyOfPropertyChange(() => this.ClientVersion);
            }
        }

        /// <summary>
        /// Gets or sets the settings version number of the user's Settings file  - used to compare against their ClientVersion to handle settings upgrades
        /// </summary>
        public string SettingsVersion
        {
            get
            {
                return IsolatedStorageSettings.ApplicationSettings.GetSetting("SettingsVersion", this.ClientVersion);
            }

            set
            {
                IsolatedStorageSettings.ApplicationSettings["SettingsVersion"] = value;
                this.NotifyOfPropertyChange(() => this.SettingsVersion);
            }
        }

        /// <summary>
        /// The delete all settings.
        /// </summary>
        public void DeleteAllSettings()
        {
            this.SettingsVersion = this.ClientVersion;
        }
    }
}