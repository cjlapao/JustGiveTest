using System;
using System.Collections.Generic;
using System.Text;
using JG.FinTechTest.Models;
using JG.FinTechTest.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JG.FinTechTest.Tests
{
    /// <summary>
    ///  We will use this class to mock a database for our tests
    /// </summary>
    public class MemoryRepository
    {
        public MemoryRepository()
            : this("Add_writes_To_Database")
        {
        }

        public MemoryRepository(string databaseName)
        {
            DatabaseName = databaseName;
            var options = new DbContextOptionsBuilder<JGFinTechTestContext>()
                .UseInMemoryDatabase(DatabaseName)
                .Options;

            Context = new JGFinTechTestContext(options);
        }

        public List<GiftAidDeclaration> DbAppUsers { get; } = new List<GiftAidDeclaration>();

        public JGFinTechTestContext Context { get; set; }

        public string DatabaseName { get; set; }

        public void Dispose()
        {
            Context?.Dispose();
        }

        /// <summary>
        /// Creates a memory repository with a given name
        /// </summary>
        /// <param name="database">Database name</param>
        /// <returns><see cref="MemoryRepository"/></returns>
        public static MemoryRepository Create(string database)
        {
            return new MemoryRepository(database);
        }
    }
}
