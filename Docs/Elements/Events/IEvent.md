# 🧩 IEvent

Represents a <b>parameterless event</b> that can be subscribed to and invoked.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Events](#-events)
        - [OnEvent](#onevent)
    - [Methods](#-methods)
        - [Invoke()](#invoke)

---

## 🗂 Example of Usage

```csharp
//Assume we have an instance of event
IEvent playerDiedEvent = ...

// Subscribe to the event
playerDiedEvent.OnEvent += () => Console.WriteLine("Player died event triggered.");

// Invoke the event
playerDiedEvent.Invoke(); // Output: Player died event triggered.
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEvent : ISignal, IAction
```

- **Description:** Represents a <b>parameterless event</b> that can be subscribed to and invoked.
- **Inheritance:** [ISignal](ISignal.md), [IAction](../Actions/IAction.md)

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