//-----------------------------------------------------------------------
// <copyright file="VBForumsMetroDB.cs" company="GEP13">
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

namespace Gep13.WindowsPhone.VBForumsMetro.Core.Model
{
    using System.Collections.Generic;

    using Wintellect.Sterling.Database;

    /// <summary>
    /// The definition of the database that will be used by the VBForumsMetro application
    /// </summary>
    public class VBForumsMetroDB : BaseDatabaseInstance
    {
        /// <summary>
        /// Gets the name of the Database
        /// </summary>
        public override string Name
        {
            get
            {
                return "VBForumsMetroDB";
            }
        }

        /// <summary>
        /// Register all the tables that need to exist within this database
        /// </summary>
        /// <returns>
        /// A list of the all the table definitions for the database
        /// </returns>
        protected override List<ITableDefinition> RegisterTables()
        {
            return new List<ITableDefinition>()
            {
                // TODO: Need to create the entities that are going to be used here
                ////CreateTableDefinition<ServerCrendentials, Guid>(
                ////   k => k.ServerCredentialsID)
            };
        }
    }
}