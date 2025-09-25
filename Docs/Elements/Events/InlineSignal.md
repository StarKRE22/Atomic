# 🧩 InlineSignal

```csharp
public class InlineSignal : ISignal
```

- **Description:** Represents a signal that can notify subscribers of events <b>without passing any data</b>.
- **Inheritance:** [ISignal](ISignal.md)

---

## 🏗️ Constructors

#### `InlineSignal(Action<Action>, Action<Action>)`

```csharp
public InlineSignal(Action<Action> subscribe, Action<Action> unsubscribe)
```

- **Description:** Initializes a new instance with provided subscription and unsubscription delegates.
- **Parameters:**
    - `subscribe` — Action handling subscription logic
    - `unsubscribe` — Action handling unsubscription logic
- **Throws:** `ArgumentNullException` if `subscribe` or `unsubscribe` is null.

---

## 🏹 Methods

#### `Subscribe(Action)`

```csharp
public Subscription Subscribe(Action action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** The active [subscription](Subscription.md) that can be used to dispose of it.

#### `Unsubscribe(Action)`

```csharp
public void Unsubscribe(Action action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.
