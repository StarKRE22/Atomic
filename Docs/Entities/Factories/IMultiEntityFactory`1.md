

# 🧩️ IMultiEntityFactory\<TKey, E>

```csharp
public interface IMultiEntityFactory<in TKey, E> where E : IEntity
```

- **Description:** Generic registry interface for storing and retrieving entity factories by key.
- **Type Parameters:**
    - `TKey` — The type of the key used to identify factories.
    - `E` — The type of entity created by the factories. Must implement [IEntity](../Entities/IEntity.md).
- **See also:** [IMultiEntityFactory](IMultiEntityFactory.md), [MultiEntityFactory\<E>](MultiEntityFactory%601.md)

---

## 🏹 Methods

#### `Register(TKey, IEntityFactory<E>)`

```csharp
public void Register(TKey key, IEntityFactory<E> factory);
```

- **Description:** Registers an entity factory with the specified key.
- **Parameters:**
    - `key` — The key to associate with the factory.
    - `factory` — The factory instance to register.

#### `Unregister(TKey)`

```csharp
public void Unregister(TKey key);
```

- **Description:** Removes the entity factory associated with the specified key.
- **Parameter:** `key` — The key of the factory to remove.

#### `Create(TKey)`

```csharp
public E Create(TKey key);
```
- **Description:** Creates an entity using the factory associated with the specified key.
- **Parameter:** `key` — The key of the factory to use.
- **Returns:** A new instance of type `E`.

---

## 🗂 Example of Usage

Below is example of creating enemies through multi entity factory: 

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
// Assume we have an instance of MultiEnttiyFactory
IMultiEntityFactory<EnemyType, EnemyEntity> multiFactory = ...

// For example: registers inline factories     
multiFactory.Register(EnemyType.Orc, new InlineEntityFactory<EnemyEntity>(
    () => new EnemyEntity("Orc"))
);
multiFactory.Register(EnemyType.Goblin, new InlineEntityFactory<EnemyEntity>(
    () => new EnemyEntity("Goblin"))
);
```

```csharp
//Usage
var orc = multiFactory.Create(EnemyType.Orc);
var goblin = multiFactory.Create(EnemyType.Goblin);
```