# 🧩 InlinePredicates

Provides wrappers around standard `System.Func` delegates that return a boolean value. It extends
from [InlineFunction](InlineFunctions.md) and implements the corresponding [IPredicate](IPredicates.md) interfaces and
allow invoking predicates directly, optionally with parameters.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Predicate without arguments](#ex-1)
    - [Predicate with one argument](#ex-2)
    - [Predicate with two arguments](#ex-3)
- [API Reference](#-api-reference)

---

## 🗂 Examples of Usage

<div id="ex-1"></div>

### 1️⃣ Predicate without arguments

```csharp
GameObject gameObject = ...
IPredicate predicate = new InlinePredicate(
    () => gameObject.activeSelf
);

bool activeSelf = predicate.Invoke();
```

<div id="ex-2"></div>

### 2️⃣ Predicate with one argument

```csharp
Character player = ...
IPredicate<Character> predicate = new InlinePredicate<Character>(
    other => player.Team != other.Team
);

bool isEnemy = predicate.Invoke(enemy);
```

<div id="ex-3"></div>

### 3️⃣ Predicate with two arguments

```csharp
Character player, enemy = ...
IPredicate<Character, Character> predicate = new InlinePredicate<Character, Character>(
    (a, b) => a.Team != b.Team
);

bool areEnemies = predicate.Invoke(player, enemy);
```

---

## 🔍 API Reference

There are several implementations of inline predicates, depending on the number of arguments they take:

- [InlinePredicate](InlinePredicate.md) — Predicate without parameters.
- [InlinePredicate&lt;T&gt;](InlinePredicate%601.md) — Predicate that takes one argument.
- [InlinePredicate&lt;T1, T2&gt;](InlinePredicate%602.md) — Predicate that takes two arguments.