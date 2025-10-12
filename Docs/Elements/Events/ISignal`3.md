# 🧩 ISignal&lt;T1, T2, T3&gt;


## Example of Usage

```csharp
ISignal<string, int, bool> attackEvent = ...

// Subscribe to the event
attackEvent.OnEvent += (attacker, damage, critical) =>
{
    if (critical)
        Console.WriteLine($"{attacker} performed a CRITICAL hit for {damage} damage!");
    else
        Console.WriteLine($"{attacker} hit for {damage} damage.");
};
```

```csharp
public interface ISignal<T1, T2, T3>
```

- **Description:** Represents a signal that notifies subscribers with <b>three values</b>.
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
    - `T3` — the third emitted value

---

## 🏹 Methods

#### `Subscribe(Action<T1, T2, T3>)`

```csharp
public Subscription<T1, T2, T3> Subscribe(Action<T1, T2, T3> action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** The active [subscription](Subscription%603.md) that can be used to dispose of it.

#### `Unsubscribe(Action<T1, T2, T3>)`

```csharp
public void Unsubscribe(Action<T1, T2, T3> action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.