# 🧩 IReactiveVariable&lt;T&gt;

Represents a **reactive read-write variable** that combines **getter and setter access** with **change notifications**.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Events](#-events)
        - [OnEvent](#onevent)
    - [Properties](#-properties)
        - [Value](#value)
    - [Methods](#-methods)
        - [Invoke()](#invoke)
        - [Invoke(T)](#invoket)

---

## 🗂 Example of Usage

Below is an example of creating a reactive wrapper around `Transform.position`

```csharp
public sealed class ReactiveTransformPosition : IReactiveVariable<Vector3>
{
    public event Action<Vector3> OnValueChanged;
    
    public Vector3 Value
    {
        get => _value;
        set
        {
            if (_value != value)
            {
                _value = value;
                _target.position = value;
                OnValueChanged?.Invoke(value);
            }
        }
    }
    
    private readonly Transform _target;
    private Vector3 _value;

    public ReactiveTransformPosition(Transform target)
    {
        _target = target ?? throw new ArgumentNullException(nameof(target));
        _value = _target.position;
    }
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IReactiveVariable<T> : IVariable<T>, IReactiveValue<T>
```

- **Description:** Represents a **reactive read-write variable** that combines **getter and setter access** with 
  **change notifications**.
- **Inheritance:** [IVariable&lt;T&gt;](IVariable.md), [IReactiveValue&lt;T&gt;](../Values/IReactiveValue.md)
- **Type Parameter:** `T` – The type of the value.
- **See also:** [ReactiveVariable&lt;T&gt;](ReactiveVariable.md)

---

### ⚡ Events

#### `OnEvent`

```csharp
public event Action<T> OnEvent;
```

- **Description:** Occurs when the signal is emitted with single argument.
- **Parameters:** `T` — the emitted value.

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
public T Invoke();
```

- **Description:** Invokes the variable and returns its current value.
- **Returns:** The current value of type `T`.
- **Note:** Default implementation comes from [IFunction&lt;R&gt;](../Functions/IFunction.md).

#### `Invoke(T)`

```csharp
public void Invoke(T arg)
```

- **Description:** Sets the value of the variable to the provided argument.
- **Parameter:** `arg` – The new value to assign to the variable.
- **Notes:**
    - Acts as a setter method, complementing the `Value` property.
    - Default implementation comes from [IAction&lt;T&gt;](../Actions/IAction%601.md).