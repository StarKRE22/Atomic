# 🧩 Entity API Source Generator

The **Entity API Source Generator** is a Roslyn incremental source generator that reads `[EntityExtensionsAPI]`-marked static classes and emits strongly-typed extension methods for [Atomic.Entities](https://github.com/StarKRE22/Atomic) tags and values.

It replaces the legacy `.atomic` YAML or IDE-plugin workflows: add the DLL, declare keys in a `partial` static class, and the extension methods appear on the next build.

---

## 📑 Table of Contents

- [Requirements](#-requirements)
- [Setup](#-setup)
- [Basic Usage](#-basic-usage)
  - [Declaring an API Definition](#declaring-an-api-definition)
  - [Using the Generated Extensions](#using-the-generated-extensions)
- [Generated Code](#-generated-code)
  - [Tags](#tags)
  - [Values](#values)
- [Configuration](#-configuration)
- [Analyzer](#-analyzer)
- [Troubleshooting](#-troubleshooting)
- [Implementation Notes](#-implementation-notes)

---

## 📝 Requirements

- **Unity 6** (6000.0 LTS or newer) with source-generator support
- The **EntityAPIGenerator.dll** analyzer added to your Unity project (see [Setup.md](Setup.md))
- The `[EntityExtensionsAPI]` attribute from [Atomic.Entities](https://github.com/StarKRE22/Atomic/blob/main/Assets/Plugins/Atomic/Entities/Scripts/Codegen/EntityExtensionsAPIAttribute.cs)

---

## 🔧 Setup

For build, deploy, and Unity import instructions, see the shared [Setup.md](Setup.md).

---

## 🧩 Basic Usage

### Declaring an API Definition

Create a `public static partial` class, mark it with `[EntityExtensionsAPI]`, and add static readonly fields of type `ValueKey<>` or `TagKey<>`:

```csharp
using Atomic.Entities;
using UnityEngine;

[EntityExtensionsAPI]
public static partial class PlayerAPI
{
    public static readonly TagKey<IEntity> Alive = new(nameof(Alive));
    public static readonly TagKey<IEntity> Dead = new(nameof(Dead));

    public static readonly ValueKey<IEntity, int> Health = new(nameof(Health));
    public static readonly ValueKey<IEntity, float> Speed = new(nameof(Speed));
    public static readonly ValueKey<IPlayerContext, Camera> Camera = new(nameof(Camera));
}
```

The entity type is taken from the **first generic argument** of each key, so one API class can target multiple entity interfaces.

#### Supported declaration styles

| Declaration | Namespace | Entity type | Kind |
|---|---|---|---|
| `TagKey<TContext> Name` | `Atomic.Entities` | `TContext` | Tag |
| `TagKey Name` | `Atomic.Entities` | `IEntity` | Tag |
| `ValueKey<TContext, TValue> Name` | `Atomic.Entities` | `TContext` | Value |
| `ValueKey<TValue> Name` | `Atomic.Entities` | `IEntity` | Value |

> 💡 **Tip:** Always initialize key fields with a non-default constructor (e.g. `new(nameof(Health))`). The [Entity API Analyzer](EntityAPIAnalyzer.md) reports missing or parameterless initializers as build errors.

> ⚠️ **Important:** Plain types and the legacy `Tag` struct are no longer supported. Use `ValueKey<>` / `TagKey<>` for every field.

### Using the Generated Extensions

After the first build, the methods are available on the entity type declared in each key:

```csharp
IEntity entity = new Entity();

// Tags
entity.AddAliveTag();
if (entity.HasAliveTag())
    entity.DelAliveTag();

// Values
entity.AddHealth(100);
int health = entity.GetHealth();
entity.SetHealth(80);

if (entity.TryGetHealth(out int current))
    Debug.Log(current);

if (entity.HasHealth())
    entity.DelHealth();

// Camera is declared on IPlayerContext
IPlayerContext context = ...;
context.AddCamera(camera);
Camera cam = context.GetCamera();
```

---

## 🔍 Generated Code

For each `[EntityExtensionsAPI]` class the generator emits a matching `partial` class with the same name and namespace. You do **not** edit this file.

### Example input

```csharp
[EntityExtensionsAPI]
public static partial class PlayerAPI
{
    public static readonly TagKey<IEntity> Alive = new(nameof(Alive));
    public static readonly ValueKey<IEntity, int> Health = new(nameof(Health));
}
```

### Example output

```csharp
public static partial class PlayerAPI
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetHealth(this IEntity entity) =>
        entity.GetValue<int>(PlayerAPI.Health.Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGetHealth(this IEntity entity, out int value) =>
        entity.TryGetValue(PlayerAPI.Health.Id, out value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddHealth(this IEntity entity, int value) =>
        entity.AddValue(PlayerAPI.Health.Id, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasHealth(this IEntity entity) =>
        entity.HasValue(PlayerAPI.Health.Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool DelHealth(this IEntity entity) =>
        entity.DelValue(PlayerAPI.Health.Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetHealth(this IEntity entity, int value) =>
        entity.SetValue(PlayerAPI.Health.Id, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasAliveTag(this IEntity entity) =>
        entity.HasTag(PlayerAPI.Alive.Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AddAliveTag(this IEntity entity) =>
        entity.AddTag(PlayerAPI.Alive.Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool DelAliveTag(this IEntity entity) =>
        entity.DelTag(PlayerAPI.Alive.Id);
}
```

### Tags

For every `TagKey` / `TagKey<T>` field the generator creates:

| Method | Description |
|---|---|
| `Has{Name}Tag` | Returns `true` if the tag is present. |
| `Add{Name}Tag` | Adds the tag. |
| `Del{Name}Tag` | Removes the tag. |

### Values

For every `ValueKey<TContext, TValue>` / `ValueKey<TValue>` field the generator creates:

| Method | Description |
|---|---|
| `Get{Name}` | Returns the stored value. |
| `TryGet{Name}` | Safely retrieves the value via an `out` parameter. |
| `Add{Name}` | Adds the value to the entity. |
| `Has{Name}` | Returns `true` if the value exists. |
| `Del{Name}` | Removes the value. |
| `Set{Name}` | Sets the value. |

---

## ⚙️ Configuration

### `[EntityExtensionsAPI]`

| Property | Type | Default | Description |
|---|---|---|---|
| `Unsafe` | `bool` | `false` | When `true`, uses unsafe value accessors and emits `Ref{Name}` methods. |
| `AggressiveInlining` | `bool` | `true` | When `true`, adds `MethodImpl(MethodImplOptions.AggressiveInlining)` to every method. |

The entity type is no longer passed to `[EntityExtensionsAPI]`. It is read from each field's first generic argument.

### `[Unsafe]`

| Property | Type | Default | Description |
|---|---|---|---|
| `value` | `bool` | `true` | Override the class-level unsafe flag for a single field. |

#### Unsafe mode

Enable unsafe accessors for the whole class:

```csharp
[EntityExtensionsAPI(Unsafe = true)]
public static partial class PlayerAPI
{
    public static readonly ValueKey<IEntity, int> Health = new(nameof(Health));
}
```

Generated methods include direct references:

```csharp
public static int GetHealth(this IEntity entity) =>
    entity.GetValueUnsafe<int>(PlayerAPI.Health.Id);

public static ref int RefHealth(this IEntity entity) =>
    ref entity.GetValueUnsafe<int>(PlayerAPI.Health.Id);
```

| Mode | `Get` implementation | `Ref` method | Use when |
|---|---|---|---|
| Safe | `entity.GetValue<int>(PlayerAPI.Health.Id)` | ❌ no | You need validation and safety. |
| Unsafe | `entity.GetValueUnsafe<int>(PlayerAPI.Health.Id)` | ✅ `ref int RefHealth(...)` | You have verified the value exists and want zero-overhead access. |

> ⚠️ **Warning:** `Unsafe = true` removes runtime checks. Calling `GetHealth` on an entity that does not have the value can crash or return undefined data. Only use it in performance-critical, verified code paths.

You can mix modes by applying `[Unsafe(false)]` to individual fields:

```csharp
[EntityExtensionsAPI(Unsafe = true)]
public static partial class MixedAPI
{
    public static readonly ValueKey<IEntity, int> Health = new(nameof(Health));      // unsafe

    [Unsafe(false)]
    public static readonly ValueKey<IEntity, float> Speed = new(nameof(Speed));    // safe
}
```

---

## 🔬 Analyzer

Deploy the [Entity API Analyzer](EntityAPIAnalyzer.md) alongside the generator. It reports build errors when `ValueKey<>` / `TagKey<>` fields inside `[EntityExtensionsAPI]` classes are missing an initializer or are initialized with `new()` / `default`.

---

## 🔧 Troubleshooting

### Extensions are not showing in IntelliSense

1. Make sure `EntityAPIGenerator.dll` is in `Assets/Plugins/Atomic/SourceGenerators/`.
2. Check that the **Asset Label** is `RoslynAnalyzer`.
3. Verify platform settings: **Any Platform** must be **unchecked**, and all individual platforms must be **unchecked**.
4. Rebuild the Unity project (`Assets → Reimport All` or restart the editor).

### Build errors after adding the DLL

- Ensure the DLL is **not** included in any runtime platform.
- Ensure all `[EntityExtensionsAPI]` fields are `ValueKey<>` or `TagKey<>` and are initialized (e.g. `new(nameof(Field))`).

### Generated file is not written to disk

The generator produces source **in-memory**. To write generated files to disk, define `ATOMIC_OUTPUT_SOURCEGEN_FILES` in `Edit → Project Settings → Player → Scripting Define Symbols`. Files are written to `Temp/GeneratedCode/`.

---

## 🏗️ Implementation Notes

- Targets `netstandard2.0` and `Microsoft.CodeAnalysis.CSharp` **4.3.0** for Unity 6000 compatibility.
- Uses `SyntaxProvider.CreateSyntaxProvider` instead of `ForAttributeWithMetadataName` because Unity 6000 ships Roslyn 4.3.0.
- Reads the `[EntityExtensionsAPI]` attribute from the `Atomic.Entities` assembly.
- Skips IDE analysis and runs only during actual builds.
- For more details, see [Implementation.md](Implementation.md).
