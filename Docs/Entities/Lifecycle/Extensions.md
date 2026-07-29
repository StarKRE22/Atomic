# 🧩 Lifecycle Extensions

Provides **extension methods** for convenient and safe subscriptions to lifecycle and update events. Each method returns
a **disposable subscription**, ensuring automatic unsubscription when no longer needed.

---

## 📑 Table of Contents

<ul>
  <li><a href="#-examples-of-usage">Examples of Usage</a>
    <ul>
      <li><a href="#ex1">WhenInit()</a></li>
      <li><a href="#ex2">WhenDispose()</a></li>
      <li><a href="#ex3">WhenEnable()</a></li>
      <li><a href="#ex4">WhenDisable()</a></li>
      <li><a href="#ex5">WhenTick()</a></li>
      <li><a href="#ex6">WhenFixedTick()</a></li>
      <li><a href="#ex7">WhenLateTick()</a></li>
    </ul>
  </li>
  <li><a href="#-api-reference">API Reference</a>
    <ul>
      <li><a href="#-type">Type</a></li>
      <li>
        <details>
        <summary><a href="#-methods">Methods</a></summary>
        <ul>
          <li><a href="#wheninitaction">WhenInit(Action)</a></li>
          <li><a href="#whendisposeaction">WhenDispose(Action)</a></li>
          <li><a href="#whenenableaction">WhenEnable(Action)</a></li>
          <li><a href="#whendisableaction">WhenDisable(Action)</a></li>
          <li><a href="#whentickactionfloat">WhenTick(Action&lt;float&gt;)</a></li>
          <li><a href="#whenfixedtickactionfloat">WhenFixedTick(Action&lt;float&gt;)</a></li>
          <li><a href="#whenlatetickactionfloat">WhenLateTick(Action&lt;float&gt;)</a></li>
        </ul>
        </details>
      </li>
    </ul>
  </li>
</ul>


---

## 🗂 Examples of Usage

Below are examples of using extension methods for lifecycle events:

<div id="ex1"></div>

### 1️⃣ WhenInit()

```csharp
//Assume we have an instance of entity
IEntity entity = ...;

// Subscribe to initialization
InitSubscription initSubscription = entity.WhenInit(() => Console.WriteLine("Initialized!"));

//Unsubscribe from initialization
initSubscription.Dispose(); 
```

<div id="ex2"></div>

### 2️⃣ WhenDispose()

```csharp
//Assume we have an instance of entity
IEntity entity = ...;

// Subscribe to disposal
DisposeSubscription disposeSubscription = entity.WhenDispose(() => Console.WriteLine("Disposed!"));

// Unsubscribe from disposal
disposeSubscription.Dispose(); 
```

<div id="ex3"></div>

### 3️⃣ WhenEnable()

```csharp
//Assume we have an instance of entity
IEntity entity = ...;

// Subscribe to enabling
EnableSubscription enableSubscription = entity.WhenEnable(() => Console.WriteLine("Enabled!"));

// Unsubscribe from enabling
enableSubscription.Dispose();
```

<div id="ex4"></div>

### 4️⃣ WhenDisable()

```csharp
//Assume we have an instance of entity
IEntity entity = ...;

// Subscribe to disabling
DisableSubscription disableSubscription = entity.WhenDisable(() => Console.WriteLine("Disabled!"));

// Unsubscribe from disabling 
disableSubscription.Dispose();
```

<div id="ex5"></div>

### 5️⃣ WhenTick()

```csharp
//Assume we have an instance of entity
IEntity entity = ...;

// Subscribe to frame updates
TickSubscription tickSubscription = entity.WhenTick(delta => Console.WriteLine($"Tick: {delta}"));

// Unsubscribe from frame updates
tickSubscription.Dispose();
```

<div id="ex6"></div>

### 6️⃣ WhenFixedTick()

```csharp
//Assume we have an instance of entity
IEntity entity = ...;

// Subscribe to fixed updates
FixedTickSubscription fixedTickSubscription = entity.WhenFixedTick(delta => Console.WriteLine($"FixedTick: {delta}"));

// Unsubscribe from fixed updates 
fixedTickSubscription.Dispose();
```

<div id="ex7"></div>

### 7️⃣ WhenLateTick()

```csharp
//Assume we have an instance of entity
IEntity entity = ...;

// Subscribe to late updates
LateTickSubscription lateTickSubscription = entity.WhenLateTick(delta => Console.WriteLine($"LateTick: {delta}"));

// Unsubscribe from late updates 
lateTickSubscription.Dispose();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public static class Extensions
```

---

### 🏹 Methods

#### `WhenInit(Action)`

```csharp
public static InitSubscription WhenInit(this IInitSource source, Action action)
```

- **Description:** Subscribes to the [IInitSource.OnInitialized](Sources/IInitSource.md#oninitialized) event.
- **Parameters:**
    - `source` — The spawnable object to observe.
    - `action` — The callback to invoke on initialization.
- **Returns:** A disposable [InitSubscription](Subscriptions/InitSubscription.md) that unsubscribes when disposed.

#### `WhenDispose(Action)`

```csharp
public static DisposeSubscription WhenDispose(this IInitSource source, Action action)
```

- **Description:** Subscribes to the [IInitSource.OnDisposed](Sources/IInitSource.md#ondisposed) event.
- **Parameters:**
    - `source` — The spawnable object to observe.
    - `action` — The callback to invoke on disposal.
- **Returns:** A disposable [DisposeSubscription](Subscriptions/DisposeSubscription.md) that unsubscribes when disposed.

#### `WhenEnable(Action)`

```csharp
public static EnableSubscription WhenEnable(this IEnableSource source, Action action)
```

- **Description:** Subscribes to the [IEnableSource.OnEnabled](Sources/IEnableSource.md#onenabled) event.
- **Parameters:**
    - `source` — The activatable object.
    - `action` — The callback to invoke when enabled.
- **Returns:** A disposable [EnableSubscription](Subscriptions/EnableSubscription.md) that unsubscribes when disposed.

#### `WhenDisable(Action)`

```csharp
public static DisableSubscription WhenDisable(this IEnableSource source, Action action)
```

- **Description:** Subscribes to the [IEnableSource.OnDisabled](Sources/IEnableSource.md#ondisabled) event.
- **Parameters:**
    - `source` — The activatable object.
    - `action` — The callback to invoke when disabled.
- **Returns:** A disposable `DisableSubscription` that unsubscribes when disposed.

#### `WhenTick(Action<float>)`

```csharp
public static TickSubscription WhenTick(this ITickSource source, Action<float> action)
```

- **Description:** Subscribes to the [ITickSource.OnTicked](Sources/ITickSource.md#onticked) event.
- **Parameters:**
    - `source` — The updatable object.
    - `action` — The callback to invoke on each frame update.
- **Returns:** A disposable [TickSubscription](Subscriptions/TickSubscription.md) that unsubscribes when disposed.

#### `WhenFixedTick(Action<float>)`

```csharp
public static FixedTickSubscription WhenFixedTick(this ITickSource source, Action<float> action)
```

- **Description:** Subscribes to the [ITickSource.OnFixedTicked](Sources/ITickSource.md#onfixedticked) event.
- **Parameters:**
    - `source` — The updatable object.
    - `action` — The callback to invoke on each fixed update.
- **Returns:** A disposable [FixedTickSubscription](Subscriptions/FixedTickSubscription.md) that unsubscribes when
  disposed.

#### `WhenLateTick(Action<float>)`

```csharp
public static LateTickSubscription WhenLateTick(this ITickSource source, Action<float> action)
```

- **Description:** Subscribes to the [ITickSource.OnLateTicked](Sources/ITickSource.md#onlateticked) event.
- **Parameters:**
    - `source` — The updatable object.
    - `action` — The callback to invoke on each late update.
- **Returns:** A disposable [LateTickSubscription](Subscriptions/LateTickSubscription.md) that unsubscribes when
  disposed.