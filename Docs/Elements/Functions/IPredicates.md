# 🧩 IPredicates

The **IPredicate** interfaces are specialized forms of [functions](IFunctions.md) that evaluate a condition and return
`true` or `false`, useful in filtering, validation, and decision-making logic.

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
public class IsGameActivePredicate : IPredicate
{
    private readonly GameManager _manager;

    public IsGameActivePredicate(GameManager manager) => _manager = manager;
    
    public bool Invoke() => _manager.IsActive;
}
```

<div id="ex-2"></div>

### 2️⃣ Predicate with one argument

```csharp
public class IsEnemyPredicate : IPredicate<Character>
{
    private readonly Character _source;

    public IsEnemyPredicate(Character source) => _source = source;
    
    public bool Invoke(Character other) => _source.Team != other.Team;
}
```

<div id="ex-3"></div>

### 3️⃣ Predicate with two arguments

```csharp
public class AreAlliesPredicate : IPredicate<Character, Character>
{
    public bool Invoke(Character a, Character b) => a.Team == b.Team;
}
```

---

## 🔍 API Reference

There are several interfaces of predicates, depending on the number of arguments they take:

- [IPredicate&lt;R&gt;](IPredicate.md) — Predicate without parameters.
- [IPredicate&lt;T, R&gt;](IPredicate%601.md) — Predicate that takes one argument.
- [IPredicate&lt;T1, T2, R&gt;](IPredicate%602.md) — Predicate that takes two arguments.