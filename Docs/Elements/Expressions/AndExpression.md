# 🧩 AndExpression Classes

The **AndExpression** classes represent **logical AND expressions** composed of one or more boolean-returning functions. They extend from the [ExpressionBase](ExpressionBase.md) family of classes and implement the corresponding [IPredicate](../Functions/IPredicate.md) interfaces.

> [!NOTE]
> The expression evaluates to `true` **only if all function members return `true`**.
> If the collection is empty, the expression evaluates to `true` by default.

---

<details>
  <summary>
    <h2>🧩 AndExpression</h2>
    <br> Represents a <b>parameterless logical AND expression</b> aggregating multiple <code>Func&lt;bool&gt;</code> members
  </summary>

<br>

```csharp
public class AndExpression : ExpressionBase<bool>, IPredicate
```

### Constructors
#### `AndExpression(int)`
```csharp
 public AndExpression(int capacity)
```
- **Description:** Initializes a new empty `AndExpression` with the given capacity.
- **Parameter:** `capacity` — Initial capacity for the internal function list. Default is `4`.

#### `AndExpression(params Func<bool>[])`
```csharp
public AndExpression(params Func<bool>[] members)
```
- **Description:** Initializes the expression with an array of parameterless boolean-returning functions.
- **Parameter:** `members` — Array of `Func<bool>` delegates.

#### `AndExpression(IEnumerable<Func<bool>>)`
```csharp
public AndExpression(IEnumerable<Func<bool>> members)
```
- **Description:** Initializes the expression with a collection of parameterless boolean-returning functions.
- **Parameter:** `members` — Enumerable of `Func<bool>` delegates.

### Events
#### `OnStateChanged`
```csharp
public event StateChangedHandler OnStateChanged;
```
- **Description:** Occurs when the state of the expression changes (e.g., when functions are added, removed, or the list is cleared).

#### `OnItemChanged`
```csharp
public event ChangeItemHandler<Func<bool>> OnItemChanged;
```
- **Description:** Occurs when an existing function in the expression is replaced or modified.

#### `OnItemInserted`
```csharp
public event InsertItemHandler<Func<bool>> OnItemInserted;
```
- **Description:** Occurs when a new function is inserted into the expression at a specific position.

#### `OnItemDeleted`
```csharp
public event DeleteItemHandler<Func<bool>> OnItemDeleted;
```
- **Description:** Occurs when a function is removed from the expression.

### Properties
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

### Indexers
#### `this[int index]`
```csharp
public Func<bool> this[int index] { get; set; }
```
- **Description:** Indexer to access a function at a specific position.
- **Parameter:** `index` — The position of the function.
- **Returns:** `Func<bool>` — The function at the given index.

### Methods
#### `Invoke()`
```csharp
public bool Invoke()
```
- **Description:** Evaluates all function members of the expression.  
  Returns `false` immediately if any function evaluates to `false`; otherwise returns `true`.
- **Returns:** `bool` — The aggregated logical AND result.

#### `Add(Func<bool> item)`
```csharp
public void Add(Func<bool> item)
```
- **Description:** Adds a function to the expression.
- **Parameter:** `item` — The function to add.

#### `AddRange(IEnumerable<Func<bool>> items)`
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

#### `Contains(Func<bool> item)`
```csharp
public bool Contains(Func<bool> item)
```
- **Description:** Checks if the specified function exists in the expression.
- **Parameter:** `item` — The function to check.
- **Returns:** `bool` — `true` if the function exists, otherwise `false`.

#### `CopyTo(Func<bool>[] array, int arrayIndex)`
```csharp
public void CopyTo(Func<bool>[] array, int arrayIndex)
```
- **Description:** Copies all functions in the expression to the specified array starting at the given index.
- **Parameters:**
  - `array` — The destination array.
  - `arrayIndex` — The starting index in the array.

#### `IndexOf(Func<bool> item)`
```csharp
public int IndexOf(Func<bool> item)
```
- **Description:** Returns the index of the specified function in the expression.
- **Parameter:** `item` — The function to locate.
- **Returns:** `int` — The index of the function, or `-1` if not found.

#### `Insert(int index, Func<bool> item)`
```csharp
public void Insert(int index, Func<bool> item)
```
- **Description:** Inserts a function at the specified index.
- **Parameters:**
  - `index` — The position at which to insert.
  - `item` — The function to insert.

#### `Remove(Func<bool> item)`
```csharp
public bool Remove(Func<bool> item)
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
---
</details>





## AndExpression<T>
!!!
[Serializable]
public class AndExpression<T> : ExpressionBase<T, bool>, IPredicate<T>
!!!
- **Description:** Represents a **logical AND expression with a single input parameter** of type `T`.

### Constructors

#### `AndExpression(int capacity = INITIAL_CAPACITY)`
!!!
public AndExpression(int capacity = INITIAL_CAPACITY)
!!!
- **Description:** Initializes a new empty `AndExpression<T>` with the given capacity.
- **Parameter:** `capacity` — Initial capacity for the internal function list. Default is `4`.

#### `AndExpression(params Func<T, bool>[] members)`
!!!
public AndExpression(params Func<T, bool>[] members)
!!!
- **Description:** Initializes the expression with an array of boolean-returning functions.
- **Parameter:** `members` — Array of `Func<T, bool>` delegates.

#### `AndExpression(IEnumerable<Func<T, bool>> members)`
!!!
public AndExpression(IEnumerable<Func<T, bool>> members)
!!!
- **Description:** Initializes the expression with a collection of boolean-returning functions.
- **Parameter:** `members` — Enumerable of `Func<T, bool>` delegates.

### Methods

#### `Invoke(Enumerator enumerator, T arg)`
!!!
protected override bool Invoke(Enumerator enumerator, T arg)
!!!
- **Description:** Evaluates all function members with the provided argument `arg`.  
  Returns `false` immediately if any function evaluates to `false`. Otherwise returns `true`.
- **Parameters:**
    - `enumerator` — Enumerator over function members.
    - `arg` — Input argument of type `T`.
- **Returns:** `bool` — `true` if all members evaluate to `true`, otherwise `false`.

---

## AndExpression<T1, T2>
!!!
[Serializable]
public class AndExpression<T1, T2> : ExpressionBase<T1, T2, bool>, IPredicate<T1, T2>
!!!
- **Description:** Represents a **logical AND expression with two input parameters** of types `T1` and `T2`.

### Constructors

#### `AndExpression(int capacity = INITIAL_CAPACITY)`
!!!
public AndExpression(int capacity = INITIAL_CAPACITY)
!!!
- **Description:** Initializes a new empty `AndExpression<T1, T2>` with the given capacity.
- **Parameter:** `capacity` — Initial capacity for the internal function list. Default is `4`.

#### `AndExpression(params Func<T1, T2, bool>[] members)`
!!!
public AndExpression(params Func<T1, T2, bool>[] members)
!!!
- **Description:** Initializes the expression with an array of boolean-returning functions.
- **Parameter:** `members` — Array of `Func<T1, T2, bool>` delegates.

#### `AndExpression(IEnumerable<Func<T1, T2, bool>> members)`
!!!
public AndExpression(IEnumerable<Func<T1, T2, bool>> members)
!!!
- **Description:** Initializes the expression with a collection of boolean-returning functions.
- **Parameter:** `members` — Enumerable of `Func<T1, T2, bool>` delegates.

### Methods

#### `Invoke(Enumerator enumerator, T1 arg1, T2 arg2)`
!!!
protected override bool Invoke(Enumerator enumerator, T1 arg1, T2 arg2)
!!!
- **Description:** Evaluates all function members with the provided arguments `arg1` and `arg2`.  
  Returns `false` immediately if any function evaluates to `false`. Otherwise returns `true`.
- **Parameters:**
    - `enumerator` — Enumerator over function members.
    - `arg1` — First input argument of type `T1`.
    - `arg2` — Second input argument of type `T2`.
- **Returns:** `bool` — `true` if all members evaluate to `true`, otherwise `false`.



==========================
==========================

# 🧩 AndExpression

These classes provide **logical AND expressions** that aggregate multiple functions returning boolean values.  
They evaluate all registered functions and return `true` **only if all functions return `true`**.

---

## `AndExpression`

A parameterless logical AND expression.

```csharp
public class AndExpression : ExpressionBase<bool>, IPredicate
```

These classes provide **logical AND expressions** that aggregate multiple functions returning boolean values.  
They evaluate all registered functions and return `true` **only if all functions return `true`**.
---

## `AndExpression`

A parameterless logical AND expression.

```csharp
public class AndExpression : ExpressionBase<bool>, IPredicate
```

### Constructors
- `AndExpression()` – Initializes an empty AND expression.
- `AndExpression(params Func<bool>[] members)` – Initializes with an array of functions.
- `AndExpression(IEnumerable<Func<bool>> members)` – Initializes with a collection of functions.
### Methods
- `protected sealed override bool Invoke(Enumerator enumerator)` – Returns `true` if all functions return `true` (or if no functions exist).
---
## `AndExpression<T>`
A generic logical AND expression for functions with **one input parameter**.
```csharp
public class AndExpression<T> : ExpressionBase<T, bool>, IPredicate<T>
```
### Constructors
- `AndExpression()` – Initializes an empty AND expression.
- `AndExpression(params Func<T, bool>[] members)` – Initializes with an array of functions.
- `AndExpression(IEnumerable<Func<T, bool>> members)` – Initializes with a collection of functions.

### Methods
- `protected sealed override bool Invoke(Enumerator enumerator, T arg)` – Returns `true` if all functions return `true` for the given argument.
---
## `AndExpression<T1, T2>`
A generic logical AND expression for functions with **two input parameters**.
```csharp
public class AndExpression<T1, T2> : ExpressionBase<T1, T2, bool>, IPredicate<T1, T2>
```
### Constructors
- `AndExpression()` – Initializes an empty AND expression.
- `AndExpression(params Func<T1, T2, bool>[] members)` – Initializes with an array of functions.
- `AndExpression(IEnumerable<Func<T1, T2, bool>> members)` – Initializes with a collection of functions.
### Methods
- `protected override bool Invoke(Enumerator enumerator, T1 arg1, T2 arg2)` – Returns `true` if all functions return `true` for the given arguments.

## Examples of Usage
Example of using AndExpression for a character
```csharp
// Simple logic to check if the character can attack
IFunction<bool> canAttack = new AndExpression(
    () => player.IsAlive,           // Player is alive
    () => !player.IsStunned,        // Player is not stunned
    () => enemy != null,            // There is a target
    () => player.HasWeapon          // Player has a weapon
);

bool attackAllowed = canAttack.Invoke(); // true if all conditions are met
```
```csharp
// Movement logic with directional checks
IFunction<Vector3> canMove = new AndExpression<Vector3>(
    dir => !player.IsStunned,           // Player is not stunned
    dir => !IsObstacleInDirection(dir), // No obstacle in the given direction
    dir => player.Stamina > 0           // Player has enough stamina
);

bool moveAllowed = canMove.Invoke(Vector3.forward); // Check if forward movement is allowed
```