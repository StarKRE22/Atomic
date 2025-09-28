# 🧩 FloatSumExpressions

Represents an expression that computes the **sum** of multiple float-returning functions. These classes extend from
the [ExpressionBase](ExpressionsBase.md) family.

> [NOTE]
> If the collection is empty, the expression evaluates to `0` by default.

There are several implementations of expressions, depending on the number of arguments the actions take:

- [FloatSumExpression](FloatSumExpression.md) — Non-generic version; works without parameters.
- [FloatSumExpression&lt;T&gt;](FloatSumExpression%601.md) — Expression that takes one argument.
- [FloatSumExpression&lt;T1, T2&gt;](FloatSumExpression%602.md) — Expression that takes two arguments.

---

## 🗂 Examples of Usage

#### Expression without args

```csharp
var expression = new FloatSumExpression(
    () => 2.0f,
    () => 3.0f,
    () => 4.0f
);
float result = expression.Invoke(); // 2.0f + 3.0f + 4.0f = 9
```

#### Expression with single arg

```csharp
var expression = new FloatSumExpression<float>(
    x => x,
    x => x + 0.5f
);
float result = expression.Invoke(3.5f); // 3.5f + (3.5f + 0.5f) = 7.5f
```

#### Expression with two args

```csharp
var expression = new FloatSumExpression<float, float>(
    (a, b) => a,
    (a, b) => b,
    (a, b) => a + b
);
float result = expression.Invoke(2, 3); // 2 + 3 + (2 + 3) = 10
```