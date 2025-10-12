# 🧩 Subscription&lt;T&gt;

Represents a subscription to a <b>signal emitting one value</b>.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [Subscription(ISignal, Action)](#subscriptionisignalt-actiont)
    - [Methods](#-methods)
        - [Dispose()](#dispose)

---

## 🗂 Example of Usage

```csharp
//Assume we have an instance of ISignal
ISignal<T> signal = ...
    
//Assume we have an instance of Action 
Action<T> action = ...

//Subscribe on the signal
Subscription<T> subscription = new Subscription<T>(signal, action);

// Later, dispose to unsubscribe
subscription.Dispose();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public readonly struct Subscription<T> : IDisposable
```

- **Description:** Represents a subscription to a <b>signal emitting one value</b>.
- **Type parameter:** `T` — The type of the emitted value.
- **Inheritance:** `IDisposable`

---

### 🏗️ Constructors <div id="-constructors"></div>

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