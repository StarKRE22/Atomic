# 🧩 MultiEntityFactory<TKey, E>

```csharp
public class MultiEntityFactory<TKey, E> : IMultiEntityFactory<TKey, E> where E : IEntity
```

- **Description:** Stores and manages entity factories internally using a dictionary.
- **Inheritance:**  [IMultiEntityFactory\<TKey, E>](IMultiEntityFactory%601.md)
- **Type Parameters:**
    - `TKey` — The type of key used to identify factories.
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
public MultiEntityFactory(IEnumerable<KeyValuePair<TKey, IEntityFactory<E>>> factories);
```

- **Description:** Initializes the factory with a collection of key-factory pairs.
- **Parameter:** `factories` — The key-factory pairs to initialize with.

#### `Dictionary Constructor`

```csharp
public MultiEntityFactory(IReadOnlyDictionary<TKey, IEntityFactory<E>> factories);
```

- **Description:** Initializes the factory with a read-only dictionary of key-factory pairs.
- **Parameter:** `factories` — The key-factory dictionary to initialize with.

#### `Params Constructor`

```csharp
public MultiEntityFactory(params KeyValuePair<TKey, IEntityFactory<E>>[] factories);
```

- **Description:** Initializes the factory with a params array of key-factory pairs.
- **Parameter:** `factories` — The key-factory pairs to initialize with.

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

#### `Create(TKey key)`

```csharp
public E Create(TKey key);
```

- **Description:** Creates an entity using the factory associated with the specified key.
- **Parameter:** `key` — The key of the factory to use.
- **Returns:** A new instance of type `E`.
- **Throws:** `KeyNotFoundException` if no factory is registered for the given key.

---

## 🗂 Example of Usage

```csharp
var factory = new MultiEntityFactory<string, EnemyEntity>();

factory.Register("Orc", new InlineEntityFactory<EnemyEntity>(
    () => new EnemyEntity("Orc"))
);
factory.Register("Goblin", new InlineEntityFactory<EnemyEntity>(
    () => new EnemyEntity("Goblin"))
);

EnemyEntity orc = factory.Create("Orc");
EnemyEntity goblin = factory.Create("Goblin");
```