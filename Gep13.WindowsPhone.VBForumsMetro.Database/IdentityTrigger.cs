//-----------------------------------------------------------------------
// <copyright file="IdentityTrigger.cs" company="GEP13">
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

namespace Gep13.WindowsPhone.VBForumsMetro.Database
{
    using System.Linq;

    using Gep13.WindowsPhone.VBForumsMetro.Models;

    using Wintellect.Sterling;
    using Wintellect.Sterling.Database;

    /// <summary>
    /// An integer based Identity which will be used to uniquely identify each object added to the database
    /// </summary>
    /// <typeparam name="T">The type that requires an identity int created</typeparam>
    public class IdentityTrigger<T> : BaseSterlingTrigger<T, int> where T : class, IBaseModel, new()
    {
        /// <summary>
        /// Internal identifier used to keep track of the next Id to be allocated
        /// </summary>
        private static int idx = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityTrigger{T}"/> class. 
        /// </summary>
        /// <param name="database">
        /// The instance of the Sterling Database that is to be assigned with an IdentityTrigger
        /// </param>
        public IdentityTrigger(ISterlingDatabaseInstance database)
        {
            if (database.Query<T, int>().Any())
            {
                idx = database.Query<T, int>().Max(key => key.Key) + 1;
            }
        }

        /// <summary>
        /// See if we need to provide an id
        /// </summary>
        /// <param name="instance">Instance to check</param>
        /// <returns>True if we're good to save</returns>
        public override bool BeforeSave(T instance)
        {
            if (instance.Id < 1)
            {
                instance.Id = idx++;
            }

            return true;
        }

        /// <summary>
        /// Simply return true, as no work is required here
        /// </summary>
        /// <param name="instance">Instance to check</param>
        public override void AfterSave(T instance)
        {
            return;
        }

        /// <summary>
        /// Simply return true, as no work is required here
        /// </summary>
        /// <param name="key">The Id of the key that needs checked</param>
        /// /// <returns>True if we're good to delete</returns>
        public override bool BeforeDelete(int key)
        {
            return true;
        }
    }
}