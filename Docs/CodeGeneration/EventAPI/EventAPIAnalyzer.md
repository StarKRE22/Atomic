# 🔬 Event API Analyzer

The **Event API Analyzer** is a Roslyn diagnostic analyzer that validates `[EventAPI]` class declarations for the
[Event API Generator](EventAPIGenerator.md). It ensures every `EventKey<>` field is initialized so that the generated
extension methods read a valid id.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Setup](#-setup)
- [Rules](#-rules)
- [Code Fixes](#-code-fixes)
- [Examples](#-examples)
- [Troubleshooting](#-troubleshooting)

---

## 🧩 Overview

The analyzer inspects static fields inside classes marked with `[EventAPI]`. Only fields of type `EventKey<>` from the
`Atomic.Events` namespace are checked.

If a field is not initialized, or is initialized with a parameterless constructor, the analyzer reports a build error.

---

## ⚙️ Setup

See [Setup.md](../Setup.md) for the shared setup instructions.

The analyzer is deployed at:

```
Assets/Plugins/Atomic/SourceGenerators/EventAPIAnalyzer.dll
```

Unity loads it alongside the source generator. Diagnostics appear in the Unity console and in the IDE.

---

## 🔍 Rules

| ID | Severity | Description |
|----|----------|-------------|
| `EAPI0001` | Error | An `EventKey<>` field in an `[EventAPI]` class has no initializer. |
| `EAPI0002` | Error | An `EventKey<>` field is initialized with `new()` or `default`, leaving the id at `0`. |

---

## 🔧 Code Fixes

Both diagnostics ship with a quick fix (Ctrl+. or Alt+Enter in Rider / Visual Studio):

> **Initialize 'FieldName' with nameof(FieldName)**

The fix inserts or replaces the initializer with:

```csharp
= new(nameof(FieldName))
```

---

## 🗂 Examples

### Invalid

```csharp
using Atomic.Events;

[EventAPI]
public static partial class GameEventAPI
{
    // EAPI0001: field is not initialized
    public static readonly EventKey<IEventBus> PlayerTurnStarted;

    // EAPI0002: parameterless construction leaves the id at default
    public static readonly EventKey<IEventBus> PlayerTurnEnded = new();
}
```

### After applying the code fix

```csharp
using Atomic.Events;

[EventAPI]
public static partial class GameEventAPI
{
    public static readonly EventKey<IEventBus> PlayerTurnStarted = new(nameof(PlayerTurnStarted));
    public static readonly EventKey<IEventBus> PlayerTurnEnded = new(nameof(PlayerTurnEnded));
}
```

### Valid initializers

```csharp
[EventAPI]
public static partial class GameEventAPI
{
    public static readonly EventKey<IEventBus> PlayerTurnStarted = new(nameof(PlayerTurnStarted));
    public static readonly EventKey<IEventBus, int> DamageDealt = new("DamageDealt");
    public static readonly EventKey<IEventBus> GameEnded = new(42);
}
```

---

## 🔧 Troubleshooting

### Diagnostics do not appear

1. Confirm `EventAPIAnalyzer.dll` is in `Assets/Plugins/Atomic/SourceGenerators/`.
2. Confirm the DLL has the `RoslynAnalyzer` asset label.
3. Confirm all platforms are unchecked in the DLL import settings.
4. Restart Unity or reimport the assembly.

### Analyzer reports fields that are not event keys

Only `EventKey<>` from the `Atomic.Events` namespace is analyzed. Other field types are ignored.

---

## 📦 Source Repository

The analyzer source code is available at:

**https://github.com/dre0dru/Atomic.SourceGenerators**
