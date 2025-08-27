# IAction Interfaces

The **IAction** interfaces define a family of contracts for executing parameterized actions.  
They provide a lightweight abstraction for invoking logic, often used in event systems, command patterns, or reactive programming.

## Key Features
- **Decoupling** – Define actions without referencing concrete implementations.
- **Extensibility** – Supports multiple overloads (0–4 parameters).
- **Reusability** – Useful in generic systems, event dispatchers, or data-binding scenarios.

---
## IAction
Represents a **parameterless executable action**.

```csharp
public interface IAction
{
    /// <summary>
    /// Executes the action logic.
    /// </summary>
    void Invoke();
}
```

## IAction<T>
Represents an executable action that takes one argument.
```csharp
public interface IAction<in T>
{
    /// <summary>
    /// Executes the action with the specified argument.
    /// </summary>
    /// <param name="arg">The input parameter.</param>
    void Invoke(T arg);
}
```

## IAction<T1, T2>
Represents an executable action that takes two arguments.
```csharp
public interface IAction<in T1, in T2>
{
    /// <summary>
    /// Executes the action with the specified arguments.
    /// </summary>
    /// <param name="arg1">The first argument.</param>
    /// <param name="arg2">The second argument.</param>
    void Invoke(T1 arg1, T2 arg2);
}
```
### IAction<T1, T2, T3>
Represents an executable action that takes three arguments.
```csharp
public interface IAction<in T1, in T2, in T3>
{
    /// <summary>
    /// Executes the action with the specified arguments.
    /// </summary>
    /// <param name="arg1">The first argument.</param>
    /// <param name="arg2">The second argument.</param>
    /// <param name="arg3">The third argument.</param>
    void Invoke(T1 arg1, T2 arg2, T3 arg3);
}
```

#### IAction<T1, T2, T3, T4>
Represents an executable action that takes four arguments.
```csharp
public interface IAction<in T1, in T2, in T3, in T4>
{
    /// <summary>
    /// Invokes the action with the specified arguments.
    /// </summary>
    /// <param name="arg1">The first argument.</param>
    /// <param name="arg2">The second argument.</param>
    /// <param name="arg3">The third argument.</param>
    /// <param name="arg4">The fourth argument.</param>
    void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
}
```