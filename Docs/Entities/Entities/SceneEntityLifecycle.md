# 🧩 SceneEntity Lifecycle

Manage the entity's state transitions and update phases. It covers initialization, enabling,
per-frame updates, disabling, and disposal.

---


## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - <details>
    <summary><a href="#-events">Events</a></summary>

    - [OnInitialized](#oninitialized)
    - [OnDisposed](#ondisposed)
    - [OnEnabled](#onenabled)
    - [OnDisabled](#ondisabled)
    - [OnTicked](#onticked)
    - [OnFixedTicked](#onfixedticked)
    - [OnLateTicked](#onlateticked)

    </details>
  - <details>
    <summary><a href="#-properties">Properties</a></summary>

    - [Initialized](#initialized)
    - [Enabled](#enabled)

    </details>
  - <details>
    <summary><a href="#-methods">Methods</a></summary>

    - [Init()](#init)
    - [Enable()](#enable)
    - [Tick(float)](#tickfloat)
    - [FixedTick(float)](#fixedtickfloat)
    - [LateTick(float)](#latetickfloat)
    - [Disable()](#disable)
    - [Dispose()](#dispose)
    - [OnDispose()](#ondispose)

    </details>


---

## 🗂 Example of Usage

```csharp
// Assume we have an instance of entity
SceneEntity entity = ...

// Subscribe to lifecycle events
entity.OnInitialized += () => Console.WriteLine("Entity initialized");
entity.OnDisposed += () => Console.WriteLine("Entity disposed");
entity.OnEnabled += () => Console.WriteLine("Entity enabled");
entity.OnDisabled += () => Console.WriteLine("Entity disabled");
entity.OnTicked += deltaTime => Console.WriteLine($"Tick: {deltaTime}");
entity.OnFixedTicked += deltaTime => Console.WriteLine($"FixedTick: {deltaTime}");
entity.OnLateTicked += deltaTime => Console.WriteLine($"LateTick: {deltaTime}");

// Check if entity initialized
if (entity.Initialized) 
{
    // Do something
}

// Check if entity enabled
if (entity.Enabled)
{
    // Do something
}
```

Control the `SceneEntity` manually if `useUnityLifecycle` toggle is disabled

```csharp
// Initialize and enable the entity 
entity.Init();
entity.Enable();

// Simulate game loop updates
entity.Tick(0.016f);       // Update (frame)
entity.FixedTick(0.02f);   // Physics update
entity.LateTick(0.016f);   // Late update

// Disable the entity
entity.Disable();

// Dispose the entity
entity.Dispose();
```

---

## 🛠 Inspector Settings

| Parameters          | Description                                                                                      |
|---------------------|--------------------------------------------------------------------------------------------------|
| `useUnityLifecycle` | Enables automatic syncing with Unity MonoBehaviour lifecycle (`Start`, `OnEnable`, `OnDisable`). |
| `disposeValues`     | Determines whether **values** are disposed when `Dispose()` is called.                               |

---


## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public partial class SceneEntity
```

---

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

#### `OnDispose()`

```csharp
protected virtual void OnDispose()  
```

- **Description:**  Called during the disposal process of a `SceneEntity`. Provides a hook for derived classes to
  execute custom cleanup logic when the entity is being disposed.
- **Notes:** This method is invoked by `Dispose()`