﻿//-----------------------------------------------------------------------
// <copyright file="AppBootstrapper.cs" company="GEP13">
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

namespace Gep13.WindowsMobile.VbfViewer.Client 
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;
    using Caliburn.Micro;
    using Gep13.WindowsMobile.VbfViewer.Client.ViewModels;
    using Gep13.WindowsMobile.VbfViewer.Core.Containers;
    using Gep13.WindowsMobile.VbfViewer.Core.Progress;
    using Microsoft.Phone.Controls;

    /// <summary>
    /// This is the main class which is used by Caliburn.Micro
    /// </summary>
    public class AppBootstrapper : PhoneBootstrapper
    {
        /// <summary>
        /// Top level container object for registering new components
        /// </summary>
        private PhoneContainer container;

        /// <summary>
        /// The Caliburn.Micro method to assign the deafult set of conventions
        /// </summary>
        protected static void AddCustomConventions()
        {
            ConventionManager.AddElementConvention<Pivot>(Pivot.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
                (viewModelType, path, property, element, convention) =>
                {
                    if (ConventionManager
                        .GetElementConvention(typeof(ItemsControl))
                        .ApplyBinding(viewModelType, path, property, element, convention))
                    {
                        ConventionManager
                            .ConfigureSelectedItem(element, Pivot.SelectedItemProperty, viewModelType, path);
                        ConventionManager
                            .ApplyHeaderTemplate(element, Pivot.HeaderTemplateProperty, null, viewModelType);
                        return true;
                    }

                    return false;
                };

            ConventionManager.AddElementConvention<Panorama>(Panorama.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
                (viewModelType, path, property, element, convention) =>
                {
                    if (ConventionManager
                        .GetElementConvention(typeof(ItemsControl))
                        .ApplyBinding(viewModelType, path, property, element, convention))
                    {
                        ConventionManager
                            .ConfigureSelectedItem(element, Panorama.SelectedItemProperty, viewModelType, path);
                        ConventionManager
                            .ApplyHeaderTemplate(element, Panorama.HeaderTemplateProperty, null, viewModelType);
                        return true;
                    }

                    return false;
                };
        }

        /// <summary>
        /// Default method created by the Caliburn.Micro Framework
        /// </summary>
        protected override void Configure()
        {
            this.container = new PhoneContainer(RootFrame);
            this.container.RegisterPhoneServices();

            this.container.PerRequest<InitialViewModel>();
            this.container.PerRequest<WelcomeViewModel>();
            this.container.PerRequest<AddAccountViewModel>();
            this.container.PerRequest<ProfileViewModel>();

            this.container.PerRequest<Account>();

            this.container.Instance<IProgressService>(new ProgressService(RootFrame));

            AddCustomConventions();
        }

        /// <summary>
        /// Default method created by the Caliburn.Micro Framework
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="key">The string</param>
        /// <returns>The instance</returns>
        protected override object GetInstance(Type service, string key)
        {
            return this.container.GetInstance(service, key);
        }

        /// <summary>
        /// Default method created by the Caliburn.Micro Framework
        /// </summary>
        /// <param name="service">The service</param>
        /// <returns>All instances</returns>
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return this.container.GetAllInstances(service);
        }

        /// <summary>
        /// Default method created by the Caliburn.Micro Framework
        /// </summary>
        /// <param name="instance">The instance that needs to be built up.</param>
        protected override void BuildUp(object instance)
        {
            this.container.BuildUp(instance);
        }
    }
}