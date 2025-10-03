






# 🧩️ IEntityFactoryCatalog

`IEntityFactoryCatalog` is a **read-only catalog of entity factories**, providing a structured and lookup-efficient way to access creation logic for `IEntity` instances.  
It supports both **generic key types** and a common **string-keyed shortcut**.

---

## Key Features

- **Read-Only Lookup** – Provides dictionary-like access to factories without allowing modification.
- **Generic & String-Keyed** – Supports arbitrary keys (`TKey`) or a string shortcut for convenience.
- **Factory Integration** – Values are `IEntityFactory<E>` instances, encapsulating entity creation logic.
- **Structured Access** – Enables organized management of multiple entity types.

---

## Interface: IEntityFactoryCatalog

```csharp
public interface IEntityFactoryCatalog : IEntityFactoryCatalog<string, IEntity>
{
}
```
- Shortcut for the common case: mapping entity factories **by name**.
- Useful in registries, managers, or systems that load entities dynamically at runtime.

---

## Interface: IEntityFactoryCatalog<TKey, E>

```csharp
public interface IEntityFactoryCatalog<TKey, E> : IReadOnlyDictionary<TKey, IEntityFactory<E>> where E : IEntity
{
}
```

- **Generic catalog** that maps keys of type `TKey` to factories producing entities of type `E`.
- Extends `IReadOnlyDictionary<TKey, IEntityFactory<E>>`, providing efficient key-based access.
- Enables type-safe and structured organization of entity factories.
- Can be used in both runtime and editor workflows for dynamic entity creation.

---

## Example Usage

### Example #1. String-Keyed Catalog

```csharp
public sealed class EntityCatalog : IEntityFactoryCatalog
{
    public int Count => _factories.Count;
    
    private readonly Dictionary<string, IEntityFactory> _factories = new();

    public EntityCatalog()
    {
        _factories["Orc"] = new InlineEntityFactory(() => new Entity("Orc"));
        _factories["Goblin"] = new InlineEntityFactory(() => new Entity("Goblin"));
    }

    public IEntityFactory this[string key] => _factories[key];
  
    public IEnumerable<string> Keys => _factories.Keys;
  
    public IEnumerable<IEntityFactory> Values => _factories.Values;
  
    public bool ContainsKey(string key) => _factories.ContainsKey(key);
  
    public IEnumerator<KeyValuePair<string, IEntityFactory>> GetEnumerator() => _factories.GetEnumerator();
  
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => _factories.GetEnumerator();
  
    public bool TryGetValue(string key, out IEntityFactory value) => _factories.TryGetValue(key, out value);
}
```

> Useful for dynamically creating entities by string identifiers.