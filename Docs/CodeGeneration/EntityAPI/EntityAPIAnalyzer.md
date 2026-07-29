# 🔬 EntityAPIAnalyzer

A Roslyn diagnostic analyzer that validates `[GenerateEntityExtensionsAPI]` class declarations for the Entity API Generator. It reports
build errors when key fields are missing an initializer or are initialized with `new()` / `default`.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)

---

## 🗂 Example of Usage

The analyzer flags invalid initializers:

```csharp
using Atomic.Entities;

[GenerateEntityExtensionsAPI]
public static partial class PlayerAPI
{
    // EAPI0001: field is not initialized
    public static readonly ValueKey<IEntity, int> Health;

    // EAPI0002: parameterless construction leaves the id at default
    public static readonly TagKey<IEntity> Alive = new();
}
```

After applying the code fix:

```csharp
[GenerateEntityExtensionsAPI]
public static partial class PlayerAPI
{
    public static readonly ValueKey<IEntity, int> Health = new(nameof(Health));
    public static readonly TagKey<IEntity> Alive = new(nameof(Alive));
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
// Roslyn diagnostic analyzer (external implementation).
public class EntityAPIAnalyzer : DiagnosticAnalyzer
```

- **Description:** Roslyn diagnostic analyzer that validates `[GenerateEntityExtensionsAPI]` key initializers.
- **Inheritance:** `DiagnosticAnalyzer`
- **Notes:**
  - Only static fields of type `ValueKey<>` or `TagKey<>` from the `Atomic.Entities` namespace are checked.
  - **EAPI0001** — key field has no initializer.
  - **EAPI0002** — key field is initialized with `new()` or `default`.
  - Both diagnostics ship with a code fix that inserts `= new(nameof(FieldName))`.
- **See also:** [EntityAPIGenerator](EntityAPIGenerator.md), [Setup](../Setup.md), [Code Generation Manual](../Manual.md)
