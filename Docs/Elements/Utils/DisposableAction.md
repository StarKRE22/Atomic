# 🧩 DisposableAction

Represents a lightweight `IDisposable` implementation that invokes a specified action when disposed. Ideal for inline or
ad-hoc cleanup logic.

```csharp
public readonly struct DisposableAction : IDisposable
```

---

## 🏗️ Constructors

#### `DisposableAction(Action)`

```csharp
public DisposableAction(Action action);
```

- **Description:** Initializes a new instance of `DisposableAction` with the specified action.
- **Parameter:** `action` — The action to invoke when `Dispose` is called.
- **Exceptions:** Throws `ArgumentNullException` if `action` is `null`.
- **Remarks:** Use this to create disposable behavior without defining a separate class.

---

## 🏹 Methods

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Invokes the action provided at construction.
- **Remarks:** Once called, the action executes. Useful for automatic cleanup in `using` blocks or when chaining
  disposables.

---

## 🗂 Example of Usage

### 🔹 Inline Cleanup

```csharp
var disposable = new DisposableAction(() => Console.WriteLine("Cleanup executed."));
disposable.Dispose(); // Prints: "Cleanup executed."
```

### 🔹 Event Unsubscription

```csharp
EventHandler handler = (s, e) => Console.WriteLine("Event fired.");
someEvent += handler;

var unsub = new DisposableAction(() => someEvent -= handler);
unsub.Dispose();
```