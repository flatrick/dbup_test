using System;

namespace dbup_test.migrations
{
    public class Script202206190023_code : DbUp.Engine.IScript
    {
        public string ProvideScript(Func<System.Data.IDbCommand> commandFactory)
        {
            // https://dbup.readthedocs.io/en/latest/usage/
            // https://github.com/DbUp/DbUp/blob/master/src/Samples/SampleApplication/Program.cs
            // https://github.com/DbUp/DbUp/blob/master/src/Samples/SampleApplication/Scripts/Script0005%20-%20ComplexUpdate.cs

            // If you have something that requires logic to update, it is sometimes easier doing that in code rather than sql.
            // By creating a code script, you get an open connection and you can build the sql script on the fly at the time of execution
            // 
            // The ProvideScript method will be called when it is THIS scripts turn to be executed, so the scripts before have already been executed

            // Example
            //var cmd = sqlConnectionString.CreateCommand();
            //cmd.CommandText = "Select * from SomeTable";
            //var scriptBuilder = new StringBuilder();

            //using (var reader = cmd.ExecuteReader())
            //{
            //    while (reader.Read())
            //    {
            //        scriptBuilder.AppendLine(string.Format("insert into AnotherTable values ({0})", reader.GetString(0)));
            //    }
            //}

            //return scriptBuilder;

            return "select 1";
        }
    }
}