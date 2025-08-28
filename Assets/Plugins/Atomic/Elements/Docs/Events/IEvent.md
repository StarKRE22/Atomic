#  🧩 IEvent Interfaces

The **IEvent** interfaces define a family of contracts for **reactive events** that can be both observed and invoked.  
They combine the features of `ISignal` (reactive observation) and `IAction` (invocation logic) to create a unified event abstraction.

## Key Features
- **Reactive and Invokable** – Supports event observation and active triggering.
- **Strong Typing** – Supports 0–4 strongly typed parameters.
- **Flexibility** – Can be used for message broadcasting, event-driven systems, or reactive pipelines.
- **Integration** – Works seamlessly with `ISignal` and `IAction` infrastructure.
---
## IEvent
Represents a **parameterless reactive event**.
```csharp
public interface IEvent : ISignal, IAction
{
    /// <summary>
    /// Occurs when the event is triggered.
    /// </summary>
    event Action OnEvent;
}
```
---
## IEvent&lt;R&gt;
Represents a **reactive event with one parameter**.
```csharp
public interface IEvent<T> : ISignal<T>, IAction<T>
{
    /// <summary>
    /// Occurs when the event is triggered with the specified argument.
    /// </summary>
    event Action<T> OnEvent;
}
```
---
## IEvent<T1, T2>
Represents a **reactive event with two parameters**.
```csharp
public interface IEvent<T1, T2> : ISignal<T1, T2>, IAction<T1, T2>
{
    /// <summary>
    /// Occurs when the event is triggered with two arguments.
    /// </summary>
    event Action<T1, T2> OnEvent;
}
```
---
### IEvent<T1, T2, T3>
Represents a **reactive event with three parameters**.
```csharp
public interface IEvent<T1, T2, T3> : ISignal<T1, T2, T3>, IAction<T1, T2, T3>
{
    /// <summary>
    /// Occurs when the event is triggered with three arguments.
    /// </summary>
    event Action<T1, T2, T3> OnEvent;
}
```
---
### IEvent<T1, T2, T3, T4>
Represents a **reactive event with four parameters**.
```csharp
public interface IEvent<T1, T2, T3, T4> : ISignal<T1, T2, T3, T4>, IAction<T1, T2, T3, T4>
{
    /// <summary>
    /// Occurs when the event is raised with four arguments.
    /// </summary>
    event Action<T1, T2, T3, T4> OnEvent;
}
```