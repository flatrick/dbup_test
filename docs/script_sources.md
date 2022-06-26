# Script sources

- [Encoding - Which encoding the files are read with](https://github.com/DbUp/DbUp/blob/master/src/dbup-core/DbUpDefaults.cs)
- [filter / codeScriptFilter - Filter whichs scripts to be executed (when used, also remember to filter out non-.sql files)](https://github.com/DbUp/DbUp/blob/master/src/dbup-core/Builder/StandardExtensions.cs)
- [IScript - A class which represents a script, allowing you to dynamically generate a sql script at runtime](https://github.com/DbUp/DbUp/blob/master/src/dbup-core/Engine/IScript.cs)
- [IScriptProvider - Provides scripts to be executed](https://github.com/DbUp/DbUp/blob/master/src/dbup-core/Engine/IScriptProvider.cs)
- [SqlScript - Represents a script that comes from some source, e.g. an embedded resource in an assembly](https://github.com/DbUp/DbUp/blob/master/src/dbup-core/Engine/SqlScript.cs)
- [SqlScriptOptions - allow you to set various options for the SQL Script Model and any child models](https://github.com/DbUp/DbUp/blob/master/src/dbup-core/Engine/SqlScriptOptions.cs)

- [FileSystemScriptProvider - Alternate IScriptProvider implementation which retrieves upgrade scripts via a directory](https://github.com/DbUp/DbUp/blob/master/src/dbup-core/ScriptProviders/FileSystemScriptProvider.cs)
- [FileSystemScriptOptions - Options for FileSystemScriptProvider](https://github.com/DbUp/DbUp/blob/master/src/dbup-core/ScriptProviders/FileSystemScriptOptions.cs)

## Single script through code

```csharp
WithScript(SqlScript script);
WithScript(string name, string contents);
WithScript(string name, string contents, SqlScriptOptions sqlScriptOptions);
WithScript(string name, IScript script);
WithScript(string name, IScript script, SqlScriptOptions sqlScriptOptions);
```

## Multiple scripts through code

```csharp
WithScripts(params IScript[] scripts);
WithScripts(Func<IScript, string> namer, params IScript[] scripts);
WithScripts(params SqlScript[] scripts);
WithScripts(IEnumerable<SqlScript> scripts);
WithScripts(IScriptProvider scriptProvider);
WithScripts(Func<IScript, string> namer, SqlScriptOptions sqlScriptOptions, params IScript[] scripts);
```

## Multiple scripts from embedded files from a single assembly

```csharp
WithScriptsEmbeddedInAssemblies(Assembly[] assemblies, Encoding encoding);
WithScriptsEmbeddedInAssemblies(Assembly[] assemblies, Func<string, bool> filter, SqlScriptOptions sqlScriptOptions);
WithScriptsEmbeddedInAssemblies(Assembly[] assemblies, Func<string, bool> filter);
WithScriptsEmbeddedInAssemblies(Assembly[] assemblies);
WithScriptsEmbeddedInAssemblies(Assembly[] assemblies, Func<string, bool> filter, Encoding encoding, SqlScriptOptions sqlScriptOptions);
WithScriptsEmbeddedInAssemblies(Assembly[] assemblies, Func<string, bool> filter, Encoding encoding);
```

## Multiple scripts from embedded files from multiple assemblies

```csharp
WithScriptsEmbeddedInAssembly(Assembly assembly, Func<string, bool> filter);
WithScriptsEmbeddedInAssembly(Assembly assembly, Func<string, bool> filter, Encoding encoding, SqlScriptOptions sqlScriptOptions);
WithScriptsEmbeddedInAssembly(Assembly assembly, SqlScriptOptions sqlScriptOptions);
WithScriptsEmbeddedInAssembly(Assembly assembly);
WithScriptsEmbeddedInAssembly(Assembly assembly, Encoding encoding, SqlScriptOptions sqlScriptOptions);
WithScriptsEmbeddedInAssembly(Assembly assembly, Func<string, bool> filter, Encoding encoding);
WithScriptsEmbeddedInAssembly(Assembly assembly, Func<string, bool> filter, SqlScriptOptions sqlScriptOptions);
WithScriptsEmbeddedInAssembly(Assembly assembly, Encoding encoding);
```

## Multiple scripts through code and embedded files from a single assembly

```csharp
WithScriptsAndCodeEmbeddedInAssembly(Assembly assembly, SqlScriptOptions sqlScriptOptions);
WithScriptsAndCodeEmbeddedInAssembly(Assembly assembly, Func<string, bool> filter, SqlScriptOptions sqlScriptOptions);
WithScriptsAndCodeEmbeddedInAssembly(Assembly assembly, Func<string, bool> filter, Func<string, bool> codeScriptFilter, SqlScriptOptions sqlScriptOptions);
WithScriptsAndCodeEmbeddedInAssembly(Assembly assembly, Func<string, bool> filter, Func<string, bool> codeScriptFilter);
WithScriptsAndCodeEmbeddedInAssembly(Assembly assembly, Func<string, bool> filter);
WithScriptsAndCodeEmbeddedInAssembly(Assembly assembly);
```

1. If `codeScriptFilter` isn't supplied for `WithScriptsAndCodeEmbeddedInAssembly()`
   1. `filter` will be used to filter all scripts (i.e. both code and the embedded files from the assembly)
   2. But `filter` is mandatory if any filtering is to be used as there is no method-signature for only `codeScriptFilter`
2. 

## Multiple scripts from specified file path

```csharp
WithScriptsFromFileSystem(string path);
WithScriptsFromFileSystem(string path, SqlScriptOptions sqlScriptOptions);
WithScriptsFromFileSystem(string path, Func<string, bool> filter);
WithScriptsFromFileSystem(string path, Func<string, bool> filter, SqlScriptOptions sqlScriptOptions);
WithScriptsFromFileSystem(string path, Encoding encoding);
WithScriptsFromFileSystem(string path, Encoding encoding, SqlScriptOptions sqlScriptOptions);
WithScriptsFromFileSystem(string path, Func<string, bool> filter, Encoding encoding);
WithScriptsFromFileSystem(string path, Func<string, bool> filter, Encoding encoding, SqlScriptOptions sqlScriptOptions);
WithScriptsFromFileSystem(string path, FileSystemScriptOptions options);
WithScriptsFromFileSystem(string path, FileSystemScriptOptions options, SqlScriptOptions sqlScriptOptions);
```