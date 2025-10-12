# 🧩 Subscription

Represents a subscription to a <b>parameterless signal</b>.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [Subscription(ISignal, Action)](#subscriptionisignal-action)
    - [Methods](#-methods)
        - [Dispose()](#dispose)

---

## 🗂 Example of Usage

```csharp
//Assume we have an instance of ISignal
ISignal signal = ...
    
//Assume we have an instance of Action 
Action action = ...

//Subscribe on the signal    
Subscription subscription = new Subscription(signal, action);

// Later, dispose to unsubscribe
subscription.Dispose();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public readonly struct Subscription : IDisposable
```

- **Description:** Represents a subscription to a <b>parameterless signal</b>.
- **Inheritance:** `IDisposable`

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `Subscription(ISignal, Action)`

```csharp
public Subscription(ISignal signal, Action action)
```

- **Description:** Initializes a new subscription for a parameterless signal.
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