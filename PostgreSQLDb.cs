using System;

namespace dbup_test;

public class PostgreSQLDb(string connectionString)
{
    /// <summary>
    /// Hold the connection-string provided when creating the instance of this class
    /// </summary>
    private readonly string _dbConnectionString = connectionString;

    public void WithScript()
    {

        // Verify that the database exists, if not, create it
        DbUp.EnsureDatabase.For.PostgresqlDatabase(_dbConnectionString);

        /* DropDatabase isn't implemented for PostgreSQL in DbUp */
        // DbUp.DropDatabase.For.

        // Configure DbUp.Engine.UpgradeEngine
        var upgrader = DbUp.DeployChanges.To
            .PostgresqlDatabase(_dbConnectionString)
            .WithScript("possy_table_init.sql", "CREATE TABLE possy_table (Id INTEGER PRIMARY KEY, Name TEXT NOT NULL);")
            .LogToConsole()
            .Build();

        var watch = new System.Diagnostics.Stopwatch();

        watch.Start();
        DbUp.Engine.DatabaseUpgradeResult result = upgrader.PerformUpgrade();
        watch.Stop();

        Program.Display("PostgreSQL - WithScript:", result, watch.Elapsed);

        /* 
        https://dbup.readthedocs.io/en/latest/more-info/script-providers/
        DbUp.DeployChanges.WithScript("name.sql", "content");
         */
    }
    public void WithScripts()
    {
        throw new NotImplementedException();
        /* DbUp.DeployChanges.WithScripts(new [] {
            new SqlScript ("name.sql", "content"),
            new SqlScript ("name.sql", "content")
            });
        */
    }

    public void WithScriptsCustomScriptProvider()
    {
        throw new NotImplementedException();
        /*
        builder.WithScripts(new MyCustomScriptProvider());
        */
    }

    public void WithScriptsAndCodeEmbeddedInAssembly()
    {
        throw new NotImplementedException();
        /*
        builder..WithScriptsAndCodeEmbeddedInAssembly(System.Reflection.Assembly)
        
        Make use of run-group-order to showcase that as well.
        https://github.com/DbUp/DbUp/blob/master/docs/more-info/run-group-order.md
        */
    }

    #region Transactions

    /*
    https://github.com/DbUp/DbUp/blob/master/docs/more-info/transactions.md
    */

    public void WithScripts_WithoutTransaction()
    {
        throw new NotImplementedException();
    }

    public void WithScripts_WithTransactionPerScript()
    {
        throw new NotImplementedException();
    }

    public void WithScripts_WithTransaction()
    {
        throw new NotImplementedException();
    }

    #endregion Transactions        
}