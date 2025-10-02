# 🧩 IMultiEntityFactory

```csharp
public interface IMultiEntityFactory : IMultiEntityFactory<string, IEntity>
```

- **Description:** A non-generic registry interface for storing and retrieving entity factories by key.
- **Inheritance:** [IMultiEntityFactory\<TKey, E>](IMultiEntityFactory%601.md)
- **See also:** [MultiEntityFactory](MultiEntityFactory.md)

---

## 🏹 Methods

#### `Register(string, IEntityFactory<IEntity>)`

```csharp
public void Register(string key, IEntityFactory<IEntity> factory);
```

- **Description:** Registers an entity factory with the specified string key.
- **Parameters:**
    - `key` — The string key to associate with the factory.
    - `factory` — The factory instance to register.

#### `Unregister(string)`

```csharp
public void Unregister(string key);
```

- **Description:** Removes the entity factory associated with the specified key.
- **Parameter:** `key` — The key of the factory to remove.

#### `Create(string)`

```csharp
public IEntity Create(string key);
```

- **Description:** Creates an entity using the factory associated with the specified key.
- **Parameter:** `key` — The key of the factory to use.
- **Returns:** A new instance of type `IEntity`.

---

## 🗂 Example of Usage

```csharp
//Assume we have instance of multi factory
IMultiEntityFactory multiFactory = ...

// Registers factories for different entities
multiFactory.Register("Orc", new InlineEntityFactory<IEntity>(() => new EnemyEntity("Orc")));
multiFactory.Register("Goblin", new InlineEntityFactory<IEntity>(() => new EnemyEntity("Goblin")));

//Usage
IEntity orc = multiFactory.Create("Orc");
IEntity goblin = multiFactory.Create("Goblin");
```