# 🧩 MultiEntityFactory<K, E>

```csharp
public class MultiEntityFactory<K, E> : IMultiEntityFactory<K, E> where E : IEntity
```

- **Description:** A generic implementation for creating multiple entities identified by a key using entity factories.
- **Inheritance:**  [IMultiEntityFactory\<K, E>](IMultiEntityFactory%601.md)
- **Type Parameters:**
    - `K` — The type of key used to identify factories.
    - `E` — The type of entity created by the factories. Must implement [IEntity](../Entities/IEntity.md).
- **See also:** [MultiEntityFactory](MultiEntityFactory.md)

---

## 🏗️ Constructors

#### `Default Constructor`

```csharp
public MultiEntityFactory();
```

- **Description:** Initializes a new, empty factory.

#### `Enumerable Constructor`

```csharp
public MultiEntityFactory(IEnumerable<KeyValuePair<K, IEntityFactory<E>>> factories);
```

- **Description:** Initializes the factory with a collection of key-factory pairs.
- **Parameter:** `factories` — The key-factory pairs to initialize with.

#### `Dictionary Constructor`

```csharp
public MultiEntityFactory(IReadOnlyDictionary<K, IEntityFactory<E>> factories);
```

- **Description:** Initializes the factory with a read-only dictionary of key-factory pairs.
- **Parameter:** `factories` — The key-factory dictionary to initialize with.

#### `Params Constructor`

```csharp
public MultiEntityFactory(params KeyValuePair<K, IEntityFactory<E>>[] factories);
```

- **Description:** Initializes the factory with a params array of key-factory pairs.
- **Parameter:** `factories` — The key-factory pairs to initialize with.

---

## 🏹 Methods

#### `Register(K, IEntityFactory<E>)`

```csharp
public void Register(K key, IEntityFactory<E> factory)
```

- **Description:** Registers a new entity with the specified key.
- **Parameters:**
    - `key` — The key to associate with the entity.
    - `factory` — The entity instance to register.
- **Exceptions:** Throws `ArgumentException` if the key already exists.

#### `Unregister(K)`

```csharp
public void Unregister(K key)
```

- **Description:** Unregisters the entity associated with the specified key.
- **Parameter:** `key` — The key of the entity to remove.
- **Remarks:** If the key does not exist, the method does nothing.

#### `Create(K)`

```csharp
public E Create(K key)
```

- **Description:** Creates a new entity associated with the specified key.
- **Parameter:** `key` — The key identifying the entity to create.
- **Returns:** A new instance of type `E`.

#### `TryCreate(K, out E)`

```csharp
public bool TryCreate(K key, out E entity)
```

- **Description:** Attempts to create a new entity associated with the specified key.
- **Parameters:**
    - `key` — The key identifying the entity to create.
    - `entity` — When the method returns, contains the created entity if the key exists; otherwise, the default value of
      `E`.
- **Returns:** `true` if the entity was created successfully; otherwise, `false`.

#### `Contains(K)`

```csharp
public bool Contains(K key)
```

- **Description:** Determines whether an entity associated with the specified key exists.
- **Parameter:** `key` — The key to check.
- **Returns:** `true` if an entity with the given key exists; otherwise, `false`.

---

## 🗂 Example of Usage

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
var factory = new MultiEntityFactory<EnemyType, EnemyEntity>();

factory.Register(EnemyType.Orc, new InlineEntityFactory<EnemyEntity>(
    () => new EnemyEntity"Orc")
);
factory.Register(EnemyType.Goblin, new InlineEntityFactory<EnemyEntity>(
    () => new EnemyEntity("Goblin"))
);

EnemyEntity orc = factory.Create(EnemyType.Orc);
EnemyEntity goblin = factory.Create(EnemyType.Goblin);
```