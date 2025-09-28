# 🧩 IntMulExpressions

Represents an expression that computes the **product** of multiple integer-returning functions. They extend from
the [ExpressionBase](ExpressionsBase.md) family of classes.

There are several implementations of expressions, depending on the number of arguments the actions take:

- [IntMulExpression](IntMulExpression.md) — Non-generic version; works without parameters.
- [IntMulExpression&lt;T&gt;](IntMulExpression%601.md) — Expression that takes one argument.
- [IntMulExpression&lt;T1, T2&gt;](IntMulExpression%602.md) — Expression that takes two arguments.

---

## 🗂 Examples of Usage

#### Expression without args

```csharp
// Parameterless
var multiply = new IntMulExpression(
    () => 2,
    () => 3,
    () => 4
);
int result = multiply.Invoke(); // 2 * 3 * 4 = 24
```

---

#### Expression with one arg

```csharp

// Single-parameter
var expression = new IntMulExpression<int>(
    x => x,
    x => x + 1
);
int result = expression.Invoke(3); // 3 * (3 + 1) = 12
```

---

#### Expression with two args

```csharp
var expression = new IntMulExpression<int, int>(
    (a, b) => a,
    (a, b) => b,
    (a, b) => a + b
);
int result = expression.Invoke(2, 3); // 2 * 3 * (2 + 3) = 30
```