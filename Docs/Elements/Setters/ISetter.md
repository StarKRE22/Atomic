# 🧩 ISetter&lt;T&gt;

Defines a contract for **assigning values**.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Properties](#-properties)
        - [Value](#value)
    - [Methods](#-methods)
        - [Invoke(T)](#invoket)

---

## 🗂 Example of Usage

```csharp
// Assume we have an ISetter<Vector3> instance
ISetter<Vector3> moveDirection = ...;

// Assign the forward direction
moveDirection.Value = Vector3.forward;
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface ISetter<in T> : IAction<T>
```

- **Description:** Defines a contract for **assigning values**. 
- **Inheritance:** [IAction&lt;T&gt;](../Actions/IAction%601.md)
- **Type Parameter:** `T` – the type of the value to be set.

---

### 🔑 Properties

#### `Value`

```csharp
public T Value { set; }
```

- **Description:** Assigns the provided value.
- **Parameter:** `value` — the new value to be set.

---

### 🏹 Methods

#### `Invoke(T)`

```csharp
public void Invoke(T arg);
```

- **Description:** Invokes the setter by assigning the provided value.
- **Parameter:** `arg` — the value to set.
- **Notes:** Default implementation comes from [IAction&lt;T&gt;](../Actions/IAction%601.md)