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

    using Microsoft.Phone.Reactive;

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
        public async Task<IEnumerable<ReputationModel>> GetReputationEntriesForUser(Models.LoginCredentialModel loginCredential)
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
                        "<tbody id=\"collapseobj_usercp_reputation\"", System.StringComparison.Ordinal));

            repTable = repTable.Substring(repTable.IndexOf("<tr>", System.StringComparison.Ordinal));
            repTable = repTable.Substring(0, repTable.IndexOf("</tbody>", System.StringComparison.Ordinal) - 1);
            repTable = repTable.Replace("\n", " ").Replace("\r", " ").Replace("\t", " ");
            while (repTable.IndexOf("  ", System.StringComparison.Ordinal) > 0)
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
    }
}