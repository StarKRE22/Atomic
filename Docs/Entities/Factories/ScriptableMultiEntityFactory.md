# 🧩️ ScriptableEntityFactoryCatalog

`ScriptableEntityFactoryCatalog` is a **ScriptableObject-based catalog** for managing collections of `ScriptableEntityFactory<E>` instances.  
It provides a dictionary-like interface for retrieving entity factories by key, typically used for **runtime entity creation**, **prototyping**, or **editor tools**.

---

## Key Features

- **Unity Integration** – Built on top of `ScriptableObject`, can be created as an asset via the Inspector.
- **Read-Only Access** – Implements `IEntityFactoryCatalog<TKey, E>` for safe dictionary-like lookups.
- **Flexible Keys** – Supports generic keys (`TKey`) or common string-keyed usage.
- **Editor-Friendly** – Serialized factory array (`_factories`) can be displayed and managed in the Inspector.
- **Duplicate Detection** – Logs warnings if multiple factories share the same key.
- **Lazy Initialization** – The internal `_map` dictionary is built on first access.
- **Lightweight** – Factory references are reused; entities are created only when `Create()` is called.

---

## Class: ScriptableEntityFactoryCatalog

```csharp
[CreateAssetMenu(
    fileName = "EntityFactoryCatalog",
    menuName = "Atomic/Entities/New EntityFactoryCatalog"
)]
public class ScriptableEntityFactoryCatalog :
    ScriptableEntityFactoryCatalog<string, IEntity, ScriptableEntityFactory>,
    IEntityFactoryCatalog
{
    protected override string GetKey(ScriptableEntityFactory factory) => factory.name;
}
```
- Maps `ScriptableEntityFactory` assets by **name**.
- Can be used as a drop-in asset for dynamic entity creation, prototyping, or editor tools.
- Shortcut for the common case of a **string-keyed catalog**.

---

## Class: ScriptableEntityFactoryCatalog<TKey, E, F>

```csharp
public abstract class ScriptableEntityFactoryCatalog<TKey, E, F> :
    ScriptableObject, IEntityFactoryCatalog<TKey, E>
    where E : IEntity
    where F : ScriptableEntityFactory<E>
{
}
```

| Member                                               | Type                                                 | Description                                                                                      |
|------------------------------------------------------|------------------------------------------------------|--------------------------------------------------------------------------------------------------|
| `Count`                                              | `int`                                                | Returns the number of factories in the catalog. Lazy initialization via `EnsureInitialized()`.   |
| `Keys`                                               | `IEnumerable<TKey>`                                  | Returns all keys in the catalog.                                                                 |
| `Values`                                             | `IEnumerable<IEntityFactory<E>>`                     | Returns all factory instances in the catalog.                                                    |
| `ContainsKey(TKey key)`                              | `bool`                                               | Checks if a factory with the specified key exists.                                               |
| `TryGetValue(TKey key, out IEntityFactory<E> value)` | `bool`                                               | Attempts to retrieve a factory by key. Returns `true` if found.                                  |
| `this[TKey key]`                                     | `IEntityFactory<E>`                                  | Indexer for accessing a factory by key.                                                          |
| `GetEnumerator()`                                    | `IEnumerator<KeyValuePair<TKey, IEntityFactory<E>>>` | Returns an enumerator over key-factory pairs.                                                    |
| `IEnumerator IEnumerable.GetEnumerator()`            | `IEnumerator`                                        | Non-generic enumerator implementation.                                                           |
| `protected abstract GetKey(F factory)`               | `TKey`                                               | Extracts a key for the given factory. Must be implemented by derived classes.                    |

---

## Example Usage

### Example #1. String-Keyed Catalog

```csharp
var catalog = Resources.Load<ScriptableEntityFactoryCatalog>("EntityFactoryCatalog");
var factory = catalog["Enemy"];
var enemy = factory.Create();
```
- Loads a catalog asset from Resources.
- Retrieves a factory by **string name**.
- Creates a new entity using the retrieved factory.

---

### Example #2. Generic Key Catalog

```csharp
public enum EnemyType
{
    Orc,
    Goblin,
    Troll
}

public abstract class EnemyEntityFactory : ScriptableEntityFactory<EnemyEntity>
{
    [field: SerializeField]
    public EnemyType Type { get; private set;}

    public sealed override Enemy Create()
    {
        var entity = new Enemy(
            this.name,
            this.InitialTagCount,
            this.InitialValueCount,
            this.InitialBehaviourCount
        );
        entity.AddValue("EnemyType", this.Type);
        this.Install(entity);
        return entity;
    }

    protected abstract void Install(Enemy entity);
}

[CreateAssetMenu(
    fileName = "EnemyFactoryCatalog",
    menuName = "Entities/New EnemyFactoryCatalog"
)]
public sealed class EnemyFactoryCatalog : ScriptableEntityFactoryCatalog<EnemyType, EnemyEntity, EnemyEntityFactory>
{
    protected override EnemyType GetKey(EnemyEntityFactory factory) => factory.Type;
}
```
```csharp
var catalog = Resources.Load<EnemyFactoryCatalog>("EnemyFactoryCatalog");
var factory = catalog[EnemyType.Goblin];
var goblin = factory.Create();
```
- Uses a **typed key** (enum) for lookup.
- Provides type-safe access to factories and entities.

---

## Remarks
- Use **`ScriptableEntityFactoryCatalog`** for common string-keyed entity creation scenarios.
- Use **`ScriptableEntityFactoryCatalog<TKey, E, F>`** for more advanced cases with typed keys.
- The catalog is **lazy-initialized**: the `_map` dictionary is created on first access.
- Duplicate keys in `_factories` will log warnings and skip additional entries.
- Designed for **Editor-friendly workflows**, **runtime instantiation**, and **modular entity management**.
