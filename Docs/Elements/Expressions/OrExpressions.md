# 🧩 OrExpressions

Represents **logical OR expressions** composed of one or more boolean-returning functions. They extend from
the [ExpressionBase](ExpressionsBase.md) family of classes and implement the
corresponding [IPredicate](../Functions/IPredicates.md) interfaces.

> [!NOTE]
> The expression evaluates to `true` **if at least one function member returns `true`**.  
> If the collection is empty, the expression evaluates to `false` by default.

There are several classes of **OR** expressions, depending on the number of arguments the actions take:

- [OrExpression](OrExpression.md) — Non-generic version; works without parameters.
- [OrExpression&lt;T&gt;](OrExpression%601.md) — Expression that takes one argument.
- [OrExpression&lt;T1, T2&gt;](OrExpression%602.md) — Expression that takes two arguments.

---

## 🗂 Example of Usage

Below are examples of using `OrExpression` to configure an entity using `Atomic.Entities`.

```csharp
// Setting up a character with an OR expression for healing
public sealed class CharacterInstaller : SceneEntityInstaller
{
    [SerializeField] private ReactiveVariable<int> _medkitCount = 3;
    [SerializeField] private ReactiveVariable<IEntity> _targetMedkit = new();

    public override void Install(IEntity entity)
    {
        // Life: add a condition for healing
        entity.AddHealingCondition(new OrExpression(
            () => _medkitCount.Value > 0,         // Has medkit in the inventory
            () => _targetMedkit.Value != null     // Has medkit pick up nearby
        ));
    }
}
```

```csharp
// Use healing condition for AI as example:
IExpression<bool> condition = entity.GetHealingCondition();
bool canHealing = condition.Invoke();
```