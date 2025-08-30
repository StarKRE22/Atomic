# 🧩 FloatMulExpression Classes

`FloatMulExpression` represents an expression that computes the **product** of multiple float-returning functions. It is generic and supports functions with 0, 1, or 2 parameters.

---

## `FloatMulExpression`

Parameterless float functions.

### Constructors

```csharp
FloatMulExpression()
FloatMulExpression(IEnumerable<Func<float>> members)
FloatMulExpression(params Func<float>[] members)
```
### Methods
```csharp
protected override float Invoke(Enumerator enumerator)
```
- Computes the product of all function results.
- Returns `1` if no functions are present.
---

## `FloatMulExpression<T>`
Single-parameter float functions.
### Constructors
```csharp
FloatMulExpression()
FloatMulExpression(IEnumerable<Func<T, float>> members)
FloatMulExpression(params Func<T, float>[] members)
```
### Methods
```csharp
protected override float Invoke(Enumerator enumerator, T arg)
```
- Computes the product of all function results using `arg` as input.
- Returns `1` if no functions are present.
---

## `FloatMulExpression<T1, T2>`
Two-parameter float functions.
### Constructors
```csharp
FloatMulExpression()
FloatMulExpression(IEnumerable<Func<T1, T2, float>> members)
FloatMulExpression(params Func<T1, T2, float>[] members)
```
### Methods
```csharp
protected override float Invoke(Enumerator enumerator, T1 arg1, T2 arg2)
```
- Computes the product of all function results using `arg1` and `arg2` as inputs.
- Returns `1` if no functions are present.
---
## Example Usage
```csharp
// Parameterless
var multiply = new FloatMulExpression(
    () => 2.5f,
    () => 3.5f,
    () => 4.0f
);
float result = multiply.Invoke(); // 35f
```
```csharp

// Single-parameter
var multiplyByFactor = new FloatMulExpression<float>(
    x => x,
    x => x + 0.5f
);
float singleResult = multiplyByFactor.Invoke(3.0f); // 3 * 3.5f = 10.5f
```
```csharp

// Two-parameter
var multiplyTwoParams = new FloatMulExpression<float, float>(
    (a, b) => a,
    (a, b) => b,
    (a, b) => a + b
);
float twoParamResult = multiplyTwoParams.Invoke(2.0f, 3.0f); // 2f * 3f * 5f = 30.0f
```
