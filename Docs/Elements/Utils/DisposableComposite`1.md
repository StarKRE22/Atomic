# 🧩 DisposableComposite\<T>

Represents a **generic composite disposable container** that manages multiple `IDisposable` objects of type `T`.
Disposing the composite will automatically dispose all contained objects, making resource management easier and safer.

> [!TIP]
> For greater convenience, use this generic version when all disposables share the same type, allowing type safety and
> boxing avoidance to `IDisposable` reference.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [DisposableComposite(IEnumerable<T>)](#disposablecompositeienumerablet)
        - [DisposableComposite(params T[])](#disposablecompositeparams-t)
    - [Properties](#-properties)
        - [Count](#count)
    - [Methods](#-methods)
        - [Add(T)](#addt)
        - [Dispose()](#dispose)
- [Notes](#-notes)
- [Best Practices](#-best-practices)

---

## 🗂 Example of Usage

Below is an example of using generic disposable for struct [Subscriptions](../Events/Subscriptions.md) that guarantees
boxing avoidance:

```csharp
// Assume we have multiple subscriptions
Subscription subscription1 = ...
Subscription subscription2 = ...

// Create a generic composite without boxing to IDisposable
var composite = new DisposableComposite<Subscription>(subscription1, subscription2);

// Add another disposable dynamically
IDisposable subscription3 = ...
composite.Add(subscription3);

// Later, unsubscribe all
composite.Dispose(); // All subscriptions are disposed and the internal list is cleared
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public class DisposableComposite<T> : IDisposable where T : IDisposable
```

- **Description:** Represents a **generic composite disposable container**.
- **Inheritance:** `IDisposable`

---

<div id="-constructors"></div>

### 🏗️ Constructors

#### `DisposableComposite(IEnumerable<T>)`

```csharp
public DisposableComposite(IEnumerable<T> disposables)
```

- **Description:** Initializes a new instance with a collection of disposables.
- **Parameter:** `disposables` — Initial disposables to add.
- **Remarks:** Copies all disposables from the provided collection into the internal list.

#### `DisposableComposite(params T[])`

```csharp
public DisposableComposite(params T[] disposables)
```

- **Description:** Initializes a new instance with a params array of disposables.
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

#### `Add(T)`

```csharp
public DisposableComposite<T> Add(T disposable)
```

- **Description:** Adds a new disposable to the composite.
- **Parameter:** `disposable` — The disposable object to add. Cannot be `null`.
- **Returns:** The current `DisposableComposite<T>` instance for chaining.
- **Exceptions:** Throws `ArgumentNullException` if `disposable` is `null`.
- **Remarks:** Use this method to dynamically add disposables after initialization.

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Disposes all contained disposables and clears the composite.
- **Remarks:** After calling this method, the internal list is empty. The composite can be reused by adding new
  disposables.

---

## 📝 Notes

- Use this generic version when all disposables are of the same type for **type safety** and boxing avoidance.
- Combine with [AddTo](Extensions.md#addtott-disposablecompositet) extension methods if available for **chaining disposables** into the composite.

---

## 📌 Best Practices

- [Using DisposeComposite in EntityInstallers](../../BestPractices/UsingSubscriptionsWithDisposeComposite.md)
