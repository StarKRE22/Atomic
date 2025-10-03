# 🧩 IMultiEntityFactory<K, E>

```csharp
public interface IMultiEntityFactory<in K, E> where E : IEntity
```

- **Description:** A generic interface for creating and managing multiple entities identified by a key.
- **Type Parameters:**
    - `K` — The type of key used to identify an entity.
    - `E` — The type of entity to create, which must implement [IEntity](../Entities/IEntity.md)
- **See also:** [IMultiEntityFactory](IMultiEntityFactory.md),
  [MultiEntityFactory<K, E>](MultiEntityFactory%601.md), [ScriptableMultiEntityFactory<K, E, F>](ScriptableMultiEntityFactory%601.md)

---

## 🏹 Methods

#### `Create(K)`

```csharp
E Create(K key);
```

- **Description:** Creates a new entity associated with the specified key.
- **Parameter:** `key` — The key identifying the entity to create.
- **Returns:** A new instance of type `E`.

#### `TryCreate(K, out E)`

```csharp
bool TryCreate(K key, out E entity);
```

- **Description:** Attempts to create a new entity associated with the specified key.
- **Parameters:**
    - `key` — The key identifying the entity to create.
    - `entity` — When the method returns, contains the created entity if the key exists; otherwise, the default value of
      `E`.
- **Returns:** `true` if the entity was created successfully; otherwise, `false`.

#### `Contains(K)`

```csharp
bool Contains(K key);
```

- **Description:** Determines whether an entity associated with the specified key exists.
- **Parameter:** `key` — The key to check.
- **Returns:** `true` if an entity with the given key exists; otherwise, `false`.

---

## 🗂 Example of Usage

Below is example of creating enemies through the multi entity factory:

```csharp
// Enum of enemy types
public enum EnemyType 
{
    Orc,
    Goblin,
    Troll
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