# First setup of DbUp

First, let's begin by creating a new console application and adding DbUp to the project:

```posh
dotnet new console
dotnet add package Microsoft.Data.Sqlite
dotnet add package DbUp-sqlite
```

*Note: I chose to use SQLite in this example as it is the easiest to get started with.*

## Folder for storing SQL scripts

For this first example, we will store our SQL-scripts in a sub-folder named `migrations` that will be copied to the output-folder when we compile, so go ahead and create a folder with that name in your new project-folder.

## MSBuild options (csproj)

Open your `<project>.csproj` file and make sure whatever we put in this folder gets copied during compilation.
Add the following somewhere between the `<Project>...</Project>` tags:

```xml
<ItemGroup>
  <EmbeddedResource Include="migrations/*.sql" />
</ItemGroup>
```

*Note: you can also add a single line for every file you wish to have copied inside this `<ItemGroup>` as separate `<EmbeddedResource>` rows instead of using a wildcard like I do in my example above. As this is an example, I will try to keep things as easy as possible.*

## Source code

*Note: As we're using SQLite, DbUp comes with Helpers to assist with certain unique features of SQLite versus other RDBMS systems.*
*For the sake of this example, we'll be using an In-Memory database that won't store any data on disk when the application exits.*

```csharp
using System;

namespace dbup_example
{
    public static class Program
    {
        static void Main()
        {
            using (var database = new DbUp.SQLite.Helpers.InMemorySQLiteDatabase())
            {
                // 1. configure DbUp to deploy to our SQLite in-memory database
                // 2. using scripts embedded in this assembly
                // 3. log all events to the console screen
                // 4. build the UpgradeEngine-object needed
                DbUp.Engine.UpgradeEngine upgrader = DbUp.DeployChanges.To
                                                 .SQLiteDatabase(database.ConnectionString)
                                                 .WithScriptsEmbeddedInAssembly(System.Reflection.Assembly.GetExecutingAssembly())
                                                 .LogToConsole()
                                                 .Build();

                // Here we perform the actual upgrade using our UpgradeEngine-object and store the result in a variable
                DbUp.Engine.DatabaseUpgradeResult result = upgrader.PerformUpgrade();

                Program.Display("SQLite - InMemory:", result);
            } // The database will be removed from memory at the end of this using-clause
        }

        public static void Display(string dbType, DbUp.Engine.DatabaseUpgradeResult result)
        {
            // Display the result
            if (result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success!");
                Console.WriteLine("{0} Database Upgrade", dbType);
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
```

And now that we have done the preliminary work of setting everything up, we can start adding SQL-scripts to the `migrations` folder.

By default, DbUp will begin by collecting a list of all scripts to execute and sort them using [StringComparer.Ordinal](https://docs.microsoft.com/en-us/dotnet/api/system.stringcomparer.ordinal?view=net-6.0) before executing them.
In other words, the filenames of your scripts will dictate which order they are executed in.
For this example, we'll use a date and time-string to sort them in the desired order of oldest to newest.

Create two files named `202206262000` and `202206262100` in the folder `migrations`:

```sql
--202206262000
CREATE TABLE 'Employees' (
    'Id' INTEGER PRIMARY KEY,
    'EmployeeName' TEXT NOT NULL
);
```

```sql
--202206262100
CREATE TABLE 'EmployeeRoles' (
    'Id' INTEGER PRIMARY KEY,
    'EmployeeRoleName' TEXT NOT NULL
);
```

Whenever you build and run this application, these two scripts will now be executed on the in-memory database.
In production you will most likely not be using temporary in-memory databases, but the basics for how this works should now be clear.