# ISignal Interfaces

The **ISignal** interfaces define a family of contracts for **reactive sources** that notify subscribers of events or data changes.  

### Notes
- **Subscription Management** – Each `Subscribe` method returns a `Subscription ` `struct` for later unsubscription.
- **Reactive Design** – Supports 0–4 parameters for maximum flexibility in reactive systems.
---

## ISignal

Represents a **signal without data**.

```csharp
public interface ISignal
{
    Subscription Subscribe(Action action);
    void Unsubscribe(Action action);
}
```
### Members
- **Subscribe(Action action)** – Subscribes an action to be invoked when the signal is triggered.
- **Unsubscribe(Action action)** – Removes a previously registered action.
---
## ISignal&lt;T&gt;
Represents a **reactive source with one value**.
```csharp
public interface ISignal<T>
{
    Subscription<T> Subscribe(Action<T> action);
    void Unsubscribe(Action<T> action);
}
```
### Type Parameters
- **T** – The type of the value emitted to subscribers.
### Members
- **Subscribe(Action<T> action)** – Subscribes an action to be invoked with the emitted value.
- **Unsubscribe(Action<T> action)** – Unsubscribes a previously registered action.
---
## ISignal<T1, T2>
Represents a **reactive source with two values**.
```csharp
public interface ISignal<T1, T2>
{
    Subscription<T1, T2> Subscribe(Action<T1, T2> action);
    void Unsubscribe(Action<T1, T2> action);
}
```
### Type Parameters
- **T1** – Type of the first emitted value.
- **T2** – Type of the second emitted value.
---
## ISignal<T1, T2, T3>
Represents a **reactive source with three values**.
```csharp
public interface ISignal<T1, T2, T3>
{
    Subscription<T1, T2, T3> Subscribe(Action<T1, T2, T3> action);
    void Unsubscribe(Action<T1, T2, T3> action);
}
```
### Type Parameters
- **T1** – Type of the first emitted value.
- **T2** – Type of the second emitted value.
- **T3** – Type of the third emitted value.
---
## ISignal<T1, T2, T3, T4>
Represents a **reactive source with four values**.
```csharp
public interface ISignal<T1, T2, T3, T4>
{
    Subscription<T1, T2, T3, T4> Subscribe(Action<T1, T2, T3, T4> action);
    void Unsubscribe(Action<T1, T2, T3, T4> action);
}
```
### Type Parameters
- **T1** – Type of the first emitted value.
- **T2** – Type of the second emitted value.
- **T3** – Type of the third emitted value.
- **T4** – Type of the fourth emitted value.
---
