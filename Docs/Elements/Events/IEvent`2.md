# 🧩 IEvent&lt;T1, T2&gt;

Represents an event that emits <b>two parameters</b>.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Events](#-events)
        - [OnEvent](#onevent)
    - [Methods](#-methods)
        - [Invoke(T1, T2)](#invoket1-t2)

---

## 🗂 Example of Usage

```csharp
IEvent<string, int> attackEvent = ...

// Subscribe to the event
attackEvent.OnEvent += (attacker, damage) =>
{
    Console.WriteLine($"{attacker} hit for {damage} damage.");
};

// Invoke the event
attackEvent.Invoke("Player", 10);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEvent<T1, T2> : ISignal<T1, T2>, IAction<T1, T2>
```

- **Description:** Represents an event that emits <b>two parameters</b>.
- **Inheritance:** [ISignal&lt;T1, T2&gt;](ISignal%602.md), [IAction&lt;T1, T2&gt;](../Actions/IAction%602.md)
- **Type parameters:**
    - `T1` — The first argument
    - `T2` — The second argument

---

### ⚡ Events

#### `OnEvent`

```csharp
public event Action<T1, T2> OnEvent;
```

- **Description:** Occurs when the signal is emitted with two arguments.
- **Parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value

---

### 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
public void Invoke(T1 arg1, T2 arg2);
```

- **Description:** Executes the action with the specified arguments
- **Parameters:**
    - `arg1` — the first argument
    - `arg2` — the second argument