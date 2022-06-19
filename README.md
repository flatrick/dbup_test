# Readme

This project is merely a placeholder for my attempt to learn how to work with [DbUp](https://github.com/dbup/dbup).
As I find their documentation a bit lacking, I'm hoping to be able to add to it through my attempts of learning the library with this repo.

## Database specific information

### SQLite

- [dbup-sqlite](https://github.com/DbUp/DbUp/tree/master/src/dbup-sqlite)
- [SQLiteSampleApplication](https://github.com/DbUp/DbUp/tree/master/src/Samples/SQLiteSampleApplication)

```posh
dotnet new console
dotnet add package Microsoft.Data.Sqlite
dotnet add package DbUp-sqlite
```

### PostgreSQL

- [dbup-postgresql](https://github.com/DbUp/DbUp/tree/master/src/dbup-postgresql)
- [How to create a postgresql database with versioning?](https://thecodereaper.com/2020/10/27/how-to-create-a-postgresql-database-with-versioning/)

```posh
dotnet new console
dotnet add package Npgsql
dotnet add package DbUp-postgresql
```

## Ways of storing and executing migrations

### In code

### As EmbeddedResource in .csproj

Add the following between `<Project>...</Project>`

```xml
<ItemGroup>
  <EmbeddedResource Include="migrations/*.sql" />
</ItemGroup>
```

You can also add each as a separate `<EmbeddedResource>` row if that is preferred over a wildcard like the example above.
