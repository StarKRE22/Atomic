# 🧩 IntSumExpressions

Represent expressions those compute the **sum** of multiple integer-returning functions. These classes extend from
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

### 1️⃣ Expression without parameters

```csharp
var expression = new IntSumExpression(
    () => 2,
    () => 3,
    () => 4
);
int result = expression.Invoke(); // 9
```

<div id="ex2"></div>

### 2️⃣ Expression with one parameter

```csharp
var expression = new IntSumExpression<int>(
    x => x,
    x => x + 1
);
int result = expression.Invoke(3); // 3 + (3 + 1) = 7
```

<div id="ex3"></div>

### 3️⃣ Expression with two parameters

```csharp
var expression = new IntSumExpression<int, int>(
    (a, b) => a,
    (a, b) => b,
    (a, b) => a + b
);
int result = expression.Invoke(2, 3); // 2 + 3 + (2 + 3) = 10
```

---

## 🔍 API Reference

There are several implementations of expressions, depending on the number of arguments the expression take:

- [IntSumExpression](IntSumExpression.md) — Non-generic version; works without parameters.
- [IntSumExpression&lt;T&gt;](IntSumExpression%601.md) — Expression that takes one argument.
- [IntSumExpression&lt;T1, T2&gt;](IntSumExpression%602.md) — Expression that takes two arguments.