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

<details>
  <summary>
    <h2>🧩 IExpression&lt;R&gt;</h2>
    <br> Represents a <b>parameterless expression</b> aggregating multiple functions returning a value of type <code>R</code>
  </summary>

<br>

## 
```csharp
public interface IExpression<R> : IList<Func<R>>, IValue<R>, IFunction<R>
```
- **Type parameter**: `R` — The return type of the expression.

### Properties

#### `Value`
```csharp
public R Value { get; }
```
- **Description:** Evaluates all functions and returns the aggregated result.
- **Returns:** `R` — The evaluated result of the expression.

#### `Count`
```csharp
public int Count { get; }
```
- **Description:** Gets the number of functions in the expression.
- **Returns:** `int` — The number of function members.

#### `IsReadOnly`
```csharp
public bool IsReadOnly { get; }
```
- **Description:** Indicates whether the list of functions can be modified.
- **Returns:** `false`.

### Indexers
#### `this[int index]`
```csharp
public Func<R> this[int index] { get; set; }
```
- **Description:** Indexer to access a function at a specific position.
- **Parameter:** `index` — The position of the function.
- **Returns:** `Func<R>` — The function at the given index.

### Methods
#### `Invoke()`
```csharp
public R Invoke()
```
- **Description:** Evaluates all function members of the expression and returns the aggregated result.
- **Returns:** `R` — The evaluated result of the expression.

#### `Invoke(Enumerator enumerator)`
```csharp
protected abstract R Invoke(Enumerator enumerator)
```
- **Description:** Abstract template method. Derived classes define how the parameterless functions are aggregated and evaluated.
- **Parameter:** `enumerator` — Enumerator over the function members.
- Returns: `R` — The aggregated result of the expression.

#### `Add(Func<R> item)`
```csharp
public void Add(Func<R> item)
```
- **Description:** Adds a function to the expression.
- **Parameter:** `item` — The function to add.

#### `Clear()`
```csharp
public void Clear()
```
- **Description:** Removes all functions from the expression.

#### `Contains(Func<R> item)`
```csharp
public bool Contains(Func<R> item)
```
- **Description:** Checks if the specified function exists in the expression.
- **Parameter:** `item` — The function to check.
- **Returns:** `bool` — `true` if the function exists, otherwise `false`.

#### `CopyTo(Func<R>[] array, int arrayIndex)`
```csharp
public void CopyTo(Func<R>[] array, int arrayIndex)
```
- **Description:** Copies all functions in the expression to the specified array starting at the given index.
- **Parameters:**
    - `array` — The destination array.
    - `arrayIndex` — The starting index in the array.

#### `IndexOf(Func<R> item)`
```csharp
public int IndexOf(Func<R> item)
```
- **Description:** Returns the index of the specified function in the expression.
- **Parameter:** `item` — The function to locate.
- **Returns:** `int` — The index of the function, or `-1` if not found.

#### `Insert(int index, Func<R> item)`
```csharp
public void Insert(int index, Func<R> item)
```
- **Description:** Inserts a function at the specified index.
- **Parameters:**
    - `index` — The position at which to insert.
    - `item` — The function to insert.

#### `Remove(Func<R> item)`
```csharp
public bool Remove(Func<R> item)
```
- **Description:** Removes the specified function from the expression.
- **Parameter:** `item` — The function to remove.
- **Returns:** `bool` — `true` if removed successfully, otherwise `false`.

#### `RemoveAt(int index)`
```csharp
public void RemoveAt(int index)
```
- **Description:** Removes the function at the specified index.
- **Parameter:** `index` — The position of the function to remove.

#### `GetEnumerator()`
```csharp
public IEnumerator<Func<R>> GetEnumerator()
```
- **Description:** Returns an enumerator for iterating over all function members in the expression.
- **Returns:** `IEnumerator<Func<R>>` — Enumerator over the functions.
---
</details>

<details>
  <summary>
    <h2>🧩 IExpression&lt;T, R&gt;</h2>
    <br>Represents an expression with a <b>single input parameter</b> of type <code>T</code> that aggregates multiple functions returning a value of type <code>R</code>>
  </summary>

<br>

```csharp
public interface IExpression<T, R> : IList<Func<T, R>>, IFunction<T, R>
```
- **Description:** Represents an expression with a **single input parameter** of type `T` that aggregates multiple functions returning a value of type `R`.
- **Type Parameters:**
    - `T` - The input parameter type of the functions.
    - `R` - The return type of the expression.
  
### Properties
#### `Count`
```csharp
public int Count { get; }
```
- **Description:** Gets the number of functions in the expression.
- **Returns:** `int` — The number of function members.

#### `IsReadOnly`
```csharp
public bool IsReadOnly { get; }
```
- **Description:** Indicates whether the list of functions can be modified.
- **Returns:** `false`.

### Indexers
#### `this[int index]`
```csharp
public Func<T, R> this[int index] { get; set; }
```
- **Description:** Indexer to access a function at a specific position.
- **Parameter:** `index` — The position of the function.
- **Returns:** `Func<T, R>` — The function at the given index.

### Methods
#### `Invoke(T arg)`
```csharp
public R Invoke(T arg)
```
- **Description:** Evaluates all functions using the provided argument and returns the aggregated result.
- **Parameter:** `arg` — The input argument for the functions.
- **Returns:** `R` — The aggregated result.

#### `Invoke(Enumerator enumerator, T arg)`
```csharp
protected abstract R Invoke(Enumerator enumerator, T arg)
```
- **Description:** Abstract template method. Derived classes define how the functions are aggregated.
- **Parameters:**
    - `enumerator` — Enumerator over the function members.
    - `arg` — The input argument of type `T`.
- **Returns:** `R` — The aggregated result.

#### `Add(Func<T, R> item)`
```csharp
public void Add(Func<T, R> item)
```
- **Description:** Adds a function to the expression.
- **Parameter:** `item` — The function to add.

#### `Clear()`
```csharp
public void Clear()
```
- **Description:** Removes all functions from the expression.

#### `Contains(Func<T, R> item)`
```csharp
public bool Contains(Func<T, R> item)
```
- **Description:** Checks if the function exists in the expression.
- **Returns:** `bool` — `true` if the function is present.

#### `CopyTo(Func<T, R>[] array, int arrayIndex)`
```csharp
public void CopyTo(Func<T, R>[] array, int arrayIndex)
```
- **Description:** Copies the functions to an array.
- **Parameters:**
    - `array` — Destination array.
    - `arrayIndex` — Starting index in the array.

#### `IndexOf(Func<T, R> item)`
```csharp
public int IndexOf(Func<T, R> item)
```
- **Description:** Gets the index of a function.
- **Returns:** `int` — The index of the function, or -1 if not found.

#### `Insert(int index, Func<T, R> item)`
```csharp
public void Insert(int index, Func<T, R> item)
```
- **Description:** Inserts a function at a specific index.
- **Parameters:**
    - `index` — Position at which to insert.
    - `item` — Function to insert.

#### `Remove(Func<T, R> item)`
```csharp
public bool Remove(Func<T, R> item)
```
- **Description:** Removes the specified function.
- **Returns:** `bool` — `true` if the function was successfully removed.

#### `RemoveAt(int index)`
```csharp
public void RemoveAt(int index)
```
- **Description:** Removes the function at a specific index.
- **Parameter:** `index` — Position of the function to remove.

#### `GetEnumerator()`
```csharp
public IEnumerator<Func<T, R>> GetEnumerator()
```
- **Description:** Returns an enumerator for iterating the functions.
- **Returns:** `IEnumerator<Func<T, R>>` — Enumerator for the function members.
---
</details>


<details>
  <summary>
    <h2>🧩 IExpression&lt;T1, T2, R&gt;</h2>
    <br>Represents an expression with <b>two input parameters</b> of types <code>T1</code> and <code>T2</code> that aggregates multiple functions returning a value of type <code>R</code>
  </summary>

<br>

## IExpression&lt;T1, T2, R&gt;
```csharp
public interface IExpression<T1, T2, R> : IList<Func<T1, T2, R>>, IFunction<T1, T2, R>
```
- **Description:** Represents an expression with **two input parameters** of types `T1` and `T2` that aggregates multiple functions returning a value of type `R`.
- **Type Parameters:**
    - `T1` — The first input parameter type.
    - `T2` — The second input parameter type.
    - `R` — The return type of the expression.

### Properties
#### `Count`
```csharp
public int Count { get; }
```
- **Description:** Gets the number of functions in the expression.
- **Returns:** `int` — The number of function members.

#### `IsReadOnly`
```csharp
public bool IsReadOnly { get; }
```
- **Description:** Indicates whether the list of functions can be modified.
- **Returns:** `false`.

### Indexer
#### `this[int index]`
```csharp
public Func<T1, T2, R> this[int index] { get; set; }
```
- **Description:** Indexer to access a function at a specific position.
- **Parameter:** `index` — The position of the function.
- **Returns:** `Func<T1, T2, R>` — The function at the given index.

### Methods
#### `Invoke(T1 arg1, T2 arg2)`
```csharp
public R Invoke(T1 arg1, T2 arg2)
```
- **Description:** Evaluates all functions using the provided arguments and returns the aggregated result.
- **Parameters:**
    - `arg1` — The first input argument of type `T1`.
    - `arg2` — The second input argument of type `T2`.
- **Returns:** `R` — The aggregated result.

#### `Invoke(Enumerator enumerator, T1 arg1, T2 arg2)`
```csharp
protected abstract R Invoke(Enumerator enumerator, T1 arg1, T2 arg2)
```
- **Description:** Abstract template method. Derived classes define how the functions are aggregated.
- **Parameters:**
    - `enumerator` — Enumerator over the function members.
    - `arg1` — The first input argument.
    - `arg2` — The second input argument.
- **Returns:** `R` — The aggregated result.

#### `Add(Func<T1, T2, R> item)`
```csharp
public void Add(Func<T1, T2, R> item)
```
- **Description:** Adds a function to the expression.
- **Parameter:** `item` — The function to add.

#### `Clear()`
```csharp
public void Clear()
```
- **Description:** Removes all functions from the expression.

#### `Contains(Func<T1, T2, R> item)`
```csharp
public bool Contains(Func<T1, T2, R> item)
```
- **Description:** Checks if the function exists in the expression.
- **Returns:** `bool` — `true` if the function is present.

#### `CopyTo(Func<T1, T2, R>[] array, int arrayIndex)`
```csharp
public void CopyTo(Func<T1, T2, R>[] array, int arrayIndex)
```
- **Description:** Copies the functions to an array.
- **Parameters:**
    - `array` — Destination array.
    - `arrayIndex` — Starting index in the array.

#### `IndexOf(Func<T1, T2, R> item)`
```csharp
public int IndexOf(Func<T1, T2, R> item)
```
- **Description:** Gets the index of a function.
- **Returns:** `int` — The index of the function, or -1 if not found.

#### `Insert(int index, Func<T1, T2, R> item)`
```csharp
public void Insert(int index, Func<T1, T2, R> item)
```
- **Description:** Inserts a function at a specific index.
- **Parameters:**
    - `index` — Position at which to insert.
    - `item` — Function to insert.

#### `Remove(Func<T1, T2, R> item)`
```csharp
public bool Remove(Func<T1, T2, R> item)
```
- **Description:** Removes the specified function.
- **Returns:** `bool` — `true` if the function was successfully removed.

#### `RemoveAt(int index)`
```csharp
public void RemoveAt(int index)
```
- **Description:** Removes the function at a specific index.
- **Parameter:** `index` — Position of the function to remove.

#### `GetEnumerator()`
```csharp
public IEnumerator<Func<T1, T2, R>> GetEnumerator()
```
- **Description:** Returns an enumerator for iterating the functions.
- **Returns:** `IEnumerator<Func<T1, T2, R>>` — Enumerator for the function members.
---
<details>

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