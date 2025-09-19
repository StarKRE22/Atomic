# 🧩 IVariable&lt;T&gt;

Represents a **read-write variable** that exposes both **getter** and **setter** interfaces. It combines the
functionality of [IValue&lt;T&gt;](../Values/IValue.md) (read-only access)
and [ISetter&lt;T&gt;](../Setters/ISetter.md) (write access).

```csharp
public interface IVariable<T> : IValue<T>, ISetter<T>
```

- **Type Parameter:** `T` – The type of the value.

---

## 🔑 Properties

#### `Value`

```csharp
public T Value { get; set; }
```

- **Description:** Gets or sets the current value.
- **Access:** Read-write
- **Notes:**
    - Implements [IValue&lt;T&gt;.Value](../Values/IValue.md#value) for read access.
    - Implements [ISetter&lt;T&gt;.Value](../Setters/ISetter.md/#value) for write access.

---

## 🏹 Methods

#### `Invoke()`

```csharp
public T Invoke()
```

- **Description:** Invokes the function and returns the value.
- **Returns:** The current value of type `T`.
- **Notes**: This is the default implementation from [IFunction&lt;R&gt;.Invoke()](../Functions/IFunction.md#invoke)

#### `Invoke(T)`

```csharp
public void Invoke(T arg)
```

- **Description:** Sets the value of the variable to the provided argument.
- **Parameter:** `arg` – The new value to assign to the variable.
- **Notes:**
    - Acts as a setter method, complementing the `Value` property.
    - Default implementation comes from [IAction&lt;R&gt;.Invoke()](../Actions/IAction.md#invoket).

---

## 🗂 Example of Usage

This section demonstrates how to implement `IVariable<T>` for different cases.

### Example #1: Wrap `Transform.position`

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

### Example #2: Wrap Pointer of `Network Buffer`

```csharp
public class NetworkVariable<T> : IVariable<T> where T : unmanaged
{
    private readonly NetworkBuffer _networkBuffer;
    private IntPtr _ptr;
    
    public NetworkVariable(NetworkBuffer networkBuffer, IntPtr ptr)
    {
        _networkBuffer = networkBuffer;
        _ptr = ptr;
    }

    public T Value
    {
        get => _networkBuffer.ReadData<T>(_ptr);
        set => _networkBuffer.WriteData<T>(_ptr, value);
    }
}
```