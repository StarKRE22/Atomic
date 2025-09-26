# 🧩 IReactiveVariable&lt;T&gt;

```csharp
public interface IReactiveVariable<T> : IVariable<T>, IReactiveValue<T>
```

- **Description:** Represents a **reactive read-write variable** that combines **getter and setter access** with **change notifications**.
- **Inheritance:** [IVariable&lt;T&gt;](IVariable.md), [IReactiveValue&lt;T&gt;](../Values/IReactiveValue.md)
- **Type Parameter:** `T` – The type of the value.
- **See also:** [ReactiveVariable&lt;T&gt;](ReactiveVariable.md)

---

## 🔑 Properties

#### `Value`

```csharp
public T Value { get; set; }
```

- **Description:** Gets or sets the current value.
- **Access:** Read-write

---

## 🏹 Methods

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

#### `Subscribe(Action)`

```csharp
public Subscription<T> Subscribe(Action action)  
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription&lt;T&gt;](../Events/Subscription%601.md) struct representing the active
  subscription.


#### `Unsubscribe(Action)`

```csharp
public void Unsubscribe(Action action)  
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

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