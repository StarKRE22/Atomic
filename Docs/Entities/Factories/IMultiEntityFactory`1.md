# 🧩 IMultiEntityFactory<K, E>

A generic interface for creating and managing multiple entities identified by a key.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Create(K)](#createk)
        - [TryCreate(K, out E)](#trycreatek-out-e)
        - [Contains(K)](#containsk)
---

## 🗂 Example of Usage

Below is an example of creating enemies through the multi-entity factory:

```csharp
// Assume we have an enum of enemy types
public enum EnemyType 
{
    Orc,
    Goblin,
    Troll
}
```

```csharp
// Assume we have a enemy type derived from Entity 
public class EnemyEntity : Entity
{
}
```

```csharp
// Assume we have an instance of a multi-entity factory
IMultiEntityFactory<EnemyType, EnemyEntity> multiFactory = ...

// Check if an entity exists and create it
if (multiFactory.Contains(EnemyType.Orc))  
{  
    EnemyEntity orc = multiFactory.Create(EnemyType.Orc);  
}

// Try creating an entity safely
if (multiFactory.TryCreate(EnemyType.Goblin, out EnemyEntity goblin))  
{  
    // use goblin
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IMultiEntityFactory<in K, E> where E : IEntity
```

- **Type Parameters:**
    - `K` — The type of key used to identify an entity.
    - `E` — The type of entity to create, which must implement [IEntity](../Entities/IEntity.md)
- **See also:** [IMultiEntityFactory](IMultiEntityFactory.md),
  [MultiEntityFactory<K, E>](MultiEntityFactory%601.md), [ScriptableEntityCatalog<K, E, F>](ScriptableEntityCatalog%601.md)

---

### 🏹 Methods

#### `Create(K)`

```csharp
public E Create(K key);
```

- **Description:** Creates a new entity associated with the specified key.
- **Parameter:** `key` — The key identifying the entity to create.
- **Returns:** A new instance of type `E`.

#### `TryCreate(K, out E)`

```csharp
public bool TryCreate(K key, out E entity);
```

- **Description:** Attempts to create a new entity associated with the specified key.
- **Parameters:**
    - `key` — The key identifying the entity to create.
    - `entity` — When the method returns, contains the created entity if the key exists; otherwise, the default value of
      `E`.
- **Returns:** `true` if the entity was created successfully; otherwise, `false`.

#### `Contains(K)`

```csharp
public bool Contains(K key);
```

- **Description:** Determines whether an entity associated with the specified key exists.
- **Parameter:** `key` — The key to check.
- **Returns:** `true` if an entity with the given key exists; otherwise, `false`.