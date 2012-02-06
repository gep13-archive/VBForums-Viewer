using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Caliburn.Micro;
using Microsoft.Phone.Controls;

namespace Gep13.WindowsMobile.VbfViewer.Client.ViewModels
{
    public class WelcomeViewModel
    {
        private readonly INavigationService navigationService;

        public WelcomeViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public void NavigateToAddAccountView()
        {
            (App.Current.RootVisual as PhoneApplicationFrame).RemoveBackEntry();

            // navigationService.UriFor<AddAccountViewModel>().Navigate();
        }
    }
}