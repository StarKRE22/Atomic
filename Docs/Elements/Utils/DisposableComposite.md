# 🧩 DisposableComposite

Represents a **composite disposable container** that manages multiple `IDisposable` objects.
Disposing the composite will automatically dispose all contained objects, making resource management easier and safer.

> [!TIP]
> For greater convenience, use [AddTo](Extensions.md/#addtoidisposable-disposablecomposite) extension method for
> chaining disposables into a composite.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Constructors](#-constructors)
    - [DisposableComposite(IEnumerable<IDisposable>)](#disposablecompositeienumerableidisposable)
    - [DisposableComposite(params IDisposable[])](#disposablecompositeparams-idisposable)
  - [Properties](#-properties)
    - [Count](#count)
  - [Methods](#-methods)
    - [Add(IDisposable)](#addidisposable)
    - [Dispose()](#dispose)
- [Best Practices](#-best-practices)


---

## 🗂 Example of Usage

```csharp
//Assume we have some instance of signal
ISignal signal = ...

//Create a container of disposables 
var composite = new DisposableComposite();

//Subscribing to the signal
IDisposable disposable = signal.Subscribe(() => Console.WriteLine("Hi!"));

//Add subscription to the composite
composite.Add(disposable);

// Later, when disposing
composite.Dispose(); // All disposables including the subscription are disposed
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class DisposableComposite : DisposableComposite<IDisposable>
```

- **Inheritance:** [DisposableComposite\<T>](DisposableComposite%601.md)

---

<div id="-constructors"></div>

### 🏗️ Constructors

#### `DisposableComposite(IEnumerable<IDisposable>)`

```csharp
public DisposableComposite(IEnumerable<IDisposable> disposables);
```

- **Description:** Initializes a new instance of `DisposableComposite` with a collection of disposables.
- **Parameter:** `disposables` — Initial disposables to add.
- **Remarks:** Copies all disposables from the provided collection into the internal list.

#### `DisposableComposite(params IDisposable[])`

```csharp
public DisposableComposite(params IDisposable[] disposables);
```

- **Description:** Initializes a new instance of `DisposableComposite` with a params array of disposables.
- **Parameter:** `disposables` — Initial disposables to add.
- **Remarks:** Provides a convenient way to pass multiple disposables inline.

---

### 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** Gets the number of disposables currently in the composite.
- **Remarks:** Useful to check how many disposables are managed before disposal.

---

### 🏹 Methods

#### `Add(IDisposable)`

```csharp
public DisposableComposite Add(IDisposable disposable);
```

- **Description:** Adds a new `IDisposable` to the composite.
- **Parameter:** `disposable` — The disposable object to add. Cannot be `null`.
- **Returns:** The current `DisposableComposite` instance for chaining.
- **Exceptions:** Throws `ArgumentNullException` if `disposable` is `null`.
- **Remarks:** Use this method to dynamically add disposables after initialization.

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Disposes all contained `IDisposable` objects and clears the composite.
- **Remarks:** After calling this method, the internal list is empty. The composite can be reused by adding new
  disposables.

---

## 📌 Best Practices

- [Using DisposeComposite in EntityInstallers](../../BestPractices/UsingSubscriptionsWithDisposeComposite.md)