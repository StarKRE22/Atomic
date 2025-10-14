# 🧩 IPredicate&lt;T1, T2&gt;

Represents a predicate with <b>two input arguments</b> that returns a boolean result.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Invoke(T1, T2)](#invoket1-t2)

---

## 🗂 Example of Usage

```csharp
public class AreAlliesPredicate : IPredicate<Character, Character>
{
    public bool Invoke(Character a, Character b) => a.Team == b.Team;
}
```

```csharp
Character player, npc = ...;
IPredicate<Character, Character> predicate = new AreAlliesPredicate();
bool areAllies = predicate.Invoke(player, npc);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IPredicate<in T1, in T2> : IFunction<T1, T2, bool>
```

- **Description:** Represents a predicate with <b>two input arguments</b> that returns a boolean result.
- **Inheritance:** [IFunction&lt;T1, T2, R&gt;](IFunction%602.md)
- **Type parameters:**
    - `T1` — the first input argument type
    - `T2` — the second input argument type

---

### 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
public bool Invoke(T1 arg1, T2 arg2);
```

- **Description:** Evaluates the predicate with the specified arguments.
- **Parameters:**
    - `arg1` — the first input argument
    - `arg2` — the second input argument
- **Returns:** `true` or `false` based on the predicate logic.