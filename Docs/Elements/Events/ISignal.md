# 🧩 ISignal

Represents a signal that can notify subscribers of events <b>without passing any data</b>.

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
// Assume we have an instance of "player died event"
ISignal playerDiedEvent = ...

// Subscribe to the event
playerDiedEvent.OnEvent += () => Console.WriteLine("Player died event triggered.");
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface ISignal
```

- **Description:** Represents a signal that can notify subscribers of events <b>without passing any data</b>.

---

### ⚡ Events

#### `OnEvent`

```csharp
public event Action OnEvent;
```

- **Description:** Occurs when the signal is emitted with single argument