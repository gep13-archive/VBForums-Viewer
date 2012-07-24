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
    using System.IO;
    using System.Net;
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
            var uri = new Uri(string.Format("http://www.vbforums.com/member.php?u={0}", memberId));

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

            // Join Date:
            // <li><span class="shade">Join Date:</span> Nov 16th, 2004</li>
            // 
            // Posts Per Day:
            // <li><span class="shade">Posts Per Day:</span> 7.61</li>
            //
            // Total Posts:
            // <li><span class="shade">Total Posts:</span> 21,358</li>
            //
            // Profile Pic:
            // <td id="profilepic_cell" class="tborder alt2"><img src="image.php?u=53106&amp;dateline=1277553514&amp;type=profile"  width="64" height="64"  alt="gep13's Profile Picture" /></td>            //
            // User Name:
            // <strong>Welcome, <a href="member.php?u=53106">gep13</a>.</strong><br />
            //
            // Custom User Title:
            // <td valign="top" width="100%" id="username_box" class="profilepic_adjacent">            //   <div id="reputation_rank">            //     <div id="reputation">            //       <img class="inlineimg" src="images/reputation/reputation_pos.gif" alt="gep13 has much to be proud of (1500+)" border="0" />            //       <img class="inlineimg" src="images/reputation/reputation_pos.gif" alt="gep13 has much to be proud of (1500+)" border="0" />            //       <img class="inlineimg" src="images/reputation/reputation_pos.gif" alt="gep13 has much to be proud of (1500+)" border="0" />            //       <img class="inlineimg" src="images/reputation/reputation_pos.gif" alt="gep13 has much to be proud of (1500+)" border="0" />            //       <img class="inlineimg" src="images/reputation/reputation_pos.gif" alt="gep13 has much to be proud of (1500+)" border="0" />            //       <img class="inlineimg" src="images/reputation/reputation_highpos.gif" alt="gep13 has much to be proud of (1500+)" border="0" />            //       <img class="inlineimg" src="images/reputation/reputation_highpos.gif" alt="gep13 has much to be proud of (1500+)" border="0" />            //       <img class="inlineimg" src="images/reputation/reputation_highpos.gif" alt="gep13 has much to be proud of (1500+)" border="0" />            //       <img class="inlineimg" src="images/reputation/reputation_highpos.gif" alt="gep13 has much to be proud of (1500+)" border="0" />            //       <img class="inlineimg" src="images/reputation/reputation_highpos.gif" alt="gep13 has much to be proud of (1500+)" border="0" />            //       <img class="inlineimg" src="images/reputation/reputation_highpos.gif" alt="gep13 has much to be proud of (1500+)" border="0" />            //     </div>            //   </div>            //   <h1><font color=#FF0000>gep13</font>            //     <img class="inlineimg" src="images/statusicon/user_online.gif" alt="gep13 is online now" border="0" />            //   </h1>            //   <h2><b><font color=blue>ASP.NET</font> <font color="darkgreen">Moderator</font></b></h2>            // </td>
            return null;
        }

        /// <summary>
        /// The get member id for user.
        /// </summary>
        /// <param name="loginCredential">The login credential.</param>
        /// <returns>The System.Threading.Tasks.Task`1[TResult -&gt; System.Int32].</returns>
        public async Task<int> GetMemberIdForUser(LoginCredentialModel loginCredential)
        {
            var uri = new Uri("http://www.vbforums.com/usercp.php");

            var request = WebRequest.CreateHttp(uri);
            request.Method = "GET";
            request.Credentials = new NetworkCredential(loginCredential.UserName, loginCredential.Password);

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

            // Need to figure out how to parse out the following:
            // <strong>Welcome, <a href="member.php?u=53106">gep13</a>.</strong><br />
            return 1;
        }
    }
}