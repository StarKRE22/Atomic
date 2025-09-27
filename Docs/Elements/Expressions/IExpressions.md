# 🧩 IExpressions

These interfaces represent **expressions composed of function members** that can be dynamically added, removed, and
evaluated. They support parameterless functions as well as functions with one or more parameters.

> [!NOTE]  
> Additionally, expression **implement** [IReactiveList&lt;T&gt;](../Collections/IReactiveList.md)
> and inherit [IFunction](../Functions/IFunctions.md) interfaces. 
> So it can hold multiple function members and can be evaluated as a function.

---

There are several interfaces of expressions, depending on the number of arguments the actions take:

- [IExpression&lt;R&gt;](IExpression.md) — Non-generic version; works without parameters.
- [IExpression&lt;T, R&gt;](IExpression%601.md) — Expression that takes one argument.
- [IExpression&lt;T1, T2, R&gt;](IExpression%602.md) — Expression that takes two arguments.

---

## 🗂 Example Usage

```csharp
// Suppose we have a concrete implementation of IExpression<int>
IExpression<int> expression = ...

// Add some functions
expression.Add(() => 10);
expression.Add(() => 20);
expression.Add(() => 30);

// Evaluate the combined expression using Value
int result = expression.Invoke();
Console.WriteLine($"Combined expression result: {result}");

// Check if a function exists
Func<int> testFunc = () => 20;
bool contains = expression.Contains(testFunc); // might be false if reference differs

// Remove a function
expression.Remove(expression[0]);

// Insert a function at index 1
expression.Insert(1, () => 42);

// Enumerate all functions
foreach (Func<int> func in expression)
    Console.WriteLine($"Function result: {func()}");
```