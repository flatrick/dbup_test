using System;
using System.Diagnostics;
using System.Reflection;
using DbUp;
using DbUp.Engine;
using Microsoft.Data.Sqlite;

namespace SQLiteSampleApplication
{
    public static class Program
    {
        static void Main()
        {
            SqliteConnection connection = new("Data Source=dbup.db");
            PermanentSQLiteDb(connection);
            TemporarySQLiteDb();
            InMemorySQLiteDb();
        }

        static void PermanentSQLiteDb(SqliteConnection connection)
        {
            using (var database = new DbUp.SQLite.Helpers.SharedConnection(connection))
            {
                var upgrader = DeployChanges
                    .To
                    .SQLiteDatabase(connection.ConnectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

                var watch = new Stopwatch();
                watch.Start();

                var result = upgrader.PerformUpgrade();
                watch.Stop();

                Display("Permanent file", result, watch.Elapsed);
            }
        }

        static void TemporarySQLiteDb()
        {
            using (var database = new DbUp.SQLite.Helpers.TemporarySQLiteDatabase("test"))
            {
                database.Create();

                var upgrader =
                    DeployChanges.To
                    .SQLiteDatabase(database.SharedConnection)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

                var watch = new Stopwatch();
                watch.Start();

                var result = upgrader.PerformUpgrade();
                watch.Stop();

                Display("Temporary file", result, watch.Elapsed);
            } // Database will be deleted at this point
        }

        static void InMemorySQLiteDb()
        {
            using (var database = new DbUp.SQLite.Helpers.InMemorySQLiteDatabase())
            {
                var upgrader =
                    DeployChanges.To
                    .SQLiteDatabase(database.ConnectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

                var watch = new Stopwatch();
                watch.Start();

                var result = upgrader.PerformUpgrade();
                watch.Stop();

                Display("InMemory", result, watch.Elapsed);
            } // Database will disappear from memory at this point
        }

        static void Display(string dbType, DatabaseUpgradeResult result, TimeSpan ts)
        {
            // Display the result
            if (result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success!");
                Console.WriteLine("{0} Database Upgrade Runtime: {1}", dbType,
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