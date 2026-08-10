# 🧩️ Atomic.Entities

Represents a framework for Unity and C# that allows you to **architect your game using entities**. With this
framework, all game objects, systems, UI elements, and application contexts can be represented as
**entities**, each containing **state** and **behaviour**.

---

## 📑 Table of Contents

- [Requirements](#-requirements)
- [Using Odin Inspector](#-using-odin-inspector)
- [Entity API Source Generation](#-entity-api-source-generation)
  - [Declaring Keys](#declaring-keys)
  - [Configuration](#configuration)
  - [Analyzer](#analyzer)
  - [Setup](#setup)
- [Entity Domain Behaviours](#-entity-domain-behaviours)
- [API Reference](#-api-reference)
- [Performance](#-performance)
- [Best Practices](#-best-practices)

---

## 📝 Requirements

The framework requires **Unity 6** or **.NET 7+**. Make sure your development environment meets these requirements
before using the framework.

---

## 🎛 Using Odin Inspector

For better **debugging**, **configuration**, and **visualization** of game state, we **optionally recommend**
using [Odin Inspector](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041). The
framework **works without Odin**, but Odin makes inspection and tweaking much easier.

---

## 🧬 Entity API Source Generation

The [Entity API Generator](CodeGen/EntityExtensionsAPIAttribute.md) turns declarative `[EntityExtensionsAPI]` classes into
strongly-typed extension methods for entity tags and values. Declare `TagKey<>` and `ValueKey<>` fields once and use
generated `Add{Name}`, `Get{Name}`, `Set{Name}`, `Has{Name}Tag`, and other helper methods.

### Declaring Keys

Supported field types:

| Type | Generated As |
|------|--------------|
| `TagKey<E>` | Tag methods extending `E` |
| `TagKey` | Tag methods extending `IEntity` |
| `ValueKey<E, T>` | Value methods of type `T` extending `E` |
| `ValueKey<T>` | Value methods of type `T` extending `IEntity` |

Every field must be initialized with a non-default constructor, for example `new(nameof(FieldName))`.

```csharp
using Atomic.Entities;

[EntityExtensionsAPI]
public static partial class PlayerAPI
{
    public static readonly TagKey<IEntity> Alive = new(nameof(Alive));
    public static readonly ValueKey<IEntity, int> Health = new(nameof(Health));
    public static readonly ValueKey<IEntity, float> Speed = new(nameof(Speed));
}
```

After compilation, the generator adds extension methods such as:

```csharp
entity.AddAliveTag();
entity.AddHealth(100);
int health = entity.GetHealth();
entity.SetSpeed(5.5f);
```

### Configuration

The `[EntityExtensionsAPI]` attribute supports two properties:

| Property | Default | Description |
|----------|---------|-------------|
| `Unsafe` | `false` | Generate unsafe value accessors and `Ref{Name}` methods. |
| `AggressiveInlining` | `true` | Add `[MethodImpl(MethodImplOptions.AggressiveInlining)]` to every method. |

Apply `[Unsafe]` to individual value fields to force unsafe accessors for those fields when the class-level `Unsafe`
flag is `false`.

### Analyzer

The [Entity API Analyzer](CodeGen/EntityAPIAnalyzer.md) validates key initializers:

| Rule | Description |
|------|-------------|
| `EAPI0001` | Key field has no initializer. |
| `EAPI0002` | Key field is initialized with `new()` or `default`. |

Both diagnostics include a code fix that inserts `= new(nameof(FieldName))`.

### Setup

The generator and analyzer DLLs are compile-time only. Add them to your Unity project as Roslyn analyzers:

1. Place the four DLLs in `Assets/Plugins/Atomic/SourceGenerators/`:
   - `EntityAPIGenerator.dll`
   - `EntityAPIAnalyzer.dll`
   - `EventAPIGenerator.dll`
   - `EventAPIAnalyzer.dll`
2. Select each DLL in the Unity Project window.
3. Add the asset label `RoslynAnalyzer`.
4. Under **Select platforms for plugin**, uncheck **Any Platform** and every individual platform.
5. Click **Apply** and restart Unity or run `Assets → Reimport All`.

For full build/deploy instructions, see the generator source in
`Assets/Plugins/Atomic/SourceGenerators/Project~`.

#### Inspecting generated source

The generators produce code **in-memory**. To write the generated files to disk, define the symbol:

```
ATOMIC_OUTPUT_SOURCEGEN_FILES
```

in `Edit → Project Settings → Player → Scripting Define Symbols`. Files are then written to:

```
Temp/GeneratedCode/
```

For event-bus source generation, see the [Events manual](../Events/Manual.md#-event-api-source-generation).

---

## 🎭 Entity Domain Behaviours

The [Entity Domain Generator](CodeGen/EntityDomainBehavioursAttribute.md) emits strongly-typed domain behaviour
interfaces for a specific entity type. Mark any class with `[EntityDomainBehaviours(typeof(IYourEntity))]` and the
generator creates interfaces such as `IYourEntityBehaviour`, `IYourEntityTick`, `IYourEntityInit`, and others.

```csharp
using Atomic.Entities;

namespace Game.Domain
{
    public interface IPlayer : IEntity { }

    [EntityDomainBehaviours(typeof(IPlayer))]
    public static class PlayerDomain { }
}
```

After compilation, implement the generated interfaces:

```csharp
public sealed class PlayerMoveBehaviour : IPlayerTick
{
    public void Tick(IPlayer player, float deltaTime) { }
}
```

This removes the boilerplate of manually creating `IEntityTick<IPlayer>`, `IEntityInit<IPlayer>`, and other lifecycle
interfaces for every domain entity.

---

## 🔍 API Reference

This section provides a complete reference to all major subsystems of the framework. Each module is documented with
usage examples, lifecycle details, and integration notes to help you build, extend, and optimize your architecture.

- [Entities](Entities/Manual.md) <!-- + -->
- [Behaviours](Behaviours/Manual.md) <!-- + -->
- [Installers](Installers/Manual.md) <!-- + -->
- [Aspects](Aspects/Manual.md) <!-- + -->
- [Factories](Factories/Manual.md) <!-- + -->
- [Baking](Baking/Manual.md) <!-- + -->
- [Bootstrap](Bootstrap/Manual.md) <!-- + -->
- [Pooling](Pooling/Manual.md) <!-- + -->
- [Collections](Collections/Manual.md) <!-- + -->
- [Worlds](Worlds/Manual.md) <!-- + -->
- [Registry](Registry/EntityRegistry.md) <!-- + -->
- [Filters](Filters/Manual.md) <!-- + -->
- [Triggers](Filters/EntityTriggers.md) <!-- + -->
- [Systems](Systems/Manual.md) <!-- + -->
- [Lifecycle](Lifecycle/Manual.md) <!-- + -->
- [Inspector](Inspector/Manual.md) <!-- + -->
- [Views](UI/Manual.md) <!-- + -->
- [KeyStore](KeyStore/Manual.md) <!-- + -->
- [Source Generation](CodeGen/EntityExtensionsAPIAttribute.md) <!-- + -->
- [EntityDomainBehaviours](CodeGen/EntityDomainBehavioursAttribute.md) <!-- + -->
- [UnsafeAttribute](CodeGen/UnsafeAttribute.md) <!-- + -->

---

## 🔥 Performance

This section focuses on **runtime efficiency** within the framework. It provides detailed benchmarks, comparisons, and
implementation notes that highlight how different systems and data structures perform under real-world conditions.

- [Entity](Entities/Manual.md#-performance)
- [EntityCollection](Collections/Manual.md#-performance)

---

## 📌 Best Practices

This section provides recommended approaches, patterns, and techniques for building efficient, scalable, and
maintainable systems. Each guide focuses on solving common architectural and performance challenges, helping you write
clean and modular
entity-based code.

- **Architecture**
    - [Upgrading EntityFactory to the Builder](../BestPractices/UpgradingEntityFactoryToBuilder.md) <!-- + -->
    - [Combine EntityPool with EntityFactory](../BestPractices/UsingEntityPoolWithFactories.md) <!-- + -->
    - [Overriding EntityFactories with EntityBakers](../BestPractices/OverrideEntityFactoriesWithBakers.md) <!-- + -->
    - [Building Entity System with Model & View Separation](../BestPractices/EntitySystem.md)  <!-- + -->
- **Optimization**
    - [Iterating over Entity Tags, Values and Behaviours](../BestPractices/IteratingOverEntity.md) <!-- + -->
    - [Iterating over EntityCollections, Worlds and Filters.](../BestPractices/IteratingOverEntityCollections.md) <!-- + -->
- **Installing**
    - [Modular EntityInstallers](../BestPractices/ModularEntityInstallers.md)  <!-- + -->
    - [Uninstall Method for EntityInstallers](../BestPractices/UninstallEntityInstaller.md)
    - [DisposeComposite in EntityInstallers](../BestPractices/UsingSubscriptionsWithDisposeComposite.md)
    - [PlayMode & EditMode for EntityInstallers](../BestPractices/UsingUtilsForEntityInstallers.md) <!-- + -->
    - [Optional with EntityInstallers](../BestPractices/UsingOptionalWithInstallers.md)
- **Features**
    - [InlineActions with Entities](../BestPractices/UsingInlineActions.md) <!-- + -->
    - [InlineFunctions with Entities](../BestPractices/UsingInlineFunctions.md) <!-- + -->
    - [Events with Entities](../BestPractices/UsingEvents.md)
    - [Requests with Entities](../BestPractices/UsingRequests.md) <!-- + -->
    - [Cooldown with Entities](../BestPractices/UsingCooldownInGameMechanics.md) <!-- + -->
    - [Expressions with Entities](../BestPractices/UsingExpressions.md) <!-- + -->
    - [Setters with Entities](../BestPractices/UsingSetters.md) <!-- + -->
