//-----------------------------------------------------------------------
// <copyright file="VBForumsWebService.cs" company="GEP13">
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

namespace Gep13.WindowsPhone.VBForumsMetro.Core.Web
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Gep13.WindowsPhone.VBForumsMetro.Models;

    /// <summary>
    /// The vb forums web service.
    /// </summary>
    public class VBForumsWebService : IVBForumsWebService
    {
        /// <summary>
        /// The is valid login credential.
        /// </summary>
        /// <param name="loginCredential">The login credential.</param>
        /// <returns>True if valid credentials, otherwise false.</returns>
        public async Task<bool> IsValidLoginCredential(LoginCredentialModel loginCredential)
        {
            var uri = new Uri("http://www.vbforums.com/login.php?do=login");

            var postString =
                string.Format(
                    "do=login&url=%2Fusercp.php&vb_login_md5password=&vb_login_md5password_utf=&s=&securitytoken=guest&vb_login_username={0}&vb_login_password={1}",
                    loginCredential.UserName,
                    loginCredential.Password);

            var request = WebRequest.CreateHttp(uri);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            var requestSteam = await request.GetRequestStreamAsync();

            using (var writer = new StreamWriter(requestSteam))
            {
                writer.Write(postString);
            }

            var response = (HttpWebResponse)await request.GetResponseAsync();
            var statusCode = response.StatusCode;

            if ((int)statusCode >= 400)
            {
                return false;
            }

            string responseString;
            using (var responseStream = new StreamReader(response.GetResponseStream()))
            {
                responseString = await responseStream.ReadToEndAsync();
            }

            return !responseString.Contains("<!-- main error message -->");
        }

        /// <summary>
        /// The get reputation entries for user.
        /// </summary>
        /// <param name="loginCredential">The login credential.</param>
        /// <returns>The System.Collections.Generic.IEnumerable`1[T -&gt; Gep13.WindowsPhone.VBForumsMetro.Models.ReputationModel].</returns>
        public async Task<IEnumerable<ReputationModel>> GetReputationEntriesForUser(LoginCredentialModel loginCredential)
        {
            var uri = new Uri("http://www.vbforums.com/usercp.php");

            var request = WebRequest.CreateHttp(uri);
            request.Method = "GET";
            request.Credentials = new NetworkCredential(loginCredential.UserName, loginCredential.Password);

            var response = (HttpWebResponse)await request.GetResponseAsync();
            var statusCode = response.StatusCode;

            if ((int)statusCode >= 400)
            {
                return null;
            }

            string responseString;
            using (var responseStream = new StreamReader(response.GetResponseStream()))
            {
                responseString = await responseStream.ReadToEndAsync();
            }

            if (!responseString.Contains("id=\"collapseobj_usercp_reputation\""))
            {
                return null;
            }

            var repTable =
                responseString.Substring(
                    responseString.IndexOf(
                        "<tbody id=\"collapseobj_usercp_reputation\"", StringComparison.Ordinal));

            repTable = repTable.Substring(repTable.IndexOf("<tr>", StringComparison.Ordinal));
            repTable = repTable.Substring(0, repTable.IndexOf("</tbody>", StringComparison.Ordinal) - 1);
            repTable = repTable.Replace("\n", " ").Replace("\r", " ").Replace("\t", " ");
            while (repTable.IndexOf("  ", StringComparison.Ordinal) > 0)
            {
                repTable = repTable.Replace("  ", " ");
            }

            repTable = repTable.Replace("> <", "><").Trim();

            var reputations = new List<ReputationModel>()
                {
                    new ReputationModel()
                        {
                            AuthorName = "gep13",
                            AuthorUri = new Uri("http://www.gep13.co.uk/blog"),
                            Comment = "test comment",
                            CreationDate = DateTime.Now,
                            ThreadName = "Test Thread",
                            ThreadUri = new Uri("http://www.vbforums.com")
                        }
                };

            return reputations;
        }

        /// <summary>
        /// The get profile for user.
        /// </summary>
        /// <param name="memberId">The member id for the profile.</param>
        /// <param name="loginCredential">The login credential.</param>
        /// <returns>The System.Threading.Tasks.Task`1[TResult -&gt; Gep13.WindowsPhone.VBForumsMetro.Models.ProfileModel].</returns>
        public async Task<ProfileModel> GetProfileForUser(int memberId, LoginCredentialModel loginCredential)
        {
            var uri = new Uri("http://www.vbforums.com/login.php?do=login");

            var postString =
                string.Format(
                    "do=login&url=%2Fusercp.php&vb_login_md5password=&vb_login_md5password_utf=&s=&securitytoken=guest&vb_login_username={0}&vb_login_password={1}",
                    loginCredential.UserName,
                    loginCredential.Password);

            var cookieContainer = new CookieContainer();
            var request = WebRequest.CreateHttp(uri);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = cookieContainer;

            var requestSteam = await request.GetRequestStreamAsync();

            using (var writer = new StreamWriter(requestSteam))
            {
                writer.Write(postString);
            }

            var loginResponse = await request.GetResponseAsync();
            loginResponse.Close();

            uri = new Uri(string.Format("http://www.vbforums.com/member.php?u={0}", memberId));

            request = WebRequest.CreateHttp(uri);
            request.Method = "GET";
            request.CookieContainer = cookieContainer;

            var response = (HttpWebResponse)await request.GetResponseAsync();
            var statusCode = response.StatusCode;

            if ((int)statusCode >= 400)
            {
                return null;
            }     

            string responseString;
            using (var responseStream = new StreamReader(response.GetResponseStream()))
            {
                responseString = await responseStream.ReadToEndAsync();
            }

            var profile = new ProfileModel();

            // Example HTML that is being parsed at this point
            // <li><span class="shade">Join Date:</span> Nov 16th, 2004</li>
            var rx = new Regex("<li><span class=\"shade\">Join Date:</span> (.* [0-9]{1,2}).*, ([0-9]{2,4})</li>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var matches = rx.Matches(responseString);
            
            var joinDate = DateTime.MinValue;

            if (matches.Count != 1)
            {
                joinDate = DateTime.MaxValue;
            }
            else
            {
                var dateTimeString = string.Format(
                    "{0}, {1}", matches[0].Groups[1], matches[0].Groups[2]);

                if (!DateTime.TryParseExact(dateTimeString, "MMM d, yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out joinDate))
                {
                    joinDate = DateTime.MaxValue;
                }
            }

            profile.JoinDate = joinDate;

            // Example HTML that is being parsed at this point
            // <li><span class="shade">Posts Per Day:</span> 7.61</li>
            rx = new Regex("<li><span class=\"shade\">Posts Per Day:</span> (.*)</li>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            matches = rx.Matches(responseString);
            var postsPerDay = 0d;

            if (matches.Count != 1)
            {
                postsPerDay = -1d;
            }
            else
            {
                if (!double.TryParse(matches[0].Groups[1].ToString(), out postsPerDay))
                {
                    postsPerDay = -1d;
                }
            }

            profile.PostsPerDay = postsPerDay;

            // Example HTML that is being parsed at this point
            // <li><span class="shade">Total Posts:</span> 21,358</li>
            rx = new Regex("<li><span class=\"shade\">Total Posts:</span> (.*)</li>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            matches = rx.Matches(responseString);
            var posts = 0;

            if (matches.Count != 1)
            {
                posts = -1;
            }
            else
            {
                if (!int.TryParse(matches[0].Groups[1].ToString(), out posts))
                {
                    posts = -1;
                }
            }

            profile.Posts = posts;

            // Example HTML that is being parsed at this point
            // <td id="profilepic_cell" class="tborder alt2"><img src="image.php?u=53106&amp;dateline=1277553514&amp;type=profile"  width="64" height="64"  alt="gep13's Profile Picture" /></td>
            rx = new Regex("<td id=\"profilepic_cell\" class=\"tborder alt2\"><img src=\"(.*)\"", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            matches = rx.Matches(responseString);

            profile.ProfilePictureUrl = matches.Count != 1 ? null : new Uri(matches[0].ToString());

            // Example HTML that is being parsed at this point
            // <strong>Welcome, <a href="member.php?u=53106">gep13</a>.</strong><br />
            var pattern = string.Format("<strong>Welcome, <a href=\"member.php{0}u=.*\">(.*)</a>", Regex.Escape("?"));

            rx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            matches = rx.Matches(responseString);

            profile.UserName = matches.Count != 1 ? string.Empty : matches[0].Groups[1].ToString();

            return profile;
        }

        /// <summary>
        /// The get member id for user.
        /// </summary>
        /// <param name="loginCredential">The login credential.</param>
        /// <returns>The System.Threading.Tasks.Task`1[TResult -&gt; System.Int32].</returns>
        public async Task<int> GetMemberIdForUser(LoginCredentialModel loginCredential)
        {
            var uri = new Uri("http://www.vbforums.com/login.php?do=login");

            var postString =
                string.Format(
                    "do=login&url=%2Fusercp.php&vb_login_md5password=&vb_login_md5password_utf=&s=&securitytoken=guest&vb_login_username={0}&vb_login_password={1}",
                    loginCredential.UserName,
                    loginCredential.Password);

            var cookieContainer = new CookieContainer();
            var request = WebRequest.CreateHttp(uri);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = cookieContainer;

            var requestSteam = await request.GetRequestStreamAsync();

            using (var writer = new StreamWriter(requestSteam))
            {
                writer.Write(postString);
            }

            var loginResponse = await request.GetResponseAsync();
            loginResponse.Close();

            uri = new Uri("http://www.vbforums.com/usercp.php");

            request = WebRequest.CreateHttp(uri);
            request.Method = "GET";
            request.CookieContainer = cookieContainer;

            var response = (HttpWebResponse)await request.GetResponseAsync();
            var statusCode = response.StatusCode;

            if ((int)statusCode >= 400)
            {
                return 0;
            }

            string responseString;
            using (var responseStream = new StreamReader(response.GetResponseStream()))
            {
                responseString = await responseStream.ReadToEndAsync();
            }

            // Example HTML that is being parsed at this point
            // <strong>Welcome, <a href="member.php?u=53106">gep13</a>.</strong><br />
            var pattern = string.Format("<strong>Welcome, <a href=\"member.php{0}u=(.*)\"", Regex.Escape("?"));

            var rx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var matches = rx.Matches(responseString);
            var memberId = 0;

            if (matches.Count != 1)
            {
                memberId = -1;
            }
            else
            {
                if (!int.TryParse(matches[0].Groups[1].ToString(), out memberId))
                {
                    memberId = -1;
                }
            }
            
            return memberId;
        }
    }
}