# 🧩 InlineSignal Classes

The **InlineSignal** classes provide **base implementations of reactive sources** that notify subscribers when values are emitted.  
They support 0–4 parameters and implement the corresponding `ISignal` interfaces.

### Notes

- **Delegate-based** – All `InlineSignal` classes accept `subscribe` and `unsubscribe` delegates for flexible subscription management.
- **Subscription Structs** – Each `Subscribe` method returns a `Subscription` struct to allow easy unsubscription.
- **Serializable** – All classes are `[Serializable]`, suitable for Unity and other serialization systems.
- **Null Safety** – Constructors validate delegate arguments, throwing `ArgumentNullException` if `null`.

---

## InlineSignal

A **non-generic reactive source**.

```csharp
public class InlineSignal : ISignal
{
    Subscription Subscribe(Action action);
    void Unsubscribe(Action action);
}
```
- **Subscribe(Action action)** – Subscribes an action to be invoked when the source triggers.
- **Unsubscribe(Action action)** – Unsubscribes a previously registered action.
---

## InlineSignal&lt;T&gt;
A **generic reactive source with one value**.
```csharp
public class InlineSignal<T> : ISignal<T>
{
    Subscription<T> Subscribe(Action<T> action);
    void Unsubscribe(Action<T> action);
}
```
- **T** – Type of the emitted value.
- **Subscribe(Action<T> action)** – Subscribes to receive emitted values.
- **Unsubscribe(Action<T> action)** – Unsubscribes a previously registered action.
---
## InlineSignal<T1, T2>
A **reactive source with two values**.
```csharp
public class InlineSignal<T1, T2> : ISignal<T1, T2>
{
    Subscription<T1, T2> Subscribe(Action<T1, T2> action);
    void Unsubscribe(Action<T1, T2> action);
}
```
- **T1**, **T2** – Types of the emitted values.
---
## InlineSignal<T1, T2, T3>
A **reactive source with three values**.
```csharp
public class InlineSignal<T1, T2, T3> : ISignal<T1, T2, T3>
{
    Subscription<T1, T2, T3> Subscribe(Action<T1, T2, T3> action);
    void Unsubscribe(Action<T1, T2, T3> action);
}
```
- **T1**, **T2**, **T3** – Types of the emitted values.
---
## InlineSignal<T1, T2, T3, T4>
A **reactive source with four values**.
```csharp
public class InlineSignal<T1, T2, T3, T4> : ISignal<T1, T2, T3, T4>
{
    Subscription<T1, T2, T3, T4> Subscribe(Action<T1, T2, T3, T4> action);
    void Unsubscribe(Action<T1, T2, T3, T4> action);
}
```
- **T1**, **T2**, **T3**, **T4** – Types of the emitted values.
---