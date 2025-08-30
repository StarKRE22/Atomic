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
---
## `IntSumExpression<T1, T2>`
Two-parameter integer functions.

### Constructors
```csharp
IntSumExpression()
IntSumExpression(IEnumerable<Func<T1, T2, int>> members)
IntSumExpression(params Func<T1, T2, int>[] members)
```
### Methods
```csharp
protected override int Invoke(Enumerator enumerator, T1 arg1, T2 arg2)
```
- Computes the sum of all function results using `arg1` and `arg2` as inputs.
- Returns `0` if no functions are present.
---
## Example Usage
```csharp
// Parameterless
var sum = new IntSumExpression(
    () => 2,
    () => 3,
    () => 4
);
int result = sum.Invoke(); // 2 + 3 + 4 = 9


// Single-parameter
var sumWithFactor = new IntSumExpression<int>(
    x => x,
    x => x + 1
);
int singleResult = sumWithFactor.Invoke(3); // 3 + 4 = 7

// Two-parameter
var sumTwoParams = new IntSumExpression<int, int>(
    (a, b) => a,
    (a, b) => b,
    (a, b) => a + b
);
int twoParamResult = sumTwoParams.Invoke(2, 3); // 2 + 3 + 5 = 10
```