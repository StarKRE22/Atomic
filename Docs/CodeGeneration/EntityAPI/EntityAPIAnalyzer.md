# 🔬 Entity API Analyzer

The **Entity API Analyzer** is a Roslyn diagnostic analyzer that validates `[EntityAPI]` class declarations for the
[Entity API Generator](EntityAPIGenerator.md). It ensures every `ValueKey<>` and `TagKey<>` field is initialized so that
the generated extension methods read a valid id.

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

The analyzer inspects static fields inside classes marked with `[EntityAPI]`. Only fields of type `ValueKey<>` or
`TagKey<>` from the `Atomic.Entities` namespace are checked.

If a field is not initialized, or is initialized with a parameterless constructor, the analyzer reports a build error.

---

## ⚙️ Setup

See [Setup.md](../Setup.md) for the shared setup instructions.

The analyzer is deployed at:

```
Assets/Plugins/Atomic/SourceGenerators/EntityAPIAnalyzer.dll
```

Unity loads it alongside the source generator. Diagnostics appear in the Unity console and in the IDE.

---

## 🔍 Rules

| ID | Severity | Description |
|----|----------|-------------|
| `EAPI0001` | Error | A `ValueKey<>` / `TagKey<>` field in an `[EntityAPI]` class has no initializer. |
| `EAPI0002` | Error | A `ValueKey<>` / `TagKey<>` field is initialized with `new()` or `default`, leaving the id at `0`. |

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
using Atomic.Entities;

[EntityAPI]
public static partial class PlayerContextAPI
{
    // EAPI0001: field is not initialized
    public static readonly ValueKey<IPlayerContext, int> Health;

    // EAPI0002: parameterless construction leaves the id at default
    public static readonly TagKey<IPlayerContext> Alive = new();
}
```

### After applying the code fix

```csharp
using Atomic.Entities;

[EntityAPI]
public static partial class PlayerContextAPI
{
    public static readonly ValueKey<IPlayerContext, int> Health = new(nameof(Health));
    public static readonly TagKey<IPlayerContext> Alive = new(nameof(Alive));
}
```

### Valid initializers

```csharp
[EntityAPI]
public static partial class PlayerContextAPI
{
    public static readonly ValueKey<IPlayerContext, int> Health = new(nameof(Health));
    public static readonly TagKey<IPlayerContext> Alive = new("Alive");
    public static readonly ValueKey<IPlayerContext, float> Speed = new(123);
}
```

---

## 🔧 Troubleshooting

### Diagnostics do not appear

1. Confirm `EntityAPIAnalyzer.dll` is in `Assets/Plugins/Atomic/SourceGenerators/`.
2. Confirm the DLL has the `RoslynAnalyzer` asset label.
3. Confirm all platforms are unchecked in the DLL import settings.
4. Restart Unity or reimport the assembly.

### Analyzer reports fields that are not keys

Only `ValueKey<>` and `TagKey<>` from the `Atomic.Entities` namespace are analyzed. Other field types are ignored.

---

## 📦 Source Repository

The analyzer source code is available at:

**https://github.com/dre0dru/Atomic.SourceGenerators**
