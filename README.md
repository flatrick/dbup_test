# Readme

This project is merely a placeholder for my attempt to learn how to work with [DbUp](https://github.com/dbup/dbup).
As I find their documentation a bit lacking, I'm hoping to be able to add to it through my attempts of learning the library with this repo.

## Database specific information

### SQLite

- [dbup-sqlite](https://github.com/DbUp/DbUp/tree/master/src/dbup-sqlite)
- [SQLiteSampleApplication](https://github.com/DbUp/DbUp/tree/master/src/Samples/SQLiteSampleApplication)

```powershell
dotnet new console
dotnet add package Microsoft.Data.Sqlite
dotnet add package DbUp-core
dotnet add package DbUp-sqlite
```

### PostgreSQL

- [dbup-postgresql](https://github.com/DbUp/DbUp/tree/master/src/dbup-postgresql)
- [How to create a postgresql database with versioning?](https://thecodereaper.com/2020/10/27/how-to-create-a-postgresql-database-with-versioning/)

```powershell
dotnet new console
dotnet add package Npgsql
dotnet add package DbUp-core
dotnet add package DbUp-postgresql
```

## Use a json-file to store the connection-string for PostgreSQL

Configuration shouldn't be stored in the source-code, so to not spread bad behaviours, this sample-project uses `Microsoft.Extensions.Configuration` to read from a JSON-file.

```powershell
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.Json
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

You can also add each script as separate `<EmbeddedResource>` rows if that is preferred over a wildcard like the example above.

## Stop git from tracking changes to your local settings-files

These two files are meant to be changed according to each user's desire/needs, so to stop git from tracking changes to them on your machine, run these commands:

```powershell
git update-index --skip-worktree appsettings.json
git update-index --skip-worktree .env
```

## Steps for updating the mkdocs installation

These steps are written based on:
- The user is running on Windows
- The user has installed Python 3.x
- The user has not created a virtual environment (venv) yet
- The three dependencies are defined in the file `requirements.in` (without a version to make it install the latest version)

```powershell
python -m venv docs
cd docs
.\Scripts\Activate.ps1
python -m pip install pip-tools mkdocs mkdocstrings[python] markdown-include
pip-compile requirements.in
```