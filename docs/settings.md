# List of other settings

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
