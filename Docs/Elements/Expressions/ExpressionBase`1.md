# 🧩 ExpressionBase&lt;T, R&gt;

```csharp
public abstract class ExpressionBase<T, R> : ReactiveLinkedList<Func<T, R>>, IExpression<T, R>
```

- **Description:** Represents an expression with a <b>single input parameter</b> of type <code>T</code> that aggregates
  multiple functions returning a value of type <code>R</code>
- **Inheritance:** [ReactiveLinkedList&lt;T&gt;](../Collections/ReactiveLinkedList.md),
  [IExpression&lt;T, R&gt;](IExpression.md)
- **Type Parameters:**
    - `T` - The input parameter type of the functions.
    - `R` - The return type of the expression.
- **Note:** Supports Odin Inspector

---

## 🏗️ Constructors

#### `ExpressionBase(int)`

```csharp
protected ExpressionBase(int capacity)
```

- **Description:** Initializes an empty expression with the given capacity.
- **Parameter:** `capacity` — Initial capacity for the internal function list. Default value is `4`.

#### `ExpressionBase(params Func<T, R>[])`

```csharp
protected ExpressionBase(params Func<T, R>[] members)
```

- **Description:** Initializes the expression with an array of function members.
- **Parameter:** `members` — Array of functions to include in the expression.

#### `ExpressionBase(IEnumerable<Func<T, R>>)`

```csharp
protected ExpressionBase(IEnumerable<Func<T, R>> members)
```

- **Description:** Initializes the expression with an enumerable of function members.
- **Parameter:** `members` — Enumerable collection of functions.

---

## ⚡ Events

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;
```

- **Description:** Occurs when the state of the expression changes (e.g., when items are added, removed, or the list is
  cleared).

#### `OnItemChanged`

```csharp
public event Action<int, Func<T, R>> OnItemChanged;
```

- **Description:** Occurs when an existing function delegate in the expression is replaced or modified.

#### `OnItemInserted`

```csharp
public event Action<int, Func<T, R>> OnItemInserted;
```

- **Description:** Occurs when a new function delegate is inserted into the expression at a specific position.

#### `OnItemDeleted`

```csharp
public event Action<int, Func<T, R>> OnItemDeleted;
```

- **Description:** Occurs when a function delegate is removed from the expression.

---

## 🔑 Properties

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

## 🏷️ Indexers

#### `[int index]`

```csharp
public Func<T, R> this[int index] { get; set; }
```

- **Description:** Indexer to access a function at a specific position.
- **Parameter:** `index` — The position of the function.
- **Returns:** `Func<T, R>` — The function at the given index.

---

## 🏹 Methods

#### `Invoke(T)`

```csharp
public R Invoke(T arg)
```

- **Description:** Evaluates all functions using the provided argument and returns the aggregated result.
- **Parameter:** `arg` — The input argument for the functions.
- **Returns:** `R` — The aggregated result.

#### `Invoke(Enumerator, T)`

```csharp
protected abstract R Invoke(Enumerator enumerator, T arg)
```

- **Description:** Abstract template method. Derived classes define how the functions are aggregated.
- **Parameters:**
    - `enumerator` — Enumerator over the function members.
    - `arg` — The input argument of type `T`.
- **Returns:** `R` — The aggregated result.

#### `Add(Func<T, R>)`

```csharp
public void Add(Func<T, R> item)
```

- **Description:** Adds a function to the expression.
- **Parameter:** `item` — The function to add.

#### `AddRange(IEnumerable<Func<T, R>>)`

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

#### `Contains(Func<T, R>)`

```csharp
public bool Contains(Func<T, R> item)
```

- **Description:** Checks if the function exists in the expression.
- **Returns:** `bool` — `true` if the function is present.

#### `CopyTo(Func<T, R>[], int)`

```csharp
public void CopyTo(Func<T, R>[] array, int arrayIndex)
```

- **Description:** Copies the functions to an array.
- **Parameters:**
    - `array` — Destination array.
    - `arrayIndex` — Starting index in the array.

#### `IndexOf(Func<T, R>)`

```csharp
public int IndexOf(Func<T, R> item)
```

- **Description:** Gets the index of a function.
- **Returns:** `int` — The index of the function, or -1 if not found.

#### `Insert(int, Func<T, R>)`

```csharp
public void Insert(int index, Func<T, R> item)
```

- **Description:** Inserts a function at a specific index.
- **Parameters:**
    - `index` — Position at which to insert.
    - `item` — Function to insert.

#### `Remove(Func<T, R>)`

```csharp
public bool Remove(Func<T, R> item)
```

- **Description:** Removes the specified function.
- **Returns:** `bool` — `true` if the function was successfully removed.

#### `RemoveAt(int)`

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