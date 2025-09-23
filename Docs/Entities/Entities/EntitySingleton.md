# 🧩 EntitySingleton&lt;E&gt;

Represents an abstract class for **singleton entities**. Ensures a single globally accessible entity of type `E`. This
class combines the **Entity–State–Behaviour** model with
the [Singleton Pattern](https://en.wikipedia.org/wiki/Singleton_pattern).
Supports both **default constructor** and **factory-based creation**.

```csharp
public abstract class EntitySingleton<E> : Entity 
    where E : EntitySingleton<E>, new()
```

- **Type Parameter:** `E` — The concrete entity singleton type.
- **Inheritance:** derived from [Entity](Entity.md)
- **Notes:**
    - Subclass must inherit from `EntitySingleton<E>`
    - Provide instantiation either a **public parameterless constructor** or a registered [factory](../Factories/IEntityFactory.md) via [SetFactory](#setfactoryientityfactorye).

---

### 🏗️ Constructors

#### `String-keyed Constructor`

```csharp
protected Entity(
    string name,
    IEnumerable<string> tags,
    IEnumerable<KeyValuePair<string, object>> values,
    IEnumerable<IEntityBehaviour> behaviours,
    Settings? settings = null
) 
```

- **Description:** Creates a new entity with the specified name, string tags, values, and behaviours. Initializes
  internal capacities and immediately adds all specified tags, values, and behaviours.
- **Parameters:**
    - `name` – The name of the entity. If `null`, an empty string is used.
    - `tags` – Optional collection of string tag identifiers.
    - `values` – Optional collection of key-value pairs.
    - `behaviours` – Optional collection of behaviours to attach.
    - `settings` – Optional entity settings. If `null`, `Settings.disposeValues` defaults to `true`.

#### `Int-keyed Constructor`

```csharp
protected Entity(
    string name,
    IEnumerable<int> tags,
    IEnumerable<KeyValuePair<int, object>> values,
    IEnumerable<IEntityBehaviour> behaviours,
    Settings? settings = null
)
```

- **Description:** Creates a new entity with the specified name, integer tags, values, and behaviours. Initializes
  internal capacities and immediately adds all specified tags, values, and behaviours.
- **Parameters:**
    - `name` – The name of the entity. If `null`, an empty string is used.
    - `tags` – Optional collection of integer tag identifiers.
    - `values` – Optional collection of key-value pairs with integer keys.
    - `behaviours` – Optional collection of behaviours to attach.
    - `settings` – Optional entity settings. If `null`, `Settings.disposeValues` defaults to `true`.

#### `Capacity-based Constructor`

```csharp
protected Entity(
    string name = null,
    int tagCapacity = 0,
    int valueCapacity = 0,
    int behaviourCapacity = 0,
    Settings? settings = null
) 
```

- **Description:** Creates a new entity with the specified name and initial capacities for tags, values, and behaviours.
  Initializes internal structures efficiently and registers the entity
  in [EntityRegistry](../Registry/EntityRegistry.md).
- **Parameters:**
    - `name` – The name of the entity. If `null`, an empty string is used.
    - `tagCapacity` – Initial capacity for tag storage to minimize memory allocations.
    - `valueCapacity` – Initial capacity for value storage to minimize memory allocations.
    - `behaviourCapacity` – Initial capacity for behaviour storage to minimize memory allocations.
    - `settings` – Optional entity settings. If `null`, `Settings.disposeValues` defaults to `true`.

---

## 🔑 Static Properties

#### `Instance`

```csharp
public static E Instance { get; }
```

- **Description:** Returns the **global singleton instance** of type `E`.
- **Notes:**
    - Created via registered factory ([IEntityFactory<E>](../Factories/IEntityFactory.md)) if available.
    - Falls back to `default constructor` if no factory is registered.
    - Always returns the same instance until disposed.

---

## 🏹 Static Methods

#### `SetFactory(IEntityFactory<E>)`

```csharp
public static void SetFactory(IEntityFactory<E> factory)
```

- **Description:** Registers a custom factory method for creating the singleton instance.
- **Parameter:** `factory` — Factory that returns a new instance of `E`.
- **Throws** `ArgumentNullException` if `factory` is `null`.
- **Note**: **Must be called **before** the first access to `Instance`.

#### `DisposeInstance()`

```csharp
public static void DisposeInstance()
```

- **Description:** Disposes the current singleton instance (if any) and clears it. After disposal, the
  next access to `Instance` will create a new one (via factory or constructor).

#### `ResetInstance()`

```csharp
public static E ResetInstance()
```

- **Description:** Disposes the current singleton instance (if any) and **immediately creates a new one**.
- **Returns** New singleton instance via **factory** or **default constructor**

---

## 🗂 Examples of Usage

### 🔹 Example #1. Using default constructor

```csharp
public class GameContext : EntitySingleton<GameContext>
{
}
```

```csharp
// Usage
GameContext context = GameContext.Instance;
context.AddValue("Score", 42);
context.AddBehaviour<EnemySpawnBehaviour>();
context.Init();
```

----

### 🔹 Example #2. Using entity factory

```csharp
//Custom entity
public sealed class GameContext : EntitySingleton<GameContext>
{
    //Constructor with precompiles memory
    public GameContext(
        string name = null,
        int tagCapacity = 0,
        int valueCapacity = 0,
        int behaviourCapacity = 0
    ) : base(name, tagCapacity, valueCapacity, behaviourCapacity)
    {
    }

    //Default constructor needed also for fallback
    public GameContext()
    {
    }
}
```

```csharp
// Factory implementation
public class GameContextFactory : IEntityFactory<GameContext>
{
    private readonly string _name;
    private readonly int _tagCapacity;
    private readonly int _valueCapacity;
    private readonly int _behaviourCapacity;

    public GameContextFactory(
        string name = "DefaultGame",
        int tagCapacity = 4,
        int valueCapacity = 8,
        int behaviourCapacity = 4
    )
    {
        _name = name;
        _tagCapacity = tagCapacity;
        _valueCapacity = valueCapacity;
        _behaviourCapacity = behaviourCapacity;
    }

    public GameContext Create()
    {
        return new GameContext(_name, _tagCapacity, _valueCapacity, _behaviourCapacity);
    }
}
```

```csharp
// Register before usage
var factory = new GameContextFactory("GameContext", tagCapacity: 16, valueCapacity: 32, behaviourCapacity: 8);
GameContext.SetFactory(factory);
```

```csharp
// Usage
GameContext context = GameContext.Instance;
context.AddValue("Score", 42);
context.AddBehaviour<EnemySpawnBehaviour>();
context.Init();
```

### 🔹 Example #3. Upgrading factory to builder

```csharp
// Builder-based factory implementation
public class GameContextBuilder : IEntityFactory<GameContext>
{
    private string _name = "DefaultGame";
    private int _tagCapacity = 4;
    private int _valueCapacity = 8;
    private int _behaviourCapacity = 4;

    public GameContextBuilder WithName(string name)
    {
        _name = name; return this;
    }

    public GameContextBuilder WithTagCapacity(int capacity)
    {
        _tagCapacity = capacity; return this;
    }

    public GameContextBuilder WithValueCapacity(int capacity)
    {
        _valueCapacity = capacity; return this;
    }

    public GameContextBuilder WithBehaviourCapacity(int capacity)
    {
        _behaviourCapacity = capacity; return this;
    }

    public GameContext Create() 
    {
        return new GameContext(_name, _tagCapacity, _valueCapacity, _behaviourCapacity);
    }
}
```

```csharp
// Register before usage
var builder = new GameContextBuilder()
    .WithName("GameContext")
    .WithTagCapacity(16)
    .WithValueCapacity(32)
    .WithBehaviourCapacity(8);
    
GameContext.SetFactory(builder);
```

```csharp
// Usage
GameContext context = GameContext.Instance;

context.AddValue("Score", 42);
context.AddBehaviour<EnemySpawnBehaviour>();
context.Init();
```

### Example #4. Resetting singleton

```csharp
// Get current instance
var oldContext = GameContext.Instance;

// Dispose 
GameContext.DisposeInstance();

// Create new instance immediately
var newContext = GameContext.Instance;
```

```csharp
//Or just invoke ResetInstance()
var oldContext = GameContext.Instance;
var newContext = GameContext.ResetInstance();
```

---

## 📝 Notes

- **Singleton Pattern** – Guarantees only one instance of the entity type
- **Global Access** – Provides static property `Instance` for retrieval)
- **Lazy Initialization** – Instance is created on first access
- **Factory Support** – Allows registering a custom factory for creation
- **Lifecycle Integration** – Singleton can be disposed and recreated
- **Entity Inheritance** – Inherits all features of `Entity` (state, behaviours, tags, values, events)
- **NOT thread-safe** — The `Instance` property should only be accessed from the **main thread**


- Use `SetFactory` if your singleton requires **arguments** or **custom setup**.
- Use `DisposeInstance` or `ResetInstance` in **tests** or when context changes (e.g., editor mode vs runtime).
- For regular global managers, the default `new()` constructor is usually enough.