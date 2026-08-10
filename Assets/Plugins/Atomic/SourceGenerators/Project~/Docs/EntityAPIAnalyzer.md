# 🔬 Entity API Analyzer

The **Entity API Analyzer** is a Roslyn diagnostic analyzer and code-fix provider that validates `[EntityExtensionsAPI]` class declarations for the [Entity API Generator](EntityAPIGenerator.md).

It ensures every `ValueKey<>` and `TagKey<>` field is initialized with a valid, non-default constructor so that generated extension methods receive a correct `Id`.

---

## 📑 Table of Contents

- [Purpose](#-purpose)
- [Rules](#-rules)
- [Code Fixes](#-code-fixes)
- [Valid and Invalid Examples](#-valid-and-invalid-examples)
- [Setup](#-setup)
- [Troubleshooting](#-troubleshooting)
- [Implementation Notes](#-implementation-notes)

---

## 🎯 Purpose

The Entity API generator only accepts static fields of type `ValueKey<>` or `TagKey<>`. For the generated extension methods to work, each key must have a valid `Id`. This analyzer checks that every key field is initialized and never left with the default `0` id.

The `[EntityExtensionsAPI]` attribute is defined in [Atomic.Entities](https://github.com/StarKRE22/Atomic/blob/main/Assets/Plugins/Atomic/Entities/Scripts/Codegen/EntityExtensionsAPIAttribute.cs).

---

## 📋 Rules

| ID | Severity | Description |
|---|---|---|
| `EAPI0001` | Error | A `ValueKey<>` / `TagKey<>` field in an `[EntityExtensionsAPI]` class has no initializer. |
| `EAPI0002` | Error | A `ValueKey<>` / `TagKey<>` field is initialized with `new()` or `default`, which leaves the id at `0`. |

---

## 🔧 Code Fixes

Both diagnostics ship with a quick fix (Ctrl+. or Alt+Enter in Rider/VS):

> **Initialize 'FieldName' with nameof(FieldName)** — inserts or replaces the initializer with `= new(nameof(FieldName))`.

### Before

```csharp
[EntityExtensionsAPI]
public static partial class PlayerContextAPI
{
    public static readonly ValueKey<IPlayerContext, int> Health;
    public static readonly TagKey<IPlayerContext> Alive = new();
}
```

### After applying the code fix

```csharp
[EntityExtensionsAPI]
public static partial class PlayerContextAPI
{
    public static readonly ValueKey<IPlayerContext, int> Health = new(nameof(Health));
    public static readonly TagKey<IPlayerContext> Alive = new(nameof(Alive));
}
```

---

## ✅ Valid and Invalid Examples

### Valid

```csharp
[EntityExtensionsAPI]
public static partial class PlayerContextAPI
{
    public static readonly ValueKey<IPlayerContext, int> Health = new(nameof(Health));
    public static readonly TagKey<IPlayerContext> Alive = new("Alive");
    public static readonly ValueKey<IPlayerContext, float> Speed = new(123);
}
```

### Invalid

```csharp
[EntityExtensionsAPI]
public static partial class PlayerContextAPI
{
    // EAPI0001: field is not initialized
    public static readonly ValueKey<IPlayerContext, int> Health;

    // EAPI0002: parameterless construction leaves Id at default value
    public static readonly TagKey<IPlayerContext> Alive = new();
}
```

---

## 🔧 Setup

For build, deploy, and Unity import instructions, see the shared [Setup.md](Setup.md). The analyzer DLL (`EntityAPIAnalyzer.dll`) is deployed the same way as the generator DLLs.

---

## 🔧 Troubleshooting

### Diagnostics do not appear

1. Make sure `EntityAPIAnalyzer.dll` is in `Assets/Plugins/Atomic/SourceGenerators/`.
2. Confirm the **RoslynAnalyzer** asset label is applied.
3. Confirm all platforms are unchecked in the Inspector.
4. Restart Unity or run `Assets → Reimport All`.

### False negatives

- The analyzer only inspects **static** fields inside classes marked with `[EntityExtensionsAPI]`.
- It only checks fields whose type is `ValueKey<>` or `TagKey<>` from the `Atomic.Entities` namespace.

---

## 🏗️ Implementation Notes

- Targets `netstandard2.0` and `Microsoft.CodeAnalysis.CSharp` **4.3.0** for Unity 6000 compatibility.
- Uses `RegisterSyntaxNodeAction` on `FieldDeclarationSyntax`.
- Only analyzes static fields inside classes marked with `[EntityExtensionsAPI]`.
- Only checks fields whose type is `ValueKey<>` or `TagKey<>` from the `Atomic.Entities` namespace.
- For more details, see [Implementation.md](Implementation.md).
