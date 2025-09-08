#  🧩 IFunction

The **IFunction** interfaces define a family of contracts for representing functions with varying numbers of input parameters.  
They provide a lightweight abstraction for defining logic that returns a value, making them useful for callbacks, computations, and functional programming patterns.

## Key Features
- **Decoupling** – Define functions independently of concrete implementations.
- **Extensibility** – Supports multiple overloads (0–2 parameters).
- **Reusability** – Suitable for event-driven systems, pipelines, or functional utilities.

---


## IFunction&lt;R&gt;
Represents a **parameterless function that returns a result**.

```csharp
public interface IFunction<out R>
{
    /// <summary>
    /// Invokes the function and returns the result.
    /// </summary>
    /// <returns>The result of the function.</returns>
    R Invoke();
}
```

## `Invoke()`


## IFunction<T, R>
Represents a function with one input argument that returns a result.
```csharp
public interface IFunction<in T, out R>
{
    /// <summary>
    /// Invokes the function with the specified argument and returns the result.
    /// </summary>
    /// <param name="args">The input argument.</param>
    /// <returns>The result of the function.</returns>
    R Invoke(T args);
}
```
### Invoke(T)


### IFunction<T1, T2, R>
```csharp
public interface IFunction<in T1, in T2, out R>
{
    /// <summary>
    /// Invokes the function with the specified arguments and returns the result.
    /// </summary>
    /// <param name="args1">The first input argument.</param>
    /// <param name="args2">The second input argument.</param>
    /// <returns>The result of the function.</returns>
    R Invoke(T1 args1, T2 args2);
}
```

### Invoke(T1, T2)
