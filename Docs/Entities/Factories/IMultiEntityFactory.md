# 🧩 IMultiEntityFactory

```csharp
public interface IMultiEntityFactory : IMultiEntityFactory<string, IEntity>
```

- **Description:** A non-generic registry interface for storing and retrieving entity factories by string key.
- **Inheritance:** [IMultiEntityFactory\<TKey, E>](IMultiEntityFactory%601.md)
- **See also:** [MultiEntityFactory](MultiEntityFactory.md), [ScriptableMultiEntityFactory](ScriptableMultiEntityFactory.md)

---

## 🏹 Methods

#### `Create(string)`

```csharp
public IEntity Create(string key);  
```

- **Description:** Creates an entity using the factory associated with the specified key.
- **Parameter:** `key` — The key of the factory to use.
- **Returns:** A new instance of type `IEntity`.

#### `TryCreate(string, out IEntity)`

```csharp
public bool TryCreate(string key, out IEntity entity);  
```

- **Description:** Attempts to create an entity using the factory associated with the specified key.
- **Parameters:**
  - `key` — The key of the factory to use.
  - `entity` — When this method returns, contains the created entity if the key was found; otherwise, `null`.
- **Returns:** `true` if the entity was created successfully; otherwise, `false`.

#### `Contains(string)`

```csharp
public bool Contains(string key);  
```

- **Description:** Determines whether a factory with the specified key exists in the registry.
- **Parameter:** `key` — The key to locate.
- **Returns:** `true` if the factory exists; otherwise, `false`.

---

## 🗂 Example of Usage

```csharp
// Assume we have an instance of multi factory  
IMultiEntityFactory multiFactory = ...

// Usage  
if (multiFactory.Contains("Orc"))  
{  
    IEntity orc = multiFactory.Create("Orc");  
}

if (multiFactory.TryCreate("Goblin", out IEntity goblin))  
{  
    // use goblin  
}  
```