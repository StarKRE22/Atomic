# 🧩 InlineExpressions

A flexible class for creating expressions with **custom evaluation logic**. It allows you to define how a list of
functions is evaluated to produce a result. Extends from [ExpressionBase](ExpressionBase.md).

There are several implementations of expressions, depending on the number of arguments the actions take:

- [InlineExpression&lt;R&gt;](InlineExpression.md) — Non-generic version; works without parameters.
- [InlineExpression&lt;T, R&gt;](InlineExpression%601.md) — Expression that takes one argument.
- [InlineExpression&lt;T1, T2, R&gt;](InlineExpression%602.md) — Expression that takes two arguments.

---

## 🗂 Examples of Usage

#### `InlineExpression<R>`

```csharp
//Create an instance of "SUM" expression
var expression = new InlineExpression<int>(enumerator => {
    int sum = 0;
    while (enumerator.MoveNext())
        sum += enumerator.Current.Invoke();
    return sum;
});

//Add functions:
expression.Add(() => 1);
expression.Add(() => 2);
expression.Add(() => 3);

//Evaluate:
int sum = expression.Invoke(); // 1 + 2 + 3 = 6
```

---

#### `InlineExpression<T, R>`

```csharp
//Create an instance of "PRODUCT" expression
var expression = new InlineExpression<int, int>((enumerator, x) => {
    int product = 1;
    while (enumerator.MoveNext())
        product *= enumerator.Current.Invoke(x);
    return product;
});

//Add functions:
expression.Add(x => x + 1);
expression.Add(x => x + 2);

//Evaluate:
int product = expression.Invoke(2); // (2 + 1) * (2 + 2) = 12
```

---

#### `InlineExpression<T1, T2, R>`

```csharp
var expression = new InlineExpression<int, int, int>((enumerator, x, y) => {
    int sum = 0;
    while (enumerator.MoveNext())
        sum += enumerator.Current.Invoke(x, y);
    return sum;
});

//Add functions:
expression.Add((a, b) => a + b);
expression.Add((a, b) => a * b);

//Evaluate:
int result = expression.Invoke(2, 3); // (2 + 3) + (2 * 3) = 11
```