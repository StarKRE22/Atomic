# 🧩 FloatSumExpression Classes

`FloatSumExpression` represents an expression that computes the **sum** of multiple float-returning functions. These classes extend from the [ExpressionBase](ExpressionBase.md) family.

---

<details>
 <summary>
 <h2>🧩 FloatSumExpression</h2>
 <br> Represents an expression that computes the sum of multiple <b>parameterless float-returning</b> functions
 </summary>

##
```csharp
public class FloatSumExpression : ExpressionBase<float>
```

### Constructors

#### `FloatSumExpression(int capacity)`
```csharp
public FloatSumExpression(int capacity)
```
- **Description:** Initializes a new empty instance of the `FloatSumExpression` class.
- **Parameter:** `capacity` — Initial capacity for the function list. Default is `4`.

#### `FloatSumExpression(params Func<float>[] members)`
```csharp
public FloatSumExpression(params Func<float>[] members)
```
- **Description:** Initializes the expression with an array of float-returning functions.
- **Parameter:** `members` — Array of `Func<float>` delegates.

#### `FloatSumExpression(IEnumerable<Func<float>> members)`
```csharp
public FloatSumExpression(IEnumerable<Func<float>> members)
```
- **Description:** Initializes the expression with a collection of float-returning functions.
- **Parameter:** `members` — Enumerable collection of `Func<float>` delegates.

### Events

#### `OnStateChanged`
```csharp
public event StateChangedHandler OnStateChanged;
```
- **Description:** Occurs when the state of the expression changes (e.g., when functions are added, removed, or the list is cleared).

#### `OnItemChanged`
```csharp
public event ChangeItemHandler<Func<float>> OnItemChanged;
```
- **Description:** Occurs when an existing function in the expression is replaced or modified.

#### `OnItemInserted`
```csharp
public event InsertItemHandler<Func<float>> OnItemInserted;
```
- **Description:** Occurs when a new function is inserted floato the expression at a specific position.

#### `OnItemDeleted`
```csharp
public event DeleteItemHandler<Func<float>> OnItemDeleted;
```
- **Description:** Occurs when a function is removed from the expression.

### Properties

#### `Value`
```csharp
public float Value { get; }
```
- **Description:** Evaluates all functions and returns the sum of their results.
- **Returns:** `float` — The computed sum.
- **Note:** — If no functions are present, returns `0` by default.

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
public Func<float> this[int index] { get; set; }
```
- **Description:** Indexer to access a function at a specific position.
- **Parameter:** `index` — The position of the function.
- **Returns:** `Func<float>` — The function at the given index.

### Methods

#### `Invoke()`
```csharp
public float Invoke()
```
- **Description:** Evaluates all function members of the expression and returns their sum.
- **Returns:** `float` — The computed sum.
- **Note:** Returns `0` if no functions are present.

#### `Add(Func<float> item)`
```csharp
public void Add(Func<float> item)
```
- **Description:** Adds a function to the expression.
- **Parameter:** `item` — The function to add.

#### `AddRange(IEnumerable<Func<float>> items)`
```csharp
public void AddRange(IEnumerable<Func<float>> items)
```
- **Description:** Adds multiple functions to the expression at once.
- **Parameter:** `items` — An enumerable collection of `Func<float>` delegates to add.
- **Throws:** `ArgumentNullException` if `items` is `null`.

#### `Clear()`
```csharp
public void Clear()
```
- **Description:** Removes all functions from the expression.

#### `Contains(Func<float> item)`
```csharp
public bool Contains(Func<float> item)
```
- **Description:** Checks if the specified function exists in the expression.
- **Parameter:** `item` — The function to check.
- **Returns:** `bool` — `true` if the function exists, otherwise `false`.

#### `CopyTo(Func<float>[] array, int arrayIndex)`
```csharp
public void CopyTo(Func<float>[] array, int arrayIndex)
```
- **Description:** Copies all functions in the expression to the specified array starting at the given index.
- **Parameters:**
- `array` — The destination array.
- `arrayIndex` — The starting index in the array.

#### `IndexOf(Func<float> item)`
```csharp
public float IndexOf(Func<float> item)
```
- **Description:** Returns the index of the specified function in the expression.
- **Parameter:** `item` — The function to locate.
- **Returns:** `float` — The index of the function, or `-1` if not found.

#### `Insert(int index, Func<float> item)`
```csharp
public void Insert(int index, Func<float> item)
```
- **Description:** Inserts a function at the specified index.
- **Parameters:**
- `index` — The position at which to insert.
- `item` — The function to insert.

#### `Remove(Func<float> item)`
```csharp
public bool Remove(Func<float> item)
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
public IEnumerator<Func<float>> GetEnumerator()
```
- **Description:** Returns an enumerator for iterating over all function members in the expression.
- **Returns:** `IEnumerator<Func<float>>` — Enumerator over the functions.

#### `Dispose()`
```csharp
public void Dispose()
```
- **Description:** Releases all resources used by the expression and clears its content.  
  Also unsubscribes all event handlers.
- **Effects:**
    - Clears the function list.
    - Sets `OnItemChanged`, `OnItemInserted`, `OnItemDeleted`, and `OnStateChanged` to `null`.

## 🗂 Example Usage
```csharp
var expression = new FloatSumExpression(
    () => 2.0f,
    () => 3.0f,
    () => 4.0f
);
float result = expression.Invoke(); // 2.0f + 3.0f + 4.0f = 9
```
</details>

---

<details>
 <summary>
 <h2>🧩 FloatSumExpression&lt;T&gt;</h2>
 <br> Represents an expression that computes the sum of float values returned from functions with a <b>single input parameter</b>
 </summary>

##
```csharp
public class FloatSumExpression<T> : ExpressionBase<T, float>
```
- **Type Parameter:** `T` — The input parameter type of the functions.

### Constructors

#### `FloatSumExpression()`
```csharp
public FloatSumExpression(int capacity)
```
- **Description:** Initializes a new empty instance of the `FloatSumExpression<T>` class.
- **Parameter:** `capacity` — Initial capacity for the function list. Default is `4`.

#### `FloatSumExpression(Func<T, float>[] members)`
```csharp
public FloatSumExpression(params Func<T, float>[] members)
```
- **Description:** Initializes the expression with an array of functions that take a `T` and return an float.
- **Parameter:** `members` — Array of `Func<T, float>` delegates.

#### `FloatSumExpression(IEnumerable<Func<T, float>> members)`
```csharp
public FloatSumExpression(IEnumerable<Func<T, float>> members)
```
- **Description:** Initializes the expression with a collection of functions that take a `T` and return an float.
- **Parameter:** `members` — Enumerable collection of `Func<T, float>` delegates.

### Events

#### `OnStateChanged`
```csharp
public event StateChangedHandler OnStateChanged;
```
- **Description:** Occurs when the state of the expression changes (e.g., when functions are added, removed, or the list is cleared).

#### `OnItemChanged`
```csharp
public event ChangeItemHandler<Func<T, float>> OnItemChanged;
```
- **Description:** Occurs when an existing function in the expression is replaced or modified.

#### `OnItemInserted`
```csharp
public event InsertItemHandler<Func<T, float>> OnItemInserted;
```
- **Description:** Occurs when a new function is inserted floato the expression at a specific position.

#### `OnItemDeleted`
```csharp
public event DeleteItemHandler<Func<T, float>> OnItemDeleted;
```
- **Description:** Occurs when a function is removed from the expression.

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
public Func<T, float> this[int index] { get; set; }
```
- **Description:** Indexer to access a function at a specific position.
- **Parameter:** `index` — The position of the function.
- **Returns:** `Func<T, float>` — The function at the given index.

### Methods

#### `Invoke(T arg)`
```csharp
public float Invoke(T arg)
```
- **Description:** Evaluates all function members of the expression with the provided argument and returns their sum.
- **Parameter:** `arg` — The input argument of type T.
- **Returns:** `float` — The computed sum.
- **Note:** Returns `0` if no functions are present.

#### `Add(Func<T, float> item)`
```csharp
public void Add(Func<T, float> item)
```
- **Description:** Adds a function to the expression.
- **Parameter:** `item` — The function to add.

#### `AddRange(IEnumerable<Func<T, float>> items)`
```csharp
public void AddRange(IEnumerable<Func<T, float>> items)
```
- **Description:** Adds multiple functions to the expression at once.
- **Parameter:** `items` — An enumerable collection of `Func<T, float>` delegates to add.
- **Throws:** `ArgumentNullException` if `items` is `null`.

#### `Clear()`
```csharp
public void Clear()
```
- **Description:** Removes all functions from the expression.

#### `Contains(Func<T, float> item)`
```csharp
public bool Contains(Func<T, float> item)
```
- **Description:** Checks if the specified function exists in the expression.
- **Parameter:** `item` — The function to check.
- **Returns:** `bool` — `true` if the function exists, otherwise `false`.

#### `CopyTo(Func<T, float>[] array, int arrayIndex)`
```csharp
public void CopyTo(Func<T, float>[] array, int arrayIndex)
```
- **Description:** Copies all functions in the expression to the specified array starting at the given index.
- **Parameters:**
    - `array` — The destination array.
    - `arrayIndex` — The starting index in the array.

#### `IndexOf(Func<T, float> item)`
```csharp
public float IndexOf(Func<T, float> item)
```
- **Description:** Returns the index of the specified function in the expression.
- **Parameter:** `item` — The function to locate.
- **Returns:** `float` — The index of the function, or `-1` if not found.

#### `Insert(int index, Func<T, float> item)`
```csharp
public void Insert(int index, Func<T, float> item)
```
- **Description:** Inserts a function at the specified index.
- **Parameters:**
    - `index` — The position at which to insert.
    - `item` — The function to insert.

#### `Remove(Func<T, float> item)`
```csharp
public bool Remove(Func<T, float> item)
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
public IEnumerator<Func<T, float>> GetEnumerator()
```
- **Description:** Returns an enumerator for iterating over all function members in the expression.
- **Returns:** `IEnumerator<Func<T, float>>` — Enumerator over the functions.

#### `Dispose()`
```csharp
public void Dispose()
```
- **Description:** Releases all resources used by the expression and clears its content.  
  Also unsubscribes all event handlers.
- **Effects:**
    - Clears the function list.
    - Sets `OnItemChanged`, `OnItemInserted`, `OnItemDeleted`, and `OnStateChanged` to `null`.

### 🗂 Example Usage
```csharp
var expression = new FloatSumExpression<float>(
    x => x,
    x => x + 0.5f
);
float result = expression.Invoke(3.5f); // 3.5f + (3.5f + 0.5f) = 7.5f
```
</details>

---

<details>
 <summary>
 <h2>🧩 FloatSumExpression&lt;T1, T2&gt;</h2>
 <br> Represents an expression that computes the sum of float values returned from functions with <b>two input parameters</b>
 </summary>

##
```csharp
public class FloatSumExpression<T1, T2> : ExpressionBase<T1, T2, float>
```
- **Type Parameters:**
- `T1` — The first input parameter type.
- `T2` — The second input parameter type.

### Constructors

#### `FloatSumExpression()`
```csharp
public FloatSumExpression(int capacity)
```
- **Description:** Initializes a new empty instance of the `FloatSumExpression<T1, T2>` class.
- **Parameter:** `capacity` — Initial capacity for the function list. Default is `4`.

#### `FloatSumExpression(Func<T1, T2, float>[] members)`
```csharp
public FloatSumExpression(params Func<T1, T2, float>[] members)
```
- **Description:** Initializes the expression with an array of functions that take two parameters and return an float.
- **Parameter:** `members` — Array of `Func<T1, T2, float>` delegates.

#### `FloatSumExpression(IEnumerable<Func<T1, T2, float>> members)`
```csharp
public FloatSumExpression(IEnumerable<Func<T1, T2, float>> members)
```
- **Description:** Initializes the expression with a collection of functions that take two parameters and return an float.
- **Parameter:** `members` — Enumerable collection of `Func<T1, T2, float>` delegates.

### Events

#### `OnStateChanged`
```csharp
public event StateChangedHandler OnStateChanged;
```
- **Description:** Occurs when the state of the expression changes.

#### `OnItemChanged`
```csharp
public event ChangeItemHandler<Func<T1, T2, float>> OnItemChanged;
```
- **Description:** Occurs when an existing function is replaced or modified.

#### `OnItemInserted`
```csharp
public event InsertItemHandler<Func<T1, T2, float>> OnItemInserted;
```
- **Description:** Occurs when a new function is inserted.

#### `OnItemDeleted`
```csharp
public event DeleteItemHandler<Func<T1, T2, float>> OnItemDeleted;
```
- **Description:** Occurs when a function is removed.

### Properties

#### `Count`
```csharp
public int Count { get; }
```
- **Description:** Gets the number of functions in the expression.
- **Returns:** `float` — Number of function members.

#### `IsReadOnly`
```csharp
public bool IsReadOnly { get; }
```
- **Description:** Indicates whether the list of functions can be modified.
- **Returns:** `false`.

### Indexers

#### `this[int index]`
```csharp
public Func<T1, T2, float> this[int index] { get; set; }
```
- **Description:** Accesses a function at a specific position.
- **Parameter:** `index` — Position of the function.
- **Returns:** `Func<T1, T2, float>` — Function at the given index.

### Methods

#### `Invoke(T1 arg1, T2 arg2)`
```csharp
public float Invoke(T1 arg1, T2 arg2)
```
- **Description:** Evaluates all functions with provided arguments.
- **Parameters:**
    - `arg1` — First input argument.
    - `arg2` — Second input argument.
- **Returns:** `float` — Computed sum.
- **Note:** -Returns `0` if no functions are present.

#### `Add(Func<T1, T2, float> item)`
```csharp
public void Add(Func<T1, T2, float> item)
```
- **Description:** Adds a function to the expression.
- **Parameter:** `item` — Function to add.

#### `AddRange(IEnumerable<Func<T1, T2, float>> items)`
```csharp
public void AddRange(IEnumerable<Func<T1, T2, float>> items)
```
- **Description:** Adds multiple functions.
- **Parameter:** `items` — Collection of functions.
- **Throws:** `ArgumentNullException` if `items` is null.

#### `Clear()`
```csharp
public void Clear()
```
- **Description:** Removes all functions.

#### `Contains(Func<T1, T2, float> item)`
```csharp
public bool Contains(Func<T1, T2, float> item)
```
- **Description:** Checks if a function exists.
- **Returns:** `bool` — True if found.

#### `CopyTo(Func<T1, T2, float>[] array, int arrayIndex)`
```csharp
public void CopyTo(Func<T1, T2, float>[] array, int arrayIndex)
```
- **Description:** Copies all functions to the specified array starting at the given index.
- **Parameters:**
    - `array` — Destination array.
    - `arrayIndex` — Starting index in the array.

#### `IndexOf(Func<T1, T2, float> item)`
```csharp
public float IndexOf(Func<T1, T2, float> item)
```
- **Description:** Returns the index of the specified function.
- **Parameter:** `item` — Function to locate.
- **Returns:** `float` — Index of the function, or `-1` if not found.

#### `Insert(int index, Func<T1, T2, float> item)`
```csharp
public void Insert(int index, Func<T1, T2, float> item)
```
- **Description:** Inserts a function at the specified index.
- **Parameters:**
    - `index` — Position to insert.
    - `item` — Function to insert.

#### `Remove(Func<T1, T2, float> item)`
```csharp
public bool Remove(Func<T1, T2, float> item)
```
- **Description:** Removes the specified function.
- **Parameter:** `item` — Function to remove.
- **Returns:** `bool` — True if removed successfully.

#### `RemoveAt(int index)`
```csharp
public void RemoveAt(int index)
```
- **Description:** Removes the function at the specified index.
- **Parameter:** `index` — Position of the function to remove.

#### `GetEnumerator()`
```csharp
public IEnumerator<Func<T1, T2, float>> GetEnumerator()
```
- **Description:** Returns an enumerator for iterating over functions.
- **Returns:** `IEnumerator<Func<T1, T2, float>>` — Enumerator over functions.

#### `Dispose()`
```csharp
public void Dispose()
```
- **Description:** Releases resources and clears content.
- **Effects:**
    - Clears the function list.
    - Sets event handlers to null.

### 🗂 Example Usage
```csharp
var expression = new FloatSumExpression<float, float>(
    (a, b) => a,
    (a, b) => b,
    (a, b) => a + b
);
float result = expression.Invoke(2, 3); // 2 + 3 + (2 + 3) = 10
```
</details>