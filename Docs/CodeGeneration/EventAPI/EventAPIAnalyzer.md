# 🔬 EventAPIAnalyzer

A Roslyn diagnostic analyzer that validates `[EventAPI]` class declarations for the Event API Generator. It reports build
errors when event key fields are missing an initializer or are initialized with `new()` / `default`.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)

---

## 🗂 Example of Usage

The analyzer flags invalid initializers:

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

After applying the code fix:

```csharp
[EventAPI]
public static partial class GameEventAPI
{
    public static readonly EventKey<IEventBus> PlayerTurnStarted = new(nameof(PlayerTurnStarted));
    public static readonly EventKey<IEventBus> PlayerTurnEnded = new(nameof(PlayerTurnEnded));
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
// Roslyn diagnostic analyzer (external implementation).
public class EventAPIAnalyzer : DiagnosticAnalyzer
```

- **Description:** Roslyn diagnostic analyzer that validates `[EventAPI]` key initializers.
- **Inheritance:** `DiagnosticAnalyzer`
- **Notes:**
  - Only static fields of type `EventKey<>` from the `Atomic.Events` namespace are checked.
  - **EAPI0001** — event key field has no initializer.
  - **EAPI0002** — event key field is initialized with `new()` or `default`.
  - Both diagnostics ship with a code fix that inserts `= new(nameof(FieldName))`.
- **See also:** [EventAPIGenerator](EventAPIGenerator.md), [Setup](../Setup.md), [Code Generation Manual](../Manual.md)
