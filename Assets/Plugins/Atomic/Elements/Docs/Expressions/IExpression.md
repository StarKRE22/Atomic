# 🧩 Expression Interfaces
These interfaces represent **expressions composed of function members** that can be dynamically added, removed, and evaluated.  
They support parameterless functions as well as functions with one or more parameters.
> **Note:** These interfaces are useful because they allow **dynamic composition of multiple factors or conditions** at runtime.  
> For example, you can add multipliers for speed, apply effects when an object is frozen, or modify a value based on boosts.  
> Expressions act as dynamic functions that evaluate all registered members, making them ideal for flexible, runtime-adjustable calculations.
---
## IExpression&lt;T&gt;
A **parameterless expression** that returns a value of type `T`.
```csharp
public interface IExpression<T> : IValue<T>
{
    int Count { get; }

    void Add(Func<T> member);
    void Remove(Func<T> member);
    bool Contains(Func<T> member);
    void Clear();
}
```
- **Count** – The number of function members in the expression.
- **Add(Func<T> member)** – Adds a new function member.
- **Remove(Func<T> member)** – Removes an existing function member.
- **Contains(Func<T> member)** – Checks if a function member exists.
- **Clear()** – Removes all function members.
---
## IExpression<T, R>
An **expression with a single** input parameter of type `T` and return type `R`.
```csharp
public interface IExpression<T, R> : IFunction<T, R>
{
    int Count { get; }

    void Add(Func<T, R> member);
    void Remove(Func<T, R> member);
    bool Contains(Func<T, R> member);
    void Clear();
}
```
- **Count** – Number of function members.
- **Add(Func<T, R> member)** – Adds a function taking T and returning R.
- **Remove(Func<T, R> member)** – Removes a function.
- **Contains(Func<T, R> member)** – Checks if a function exists.
- **Clear()** – Removes all function members.
---
## IExpression<T1, T2, R>
An **expression with two input parameters** (`T1`, `T2`) and return type `R`.
```csharp
public interface IExpression<T1, T2, R> : IFunction<T1, T2, R>
{
    int Count { get; }

    void Add(Func<T1, T2, R> member);
    void Remove(Func<T1, T2, R> member);
    bool Contains(Func<T1, T2, R> member);
    void Clear();
}
```
- **Count** – Number of function members.
- **Add(Func<T1, T2, R> member)** – Adds a function taking T1, T2 and returning R.
- **Remove(Func<T1, T2, R> member)** – Removes a function.
- **Contains(Func<T1, T2, R> member)** – Checks if a function exists.
- **Clear()** – Removes all function members.
---
## Practical Use Case
Expressions are particularly useful for dynamic game calculations, such as:
- Applying **speed multipliers** from various sources (buffs, debuffs, environmental effects).
- Adding or removing conditions like **frozen state**, **boosts**, or other temporary effects.
- Combining multiple **dynamic factors** to calculate a final value on the fly.

This makes `IExpression` a flexible, runtime-adjustable function container suitable for game logic or any system requiring composable dynamic calculations.