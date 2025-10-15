# 🧩 Utility Extensions

Represents a set of **utility extension methods** for working with `IDisposable` objects, collections, and other helper
operations.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Methods](#-methods)
    - [AddTo(IDisposable, DisposableComposite)](#addtoidisposable-disposablecomposite)
    - [AddTo<T>(T, DisposableComposite<T>)](#addtott-disposablecompositet)

---

## 🗂 Example of Usage

```csharp
//Assume we have some instance of signal
ISignal signal = ...

//Create a container of disposables 
var composite = new DisposableComposite<Subscription>();

//Subscribing to events with automatic disposal
signal.Subscribe(() => Console.WriteLine("Hi!")).AddTo(composite);

// Later, when disposing
composite.Dispose(); // All disposables including the subscription are disposed
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public static class Extensions
```


---

### 🏹 Methods

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

#### `AddTo<T>(T, DisposableComposite<T>)`

```csharp
public static void AddTo<T>(this T it, DisposableComposite<T> composite);
```

- **Description:** Adds the current disposable instance to a composite.
- **Type parameter**: `T` — The `IDisposable` type
- **Parameters:**
  - `it` — The `IDisposable` instance to add.
  - `composite` — The `DisposableComposite<T>` that will manage the disposable.
- **Remarks:**
  - Allows chaining multiple disposables directly into a composite.
  - Avoid boxing to `IDisposable` reference type