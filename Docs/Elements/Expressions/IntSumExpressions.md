# 🧩 IntSumExpressions

Represent expressions those compute the **sum** of multiple integer-returning functions. These classes extend from
the [ExpressionBase](ExpressionsBase.md) family.

There are several implementations of expressions, depending on the number of arguments the actions take:

- [IntSumExpression](IntSumExpression.md) — Non-generic version; works without parameters.
- [IntSumExpression&lt;T&gt;](IntSumExpression%601.md) — Expression that takes one argument.
- [IntSumExpression&lt;T1, T2&gt;](IntSumExpression%602.md) — Expression that takes two arguments.

---

## 🗂 Examples of Usage

#### Expression without parameters

```csharp
var expression = new IntSumExpression(
    () => 2,
    () => 3,
    () => 4
);
int result = expression.Invoke(); // 9
```

---

#### Expression with one parameter

```csharp
var expression = new IntSumExpression<int>(
    x => x,
    x => x + 1
);
int result = expression.Invoke(3); // 3 + (3 + 1) = 7
```

---

#### Expression with two parameters

```csharp
var expression = new IntSumExpression<int, int>(
    (a, b) => a,
    (a, b) => b,
    (a, b) => a + b
);
int result = expression.Invoke(2, 3); // 2 + 3 + (2 + 3) = 10
```

