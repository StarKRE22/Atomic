# 🧩 FloatSumExpression Classes
`FloatSumExpression` represents an expression that computes the **sum** of multiple float-returning functions. It is generic and supports functions with 0, 1, or 2 parameters.
---
## `FloatSumExpression`
Parameterless float functions.
### Constructors
```csharp
FloatSumExpression()
FloatSumExpression(IEnumerable<Func<float>> members)
FloatSumExpression(params Func<float>[] members)
```
### Methods
```csharp
protected override float Invoke(Enumerator enumerator)
```
- Computes the sum of all function results.
- Returns `0` if no functions are present.
---
## `FloatSumExpression<T>`
Single-parameter float functions.
### Constructors
```csharp
FloatSumExpression()
FloatSumExpression(IEnumerable<Func<T, float>> members)
FloatSumExpression(params Func<T, float>[] members)
```
### Methods
```csharp
protected override float Invoke(Enumerator enumerator, T arg)
```
- Computes the sum of all function results using `arg` as input.
- Returns `0` if no functions are present.
---
## `FloatSumExpression<T1, T2>`
Two-parameter float functions.
### Constructors
```csharp
FloatSumExpression()
FloatSumExpression(IEnumerable<Func<T1, T2, float>> members)
FloatSumExpression(params Func<T1, T2, float>[] members)
```
### Methods
```csharp
protected override float Invoke(Enumerator enumerator, T1 arg1, T2 arg2)
```
- Computes the sum of all function results using `arg1` and `arg2` as inputs.
- Returns `0` if no functions are present.
---
## Example Usage
```csharp
// Parameterless
var sum = new FloatSumExpression(
    () => 2,
    () => 3,
    () => 4
);
float result = sum.Invoke(); // 2 + 3 + 4 = 9


// Single-parameter
var sumWithFactor = new FloatSumExpression<float>(
    x => x,
    x => x + 1
);
float singleResult = sumWithFactor.Invoke(3); // 3 + 4 = 7

// Two-parameter
var sumTwoParams = new FloatSumExpression<float, float>(
    (a, b) => a,
    (a, b) => b,
    (a, b) => a + b
);
float twoParamResult = sumTwoParams.Invoke(2, 3); // 2 + 3 + 5 = 10
```