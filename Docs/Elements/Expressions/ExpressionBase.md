# 🧩 ExpressionBase Classes

The **ExpressionBase** classes provide **base implementations** of the [IExpression](IExpression.md) interfaces extending from [ReactiveLinkedList&lt;R&gt;](../Collections/ReactiveLinkedList.md). They allow **aggregating multiple function members** and provide **dynamic evaluation** based on parameterless or parameterized functions.

> [!NOTE] 
> The expressions are ideal for scenarios where multiple factors or rules must be evaluated dynamically, such as game calculations, modifiers, or conditions.

---

<details>
  <summary>
    <h2>🧩 ExpressionBase&lt;R&gt;</h2>
    <br> Represents a <b>parameterless expression</b> aggregating multiple functions returning a value of type <code>R</code>
  </summary>

<br>

```csharp
public abstract class ExpressionBase<R> : ReactiveLinkedList<Func<R>>, IExpression<R>
```
- **Type parameter**: `R` — The return type of the expression.

### Constructors

#### `ExpressionBase(int capacity)`
```csharp
public ExpressionBase(int capacity)
```
- **Description:** Initializes a new empty expression with the specified capacity.
- **Parameter:** `capacity` — initial capacity of the underlying list. Default value is `4`.

#### `ExpressionBase(params Func<R>[] members)`
```csharp
public ExpressionBase(params Func<R>[] members)
```
- **Description:** Initializes a new expression containing the specified function members.
- **Parameter:** `members` — array of function delegates to add to the expression.
- **Throws:** `ArgumentNullException` if `members` is null.

#### `ExpressionBase(IEnumerable<Func<R>> members)`
```csharp
public ExpressionBase(IEnumerable<Func<R>> members)
```
- **Description:** Initializes a new expression containing function members from the provided enumerable.
- **Parameter:** `members` — enumerable of function delegates to add to the expression.
- **Throws:** `ArgumentNullException` if `members` is null.

### Events

#### `OnStateChanged`
```csharp
public event StateChangedHandler OnStateChanged;
```
- **Description:** Occurs when the state of the expression changes (e.g., when items are added, removed, or the list is cleared).

#### `OnItemChanged`
```csharp
public event ChangeItemHandler<Func<R>> OnItemChanged;
```
- **Description:** Occurs when an existing function delegate in the expression is replaced or modified.

#### `OnItemInserted`
```csharp
public event InsertItemHandler<Func<R>> OnItemInserted;
```
- **Description:** Occurs when a new function delegate is inserted into the expression at a specific position.

#### `OnItemDeleted`
```csharp
public event DeleteItemHandler<Func<R>> OnItemDeleted;
```
- **Description:** Occurs when a function delegate is removed from the expression.

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

#### `AddRange(IEnumerable<Func<R>> items)`
```csharp
public void AddRange(IEnumerable<Func<R>> items)
```
- **Description:** Adds multiple functions to the expression at once.
- **Parameter:** `items` — An enumerable collection of `Func<R>` delegates to add.
- **Throws:** `ArgumentNullException` if `items` is `null`.

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

#### `Dispose()`
```csharp
public void Dispose()
```
- **Description:** Releases all resources used by the expression and clears its content. Also unsubscribes all event handlers.
- **Effects:**
  - Clears the function list.
  - Sets `OnItemChanged`, `OnItemInserted`, `OnItemDeleted`, and `OnStateChanged` to `null`.

</details>

---

<details>
  <summary>
    <h2>🧩 ExpressionBase&lt;T, R&gt;</h2>
    <br>Represents an expression with a <b>single input parameter</b> of type <code>T</code> that aggregates multiple functions returning a value of type <code>R</code>>
  </summary>

<br>

```csharp
public abstract class ExpressionBase<T, R> : ReactiveLinkedList<Func<T, R>>, IExpression<T, R>
```
- **Type Parameters:**
  - `T` - The input parameter type of the functions.
  - `R` - The return type of the expression.

### Constructors

#### `ExpressionBase(int capacity)`
```csharp
protected ExpressionBase(int capacity)
```
- **Description:** Initializes an empty expression with the given capacity.
- **Parameter:** `capacity` — Initial capacity for the internal function list. Default value is `4`.

#### `ExpressionBase(params Func<T, R>[] members)`
```csharp
protected ExpressionBase(params Func<T, R>[] members)
```
- **Description:** Initializes the expression with an array of function members.
- **Parameter:** `members` — Array of functions to include in the expression.

#### `ExpressionBase(IEnumerable<Func<T, R>> members)`
```csharp
protected ExpressionBase(IEnumerable<Func<T, R>> members)
```
- **Description:** Initializes the expression with an enumerable of function members.
- **Parameter:** `members` — Enumerable collection of functions.

### Events
#### `OnStateChanged`
```csharp
public event StateChangedHandler OnStateChanged;
```
- **Description:** Occurs when the state of the expression changes (e.g., when items are added, removed, or the list is cleared).

#### `OnItemChanged`
```csharp
public event ChangeItemHandler<Func<T, R>> OnItemChanged;
```
- **Description:** Occurs when an existing function delegate in the expression is replaced or modified.

#### `OnItemInserted`
```csharp
public event InsertItemHandler<Func<T, R>> OnItemInserted;
```
- **Description:** Occurs when a new function delegate is inserted into the expression at a specific position.

#### `OnItemDeleted`
```csharp
public event DeleteItemHandler<Func<T, R>> OnItemDeleted;
```
- **Description:** Occurs when a function delegate is removed from the expression.

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

#### `AddRange(IEnumerable<Func<T, R>> items)`
```csharp
public void AddRange(IEnumerable<Func<T, R>> items)
```
- **Description:** Adds multiple functions to the expression at once.
- **Parameter:** `items` — An enumerable collection of `Func<T, R>` delegates to add.
- **Throws:** `ArgumentNullException` if `items` is `null`.

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

#### `Dispose()`
```csharp
public void Dispose()
```
- **Description:** Releases all resources used by the expression and clears its content.  
  Also unsubscribes all event handlers.
- **Effects:**
  - Clears the function list.
  - Sets `OnItemChanged`, `OnItemInserted`, `OnItemDeleted`, and `OnStateChanged` to `null`.
</details>

---

<details>
  <summary>
    <h2>🧩 ExpressionBase&lt;T1, T2, R&gt;</h2>
    <br>Represents an expression with <b>two input parameters</b> of types <code>T1</code> and <code>T2</code> that aggregates multiple functions returning a value of type <code>R</code>
  </summary>

<br>

```csharp
public abstract class ExpressionBase<T1, T2, R> : ReactiveLinkedList<Func<T1, T2, R>>, IExpression<T1, T2, R>
```
- **Type Parameters:**
  - `T1` — The first input parameter type.
  - `T2` — The second input parameter type.
  - `R` — The return type of the expression.

### Constructors

#### `ExpressionBase(int capacity)`
```csharp
protected ExpressionBase(int capacity)
```
- **Description:** Initializes an empty expression with the given capacity.
- **Parameter:** `capacity` — Initial capacity for the internal function list. Default value is `4`.

#### `ExpressionBase(params Func<T1, T2, R>[] members)`
```csharp
protected ExpressionBase(params Func<T1, T2, R>[] members)
```
- **Description:** Initializes the expression with an array of function members.
- **Parameter:** `members` — Array of functions to include in the expression.

#### `ExpressionBase(IEnumerable<Func<T1, T2, R>> members)`
```csharp
protected ExpressionBase(IEnumerable<Func<T1, T2, R>> members)
```
- **Description:** Initializes the expression with an enumerable of function members.
- **Parameter:** `members` — Enumerable collection of functions.

### Events
#### `OnStateChanged`
```csharp
public event StateChangedHandler OnStateChanged;
```
- **Description:** Occurs when the state of the expression changes (e.g., when items are added, removed, or the list is cleared).

#### `OnItemChanged`
```csharp
public event ChangeItemHandler<Func<T1, T2, R>> OnItemChanged;
```
- **Description:** Occurs when an existing function delegate in the expression is replaced or modified.

#### `OnItemInserted`
```csharp
public event InsertItemHandler<Func<T1, T2, R>> OnItemInserted;
```
- **Description:** Occurs when a new function delegate is inserted into the expression at a specific position.

#### `OnItemDeleted`
```csharp
public event DeleteItemHandler<Func<T1, T2, R>> OnItemDeleted;
```
- **Description:** Occurs when a function delegate is removed from the expression.

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

#### `AddRange(IEnumerable<Func<T1, T2, R>> items)`
```csharp
public void AddRange(IEnumerable<Func<T1, T2, R>> items)
```
- **Description:** Adds multiple functions to the expression at once.
- **Parameter:** `items` — An enumerable collection of `Func<T1, T2, R>` delegates to add.
- **Throws:** `ArgumentNullException` if `items` is `null`.

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

#### `Dispose()`
```csharp
public void Dispose()
```
- **Description:** Releases all resources used by the expression and clears its content.  
  Also unsubscribes all event handlers.
- **Effects:**
  - Clears the function list.
  - Sets `OnItemChanged`, `OnItemInserted`, `OnItemDeleted`, and `OnStateChanged` to `null`.
</details>

---

## 🗂 Example of Usage
Below is an example of using `ExpressionBase` to extend a simple **logical AND** expression with multiple parameterless boolean functions.

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

## 📝 Notes

Expressions are particularly useful for dynamic runtime calculations, such as:

- Applying **speed multipliers** from various sources (buffs, debuffs, environmental effects).
- Adding or removing conditions like **frozen state**, **boosts**, or other temporary effects.
- Combining multiple **dynamic factors** to calculate a final value on the fly.

This makes `IExpression` a flexible, runtime-adjustable function container suitable for game logic or any system
requiring composable dynamic calculations.