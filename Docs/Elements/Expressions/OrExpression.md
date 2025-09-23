# 🧩 OrExpression Classes

Represents **logical OR expressions** composed of one or more boolean-returning functions. They extend from
the [ExpressionBase](ExpressionBase.md) family of classes and implement the
corresponding [IPredicate](../Functions/IPredicate.md) interfaces.

> [!NOTE]
> The expression evaluates to `true` **if at least one function member returns `true`**.  
> If the collection is empty, the expression evaluates to `false` by default.

---

<details>
  <summary>
    <h2>🧩 OrExpression</h2>
    <br> Represents a <b>parameterless logical OR expression</b> aggregating multiple <code>Func&lt;bool&gt;</code> members
  </summary>

<br>

```csharp
public class OrExpression : ExpressionBase<bool>, IPredicate
```

---

### 🏗️ Constructors

#### `OrExpression(int)`

```csharp
public OrExpression(int capacity)
```
- **Description:** Initializes a new empty `OrExpression` with the given capacity.
- **Parameter:** `capacity` — Initial capacity for the internal function list. Default is `4`.


#### `OrExpression(params Func<bool>[])`

```csharp
public OrExpression(params Func<bool>[] members)
```

- **Description:** Initializes the expression with an array of parameterless boolean-returning functions.
- **Parameter:** `members` — Array of `Func<bool>` delegates.

#### `OrExpression(IEnumerable<Func<bool>>)`

```csharp
public OrExpression(IEnumerable<Func<bool>> members)
```

- **Description:** Initializes the expression with a collection of parameterless boolean-returning functions.
- **Parameter:** `members` — Enumerable of `Func<bool>` delegates.

### ⚡ Events

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;
```

- **Description:** Occurs when the state of the expression changes (e.g., when functions are added, removed, or the list
  is cleared).

#### `OnItemChanged`

```csharp
public event Action<int, Func<bool>> OnItemChanged;
```

- **Description:** Occurs when an existing function in the expression is replaced or modified.

#### `OnItemInserted`

```csharp
public event Action<int, Func<bool>> OnItemInserted;
```

- **Description:** Occurs when a new function is inserted into the expression at a specific position.

#### `OnItemDeleted`

```csharp
public event Action<int, Func<bool>> OnItemDeleted;
```

- **Description:** Occurs when a function is removed from the expression.

---

### 🔑 Properties

#### `Value`

```csharp
public bool Value { get; }
```

- **Description:** Evaluates all functions and returns `true` if all functions return `true`.  
  If no functions are present, returns `true` by default.
- **Returns:** `bool` — The evaluated logical AND result.

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
public Func<bool> this[int index] { get; set; }
```

- **Description:** Indexer to access a function at a specific position.
- **Parameter:** `index` — The position of the function.
- **Returns:** `Func<bool>` — The function at the given index.

---

### 🏹 Methods

#### `Invoke()`

```csharp
public bool Invoke()
```

- **Description:** Evaluates all function members of the expression.  
  Returns `false` immediately if any function evaluates to `false`; otherwise returns `true`.
- **Returns:** `bool` — The aggregated logical AND result.

#### `Add(Func<bool>)`

```csharp
public void Add(Func<bool> item)
```

- **Description:** Adds a function to the expression.
- **Parameter:** `item` — The function to add.

#### `AddRange(IEnumerable<Func<bool>>)`

```csharp
public void AddRange(IEnumerable<Func<bool>> items)
```

- **Description:** Adds multiple functions to the expression at once.
- **Parameter:** `items` — An enumerable collection of `Func<bool>` delegates to add.
- **Throws:** `ArgumentNullException` if `items` is `null`.

#### `Clear()`

```csharp
public void Clear()
```

- **Description:** Removes all functions from the expression.

#### `Contains(Func<bool>)`

```csharp
public bool Contains(Func<bool> item)
```

- **Description:** Checks if the specified function exists in the expression.
- **Parameter:** `item` — The function to check.
- **Returns:** `bool` — `true` if the function exists, otherwise `false`.

#### `CopyTo(Func<bool>[], int)`

```csharp
public void CopyTo(Func<bool>[] array, int arrayIndex)
```

- **Description:** Copies all functions in the expression to the specified array starting at the given index.
- **Parameters:**
    - `array` — The destination array.
    - `arrayIndex` — The starting index in the array.

#### `IndexOf(Func<bool>)`

```csharp
public int IndexOf(Func<bool> item)
```

- **Description:** Returns the index of the specified function in the expression.
- **Parameter:** `item` — The function to locate.
- **Returns:** `int` — The index of the function, or `-1` if not found.

#### `Insert(int, Func<bool>)`

```csharp
public void Insert(int index, Func<bool> item)
```

- **Description:** Inserts a function at the specified index.
- **Parameters:**
    - `index` — The position at which to insert.
    - `item` — The function to insert.

#### `Remove(Func<bool>)`

```csharp
public bool Remove(Func<bool> item)
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
public IEnumerator<Func<bool>> GetEnumerator()
```

- **Description:** Returns an enumerator for iterating over all function members in the expression.
- **Returns:** `IEnumerator<Func<bool>>` — Enumerator over the functions.

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
    <h2>🧩 OrExpression&lt;T&gt;</h2>
    <br> Represents a <b>logical OR expression</b> with <b>single parameter</b> aggregating multiple <code>Func&lt;T, bool&gt;</code> members
  </summary>

<br>

```csharp
public class OrExpression<T> : ExpressionBase<T, bool>, IPredicate<T>
```

- **Type Parameters:**
    - `T` — The input type of the functions.

---

### 🏗️ Constructors

#### `OrExpression(int)`

```csharp
public OrExpression(int capacity)
```
- **Description:** Initializes a new empty `OrExpression` with the given capacity.
- **Parameter:** `capacity` — Initial capacity for the internal function list. Default is `4`.


#### `OrExpression(params Func<T, bool>[])`

```csharp
public OrExpression(params Func<T, bool>[] members)
```

- **Description:** Initializes the expression with an array of boolean-returning functions that take an argument of type
  `T`.
- **Parameter:** `members` — Array of `Func<T, bool>` delegates.

#### `OrExpression(IEnumerable<Func<T, bool>>)`

```csharp
public OrExpression(IEnumerable<Func<T, bool>> members)
```

- **Description:** Initializes the expression with a collection of boolean-returning functions that take an argument of
  type `T`.
- **Parameter:** `members` — Enumerable of `Func<T, bool>` delegates.

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
public event Action<int, Func<T, bool>> OnItemChanged;
```

- **Description:** Occurs when an existing function in the expression is replaced or modified.

#### `OnItemInserted`

```csharp
public event Action<int, Func<T, bool>> OnItemInserted;
```

- **Description:** Occurs when a new function is inserted into the expression at a specific position.

#### `OnItemDeleted`

```csharp
public event Action<int, Func<T, bool>> OnItemDeleted;
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
public Func<T, bool> this[int index] { get; set; }
```

- **Description:** Indexer to access a function at a specific position.
- **Parameter:** `index` — The position of the function.
- **Returns:** `Func<T, bool>` — The function at the given index.

---

### 🏹 Methods

#### `Invoke(T)`

```csharp
public bool Invoke(T arg)
```

- **Description:** Evaluates all function members of the expression using the provided argument.  
  Returns `false` immediately if any function evaluates to `false`; otherwise returns `true`.
- **Parameter:** `arg` — The input value of type `T`.
- **Returns:** `bool` — The aggregated logical AND result.

#### `Add(Func<T, bool>)`

```csharp
public void Add(Func<T, bool> item)
```

- **Description:** Adds a function to the expression.
- **Parameter:** `item` — The function to add.

#### `AddRange(IEnumerable<Func<T, bool>>)`

```csharp
public void AddRange(IEnumerable<Func<T, bool>> items)
```

- **Description:** Adds multiple functions to the expression at once.
- **Parameter:** `items` — An enumerable collection of `Func<T, bool>` delegates to add.
- **Throws:** `ArgumentNullException` if `items` is `null`.

#### `Clear()`

```csharp
public void Clear()
```

- **Description:** Removes all functions from the expression.

#### `Contains(Func<T, bool>)`

```csharp
public bool Contains(Func<T, bool> item)
```

- **Description:** Checks if the specified function exists in the expression.
- **Parameter:** `item` — The function to check.
- **Returns:** `bool` — `true` if the function exists, otherwise `false`.

#### `CopyTo(Func<T, bool>[], int)`

```csharp
public void CopyTo(Func<T, bool>[] array, int arrayIndex)
```

- **Description:** Copies all functions in the expression to the specified array starting at the given index.
- **Parameters:**
    - `array` — The destination array.
    - `arrayIndex` — The starting index in the array.

#### `IndexOf(Func<T, bool>)`

```csharp
public int IndexOf(Func<T, bool> item)
```

- **Description:** Returns the index of the specified function in the expression.
- **Parameter:** `item` — The function to locate.
- **Returns:** `int` — The index of the function, or `-1` if not found.

#### `Insert(int, Func<T, bool>)`

```csharp
public void Insert(int index, Func<T, bool> item)
```

- **Description:** Inserts a function at the specified index.
- **Parameters:**
    - `index` — The position at which to insert.
    - `item` — The function to insert.

#### `Remove(Func<T, bool>)`

```csharp
public bool Remove(Func<T, bool> item)
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
public IEnumerator<Func<T, bool>> GetEnumerator()
```

- **Description:** Returns an enumerator for iterating over all function members in the expression.
- **Returns:** `IEnumerator<Func<T, bool>>` — Enumerator over the functions.

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
    <h2>🧩 OrExpression&lt;T1, T2&gt;</h2>
    <br> Represents a <b>logical OR expression</b> aggregating multiple <code>Func&lt;T1, T2, bool&gt;</code> members
  </summary>

<br>

```csharp
public class OrExpression<T1, T2> : ExpressionBase<T1, T2, bool>, IPredicate<T1, T2>
```

- **Type Parameters:**
    - `T1` — The first input type of the functions.
    - `T2` — The second input type of the functions.

---

### 🏗️ Constructors

#### `OrExpression(int)`

```csharp
public OrExpression(int capacity)
```
- **Description:** Initializes a new empty `OrExpression` with the given capacity.
- **Parameter:** `capacity` — Initial capacity for the internal function list. Default is `4`.

#### `OrExpression(params Func<T1, T2, bool>[])`

```csharp
public OrExpression(params Func<T1, T2, bool>[] members)
```

- **Description:** Initializes the expression with an array of functions that take arguments of type `T1` and `T2` and
  return a boolean.
- **Parameter:** `members` — Array of `Func<T1, T2, bool>` delegates.

#### `OrExpression(IEnumerable<Func<T1, T2, bool>>)`

```csharp
public OrExpression(IEnumerable<Func<T1, T2, bool>> members)
```

- **Description:** Initializes the expression with a collection of functions that take arguments of type `T1` and `T2`
  and return a boolean.
- **Parameter:** `members` — Enumerable of `Func<T1, T2, bool>` delegates.

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
public event Action<int, Func<T1, T2, bool>> OnItemChanged;
```

- **Description:** Occurs when an existing function in the expression is replaced or modified.

#### `OnItemInserted`

```csharp
public event Action<int, Func<T1, T2, bool>> OnItemInserted;
```

- **Description:** Occurs when a new function is inserted into the expression at a specific position.

#### `OnItemDeleted`

```csharp
public event Action<int, Func<T1, T2, bool>> OnItemDeleted;
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
public Func<T1, T2, bool> this[int index] { get; set; }
```

- **Description:** Indexer to access a function at a specific position.
- **Parameter:** `index` — The position of the function.
- **Returns:** `Func<T1, T2, bool>` — The function at the given index.

---

### 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
public bool Invoke(T1 arg1, T2 arg2)
```

- **Description:** Evaluates all function members of the expression using the provided arguments.  
  Returns `false` immediately if any function evaluates to `false`; otherwise returns `true`.
- **Parameters:**
    - `arg1` — The first input value of type `T1`.
    - `arg2` — The second input value of type `T2`.
- **Returns:** `bool` — The aggregated logical AND result.

#### `Add(Func<T1, T2, bool>)`

```csharp
public void Add(Func<T1, T2, bool> item)
```

- **Description:** Adds a function to the expression.
- **Parameter:** `item` — The function to add.

#### `AddRange(IEnumerable<Func<T1, T2, bool>>)`

```csharp
public void AddRange(IEnumerable<Func<T1, T2, bool>> items)
```

- **Description:** Adds multiple functions to the expression at once.
- **Parameter:** `items` — An enumerable collection of `Func<T1, T2, bool>` delegates to add.
- **Throws:** `ArgumentNullException` if `items` is `null`.

#### `Clear()`

```csharp
public void Clear()
```

- **Description:** Removes all functions from the expression.

#### `Contains(Func<T1, T2, bool>)`

```csharp
public bool Contains(Func<T1, T2, bool> item)
```

- **Description:** Checks if the specified function exists in the expression.
- **Parameter:** `item` — The function to check.
- **Returns:** `bool` — `true` if the function exists, otherwise `false`.

#### `CopyTo(Func<T1, T2, bool>[], int)`

```csharp
public void CopyTo(Func<T1, T2, bool>[] array, int arrayIndex)
```

- **Description:** Copies all functions in the expression to the specified array starting at the given index.
- **Parameters:**
    - `array` — The destination array.
    - `arrayIndex` — The starting index in the array.

#### `IndexOf(Func<T1, T2, bool>)`

```csharp
public int IndexOf(Func<T1, T2, bool> item)
```

- **Description:** Returns the index of the specified function in the expression.
- **Parameter:** `item` — The function to locate.
- **Returns:** `int` — The index of the function, or `-1` if not found.

#### `Insert(int, Func<T1, T2, bool>)`

```csharp
public void Insert(int index, Func<T1, T2, bool> item)
```

- **Description:** Inserts a function at the specified index.
- **Parameters:**
    - `index` — The position at which to insert.
    - `item` — The function to insert.

#### `Remove(Func<T1, T2, bool>)`

```csharp
public bool Remove(Func<T1, T2, bool> item)
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
public IEnumerator<Func<T1, T2, bool>> GetEnumerator()
```

- **Description:** Returns an enumerator for iterating over all function members in the expression.
- **Returns:** `IEnumerator<Func<T1, T2, bool>>` — Enumerator over the functions.

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

Below are examples of using `OrExpression` to configure an entity using `Atomic.Entities`.

```csharp
// Setting up a character with an OR expression for healing
public sealed class CharacterInstaller : SceneEntityInstaller
{
    [SerializeField] private ReactiveVariable<int> _medkitCount = 3;
    [SerializeField] private ReactiveVariable<IEntity> _targetMedkit = new();

    public override void Install(IEntity entity)
    {
        // Life: add a condition for healing
        entity.AddHealingCondition(new OrExpression(
            () => _medkitCount.Value > 0,         // Has medkit in the inventory
            () => _targetMedkit.Value != null     // Has medkit pick up nearby
        ));
    }
}
```

```csharp
// Use healing condition for AI as example:
IExpression<bool> condition = entity.GetHealingCondition();
bool canHealing = condition.Invoke();
```