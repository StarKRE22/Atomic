# 🧩 IReactiveVariable&lt;T&gt;

`IReactiveVariable<T>` represents a **reactive read-write variable** that combines **getter and setter access** with **change notifications**. It inherits from [IVariable&lt;T&gt;](IVariable.md) (read-write access) and [IReactiveValue&lt;T&gt;](../Values/IReactiveValue.md) (reactive observation).

---

## Type Parameter

- `T` – The type of the value.

---

## Events

#### `OnValueChanged`
```csharp
event Action<T> OnValueChanged
```
- **Description:** Triggered whenever the value changes.
- **Parameter**: `T` – The new value after the change.
- **Note:** Allows subscribers to react to value changes in a reactive programming pattern.

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

## Methods

#### `Invoke()`
```csharp
T Invoke()
```
- **Description:** Invokes the variable and returns its current value.
- **Returns:** The current value of type `T`.
- **Note:** Default implementation comes from [IFunction<R>.Invoke()](../Functions/IFunction.md#invoke).

#### `Invoke(T arg)`
```csharp
void Invoke(T arg)
```
- **Description:** Sets the value of the variable to the provided argument.
- **Parameter:** `arg` – The new value to assign to the variable.
- **Notes:** 
  - Acts as a setter method, complementing the `Value` property.
  - Default implementation comes from [IAction<T>.Invoke()](../Actions/IAction.md#invoket).

#### `Subscribe(Action)`
```csharp
Subscription<T> Subscribe(Action action)  
```
- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription&lt;T&gt;](../Signals/Subscription.md#subscriptiont) struct representing the active subscription.
- **Notes**: This is the default implementation from [ISignal&lt;T&gt;.Subscribe()](../Signals/ISignal.md#subscribeactiont)

#### `Unsubscribe(Action)`
```csharp
void `Unsubscribe(Action action)`  
```
- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.
- **Notes**: This is the default implementation from [ISignal&lt;T&gt;.Unsubscribe()](../Signals/ISignal.md#unsubscribeactiont)

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