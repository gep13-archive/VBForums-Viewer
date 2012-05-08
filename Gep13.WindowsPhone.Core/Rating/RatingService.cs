//-----------------------------------------------------------------------
// <copyright file="RatingService.cs" company="GEP13">
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

namespace Gep13.WindowsPhone.Core.Rating
{
    using Telerik.Windows.Controls;
    using Telerik.Windows.Controls.Reminders;

    /// <summary>
    /// Concrete implemenation of the IRatingService to rate the application
    /// </summary>
    public class RatingService : IRatingService
    {
        /// <summary>
        /// The DefaultText to show in the ProgressIndicator
        /// </summary>
        private string applicationName;

        /// <summary>
        /// Initializes a new instance of the RatingService class
        /// </summary>
        /// <param name="applicationName">The name of the application you want to provide ratings for</param>
        public RatingService(string applicationName)
        {
            this.applicationName = applicationName;
        }

        /// <summary>
        /// Check to see whehter the user needs to rate the application
        /// </summary>
        public void CheckWhetherUserWantsToRateApplication()
        {
            var radRateApplicationReminder = new RadRateApplicationReminder
            {
                RecurrencePerUsageCount = 50,
                AllowUsersToSkipFurtherReminders = true,
                MessageBoxInfo = new MessageBoxInfoModel()
                {
                    Buttons = MessageBoxButtons.YesNo,
                    Content =
                        "Would you like to rate our app in the marketplace?",
                    SkipFurtherRemindersMessage =
                        "Skip further reminders",
                    Title = string.Format("Rate {0}", this.applicationName)
                }
            };
            radRateApplicationReminder.Notify();
        }
    }
}