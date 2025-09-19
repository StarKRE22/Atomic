# 🧩 Utils Extensions

Represents a set of **utility extension methods** for working with `IDisposable` objects, collections, and other helper operations.

---

#### AddTo(IDisposable, DisposableComposite)
```csharp
public static void AddTo(this IDisposable it, DisposableComposite composite);
```
- **Description:** Adds the current `IDisposable` instance (`it`) to a `DisposableComposite`.
- **Parameters:**
  - `it` — The `IDisposable` instance to add.
  - `composite` — The `DisposableComposite` that will manage the disposable.
- **Remarks:**
  - Allows chaining multiple disposables directly into a composite.
  - Useful in reactive or event-driven setups to automatically clean up subscriptions or actions.

- **Example of Usage:**

  ```csharp
  //Subscribing to events with automatic disposal
  var composite = new DisposableComposite();
  
  ISignal fireEvent = GetFireEvent();
  fireEvent.Subscribe(() => Console.WriteLine("Fired!")).AddTo(composite);
  
  // Later, when disposing
  composite.Dispose(); // All disposables including the subscription are disposed
  ```

  ```csharp
  var composite = new DisposableComposite();
  
  new DisposableAction(() => Console.WriteLine("Action 1")).AddTo(composite);
  new DisposableAction(() => Console.WriteLine("Action 2")).AddTo(composite);
  
  // Composite disposes all added actions at once
  composite.Dispose();
  // Output:
  // Action 1
  // Action 2
  ```