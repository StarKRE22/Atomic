# 🧩 IVariable&lt;T&gt;

Represents a **read-write variable** that exposes both **getter** and **setter** interfaces.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Transform Position](#ex1)
    - [Native Buffer Data](#ex2)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Properties](#-properties)
        - [Value](#value)
    - [Methods](#-methods)
        - [Invoke()](#invoke)
        - [Invoke(T)](#invoket)

---

## 🗂 Examples of Usage

This section demonstrates how to implement this interface for different cases:

<div id="ex1"></div>

### 1️⃣ Transform Position

```csharp
public class TransformPositionVariable : IVariable<Vector3>
{
    private readonly Transform _target;

    public TransformPositionVariable(Transform target)
    {
        _target = target ?? throw new ArgumentNullException(nameof(target));
    }

    public Vector3 Value
    {
        get => _target.position;
        set => _target.position = value;
    }
}
```

<div id="ex2"></div>

### 2️⃣ Native Variable

```csharp
public class NativeVariable<T> : IVariable<T> where T : unmanaged
{
    private readonly NativeBuffer _buffer;
    private IntPtr _ptr;
    
    public NativeVariable(NativeBuffer buffer, IntPtr ptr)
    {
        _buffer = buffer;
        _ptr = ptr;
    }

    public T Value
    {
        get => _buffer.ReadData<T>(_ptr);
        set => _buffer.WriteData<T>(_ptr, value);
    }
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IVariable<T> : IValue<T>, ISetter<T>
```

- **Description:** Represents a **read-write variable** that exposes both **getter** and **setter** interfaces.
- **Inheritance:** [IValue&lt;T&gt;](../Values/IValue.md), [ISetter&lt;T&gt;](../Setters/ISetter.md)
- **Type Parameter:** `T` – The type of the value.

---

### 🔑 Properties

#### `Value`

```csharp
public T Value { get; set; }
```

- **Description:** Gets or sets the current value.
- **Access:** Read-write

---

### 🏹 Methods

#### `Invoke()`

```csharp
public T Invoke()
```

- **Description:** Invokes the function and returns the value.
- **Returns:** The current value of type `T`.
- **Notes**: This is the default implementation from [IFunction&lt;R&gt;](../Functions/IFunction.md)

#### `Invoke(T)`

```csharp
public void Invoke(T arg)
```

- **Description:** Sets the value of the variable to the provided argument.
- **Parameter:** `arg` – The new value to assign to the variable.
- **Notes:**
    - Acts as a setter method, complementing the `Value` property.
    - Default implementation comes from [IAction&lt;T&gt;](../Actions/IAction%601.md).