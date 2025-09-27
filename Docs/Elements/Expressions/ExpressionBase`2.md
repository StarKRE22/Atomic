# 🧩 ExpressionBase&lt;T1, T2, R&gt;

```csharp
[Serializable]
public abstract class ExpressionBase<T1, T2, R> : ReactiveLinkedList<Func<T1, T2, R>>, IExpression<T1, T2, R>
```

- **Description:** Represents an expression with <b>two input parameters</b> of types <code>T1</code> and 
  <code>T2</code> that aggregates multiple functions returning a value of type <code>R</code>
- **Inheritance:** [ReactiveLinkedList&lt;T&gt;](../Collections/ReactiveLinkedList.md),
  [IExpression&lt;T1, T2, R&gt;](IExpression%602.md)
- **Type Parameters:**
    - `T1` — The first input parameter type.
    - `T2` — The second input parameter type.
    - `R` — The return type of the expression.
- **Note:** Supports Odin Inspector

---

## 🏗️ Constructors

#### `ExpressionBase(int)`

```csharp
protected ExpressionBase(int capacity)
```

- **Description:** Initializes an empty expression with the given capacity.
- **Parameter:** `capacity` — Initial capacity for the internal function list. Default value is `4`.

#### `ExpressionBase(params Func<T1, T2, R>[])`

```csharp
protected ExpressionBase(params Func<T1, T2, R>[] members)
```

- **Description:** Initializes the expression with an array of function members.
- **Parameter:** `members` — Array of functions to include in the expression.

#### `ExpressionBase(IEnumerable<Func<T1, T2, R>>)`

```csharp
protected ExpressionBase(IEnumerable<Func<T1, T2, R>> members)
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
public event Action<int, Func<T1, T2, R>> OnItemChanged;
```

- **Description:** Occurs when an existing function delegate in the expression is replaced or modified.

#### `OnItemInserted`

```csharp
public event Action<int, Func<T1, T2, R>> OnItemInserted;
```

- **Description:** Occurs when a new function delegate is inserted into the expression at a specific position.

#### `OnItemDeleted`

```csharp
public event Action<int, Func<T1, T2, R>> OnItemDeleted;
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
public Func<T1, T2, R> this[int index] { get; set; }
```

- **Description:** Indexer to access a function at a specific position.
- **Parameter:** `index` — The position of the function.
- **Returns:** `Func<T1, T2, R>` — The function at the given index.

---

## 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
public R Invoke(T1 arg1, T2 arg2)
```

- **Description:** Evaluates all functions using the provided arguments and returns the aggregated result.
- **Parameters:**
    - `arg1` — The first input argument of type `T1`.
    - `arg2` — The second input argument of type `T2`.
- **Returns:** `R` — The aggregated result.

#### `Invoke(Enumerator, T1, T2)`

```csharp
protected abstract R Invoke(Enumerator enumerator, T1 arg1, T2 arg2)
```

- **Description:** Abstract template method. Derived classes define how the functions are aggregated.
- **Parameters:**
    - `enumerator` — Enumerator over the function members.
    - `arg1` — The first input argument.
    - `arg2` — The second input argument.
- **Returns:** `R` — The aggregated result.

#### `Add(Func<T1, T2, R>)`

```csharp
public void Add(Func<T1, T2, R> item)
```

- **Description:** Adds a function to the expression.
- **Parameter:** `item` — The function to add.

#### `AddRange(IEnumerable<Func<T1, T2, R>>)`

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

#### `Contains(Func<T1, T2, R>)`

```csharp
public bool Contains(Func<T1, T2, R> item)
```

- **Description:** Checks if the function exists in the expression.
- **Returns:** `bool` — `true` if the function is present.

#### `CopyTo(Func<T1, T2, R>[], int)`

```csharp
public void CopyTo(Func<T1, T2, R>[] array, int arrayIndex)
```

- **Description:** Copies the functions to an array.
- **Parameters:**
    - `array` — Destination array.
    - `arrayIndex` — Starting index in the array.

#### `IndexOf(Func<T1, T2, R>)`

```csharp
public int IndexOf(Func<T1, T2, R> item)
```

- **Description:** Gets the index of a function.
- **Returns:** `int` — The index of the function, or -1 if not found.

#### `Insert(int, Func<T1, T2, R>)`

```csharp
public void Insert(int index, Func<T1, T2, R> item)
```

- **Description:** Inserts a function at a specific index.
- **Parameters:**
    - `index` — Position at which to insert.
    - `item` — Function to insert.

#### `Remove(Func<T1, T2, R>)`

```csharp
public bool Remove(Func<T1, T2, R> item)
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