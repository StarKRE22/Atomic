# 🧩 ISignal&lt;T&gt;

Represents a signal that notifies subscribers with a <b>single value</b>.

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
// Assume we have an instance of "health changed event"
ISignal<int> healthChangedEvent = ...

// Subscribe to the event
healthChangedEvent.OnEvent += health => Console.WriteLine($"Health changed to: {health}");
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface ISignal<T>
```

- **Description:** Represents a signal that notifies subscribers with a <b>single value</b>.
- **Type parameter:** `T` — the emitted value type.

---

### ⚡ Events

#### `OnEvent`

```csharp
public event Action<T> OnEvent;
```

- **Description:** Occurs when the signal is emitted with single argument.
- **Parameters:** `T` — the emitted value.