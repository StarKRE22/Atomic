# 🧩 AndExpressions

Represents **logical AND expressions** composed of one or more boolean-returning functions. They extend from
the [ExpressionBase](ExpressionsBase.md) family of classes and implement the
corresponding [IPredicate](../Functions/IPredicates.md) interfaces.

> [!NOTE]
> The expression evaluates to `true` **only if all function members return `true`**.
> If the collection is empty, the expression evaluates to `true` by default.

There are several classes of **AND** expressions, depending on the number of arguments the actions take:

- [AndExpression](AndExpression.md) — Non-generic version; works without parameters.
- [AndExpression&lt;T&gt;](AndExpression%601.md) — Expression that takes one argument.
- [AndExpression&lt;T1, T2&gt;](AndExpression%602.md) — Expression that takes two arguments.

---

## 🗂 Example of Usage

Below are examples of using `AndExpression` to configure an entity using `Atomic.Entities`.

```csharp
// Setting up a character with an AND expression for firing
public sealed class CharacterInstaller : SceneEntityInstaller
{
    [SerializeField] private ReactiveVariable<int> _health = 3;
    [SerializeField] private ReactiveVariable<int> _ammo = 10;

    public override void Install(IEntity entity)
    {
        // Life:
        entity.AddHealth(_health);
        
        // Combat: add a condition for firing
        entity.AddFireCondition(new AndExpression(
            () => _health.Value > 0,  // Character is alive
            () => _ammo.Value > 0     // Has ammo
        ));

        // Add the fire action, executed only if the condition is true
        entity.AddFireAction(new InlineAction(() => 
        {
            IFunction<bool> condition = entity.GetFireCondition();
            if (condition.Invoke())
            {
                // Perform fire action...
            }
        }));
    }
}
```

```csharp
// Dynamically disable firing (e.g., character is disarmed)
IExpression<bool> condition = entity.GetFireCondition();
condition.Add(() => false);
```