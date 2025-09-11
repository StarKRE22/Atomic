# 🧩 InlineSignal Classes

The **InlineSignal** classes provide wrappers for reactive event sources.  
They implement the corresponding [ISignal](ISignal.md) interfaces and allow entities to **subscribe / unsubscribe** from events, optionally with parameters.

---

## 🧩 InlineSignal
```csharp
public class InlineSignal : ISignal
```
- **Description:** Represents a **parameterless reactive signal**.

### Constructors

#### `InlineSignal(Action<Action>, Action<Action>)`
```csharp
public InlineSignal(Action<Action> subscribe, Action<Action> unsubscribe)
```
- **Description:** Initializes a new instance with provided subscription and unsubscription delegates.
- **Parameters:**
    - `subscribe` — Action handling subscription logic
    - `unsubscribe` — Action handling unsubscription logic

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
- **Description:** Represents a reactive signal with **one parameter**.

### Constructors

#### `InlineSignal(Action<Action<T>>, Action<Action<T>>)`

```csharp
public InlineSignal(Action<Action<T>> subscribe, Action<Action<T>> unsubscribe)
```
- **Description:** Initializes a new instance with provided delegates.
- **Parameters:**
    - `subscribe` — Action handling subscription logic
    - `unsubscribe` — Action handling unsubscription logic

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

### Constructors

#### `InlineSignal(Action<Action<T1, T2>>, Action<Action<T1, T2>>)`
```csharp
public InlineSignal(Action<Action<T1, T2>> subscribe, Action<Action<T1, T2>> unsubscribe)
```
- **Description:** Initializes a new instance with provided delegates.
- **Parameters:**
    - `subscribe` — Action handling subscription logic
    - `unsubscribe` — Action handling unsubscription logic

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

## 🔔 InlineSignal&lt;T1, T2, T3&gt;
!!!
public sealed class InlineSignal<T1, T2, T3> : ISignal<T1, T2, T3>
!!!
- **Description:** Represents a reactive signal with **three parameters**.

### Constructors
#### `InlineSignal(Action<Action<T1, T2, T3>> subscribe, Action<Action<T1, T2, T3>> unsubscribe)`
!!!
public InlineSignal(Action<Action<T1, T2, T3>> subscribe, Action<Action<T1, T2, T3>> unsubscribe)
!!!

### Methods

#### `Subscribe(Action<T1, T2, T3> action)`
!!!
public Subscription<T1, T2, T3> Subscribe(Action<T1, T2, T3> action)
!!!
- **Description:** Subscribes to the signal with three arguments.

#### `Unsubscribe(Action<T1, T2, T3> action)`
!!!
public void Unsubscribe(Action<T1, T2, T3> action)
!!!

### 🗂 Example of Usage
!!!
var signal = new InlineSignal<string, int, bool>(
onSubscribe => Console.WriteLine("Subscribed!"),
onUnsubscribe => Console.WriteLine("Unsubscribed!")
);

var subscription = signal.Subscribe((player, score, alive) =>
Console.WriteLine($"{player} scored {score}, Alive: {alive}")
);
subscription.Dispose();
!!!

---

## 🔔 InlineSignal&lt;T1, T2, T3, T4&gt;
!!!
public sealed class InlineSignal<T1, T2, T3, T4> : ISignal<T1, T2, T3, T4>
!!!
- **Description:** Represents a reactive signal with **four parameters**.

### Constructors
#### `InlineSignal(Action<Action<T1, T2, T3, T4>> subscribe, Action<Action<T1, T2, T3, T4>> unsubscribe)`
!!!
public InlineSignal(Action<Action<T1, T2, T3, T4>> subscribe, Action<Action<T1, T2, T3, T4>> unsubscribe)
!!!

### Methods

#### `Subscribe(Action<T1, T2, T3, T4> action)`
!!!
public Subscription<T1, T2, T3, T4> Subscribe(Action<T1, T2, T3, T4> action)
!!!

#### `Unsubscribe(Action<T1, T2, T3, T4> action)`
!!!
public void Unsubscribe(Action<T1, T2, T3, T4> action)
!!!

### 🗂 Example of Usage
!!!
var signal = new InlineSignal<string, int, float, bool>(
onSubscribe => Console.WriteLine("Subscribed!"),
onUnsubscribe => Console.WriteLine("Unsubscribed!")
);

var subscription = signal.Subscribe((name, score, time, isWinner) =>
Console.WriteLine($"{name} scored {score} in {time}s. Winner: {isWinner}")
);
subscription.Dispose();
!!!

---

## 📌 Best Practice

> `InlineSignal` is best used for decoupled **reactive event streams**, enabling **pub/sub architectures** without tight coupling between publishers and subscribers.

Typical use cases:
- Entity events in games (damage, death, resource collection)
- UI updates and notifications
- Reactive programming pipelines




































======
======



# 🧩 InlineSignal Classes

The **InlineSignal** classes provide **base implementations of reactive sources** that notify subscribers when values are emitted.  
They support 0–4 parameters and implement the corresponding `ISignal` interfaces.

### Notes

- **Delegate-based** – All `InlineSignal` classes accept `subscribe` and `unsubscribe` delegates for flexible subscription management.
- **Subscription Structs** – Each `Subscribe` method returns a `Subscription` struct to allow easy unsubscription.
- **Serializable** – All classes are `[Serializable]`, suitable for Unity and other serialization systems.
- **Null Safety** – Constructors validate delegate arguments, throwing `ArgumentNullException` if `null`.

---

## InlineSignal

A **non-generic reactive source**.

```csharp
public class InlineSignal : ISignal
{
    Subscription Subscribe(Action action);
    void Unsubscribe(Action action);
}
```
- **Subscribe(Action action)** – Subscribes an action to be invoked when the source triggers.
- **Unsubscribe(Action action)** – Unsubscribes a previously registered action.
---

## InlineSignal&lt;T&gt;
A **generic reactive source with one value**.
```csharp
public class InlineSignal<T> : ISignal<T>
{
    Subscription<T> Subscribe(Action<T> action);
    void Unsubscribe(Action<T> action);
}
```
- **T** – Type of the emitted value.
- **Subscribe(Action<T> action)** – Subscribes to receive emitted values.
- **Unsubscribe(Action<T> action)** – Unsubscribes a previously registered action.
---
## InlineSignal<T1, T2>
A **reactive source with two values**.
```csharp
public class InlineSignal<T1, T2> : ISignal<T1, T2>
{
    Subscription<T1, T2> Subscribe(Action<T1, T2> action);
    void Unsubscribe(Action<T1, T2> action);
}
```
- **T1**, **T2** – Types of the emitted values.
---
## InlineSignal<T1, T2, T3>
A **reactive source with three values**.
```csharp
public class InlineSignal<T1, T2, T3> : ISignal<T1, T2, T3>
{
    Subscription<T1, T2, T3> Subscribe(Action<T1, T2, T3> action);
    void Unsubscribe(Action<T1, T2, T3> action);
}
```
- **T1**, **T2**, **T3** – Types of the emitted values.
---
## InlineSignal<T1, T2, T3, T4>
A **reactive source with four values**.
```csharp
public class InlineSignal<T1, T2, T3, T4> : ISignal<T1, T2, T3, T4>
{
    Subscription<T1, T2, T3, T4> Subscribe(Action<T1, T2, T3, T4> action);
    void Unsubscribe(Action<T1, T2, T3, T4> action);
}
```
- **T1**, **T2**, **T3**, **T4** – Types of the emitted values.
---