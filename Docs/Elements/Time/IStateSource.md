# 🧩 IStateSource&lt;T&gt;

Represents a source that <b>provides state notifications</b>.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Events](#-events)
        - [OnStateChanged](#onstatechanged)
    - [Methods](#-methods)
        - [GetState()](#getstate)
---

## 🗂 Example of Usage

```csharp
// Assume we have an IStateSource<T> instance
IStateSource<T> stateSource = ...;

// Subscribe to the state change event
stateSource.OnStateChanged += (state) =>
{
    Console.WriteLine($"State changed to: {state}");
};

// Get the current state
T currentState = stateSource.GetState();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IStateSource<T>
```

- **Type Parameter:** `T` — Enum type representing the state.

--- 

### ⚡ Events

#### `OnStateChanged`

```csharp
public event Action<T> OnStateChanged;  
```

- **Description:** Raised when the state changes.

---

### 🏹 Methods

#### `GetState()`

```csharp
public T GetState();  
```

- **Description:** Gets the current internal state.
- **Returns:** The current state of type `T`.