# 📌 Using Expressions with Entities

This section demonstrates how to use logical expressions such
as [OrExpression](../Elements/Expressions/OrExpressions.md) and [
AndExpression](../Elements/Expressions/AndExpressions.md) to **define conditional logic** within entities using
the [Atomic.Entities](../Entities/Manual.md) framework.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
    - [Using OrExpression for Healing Condition](#1-using-orexpression-for-healing-condition)
    - [Using AndExpression for Fire Condition](#2-using-andexpression-for-fire-condition)
    - [Result](#3-result)
- [Conclusion](#-conclusion)
- [Benefits](#-benefits)

---

## 🗂 Example of Usage

### 1. Using OrExpression for Healing Condition

Below is an example of how to use `OrExpression` to define **healing conditions** for an entity.

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

---

### 2. Using AndExpression for Fire Condition

Another example demonstrates `AndExpression` — for defining a **firing condition** that requires both health and ammo.

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

---

### 3. Result

By combining multiple conditions through `OrExpression` and `AndExpression`, you can easily control complex gameplay
logic such as **when a character can heal, shoot, or perform any other conditional action** — all without hardcoding
dependencies or state checks.

---

## 🏁 Conclusion

- [OrExpression](../Elements/Expressions/OrExpression.md) and [
  AndExpression](../Elements/Expressions/AndExpression.md) allow entities to **evaluate multiple logical conditions**
  dynamically.
- Expressions are fully **reactive** — when underlying variables change, the logic automatically reflects the new state.
- These tools enable **modular gameplay logic**, allowing you to compose conditions from independent sources.
- This pattern encourages **cleaner, declarative code**, making behaviors easier to extend or modify.
- Integrated with the [Atomic.Entities](../Entities/Manual.md) framework, expressions serve as the foundation for 
  **conditional actions, AI decisions, and reactive gameplay systems**.

---

## ✅ Benefits

- Promotes **reactive and declarative logic** within entities.
- Simplifies **complex condition evaluation** (e.g., health, ammo, status, proximity).
- Reduces **boilerplate checks** and manual state management in gameplay code.
- Increases **maintainability** — conditions are easy to update and reason about.
- Enables **flexible behavior composition**, combining multiple logic expressions as needed.

<!--

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

-->