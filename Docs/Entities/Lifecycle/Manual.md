# 🧩 Lifecycle System

Defines standardized interfaces and subscriptions for different stages of an entity or world lifecycle, such as
initialization, enabling, and ticking. **Subscriptions** provide a declarative mechanism to register actions that
execute automatically at specific lifecycle events. Lifecycle contracts serve as parent interfaces
for [IEntity](../Entities/IEntity.md) and [IEntityWorld](../Worlds/IEntityWorld.md).

---

## 📑 Table of Contents

- [Examples of Usage](#-example-of-usage)
    - [Contracts](#contracts)
        - [IInitSource](#IInitSource)
        - [IEnableSource](#IEnableSource)
        - [ITickSource](#ITickSource)
    - [Subscription](#subscription)
    - [Extension Methods](#extensions)
        - [WhenInit()](#wheninit)
        - [WhenEnable()](#whenenable)
        - [WhenTick()](#whentick)
- [API Reference](#-api-reference)

---

## 🗂 Examples of Usage

<div id="contracts"></div>

### 1️⃣ Contracts

Below are examples of all lifecycle contracts serving as parent interfaces for [IEntity](../Entities/IEntity.md):
and [IEntityWorld](../Worlds/IEntityWorld.md):

#### `IInitSource`

```csharp
// Assume we have an instance of IInitSource
IInitSource initSource = ...;

// Subscribe to events
initSource.OnInitialized += () => Console.WriteLine("Object initialized");
initSource.OnDisposed += () => Console.WriteLine("Object disposed");

// Check initial state
Console.WriteLine($"Initialized: {initSource.Initialized}");

// Initialize the object
initSource.Init();

// Dispose the object
initSource.Dispose();
```

#### `IEnableSource`

```csharp
//Assume we have an instance of IEnableSource
IEnableSource enableSource = ...;

// Subscribe to events
enableSource.OnEnabled += () => Console.WriteLine("Player enabled");
enableSource.OnDisabled += () => Console.WriteLine("Player disabled");

// Check initial state
Console.WriteLine($"Enabled: {enableSource.Enabled}");

// Enable the object
enableSource.Enable();

// Disable the object
enableSource.Disable();
```

#### `ITickSource`

```csharp
// Assume we have an instance of ITickSource
ITickSource tickSource = ...;

// Subscribe to events
tickSource.OnTicked += deltaTime => Console.WriteLine($"Update tick: {deltaTime:F3}s");
tickSource.OnFixedTicked += deltaTime => Console.WriteLine($"FixedUpdate tick: {deltaTime:F3}s");
tickSource.OnLateTicked += deltaTime => Console.WriteLine($"LateUpdate tick: {deltaTime:F3}s");

// Simulate update cycles
float deltaTime = 0.016f; // ~60 FPS

// Regular Update
tickSource.Tick(deltaTime);

// FixedUpdate
tickSource.FixedTick(0.02f);

// LateUpdate
tickSource.LateTick(deltaTime);
```

### 2️⃣ Subscription

<div id="subscription"></div>

This example demonstrates usage of [DisableSubscription](Subscriptions/DisableSubscription.md)
with [IEnableSource](Sources/IEnableSource.md):

```csharp
 //Assume we have an instance of IEnableSource
IEnableSource enableSource = ...;

// Subscribe to the disable event
var subscription = new DisableSubscription(enableSource, () => 
    Console.WriteLine("Source has been disabled"));

// Disabling source triggers the subscription
enableSource.Disable();

// Later, we can unsubscribe from the source
subscription.Dispose();
```

### 3️⃣ Extension Methods

<div id="extensions"></div>

For convenience, there are several subscription methods that return an instance of a concrete `Subscription` struct.

#### `WhenInit()`

```csharp
//Assume we have an instance of entity
IEntity entity = ...;

// Subscribe to initialization
InitSubscription initSubscription = entity.WhenInit(() => Console.WriteLine("Initialized!"));

//Unsubscribe from initialization
initSubscription.Dispose(); 
```

#### `WhenEnable()`

```csharp
//Assume we have an instance of entity
IEntity entity = ...;

// Subscribe to enabling
EnableSubscription enableSubscription = entity.WhenEnable(() => Console.WriteLine("Enabled!"));

// Unsubscribe from enabling
enableSubscription.Dispose();
```

#### `WhenTick()`

```csharp
//Assume we have an instance of entity
IEntity entity = ...;

// Subscribe to frame updates
TickSubscription tickSubscription = entity.WhenTick(delta => Console.WriteLine($"Tick: {delta}"));

// Unsubscribe from frame updates
tickSubscription.Dispose();
```

---

## 🔍 API Reference

This section contains reference documentation for all lifecycle-related APIs, including contracts, subscription types,
and supporting extensions used to manage initialization, enable/disable states, ticking, and disposal within the
framework.

- **Contracts**
    - [IInitSource](Sources/IInitSource.md) <!-- + -->
    - [IEnableSource](Sources/IEnableSource.md) <!-- + -->
    - [ITickSource](Sources/ITickSource.md) <!-- + -->
- **Subscriptions**
    - [InitSubscription](Subscriptions/InitSubscription.md) <!-- + -->
    - [EnableSubscription](Subscriptions/EnableSubscription.md) <!-- + -->
    - [DisableSubscription](Subscriptions/DisableSubscription.md) <!-- + -->
    - [DisposeSubscription](Subscriptions/DisposeSubscription.md) <!-- + -->
    - [TickSubscription](Subscriptions/TickSubscription.md) <!-- + -->
    - [FixedTickSubscription](Subscriptions/FixedTickSubscription.md) <!-- + -->
    - [LateTickSubscription](Subscriptions/LateTickSubscription.md) <!-- + -->
- [Extensions](Extensions.md) <!-- + -->