# 🧩 InlineSignal Classes

The **InlineSignal** classes provide wrappers for reactive event sources.  
They implement the corresponding [ISignal](ISignal.md) interfaces and allow entities to **subscribe / unsubscribe** from events, optionally with parameters.

---

## 🧩 InlineSignal
```csharp
public class InlineSignal : ISignal
```
- **Description:** Represents a signal that can notify subscribers of events **without passing any data**.

### Constructors

#### `InlineSignal(Action<Action>, Action<Action>)`
```csharp
public InlineSignal(Action<Action> subscribe, Action<Action> unsubscribe)
```
- **Description:** Initializes a new instance with provided subscription and unsubscription delegates.
- **Parameters:**
    - `subscribe` — Action handling subscription logic
    - `unsubscribe` — Action handling unsubscription logic
- **Throws:** `ArgumentNullException` if `subscribe` or `unsubscribe` is null.

### Methods

#### `Subscribe(Action)`
```csharp
Subscription Subscribe(Action action)  
```
- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription](../Signals/Subscription.md#subscription) struct representing the active subscription.

#### `Unsubscribe(Action)`
```csharp
void `Unsubscribe(Action action)`  
```
- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

---

## 🧩 InlineSignal&lt;T&gt;
```csharp
public class InlineSignal<T> : ISignal<T>
```
- **Description:** Represents a signal that notifies subscribers with a **single value**.
- **Type parameter:** `T` — the emitted value type.

### Constructors

#### `InlineSignal(Action<Action<T>>, Action<Action<T>>)`

```csharp
public InlineSignal(Action<Action<T>> subscribe, Action<Action<T>> unsubscribe)
```
- **Description:** Initializes a new instance with provided delegates.
- **Parameters:**
    - `subscribe` — Action handling subscription logic
    - `unsubscribe` — Action handling unsubscription logic
- **Throws:** `ArgumentNullException` if `subscribe` or `unsubscribe` is null.

### Methods

#### `Subscribe(Action<T>)`
```csharp
Subscription<T> Subscribe(Action<T> action)  
```
- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription&lt;T&gt;](../Signals/Subscription.md#subscriptiont) struct representing the active subscription.

#### `Unsubscribe(Action<T>)`
```csharp
void `Unsubscribe(Action<T> action)`  
```
- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

---

## 🧩 InlineSignal&lt;T1, T2&gt;
```csharp
public class InlineSignal<T1, T2> : ISignal<T1, T2>
```
- **Description:** Represents a reactive signal with **two parameters**.
- **Type parameters:**
  - `T1` — the first emitted value
  - `T2` — the second emitted value
  
### Constructors

#### `InlineSignal(Action<Action<T1, T2>>, Action<Action<T1, T2>>)`
```csharp
public InlineSignal(Action<Action<T1, T2>> subscribe, Action<Action<T1, T2>> unsubscribe)
```
- **Description:** Initializes a new instance with provided delegates.
- **Parameters:**
    - `subscribe` — Action handling subscription logic
    - `unsubscribe` — Action handling unsubscription logic
- **Throws:** `ArgumentNullException` if `subscribe` or `unsubscribe` is null.

### Methods

#### `Subscribe(Action<T1, T2>)`
```csharp
Subscription<T1, T2> Subscribe(Action<T1, T2> action)  
```
- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription<T1, T2>](../Signals/Subscription.md#subscriptiont1-t2) struct representing the active subscription.

#### `Unsubscribe(Action<T1, T2>)`
```csharp
void `Unsubscribe(Action<T1, T2> action)`  
```
- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.
---

## 🧩 InlineSignal&lt;T1, T2, T3&gt;
```csharp
public sealed class InlineSignal<T1, T2, T3> : ISignal<T1, T2, T3>
```
- **Description:** Represents a signal that notifies subscribers with **three values**.
- **Type parameters:**
  - `T1` — the first emitted value
  - `T2` — the second emitted value
  - `T3` — the third emitted value

### Constructors
#### `InlineSignal(Action<Action<T1, T2, T3>>, Action<Action<T1, T2, T3>>)`
```csharp
public InlineSignal(Action<Action<T1, T2, T3>> subscribe, Action<Action<T1, T2, T3>> unsubscribe)
```
- **Description:** Initializes a new instance with provided subscription and unsubscription delegates.
- **Parameters:**
  - `subscribe` — Action handling subscription logic
  - `unsubscribe` — Action handling unsubscription logic
- **Throws:** `ArgumentNullException` if `subscribe` or `unsubscribe` is null.

#### `Subscribe(Action<T1, T2, T3>)`
```csharp
Subscription<T1, T2, T3> Subscribe(Action<T1, T2, T3> action)  
```
- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription<T1, T2, T3>](../Signals/Subscription.md#subscriptiont1-t2-t3) struct representing the active subscription.

#### `Unsubscribe(Action<T1, T2, T3>)`
```csharp
void `Unsubscribe(Action<T1, T2, T3> action)`  
```
- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.
---

## 🧩 InlineSignal&lt;T1, T2, T3, T4&gt;
```csharp
public sealed class InlineSignal<T1, T2, T3, T4> : ISignal<T1, T2, T3, T4>
```
- **Description:** Represents a signal that notifies subscribers with **four values**.
- **Type parameters:**
  - `T1` — the first emitted value
  - `T2` — the second emitted value
  - `T3` — the third emitted value
  - `T4` — the fourth emitted value

### Constructors
#### `InlineSignal(Action<Action<T1, T2, T3, T4>> subscribe, Action<Action<T1, T2, T3, T4>> unsubscribe)`
```csharp
public InlineSignal(Action<Action<T1, T2, T3, T4>> subscribe, Action<Action<T1, T2, T3, T4>> unsubscribe)
```
- **Description:** Initializes a new instance with provided subscription and unsubscription delegates.
- **Parameters:**
  - `subscribe` — Action handling subscription logic
  - `unsubscribe` — Action handling unsubscription logic
- **Throws:** `ArgumentNullException` if `subscribe` or `unsubscribe` is null.

### Methods

#### `Subscribe(Action<T1, T2, T3, T4>)`
```csharp
Subscription<T1, T2, T3, T4> Subscribe(Action<T1, T2, T3, T4> action)  
```
- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription<T1, T2, T3, T4>](../Signals/Subscription.md#subscriptiont1-t2-t3-t4) struct representing the active subscription.

#### `Unsubscribe(Action<T1, T2, T3, T4>)`
```csharp
void `Unsubscribe(Action<T1, T2, T3, T4> action)`  
```
- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.
---
## 🗂 Example of Usage

The following example demonstrates how to wrap the `OnEntered` event from the [TriggerEvents](../UnityComponents/TriggerEvents.md) class into a `Signal<Collider>`:

```csharp
// Wrap the Unity event into an InlineSignal
ISignal<Collider> onTriggerEnter = new InlineSignal<Collider>(
    subscribe: action => triggerEvents.OnEntered += action,
    unsubscribe: action => triggerEvents.OnEntered -= action
);

// Subscribe to the signal
Subscription<Collider> subscription = onTriggerEnter.Subscribe(
    collider => Debug.Log($"On Trigger Entered: {collider.name}")
);

// Later, dispose to unsubscribe
subscription.Dispose();
```