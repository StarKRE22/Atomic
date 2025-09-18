# 🧩 IntMulExpression

Represents an expression that computes the **product** of multiple integer-returning functions. They extend from
the [ExpressionBase](ExpressionBase.md) family of classes.

---

<details>
 <summary>
 <h2>🧩 IntMulExpression</h2>
 <br> Represents an expression that computes the product of multiple <b>parameterless integer-returning</b> functions
 </summary>

<br>

```csharp
public class IntMulExpression : ExpressionBase<int>
```

---

### 🏗️ Constructors

#### `IntMulExpression(int)`

```csharp
public IntMulExpression(int capacity)
```

- **Description:** Initializes a new empty instance of the `IntMulExpression` class.
- **Parameter:** `capacity` — Initial capacity for the internal function list. Default is `4`.

#### `IntMulExpression(Func<int>[])`

```csharp
public IntMulExpression(params Func<int>[] members)
```

- **Description:** Initializes the expression with an array of integer-returning functions.
- **Parameter:** `members` — Array of `Func<int>` delegates.

#### `IntMulExpression(IEnumerable<Func<int>>)`

```csharp
public IntMulExpression(IEnumerable<Func<int>> members)
```

- **Description:** Initializes the expression with a collection of integer-returning functions.
- **Parameter:** `members` — Enumerable collection of `Func<int>` delegates.

---

### ⚡ Events

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;
```

- **Description:** Occurs when the state of the expression changes (e.g., when functions are added, removed, or the list
  is cleared).

#### `OnItemChanged`

```csharp
public event Action<int, Func<int>> OnItemChanged;
```

- **Description:** Occurs when an existing function in the expression is replaced or modified.

#### `OnItemInserted`

```csharp
public event Action<int, Func<int>> OnItemInserted;
```

- **Description:** Occurs when a new function is inserted into the expression at a specific position.

#### `OnItemDeleted`

```csharp
public event Action<int, Func<int>> OnItemDeleted;
```

- **Description:** Occurs when a function is removed from the expression.

---

### 🔑 Properties

#### `Value`

```csharp
public int Value { get; }
```

- **Description:** Evaluates all functions and returns the product of their results.
  If no functions are present, returns 1 by default.
- **Returns:** `int` — The computed product.

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

---

### 🏷️ Indexers

#### `[int index]`

```csharp
public Func<int> this[int index] { get; set; }
```

- **Description:** Indexer to access a function at a specific position.
- **Parameter:** `index` — The position of the function.
- **Returns:** `Func<int>` — The function at the given index.

---

### 🏹 Methods

#### `Invoke()`

```csharp
public int Invoke()
```

- **Description:** Evaluates all function members of the expression and returns their product.
- **Returns:** `int` — The computed product.
- **Note:** -Returns `1` if no functions are present.

#### `Add(Func<int>)`

```csharp
public void Add(Func<int> item)
```

- **Description:** Adds a function to the expression.
- **Parameter:** `item` — The function to add.

#### `AddRange(IEnumerable<Func<int>>)`

```csharp
public void AddRange(IEnumerable<Func<int>> items)
```

- **Description:** Adds multiple functions to the expression at once.
- **Parameter:** `items` — An enumerable collection of `Func<int>` delegates to add.
- **Throws:** `ArgumentNullException` if `items` is `null`.

#### `Clear()`

```csharp
public void Clear()
```

- **Description:** Removes all functions from the expression.

#### `Contains(Func<int>)`

```csharp
public bool Contains(Func<int> item)
```

- **Description:** Checks if the specified function exists in the expression.
- **Parameter:** `item` — The function to check.
- **Returns:** `bool` — `true` if the function exists, otherwise `false`.

#### `CopyTo(Func<int>[], int)`

```csharp
public void CopyTo(Func<int>[] array, int arrayIndex)
```

- **Description:** Copies all functions in the expression to the specified array starting at the given index.
- **Parameters:**
- `array` — The destination array.
- `arrayIndex` — The starting index in the array.

#### `IndexOf(Func<int>)`

```csharp
public int IndexOf(Func<int> item)
```

- **Description:** Returns the index of the specified function in the expression.
- **Parameter:** `item` — The function to locate.
- **Returns:** `int` — The index of the function, or `-1` if not found.

#### `Insert(int, Func<int>)`

```csharp
public void Insert(int index, Func<int> item)
```

- **Description:** Inserts a function at the specified index.
- **Parameters:**
- `index` — The position at which to insert.
- `item` — The function to insert.

#### `Remove(Func<int>)`

```csharp
public bool Remove(Func<int> item)
```

- **Description:** Removes the specified function from the expression.
- **Parameter:** `item` — The function to remove.
- **Returns:** `bool` — `true` if removed successfully, otherwise `false`.

#### `RemoveAt(int)`

```csharp
public void RemoveAt(int index)
```

- **Description:** Removes the function at the specified index.
- **Parameter:** `index` — The position of the function to remove.

#### `GetEnumerator()`

```csharp
public IEnumerator<Func<int>> GetEnumerator()
```

- **Description:** Returns an enumerator for iterating over all function members in the expression.
- **Returns:** `IEnumerator<Func<int>>` — Enumerator over the functions.

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Releases all resources used by the expression and clears its content.  
  Also unsubscribes all event handlers.
- **Effects:**
    - Clears the function list.
    - Sets `OnItemChanged`, `OnItemInserted`, `OnItemDeleted`, and `OnStateChanged` to `null`.

---

### 🗂 Example Usage

```csharp
// Parameterless
var multiply = new IntMulExpression(
    () => 2,
    () => 3,
    () => 4
);
int result = multiply.Invoke(); // 2 * 3 * 4 = 24
```

</details>

---

<details>
 <summary>
 <h2>🧩 IntMulExpression&lt;T&gt;</h2>
 <br> Represents an expression that computes the product of integer values returned from functions with a <b>single input parameter</b>
 </summary>

<br>

```csharp
public class IntMulExpression<T> : ExpressionBase<T, int>
```

- **Type Parameter:** `T` — The input parameter type of the functions.

### 🏗️ Constructors

---

#### `IntMulExpression(int)`

```csharp
public IntMulExpression(int capacity)
```

- **Description:** Initializes a new empty instance of the `IntMulExpression<T>` class.
- **Parameter:** `capacity` — Initial capacity for the internal function list. Default is `4`.

#### `IntMulExpression(Func<T, int>[])`

```csharp
public IntMulExpression(params Func<T, int>[] members)
```

- **Description:** Initializes the expression with an array of functions that take a `T` and return an integer.
- **Parameter:** `members` — Array of `Func<T, int>` delegates.

#### `IntMulExpression(IEnumerable<Func<T, int>>)`

```csharp
public IntMulExpression(IEnumerable<Func<T, int>> members)
```

- **Description:** Initializes the expression with a collection of functions that take a `T` and return an integer.
- **Parameter:** `members` — Enumerable collection of `Func<T, int>` delegates.

---

### ⚡ Events

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;
```

- **Description:** Occurs when the state of the expression changes (e.g., when functions are added, removed, or the list
  is cleared).

#### `OnItemChanged`

```csharp
public event Action<int, Func<T, int>> OnItemChanged;
```

- **Description:** Occurs when an existing function in the expression is replaced or modified.

#### `OnItemInserted`

```csharp
public event Action<int, Func<T, int>> OnItemInserted;
```

- **Description:** Occurs when a new function is inserted into the expression at a specific position.

#### `OnItemDeleted`

```csharp
public event Action<int, Func<T, int>> OnItemDeleted;
```

- **Description:** Occurs when a function is removed from the expression.

---

### 🔑 Properties

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

---

### 🏷️ Indexers

#### `[int index]`

```csharp
public Func<T, int> this[int index] { get; set; }
```

- **Description:** Indexer to access a function at a specific position.
- **Parameter:** `index` — The position of the function.
- **Returns:** `Func<T, int>` — The function at the given index.

---

### 🏹 Methods

#### `Invoke(T arg)`

```csharp
public int Invoke(T arg)
```

- **Description:** Evaluates all function members of the expression with the provided argument and returns their
  product.
- **Parameter:** `arg` — The input argument of type T.
- **Returns:** `int` — The computed product.
- **Note:** -Returns `1` if no functions are present.

#### `Add(Func<T, int>)`

```csharp
public void Add(Func<T, int> item)
```

- **Description:** Adds a function to the expression.
- **Parameter:** `item` — The function to add.

#### `AddRange(IEnumerable<Func<T, int>>)`

```csharp
public void AddRange(IEnumerable<Func<T, int>> items)
```

- **Description:** Adds multiple functions to the expression at once.
- **Parameter:** `items` — An enumerable collection of `Func<T, int>` delegates to add.
- **Throws:** `ArgumentNullException` if `items` is `null`.

#### `Clear()`

```csharp
public void Clear()
```

- **Description:** Removes all functions from the expression.

#### `Contains(Func<T, int>)`

```csharp
public bool Contains(Func<T, int> item)
```

- **Description:** Checks if the specified function exists in the expression.
- **Parameter:** `item` — The function to check.
- **Returns:** `bool` — `true` if the function exists, otherwise `false`.

#### `CopyTo(Func<T, int>[], int)`

```csharp
public void CopyTo(Func<T, int>[] array, int arrayIndex)
```

- **Description:** Copies all functions in the expression to the specified array starting at the given index.
- **Parameters:**
    - `array` — The destination array.
    - `arrayIndex` — The starting index in the array.

#### `IndexOf(Func<T, int>)`

```csharp
public int IndexOf(Func<T, int> item)
```

- **Description:** Returns the index of the specified function in the expression.
- **Parameter:** `item` — The function to locate.
- **Returns:** `int` — The index of the function, or `-1` if not found.

#### `Insert(int, Func<T, int>)`

```csharp
public void Insert(int index, Func<T, int> item)
```

- **Description:** Inserts a function at the specified index.
- **Parameters:**
    - `index` — The position at which to insert.
    - `item` — The function to insert.

#### `Remove(Func<T, int>)`

```csharp
public bool Remove(Func<T, int> item)
```

- **Description:** Removes the specified function from the expression.
- **Parameter:** `item` — The function to remove.
- **Returns:** `bool` — `true` if removed successfully, otherwise `false`.

#### `RemoveAt(int)`

```csharp
public void RemoveAt(int index)
```

- **Description:** Removes the function at the specified index.
- **Parameter:** `index` — The position of the function to remove.

#### `GetEnumerator()`

```csharp
public IEnumerator<Func<T, int>> GetEnumerator()
```

- **Description:** Returns an enumerator for iterating over all function members in the expression.
- **Returns:** `IEnumerator<Func<T, int>>` — Enumerator over the functions.

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

---

```csharp

// Single-parameter
var expression = new IntMulExpression<int>(
    x => x,
    x => x + 1
);
int result = expression.Invoke(3); // 3 * (3 + 1) = 12
```

</details>

---

<details>
 <summary>
 <h2>🧩 IntMulExpression&lt;T1, T2&gt;</h2>
 <br> Represents an expression that computes the product of integer values returned from functions with <b>two input parameters</b>
 </summary>

<br>

```csharp
public class IntMulExpression<T1, T2> : ExpressionBase<T1, T2, int>
```

- **Type Parameters:**
- `T1` — The first input parameter type.
- `T2` — The second input parameter type.

---

### 🏗️ Constructors

#### `IntMulExpression()`

```csharp
public IntMulExpression(int capacity)
```

- **Description:** Initializes a new empty instance of the `IntMulExpression<T1, T2>` class.
- **Parameter:** `capacity` — Initial capacity for the internal function list. Default is `4`.

#### `IntMulExpression(Func<T1, T2, int>[] members)`

```csharp
public IntMulExpression(params Func<T1, T2, int>[] members)
```

- **Description:** Initializes the expression with an array of functions that take two parameters and return an integer.
- **Parameter:** `members` — Array of `Func<T1, T2, int>` delegates.

#### `IntMulExpression(IEnumerable<Func<T1, T2, int>> members)`

```csharp
public IntMulExpression(IEnumerable<Func<T1, T2, int>> members)
```

- **Description:** Initializes the expression with a collection of functions that take two parameters and return an
  integer.
- **Parameter:** `members` — Enumerable collection of `Func<T1, T2, int>` delegates.

---

### ⚡ Events

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;
```

- **Description:** Occurs when the state of the expression changes.

#### `OnItemChanged`

```csharp
public event Action<int, Func<T1, T2, int>> OnItemChanged;
```

- **Description:** Occurs when an existing function is replaced or modified.

#### `OnItemInserted`

```csharp
public event Action<int, Func<T1, T2, int>> OnItemInserted;
```

- **Description:** Occurs when a new function is inserted.

#### `OnItemDeleted`

```csharp
public event Action<int, Func<T1, T2, int>> OnItemDeleted;
```

- **Description:** Occurs when a function is removed.

---

### 🔑 Properties

### `Count`

```csharp
public int Count { get; }
```

- **Description:** Gets the number of functions in the expression.
- **Returns:** `int` — Number of function members.

### `IsReadOnly`

```csharp
public bool IsReadOnly { get; }
```

- **Description:** Indicates whether the list of functions can be modified.
- **Returns:** `false`.

---

### 🏷️ Indexers

#### `[int index]`

```csharp
public Func<T1, T2, int> this[int index] { get; set; }
```

- **Description:** Accesses a function at a specific position.
- **Parameter:** `index` — Position of the function.
- **Returns:** `Func<T1, T2, int>` — Function at the given index.

---

### 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
public int Invoke(T1 arg1, T2 arg2)
```

- **Description:** Evaluates all functions with provided arguments.
- **Parameters:**
    - `arg1` — First input argument.
    - `arg2` — Second input argument.
- **Returns:** `int` — Computed product.
- **Note:** -Returns `1` if no functions are present.

#### `Add(Func<T1, T2, int>)`

```csharp
public void Add(Func<T1, T2, int> item)
```

- **Description:** Adds a function to the expression.
- **Parameter:** `item` — Function to add.

#### `AddRange(IEnumerable<Func<T1, T2, int>>)`

```csharp
public void AddRange(IEnumerable<Func<T1, T2, int>> items)
```

- **Description:** Adds multiple functions.
- **Parameter:** `items` — Collection of functions.
- **Throws:** `ArgumentNullException` if `items` is null.

#### `Clear()`

```csharp
public void Clear()
```

- **Description:** Removes all functions.

#### `Contains(Func<T1, T2, int>)`

```csharp
public bool Contains(Func<T1, T2, int> item)
```

- **Description:** Checks if a function exists.
- **Returns:** `bool` — True if found.

#### `CopyTo(Func<T1, T2, int>[], int)`

```csharp
public void CopyTo(Func<T1, T2, int>[] array, int arrayIndex)
```

- **Description:** Copies all functions to the specified array starting at the given index.
- **Parameters:**
    - `array` — Destination array.
    - `arrayIndex` — Starting index in the array.

#### `IndexOf(Func<T1, T2, int>)`

```csharp
public int IndexOf(Func<T1, T2, int> item)
```

- **Description:** Returns the index of the specified function.
- **Parameter:** `item` — Function to locate.
- **Returns:** `int` — Index of the function, or `-1` if not found.

#### `Insert(int, Func<T1, T2, int>)`

```csharp
public void Insert(int index, Func<T1, T2, int> item)
```

- **Description:** Inserts a function at the specified index.
- **Parameters:**
    - `index` — Position to insert.
    - `item` — Function to insert.

#### `Remove(Func<T1, T2, int>)`

```csharp
public bool Remove(Func<T1, T2, int> item)
```

- **Description:** Removes the specified function.
- **Parameter:** `item` — Function to remove.
- **Returns:** `bool` — True if removed successfully.

#### `RemoveAt(int)`

```csharp
public void RemoveAt(int index)
```

- **Description:** Removes the function at the specified index.
- **Parameter:** `index` — Position of the function to remove.

#### `GetEnumerator()`

```csharp
public IEnumerator<Func<T1, T2, int>> GetEnumerator()
```

- **Description:** Returns an enumerator for iterating over functions.
- **Returns:** `IEnumerator<Func<T1, T2, int>>` — Enumerator over functions.

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Releases resources and clears content.
- **Effects:**
    - Clears the function list.
    - Sets event handlers to null.

---

### 🗂 Example Usage

```csharp
var expression = new IntMulExpression<int, int>(
    (a, b) => a,
    (a, b) => b,
    (a, b) => a + b
);
int result = expression.Invoke(2, 3); // 2 * 3 * (2 + 3) = 30
```

</details>