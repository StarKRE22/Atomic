# 🧩 DisposableComposite

```csharp
public class DisposableComposite : IDisposable
```

- **Description:** Represents a **composite disposable container** that manages multiple `IDisposable` objects.
  Disposing the composite
  will automatically dispose all contained objects, making resource management easier and safer.
- **Inheritance:** `IDisposable`

> [!TIP]
> For greater convenience, use [AddTo](Extensions.md/#addtoidisposable-disposablecomposite) extension method for
> chaining disposables into a composite.


---

## 🏗️ Constructors

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

## 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** Gets the number of disposables currently in the composite.
- **Remarks:** Useful to check how many disposables are managed before disposal.

---

## 🏹 Methods

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