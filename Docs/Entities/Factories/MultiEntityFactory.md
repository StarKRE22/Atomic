




=====
=====

# 🧩 MultiEntityFactory

```csharp
public class MultiEntityFactory : MultiEntityFactory<string, IEntity>, IMultiEntityFactory
```



---

## 🏗️ Constructors

#### `Default Constructor`

```csharp
public MultiEntityFactory();
```

- **Description:** Initializes a new empty factory.

#### `Dictionary Constructor`

```csharp
public MultiEntityFactory(IReadOnlyDictionary<string, IEntityFactory<IEntity>> factories);
```

- **Description:** Initializes the factory using a catalog of scriptable entity factories.
- **Parameter:** `factories` — The map providing entity factories.

#### `Enumerable Constructor`

```csharp
public MultiEntityFactory(IEnumerable<KeyValuePair<string, IEntityFactory<IEntity>>> factories);
```

- **Description:** Initializes the factory using a collection of key-factory pairs.
- **Parameter:** `factories` — Key-factory pairs to initialize the factory with.

#### `Params Constructor`

```csharp
public MultiEntityFactory(params KeyValuePair<string, IEntityFactory<IEntity>>[] factories);
```

- **Description:** Initializes the factory using a params array of key-factory pairs.
- **Parameter:** `factories` — The key-factory pairs to initialize with.

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
- **Exceptions:**  
  Throws `ArgumentException` if a factory with the same key already exists.

---

#### `Unregister(string)`

```csharp
public void Unregister(string key);
```

- **Description:** Removes the entity factory associated with the specified key.
- **Parameter:** `key` — The key of the factory to remove.
- **Returns:** `void`
- **Notes:** If the key does not exist, the method does nothing.

---

#### `Create(string)`

```csharp
public IEntity Create(string key);
```

- **Description:** Creates an entity using the factory associated with the specified key.
- **Parameter:** `key` — The key of the factory to use.
- **Returns:** A new instance of type `IEntity`.
- **Exceptions:**  
  Throws `KeyNotFoundException` if no factory is registered for the given key.

---

## 🗂 Example of Usage

```csharp
IMultiEntityFactory factory = new MultiEntityFactory();

factory.Register("Orc", new InlineEntityFactory<IEntity>(() => new Entity("Orc")));
factory.Register("Goblin", new InlineEntityFactory<IEntity>(() => new Entity("Goblin")));

IEntity orc = factory.Create("Orc");
IEntity goblin = factory.Create("Goblin");
```