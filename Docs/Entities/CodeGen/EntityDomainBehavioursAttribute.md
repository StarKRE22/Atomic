# 🧩 EntityDomainBehavioursAttribute

Marks a class as a **domain behaviour definition** for the Entity Domain Generator. The generator reads the entity type
passed to the constructor and emits strongly-typed behaviour interfaces for that entity type.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
  - [All Generated Interfaces](#all-generated-interfaces)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Constructors](#-constructors)
    - [`EntityDomainBehavioursAttribute(Type)`](#entitydomainbehavioursattributetype)
  - [Properties](#-properties)
    - [`EntityType`](#entitytype)

---

## 🗂 Example of Usage

Define a domain entity interface and mark any class with `[EntityDomainBehaviours(typeof(IYourEntity))]`:

```csharp
using Atomic.Entities;

namespace Game.Domain
{
    public interface IPlayer : IEntity
    {
    }

    [EntityDomainBehaviours(typeof(IPlayer))]
    public static class PlayerDomain
    {
    }
}
```

After compilation, the generator creates interfaces such as `IPlayerBehaviour`, `IPlayerTick`, `IPlayerInit`, and others:

```csharp
namespace Game.Domain
{
    public sealed class PlayerMoveBehaviour : IPlayerTick
    {
        public void Tick(IPlayer player, float deltaTime)
        {
            // per-frame logic for IPlayer
        }
    }

    public sealed class PlayerInitBehaviour : IPlayerInit
    {
        public void Init(IPlayer player)
        {
            // setup logic for IPlayer
        }
    }
}
```

### All Generated Interfaces

For `[EntityDomainBehaviours(typeof(IPlayer))]` the generator emits the following interfaces. Each one is empty and
inherits from the generic lifecycle interface in `Atomic.Entities` plus the domain marker `IPlayerBehaviour`.

```csharp
namespace Game.Domain
{
    public interface IPlayerBehaviour : IEntityBehaviour { }

    public interface IPlayerTick : IEntityTick<IPlayer>, IPlayerBehaviour { }

    public interface IPlayerFixedTick : IEntityFixedTick<IPlayer>, IPlayerBehaviour { }

    public interface IPlayerLateTick : IEntityLateTick<IPlayer>, IPlayerBehaviour { }

    public interface IPlayerInit : IEntityInit<IPlayer>, IPlayerBehaviour { }

    public interface IPlayerEnable : IEntityEnable<IPlayer>, IPlayerBehaviour { }

    public interface IPlayerDisable : IEntityDisable<IPlayer>, IPlayerBehaviour { }

    public interface IPlayerDispose : IEntityDispose<IPlayer>, IPlayerBehaviour { }

    public interface IPlayerGizmos : IEntityGizmos<IPlayer>, IPlayerBehaviour { }
}
```

Implementations for each lifecycle look like this:

```csharp
namespace Game.Domain
{
    public sealed class PlayerTickBehaviour : IPlayerTick
    {
        public void Tick(IPlayer player, float deltaTime) { }
    }

    public sealed class PlayerFixedTickBehaviour : IPlayerFixedTick
    {
        public void FixedTick(IPlayer player, float deltaTime) { }
    }

    public sealed class PlayerLateTickBehaviour : IPlayerLateTick
    {
        public void LateTick(IPlayer player, float deltaTime) { }
    }

    public sealed class PlayerInitBehaviour : IPlayerInit
    {
        public void Init(IPlayer player) { }
    }

    public sealed class PlayerEnableBehaviour : IPlayerEnable
    {
        public void Enable(IPlayer player) { }
    }

    public sealed class PlayerDisableBehaviour : IPlayerDisable
    {
        public void Disable(IPlayer player) { }
    }

    public sealed class PlayerDisposeBehaviour : IPlayerDispose
    {
        public void Dispose(IPlayer player) { }
    }

    public sealed class PlayerGizmosBehaviour : IPlayerGizmos
    {
        public void DrawGizmos(IPlayer player) { }
    }
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class EntityDomainBehavioursAttribute : Attribute
```

- **Description:** Marks a class as a domain behaviour definition for source generation.
- **Inheritance:** `Attribute`
- **Notes:**
  - The target class provides the namespace for the generated interfaces.
  - The target class can be `static` or instance; it is only used as a marker.
  - The `EntityType` argument must implement `IEntity`.
  - Generated interfaces are empty composite/marker interfaces that inherit from the generic lifecycle interfaces in
    `Atomic.Entities` (for example, `IEntityTick<TEntity>`) and from the domain `I{Entity}Behaviour` marker.
- **See also:** [Entity API Source Generation](../Manual.md#-entity-api-source-generation)

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `EntityDomainBehavioursAttribute(Type)`

```csharp
public EntityDomainBehavioursAttribute(Type entityType)
```

- **Description:** Initializes a new instance of the attribute with the target entity type.
- **Parameters:**
  - `entityType` — The entity type (interface or concrete class) for which behaviour interfaces will be generated.
    Must implement `IEntity`.
- **Notes:** The generator validates at build time that `entityType` implements `Atomic.Entities.IEntity`.

---

### 🔑 Properties

#### `EntityType`

```csharp
public Type EntityType { get; }
```

- **Description:** Gets the entity type for which behaviour interfaces are generated.
- **Access:** Read-only
- **Notes:** Set through the constructor. The generator uses this type to build interface names such as
  `I{EntityName}Behaviour`, `I{EntityName}Tick`, `I{EntityName}Init`, and so on.
