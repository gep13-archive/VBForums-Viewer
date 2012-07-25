//-----------------------------------------------------------------------
// <copyright file="PivotItemViewModelBase.cs" company="GEP13">
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

    using Caliburn.Micro;

    using Gep13.WindowsPhone.VBForumsMetro.Core.Workers;

    /// <summary>
    /// The pivot item view model base.
    /// </summary>
    public class PivotItemViewModelBase : Screen, IHandle<DateTime>
    {
        /// <summary>
        /// The current date time text.
        /// </summary>
        private string currentDateTimeText;

        /// <summary>
        /// The vm worker.
        /// </summary>
        private VBForumsMetroViewModelWorker viewModelWorker;

        /// <summary>
        /// Initializes a new instance of the <see cref="PivotItemViewModelBase"/> class.
        /// </summary>
        /// <param name="viewModelWorker">The vm worker.</param>
        public PivotItemViewModelBase(VBForumsMetroViewModelWorker viewModelWorker)
        {
            this.viewModelWorker = viewModelWorker;
        }

        /// <summary>
        /// Gets or sets the current date time text.
        /// </summary>
        public string CurrentDateTimeText
        {
            get
            {
                return this.currentDateTimeText;
            }

            set
            {
                this.currentDateTimeText = value;
                this.NotifyOfPropertyChange(() => this.CurrentDateTimeText);
            }
        }

        /// <summary>
        /// handle datetime message
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        public void Handle(DateTime dateTime)
        {
            this.CurrentDateTimeText = dateTime.ToString("dd/MM/yyyy hh:mm:ss");
        }

        /// <summary>
        /// View navigated to or brought into view
        /// </summary>
        protected override void OnActivate()
        {
            base.OnActivate();

            // subscribe the pivot item to future messages
            this.viewModelWorker.EventAggregator.Subscribe(this);
        }

        /// <summary>
        /// The on deactivate.
        /// </summary>
        /// <param name="close">The close.</param>
        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);

            // unsubscribe the pivot item from future messages
            this.viewModelWorker.EventAggregator.Unsubscribe(this);
        }
    }
}