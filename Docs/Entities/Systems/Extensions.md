# 🧩 Systems Extensions

Systems extensions provide helper methods for wiring an [EntitySystemBase<E>](EntitySystemBase.md) into an entity
lifecycle.

Use these methods when a context entity should own a system and drive it from `Enable`, `Tick` / `FixedTick` /
`LateTick`, `Disable`, and `Dispose` callbacks.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
  - [Tick System](#1️⃣-tick-system)
  - [Fixed System](#2️⃣-fixed-system)
  - [Late Tick System](#3️⃣-late-tick-system)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Methods](#-methods)
    - [AddTickSystem<TContext, TEntity>](#addticksystemtcontext-tentity)
    - [AddFixedTickSystem<TContext, TEntity>](#addfixedticksystemtcontext-tentity)
- [Notes](#-notes)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Tick System

```csharp
IEntity gameContext = ...;
IReadOnlyEntityCollection<IGameEntity> world = ...;

// Enables the system with gameContext.Enable(),
// updates it with gameContext.Tick(deltaTime),
// disables it with gameContext.Disable(),
// and disposes it with gameContext.Dispose().
gameContext.AddTickSystem(new MovementSystem(world, new MovementSystem.Settings
{
    frameBudget = 0.016f,
    batching = { minSize = 64, maxSize = 512 }
}));
```

---

<div id="ex2"></div>

### 2️⃣ Fixed Tick System

```csharp
IEntity gameContext = ...;
IReadOnlyEntityCollection<IGameEntity> world = ...;

// Enables the system with gameContext.Enable(),
// updates it with gameContext.FixedTick(deltaTime),
// disables it with gameContext.Disable(),
// and disposes it with gameContext.Dispose().
gameContext.AddFixedTickSystem(new MovementSystem(world, new MovementSystem.Settings
{
    frameBudget = 0.016f,
    batching = { minSize = 64, maxSize = 512 }
}));
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public static partial class Extensions
```

- **Namespace:** `Atomic.Entities`
- **Description:** Contains extension methods for attaching entity systems to entity lifecycle callbacks.
- **See also:** [EntitySystemBase<E>](EntitySystemBase.md), [Lifecycle Extensions](../Lifecycle/Extensions.md)

---

### 🏹 Methods

#### `AddTickSystem<TContext, TEntity>`

```csharp
public static void AddTickSystem<TContext, TEntity>(
    this TContext entity,
    EntitySystemBase<TEntity> system
)
    where TContext : IEntity
    where TEntity : IEntity;
```

- **Description:** Adds a system to the context entity and updates it during `Tick`.
- **Parameters:**
  - `entity` — The context entity that owns the system lifecycle.
  - `system` — The entity system to enable, update, disable, and dispose.
- **Lifecycle Wiring:**
  - `entity.WhenEnable(system.Enable)`
  - `entity.WhenTick(system.Update)`
  - `entity.WhenDisable(system.Disable)`
  - `entity.WhenDispose(system.Dispose)`

#### `AddFixedTickSystem<TContext, TEntity>`

```csharp
public static void AddFixedTickSystem<TContext, TEntity>(
    this TContext entity,
    EntitySystemBase<TEntity> system
)
    where TContext : IEntity
    where TEntity : IEntity;
```

- **Description:** Adds a system to the context entity and updates it during `FixedTick`.
- **Parameters:**
  - `entity` — The context entity that owns the system lifecycle.
  - `system` — The entity system to enable, update, disable, and dispose.
- **Lifecycle Wiring:**
  - `entity.WhenEnable(system.Enable)`
  - `entity.WhenFixedTick(system.Update)`
  - `entity.WhenDisable(system.Disable)`
  - `entity.WhenDispose(system.Dispose)`

#### `AddLateTickSystem<TContext, TEntity>`

```csharp
public static void AddLateTickSystem<TContext, TEntity>(
    this TContext entity,
    EntitySystemBase<TEntity> system
)
    where TContext : IEntity
    where TEntity : IEntity;
```

- **Description:** Adds a system to the context entity and updates it during `LateTick`.
- **Parameters:**
  - `entity` — The context entity that owns the system lifecycle.
  - `system` — The entity system to enable, update, disable, and dispose.
- **Lifecycle Wiring:**
  - `entity.WhenEnable(system.Enable)`
  - `entity.WhenLateTick(system.Update)`
  - `entity.WhenDisable(system.Disable)`
  - `entity.WhenDispose(system.Dispose)`

---

## 📝 Notes

- These methods do not add the system as an entity behaviour. They subscribe the system methods to lifecycle callbacks.
- The owning context must be driven through the corresponding lifecycle method: `Tick`, `FixedTick`, or `LateTick`.
- The system is disposed when the context entity is disposed.
