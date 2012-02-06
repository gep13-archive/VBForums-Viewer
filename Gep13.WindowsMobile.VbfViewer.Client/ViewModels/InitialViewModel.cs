using Caliburn.Micro;
using Gep13.WindowsMobile.VbfViewer.Core.Storage;

namespace Gep13.WindowsMobile.VbfViewer.Client.ViewModels
{
    public class InitialViewModel : PropertyChangedBase
    {
        // The NavigationService is an object built into
        // Caliburn.Micro to enable ViewModel to ViewModel
        // navigation. We are going to get this via IOC
        // from the container we made in the bootstrapper. I
        // think this is added to the container from register
        // phone services. Either way we get it for free.

        private readonly INavigationService _navigationService;

        // Constructor asks for an INavigationService, Container
        // obliges. We don't need to worry how it gets here :)

        public InitialViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        // Bound to the loaded event of the view. Caliburn.Micro
        // has a concept called Event Triggers that allow you
        // to fire methods in response to events.

        public void DetermineNavigationPath()
        {

            switch (ObtainFirstRunFlag())
            {
                case true:

                    NavigateToWelcomeView();
                    break;

                default:

                    NavigateToComposeView();
                    break;
            }
        }

        // ApplicationStorage is a wrapper around Isolated Storage
        // that allows us to get certain values in the context of
        // the application. Here we are asking if the app has
        // been ran before on this unit.

        private bool ObtainFirstRunFlag()
        {
            return ApplicationStorage.RetriveFirstRunFlag();
        }

        // Use the NavigationService to navigate to the WelcomeView.
        // This view allows the user to set up an account if it
        // is the first time they have ran the app on their device.
        private void NavigateToWelcomeView()
        {
            _navigationService.UriFor<WelcomeViewModel>().Navigate();
        }

        // If it is not the first time the app has been ran then
        // navigate to the ComposeViewModel and don't take them
        // through account creation.
        private void NavigateToComposeView()
        {
            // _navigationService.UriFor<ComposeViewModel>().Navigate();
        }
    }
}