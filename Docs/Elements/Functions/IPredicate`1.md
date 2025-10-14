# 🧩 IPredicate&lt;T&gt;

Represents a predicate with <b>one input argument</b> that returns a boolean result.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Invoke(T)](#invoket)

---

## 🗂 Example of Usage

```csharp
public class IsEnemyPredicate : IPredicate<Character>
{
    private readonly Character _source;

    public IsEnemyPredicate(Character source) => _source = source;
    
    public bool Invoke(Character other) => _source.Team != other.Team;
}
```

Usage:

```csharp
Character player, enemy = ...;
IPredicate<Character> predicate = new IsEnemyPredicate(player);
bool isEnemy = predicate.Invoke(enemy);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IPredicate<in T> : IFunction<T, bool>
```

- **Description:**  Represents a predicate with <b>one input argument</b> that returns a boolean result.
- **Inheritance:** [IFunction&lt;T, R&gt;](IFunction%601.md)
- **Type parameter:** `T` — the input argument type.

---

### 🏹 Methods

#### `Invoke(T)`

```csharp
public bool Invoke(T arg);
```

- **Description:** Evaluates the predicate with the specified argument.
- **Parameter:** `arg` — the input argument.
- **Returns:** `true` or `false` based on the predicate logic.