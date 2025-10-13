# 📌 Using Constants with AndExpressions

If you need to add constant `true` or `false` conditions **without allocations** to the [AndExpression](../Elements/Expressions/AndExpression.md), you can use boolean constants
from [DefaultConstants](../Values/DefaultConstants.md#-boolean-constants)

```csharp
IExpression<bool> condition = new AndExpression(cond1, cond2, cond3);

// Fully disable the AND condition
condition.Add(DefaultConstants.False);

// Reset the lock from the condition
condition.Remove(DefaultConstants.False);
```