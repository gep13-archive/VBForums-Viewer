//-----------------------------------------------------------------------
// <copyright file="ProgressService.cs" company="GEP13">
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
//
//      This implementation has been taken from http://blog.caraulean.com/2011/12/09/using-wp7s-progressindicator-with-caliburn-micro/ and
//      adapted as required.
// </copyright>
//-----------------------------------------------------------------------

namespace Gep13.WindowsMobile.VbfViewer.Core.Progress
{
    using System.Windows.Navigation;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;

    /// <summary>
    /// Concrete implemenation of the IProgressService to provide ability for any page to Show/Hide a ProgressIndicator
    /// </summary>
    public class ProgressService : IProgressService
    {
        /// <summary>
        /// The DefaultText to show in the ProgressIndicator
        /// </summary>
        private const string DefaultIndicatorText = "Loading...";

        /// <summary>
        /// Local instance of the ProgressIndicator to show on the page
        /// </summary>
        private readonly ProgressIndicator progressIndicator;

        /// <summary>
        /// Initializes a new instance of the ProgressService class
        /// </summary>
        /// <param name="rootFrame">The incoming page that is being navigated to</param>
        public ProgressService(PhoneApplicationFrame rootFrame)
        {
            this.progressIndicator = new ProgressIndicator { Text = DefaultIndicatorText };

            rootFrame.Navigated += this.RootFrameOnNavigated;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the ProgressIndicator is currently visible or not
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Show the ProgressIndicator on the page
        /// </summary>
        public void Show()
        {
            Show(DefaultIndicatorText);
            this.IsEnabled = true;
        }

        /// <summary>
        /// Show the ProgressIndicator on the page along with a custom text string
        /// </summary>
        /// <param name="text">The Text to be shown in the ProgressIndicator</param>
        public void Show(string text)
        {
            this.progressIndicator.Text = text;
            this.progressIndicator.IsIndeterminate = true;
            this.progressIndicator.IsVisible = true;

            this.IsEnabled = true;
        }

        /// <summary>
        /// Hide the ProgressIndicator on the page
        /// </summary>
        public void Hide()
        {
            this.progressIndicator.IsIndeterminate = false;
            this.progressIndicator.IsVisible = false;

            this.IsEnabled = false;
        }

        /// <summary>
        /// Handle the OnNavigated event to this page, in order to assign our instance of the ProgressIndicator to this page
        /// </summary>
        /// <param name="sender">The object that raised the Navigation Event</param>
        /// <param name="args">Arguments passed in with the event</param>
        private void RootFrameOnNavigated(object sender, NavigationEventArgs args)
        {
            var content = args.Content;
            var page = content as PhoneApplicationPage;

            if (page == null)
            { 
                return;
            }

            page.SetValue(SystemTray.ProgressIndicatorProperty, this.progressIndicator);
        }
    }
}