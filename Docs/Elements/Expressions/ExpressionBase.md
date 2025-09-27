# 🧩 ExpressionBase&lt;R&gt;

```csharp
[Serializable]
public abstract class ExpressionBase<R> : ReactiveLinkedList<Func<R>>, IExpression<R>
```

- **Description:** Represents a <b>parameterless expression</b> aggregating multiple functions returning a value of
  type <code>R</code>
- **Inheritance:** [ReactiveLinkedList&lt;T&gt;](../Collections/ReactiveLinkedList.md),
  [IExpression&lt;R&gt;](IExpression.md)
- **Type parameter**: `R` — The return type of the expression.
- **Note:** Supports Odin Inspector

---

## 🏗️ Constructors

#### `ExpressionBase(int)`

```csharp
public ExpressionBase(int capacity)
```

- **Description:** Initializes a new empty expression with the specified capacity.
- **Parameter:** `capacity` — initial capacity of the underlying list. Default value is `4`.

#### `ExpressionBase(params Func<R>[])`

```csharp
public ExpressionBase(params Func<R>[] members)
```

- **Description:** Initializes a new expression containing the specified function members.
- **Parameter:** `members` — array of function delegates to add to the expression.
- **Throws:** `ArgumentNullException` if `members` is null.

#### `ExpressionBase(IEnumerable<Func<R>>)`

```csharp
public ExpressionBase(IEnumerable<Func<R>> members)
```

- **Description:** Initializes a new expression containing function members from the provided enumerable.
- **Parameter:** `members` — enumerable of function delegates to add to the expression.
- **Throws:** `ArgumentNullException` if `members` is null.

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
public event Action<int, Func<R>> OnItemChanged;
```

- **Description:** Occurs when an existing function delegate in the expression is replaced or modified.

#### `OnItemInserted`

```csharp
public event Action<int, Func<R>> OnItemInserted;
```

- **Description:** Occurs when a new function delegate is inserted into the expression at a specific position.

#### `OnItemDeleted`

```csharp
public event Action<int, Func<R>> OnItemDeleted;
```

- **Description:** Occurs when a function delegate is removed from the expression.

---

## 🔑 Properties

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

---

## 🏷️ Indexers

#### `[int index]`

```csharp
public Func<R> this[int index] { get; set; }
```

- **Description:** Indexer to access a function at a specific position.
- **Parameter:** `index` — The position of the function.
- **Returns:** `Func<R>` — The function at the given index.

---

## 🏹 Methods

#### `Invoke()`

```csharp
public R Invoke()
```

- **Description:** Evaluates all function members of the expression and returns the aggregated result.
- **Returns:** `R` — The evaluated result of the expression.

#### `Invoke(Enumerator)`

```csharp
protected abstract R Invoke(Enumerator enumerator)
```

- **Description:** Abstract template method. Derived classes define how the parameterless functions are aggregated and
  evaluated.
- **Parameter:** `enumerator` — Enumerator over the function members.
- Returns: `R` — The aggregated result of the expression.

#### `Add(Func<R> item)`

```csharp
public void Add(Func<R> item)
```

- **Description:** Adds a function to the expression.
- **Parameter:** `item` — The function to add.

#### `AddRange(IEnumerable<Func<R>>)`

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

#### `Contains(Func<R>)`

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

#### `IndexOf(Func<R>)`

```csharp
public int IndexOf(Func<R> item)
```

- **Description:** Returns the index of the specified function in the expression.
- **Parameter:** `item` — The function to locate.
- **Returns:** `int` — The index of the function, or `-1` if not found.

#### `Insert(int, Func<R>)`

```csharp
public void Insert(int index, Func<R> item)
```

- **Description:** Inserts a function at the specified index.
- **Parameters:**
    - `index` — The position at which to insert.
    - `item` — The function to insert.

#### `Remove(Func<R>)`

```csharp
public bool Remove(Func<R> item)
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
public IEnumerator<Func<R>> GetEnumerator()
```

- **Description:** Returns an enumerator for iterating over all function members in the expression.
- **Returns:** `IEnumerator<Func<R>>` — Enumerator over the functions.

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Releases all resources used by the expression and clears its content. Also unsubscribes all event
  handlers.
- **Effects:**
    - Clears the function list.
    - Sets `OnItemChanged`, `OnItemInserted`, `OnItemDeleted`, and `OnStateChanged` to `null`.