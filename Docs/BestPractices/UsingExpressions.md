
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