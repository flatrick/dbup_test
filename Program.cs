using System;

namespace SQLiteSampleApplication
{
    public static class Program
    {
        static void Main()
        {
            InMemoryDb();
            TemporaryFileDb();
            PermanentFileDb();
        }

        static void InMemoryDb()
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

                var result = upgrader.PerformUpgrade();
                watch.Stop();

                Display("InMemory", result, watch.Elapsed);
            } // The database will be removed from memory at the end of the using-clause
        }

        static void TemporaryFileDb()
        {
            using (var database = new DbUp.SQLite.Helpers.TemporarySQLiteDatabase("test.db"))
            {
                var upgrader =
                    DbUp.DeployChanges.To
                        .SQLiteDatabase(database.SharedConnection)
                        .WithScriptsEmbeddedInAssembly(System.Reflection.Assembly.GetExecutingAssembly())
                        .LogToConsole()
                        .Build();

                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();

                var result = upgrader.PerformUpgrade();
                watch.Stop();

                Display("Temporary file", result, watch.Elapsed);

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

        static void PermanentFileDb()
        {
            Microsoft.Data.Sqlite.SqliteConnection connection = new("Data Source=dbup.db");

            using (var database = new DbUp.SQLite.Helpers.SharedConnection(connection))
            {
                var upgrader = DbUp.DeployChanges
                    .To
                    .SQLiteDatabase(connection.ConnectionString)
                    .WithScriptsEmbeddedInAssembly(System.Reflection.Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();

                DbUp.Engine.DatabaseUpgradeResult result = upgrader.PerformUpgrade();
                watch.Stop();

                Display("Permanent file", result, watch.Elapsed);
            } // The file will NOT be removed after the using-clause
        }

        static void Display(string dbType, DbUp.Engine.DatabaseUpgradeResult result, TimeSpan ts)
        {
            // Display the result
            if (result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success!");
                Console.WriteLine(
                    "{0} Database Upgrade Runtime: {1}",
                    dbType,
                    string.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10));
                Console.ReadKey();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ReadKey();
                Console.WriteLine("Failed!");
            }
        }
    }
}