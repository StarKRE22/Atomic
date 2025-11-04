# 🧩 Event&lt;T&gt;

Represents an event that emits <b>one parameter</b>.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Events](#-events)
        - [OnEvent](#onevent)
    - [Methods](#-methods)
        - [Invoke(T)](#invoket)
        - [Dispose()](#dispose)

---

## 🗂 Example of Usage

```csharp
var healthChangedEvent = new Event<int>();

// Subscribe to the event
healthChangedEvent.OnEvent += health => Console.WriteLine($"Health changed to: {health}");

// Invoke the event with a value
healthChangedEvent.Invoke(100); // Output: Health changed to: 100
healthChangedEvent.Invoke(75);  // Output: Health changed to: 75

// Dispose all subscriptions
healthChangedEvent.Dispose();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public class Event<T> : IEvent<T>, IDisposable
```

- **Description:** Represents an event that emits <b>one parameter</b>.
- **Inheritance:** [IEvent&lt;T&gt;](IEvent%601.md), `IDisposable`
- **Type parameter:** `T` — The type of the event argument.
- **Note:** Supports Unity serialization and Odin Inspector

---

### ⚡ Events

#### `OnEvent`

```csharp
public event Action<T> OnEvent;
```

- **Description:** Occurs when the signal is emitted with single argument.
- **Parameters:** `T` — the emitted value.

---

### 🏹 Methods

#### `Invoke(T)`

```csharp
public void Invoke(T arg)
```

- **Description:** Triggers the event with the specified argument.
- **Parameter:** `arg` — The input parameter.

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Clears all subscriptions for this event.