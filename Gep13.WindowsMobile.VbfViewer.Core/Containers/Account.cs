
namespace Gep13.WindowsMobile.VbfViewer.Core.Containers
{
    public class Account
    {
        /// <summary>
        /// Gets or Sets the Username used
        /// to log in to the users chosen
        /// provider.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or Sets the Password used
        /// to log in to the users chosen
        /// provider.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or Sets the friendly name
        /// for this account instance.
        /// </summary>
        public string DisplayName { get; set; }
    }
}