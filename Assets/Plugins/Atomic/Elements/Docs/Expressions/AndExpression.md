# 🧩 AndExpression Classes

These classes provide **logical AND expressions** that aggregate multiple functions returning boolean values.  
They evaluate all registered functions and return `true` **only if all functions return `true`**.

---

## `AndExpression`

A parameterless logical AND expression.

```csharp
public class AndExpression : ExpressionBase<bool>, IPredicate
```

These classes provide **logical AND expressions** that aggregate multiple functions returning boolean values.  
They evaluate all registered functions and return `true` **only if all functions return `true`**.
---

## `AndExpression`

A parameterless logical AND expression.

```csharp
public class AndExpression : ExpressionBase<bool>, IPredicate
```

### Constructors
- `AndExpression()` – Initializes an empty AND expression.
- `AndExpression(params Func<bool>[] members)` – Initializes with an array of functions.
- `AndExpression(IEnumerable<Func<bool>> members)` – Initializes with a collection of functions.
### Methods
- `protected sealed override bool Invoke(Enumerator enumerator)` – Returns `true` if all functions return `true` (or if no functions exist).
---
## `AndExpression<T>`
A generic logical AND expression for functions with **one input parameter**.
```csharp
public class AndExpression<T> : ExpressionBase<T, bool>, IPredicate<T>
```
### Constructors
- `AndExpression()` – Initializes an empty AND expression.
- `AndExpression(params Func<T, bool>[] members)` – Initializes with an array of functions.
- `AndExpression(IEnumerable<Func<T, bool>> members)` – Initializes with a collection of functions.

### Methods
- `protected sealed override bool Invoke(Enumerator enumerator, T arg)` – Returns `true` if all functions return `true` for the given argument.
---
## `AndExpression<T1, T2>`
A generic logical AND expression for functions with **two input parameters**.
```csharp
public class AndExpression<T1, T2> : ExpressionBase<T1, T2, bool>, IPredicate<T1, T2>
```
### Constructors
- `AndExpression()` – Initializes an empty AND expression.
- `AndExpression(params Func<T1, T2, bool>[] members)` – Initializes with an array of functions.
- `AndExpression(IEnumerable<Func<T1, T2, bool>> members)` – Initializes with a collection of functions.
### Methods
- `protected override bool Invoke(Enumerator enumerator, T1 arg1, T2 arg2)` – Returns `true` if all functions return `true` for the given arguments.

## Examples of Usage
Example of using AndExpression for a character
```csharp
// Simple logic to check if the character can attack
IFunction<bool> canAttack = new AndExpression(
    () => player.IsAlive,           // Player is alive
    () => !player.IsStunned,        // Player is not stunned
    () => enemy != null,            // There is a target
    () => player.HasWeapon          // Player has a weapon
);

bool attackAllowed = canAttack.Invoke(); // true if all conditions are met
```
```csharp
// Movement logic with directional checks
IFunction<Vector3> canMove = new AndExpression<Vector3>(
    dir => !player.IsStunned,           // Player is not stunned
    dir => !IsObstacleInDirection(dir), // No obstacle in the given direction
    dir => player.Stamina > 0           // Player has enough stamina
);

bool moveAllowed = canMove.Invoke(Vector3.forward); // Check if forward movement is allowed
```