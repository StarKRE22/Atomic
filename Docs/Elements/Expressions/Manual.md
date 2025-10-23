# 🧩 Expressions

Represent **expressions composed of function members** that can be dynamically added, removed, and evaluated. It
supports both parameterless functions and functions with one or more parameters, enabling flexible and reusable logic
composition.

> [!NOTE]
> Expressions act as dynamic functions that evaluate all registered members, making them ideal for flexible,
> runtime-adjustable calculations. For example, you can add multipliers for speed, apply effects when an object is
> frozen, or modify a value based on boosts.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [And Expression](#ex-1)
    - [Or Expression](#ex-2)
    - [Int Sum Expression](#ex-3)
    - [Float Mul Expression](#ex-4)
    - [Generic Expression](#ex-5)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

<div id="ex-1"></div>

### 1️⃣ And Expression

In the example, we define three firing conditions for a game character — checking health, ammo, and weapon cooldown.

```csharp
// Create an instance of the combined expression
AndExpression fireCondition = new AndExpression();

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

---

<div id="ex-2"></div>

### 2️⃣ Or Expression

In the example, we define three discount conditions for a store customer — checking if they are a VIP member, have a
discount coupon, or if there is a holiday sale.

```csharp
// Create an OrExpression for discount eligibility
OrExpression discountCondition = new OrExpression();

// Define separate conditions for applying a discount
Func<bool> isVIPCustomer     = () => customer.IsVIP;
Func<bool> hasCoupon         = () => customer.HasCoupon;
Func<bool> isHolidaySale     = () => DateTime.Now.DayOfWeek == DayOfWeek.Friday;

// Add conditions to the expression
discountCondition.Add(isVIPCustomer);
discountCondition.Add(hasCoupon);
discountCondition.Add(isHolidaySale);

// Evaluate the combined condition
bool canApplyDiscount = discountCondition.Invoke();
Console.WriteLine($"Can apply discount: {canApplyDiscount}");

// Check if a condition exists
bool contains = discountCondition.Contains(hasCoupon);
Console.WriteLine($"Contains coupon condition: {contains}");

// Remove a specific condition
discountCondition.RemoveAt(isHolidaySale);

// Insert a new one at position 1
discountCondition.Insert(1, () => customer.HasMembership);

// Enumerate all conditions
foreach (Func<bool> func in discountCondition)
    Console.WriteLine($"Condition result: {func()}");
```

---

<div id="ex-3"></div>

### 3️⃣ Int Sum Expression

```csharp
var expression = new IntSumExpression(
    () => 2,
    () => 3,
    () => 4
);
int result = expression.Invoke(); // 9
```

<div id="ex-4"></div>

---

### 4️⃣ Float Mul Expression

Below is an example where multiple factors or multipliers influence a single numeric outcome — such as calculating the
total store profit.

```csharp
// Example data
float baseProfit = 1000f;
float seasonalBonus = 1.2f;     // +20% due to season
float marketingBoost = 1.5f;    // +50% from advertising
float loyaltyMultiplier = 1.1f; // +10% from loyal customers

//Create an expression
var profitMultiplier = new FloatMulExpression(
    () => baseProfit,          // Base daily profit
    () => seasonalBonus,       // Seasonal bonus multiplier
    () => marketingBoost,      // Marketing campaign multiplier
    () => loyaltyMultiplier    // Customer loyalty multiplier
);

// Calculate final profit
float totalProfit = profitMultiplier.Invoke();
Console.WriteLine($"Total Profit: {totalProfit}"); // Output: 1980
```

---

<div id="ex-5"></div>

### 5️⃣ Generic Expression

Also, you can use generic versions of expression depending on different scenarios:

```csharp
IExpression<GameObject, bool> attackExpression = ...

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

There are several interfaces and implementations of expressions, depending on the concrete scenario and the number of
arguments the expression take:

<details>
  <summary><a href="IExpressions.md">IExpressions</a></summary>
  <ul>
    <li><a href="IExpression.md">IExpression&lt;R&gt;</a></li>
    <li><a href="IExpression%601.md">IExpression&lt;T, R&gt;</a></li>
    <li><a href="IExpression%602.md">IExpression&lt;T1, T2, R&gt;</a></li>
  </ul>
</details>

<details>
  <summary><a href="ExpressionsBase.md">ExpressionsBase</a></summary>
  <ul>
    <li><a href="ExpressionBase.md">ExpressionBase&lt;R&gt;</a></li>
    <li><a href="ExpressionBase%601.md">ExpressionBase&lt;T, R&gt;</a></li>
    <li><a href="ExpressionBase%602.md">ExpressionBase&lt;T1, T2, R&gt;</a></li>
  </ul>
</details>

<details>
  <summary><a href="AndExpressions.md">AndExpressions</a></summary>
  <ul>
    <li><a href="AndExpression.md">AndExpression</a></li>
    <li><a href="AndExpression%601.md">AndExpression&lt;T&gt;</a></li>
    <li><a href="AndExpression%602.md">AndExpression&lt;T1, T2&gt;</a></li>
  </ul>
</details>

<details>
  <summary><a href="OrExpressions.md">OrExpressions</a></summary>
  <ul>
    <li><a href="OrExpression.md">OrExpression</a></li>
    <li><a href="OrExpression%601.md">OrExpression&lt;T&gt;</a></li>
    <li><a href="OrExpression%602.md">OrExpression&lt;T1, T2&gt;</a></li>
  </ul>
</details>

<details>
  <summary><a href="IntMulExpressions.md">IntMulExpressions</a></summary>
  <ul>
    <li><a href="IntMulExpression.md">IntMulExpression</a></li>
    <li><a href="IntMulExpression%601.md">IntMulExpression&lt;T&gt;</a></li>
    <li><a href="IntMulExpression%602.md">IntMulExpression&lt;T1, T2&gt;</a></li>
  </ul>
</details>

<details>
  <summary><a href="IntSumExpressions.md">IntSumExpressions</a></summary>
  <ul>
    <li><a href="IntSumExpression.md">IntSumExpression</a></li>
    <li><a href="IntSumExpression%601.md">IntSumExpression&lt;T&gt;</a></li>
    <li><a href="IntSumExpression%602.md">IntSumExpression&lt;T1, T2&gt;</a></li>
  </ul>
</details>

<details>
  <summary><a href="FloatMulExpressions.md">FloatMulExpressions</a></summary>
  <ul>
    <li><a href="FloatMulExpression.md">FloatMulExpression</a></li>
    <li><a href="FloatMulExpression%601.md">FloatMulExpression&lt;T&gt;</a></li>
    <li><a href="FloatMulExpression%602.md">FloatMulExpression&lt;T1, T2&gt;</a></li>
  </ul>
</details>

<details>
  <summary><a href="FloatSumExpressions.md">FloatSumExpressions</a></summary>
  <ul>
    <li><a href="FloatSumExpression.md">FloatSumExpression</a></li>
    <li><a href="FloatSumExpression%601.md">FloatSumExpression&lt;T&gt;</a></li>
    <li><a href="FloatSumExpression%602.md">FloatSumExpression&lt;T1, T2&gt;</a></li>
  </ul>
</details>

<details>
  <summary><a href="InlineExpressions.md">InlineExpressions</a></summary>
  <ul>
    <li><a href="InlineExpression.md">InlineExpression</a></li>
    <li><a href="InlineExpression%601.md">InlineExpression&lt;T&gt;</a></li>
    <li><a href="InlineExpression%602.md">InlineExpression&lt;T1, T2&gt;</a></li>
  </ul>
</details>

---

## 📌 Best Practices

- [Using Expressions for Entities](../../BestPractices/UsingExpressions.md)
- [Constants with AndExpressions](../../BestPractices/UsingConstantsWithAndExpressions.md) <!-- + -->