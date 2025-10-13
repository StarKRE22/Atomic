# 🧩 IExpressions

These interfaces represent **expressions composed of function members** that can be dynamically added, removed, and
evaluated. They support parameterless functions as well as functions with one or more parameters.

> [!NOTE]  
> Additionally, expression **implement** [IReactiveList&lt;T&gt;](../Collections/IReactiveList.md)
> and inherit [IFunction](../Functions/IFunctions.md) interfaces.
> So it can hold multiple function members and can be evaluated as a function.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Parameterless Expression](#ex-1)
    - [Expression with single parameter](#ex-2)
    - [Expression with two parameters](#ex-3)
- [API Reference](#-api-reference)

---

## 🗂 Examples of Usage

<div id="ex-1"></div>

### 1️⃣ Parameterless Expression 

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

<div id="ex-2"></div>

### 2️⃣ Expression with single parameter

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

<div id="ex-3"></div>

### 3️⃣ Expression with two parameters

```csharp
IExpression<int, int, int> expression = ...
    
// Add some functions
expression.Add((a, b) => a + b);
expression.Add((a, b) => a * 2 + b);

// Evaluate expression
int result = sumExpression.Invoke(3, 5);
Console.WriteLine($"Result: {result}"); // Output depends on how the expression combines functions
```

---

## 🔍 API Reference

There are several interfaces of expressions, depending on the number of arguments the expressions take:

- [IExpression&lt;R&gt;](IExpression.md) — Non-generic version; works without parameters.
- [IExpression&lt;T, R&gt;](IExpression%601.md) — Expression that takes one argument.
- [IExpression&lt;T1, T2, R&gt;](IExpression%602.md) — Expression that takes two arguments.
