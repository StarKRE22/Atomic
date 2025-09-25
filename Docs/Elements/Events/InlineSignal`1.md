# 🧩 InlineSignal&lt;T&gt;

```csharp
public class InlineSignal<T> : ISignal<T>
```
- **Description:** Represents a signal that notifies subscribers with a **single value**.
- **Type parameter:** `T` — the emitted value type.
- **Inheritance:** [ISignal&lt;T&gt;](ISignal%601.md)

---

## 🏗️ Constructors

#### `InlineSignal(Action<Action<T>>, Action<Action<T>>)`

```csharp
public InlineSignal(Action<Action<T>> subscribe, Action<Action<T>> unsubscribe)
```

- **Description:** Initializes a new instance with provided delegates.
- **Parameters:**
    - `subscribe` — Action handling subscription logic
    - `unsubscribe` — Action handling unsubscription logic
- **Throws:** `ArgumentNullException` if `subscribe` or `unsubscribe` is null.

---

## 🏹 Methods

#### `Subscribe(Action<T>)`

```csharp
public Subscription<T> Subscribe(Action<T> action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** The active [subscription](Subscription%601.md) that can be used to dispose of it.

#### `Unsubscribe(Action<T>)`

```csharp
public void Unsubscribe(Action<T> action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.