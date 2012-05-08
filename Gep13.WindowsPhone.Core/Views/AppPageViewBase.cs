//-----------------------------------------------------------------------
// <copyright file="AppPageViewBase.cs" company="GEP13">
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

namespace Gep13.WindowsPhone.Core.Views
{
    using System.Reflection;
    using System.Windows.Navigation;
    using Microsoft.Phone.Controls;
    using TombstoneHelper;

    /// <summary>
    /// The base class for all views
    /// </summary>
    public class AppPageViewBase : PhoneApplicationPage
    {
        /// <summary>
        /// Initializes a new instance of the AppPageViewBase class
        /// </summary>
        public AppPageViewBase()
        {
            var initializeComponentMethod = GetType().GetMethod("InitializeComponent", BindingFlags.Public | BindingFlags.Instance);
            if (initializeComponentMethod != null)
            {
                initializeComponentMethod.Invoke(this, new object[0]);
            }
        }

        /// <summary>
        /// Override the OnNavigatedTo Method
        /// </summary>
        /// <param name="e">The NavigationEventArgs</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.RestoreState();
        }

        /// <summary>
        /// Override the OnNavigatedFrom Method
        /// </summary>
        /// <param name="e">The NavigationEventArgs</param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        /// <summary>
        /// Override the OnNavigatingFrom Method
        /// </summary>
        /// <param name="e">The NavigatingCancelEventArgs</param>
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            this.SaveState(e);
        }
    }
}