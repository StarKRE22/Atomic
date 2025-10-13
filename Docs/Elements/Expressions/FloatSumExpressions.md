# 🧩 FloatSumExpressions

Represents an expression that computes the **sum** of multiple float-returning functions. These classes extend from
the [ExpressionBase](ExpressionsBase.md) family.

> [!NOTE]
> If the collection is empty, the expression evaluates to `0` by default.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
    - [Expression without args](#ex1)
    - [Expression with single arg](#ex2)
    - [Expression with two args](#ex3)
- [API Reference](#-api-reference)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Expression without args

```csharp
var expression = new FloatSumExpression(
    () => 2.0f,
    () => 3.0f,
    () => 4.0f
);
float result = expression.Invoke(); // 2.0f + 3.0f + 4.0f = 9
```

<div id="ex2"></div>

### 2️⃣ Expression with single arg

```csharp
var expression = new FloatSumExpression<float>(
    x => x,
    x => x + 0.5f
);
float result = expression.Invoke(3.5f); // 3.5f + (3.5f + 0.5f) = 7.5f
```

<div id="ex3"></div>

### 3️⃣ Expression with two args

```csharp
var expression = new FloatSumExpression<float, float>(
    (a, b) => a,
    (a, b) => b,
    (a, b) => a + b
);
float result = expression.Invoke(2, 3); // 2 + 3 + (2 + 3) = 10
```

---

## 🔍 API Reference

There are several implementations of expressions, depending on the number of arguments the expressions take:

- [FloatSumExpression](FloatSumExpression.md) — Non-generic version; works without parameters.
- [FloatSumExpression&lt;T&gt;](FloatSumExpression%601.md) — Expression that takes one argument.
- [FloatSumExpression&lt;T1, T2&gt;](FloatSumExpression%602.md) — Expression that takes two arguments.