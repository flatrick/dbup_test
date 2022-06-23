using System;

namespace dbup_test
{
    public static class Program
    {
        static void Main()
        {
            SQLiteDb.InMemorySQLiteDatabase_WithScriptsEmbeddedInAssembly();
            SQLiteDb.TemporarySQLiteDatabase_WithScriptsEmbeddedInAssemblies();
            SQLiteDb.SharedConnection_WithScriptsFromFileSystem();
            //PostgreSQLDb.WithScript();
            //PostgreSQLDb.WithScripts();
            //PostgreSQLDb.WithScriptsCustomScriptProvider();
            //PostgreSQLDb.WithScriptsAndCodeEmbeddedInAssembly();
        }

        public static void Display(string dbType, DbUp.Engine.DatabaseUpgradeResult result, TimeSpan ts)
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