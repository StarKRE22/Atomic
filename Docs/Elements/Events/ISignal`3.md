# 🧩 ISignal&lt;T1, T2, T3&gt;

Represents a signal that notifies subscribers with <b>three values</b>.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Events](#-events)
    - [OnEvent](#onevent)

---

## 🗂 Example of Usage

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

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface ISignal<T1, T2, T3>
```

- **Description:** Represents a signal that notifies subscribers with <b>three values</b>.
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
    - `T3` — the third emitted value

---

### ⚡ Events

#### `OnEvent`

```csharp
public event Action<T1, T2, T3> OnEvent;
```

- **Description:** Occurs when the signal is emitted with three arguments.
- **Parameters:**
  - `T1` — the first emitted value
  - `T2` — the second emitted value
  - `T3` — the third emitted value