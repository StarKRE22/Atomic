# 🧩 IntMulExpression Classes

`IntMulExpression` represents an expression that computes the **product** of multiple integer-returning functions. It is generic and supports functions with 0, 1, or 2 parameters.

---

## `IntMulExpression`

Parameterless integer functions.

### Constructors

```csharp
IntMulExpression()
IntMulExpression(IEnumerable<Func<int>> members)
IntMulExpression(params Func<int>[] members)
```
### Methods
```csharp
protected override int Invoke(Enumerator enumerator)
```
- Computes the product of all function results.
- Returns `1` if no functions are present.
---

## `IntMulExpression<T>`
Single-parameter integer functions.
### Constructors
```csharp
IntMulExpression()
IntMulExpression(IEnumerable<Func<T, int>> members)
IntMulExpression(params Func<T, int>[] members)
```
### Methods
```csharp
protected override int Invoke(Enumerator enumerator, T arg)
```
- Computes the product of all function results using `arg` as input.
- Returns `1` if no functions are present.
---

## `IntMulExpression<T1, T2>`
Two-parameter integer functions.
### Constructors
```csharp
IntMulExpression()
IntMulExpression(IEnumerable<Func<T1, T2, int>> members)
IntMulExpression(params Func<T1, T2, int>[] members)
```
### Methods
```csharp
protected override int Invoke(Enumerator enumerator, T1 arg1, T2 arg2)
```
- Computes the product of all function results using `arg1` and `arg2` as inputs.
- Returns `1` if no functions are present.
---
## Example Usage
```csharp
// Parameterless
var multiply = new IntMulExpression(
    () => 2,
    () => 3,
    () => 4
);
int result = multiply.Invoke(); // 24
```
```csharp

// Single-parameter
var multiplyByFactor = new IntMulExpression<int>(
    x => x,
    x => x + 1
);
int singleResult = multiplyByFactor.Invoke(3); // 3 * 4 = 12
```
```csharp

// Two-parameter
var multiplyTwoParams = new IntMulExpression<int, int>(
    (a, b) => a,
    (a, b) => b,
    (a, b) => a + b
);
int twoParamResult = multiplyTwoParams.Invoke(2, 3); // 2 * 3 * 5 = 30
```
