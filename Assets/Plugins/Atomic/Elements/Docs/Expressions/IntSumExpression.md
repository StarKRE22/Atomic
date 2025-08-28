# 🧩 IntSumExpression Classes

`IntSumExpression` represents an expression that computes the **sum** of multiple integer-returning functions. It is generic and supports functions with 0, 1, or 2 parameters.

---

## `IntSumExpression`

Parameterless integer functions.

### Constructors

```csharp
IntSumExpression()
IntSumExpression(IEnumerable<Func<int>> members)
IntSumExpression(params Func<int>[] members)
```
### Methods
```csharp
protected override int Invoke(Enumerator enumerator)
```
- Computes the sum of all function results.
- Returns `0` if no functions are present.
---
## `IntSumExpression<T>`
Single-parameter integer functions.
### Constructors
```csharp
IntSumExpression()
IntSumExpression(IEnumerable<Func<T, int>> members)
IntSumExpression(params Func<T, int>[] members)
```
### Methods
```csharp
protected override int Invoke(Enumerator enumerator, T arg)
```
- Computes the sum of all function results using `arg` as input.
- Returns `0` if no functions are present.
