# 🧩 Expressions

Represents **expressions composed of function members** that can be dynamically added, removed, and evaluated. It
supports both parameterless functions and functions with one or more parameters, enabling flexible and reusable logic
composition.

> [!NOTE]
> Expressions act as dynamic functions that evaluate all registered members, making them ideal for flexible,
> runtime-adjustable calculations. For example, you can add multipliers for speed, apply effects when an object is
> frozen, or modify a value based on boosts.

- [IExpressions](IExpressions.md) <!-- + -->
    - [IExpression&lt;R&gt;](IExpression.md) <!-- + -->
    - [IExpression&lt;T, R&gt;](IExpression%601.md) <!-- + -->
    - [IExpression&lt;T1, T2, R&gt;](IExpression%602.md) <!-- + -->
- [Expressions Base]()
    - [ExpressionBase&lt;R&gt;]()
    - [ExpressionBase&lt;T, R&gt;]()
    - [ExpressionBase&lt;T1, T2, R&gt;]()
- [AndExpressions]()
    - [AndExpression&lt;R&gt;]()
    - [AndExpression&lt;T, R&gt;]()
    - [AndExpression&lt;T1, T2, R&gt;]()
- [OrExpressions]()
    - [OrExpression&lt;R&gt;]()
    - [OrExpression&lt;T, R&gt;]()
    - [OrExpression&lt;T1, T2, R&gt;]()
- [IntMulExpressions]()
    - [IntMulExpression&lt;R&gt;]()
    - [IntMulExpression&lt;T, R&gt;]()
    - [IntMulExpression&lt;T1, T2, R&gt;]()
- [IntSumExpressions]()
    - [IntSumExpression&lt;R&gt;]()
    - [IntSumExpression&lt;T, R&gt;]()
    - [IntSumExpression&lt;T1, T2, R&gt;]()
- [FloatMulExpressions]()
    - [FloatMulExpression&lt;R&gt;]()
    - [FloatMulExpression&lt;T, R&gt;]()
    - [FloatMulExpression&lt;T1, T2, R&gt;]()
- [FloatSumExpressions]()
    - [FloatSumExpression&lt;R&gt;]()
    - [FloatSumExpression&lt;T, R&gt;]()
    - [FloatSumExpression&lt;T1, T2, R&gt;]()
- [InlineExpressions]()
    - [InlineExpression&lt;R&gt;]()
    - [InlineExpression&lt;T, R&gt;]()
    - [InlineExpression&lt;T1, T2, R&gt;]()