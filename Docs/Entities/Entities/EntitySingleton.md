# EntitySingleton\<E\>

Abstract base class for **singleton entities**.  
Ensures a single globally accessible instance of type `E`.  
Supports both **default constructor** and **factory-based creation**.

This class extends `Entity` and combines the **Entity–State–Behaviour** model with the **Singleton pattern**.

---

## Key Features
- **Singleton Pattern** – Guarantees only one instance of the entity type
- **Lazy Initialization** – Instance is created on first access
- **Factory Support** – Allows registering a custom factory for creation
- **Lifecycle Integration** – Singleton can be disposed and recreated
- **Entity Powered** – Inherits all features of `Entity` (state, behaviours, tags, values, events)

---

## Thread Safety
- Entity is **NOT thread-safe**
- All operations should be performed on the main thread
- Use synchronization if accessing from multiple threads

---

## Type Parameters

- `E` — The concrete entity singleton type.  
  Must inherit from `EntitySingleton<E>` and provide either:
  - a **public parameterless constructor**, or
  - a registered factory via `SetFactory`.

---

## Properties

- `static E Instance`  
  Returns the **global singleton instance** of type `E`.
  - Created via registered factory (`IEntityFactory<E>`) if available.
  - Falls back to `new()` if no factory is registered.
  - Always returns the same instance until disposed.

---

## Methods

- `static void SetFactory(IEntityFactory<E> factory)` — registers a custom factory method for creating the singleton instance.  
  - Must be called **before** the first access to `Instance`.
  - `factory` — Factory that returns a new instance of `E`.
  - Throws `ArgumentNullException` if `factory` is `null`.

---

- `static void DisposeInstance()` — disposes the current singleton instance (if any) and clears it. After disposal, the next access to `Instance` will create a new one (via factory or constructor).

---

- `static E ResetInstance()` — disposes the current singleton instance (if any) and **immediately creates a new one**.  

---

## Constructors
#### Creates a new entity with the specified name, tags, values, behaviours, and optional settings.
```csharp
protected EntitySingleton(
    string name,
    IEnumerable<string> tags,
    IEnumerable<KeyValuePair<string, object>> values,
    IEnumerable<IEntityBehaviour> behaviours,
    Settings? settings = null
)
```
- `name` — Entity name.
- `tags` — Optional initial tags (`string` identifiers).
- `values` — Optional initial values (`string` keys).
- `behaviours` — Optional initial behaviours.
- `settings` — Optional entity settings. Defaults to `disposeValues = true`.
---
#### Creates a new entity with the specified name, tags, values, behaviours, and optional settings.
```csharp
protected EntitySingleton(
    string name,
    IEnumerable<int> tags,
    IEnumerable<KeyValuePair<int, object>> values,
    IEnumerable<IEntityBehaviour> behaviours,
    Settings? settings = null
)
```
- `name` — Entity name.
- `tags` — Optional initial tags (`int` identifiers).
- `values` — Optional initial values (`int` keys).
- `behaviours` — Optional initial behaviours.
- `settings` — Optional entity settings. Defaults to `disposeValues = true`.
---
#### Creates a new entity with the specified name and initial capacities for tags, values, and behaviours.
```csharp
protected EntitySingleton(
    string name = null,
    int tagCapacity = 0,
    int valueCapacity = 0,
    int behaviourCapacity = 0,
    Settings? settings = null
)
```
- `name` — Entity name.
- `tagCapacity` — Initial capacity for tags.
- `valueCapacity` — Initial capacity for values.
- `behaviourCapacity` — Initial capacity for behaviours.
- `settings` — Optional entity settings. Defaults to `disposeValues = true`.
---

## Examples

### Example #1. Default constructor

```csharp
public class GameContext : EntitySingleton<GameContext>
{
}

// Usage
GameManager.Instance.AddValue("Score", 42);
Console.WriteLine(GameManager.Instance.GetValue<int>("Score")); // 42
```
----

### Example #2. With custom factory
```csharp
public sealed class GameContext : EntitySingleton<GameContext>, IGameContext
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

// Register before usage
var factory = new GameContextFactory("MainGame", tagCapacity: 16, valueCapacity: 32, behaviourCapacity: 8);
EntitySingleton<GameContext>.SetFactory(factory );

// Usage
GameContext.Instance.Init();
GameContext.Instance.Enable();
```

### Example #3. With custom builder
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

// Register before usage
var builder = new GameContextBuilder()
    .WithName("MainGame")
    .WithTagCapacity(16)
    .WithValueCapacity(32)
    .WithBehaviourCapacity(8);
    
EntitySingleton<GameContext>.SetFactory(builder);

// Usage
GameContext.Instance.Init();
GameContext.Instance.Enable();
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
## Remarks
- Use `SetFactory` if your singleton requires **arguments** or **custom setup**.
- Use `DisposeInstance` or `ResetInstance` in **tests** or when context changes (e.g., editor mode vs runtime).
- For regular global managers, the default `new()` constructor is usually enough.



























[//]: # (# 🧩️ EntitySingleton\<E\>)

[//]: # ()
[//]: # (Abstract base class for **singleton entities**.  )

[//]: # (Ensures a single globally accessible instance of type `<typeparamref name="E"/>`.)

[//]: # ()
[//]: # (This class extends `Entity` and follows the **Singleton Pattern**, allowing only one instance of the entity type to exist during runtime.)

[//]: # ()
[//]: # (---)

[//]: # ()
[//]: # (## Key Features)

[//]: # (- **Singleton Pattern** – Guarantees only one instance of the entity type)

[//]: # (- **Lazy Initialization** – Instance is created only on first access)

[//]: # (- **Global Access** – Provides static property `Instance` for retrieval)

[//]: # (- **Entity Integration** – Inherits all features of `Entity` &#40;lifecycle, behaviours, events, values, tags, etc.&#41;)

[//]: # ()
[//]: # (---)

[//]: # ()
[//]: # (## Type Parameters)

[//]: # ()
[//]: # (- `E` — The concrete entity singleton type.  )

[//]: # (  Must inherit from `EntitySingleton<E>` and provide a **public parameterless constructor**.)

[//]: # ()
[//]: # (---)

[//]: # ()
[//]: # (## Properties)

[//]: # ()
[//]: # (- `static E Instance`  )

[//]: # (  Returns the **global singleton instance** of type `E`.  )

[//]: # (  If the instance does not exist, it is created automatically on first access.)

[//]: # ()
[//]: # (---)

[//]: # ()
[//]: # (## Thread Safety)

[//]: # ()
[//]: # (- `EntitySingleton<E>` is **NOT thread-safe**.)

[//]: # (- The `Instance` property should only be accessed from the **main thread**.)

[//]: # (- Use explicit synchronization if multi-threaded access is required.)

[//]: # ()
[//]: # (---)

[//]: # ()
[//]: # (## Usage Examples)

[//]: # ()
[//]: # (### Example #1. Using a game context as singleton)

[//]: # (```csharp)

[//]: # (public sealed class GameContext : EntitySingleton<GameManager>)

[//]: # ({)

[//]: # (})

[//]: # ()
[//]: # ()
[//]: # (// Access the singleton instance globally)

[//]: # (GameContext.Instance.AddValue&#40;"GameTime", 60.0f&#41;;)

[//]: # (GameContext.Instance.AddValue&#40;"Score", 10&#41;;)

[//]: # (GameContext.Instance.AddValue&#40;"Money", 50&#41;;)

[//]: # (```)

[//]: # (> Note: не имеет возможности делать прекомпиляцию тэгов, capacity, и behaviours, нет имени и так далее!)

[//]: # ()
[//]: # ()
[//]: # (### )

[//]: # ()
[//]: # (```csharp)

[//]: # (public sealed class GameContext : Entity, IGameContext)

[//]: # ({)

[//]: # (    public static GameContext Instance;)

[//]: # ()
[//]: # (    public GameContext&#40;)

[//]: # (        string name = null,)

[//]: # (        int tagCapacity = 0,)

[//]: # (        int valueCapacity = 0,)

[//]: # (        int behaviourCapacity = 0)

[//]: # (    &#41; : base&#40;name, tagCapacity, valueCapacity, behaviourCapacity&#41;)

[//]: # (    {)

[//]: # (        Instance = this;)

[//]: # (    })

[//]: # ()
[//]: # (    protected override void OnDispose&#40;&#41;)

[//]: # (    {)

[//]: # (        Instance = null;)

[//]: # (    })

[//]: # (})

[//]: # (```)