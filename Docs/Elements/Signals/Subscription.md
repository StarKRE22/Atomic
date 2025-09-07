# 🧩 Subscription Structs

The **Subscription** structs represent a subscription to a reactive `ISignal` source.  
Disposing a `Subscription` automatically unsubscribes the associated action.

### Notes
- **IDisposable** – Disposing the subscription removes the handler from the signal automatically.
- **Immutable** – All fields are `readonly`.
- **Safety** – Dispose is null-safe; if the signal is `null`, no action is taken.
- **Stack Allocation** – `Subscription` is a struct, so it lives on the stack and does **not** allocate memory on the heap unless it is explicitly converted to `IDisposable` (boxing occurs on conversion).

---

## Subscription

A **parameterless subscription**.

```csharp
public readonly struct Subscription : IDisposable
{
    public Subscription(ISignal signal, Action action);
    public void Dispose();
}
```
- **Constructor** – `Subscription(ISignal signal, Action action)` Creates a subscription for a parameterless signal.
- **Dispose()** – Unsubscribes the associated action.
---
## Subscription&lt;T&gt;
A **subscription for a single-value signal**.
```csharp
public readonly struct Subscription<T> : IDisposable
{
    public Subscription(ISignal<T> signal, Action<T> action);
    public void Dispose();
}
```
- **T** – Type of the emitted value.
- **Dispose()** – Unsubscribes the action from the signal.
---
## Subscription<T1, T2>
A **subscription for a two-value signal**.
```csharp
public readonly struct Subscription<T1, T2> : IDisposable
{
    public Subscription(ISignal<T1, T2> signal, Action<T1, T2> action);
    public void Dispose();
}
```
- **T1**, **T2** – Types of the emitted values.
---
## Subscription<T1, T2, T3>
A **subscription for a three-value signal**.
```csharp
public readonly struct Subscription<T1, T2, T3> : IDisposable
{
    public Subscription(ISignal<T1, T2, T3> signal, Action<T1, T2, T3> action);
    public void Dispose();
}
```
- **T1**, **T2**, **T3** – Types of the emitted values.
---
## Subscription<T1, T2, T3, T4>
A **subscription for a four-value signal**.
```csharp
public readonly struct Subscription<T1, T2, T3, T4> : IDisposable
{
    public Subscription(ISignal<T1, T2, T3, T4> signal, Action<T1, T2, T3, T4> action);
    public void Dispose();
}
```
- **T1**, **T2**, **T3**, **T4** – Types of the emitted values.
