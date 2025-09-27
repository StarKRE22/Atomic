# 🧩 Utils Extensions

Represents a set of **utility extension methods** for working with `IDisposable` objects, collections, and other helper
operations.

---

## 🏹 Methods

#### `AddTo(IDisposable, DisposableComposite)`

```csharp
public static void AddTo(this IDisposable it, DisposableComposite composite);
```

- **Description:** Adds the current disposable instance to a composite.
- **Parameters:**
    - `it` — The `IDisposable` instance to add.
    - `composite` — The `DisposableComposite` that will manage the disposable.
- **Remarks:**
    - Allows chaining multiple disposables directly into a composite.
    - Useful in reactive or event-driven setups to automatically clean up subscriptions or actions.

---

## 🗂 Examples of Usage

#### `AddTo`

```csharp
//Assume we have some instance of signal
ISignal signal = ...

//Create a container of disposables 
var composite = new DisposableComposite();

//Subscribing to events with automatic disposal
signal
    .Subscribe(() => Console.WriteLine("Hi!"))
    .AddTo(composite);

// Later, when disposing
composite.Dispose(); // All disposables including the subscription are disposed
```