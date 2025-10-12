# 🧩 Subscription&lt;T1, T2&gt;

Represents a subscription to a <b>signal emitting two values</b>.

---

## 📑 Table of Contents

- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [Subscription(ISignal, Action)](#subscriptionisignalt1-t2-actiont1-t2)
    - [Methods](#-methods)
        - [Dispose()](#dispose)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public readonly struct Subscription<T1, T2> : IDisposable
```

- **Description:** Represents a subscription to a <b>signal emitting two values</b>.
- **Inheritance:** `IDisposable`
- **Type parameters:**
    - `T1` — The type of the first emitted value.
    - `T2` — The type of the second emitted value.

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `Subscription(ISignal<T1, T2>, Action<T1, T2>)`

```csharp
public Subscription(ISignal<T1, T2> signal, Action<T1, T2> action)
```

- **Description:** Initializes a new subscription for a signal emitting two values.
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