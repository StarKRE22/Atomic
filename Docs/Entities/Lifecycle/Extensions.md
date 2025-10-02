# 🧩 Lifecycle Extension Methods

Provides **extension methods** for convenient and safe subscriptions to lifecycle and update events. Each method returns
a **disposable subscription**, ensuring automatic unsubscription when no longer needed.

---

## 🏹 Methods

#### `WhenInit(Action)`

```csharp
public static InitSubscription WhenInit(this IInitLifecycle source, Action action)
```

- **Description:** Subscribes to the [IInitLifecycle.OnInitialized](Sources/IInitLifecycle.md#oninitialized) event.
- **Parameters:**
    - `source` — The spawnable object to observe.
    - `action` — The callback to invoke on initialization.
- **Returns:** A disposable [InitSubscription](Subscriptions/InitSubscription.md) that unsubscribes when disposed.

#### `WhenDispose(Action)`

```csharp
public static DisposeSubscription WhenDispose(this IInitLifecycle source, Action action)
```

- **Description:** Subscribes to the [IInitLifecycle.OnDisposed](Sources/IInitLifecycle.md#ondisposed) event.
- **Parameters:**
    - `source` — The spawnable object to observe.
    - `action` — The callback to invoke on disposal.
- **Returns:** A disposable [DisposeSubscription](Subscriptions/DisposeSubscription.md) that unsubscribes when disposed.

#### `WhenEnable(Action)`

```csharp
public static EnableSubscription WhenEnable(this IEnableLifecycle source, Action action)
```

- **Description:** Subscribes to the [IEnableLifecycle.OnEnabled](Sources/IEnableLifecycle.md#onenabled) event.
- **Parameters:**
    - `source` — The activatable object.
    - `action` — The callback to invoke when enabled.
- **Returns:** A disposable [EnableSubscription](Subscriptions/EnableSubscription.md) that unsubscribes when disposed.

#### `WhenDisable(Action)`

```csharp
public static DisableSubscription WhenDisable(this IEnableLifecycle source, Action action)
```

- **Description:** Subscribes to the [IEnableLifecycle.OnDisabled](Sources/IEnableLifecycle.md#ondisabled) event.
- **Parameters:**
    - `source` — The activatable object.
    - `action` — The callback to invoke when disabled.
- **Returns:** A disposable `DisableSubscription` that unsubscribes when disposed.

#### `WhenTick(Action<float>)`

```csharp
public static TickSubscription WhenTick(this ITickLifecycle source, Action<float> action)
```

- **Description:** Subscribes to the [ITickLifecycle.OnTicked](Sources/ITickLifecycle.md#onticked) event.
- **Parameters:**
    - `source` — The updatable object.
    - `action` — The callback to invoke on each frame update.
- **Returns:** A disposable [TickSubscription](Subscriptions/TickSubscription.md) that unsubscribes when disposed.

#### `WhenFixedTick(Action<float>)`

```csharp
public static FixedTickSubscription WhenFixedTick(this ITickLifecycle source, Action<float> action)
```

- **Description:** Subscribes to the [ITickLifecycle.OnFixedTicked](Sources/ITickLifecycle.md#onfixedticked) event.
- **Parameters:**
    - `source` — The updatable object.
    - `action` — The callback to invoke on each fixed update.
- **Returns:** A disposable [FixedTickSubscription](Subscriptions/FixedTickSubscription.md) that unsubscribes when disposed.

#### `WhenLateTick(Action<float>)`

```csharp
public static LateTickSubscription WhenLateTick(this ITickLifecycle source, Action<float> action)
```

- **Description:** Subscribes to the [ITickLifecycle.OnLateTicked](Sources/ITickLifecycle.md#onlateticked) event.
- **Parameters:**
    - `source` — The updatable object.
    - `action` — The callback to invoke on each late update.
- **Returns:** A disposable [LateTickSubscription](Subscriptions/LateTickSubscription.md) that unsubscribes when disposed.

---

## 🗂 Examples of Usage

Below are examples of using extension methods for lifecycle events: 

#### `WhenInit` 

```csharp
//Assume we have an instance of entity
IEntity entity = ...;

// Subscribe to initialization
InitSubscription initSubscription = entity.WhenInit(() => Console.WriteLine("Initialized!"));

//Unsubscribe from initialization
initSubscription.Dispose(); 
```

#### `WhenDispose`

```csharp
//Assume we have an instance of entity
IEntity entity = ...;

// Subscribe to disposal
DisposeSubscription disposeSubscription = entity.WhenDispose(() => Console.WriteLine("Disposed!"));

// Unsubscribe from disposal
disposeSubscription.Dispose(); 
```

#### `WhenEnable`
```csharp
//Assume we have an instance of entity
IEntity entity = ...;

// Subscribe to enabling
EnableSubscription enableSubscription = entity.WhenEnable(() => Console.WriteLine("Enabled!"));

// Unsubscribe from enabling
enableSubscription.Dispose();
```

#### `WhenDisable`
```csharp
//Assume we have an instance of entity
IEntity entity = ...;

// Subscribe to disabling
DisableSubscription disableSubscription = entity.WhenDisable(() => Console.WriteLine("Disabled!"));

// Unsubscribe from disabling 
disableSubscription.Dispose();
```

#### `WhenTick`

```csharp
//Assume we have an instance of entity
IEntity entity = ...;

// Subscribe to frame updates
TickSubscription tickSubscription = entity.WhenTick(delta => Console.WriteLine($"Tick: {delta}"));

// Unsubscribe from frame updates
tickSubscription.Dispose();
```

#### `WhenFixedTick`

```csharp
//Assume we have an instance of entity
IEntity entity = ...;

// Subscribe to fixed updates
FixedTickSubscription fixedTickSubscription = entity.WhenFixedTick(delta => Console.WriteLine($"FixedTick: {delta}"));

// Unsubscribe from fixed updates 
fixedTickSubscription.Dispose();
```

#### `WhenLateTick`

```csharp
//Assume we have an instance of entity
IEntity entity = ...;

// Subscribe to late updates
LateTickSubscription lateTickSubscription = entity.WhenLateTick(delta => Console.WriteLine($"LateTick: {delta}"));

// Unsubscribe from late updates 
lateTickSubscription.Dispose();
```