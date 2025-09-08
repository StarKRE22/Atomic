# 🧩 IVariable&lt;T&gt;

`IVariable<T>` represents a **read-write variable** that exposes both **getter** and **setter** interfaces. It combines the functionality of [IValue&lt;T&gt;](../Values/IValue.md) (read-only access) and [ISetter&lt;T&gt;](../Setters/ISetter.md) (write access).

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
  - Implements [IValue<T>.Value](../Values/IValue.md#value) for read access.
  - Implements [ISetter<T>.Value](../Setters/ISetter.md/#value) for write access.

---

## Methods

#### `Invoke()`
```csharp
T Invoke()
```
- **Description:** Invokes the function and returns the value.
- **Returns:** The current value of type `T`.
- **Notes**: This is the default implementation from [IFunction&lt;R&gt;.Invoke()](../Functions/IFunction.md#invoke)


#### `Invoke(T arg)`
```csharp
void Invoke(T arg)
```
- **Description:** Sets the value of the variable to the provided argument.
- **Parameter:** `arg` – The new value to assign to the variable.
- **Notes:**
  - Acts as a setter method, complementing the `Value` property.
  - Default implementation comes from [IAction<T>.Invoke()](../Actions/IAction.md#invoket).

---

## 🗂 Example of Usage

This section demonstrates how to implement `IVariable<T>` for **Transform position** and a **networked variable**.

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

```csharp
public class NetworkVariable<T> : IVariable<T> where T : unmanaged
{
    private readonly NetworkObject _networkObject;
    private IntPtr _ptr;
    
    public NetworkVariable(NetworkObject networkObject, IntPtr ptr)
    {
        _networkObject = networkObject ?? throw new ArgumentNullException(nameof(networkObject));
        _ptr = ptr;
    }

    public T Value
    {
        get => _networkObject.ReadData<T>(_ptr);
        set => _networkObject.WriteData<T>(_ptr, value);
    }
}
```