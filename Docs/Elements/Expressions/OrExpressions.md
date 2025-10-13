# 🧩 OrExpressions

Represents **logical OR expressions** composed of one or more boolean-returning functions. They extend from
the [ExpressionBase](ExpressionsBase.md) family of classes and implement the
corresponding [IPredicate](../Functions/IPredicates.md) interfaces.

> [!NOTE]
> The expression evaluates to `true` **if at least one function member returns `true`**.  
> If the collection is empty, the expression evaluates to `false` by default.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)

---

## 🗂 Example of Usage

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

## 🔍 API Reference

There are several classes of **OR** expressions, depending on the number of arguments the expressions take:

- [OrExpression](OrExpression.md) — Non-generic version; works without parameters.
- [OrExpression&lt;T&gt;](OrExpression%601.md) — Expression that takes one argument.
- [OrExpression&lt;T1, T2&gt;](OrExpression%602.md) — Expression that takes two arguments.
