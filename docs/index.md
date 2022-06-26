# Vocabulary for DbUp

Here I'll attempt to create a vocabulary with all methods and link to examples using said methods.

[Script-sources](script_sources.md)

## Script settings
### Configure sort order for list of scripts

```csharp
WithScriptNameComparer(IComparer<string> comparer);
```

```csharp
    /// <summary>
    /// Sets the comparer used to sort scripts and match script names against the log of already run scripts.
    /// The default comparer is StringComparer.Ordinal.
    /// By implementing your own comparer you can make the matching and ordering case insensitive,
    /// change how numbers are handled or support the renaming of scripts
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="comparer">The sorter.</param>
    /// <returns>
    /// The same builder
    /// </returns>
    public static UpgradeEngineBuilder WithScriptNameComparer(IComparer<string> comparer)
    {
        builder.Configure(b => b.ScriptNameComparer = new ScriptNameComparer(comparer));
        return builder;
    }
```

[StringComparer.Ordinal Property](https://docs.microsoft.com/en-us/dotnet/api/system.stringcomparer.ordinal?view=net-6.0)

### Filter as a separate option

I haven't verified this yet, but I'm guessing this extension-method can be used as a filter on all script-sources supplied before in the `.Build()` chain.


```csharp
WithFilter(IScriptFilter filter);
```

## Journal

[Journaling doc](https://github.com/DbUp/DbUp/blob/master/docs/more-info/journaling.md)

```csharp
JournalTo(IJournal journal);
JournalTo(Func<Func<IConnectionManager>, Func<IUpgradeLog>, IJournal> createJournal);
```

## Logging

```csharp
LogScriptOutput(this UpgradeEngineBuilder builder);
LogTo(IUpgradeLog log);
LogToAutodetectedLog(this UpgradeEngineBuilder builder);
LogToConsole(this UpgradeEngineBuilder builder);
LogToNowhere(this UpgradeEngineBuilder builder);
LogToTrace(this UpgradeEngineBuilder builder);
ResetConfiguredLoggers(this UpgradeEngineBuilder builder);
```

## Transaction options

```csharp
WithTransaction(this UpgradeEngineBuilder builder);
WithTransactionAlwaysRollback(this UpgradeEngineBuilder builder);
WithTransactionPerScript(this UpgradeEngineBuilder builder);
WithoutTransaction(this UpgradeEngineBuilder builder);
```

## Variable replacement

```csharp
WithVariable(string variableName, string value);
WithVariables(IDictionary<string, string> variables);
WithVariablesDisabled(this UpgradeEngineBuilder builder);
WithVariablesEnabled(this UpgradeEngineBuilder builder);
```

## Pre-processor

[Writing your own pre-processor](https://github.com/DbUp/DbUp/blob/master/docs/more-info/preprocessors.md)

```csharp
WithPreprocessor(IScriptPreprocessor preprocessor);
```

## Methods available after having setup DbUp

When all options have been supplied and `.Build()` has been run, the resulting `UpgradeEngine`-object will have the following methods available:

- `GetScriptsToExecute()` - Show all scripts that will be executed
- `GetExecutedScripts()` - Show all scripts that have been executed
- `IsUpgradeRequired()` - ??? Verify that all scripts have already been run?
- `MarkAsExecuted()` - ??? Mark scripts as executed?
- `TryConnect()` - Attempt to connect to the configured database
- `PerformUpgrade()` - Run all scripts that either haven't run or have the option RunAlways set
- `LogScriptOutput()` - Log the output from the scripts

## UNSORTED

```csharp
WithExecutionTimeout(TimeSpan? timeout);
WithFilter(IScriptFilter filter);
```
