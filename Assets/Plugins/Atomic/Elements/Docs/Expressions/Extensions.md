#  🧩 Extensions for `IExpression`

The `Extensions` static class provides helper extension methods for working with  
[`IExpression<T>`](#), [`IExpression<T, R>`](#), and [`IExpression<T1, T2, R>`](#).

These methods simplify adding and removing functions from expressions.

---

## AddRange
Adds multiple function delegates at once.

```csharp
// Parameterless
expr.AddRange(
    () => 1,
    () => 2,
    () => 3
);

// With one parameter
expr1.AddRange(
    x => x + 1,
    x => x * 2
);

// With two parameters
expr2.AddRange(
    (a, b) => a + b,
    (a, b) => a * b
);
```
---
## Add
Adds a single function to the expression.
Supports both direct delegates and `IFunction<R>` instances (wrapped internally).

```csharp
// Parameterless
IExpression<bool> canMove = new AndExpression();
canMove.Add(new InlineFunction<bool>(() => HealthExists()));
canMove.Add(new InlineFunction<bool>(() => HasBoots()));
canMove.Add(new InlineFunction<bool>(() => HasStamina()));

// With one parameter
IExpression<IEntity, bool> canAttack = new AndExpression<IEntity>();
canAttack.Add(new InlineFunction<IEntity, bool>(target => IsEnemy(target)));
canAttack.Add(new InlineFunction<IEntity, bool>(() => HasWeapon()));

// With two parameters
IExpression<IEntity, IEntity, bool> canTrade = new AndExpression<IEntity, IEntity>();
canTrade.Add(new InlineFunction<IEntity, IEntity, bool>((buyer, seller) => buyer.HasGold()));
canTrade.Add(new InlineFunction<IEntity, IEntity, bool>((buyer, seller) => seller.HasItems()));
```

## Remove
Removes a function from an expression.
Supports both direct delegates and `IFunction<R>` instances (wrapped internally).
Useful for dynamically adjusting conditions, rules, or behaviors.
```csharp
// Parameterless
IExpression<bool> canMove = new AndExpression();
var bootsCheck = new InlineFunction<bool>(() => HasBoots());
canMove.Add(bootsCheck);
canMove.Remove(bootsCheck); // Now boots are not required anymore

// With one parameter
IExpression<IEntity, bool> canAttack = new AndExpression<IEntity>();
var enemyCheck = new InlineFunction<IEntity, bool>(target => IsEnemy(target));
canAttack.Add(enemyCheck);
canAttack.Remove(enemyCheck); // No longer requires "IsEnemy" check

// With two parameters
IExpression<IEntity, IEntity, bool> canTrade = new AndExpression<IEntity, IEntity>();
var buyerCheck = new InlineFunction<IEntity, IEntity, bool>((buyer, seller) => buyer.HasGold());
canTrade.Add(buyerCheck);
canTrade.Remove(buyerCheck); // Trade rule now ignores gold possession
```