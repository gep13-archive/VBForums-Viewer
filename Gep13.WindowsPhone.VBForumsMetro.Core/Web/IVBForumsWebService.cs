//-----------------------------------------------------------------------
// <copyright file="IVBForumsWebService.cs" company="GEP13">
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
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Gep13.WindowsPhone.VBForumsMetro.Models;

    /// <summary>
    /// The VBForumsWebService interface.
    /// </summary>
    public interface IVBForumsWebService
    {
        /// <summary>
        /// The is valid login credential.
        /// </summary>
        /// <param name="loginCredential">The login credential.</param>
        /// <returns>True if valid credentials, otherwise false.</returns>
        Task<bool> IsValidLoginCredential(LoginCredentialModel loginCredential);

        /// <summary>
        /// The get reputation entries for user.
        /// </summary>
        /// <param name="loginCredential">The login credential.</param>
        /// <returns>The System.Collections.Generic.IEnumerable`1[T -&gt; Gep13.WindowsPhone.VBForumsMetro.Models.ReputationModel].</returns>
        Task<IEnumerable<ReputationModel>> GetReputationEntriesForUser(LoginCredentialModel loginCredential);

        /// <summary>
        /// The get profile for user.
        /// </summary>
        /// <param name="memberId">The member id for the profile.</param>
        /// <param name="loginCredential">The login credential.</param>
        /// <returns>The System.Threading.Tasks.Task`1[TResult -&gt; Gep13.WindowsPhone.VBForumsMetro.Models.ProfileModel].</returns>
        Task<ProfileModel> GetProfileForUser(int memberId, LoginCredentialModel loginCredential);

        /// <summary>
        /// The get member id for user.
        /// </summary>
        /// <param name="loginCredential">The login credential.</param>
        /// <returns>The System.Threading.Tasks.Task`1[TResult -&gt; System.Int32].</returns>
        Task<int> GetMemberIdForUser(LoginCredentialModel loginCredential);
    }   
}