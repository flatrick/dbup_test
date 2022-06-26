using System;

namespace dbup_test
{
    public static class PostgreSQLDb
    {
        public static void WithScript()
        {
            const string connectionString = "Host=localhost; Port=2345; Database=mydbc; Username=myuser; Password=mypassword;";

            // Verify that the database exists, if not, create it
            DbUp.EnsureDatabase.For.PostgresqlDatabase(connectionString);

            /* DropDatabase isn't implemented for PostgreSQL in DbUp */
            // DbUp.DropDatabase.For.

            // Configure DbUp.Engine.UpgradeEngine
            var upgrader = DbUp.DeployChanges.To
                .PostgresqlDatabase(connectionString)
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
        public static void WithScripts()
        {
            throw new NotImplementedException();
            /* DbUp.DeployChanges.WithScripts(new [] {
                new SqlScript ("name.sql", "content"),
                new SqlScript ("name.sql", "content")
                });
            */
        }

        public static void WithScriptsCustomScriptProvider()
        {
            throw new NotImplementedException();
            /*
            builder.WithScripts(new MyCustomScriptProvider());
            */
        }

        public static void WithScriptsAndCodeEmbeddedInAssembly()
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

        public static void WithScripts_WithoutTransaction()
        {
            throw new NotImplementedException();
        }

        public static void WithScripts_WithTransactionPerScript()
        {
            throw new NotImplementedException();
        }

        public static void WithScripts_WithTransaction()
        {
            throw new NotImplementedException();
        }

        #endregion Transactions        
    }
}