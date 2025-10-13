# 🧩 AndExpressions

Represents **logical AND expressions** composed of one or more boolean-returning functions. They extend from
the [ExpressionBase](ExpressionsBase.md) family of classes and implement the
corresponding [IPredicate](../Functions/IPredicates.md) interfaces.

> [!NOTE]
> The expression evaluates to `true` **only if all function members return `true`**.
> If the collection is empty, the expression evaluates to `true` by default.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Non-generic Version](#ex-1)
    - [Generic Version](#ex-2)
- [API Reference](#-api-reference)

---

## 🗂 Examples of Usage

<div id="ex-1"></div>

### 1️⃣ Non-generic version

```csharp
// Create an instance of the combined expression
var fireCondition = new AndExpression();

// Assume we have some preconditions for firing a weapon
Func<bool> healthExists = () => player.Health > 0;
Func<bool> ammoExists   = () => player.Ammo > 0;
Func<bool> isCooldown   = () => !player.IsWeaponOnCooldown;

// Add preconditions
fireCondition.Add(healthExists);
fireCondition.Add(ammoExists);
fireCondition.Add(isCooldown);

// Evaluate the combined expression
bool canFire = fireCondition.Invoke();

// Check if a specific condition exists
bool contains = fireCondition.Contains(ammoExists);

// Remove a condition by reference
fireCondition.RemoveAt(isCooldown);

// Remove the first condition by index
fireCondition.RemoveAt(0);

// Insert a new condition at index 1
fireCondition.Insert(1, () => true);

// Enumerate all conditions and print their results
foreach (Func<bool> func in fireCondition)
    Console.WriteLine($"Function result: {func()}");
```

<div id="ex-2"></div>

### 2️⃣ Generic version

```csharp
var attackExpression = new AndExpression<GameObject>()

// Assume we have a group of preconditions for attack
Func<GameObject, bool> isEnemy, isAlive  = ...
    
// Add some functions
attackExpression.Add(isEnemy);
attackExpression.Add(isAlive);

// Evaluate the combined expression using Value
int result = attackExpression.Invoke();
```

---

## 🔍 API Reference

There are several classes of **AND** expressions, depending on the number of arguments the expressions take:

- [AndExpression](AndExpression.md) — Non-generic version; works without parameters.
- [AndExpression&lt;T&gt;](AndExpression%601.md) — Expression that takes one argument.
- [AndExpression&lt;T1, T2&gt;](AndExpression%602.md) — Expression that takes two arguments.