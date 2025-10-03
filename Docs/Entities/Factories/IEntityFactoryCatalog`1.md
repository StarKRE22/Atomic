# 🧩️ IEntityFactoryCatalog&lt;TKey, E&gt;

```csharp
public interface IEntityFactoryCatalog<TKey, E> : IReadOnlyDictionary<TKey, IEntityFactory<E>>
    where E : IEntity
```

- **Description:** Represents a **read-only catalog of entity factories**, indexed by a key of type `TKey`.  
  Provides structured, lookup-efficient access to entity creation logic.
- **Inheritance:** `IReadOnlyDictionary<K, V>`
- **Type Parameters:**
    - `TKey` — the type of the key used to identify factories (e.g., `string`, `enum`).
    - `E` — the type of entity each factory creates, constrained to `IEntity`.
- **Note:** Serves as a centralized registry of factories, ensuring that entities can be created consistently based on
  their key.
- **See also:** [IEntityFactoryCatalog](IEntityFactoryCatalog.md), [IEntityFactory\<E>](IEntityFactory%601.md)

---

## 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** Gets the number of registered factories.

#### `Keys`

```csharp
public IEnumerable<TKey> Keys { get; }
```

- **Description:** Gets an enumerable collection of all keys available in the catalog.

#### `Values`

```csharp
public IEnumerable<IEntityFactory<E>> Values { get; }
```

- **Description:** Gets an enumerable collection of all factories.

---

## 🏷️ Indexer

#### `this[TKey key]`

```csharp
public IEntityFactory<E> this[TKey key] { get; }
```

- **Description:** Gets the entity factory associated with the specified key.
- **Parameters:** `key` — the key identifying the factory.
- **Returns:** An `IEntityFactory<E>` for the given key.
- **Exceptions:** Throws `KeyNotFoundException` if the key is not present.

---

## 🏹 Methods

#### `ContainsKey(TKey)`

```csharp
public bool ContainsKey(TKey key);
```

- **Description:** Determines whether the catalog contains a factory for the specified key.
- **Parameters:** `key` — the key to locate.
- **Returns:** `true` if the key exists; otherwise, `false`.

#### `TryGetValue(TKey, out IEntityFactory<E>)`

```csharp
public bool TryGetValue(TKey key, out IEntityFactory<E> factory);
```

- **Description:** Attempts to get a factory associated with the specified key.
- **Parameters:**
    - `key` — the key to locate.
    - `factory` — when this method returns, contains the factory if found; otherwise, `null`.
- **Returns:** `true` if the factory is found; otherwise, `false`.

#### `GetEnumerator()`

```csharp
public IEnumerator<KeyValuePair<TKey, IEntityFactory<E>>> GetEnumerator();
```

- **Description:** Returns an enumerator that iterates through the catalog.

---

## 🗂 Example of Usage

Use `EntityFactoryCatalog` with `MultiEntityFactory`

```csharp
// Suppose we have factories for game entities (e.g., Units, Buildings)
IEntityFactoryCatalog<string, IEntity> catalog = GetEntityFactoryCatalog();

// Check if a factory exists
if (catalog.ContainsKey("Soldier"))
{
    var soldierFactory = catalog["Soldier"];
    var soldier = soldierFactory.Create(); // Create an entity instance
}

// Safe retrieval
if (catalog.TryGetValue("Tank", out var tankFactory))
{
    var tank = tankFactory.Create();
}

// Enumerate all factories
foreach (var kvp in catalog)
{
    Console.WriteLine($"Key: {kvp.Key}, Factory: {kvp.Value}");
}
```

!!!

!!!
