namespace dbup_test
{
    public static class SQLiteDb
    {
        /// <summary>WithScriptsEmbeddedInAssembly() depends on the <EmbeddedResource> in the .csproj</summary>
        public static void InMemorySQLiteDatabase_WithScriptsEmbeddedInAssembly()
        {
            using (var database = new DbUp.SQLite.Helpers.InMemorySQLiteDatabase())
            {
                var upgrader =
                    DbUp.DeployChanges.To
                        .SQLiteDatabase(database.ConnectionString)
                        .WithScriptsEmbeddedInAssembly(System.Reflection.Assembly.GetExecutingAssembly())
                        .LogToConsole()
                        .Build();

                var watch = new System.Diagnostics.Stopwatch();

                watch.Start();
                DbUp.Engine.DatabaseUpgradeResult result = upgrader.PerformUpgrade();
                watch.Stop();

                Program.Display("InMemory", result, watch.Elapsed);
            } // The database will be removed from memory at the end of the using-clause
        }

        /// <summary>WithScriptsEmbeddedInAssemblies() depends on the <EmbeddedResource> in the .csproj</summary>
        public static void TemporarySQLiteDatabase_WithScriptsEmbeddedInAssemblies()
        {
            using (var database = new DbUp.SQLite.Helpers.TemporarySQLiteDatabase("test.db"))
            {
                var upgrader =
                    DbUp.DeployChanges.To
                        .SQLiteDatabase(database.SharedConnection)
                        .WithScriptsEmbeddedInAssemblies(new[] { System.Reflection.Assembly.GetExecutingAssembly() })
                        .LogToConsole()
                        .Build();

                var watch = new System.Diagnostics.Stopwatch();

                watch.Start();
                DbUp.Engine.DatabaseUpgradeResult result = upgrader.PerformUpgrade();
                watch.Stop();

                Program.Display("Temporary file", result, watch.Elapsed);

                /* 
                Because of a change with dotNet 6.0, all SQLite connections are pooled.
                This means that there will continue to exist a lock on the file, even after Dispose() has been called when running on Windows.
                This doesn't occur on Linux because of differences in filesystem operations.
                A solution is to explicitly tell the SqliteConnection-library to clear all pooled connections before Dispose() is called.
                https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-6.0/breaking-changes#connection-pool
                */
                Microsoft.Data.Sqlite.SqliteConnection.ClearAllPools();
            } // The database-file is deleted at the end of the using-clause
        }

        /// <summary>WithScriptsFromFileSystem() depends on <EmbeddedResource> AND the defined <Target></summary>
        public static void SharedConnection_WithScriptsFromFileSystem()
        {
            Microsoft.Data.Sqlite.SqliteConnection connection = new("Data Source=dbup.db");

            using (var database = new DbUp.SQLite.Helpers.SharedConnection(connection))
            {
                var upgrader = DbUp.DeployChanges
                    .To
                    .SQLiteDatabase(connection.ConnectionString)
                    .WithScriptsFromFileSystem(System.AppDomain.CurrentDomain.BaseDirectory)
                    .LogToConsole()
                    .Build();

                var watch = new System.Diagnostics.Stopwatch();

                watch.Start();
                DbUp.Engine.DatabaseUpgradeResult result = upgrader.PerformUpgrade();
                watch.Stop();

                Program.Display("Permanent file", result, watch.Elapsed);
            } // The file will NOT be removed after the using-clause
        }
    }
}