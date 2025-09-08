# 🧩 IReactiveVariable&lt;T&gt;

`IReactiveVariable<T>` represents a **reactive read-write variable** that combines **getter and setter access** with **change notifications**. It inherits from [IVariable&lt;T&gt;](IVariable.md) (read-write access) and [IReactiveValue&lt;T&gt;](../Values/IReactiveValue.md) (reactive observation).

---

## Type Parameter

- `T` – The type of the value.

---

## Properties

#### `Value`
```csharp
new T Value { get; set; }
```
- **Description:** Gets or sets the current value.
- **Access:** Read-write
- **Notes:**
    - Implements [IVariable<T>.Value](IVariable.md#value) for read-write access.
    - Implements [IReactiveValue<T>.Value](../Values/IReactiveValue.md#value) for reactive observation.

---

## Events

#### `OnValueChanged`
```csharp
event System.Action<T> OnValueChanged
```
- **Description:** Triggered whenever the value changes.
- **Parameter**: `T` – The new value after the change.
- **Note:** Allows subscribers to react to value changes in a reactive programming pattern.

---

## Methods

#### `Invoke()`
```csharp
T Invoke()
```
- **Description:** Invokes the variable and returns its current value.
- **Returns:** The current value of type `T`.
- **Note:** Default implementation comes from [IFunction<R>.Invoke()](../Functions/IFunction.md#invoke).

---

## 🗂 Example of Usage

```csharp
public class ReactiveTransformPosition : IReactiveVariable<Vector3>
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