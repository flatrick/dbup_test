using System;
using Microsoft.Extensions.Configuration;

namespace dbup_test;

public class Program
{
    static void Main()
    {
        SQLiteDbExamples();
        PostgresqlDbExamples(GetPostgresConnectionString());
    }

    private static void SQLiteDbExamples()
    {
        SQLiteDb.InMemorySQLiteDatabase_WithScriptsEmbeddedInAssembly();
        SQLiteDb.TemporarySQLiteDatabase_WithScriptsEmbeddedInAssemblies();
        SQLiteDb.SharedConnection_WithScriptsFromFileSystem();

        SQLiteDb.SharedConnection_ScriptType_RunAlways();
        SQLiteDb.InMemorySQLiteDatabase_ScriptType_RunOnce();
    }

    private static void PostgresqlDbExamples(string connString)
    {
        var postgres = new PostgreSQLDb(connString);
        postgres.WithScript();
        postgres.WithScripts();
        postgres.WithScriptsCustomScriptProvider();
        postgres.WithScriptsAndCodeEmbeddedInAssembly();
        postgres.WithScripts_WithoutTransaction();
        postgres.WithScripts_WithTransactionPerScript();
        postgres.WithScripts_WithTransaction();
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
    
    /// <summary>
    /// Helper-method to deal with reading and returning the connection-string for PostgreSQL in AppSettings.json
    /// </summary>
    /// <returns>A connectionstring in the form of a string</returns>
    private static string GetPostgresConnectionString()
    {
        IConfigurationRoot config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

        return config.GetConnectionString("postgres")!;
    }
}

