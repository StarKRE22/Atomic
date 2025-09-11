# 🧩 Expression Interfaces

These interfaces represent **expressions composed of function members** that can be dynamically added, removed, and
evaluated. They support parameterless functions as well as functions with one or more parameters.

> [!IMPORTANT]
> Expressions act as dynamic functions that evaluate all registered members, making them ideal for flexible,
> runtime-adjustable calculations. For example, you can add multipliers for speed, apply effects when an object is frozen,
> or modify a value based on boosts.

> [!NOTE]  
> Additionally, `IExpression` **implements** `IList` (so it can hold multiple function members) and [IFunction](../Functions/IFunction.md) (so it itself can be evaluated as a function).
---

## IExpression&lt;R&gt;

```csharp
public interface IExpression<R> : IList<Func<R>>, IValue<R>
```
- A **parameterless expression** that returns a value of type `R`.

### Properties

| Property          | Type      | Description                                              |
|-------------------|-----------|----------------------------------------------------------|
| `Value`           | `R`       | Evaluates the expression and returns the result.         |
| `Count`           | `int`     | Gets the number of functions in the expression.          |
| `IsReadOnly`      | `bool`    | Indicates whether the list of functions can be modified. |
| `this[int index]` | `Func<R>` | Indexer to access a function at a specific position.     |

### Methods

| Method          | Parameters                        | Returns                | Description                                        |
|-----------------|-----------------------------------|------------------------|----------------------------------------------------|
| `Invoke`        | -                                 | `R`                    | Evaluates the expression and returns the result.   |
| `Add`           | `Func<R> item`                    | `void`                 | Adds a function to the expression.                 |
| `Clear`         | —                                 | `void`                 | Removes all functions from the expression.         |
| `Contains`      | `Func<R> item`                    | `bool`                 | Checks if the function exists in the expression.   |
| `CopyTo`        | `Func<R>[] array, int arrayIndex` | `void`                 | Copies the functions to an array.                  |
| `IndexOf`       | `Func<R> item`                    | `int`                  | Gets the index of a function.                      |
| `Insert`        | `int index, Func<R> item`         | `void`                 | Inserts a function at a specific index.            |
| `Remove`        | `Func<R> item`                    | `bool`                 | Removes the specified function.                    |
| `RemoveAt`      | `int index`                       | `void`                 | Removes the function at a specific index.          |
| `GetEnumerator` | —                                 | `IEnumerator<Func<R>>` | Returns an enumerator for iterating the functions. |
---

## IExpression&lt;T, R&gt;
```csharp
public interface IExpression<T, R> : IList<Func<T, R>>, IValue<R>
```
- An **expression with a single parameter** of type `T` that returns a value of type `R`.

### Properties

| Property          | Type           | Description                                              |
|-------------------|----------------|----------------------------------------------------------|
| `Value`           | `R`            | Evaluates the expression with the current argument and returns the result. |
| `Count`           | `int`          | Gets the number of functions in the expression.          |
| `IsReadOnly`      | bool           | Indicates whether the list of functions can be modified. |
| `this[int index]` | `Func<T, R>`   | Indexer to access a function at a specific position.     |

### Methods

| Method          | Parameters                        | Returns                | Description                                        |
|-----------------|-----------------------------------|------------------------|----------------------------------------------------|
| `Invoke`        | `T arg`                           | `R`                    | Evaluates the expression with the provided argument and returns the result. |
| `Add`           | `Func<T, R> item`                 | `void`                 | Adds a function to the expression.                 |
| `Clear`         | —                                 | `void`                 | Removes all functions from the expression.         |
| `Contains`      | `Func<T, R> item`                 | `bool`                 | Checks if the function exists in the expression.   |
| `CopyTo`        | `Func<T, R>[] array, int arrayIndex` | `void`              | Copies the functions to an array.                  |
| `IndexOf`       | `Func<T, R> item`                 | `int`                  | Gets the index of a function.                      |
| `Insert`        | `int index, Func<T, R> item`      | `void`                 | Inserts a function at a specific index.            |
| `Remove`        | `Func<T, R> item`                 | `bool`                 | Removes the specified function.                    |
| `RemoveAt`      | `int index`                        | `void`                 | Removes the function at a specific index.          |
| `GetEnumerator` | —                                 | `IEnumerator<Func<T, R>>` | Returns an enumerator for iterating the functions. |
---

## IExpression&lt;T1, T2, R&gt;
```csharp
public interface IExpression<T1, T2, R> : IList<Func<T1, T2, R>>, IValue<R>
```
- An **expression with two parameters** of types `T1` and `T2` that returns a value of type `R`.

### Properties

| Property          | Type             | Description                                              |
|-------------------|-----------------|----------------------------------------------------------|
| `Value`           | `R`              | Evaluates the expression with the current arguments and returns the result. |
| `Count`           | `int`            | Gets the number of functions in the expression.          |
| `IsReadOnly`      | `bool`           | Indicates whether the list of functions can be modified. |
| `this[int index]` | `Func<T1, T2, R>` | Indexer to access a function at a specific position.     |

### Methods

| Method          | Parameters                       | Returns                  | Description                                        |
|-----------------|----------------------------------|--------------------------|----------------------------------------------------|
| `Invoke`        | `T1 arg1, T2 arg2`               | `R`                      | Evaluates the expression with the provided arguments and returns the result. |
| `Add`           | `Func<T1, T2, R> item`           | `void`                   | Adds a function to the expression.                 |
| `Clear`         | —                                | `void`                   | Removes all functions from the expression.         |
| `Contains`      | `Func<T1, T2, R> item`           | `bool`                   | Checks if the function exists in the expression.   |
| `CopyTo`        | `Func<T1, T2, R>[] array, int arrayIndex` | `void`          | Copies the functions to an array.                  |
| `IndexOf`       | `Func<T1, T2, R> item`           | `int`                    | Gets the index of a function.                      |
| `Insert`        | `int index, Func<T1, T2, R> item` | `void`                  | Inserts a function at a specific index.            |
| `Remove`        | `Func<T1, T2, R> item`           | `bool`                   | Removes the specified function.                    |
| `RemoveAt`      | `int index`                       | `void`                  | Removes the function at a specific index.          |
| `GetEnumerator` | —                                | `IEnumerator<Func<T1, T2, R>>` | Returns an enumerator for iterating the functions. |
---

## 🗂 Example Usage

```csharp
// Suppose we have a concrete implementation of IExpression<int>
IExpression<int> expression = ...;

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

## 📝 Notes

Expressions are particularly useful for dynamic runtime calculations, such as:

- Applying **speed multipliers** from various sources (buffs, debuffs, environmental effects).
- Adding or removing conditions like **frozen state**, **boosts**, or other temporary effects.
- Combining multiple **dynamic factors** to calculate a final value on the fly.

This makes `IExpression` a flexible, runtime-adjustable function container suitable for game logic or any system
requiring composable dynamic calculations.