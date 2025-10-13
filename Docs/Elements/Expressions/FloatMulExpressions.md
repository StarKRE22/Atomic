# 🧩 FloatMulExpressions

Represents an expression that computes the **product** of multiple float-returning functions. It extends from
the [ExpressionBase](ExpressionsBase.md) family of classes.

> [!NOTE]
> If the collection is empty, the expression evaluates to `1` by default.


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
var multiply = new FloatMulExpression(
    () => 2,
    () => 3,
    () => 4
);
float result = multiply.Invoke(); // 24
```

<div id="ex2"></div>

### 2️⃣ Expression with single arg

```csharp
var expression = new FloatMulExpression<float>(
    x => x,
    x => x + 1
);
float result = expression.Invoke(3); // 3 * (3 + 1) = 12
```

<div id="ex3"></div>

### 3️⃣ Expression with two args

```csharp
var expression = new FloatMulExpression<float, float>(
    (a, b) => a,
    (a, b) => b,
    (a, b) => a + b
);
float result = expression.Invoke(2, 3); // 2 * 3 * (2 + 3) = 30
```

---

## 🔍 API Reference

There are several implementations of expressions, depending on the number of arguments the expressions take:

- [FloatMulExpression](FloatMulExpression.md) — Non-generic version; works without parameters.
- [FloatMulExpression&lt;T&gt;](FloatMulExpression%601.md) — Expression that takes one argument.
- [FloatMulExpression&lt;T1, T2&gt;](FloatMulExpression%602.md) — Expression that takes two arguments.