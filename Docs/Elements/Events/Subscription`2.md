# 🧩 Subscription&lt;T1, T2&gt;

```csharp
public readonly struct Subscription<T1, T2> : IDisposable
```

- **Description:** Represents a subscription to a <b>signal emitting two values</b>.
- **Inheritance:** `IDisposable`
- **Type parameters:**
    - `T1` — The type of the first emitted value.
    - `T2` — The type of the second emitted value.

---

## 🏗️ Constructors

#### `Subscription(ISignal<T1, T2>, Action<T1, T2>)`

```csharp
public Subscription(ISignal<T1, T2> signal, Action<T1, T2> action)
```

- **Description:** Initializes a new subscription for a signal emitting two values.
- **Parameters:**
    - `signal` — The signal source.
    - `action` — The delegate to unsubscribe on disposal.

---

## 🏹 Methods

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Unsubscribes the associated action from the signal source.

---

## 🗂 Example of Usage

```csharp
//Assume we have a instance of ISignal
ISignal<T1, T2> signal = ...

//Subscribe on the signal
Subscription<T1, T2> subscription = signal.Subscribe<T1, T2>(lambda);

// Later, dispose to unsubscribe
subscription.Dispose();
```