# 🧩 OrExpressions

Represents **logical OR expressions** composed of one or more boolean-returning functions. They extend from
the [ExpressionBase](ExpressionsBase.md) family of classes and implement the
corresponding [IPredicate](../Functions/IPredicates.md) interfaces.

> [!NOTE]
> The expression evaluates to `true` **if at least one function member returns `true`**.  
> If the collection is empty, the expression evaluates to `false` by default.

There are several classes of **OR** expressions, depending on the number of arguments the actions take:

- [OrExpression](OrExpression.md) — Non-generic version; works without parameters.
- [OrExpression&lt;T&gt;](OrExpression%601.md) — Expression that takes one argument.
- [OrExpression&lt;T1, T2&gt;](OrExpression%602.md) — Expression that takes two arguments.

---
