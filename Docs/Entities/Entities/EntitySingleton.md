# 🧩 EntitySingleton&lt;E&gt;

Represents an abstract class for **singleton entities**. Ensures a single globally accessible entity
of type `E`. Combines two patterns — **Entity-State-Behaviour**
and [Singleton](https://en.wikipedia.org/wiki/Singleton_pattern).

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Basic Usage](#ex1)
    - [Using entity factory](#ex2)
    - [Resetting singleton](#ex3)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [String-keyed Constructor](#string-keyed-constructor)
        - [Int-keyed Constructor](#int-keyed-constructor)
        - [Capacity-based Constructor](#capacity-based-constructor)
    - [Static Properties](#-static-properties)
        - [Instance](#instance)
    - [Static Methods](#-static-methods)
        - [SetFactory(IEntityFactory<E>)](#setfactoryientityfactorye)
        - [DisposeInstance()](#disposeinstance)
        - [ResetInstance()](#resetinstance)
- [Notes](#-notes)

---

## 🗂 Examples of Usage

Below are examples of using `EntitySingleton`:

<div id="ex1"></div>

### 1️⃣ Basic Usage

1. Create a specific type of singleton entity

```csharp
public class GameContext : EntitySingleton<GameContext>
{
}
```

2. Use singleton in your project

```csharp
// Get global instance
GameContext context = GameContext.Instance;

// Use as usual entity
context.AddValue("Score", 42);
context.AddBehaviour<EnemySpawnBehaviour>();
context.Init();
```

---

<div id="ex2"></div>

### 2️⃣ Using entity factory

Also, you may want to create a custom instantiation logic for your singleton. For this, use the special factory api.

1. Create a specific type of singleton entity with the default and your custom constructors.

```csharp
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

2. Create specific factory for your singleton implementing [IEntityFactory\<E>](../Factories/IEntityFactory%601.md)

```csharp
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

3. Register this factory before the first access to the `Instance` property of the entity singleton:

```csharp
var factory = new GameContextFactory("GameContext", tagCapacity: 16, valueCapacity: 32, behaviourCapacity: 8);
GameContext.SetFactory(factory);
```

4. Usage in a project

```csharp
// GameContextFactory is invoked under the hood
GameContext context = GameContext.Instance;

// Use as usual entity
context.AddValue("Score", 42);
context.AddBehaviour<EnemySpawnBehaviour>();
context.Init();
```

> [!NOTE]
> Also, you can upgrade factory to the builder pattern. Read more
> in [best practices](../../BestPractices/UpgradingEntityFactoryToBuilder.md).

---

<div id="ex3"></div>

### 3️⃣ Resetting singleton

If current instance is not valid you can drop or recreate this instance

```csharp
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

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public abstract class EntitySingleton<E> : Entity 
    where E : EntitySingleton<E>, new()
```

- **Type Parameter:** `E` — The concrete entity singleton type.
- **Inheritance:** [Entity](Entity.md)
- **Notes:**
    - Subclass must inherit from `EntitySingleton<E>`
    - Supports both **default constructor** and **[factory-based creation](../Factories/IEntityFactory.md)**
      via [SetFactory()](#setfactoryientityfactorye).

---

<div id="-constructors"></div>

### 🏗️ Constructors

Below are represent three ways of creating entity. When an entity is created through its constructor, it automatically
registers itself in a special registry that stores
all entities — [EntityRegistry](../Registry/EntityRegistry.md)

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
  Initializes internal structures efficiently.
- **Parameters:**
    - `name` – The name of the entity. If `null`, an empty string is used.
    - `tagCapacity` – Initial capacity for tag storage to minimize memory allocations.
    - `valueCapacity` – Initial capacity for value storage to minimize memory allocations.
    - `behaviourCapacity` – Initial capacity for behaviour storage to minimize memory allocations.
    - `settings` – Optional entity settings. If `null`, `Settings.disposeValues` defaults to `true`.

---

### 🔑 Static Properties

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

### 🏹 Static Methods

#### `SetFactory(IEntityFactory<E>)`

```csharp
public static void SetFactory(IEntityFactory<E> factory)
```

- **Description:** Registers a custom factory method for creating the singleton instance.
- **Parameter:** `factory` — Factory that returns a new instance of `E`.
- **Throws** `ArgumentNullException` if `factory` is `null`.
- **Note**: **Must be called **before** the first access to `Instance`.

#### `DropInstance()`

```csharp
public static void DropInstance()
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