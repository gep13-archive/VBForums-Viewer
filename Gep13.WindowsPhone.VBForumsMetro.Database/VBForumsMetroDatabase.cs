//-----------------------------------------------------------------------
// <copyright file="VBForumsMetroDatabase.cs" company="GEP13">
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
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using Gep13.WindowsPhone.VBForumsMetro.Models;
    using Wintellect.Sterling;
    using Wintellect.Sterling.Database;

    /// <summary>
    /// Set up for the VBForumsMetro Sterling database
    /// </summary>
    public class VBForumsMetroDatabase : BaseDatabaseInstance
    {
        /// <summary>
        /// Database name
        /// </summary>
        public const string VBForumsMetroDatabaseName = "VBForumsMetro";

        /// <summary>
        ///     Signature for a parser
        /// </summary>
        /// <typeparam name="T">Type to parse</typeparam>
        /// <param name="parts">Split line</param>
        /// <returns>Constructed entity</returns>
        // ReSharper disable TypeParameterCanBeVariant
        private delegate T Parser<T>(string[] parts);
        // ReSharper restore TypeParameterCanBeVariant

        /// <summary>
        /// Database name
        /// </summary>
        public override string Name
        {
            get { return VBForumsMetroDatabaseName; }
        }

        /// <summary>
        ///     Check to see if the database has been populated. If not, populate it.
        /// </summary>
        /// <param name="database">The database instance</param>
        public static void CheckAndCreate(ISterlingDatabaseInstance database)
        {
            // register the triggers
            database.RegisterTrigger(new IdentityTrigger<ReputationModel>(database));
            database.RegisterTrigger(new IdentityTrigger<LoginCredentialModel>(database));
            database.RegisterTrigger(new IdentityTrigger<ProfileModel>(database));

            // if any are here we've already set things up because users can't delete these
            if (database.Query<ReputationModel, int>().Any())
            {
                return;
            }

            // get rid of old data
            database.Truncate(typeof(ReputationModel));
            database.Truncate(typeof(LoginCredentialModel));

            database.Save(new LoginCredentialModel() { UserName = "gep31", Password = "qwerty" });
            database.Save(
                new ProfileModel()
                    {
                        UserName = "gep13",
                        CustomUserTitle = "ASP.NET Moderator",
                        JoinDate = new DateTime(2004, 11, 16),
                        Posts = 21352,
                        PostsPerDay = 7.6
                    });
            database.Save(
                new ReputationModel()
                    {
                        ThreadName = "test1",
                        ThreadUri = new Uri("http://www.vbforums.com"),
                        AuthorName = "gep13",
                        AuthorUri = new Uri("http://www.gep13.co.uk/blog"),
                        Comment = "this is a test",
                        CreationDate = DateTime.Now
                    });
            
            database.Flush();
        }

        /// <summary>
        /// Sets up the tables and corresponding keys
        /// </summary>
        /// <returns>A List of all the TableDefinitions within the database</returns>
        protected override List<ITableDefinition> RegisterTables()
        {
            return new List<ITableDefinition>
            {
                CreateTableDefinition<ReputationModel, int>(c => c.Id),

                CreateTableDefinition<LoginCredentialModel, int>(i => i.Id),

                CreateTableDefinition<ProfileModel, int>(i => i.Id)
            };
        }

        /// <summary>
        /// Parse a list from the resource
        /// </summary>
        /// <typeparam name="T">The type to parse</typeparam>
        /// <param name="resourceName">The embedded resource</param>
        /// <param name="parser">The parser strategy</param>
        /// <returns>The list</returns>
        private static IEnumerable<T> ParseFromResource<T>(string resourceName, Parser<T> parser)
        {
            using (var stream = Application.GetResourceStream(new Uri(resourceName, UriKind.Relative)).Stream)
            {
                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (string.IsNullOrEmpty(line))
                        {
                            break;
                        }

                        yield return parser(line.Split(','));
                    }
                }
            }
        }
    }
}