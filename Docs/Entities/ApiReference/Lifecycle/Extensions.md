# 🧩 Lifecycle Extension Methods

Provides **extension methods** for convenient and safe subscriptions to lifecycle and update events.  
Each method returns a **disposable subscription**, ensuring automatic unsubscription when no longer needed.

---

## WhenInit
```csharp
public static InitSubscription WhenInit(this IInitSource source, Action action)
```
- Subscribes to the `IInitSource.OnInitialized` event.
- **Parameters:**
    - `source` — The spawnable object to observe.
    - `action` — The callback to invoke on initialization.
- **Returns:** A disposable `InitSubscription` that unsubscribes when disposed.

---

## WhenDispose
```csharp
public static DisposeSubscription WhenDispose(this IInitSource source, Action action)
```
- Subscribes to the `IInitSource.OnDisposed` event.
- **Parameters:**
    - `source` — The spawnable object to observe.
    - `action` — The callback to invoke on disposal.
- **Returns:** A disposable `DisposeSubscription` that unsubscribes when disposed.

---

## WhenEnable
```csharp
public static EnableSubscription WhenEnable(this IEnableSource source, Action action)
```
- Subscribes to the `IEnableSource.OnEnabled` event.
- **Parameters:**
    - `source` — The activatable object.
    - `action` — The callback to invoke when enabled.
- **Returns:** A disposable `EnableSubscription` that unsubscribes when disposed.

---

## WhenDisable
```csharp
public static DisableSubscription WhenDisable(this IEnableSource source, Action action)
```
- Subscribes to the `IEnableSource.OnDisabled` event.
- **Parameters:**
    - `source` — The activatable object.
    - `action` — The callback to invoke when disabled.
- **Returns:** A disposable `DisableSubscription` that unsubscribes when disposed.

---

## WhenUpdate
```csharp
public static UpdateSubscription WhenUpdate(this IUpdateSource source, Action<float> action)
```
- Subscribes to the `IUpdateSource.OnUpdated` event.
- **Parameters:**
    - `source` — The updatable object.
    - `action` — The callback to invoke on each frame update.
- **Returns:** A disposable `UpdateSubscription` that unsubscribes when disposed.

---

## WhenFixedUpdate
```csharp
public static FixedUpdateSubscription WhenFixedUpdate(this IUpdateSource source, Action<float> action)
```
- Subscribes to the `IUpdateSource.OnFixedUpdated` event.
- **Parameters:**
    - `source` — The updatable object.
    - `action` — The callback to invoke on each fixed update.
- **Returns:** A disposable `FixedUpdateSubscription` that unsubscribes when disposed.

---

## WhenLateUpdate
```csharp
public static LateUpdateSubscription WhenLateUpdate(this IUpdateSource source, Action<float> action)
```
- Subscribes to the `IUpdateSource.OnLateUpdated` event.
- **Parameters:**
    - `source` — The updatable object.
    - `action` — The callback to invoke on each late update.
- **Returns:** A disposable `LateUpdateSubscription` that unsubscribes when disposed.

---

## Example Usage

**Create a new entity**
```csharp
var entity = new Entity();
```

**Subscribe on lifecycle events**
```csharp
// Subscribe to initialization
InitSubscription initSubscription = entity.WhenInit(() => Console.WriteLine("Initialized!"));

// Subscribe to disposal
DisposeSubscription disposeSubscription = entity.WhenDispose(() => Console.WriteLine("Disposed!"));

// Subscribe to enabling
EnableSubscription enableSubscription = entity.WhenEnable(() => Console.WriteLine("Enabled!"));

// Subscribe to disabling
DisableSubscription disableSubscription = entity.WhenDisable(() => Console.WriteLine("Disabled!"));

// Subscribe to frame updates
UpdateSubscription updateSubscription = entity.WhenUpdate(delta => Console.WriteLine($"Update: {delta}"));

// Subscribe to fixed updates
FixedUpdateSubscription fixedUpdateSubscription = entity.WhenFixedUpdate(delta => Console.WriteLine($"FixedUpdate: {delta}"));

// Subscribe to late updates
LateUpdateSubscription lateUpdateSubscription = entity.WhenLateUpdate(delta => Console.WriteLine($"LateUpdate: {delta}"));
```

**Unsubscribe from lifecycle events**
```csharp
//Unsubscribe from initialization
initSubscription.Dispose(); 

// Unsubscribe from disposal
disposeSubscription.Dispose(); 

// Unsubscribe from enabling
enableSubscription.Dispose();

// Unsubscribe from disabling 
disableSubscription.Dispose();

// Unsubscribe from frame updates
updateSubscription.Dispose();

// Unsubscribe from fixed updates 
fixedUpdateSubscription.Dispose();

// Unsubscribe from late updates 
lateUpdateSubscription.Dispose();
```