# 🧩 FloatMulExpression Classes

Represents an expression that computes the **product** of multiple float-returning functions. It extends from
the [ExpressionBase](ExpressionsBase.md) family of classes.

There are several implementations of expressions, depending on the number of arguments the actions take:

- [FloatMulExpression](FloatMulExpression.md) — Non-generic version; works without parameters.
- [FloatMulExpression&lt;T&gt;](FloatMulExpression%601.md) — Expression that takes one argument.
- [FloatMulExpression&lt;T1, T2&gt;](FloatMulExpression%602.md) — Expression that takes two arguments.

---

## 🗂 Examples of Usage

#### Expression without args

```csharp
var multiply = new FloatMulExpression(
    () => 2,
    () => 3,
    () => 4
);
float result = multiply.Invoke(); // 24
```

---

#### Expression with single arg

```csharp
var expression = new FloatMulExpression<float>(
    x => x,
    x => x + 1
);
float result = expression.Invoke(3); // 3 * (3 + 1) = 12
```

---

#### Expression with two args

```csharp
var expression = new FloatMulExpression<float, float>(
    (a, b) => a,
    (a, b) => b,
    (a, b) => a + b
);
float result = expression.Invoke(2, 3); // 2 * 3 * (2 + 3) = 30
```
