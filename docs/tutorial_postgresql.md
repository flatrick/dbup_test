# Console application with DbUp and PostgreSQL

## Step 1: create the project and add the necessary packages

Run the following commands to create a new console application that will use [PostgreSQL](https://www.postgresql.org/) as the database-engine:

```posh
dotnet new console
dotnet add package Npgsql
dotnet add package DbUp-Core
dotnet add package DbUp-postgresql
```

## Step 2: Create the folder for storing the SQL scripts

We will store our SQL-scripts in a sub-folder named `migrations` that will be copied to the output-folder when we compile, so go ahead and create a folder with that name in your new project-folder.

## Step 3: MSBuild options (csproj) to copy the SQL-scripts to the output folder

To make sure whatever we put in the folder `migrations` gets copied during compilation, we need to edit the `<project>.csproj` file.
Add the following somewhere between the `<Project>...</Project>` tags:

```xml
<ItemGroup>
  <EmbeddedResource Include="migrations/*.sql" />
</ItemGroup>
```

*Note: you can also add a single line for every file you wish to have copied inside this `<ItemGroup>` as separate `<EmbeddedResource>` rows instead of using a wildcard like I do in my example above. As this is an example, I elected to try and keep things as easy as possible.*


## Step 4: Source code

Below is an example of a simple source code that connects to the PostgreSQL-database and runs the .sql-scripts found in the folder `migrations/`.
*Take note that we aren't using any helper-methods like in the SQLite-example as the database is being handled by a separate process.*

```csharp
using System;

namespace dbup_example
{
    public static class Program
    {
        static void Main()
        {
            const string connectionString = "Host=localhost; Port=5432; Database=mydb; Username=myuser; Password=mypassword;";
            // 1. configure DbUp to deploy to our PostgreSQL database
            // 2. using scripts embedded in this assembly
            // 3. log all events to the console screen
            // 4. build the UpgradeEngine-object needed
            DbUp.Engine.UpgradeEngine upgrader = 
                DbUp.DeployChanges.To
                    .PostgresqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(System.Reflection.Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            // Here we perform the actual upgrade using our UpgradeEngine-object and store the result in a variable
            DbUp.Engine.DatabaseUpgradeResult result = upgrader.PerformUpgrade();

            if (result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("The upgrade was successful!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.WriteLine("Failed!");
            }
            Console.ReadKey();
        }
    }
}
```

## Step 5: SQL scripts

Create two files named `202206262000.sql` and `202206262100.sql` in the folder `migrations`:

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

## Step 6: Build and run!

Whenever you build and run this application, these two scripts will now be executed on the PostgreSQL-database.