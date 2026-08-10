# 🔬 Event API Analyzer

The **Event API Analyzer** is a Roslyn diagnostic analyzer and code-fix provider that validates `[EventExtensionsAPI]` class declarations for the [Event API Generator](EventAPIGenerator.md).

It ensures every `EventKey<>` field is initialized with a valid, non-default constructor so that generated event-bus extension methods receive a correct `Id`.

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

The Event API generator only accepts static fields of type `EventKey<>` from the `Atomic.Events` namespace. For the generated extension methods to work, each key must have a valid `Id`. This analyzer checks that every event key field is initialized and never left with the default `0` id.

The `[EventExtensionsAPI]` attribute is defined in [Atomic.Events](https://github.com/StarKRE22/Atomic/blob/main/Assets/Plugins/Atomic/Events/Scripts/CodeGen/EventExtensionsAPIAttribute.cs).

---

## 📋 Rules

| ID | Severity | Description |
|---|---|---|
| `EAPI0001` | Error | An `EventKey<>` field in an `[EventExtensionsAPI]` class has no initializer. |
| `EAPI0002` | Error | An `EventKey<>` field is initialized with `new()` or `default`, which leaves the id at `0`. |

---

## 🔧 Code Fixes

Both diagnostics ship with a quick fix (Ctrl+. or Alt+Enter in Rider/VS):

> **Initialize 'FieldName' with nameof(FieldName)** — inserts or replaces the initializer with `= new(nameof(FieldName))`.

### Before

```csharp
[EventExtensionsAPI]
public static partial class GameEventAPI
{
    public static readonly EventKey<IEventBus> PlayerTurnStarted;
    public static readonly EventKey<IEventBus> PlayerTurnEnded = new();
}
```

### After applying the code fix

```csharp
[EventExtensionsAPI]
public static partial class GameEventAPI
{
    public static readonly EventKey<IEventBus> PlayerTurnStarted = new(nameof(PlayerTurnStarted));
    public static readonly EventKey<IEventBus> PlayerTurnEnded = new(nameof(PlayerTurnEnded));
}
```

---

## ✅ Valid and Invalid Examples

### Valid

```csharp
[EventExtensionsAPI]
public static partial class GameEventAPI
{
    public static readonly EventKey<IEventBus> GameStarted = new(nameof(GameStarted));
    public static readonly EventKey<IEventBus, int> DamageDealt = new(nameof(DamageDealt));
}
```

### Invalid

```csharp
[EventExtensionsAPI]
public static partial class GameEventAPI
{
    // EAPI0001: field is not initialized
    public static readonly EventKey<IEventBus> GameStarted;

    // EAPI0002: parameterless construction leaves Id at default value
    public static readonly EventKey<IEventBus, int> DamageDealt = new();
}
```

---

## 🔧 Setup

For build, deploy, and Unity import instructions, see the shared [Setup.md](Setup.md). The analyzer DLL (`EventAPIAnalyzer.dll`) is deployed the same way as the generator DLLs.

---

## 🔧 Troubleshooting

### Diagnostics do not appear

1. Make sure `EventAPIAnalyzer.dll` is in `Assets/Plugins/Atomic/SourceGenerators/`.
2. Confirm the **RoslynAnalyzer** asset label is applied.
3. Confirm all platforms are unchecked in the Inspector.
4. Restart Unity or run `Assets → Reimport All`.

### False negatives

- The analyzer only inspects **static** fields inside classes marked with `[EventExtensionsAPI]`.
- It only checks fields whose type is `EventKey<>` from the `Atomic.Events` namespace.

---

## 🏗️ Implementation Notes

- Targets `netstandard2.0` and `Microsoft.CodeAnalysis.CSharp` **4.3.0** for Unity 6000 compatibility.
- Uses `RegisterSyntaxNodeAction` on `FieldDeclarationSyntax`.
- Only analyzes static fields inside classes marked with `[EventExtensionsAPI]`.
- Only checks fields whose type is `EventKey<>` from the `Atomic.Events` namespace.
- For more details, see [Implementation.md](Implementation.md).
