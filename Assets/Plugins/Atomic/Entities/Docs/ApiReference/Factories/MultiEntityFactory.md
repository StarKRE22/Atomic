# 🧩 MultiEntityFactory

`MultiEntityFactory` is a specialized registry for managing entity factories (`IEntityFactory`)  
using keys for registration and lookup. It provides **dynamic entity creation**, factory registration,  
and removal in a **type-safe** manner.

---

## Key Features

- **String-Keyed and Generic** – Supports both string keys and generic keys (`TKey`).
- **Dynamic Registration** – Factories can be added or removed at runtime.
- **Type-Safe Creation** – The `Create` method returns a strongly-typed entity `E`.
- **Flexible Initialization** – Can be initialized from a catalog, a collection, or a params array.
- **Lightweight** – Factories are reused; entities are created only when `Create()` is called.

---

## Classes

### MultiEntityFactory (Non-generic)

```csharp
public class MultiEntityFactory : MultiEntityFactory<string, IEntity>, IMultiEntityFactory
```
- Implementation using **string keys**.
- Can be initialized in several ways:

| Constructor                                                                                | Description                                                                         |
|--------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------|
| `MultiEntityFactory()`                                                                     | Initializes an empty factory registry.                                              |
| `MultiEntityFactory(IReadOnlyDictionary<string, IEntityFactory<IEntity>> factories)`       | Initializes the registry using a read-only dictionary of string keys and factories. |
| `MultiEntityFactory(IEnumerable<KeyValuePair<string, IEntityFactory<IEntity>>> factories)` | Initializes the registry using a collection of key-factory pairs.                   |
| `MultiEntityFactory(params KeyValuePair<string, IEntityFactory<IEntity>>[] factories)`     | Initializes the registry using a params array of key-factory pairs.                 |


### MultiEntityFactory<TKey, E> (generic)

```csharp
public class MultiEntityFactory<TKey, E> : IMultiEntityFactory<TKey, E> where E : IEntity
```
- Generic implementation for **any key type**.
- Stores factories in an internal `Dictionary<TKey, IEntityFactory<E>>`.
- Provides methods to dynamically manage factories and create entities.

#### Constructors

| Constructor                                                                        | Description                                                       |
|------------------------------------------------------------------------------------|-------------------------------------------------------------------|
| `MultiEntityFactory()`                                                             | Initializes an empty factory dictionary.                          |
| `MultiEntityFactory(IEnumerable<KeyValuePair<TKey, IEntityFactory<E>>> factories)` | Initializes the factory with a collection of key-factory pairs.   |
| `MultiEntityFactory(IReadOnlyDictionary<TKey, IEntityFactory<E>> factories)`       | Initializes the factory from a read-only dictionary of factories. |
| `MultiEntityFactory(params KeyValuePair<TKey, IEntityFactory<E>>[] factories)`     | Initializes the factory with a params array of key-factory pairs. |

#### Methods

| Method                                     | Description                                                                                                   |
|--------------------------------------------|---------------------------------------------------------------------------------------------------------------|
| `Add(TKey key, IEntityFactory<E> factory)` | Adds a new factory to the registry.                                                                           |
| `Remove(TKey key)`                         | Removes the factory associated with the specified key.                                                        |
| `Create(TKey key)`                         | Creates an entity using the factory associated with the key. Throws `KeyNotFoundException` if key is missing. |

---

## Example Usage

### String-Keyed

```csharp
var registry = new MultiEntityFactory();
registry.Add("Enemy", new InlineEntityFactory(() => new EnemyEntity()));
var entity = registry.Create("Enemy");
```

### Generic Key
```csharp
var goblinFactory = new MultiEntityFactory<EnemyType, EnemyEntity>();
goblinFactory.Add(EnemyType.Goblin, new GoblinFactory());
var goblin = goblinFactory.Create(EnemyType.Goblin);
```
- Supports **typed keys** (like enums) for safer lookups.
- Enables modular and dynamic management of entity creation.