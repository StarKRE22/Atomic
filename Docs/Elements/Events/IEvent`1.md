# 🧩 IEvent&lt;T&gt;

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

---

## 🗂 Example of Usage

```csharp
//Assume we have an instance of event
IEvent<int> healthChangedEvent = ...

// Subscribe to the event
healthChangedEvent.OnEvent += health => Console.WriteLine($"Health changed to: {health}");

// Invoke the event with a value
healthChangedEvent.Invoke(100); // Output: Health changed to: 100
healthChangedEvent.Invoke(75);  // Output: Health changed to: 75
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEvent<T> : ISignal<T>, IAction<T>
```

- **Description:** Represents an event that emits <b>one parameter</b>.
- **Type parameter:** `T` — The type of the event parameter.
- **Inheritance:** [ISignal&lt;T&gt;](ISignal%601.md), [IAction&lt;T&gt;](../Actions/IAction%601.md)

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
public void Invoke(T arg);
```

- **Description:** Executes the event with the specified argument
- **Parameter:** `arg` — the input parameter