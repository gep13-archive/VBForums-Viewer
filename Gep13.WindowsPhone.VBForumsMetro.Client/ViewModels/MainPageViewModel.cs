//-----------------------------------------------------------------------
// <copyright file="MainPageViewModel.cs" company="GEP13">
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
    using Caliburn.Micro;

    using Gep13.WindowsPhone.VBForumsMetro.Core.Workers;

    /// <summary>
    /// The main page view model.
    /// </summary>
    public sealed class MainPageViewModel : Conductor<PivotItemViewModelBase>.Collection.OneActive
    {
        /// <summary>
        /// The view model worker.
        /// </summary>
        private VBForumsMetroViewModelWorker viewModelWorker;

        /// <summary>
        /// The profile view model.
        /// </summary>
        private ProfileViewModel profileViewModel;

        /// <summary>
        /// The reputation view model.
        /// </summary>
        private ReputationListViewModel reputationViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageViewModel"/> class. 
        /// Use constructor injection to supply child VMs as required
        /// </summary>
        /// <param name="viewModelWorker">The View Model Worker.</param>
        /// <param name="profileViewModel">The Profile View Model.</param>
        /// <param name="reputationListViewModel">The Reputation View Model.</param>
        public MainPageViewModel(VBForumsMetroViewModelWorker viewModelWorker, ProfileViewModel profileViewModel, ReputationListViewModel reputationListViewModel)
        {
            // set the display name to bind
            this.DisplayName = "PIVOT PAGE";

            this.viewModelWorker = viewModelWorker;
            this.profileViewModel = profileViewModel;
            this.reputationViewModel = reputationListViewModel;
        }

        /// <summary>
        /// called once only before activation per VM instance
        /// </summary>
        protected override void OnInitialize()
        {
            base.OnInitialize();

            // add the pivot items vm to the pivot bound items collection
            Items.Add(this.profileViewModel);
            Items.Add(this.reputationViewModel);

            // activate first pivot item
            this.ActivateItem(this.Items[0]);
        }
    }
}
