# 🧩 Event

Represents a <b>parameterless event</b> that can be subscribed and invoked.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Events](#-events)
        - [OnEvent](#onevent)
    - [Methods](#-methods)
        - [Invoke()](#invoke)
        - [Dispose()](#dispose)
---

## 🗂 Examples of Usage

```csharp
var playerDiedEvent = new Event();

// Subscribe to the event
playerDiedEvent.OnEvent += () => Console.WriteLine("Player died event triggered.");

// Invoke the event
playerDiedEvent.Invoke(); // Output: Player died event triggered.

// Dispose all subscriptions
playerDiedEvent.Dispose();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public class Event : IEvent, IDisposable
```

- **Description:** Represents a <b>parameterless event</b> that can be subscribed to and invoked.
- **Inheritance:** [IEvent](IEvent.md), `IDisposable`
- **Note:** Supports Unity serialization and Odin Inspector

---

### ⚡ Events

#### `OnEvent`

```csharp
public event Action OnEvent;
```

- **Description:** Occurs when the signal is emitted with single argument

---

### 🏹 Methods

#### `Invoke()`

```csharp
public void Invoke();
```

- **Description:** Executes the event logic

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Clears all subscriptions for this event.
