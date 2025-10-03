# 🧩 IMultiEntityFactory

```csharp
public interface IMultiEntityFactory : IMultiEntityFactory<string, IEntity>
```

- **Description:** A non-generic registry interface for creating and managing entities using string keys.
- **Inheritance:** [IMultiEntityFactory\<TKey, E>](IMultiEntityFactory%601.md)
- **See also:** [MultiEntityFactory](MultiEntityFactory.md),
  [ScriptableMultiEntityFactory](ScriptableMultiEntityFactory.md)

---

## 🏹 Methods

#### `Create(string)`

```csharp
public IEntity Create(string key);  
```

- **Description:** Creates a new entity associated with the specified key.
- **Parameter:** `key` — The string key identifying the entity to create.
- **Returns:** A new instance of type `IEntity`.

#### `TryCreate(string, out IEntity)`

```csharp
public bool TryCreate(string key, out IEntity entity);  
```

- **Description:** Attempts to create a new entity associated with the specified key.
- **Parameters:**
    - `key` — The string key identifying the entity to create.
    - `entity` — When the method returns, contains the created entity if the key exists; otherwise, `null`.
- **Returns:** `true` if the entity was created successfully; otherwise, `false`.

#### `Contains(string)`

```csharp
public bool Contains(string key);  
```

- **Description:** Determines whether an entity associated with the specified key exists.
- **Parameter:** `key` — The string key to check.
- **Returns:** `true` if an entity with the given key exists; otherwise, `false`.

---

## 🗂 Example of Usage

```csharp
// Assume we have an instance of a multi-entity factory
IMultiEntityFactory multiFactory = ...

// Check if an entity exists and create it
if (multiFactory.Contains("Orc"))  
{  
    IEntity orc = multiFactory.Create("Orc");  
}

// Try creating an entity safely
if (multiFactory.TryCreate("Goblin", out IEntity goblin))  
{  
    // use goblin
}
```