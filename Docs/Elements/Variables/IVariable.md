# 🧩 IVariable&lt;T&gt;

```csharp
public interface IVariable<T> : IValue<T>, ISetter<T>
```

- **Description:** Represents a **read-write variable** that exposes both **getter** and **setter** interfaces.
- **Inheritance:** [IValue&lt;T&gt;](../Values/IValue.md), [ISetter&lt;T&gt;](../Setters/ISetter.md)
- **Type Parameter:** `T` – The type of the value.

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

---

## 🗂 Examples of Usage

This section demonstrates how to implement `IVariable<T>` for different cases.

### 1️⃣ Wrapping Transform Position

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

---

### 2️⃣ Wrapping Network Buffer Data

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