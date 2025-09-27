

<details>
  <summary>
    <h2>🧩 IExpression&lt;T1, T2, R&gt;</h2>
    <br>Represents an expression with <b>two input parameters</b> of types <code>T1</code> and <code>T2</code> that aggregates multiple functions returning a value of type <code>R</code>
  </summary>
<br>

```csharp
public interface IExpression<T1, T2, R> : IList<Func<T1, T2, R>>, IFunction<T1, T2, R>
```

- **Type Parameters:**
    - `T1` — The first input parameter type.
    - `T2` — The second input parameter type.
    - `R` — The return type of the expression.

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
public Func<T1, T2, R> this[int index] { get; set; }
```

- **Description:** Indexer to access a function at a specific position.
- **Parameter:** `index` — The position of the function.
- **Returns:** `Func<T1, T2, R>` — The function at the given index.

---

### 🏹 Methods

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

</details>