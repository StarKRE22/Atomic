# 🧩 ExpressionBase Classes

These classes provide **base implementations for dynamic expressions** that aggregate multiple functions. They allow flexible runtime evaluation of function members with zero, one, or two input parameters.

> **Note:** These classes inherit from `LinkedList<Func<...>>` and implement `IExpression` interfaces.  
> They are ideal for scenarios where multiple factors or rules must be evaluated dynamically, such as game calculations, modifiers, or conditions.

---

## `ExpressionBase<R>`

A base class for **parameterless expressions** returning a value of type `R`.

```csharp
public abstract class ExpressionBase<R> : LinkedList<Func<R>>, IExpression<R>
```
## Properties
- `R Value` – Evaluates all registered functions and returns the aggregated result.
## Methods
- `R Invoke()` – Invokes all function members and returns the result.
- `protected abstract R Invoke(Enumerator enumerator)` – Template method for derived classes to define how function results are aggregated.
## Constructors
- `ExpressionBase(int capacity = INITIAL_CAPACITY)` – Initializes an empty expression with the specified capacity.
- `ExpressionBase(params Func<R>[] members)` – Initializes with an array of function members.
- `ExpressionBase(IEnumerable<Func<R>> members)` – Initializes with an enumerable of function members.
---

## `ExpressionBase<T, R>`
A base class for **expressions with one input parameter** of type `T` returning a value of type `R`.
```csharp
public abstract class ExpressionBase<T, R> : LinkedList<Func<T, R>>, IExpression<T, R>
```
## Methods
- `R Invoke(T arg)` – Invokes all function members with the provided argument and returns the aggregated result.
- `protected abstract R Invoke(Enumerator enumerator, T arg)` – Template method for derived classes to define aggregation logic.
## Constructors
- `ExpressionBase(int capacity = INITIAL_CAPACITY)` – Initializes an empty expression.
- `ExpressionBase(params Func<T, R>[] members)` – Initializes with an array of function members.
- `ExpressionBase(IEnumerable<Func<T, R>> members)` – Initializes with an enumerable of function
---
## `ExpressionBase<T1, T2, R>`
A base class for **expressions with two input parameters** (`T1`, `T2`) returning a value of type R.
```csharp
public abstract class ExpressionBase<T1, T2, R> : LinkedList<Func<T1, T2, R>>, IExpression<T1, T2, R>
```
## Methods
- `R Invoke(T1 arg1, T2 arg2)` – Invokes all function members with the provided arguments and returns the aggregated result.
- `protected abstract R Invoke(Enumerator enumerator, T1 arg1, T2 arg2)` – Template method for derived classes to define aggregation logic.
## Constructors
- `ExpressionBase(int capacity = INITIAL_CAPACITY)` – Initializes an empty expression.
- `ExpressionBase(params Func<T1, T2, R>[] members)` – Initializes with an array of function members.
- `ExpressionBase(IEnumerable<Func<T1, T2, R>> members)` – Initializes with an enumerable of function members.
---
## Practical Use Cases
- **Dynamic calculations** in games (speed, health, damage modifiers).
- **Composable rules** for AI decisions or conditions.
- **Runtime-adjustable expressions** where factors can be added, removed, or replaced dynamically.