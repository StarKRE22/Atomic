# 🧩 InlineSignal&lt;T1, T2&gt;

```csharp
public class InlineSignal<T1, T2> : ISignal<T1, T2>
```

- **Description:** Represents a reactive signal with **two parameters**.
- **Inheritance:** [ISignal&lt;T1, T2&gt;](ISignal%602.md)
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value

---

## 🏗️ Constructors

#### `InlineSignal(Action<Action<T1, T2>>, Action<Action<T1, T2>>)`

```csharp
public InlineSignal(Action<Action<T1, T2>> subscribe, Action<Action<T1, T2>> unsubscribe)
```

- **Description:** Initializes a new instance with provided delegates.
- **Parameters:**
    - `subscribe` — Action handling subscription logic
    - `unsubscribe` — Action handling unsubscription logic
- **Throws:** `ArgumentNullException` if `subscribe` or `unsubscribe` is null.

---

## 🏹 Methods

#### `Subscribe(Action<T1, T2>)`

```csharp
public Subscription<T1, T2> Subscribe(Action<T1, T2> action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:**  The active [subscription](Subscription%602.md) that can be used to dispose of
  it.

#### `Unsubscribe(Action<T1, T2>)`

```csharp
public void Unsubscribe(Action<T1, T2> action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.