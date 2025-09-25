# 🧩 InlineSignal&lt;T1, T2, T3&gt;

```csharp
public class InlineSignal<T1, T2, T3> : ISignal<T1, T2, T3>
```

- **Description:** Represents a signal that notifies subscribers with **three values**.
- **Inheritance:** [ISignal&lt;T1, T2, T3&gt;](ISignal%603.md)
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
    - `T3` — the third emitted value

---

## 🏗️ Constructors

#### `InlineSignal(Action<Action<T1, T2, T3>>, Action<Action<T1, T2, T3>>)`

```csharp
public InlineSignal(Action<Action<T1, T2, T3>> subscribe, Action<Action<T1, T2, T3>> unsubscribe)
```

- **Description:** Initializes a new instance with provided subscription and unsubscription delegates.
- **Parameters:**
    - `subscribe` — Action handling subscription logic
    - `unsubscribe` — Action handling unsubscription logic
- **Throws:** `ArgumentNullException` if `subscribe` or `unsubscribe` is null.

---

## 🏹 Methods

#### `Subscribe(Action<T1, T2, T3>)`

```csharp
public Subscription<T1, T2, T3> Subscribe(Action<T1, T2, T3> action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** The active [subscription](Subscription%603.md) that can be used to dispose of
  it.

#### `Unsubscribe(Action<T1, T2, T3>)`

```csharp
public void Unsubscribe(Action<T1, T2, T3> action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.