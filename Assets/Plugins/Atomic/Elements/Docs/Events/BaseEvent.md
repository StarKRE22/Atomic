# 🧩 BaseEvent Classes
The **BaseEvent** classes provide a concrete implementation of the `IEvent` interfaces.  
They support **subscription**, **invocation**, and **disposal** of event handlers, and optionally integrate with **Odin Inspector** for editor buttons and inline property visualization.
## Key Features
- **Reactive Events** – Implements `IEvent` interfaces for 0–4 parameters.
- **Subscription Management** – Allows subscribing and unsubscribing handlers.
- **Invocation** – Provides `Invoke` methods to trigger all subscribers.
- **Disposal** – Implements `IDisposable` to clear all subscriptions.
- **Odin Inspector Support** – Optional attributes `[InlineProperty]` and `[Button]` for editor integration.
---
## BaseEvent
Represents a **parameterless event**.
```csharp
public class BaseEvent : IEvent, IDisposable
{
    public event Action OnEvent;
    public Subscription Subscribe(Action action);
    public void Unsubscribe(Action action);
    public void Invoke();
    public void Dispose();
}
```
### Members

- **Subscribe(Action action)** – Subscribes a handler and returns a `Subscription`.
- **Unsubscribe(Action action)** – Removes a previously subscribed handler.
- **Invoke()** – Triggers the event, calling all subscribed handlers.
- **Dispose()** – Clears all subscriptions.
---
## BaseEvent&lt;T&gt;
Represents an **event with one argument**.

```csharp
public class BaseEvent<T> : IEvent<T>, IDisposable
{
    public event Action<T> OnEvent;
    public Subscription<T> Subscribe(Action<T> action);
    public void Unsubscribe(Action<T> action);
    public void Invoke(T arg);
    public void Dispose();
}
```
---
## BaseEvent<T1, T2>
Represents an **event with two arguments**.
```csharp
public class BaseEvent<T1, T2> : IEvent<T1, T2>, IDisposable
{
    public event Action<T1, T2> OnEvent;
    public Subscription<T1, T2> Subscribe(Action<T1, T2> action);
    public void Unsubscribe(Action<T1, T2> action);
    public void Invoke(T1 arg1, T2 arg2);
    public void Dispose();
}
```
---
## BaseEvent<T1, T2, T3>
Represents an **event with three arguments**.
```csharp
public class BaseEvent<T1, T2, T3> : IEvent<T1, T2, T3>, IDisposable
{
    public event Action<T1, T2, T3> OnEvent;
    public Subscription<T1, T2, T3> Subscribe(Action<T1, T2, T3> action);
    public void Unsubscribe(Action<T1, T2, T3> action);
    public void Invoke(T1 arg1, T2 arg2, T3 arg3);
    public void Dispose();
}
```
---
## BaseEvent<T1, T2, T3, T4>
Represents an **event with four arguments**.
```csharp
public class BaseEvent<T1, T2, T3, T4> : IEvent<T1, T2, T3, T4>, IDisposable
{
    public event Action<T1, T2, T3, T4> OnEvent;
    public Subscription<T1, T2, T3, T4> Subscribe(Action<T1, T2, T3, T4> action);
    public void Unsubscribe(Action<T1, T2, T3, T4> action);
    public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    public void Dispose();
}
```
## Notes
- **IDisposable** – Disposing a BaseEvent clears all subscribers.
- **Subscription Objects** – Returned from Subscribe, useful for automatic unsubscription.
- **Thread Safety** – Invocation is not inherently thread-safe; synchronize externally if needed.
- **Odin Inspector** – [InlineProperty] and [Button] attributes enhance editor interaction when enabled.
### Example of Usage
Take damage animation
```csharp
//Create a solider entity
var soldier = new Entity("Soldier");
soldier.AddValue<IEvent<int>>("TakeDamageEvent", new BaseEvent<int>());
soldier.AddValue<Animator>("Animator", _animator);
```
```csharp
//Create a take damage visualization
class TakeDamagePresenter : IDisposable
{
    private readonly IEvent<int> _takeDamageEvent;
    private readonly Animator _animator;

    public TakeDamagePresenter(Entity entity)
    {
        _animator = entity.GetValue<Animator>("Animator");
        _takeDamageEvent = entity.GetValue<IEvent<int>>("TakeDamageEvent");
        _takeDamageEvent.Subscribe(this.OnTakeDamage);
    }

    public void Dispose() =>
        _takeDamageEvent.Unsubscribe(this.OnTakeDamage);

    private void OnTakeDamage(int damage) =>
        _animator.SetTrigger("TakeDamage");
}
```