# 🧩 IEntity

```csharp
public interface IEntity : IInitLifecycle, IEnableLifecycle, ITickLifecycle
``` 

- **Description:** Represents the fundamental interface of entity in the framework.
  It provides a modular container for **dynamic state**, **tags**, **values**, **behaviours**
  and **lifecycle management**.

- **Inheritance:**
    - [IInitLifecycle](../Lifecycle/Sources/IInitLifecycle.md) – Supports explicit initialization and disposal.
    - [IEnableLifecycle](../Lifecycle/Sources/IEnableLifecycle.md) – Supports runtime enabling and disabling.
    - [ITickLifecycle](../Lifecycle/Sources/ITickLifecycle.md) – Supports `Tick`, `FixedTick`, and `LateTick` callbacks.

- **Sections:**
  - [Core]
  - [Tags]
  - [Values]
  - [Behaviours]
  - [Lifecycle]

---


---


---


---

<details>
  <summary>
    <h2 id="-behaviour-members">⚙️ Behaviours</h2>
    <br>
    Manage modular logic attached to the entity. Behaviours implement 
    <a href="../Behaviours/IEntityBehaviour.md">IEntityBehaviour</a> interfaces and can be added, removed, queried, or enumerated at runtime. 
    This allows flexible composition of entity logic, enabling dynamic functionality without changing the core entity structure. 
    Behaviours can respond to lifecycle events (<code>Init</code>, <code>Enable</code>, <code>Tick</code>, <code>Disable</code>, <code>Dispose</code>), 
    enabling dynamic logic composition without changing the core entity structure.
  </summary>

<br>

> For behaviours entity acts as a container using a **List**, which means that all algorithmic operations have
> **List-like time complexity**.
> Additionally, the entity **can store multiple references to the same behaviour instance**,
> so duplicate entries are allowed.

---

### ⚡ Events

#### `OnBehaviourAdded`

```csharp
public event Action<IEntity, IEntityBehaviour> OnBehaviourAdded  
```

- **Description:** Triggered when a behaviour is added to the entity.
- **Parameters:**
    - `IEntity` – The entity where the behaviour was added.
    - `IEntityBehaviour` – The behaviour that was added.
- **Note:** Allows subscribers to react whenever a new behaviour is attached.

#### `OnBehaviourDeleted`

```csharp
public event Action<IEntity, IEntityBehaviour> OnBehaviourDeleted  
```

- **Description:** Triggered when a behaviour is removed from the entity.
- **Parameters:**
    - `IEntity` – The entity where the behaviour was removed.
    - `IEntityBehaviour` – The behaviour that was removed.
- **Note:** Useful for cleanup or reactive updates when behaviours are detached.

---

### 🔑 Properties

#### `BehaviourCount`

```csharp
public int BehaviourCount { get; }  
```

- **Description:** Number of behaviours currently attached to the entity.
- **Note:** Provides a quick way to check how many behaviours are associated with this entity.

---

### 🏹 Methods

#### `AddBehaviour(IEntityBehaviour)`

```csharp
public void AddBehaviour(IEntityBehaviour behaviour)  
```

- **Description:** Adds a behaviour to the entity.
- **Parameters:** `behaviour` – The behaviour instance to attach.
- **Triggers:** `OnBehaviourAdded` and `OnStateChanged`.
- **Exceptions:** Throws if `behaviour` is null.
- **Note:** Allows to add existing behaviours.

#### `GetBehaviour<T>()`

```csharp
public T GetBehaviour<T>() where T : IEntityBehaviour  
```

- **Description:** Gets the first behaviour of the specified type.
- **Returns:** The first attached behaviour of type `T`.
- **Exceptions:** Throws if no behaviour of type `T` exists.

#### `GetBehaviourAt(int)`

```csharp
public IEntityBehaviour GetBehaviour(int index)  
```

- **Description:** Returns the behaviour instance at the given index.
- **Parameters:** `index` – The zero-based index of the behaviour.
- **Returns:** The behaviour at the specified index.
- **Exceptions:** Throws if `index` is out of range.

#### `TryGetBehaviour<T>(out T)`

```csharp
public bool TryGetBehaviour<T>(out T behaviour) where T : IEntityBehaviour  
```

- **Description:** Tries to get a behaviour of the specified type.
- **Parameters:** `out behaviour` – Output parameter for the behaviour.
- **Returns:** `true` if a behaviour of type `T` exists, otherwise `false`.

#### `HasBehaviour(IEntityBehaviour)`

```csharp
public bool HasBehaviour(IEntityBehaviour behaviour)  
```

- **Description:** Checks if a specific behaviour exists.
- **Parameters:** `behaviour` – The behaviour instance to check.
- **Returns:** `true` if the behaviour is attached, otherwise `false`.

#### `HasBehaviour<T>()`

```csharp
public bool HasBehaviour<T>() where T : IEntityBehaviour  
```

- **Description:** Checks if a behaviour of the specified type exists.
- **Returns:** `true` if any behaviour of type `T` is attached, otherwise `false`.

#### `DelBehaviour(IEntityBehaviour)`

```csharp
public bool DelBehaviour(IEntityBehaviour behaviour)  
```

- **Description:** Removes a specific behaviour.
- **Parameters:** `behaviour` – The behaviour to remove.
- **Returns:** `true` if the behaviour existed and was removed, otherwise `false`.
- **Triggers:** `OnBehaviourDeleted` and `OnStateChanged`.

#### `DelBehaviour<T>()`

```csharp
public bool DelBehaviour<T>() where T : IEntityBehaviour  
```

- **Description:** Removes a behaviour of the specified type.
- **Returns:** `true` if a behaviour of type `T` was removed, otherwise `false`.
- **Triggers:** `OnBehaviourDeleted` and `OnStateChanged`.

#### `DelBehaviours<T>()`

```csharp
public void DelBehaviours<T>() where T : IEntityBehaviour  
```

- **Description:** Removes all behaviours of the specified type.
- **Triggers:** `OnBehaviourDeleted` and `OnStateChanged` for each removed behaviour.

#### `ClearBehaviours()`

```csharp
public void ClearBehaviours()  
```

- **Description:** Clears all behaviours from the entity.
- **Triggers:** `OnBehaviourDeleted` and `OnStateChanged` for each removed behaviour.

#### `GetBehaviours()`

```csharp
public IEntityBehaviour[] GetBehaviours()  
```

- **Description:** Returns all behaviours attached to the entity.
- **Returns:** Array of all behaviours.

#### `GetBehaviours<T>()`

```csharp
public T[] GetBehaviours<T>() where T : IEntityBehaviour  
```

- **Description:** Returns all behaviours of type `T` attached to the entity.
- **Returns:** Array of behaviours of type `T`.

#### `CopyBehaviours(IEntityBehaviour[])`

```csharp
public int CopyBehaviours(IEntityBehaviour[] results)  
```

- **Description:** Copies all behaviours into the provided array.
- **Parameters:** `results` – Array to copy behaviours into.
- **Returns:** Number of behaviours copied.
- **Exceptions:** Throws if `results` is null or too small.

#### `CopyBehaviours<T>(T[])`

```csharp
public int CopyBehaviours<T>(T[] results) where T : IEntityBehaviour  
```

- **Description:** Copies behaviours of type `T` into the provided array.
- **Parameters:** `results` – Array to copy behaviours into.
- **Returns:** Number of behaviours copied.
- **Exceptions:** Throws if `results` is null or too small.

#### `GetBehaviourEnumerator()`

```csharp
public IEnumerator<IEntityBehaviour> GetBehaviourEnumerator()  
```

- **Description:** Enumerates all behaviours attached to the entity.
- **Returns:** Enumerator for iterating through behaviours.

---

### 🗂 Example of Usage

```csharp
// Assume we have a player entity:
IEntity player = ...

// Subscribe to events
player.OnBehaviourAdded += (e, b) => 
    Console.WriteLine($"Behaviour {b.GetType().Name} added to {e.Id}");

player.OnBehaviourDeleted += (e, b) => 
    Console.WriteLine($"Behaviour {b.GetType().Name} removed from {e.Id}");

// Add behaviours
player.AddBehaviour(new MovementBehaviour());
player.AddBehaviour(new RotationBehaviour());

// Check count
Console.WriteLine($"Total behaviours: {player.BehaviourCount}");

// Retrieve behaviour by type
MovementBehaviour movementBehaviour = player.GetBehaviour<MovementBehaviour>();

// Try to retrieve behaviour by type
if (player.TryGetBehaviour<RotationBehaviour>(out var rotation))
    Console.WriteLine("Found RotationBehaviour");

// Remove behaviour
player.DelBehaviour<MovementBehaviour>();

// Clear all behaviours
player.ClearBehaviours();

// Enumerate all behaviours
foreach (IEntityBehaviour behaviour in player.GetBehaviourEnumerator())
    Console.WriteLine($"Behaviour: {behaviour.GetType().Name}");

// Get array of behaviours
IEntityBehaviour[] behaviours = player.GetBehaviours();

// Copy to array
IEntityBehaviour[] buffer = new IEntityBehaviour[10];
int copied = player.CopyBehaviours(buffer);

Console.WriteLine($"Copied {copied} behaviours into buffer");
```

</details>

---

<details>
  <summary>
    <h2 id="-lifecycle-members">♻️ Lifecycle</h2>
    <br>
    Manage the entity's state transitions and update phases. It covers initialization, enabling,
    per-frame updates, disabling, and disposal. Lifecycle events allow reactive systems to respond to changes in the
    entity's state.
  </summary>

### ⚡ Events

#### `OnInitialized`

```csharp
public event Action OnInitialized  
```

- **Description:** Occurs when the object has been successfully initialized.
- **Triggers:** Fired by the `Init()` method after successful initialization.

#### `OnDisposed`

```csharp
public event Action OnDisposed  
```

- **Description:** Occurs when the object has been disposed and its resources released.
- **Triggers:** Fired when `Dispose()` is called.

#### `OnEnabled`

```csharp
public event Action OnEnabled  
```

- **Description:** Occurs when the object is enabled.
- **Triggers:** Fired by the `Enable()` method.

#### `OnDisabled`

```csharp
public event Action OnDisabled  
```

- **Description:** Occurs when the object is disabled.
- **Triggers:** Fired by the `Disable()` method.

#### `OnTicked`

```csharp
public event Action<float> OnTicked  
```

- **Description:** Occurs during the regular `Update` phase, once per frame.
- **Triggers:** Fired inside `Tick(float deltaTime)`.
- **Parameter:** `deltaTime` – Time in seconds since the last frame.

#### `OnFixedTicked`

```csharp
public event Action<float> OnFixedTicked  
```

- **Description:** Occurs during the `FixedUpdate` phase, typically used for physics updates.
- **Triggers:** Fired inside `FixedTick(float deltaTime)`.
- **Parameter:** `deltaTime` – Fixed time step used by the physics engine.
- **Exceptions:** None.

#### `OnLateTicked`

```csharp
public event Action<float> OnLateTicked  
```

- **Description:** Occurs during the `LateUpdate` phase, after all `Update` calls have been made.
- **Triggers:** Fired inside `LateTick(float deltaTime)`.
- **Parameter:** `deltaTime` – Time in seconds since the last frame.

---

### 🔑 Properties

#### `Initialized`

```csharp
public bool Initialized { get; }  
```

- **Description:** Indicates whether the object is currently initialized.
- **Returns:** `true` if the object has been initialized, otherwise `false`.

#### `Enabled`

```csharp
public bool Enabled { get; }  
```

- **Description:** Indicates whether the object is currently enabled.
- **Returns:** `true` if enabled, otherwise `false`.

---

### 🏹 Methods

#### `Init()`

```csharp
public void Init()  
```

- **Description:** Initializes the entity.
- **Behavior:**
    - Transitions the entity to the `Initialized` state.
    - Calls `Init` on all behaviours implementing `IEntityInit`.
    - Triggers the `OnInitialized` event.
    - If the entity is already initialized, does nothing.

#### `Enable()`

```csharp
public void Enable()  
```

- **Description:** Enables the entity for updates.
- **Behavior:**
    - Transitions the entity to the `Enabled` state.
    - Calls `Enable` on all behaviours implementing `IEntityEnable`.
    - Triggers the `OnEnabled` event.
    - If the entity is not initialized yet, it will be initialized automatically.
    - If the entity is already enabled, does nothing.

#### `Tick(float)`

```csharp
public void Tick(float deltaTime)  
```

- **Description:** Calls `Update` on all behaviours implementing `IEntityUpdate`.
- **Behavior:**
    - Triggers the `OnTicked` event.
    - Can only be invoked if the entity is enabled.
- **Parameter:** `deltaTime` – Time in seconds since the last frame.
- **Exceptions:** Throws if the entity is not enabled.

#### `FixedTick(float)`

```csharp
public void FixedTick(float deltaTime)  
```

- **Description:** Calls `FixedUpdate` on all behaviours implementing `IEntityFixedUpdate`.
- **Behavior:**
    - Triggers the `OnFixedTicked` event.
    - Can only be invoked if the entity is enabled.
- **Parameter:** `deltaTime` – Fixed time step used by the physics engine.
- **Exceptions:** Throws if the entity is not enabled.

#### `LateTick(float)`

```csharp
public void LateTick(float deltaTime)  
```

- **Description:** Calls `LateUpdate` on all behaviours implementing `IEntityLateUpdate`.
- **Behavior:**
    - Triggers the `OnLateTicked` event.
    - Can only be invoked if the entity is enabled.
- **Parameter:** `deltaTime` – Time in seconds since the last frame.
- **Exceptions:** Throws if the entity is not enabled.

#### `Disable()`

```csharp
public void Disable()  
```

- **Description:** Disables the entity for updates.
- **Behavior:**
    - Transitions the entity to a not `Enabled` state.
    - Calls `Disable` on all behaviours implementing `IEntityDisable`.
    - Triggers the `OnDisabled` event.
    - If the entity is not enabled yet, does nothing.

#### `Dispose()`

```csharp
public void Dispose()  
```

- **Description:** Cleans up all resources used by the entity.
- **Behavior:**
    - Transitions the entity to a not `Initialized` state.
    - Calls `Dispose` on all behaviours implementing `IEntityDispose`.
    - Clears all tags, values, and behaviours.
    - Unsubscribes from all events.
    - Unregisters the entity from the `EntityRegistry`.
    - Disposes stored values if `Settings.disposeValues` is `true`.
    - If the entity is enabled, calls `Disable()` automatically.
    - If the entity is not initialized yet, does not call `IEntityDispose.Dispose` or trigger `OnDisposed`.

---

### 🗂 Example of Usage

```csharp
// Create a new entity
IEntity player = new Entity();

// Subscribe to lifecycle events
player.OnInitialized += () => Console.WriteLine("Entity initialized");
player.OnDisposed += () => Console.WriteLine("Entity disposed");
player.OnEnabled += () => Console.WriteLine("Entity enabled");
player.OnDisabled += () => Console.WriteLine("Entity disabled");
player.OnTicked += deltaTime => Console.WriteLine($"Tick: {deltaTime}");
player.OnFixedTicked += deltaTime => Console.WriteLine($"FixedTick: {deltaTime}");
player.OnLateTicked += deltaTime => Console.WriteLine($"LateTick: {deltaTime}");

// Initialize and enable the entity
player.Init();
player.Enable();

// Simulate game loop updates
player.Tick(0.016f);       // Update (frame)
player.FixedTick(0.02f);   // Physics update
player.LateTick(0.016f);   // Late update

// Disable the entity
player.Disable();

// Dispose the entity
player.Dispose();
```

</details>

---

<details>
  <summary>
    <h2 id="-example-of-usage"> 🗂 Example of Usage</h2>
    <br> The example below demonstrates quick entity creation and configuration with <code>Atomic.Elements</code>:
  </summary>
<br>

```csharp
// Create a new entity in C#
IEntity entity = new Entity();
entity.Name = "Player Character";

// Add tags
entity.AddTag("Character");
entity.AddTag("Moveable");
entity.AddTag("Damageable");

// Add values
entity.AddValue("Health", new ReactiveVariable<int>(100));
entity.AddValue("MoveSpeed", new ReactiveVariable<float>(5));
entity.AddValue("MoveDirection", new ReactiveVariable<Vector3>());

// Add a behaviour
entity.AddBehaviour<MovementBehaviour>();

// Initialize entity after configuration
entity.Init();

// Enable entity for updates (e.g., when retrieved from a pool)
entity.Enable();

// Update manually (e.g., in a game loop)
entity.Tick(Time.deltaTime);

// Disable entity (e.g., if it moved back into a pool)
entity.Disable();

// Dispose entity when game is unloading
entity.Dispose();
```

</details>

---

<details>
  <summary>
    <h2 id="-notes">📝 Notes</h2>
  </summary>

- **Event-Driven** – Reactive programming support via state change notifications.
- **Unique Identity** – Runtime-generated instance ID for entity tracking.
- **Tag System** – Lightweight categorization and filtering.
- **State Management** – Dynamic key-value storage for runtime data.
- **Behaviour Composition** – Attach or detach modular logic at runtime.
- **Lifecycle Control** – Built-in support for `Init`, `Enable`, `Tick`, `Disable`, and `Dispose` phases.
</details>