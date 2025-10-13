# 🧩 Expressions Base

Provides **base implementations** of the [IExpression](IExpressions.md) interfaces extending
from [ReactiveLinkedList&lt;T&gt;](../Collections/ReactiveLinkedList.md). They allow **aggregating multiple function
members** and provide **dynamic evaluation** based on parameterless or parameterized functions.

> [!NOTE]
> The expressions are ideal for scenarios where multiple factors or rules must be evaluated dynamically, such as game
> calculations, modifiers, or conditions.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)

---

## 🗂 Example of Usage

Below is an example of using [ExpressionBase](ExpressionBase.md) to extend a simple **logical AND** expression with
multiple parameterless
boolean functions.

```csharp
// Define a concrete implementation of "ExpressionBase<bool>"
public sealed class AndExpression : ExpressionBase<bool>
{
    public AndExpression(params Func<bool>[] members) : base(members) 
    {
    }

    protected override bool Invoke(Enumerator enumerator)
    {
        while (enumerator.MoveNext())
              if (!enumerator.Current.Invoke())
                  return false;

        return true;
    }
}
```

```csharp
// "AndExpression" Usage
var expression = new AndExpression(
    () => true,
    () => true,
    () => false
);

// Evaluate the expression
bool finalResult = expression.Invoke(); // false
Console.WriteLine($"AND Expression result: {finalResult}");

// You can add more functions dynamically
expression.Add(() => true);
finalResult = expression.Invoke(); // still false
```

---

## 🔍 API Reference

There are several base classes of expressions, depending on the number of arguments the expressions take:

- [ExpressionBase&lt;R&gt;](ExpressionBase.md) — Non-generic version; works without parameters.
- [ExpressionBase&lt;T, R&gt;](ExpressionBase%601.md) — Expression that takes one argument.
- [ExpressionBase&lt;T1, T2, R&gt;](ExpressionBase%602.md) — Expression that takes two arguments.
