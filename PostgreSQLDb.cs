using System;

namespace dbup_test
{
    public static class PostgreSQLDb
    {
        public static void WithScript()
        {
            throw new NotImplementedException();

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
            builder..WithScriptsAndCodeEmbeddedInAssembly(Assembly)
            */
        }
    }
}