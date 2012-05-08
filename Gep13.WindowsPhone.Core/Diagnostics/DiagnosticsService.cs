//-----------------------------------------------------------------------
// <copyright file="DiagnosticsService.cs" company="GEP13">
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

namespace Gep13.WindowsPhone.Core.Diagnostics
{
    using System.IO;
    using System.IO.IsolatedStorage;
    using Microsoft.Phone.Tasks;
    using Telerik.Windows.Controls;

    /// <summary>
    /// Concrete implemenation of the IDiagnosticsService to report exceptions
    /// </summary>
    public class DiagnosticsService : IDiagnosticsService
    {
        /// <summary>
        /// The file name to be used to store excpetion information
        /// </summary>
        private string fileName;

        /// <summary>
        /// The name of the Application that is being used
        /// </summary>
        private string applicationName;

        /// <summary>
        /// The email address that exception messages should be sent to
        /// </summary>
        private string emailAddress;

        /// <summary>
        /// Initializes a new instance of the DiagnosticsService class
        /// </summary>
        /// <param name="fileName">The file name to be used to store excpetion information</param>
        /// /// <param name="applicationName">The name of the Application that is being used</param>
        /// /// <param name="emailAddress">The email address that exception messages should be sent to</param>
        public DiagnosticsService(string fileName, string applicationName, string emailAddress)
        {
            this.fileName = fileName;
            this.applicationName = applicationName;
            this.emailAddress = emailAddress;
        }

        /// <summary>
        /// Report the exception to the Diagnostics Service
        /// </summary>
        /// <param name="ex">The exception which is to be reported</param>
        /// <param name="extra">Additional information to include within the report</param>
        public void ReportException(System.Exception ex, string extra)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                this.SafeDeleteFile(store);
                using (TextWriter output = new StreamWriter(store.CreateFile(this.fileName)))
                {
                    output.WriteLine(extra);
                    output.WriteLine(ex.Message);
                    output.WriteLine(ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// Check to see whether there are any existing exceptions which need to be reported.
        /// </summary>
        public void CheckForPreviousException()
        {
            try
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (store.FileExists(this.fileName))
                    {
                        string contents = null;

                        using (TextReader reader = new StreamReader(store.OpenFile(this.fileName, FileMode.Open, FileAccess.Read, FileShare.None)))
                        {
                            contents = reader.ReadToEnd();
                        }

                        this.SafeDeleteFile(store);

                        RadMessageBox.Show(
                            "Problem Report",
                            MessageBoxButtons.YesNo,
                            string.Format("A problem has occurred with {0}, we apologise for any inconvenience that this has caused.  In order to help us solve this problem in the future, we would kindly ask that you report this issue to us for investigation.  Would you like to send an email to report it?", this.applicationName),
                            closedHandler: (args) =>
                            {
                                if (args.Result == DialogResult.OK)
                                {
                                    EmailComposeTask email = new EmailComposeTask()
                                    {
                                        To = emailAddress,
                                        Subject =
                                            this.applicationName +
                                            " auto-generated problem report",
                                        Body = contents
                                    };

                                    SafeDeleteFile(IsolatedStorageFile.GetUserStoreForApplication());
                                    email.Show();
                                }
                            });
                    }
                }
            }
            finally
            {
                this.SafeDeleteFile(IsolatedStorageFile.GetUserStoreForApplication());
            }
        }

        /// <summary>
        /// Helper method to delete the file that contains the exception information when no longer required
        /// </summary>
        /// <param name="store">The IsoloatedStorageFile that holds the exception</param>
        private void SafeDeleteFile(IsolatedStorageFile store)
        {
            store.DeleteFile(this.fileName);
        }
    }
}