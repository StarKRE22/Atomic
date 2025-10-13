
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