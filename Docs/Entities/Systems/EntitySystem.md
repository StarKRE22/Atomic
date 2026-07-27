# 🧩 EntitySystem

Concrete base class for systems that update entities in a simple round-robin fashion. It tracks entities in an internal array and processes up to `batchSize` of them each frame.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Constructors](#-constructors)
    - [EntitySystem(IReadOnlyEntityCollection&lt;E&gt;, Settings)](#entitysystemireadonlyentitycollectione-settings)
  - [Methods](#-methods)
    - [Update(E, float)](#updatee-float)
    - [Dispose()](#dispose)

---

## 🗂 Example of Usage

```csharp
public sealed class MovementSystem : EntitySystem<IGameEntity>
{
    public MovementSystem(
        IReadOnlyEntityCollection<IGameEntity> source,
        Settings settings
    ) : base(source, settings) { }

    protected override void Update(IGameEntity entity, float deltaTime)
    {
        Vector3 velocity = entity.GetValue<Vector3>("velocity");
        Vector3 position = entity.GetValue<Vector3>("position");
        entity.SetValue("position", position + velocity * deltaTime);
    }
}

// Wire to a context entity
var settings = new EntitySystem<IGameEntity>.Settings
{
    frameBudget = 0.02f
};

MovementSystem movement = new MovementSystem(world, settings);
contextEntity.AddTickSystem(movement);
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
[Serializable]
public abstract class EntitySystem<E> : EntitySystemBase<E>, IDisposable where E : IEntity
```

- **Description:** Concrete base class for systems that update entities in a simple round-robin fashion.
- **Inheritance:** [EntitySystemBase<E>](EntitySystemBase.md), `IDisposable`
- **Type Parameters:** `E` — The entity type managed by the system. Must implement [IEntity](../Entities/IEntity.md).
- **Notes:** Maintains an internal array and processes entities sequentially using a cursor. Wraps around to the beginning when the end is reached.
- **See also:** [EntitySystemBase<E>](EntitySystemBase.md), [PriorityEntitySystem<E>](PriorityEntitySystem.md)

### 🏗️ Constructors

#### `EntitySystem(IReadOnlyEntityCollection<E>, Settings)`

```csharp
protected EntitySystem(IReadOnlyEntityCollection<E> source, Settings settings)
```

- **Description:** Initializes the system with the specified entity collection and settings.
- **Parameters:**
  - `source` — The collection of entities to process.
  - `settings` — System settings inherited from [EntitySystemBase<E>.Settings](EntitySystemBase.md).

### 🏹 Methods

#### `Update(E, float)`

```csharp
protected abstract void Update(E entity, float deltaTime)
```

- **Description:** Called for each entity that should be updated this frame.
- **Parameters:**
  - `entity` — The entity to update.
  - `deltaTime` — Time elapsed since the last update.
- **Remarks:** Must be implemented in derived classes.

#### `Dispose()`

```csharp
public override void Dispose()
```

- **Description:** Clears the internal entity array and lookup.
