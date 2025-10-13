# 🧩 InlineFunctions

The **InlineFunction** classes provide wrappers around standard `System.Func` delegates. They implement the
corresponding [IFunction](IFunctions.md) interfaces and allow invoking functions
directly, optionally with parameters.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Function without arguments](#ex-1)
    - [Function with one argument](#ex-2)
    - [Function with two arguments](#ex-3)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

<div id="ex-1"></div>

### 1️⃣ Function without arguments

```csharp
GameObject gameObject = ...
IFunction<bool> function = new InlineFunction<bool>(
    () => gameObject.activeSelf
);

bool activeSelf = function.Invoke();
```

<div id="ex-2"></div>

### 2️⃣ Function with one argument

```csharp
Character player, enemy = ...
IFunction<bool> function = new InlineFunction<Character, bool>(
    other => player.Team != other.Team
);

bool isEnemies = function.Invoke(enemy);
```

<div id="ex-3"></div>

### 3️⃣ Function with two arguments

```csharp
IFunction<int, int, int> function = new InlineFunction<int, int, int>(
    (a, b) => a + b
);

int sum = function.Invoke(3, 4); // 7
```

---

## 🔍 API Reference

There are several implementations of inline functions, depending on the number of arguments they take:

- [InlineFunction&lt;R&gt;](InlineFunction.md) — Function without parameters.
- [InlineFunction&lt;T, R&gt;](InlineFunction%601.md) — Function that takes one argument.
- [InlineFunction&lt;T1, T2, R&gt;](InlineFunction%602.md) — Function that takes two arguments.

---

## 📌 Best Practices

- [Using InlineFunctions with Entities](../../BestPractices/UsingInlineFunctions.md)