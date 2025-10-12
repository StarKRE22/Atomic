# 🧩 Subscription&lt;T1, T2, T3, T4&gt;

```csharp
public readonly struct Subscription<T1, T2, T3, T4> : IDisposable
```

- **Description:** Represents a subscription to a <b>signal emitting four values</b>.
- **Inheritance:** `IDisposable`
- **Type parameters:**
    - `T1` — The type of the first emitted value.
    - `T2` — The type of the second emitted value.
    - `T3` — The type of the third emitted value.
    - `T4` — The type of the fourth emitted value.

---

## 🏗️ Constructors

#### `Subscription(ISignal<T1, T2, T3, T4>, Action<T1, T2, T3, T4>)`

```csharp
public Subscription(ISignal<T1, T2, T3, T4> signal, Action<T1, T2, T3, T4> action)
```

- **Description:** Initializes a new subscription for a signal emitting four values.
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
