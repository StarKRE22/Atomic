# 🧩 OrExpression Classes
`OrExpression` represents a logical OR evaluation composed of functions that return boolean values. The expression evaluates to `true` if **any** function returns `true`.
---
## `OrExpression`
Represents an OR expression with parameterless boolean functions.
### Constructors
```csharp
OrExpression()
OrExpression(IEnumerable<Func<bool>> members)
OrExpression(params Func<bool>[] members)
```
### Methods
```csharp
protected override bool Invoke(Enumerator enumerator)
```
- Evaluates all function members.
- Returns `true` if at least one function returns `true`, otherwise `false`.
---
## `OrExpression<T>`
Represents an OR expression with single-parameter boolean functions.
### Constructors
```csharp
OrExpression()
OrExpression(IEnumerable<Func<T, bool>> members)
OrExpression(params Func<T, bool>[] members)
```
### Methods
```csharp
protected override bool Invoke(Enumerator enumerator, T arg)
```
- Evaluates all function members with the argument `arg`.
- Returns `true` if any function returns true, otherwise `false`.
---
## `OrExpression<T1, T2>`
Represents an OR expression with two-parameter boolean functions.
### Constructors
```csharp
OrExpression()
OrExpression(IEnumerable<Func<T1, T2, bool>> members)
OrExpression(params Func<T1, T2, bool>[] members)
```
### Methods
```csharp
protected override bool Invoke(Enumerator enumerator, T1 arg1, T2 arg2)
```
- Evaluates all function members with arguments `arg1` and `arg2`.
- Returns `true` if any function returns `true`, otherwise `false`.
---
## Example Usage
```csharp
// Parameterless OR expression
var canAct = new OrExpression(
    () => IsEnemyNearby(),
    () => IsInjured()
);

bool result = canAct.Invoke(); // true if any condition is true
```
```csharp
// Single-parameter OR expression
var canAttack = new OrExpression<Player>(player =>
    player.IsAlive && player.HasWeapon,
    player => player.HasMagic
);

bool attackPossible = canAttack.Invoke(myPlayer);
```
```csharp
// Two-parameter OR expression
var canMove = new OrExpression<Player, Environment>((player, env) =>
    env.IsPathClear(player.Position),
    (player, env) => player.HasFlyAbility()
);

bool movePossible = canMove.Invoke(myPlayer, gameWorld);
```