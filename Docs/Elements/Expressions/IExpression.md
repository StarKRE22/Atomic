# 🧩 Expression Interfaces
These interfaces represent **expressions composed of function members** that can be dynamically added, removed, and evaluated. They support parameterless functions as well as functions with one or more parameters.

> **Note:** These interfaces allow **dynamic composition of multiple factors or conditions** at runtime.  
> For example, you can add multipliers for speed, apply effects when an object is frozen, or modify a value based on boosts.  
> Expressions act as dynamic functions that evaluate all registered members, making them ideal for flexible, runtime-adjustable calculations.

---


## IExpression&lt;R&gt;
A **parameterless expression** that returns a value of type `R`.

```csharp
public interface IExpression<R> : IList<Func<R>>, IValue<R>
{
}
```
**Implements** `IList<Func<R>>` – allows indexed access and enumeration of the function members.
**Implements** `IValue<R>` – provides the evaluated result of the expression via a `Value` property.
---
## IExpression<T, R>
An **expression with a single input parameter** of type `T` and return type `R`.
```csharp
public interface IExpression<T, R> : IList<Func<T, R>>, IFunction<T, R>
{
}
```
- **Implements** `IList<Func<T, R>>` – allows indexed access and enumeration of the function members.
- **Implements** `IFunction<T, R>` – allows evaluation of the expression with one argument of type `T`.
---
## IExpression<T1, T2, R>
An **expression with two input parameters** (`T1`, `T2`) and return type `R`.
```csharp
public interface IExpression<T1, T2, R> : IList<Func<T1, T2, R>>, IFunction<T1, T2, R>
{
}
```
- **Implements** `IList<Func<T1, T2, R>>` – allows indexed access and enumeration of the function members.
- **Implements** `IFunction<T1, T2, R>` – allows evaluation of the expression with two arguments of types `T1` and `T2`.
---
## Practical Use Cases
Expressions are particularly useful for dynamic runtime calculations, such as:

- Applying **speed multipliers** from various sources (buffs, debuffs, environmental effects).
- Adding or removing conditions like **frozen state**, **boosts**, or other temporary effects.
- Combining multiple **dynamic factors** to calculate a final value on the fly.

This makes `IExpression` a flexible, runtime-adjustable function container suitable for game logic or any system requiring composable dynamic calculations.