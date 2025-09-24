# 🧩 IPredicates

The **IPredicate** interfaces are specialized forms of [functions](IFunctions.md) that evaluate a condition and return
`true` or `false`, useful in filtering, validation, and decision-making logic.

There are several interfaces of predicaes, depending on the number of arguments the actions take:

- [IPredicate&lt;R&gt;](IPredicate.md) — Predicate without parameters.
- [IPredicate&lt;T, R&gt;](IPredicate%601.md) — Predicate that takes one argument.
- [IPredicate&lt;T1, T2, R&gt;](IPredicate%602.md) — Predicate that takes two arguments.


---

## 🗂 Examples of Usage

### Predicate without arguments

```csharp
public class IsGameActivePredicate : IPredicate
{
    private readonly GameManager _manager;

    public IsGameActivePredicate(GameManager manager) => _manager = manager;
    
    public bool Invoke() => _manager.IsActive;
}
```

---

### Predicate with one argument

```csharp
public class IsEnemyPredicate : IPredicate<Character>
{
    private readonly Character _source;

    public IsEnemyPredicate(Character source) => _source = source;
    
    public bool Invoke(Character other) => _source.Team != other.Team;
}
```

---

### Predicate with two arguments

```csharp
public class AreAlliesPredicate : IPredicate<Character, Character>
{
    public bool Invoke(Character a, Character b) => a.Team == b.Team;
}
```