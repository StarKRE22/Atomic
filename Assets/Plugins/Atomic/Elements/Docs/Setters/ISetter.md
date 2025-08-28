# ISetter Interface

The **ISetter<T>** interface represents a **setter contract** that accepts a value of type `T`.  
It extends `IAction<T>` to provide a unified way to assign values through both direct property access and method invocation.

## Key Features
- **Type-Safe Assignment** – Ensures the value being set matches the specified generic type `T`.
- **Action Integration** – Implements `IAction<T>` so that the setter can be invoked like a method.
- **Performance** – Uses `[MethodImpl(MethodImplOptions.AggressiveInlining)]` for efficient invocation.

---

## ISetter<T>

```csharp
public interface ISetter<in T> : IAction<T>
{
    /// <summary>
    /// Sets the value.
    /// </summary>
    T Value { set; }

    /// <summary>
    /// Invokes the setter by assigning the provided value.
    /// </summary>
    /// <param name="arg">The value to set.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void IAction<T>.Invoke(T arg) => this.Value = arg;
}
```

## Notes
**Direct Assignment** – The `Value` property allows direct setting of the value.
**Invocation Syntax** – Calling `Invoke(value)` is equivalent to `Value = value`.
**Contravariance** – The `in T` modifier allows flexibility with compatible types for input.