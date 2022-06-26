# Script settings
## Configure sort order for list of scripts

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

## Filter as a separate option

I haven't verified this yet, but I'm guessing this extension-method can be used as a filter on all script-sources supplied before in the `.Build()` chain.


```csharp
WithFilter(IScriptFilter filter);
```