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


## 🧩 Examples Usage

This section demonstrates how to implement `IVariable<T>` for **Transform position** and a **networked variable**.

```csharp
using UnityEngine;
using Atomic.Elements;
using System;

// -------------------- Transform Position Variable --------------------
public class TransformPositionVariable : IVariable<Vector3>
{
    private Transform _target;

    public TransformPositionVariable(Transform target)
    {
        _target = target;
    }

    public Vector3 Value
    {
        get => _target.position;
        set => _target.position = value;
    }
}

// -------------------- Network Variable --------------------
public class NetworkVariable<T> : IVariable<T> where T : unmanaged
{
    private readonly NetworkObject _networkObject;
    private IntPtr _ptr;
    
    public NetworkVariable(NetworkObject networkObject, IntPtr ptr)
    {
        _networkObject = networkObject;
        _ptr = ptr;
    }

    public T Value
    {
        get => _networkObject.ReadData<T>(_ptr);
        set => _networkObject.WriteData<T>(_ptr, value);
    }
}
```