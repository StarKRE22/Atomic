# 🧩 InlineSignal

Provides a wrapper for reactive event source. Implements the corresponding [ISignal](ISignal.md) interface and allow
entities to **subscribe / unsubscribe** from events, optionally with parameters. When subscribing to a signal, the
method returns
a [subscription](../Signals/Subscription.md) struct.

---

<details>
  <summary>
    <h2>🧩 InlineSignal</h2>
    <br> Represents a signal that can notify subscribers of events <b>without passing any data</b>.
  </summary>

<br>

```csharp
public class InlineSignal : ISignal
```

---

### 🏗️ Constructors

#### `InlineSignal(Action<Action>, Action<Action>)`

```csharp
public InlineSignal(Action<Action> subscribe, Action<Action> unsubscribe)
```

- **Description:** Initializes a new instance with provided subscription and unsubscription delegates.
- **Parameters:**
    - `subscribe` — Action handling subscription logic
    - `unsubscribe` — Action handling unsubscription logic
- **Throws:** `ArgumentNullException` if `subscribe` or `unsubscribe` is null.

---

### 🏹 Methods

#### `Subscribe(Action)`

```csharp
public Subscription Subscribe(Action action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** The active [subscription](../Signals/Subscription.md#subscription) that can be used to dispose of it.

#### `Unsubscribe(Action)`

```csharp
public void Unsubscribe(Action action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

</details>

---

<details>
  <summary>
    <h2>🧩 InlineSignal&lt;T&gt;</h2>
    <br> Represents a signal that notifies subscribers with a <b>single value</b>.
  </summary>

```csharp
public class InlineSignal<T> : ISignal<T>
```

- **Description:** Represents a signal that notifies subscribers with a **single value**.
- **Type parameter:** `T` — the emitted value type.

---

### 🏗️ Constructors

#### `InlineSignal(Action<Action<T>>, Action<Action<T>>)`

```csharp
public InlineSignal(Action<Action<T>> subscribe, Action<Action<T>> unsubscribe)
```

- **Description:** Initializes a new instance with provided delegates.
- **Parameters:**
    - `subscribe` — Action handling subscription logic
    - `unsubscribe` — Action handling unsubscription logic
- **Throws:** `ArgumentNullException` if `subscribe` or `unsubscribe` is null.

---

### 🏹 Methods

#### `Subscribe(Action<T>)`

```csharp
public Subscription<T> Subscribe(Action<T> action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** The active [subscription](../Signals/Subscription.md#subscriptiont) that can be used to dispose of it.

#### `Unsubscribe(Action<T>)`

```csharp
public void Unsubscribe(Action<T> action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

</details>

---


<details>
  <summary>
    <h2>🧩 ISignal&lt;T1, T2&gt;</h2>
    <br> Represents a signal that notifies subscribers with <b>two values</b>.
  </summary>

```csharp
public class InlineSignal<T1, T2> : ISignal<T1, T2>
```

- **Description:** Represents a reactive signal with **two parameters**.
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value

---

### 🏗️ Constructors

#### `InlineSignal(Action<Action<T1, T2>>, Action<Action<T1, T2>>)`

```csharp
public InlineSignal(Action<Action<T1, T2>> subscribe, Action<Action<T1, T2>> unsubscribe)
```

- **Description:** Initializes a new instance with provided delegates.
- **Parameters:**
    - `subscribe` — Action handling subscription logic
    - `unsubscribe` — Action handling unsubscription logic
- **Throws:** `ArgumentNullException` if `subscribe` or `unsubscribe` is null.

---

### 🏹 Methods

#### `Subscribe(Action<T1, T2>)`

```csharp
public Subscription<T1, T2> Subscribe(Action<T1, T2> action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:**  The active [subscription](../Signals/Subscription.md#subscriptiont1-t2) that can be used to dispose of
  it.

#### `Unsubscribe(Action<T1, T2>)`

```csharp
public void Unsubscribe(Action<T1, T2> action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

</details>


---


<details>
  <summary>
    <h2>🧩 InlineSignal&lt;T1, T2, T3&gt;</h2>
    <br> Represents a signal that notifies subscribers with <b>three values</b>.
  </summary>

```csharp
public sealed class InlineSignal<T1, T2, T3> : ISignal<T1, T2, T3>
```

- **Description:** Represents a signal that notifies subscribers with **three values**.
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
    - `T3` — the third emitted value

---

### 🏗️ Constructors

#### `InlineSignal(Action<Action<T1, T2, T3>>, Action<Action<T1, T2, T3>>)`

```csharp
public InlineSignal(Action<Action<T1, T2, T3>> subscribe, Action<Action<T1, T2, T3>> unsubscribe)
```

- **Description:** Initializes a new instance with provided subscription and unsubscription delegates.
- **Parameters:**
    - `subscribe` — Action handling subscription logic
    - `unsubscribe` — Action handling unsubscription logic
- **Throws:** `ArgumentNullException` if `subscribe` or `unsubscribe` is null.

---

### 🏹 Methods

#### `Subscribe(Action<T1, T2, T3>)`

```csharp
public Subscription<T1, T2, T3> Subscribe(Action<T1, T2, T3> action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** The active [subscription](../Signals/Subscription.md#subscriptiont1-t2-t3) that can be used to dispose of
  it.

#### `Unsubscribe(Action<T1, T2, T3>)`

```csharp
public void Unsubscribe(Action<T1, T2, T3> action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

</details>

---

<details>
  <summary>
    <h2>🧩 InlineSignal&lt;T1, T2, T3, T4&gt;</h2>
    <br> Represents a signal that notifies subscribers with <b>four values</b>.
  </summary>

```csharp
public sealed class InlineSignal<T1, T2, T3, T4> : ISignal<T1, T2, T3, T4>
```

- **Description:** Represents a signal that notifies subscribers with **four values**.
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
    - `T3` — the third emitted value
    - `T4` — the fourth emitted value

---

### 🏗️ Constructors

#### `InlineSignal(Action<Action<T1, T2, T3, T4>> subscribe, Action<Action<T1, T2, T3, T4>> unsubscribe)`

```csharp
public InlineSignal(Action<Action<T1, T2, T3, T4>> subscribe, Action<Action<T1, T2, T3, T4>> unsubscribe)
```

- **Description:** Initializes a new instance with provided subscription and unsubscription delegates.
- **Parameters:**
    - `subscribe` — Action handling subscription logic
    - `unsubscribe` — Action handling unsubscription logic
- **Throws:** `ArgumentNullException` if `subscribe` or `unsubscribe` is null.

---

### 🏹 Methods

#### `Subscribe(Action<T1, T2, T3, T4>)`

```csharp
public Subscription<T1, T2, T3, T4> Subscribe(Action<T1, T2, T3, T4> action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** The active [subscription](../Signals/Subscription.md#subscriptiont1-t2-t3-t4) that can be used to dispose
  of it.

#### `Unsubscribe(Action<T1, T2, T3, T4>)`

```csharp
public void Unsubscribe(Action<T1, T2, T3, T4> action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

</details>

---

## 🗂 Example of Usage

The following example demonstrates how to wrap the `OnEntered` event from
the [TriggerEvents](../UnityComponents/TriggerEvents.md) class into a `Signal<Collider>`:

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