# 🧩 Subscription

```csharp
public readonly struct Subscription : IDisposable
```

- **Description:** Represents a subscription to a <b>parameterless signal</b>.
- **Inheritance:** `IDisposable`

---

## 🏗️ Constructors

#### `Subscription(ISignal, Action)`

```csharp
public Subscription(ISignal signal, Action action)
```

- **Description:** Initializes a new subscription for a parameterless signal.
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
ISignal signal = ...

//Subscribe on the signal    
Subscription subscription = signal.Subscribe(lambda);

// Later, dispose to unsubscribe
subscription.Dispose();
```