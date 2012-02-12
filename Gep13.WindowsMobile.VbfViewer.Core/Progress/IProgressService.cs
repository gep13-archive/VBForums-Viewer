//-----------------------------------------------------------------------
// <copyright file="IProgressService.cs" company="GEP13">
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
    /// <summary>
    /// Interface to provide the ability to Show/Hide a ProgressIndicator on the page
    /// </summary>
    public interface IProgressService
    {
        /// <summary>
        /// Gets or sets a value indicating whether the ProgressIndicator is currently visible or not
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Show the ProgressIndicator on the page
        /// </summary>
        void Show();

        /// <summary>
        /// Show the ProgressIndicator on the page along with a custom text string
        /// </summary>
        /// <param name="text">The Text to be shown in the ProgressIndicator</param>
        void Show(string text);

        /// <summary>
        /// Hide the ProgressIndicator on the page
        /// </summary>
        void Hide();
    }
}