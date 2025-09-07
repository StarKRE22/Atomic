# 🧩 InlineExpression Classes

`InlineExpression` is a flexible class for creating expressions with custom evaluation logic. It allows you to define how a list of functions is evaluated to produce a result.

---

## `InlineExpression<R>`

Represents an expression that uses a custom function to evaluate a list of parameterless functions returning a result of type `R`.

### Constructors
```csharp
InlineExpression(Func<Enumerator, R> function)
InlineExpression(Func<Enumerator, R> function, params Func<R>[] enumerator)
InlineExpression(Func<Enumerator, R> function, IEnumerable<Func<R>> enumerator)
```
### Methods
```csharp
protected override R Invoke(Enumerator enumerator)
```
- Invokes the expression using the custom evaluation logic.
- **Parameters**: `Enumerator enumerator` – the list of functions.
- **Returns**: The result of type R.
---
## `InlineExpression<T, R>`
Represents an expression that uses a custom function to evaluate a list of single-parameter functions.
### Constructors
```csharp
InlineExpression(Func<Enumerator, T, R> function)
InlineExpression(Func<Enumerator, T, R> function, params Func<T, R>[] enumerator)
InlineExpression(Func<Enumerator, T, R> function, IEnumerable<Func<T, R>> enumerator)
```
### Methods
```csharp
protected override R Invoke(Enumerator enumerator, T arg)
```
- Invokes the expression with a single argument using custom evaluation logic.
- Parameters:
  - `Enumerator enumerator` – the list of functions.
  - `T arg` – the argument passed to each function.
- **Returns**: The result of type `R`.
---
## `InlineExpression<T1, T2, R>`
Represents an expression that uses a custom function to evaluate a list of functions taking two parameters.

### Constructors
```csharp
InlineExpression(Func<Enumerator, T1, T2, R> function)
InlineExpression(Func<Enumerator, T1, T2, R> function, params Func<T1, T2, R>[] enumerator)
InlineExpression(Func<Enumerator, T1, T2, R> function, IEnumerable<Func<T1, T2, R>> enumerator)
```

### Methods
```csharp
protected override R Invoke(Enumerator enumerator, T1 arg1, T2 arg2)
```

- Invokes the expression with two arguments using custom evaluation logic.
- **Parameters**:
  - `Enumerator enumerator` – the list of functions.
  - T1 arg1 – the first argument.
  - T2 arg2 – the second argument.
**Returns**: The result of type `R`.
---
## Example Usage
```csharp
// Parameterless expression
var sumExpression = new InlineExpression<int>(enumerator => {
    int sum = 0;
    while (enumerator.MoveNext())
        sum += enumerator.Current.Invoke();
    return sum;
}, () => 1, () => 2, () => 3);

int total = sumExpression.Invoke(); // total = 6

// Single-parameter expression
var multiplyExpression = new InlineExpression<int, int>((enumerator, x) => {
    int product = 1;
    while (enumerator.MoveNext())
        product *= enumerator.Current.Invoke(x);
    return product;
}, x => x + 1, x => x + 2);

int result = multiplyExpression.Invoke(2); // result = 12 ((2+1)*(2+2))

// Two-parameter expression
var sumTwoParams = new InlineExpression<int, int, int>((enumerator, a, b) => {
    int sum = 0;
    while (enumerator.MoveNext())
        sum += enumerator.Current.Invoke(a, b);
    return sum;
}, (a, b) => a + b, (a, b) => a * b);

int combined = sumTwoParams.Invoke(2, 3); // combined = 11 (2+3 + 2*3)
```