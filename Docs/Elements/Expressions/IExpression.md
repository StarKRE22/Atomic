# 🧩 IExpression&lt;R&gt;

Represents a <b>parameterless expression</b> aggregating multiple functions returning a value of
type <code>R</code>

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IExpression<R> : IList<Func<R>>, IValue<R>, IFunction<R>
```

- **Description:** Represents a <b>parameterless expression</b> aggregating multiple functions returning a value of
  type <code>R</code>
- **Inheritance:** `IList<T>`, [IValue&lt;T&gt;](../Values/IValue.md), [IFunction&lt;R&gt;](../Functions/IFunction.md)
- **Type parameter**: `R` — The return type of the expression.

---

### ⚡ Events

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

### 🔑 Properties

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

### 🏷️ Indexers

#### `[int index]`

```csharp
public Func<R> this[int index] { get; set; }
```

- **Description:** Indexer to access a function at a specific position.
- **Parameter:** `index` — The position of the function.
- **Returns:** `Func<R>` — The function at the given index.

---

### 🏹 Methods

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

#### `Add(Func<R>)`

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

#### `Contains(Func<R>)`

```csharp
public bool Contains(Func<R> item)
```

- **Description:** Checks if the specified function exists in the expression.
- **Parameter:** `item` — The function to check.
- **Returns:** `bool` — `true` if the function exists, otherwise `false`.

#### `CopyTo(Func<R>[], int)`

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