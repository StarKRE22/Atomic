
<details>
  <summary>
    <h2 id="subscriptiont">🧩 Subscription&lt;T&gt;</h2>
    <br> Represents a subscription to a <b>signal emitting one value</b>.
  </summary>

<br>

```csharp
public readonly struct Subscription<T> : IDisposable
```

- **Type parameter:** `T` — The type of the emitted value.

---

### 🏗️ Constructors

#### `Subscription(ISignal<T>, Action<T>)`

```csharp
public Subscription(ISignal<T> signal, Action<T> action)
```

- **Description:** Initializes a new subscription for a signal emitting one value.
- **Parameters:**
    - `signal` — The signal source.
    - `action` — The delegate to unsubscribe on disposal.

---

### 🏹 Methods

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Unsubscribes the associated action from the signal source.

---

### 🗂 Example of Usage

```csharp
//Assume we have a instance of ISignal
ISignal<T> signal = ...

//Subscribe on the signal
Subscription<T> subscription = signal.Subscribe<T>(lambda);

// Later, dispose to unsubscribe
subscription.Dispose();
```

</details>