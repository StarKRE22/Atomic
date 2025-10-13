# 🧩 AndExpression&lt;T1, T2&gt;

<br> Represents a <b>logical AND expression</b> aggregating multiple <code>Func&lt;T1, T2, bool&gt;</code> members.

---

## 📑 Table of Contents

<ul>
  <li><a href="#-api-reference">API Reference</a>
    <ul>
      <li><a href="#-type">Type</a></li>
      <li>
        <details>
          <summary><a href="#-constructors">Constructors</a></summary>
          <ul>
            <li><a href="#capacity-based-constructor">Capacity-based Constructor</a></li>
            <li><a href="#params-constructor">Params Constructor</a></li>
            <li><a href="#ienumerable-constructor">IEnumerable Constructor</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-events">Events</a></summary>
          <ul>
            <li><a href="#onstatechanged">OnStateChanged</a></li>
            <li><a href="#onitemchanged">OnItemChanged</a></li>
            <li><a href="#oniteminserted">OnItemInserted</a></li>
            <li><a href="#onitemdeleted">OnItemDeleted</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-properties">Properties</a></summary>
          <ul>
            <li><a href="#count">Count</a></li>
            <li><a href="#isreadonly">IsReadOnly</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-indexers">Indexers</a></summary>
          <ul>
            <li><a href="#thisint-index">this[int index]</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-methods">Methods</a></summary>
          <ul>
            <li><a href="#invoket1-arg1-t2-arg2">Invoke(T1 arg1, T2 arg2)</a></li>
            <li><a href="#add">Add(Func&lt;T1, T2, bool&gt; item)</a></li>
            <li><a href="#addrange">AddRange(IEnumerable&lt;Func&lt;T1, T2, bool&gt;&gt; items)</a></li>
            <li><a href="#clear">Clear()</a></li>
            <li><a href="#contains">Contains(Func&lt;T1, T2, bool&gt; item)</a></li>
            <li><a href="#copyto">CopyTo(Func&lt;T1, T2, bool&gt;[] array, int arrayIndex)</a></li>
            <li><a href="#indexof">IndexOf(Func&lt;T1, T2, bool&gt; item)</a></li>
            <li><a href="#insert">Insert(int index, Func&lt;T1, T2, bool&gt; item)</a></li>
            <li><a href="#remove">Remove(Func&lt;T1, T2, bool&gt; item)</a></li>
            <li><a href="#removeat">RemoveAt(int index)</a></li>
            <li><a href="#getenumerator">GetEnumerator()</a></li>
            <li><a href="#dispose">Dispose()</a></li>
          </ul>
        </details>
      </li>
    </ul>
  </li>
</ul>

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public class AndExpression<T1, T2> : ExpressionBase<T1, T2, bool>, IPredicate<T1, T2>
```

- **Description:** <br> Represents a <b>logical AND expression</b> aggregating multiple 
  <code>Func&lt;T1, T2, bool&gt;</code> members
- **Type Parameters:**
    - `T1` - The first input parameter type of the functions.
    - `T2` - The second input parameter type of the functions.
- **Inheritance:** [ExpressionBase&lt;T1, T2, R&gt;](ExpressionBase%602.md), [IPredicate&lt;T1, T2&gt;](../Functions/IPredicate%602.md)
- **Note:** Supports Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `Capacity-based Constructor`

```csharp
public AndExpression(int capacity)
```

- **Description:** Initializes a new empty `AndExpression<T1, T2>` with the given capacity.
- **Parameter:** `capacity` — Initial capacity for the internal function list. Default is `4`.

#### `Params Constructor`

```csharp
public AndExpression(params Func<T1, T2, bool>[] members)
```

- **Description:** Initializes the expression with an array of boolean-returning functions that take arguments of type
  `T1` and `T2`.
- **Parameter:** `members` — Array of `Func<T1, T2, bool>` delegates.

#### `IEnumerable Constructor`

```csharp
public AndExpression(IEnumerable<Func<T1, T2, bool>> members)
```

- **Description:** Initializes the expression with a collection of boolean-returning functions that take arguments of
  type `T1` and `T2`.
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

#### `this[int index]`

```csharp
public Func<T1, T2, bool> this[int index] { get; set; }
```

- **Description:** Indexer to access a function at a specific position.
- **Parameter:** `index` — The position of the function.
- **Returns:** `Func<T1, T2, bool>` — The function at the given index.

---

### 🏹 Methods

#### `Invoke(T1 arg1, T2 arg2)`

```csharp
public bool Invoke(T1 arg1, T2 arg2)
```

- **Description:** Evaluates all function members of the expression using the provided arguments.  
  Returns `false` immediately if any function evaluates to `false`; otherwise returns `true`.
- **Parameters:**
    - `arg1` — The first input value of type `T1`.
    - `arg2` — The second input value of type `T2`.
- **Returns:** `bool` — The aggregated logical AND result.

<div id="add"></div>

#### `Add(Func<T1, T2, bool> item)`

```csharp
public void Add(Func<T1, T2, bool> item)
```

- **Description:** Adds a function to the expression.
- **Parameter:** `item` — The function to add.

<div id="addrange"></div>

#### `AddRange(IEnumerable<Func<T1, T2, bool>> items)`

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

<div id="contains"></div>

#### `Contains(Func<T1, T2, bool> item)`

```csharp
public bool Contains(Func<T1, T2, bool> item)
```

- **Description:** Checks if the specified function exists in the expression.
- **Parameter:** `item` — The function to check.
- **Returns:** `bool` — `true` if the function exists, otherwise `false`.

<div id="copyto"></div>

#### `CopyTo(Func<T1, T2, bool>[] array, int arrayIndex)`

```csharp
public void CopyTo(Func<T1, T2, bool>[] array, int arrayIndex)
```

- **Description:** Copies all functions in the expression to the specified array starting at the given index.
- **Parameters:**
    - `array` — The destination array.
    - `arrayIndex` — The starting index in the array.

<div id="indexof"></div>

#### `IndexOf(Func<T1, T2, bool> item)`

```csharp
public int IndexOf(Func<T1, T2, bool> item)
```

- **Description:** Returns the index of the specified function in the expression.
- **Parameter:** `item` — The function to locate.
- **Returns:** `int` — The index of the function, or `-1` if not found.

<div id="insert"></div>

#### `Insert(int index, Func<T1, T2, bool> item)`

```csharp
public void Insert(int index, Func<T1, T2, bool> item)
```

- **Description:** Inserts a function at the specified index.
- **Parameters:**
    - `index` — The position at which to insert.
    - `item` — The function to insert.

<div id="remove"></div>

#### `Remove(Func<T1, T2, bool> item)`

```csharp
public bool Remove(Func<T1, T2, bool> item)
```

- **Description:** Removes the specified function from the expression.
- **Parameter:** `item` — The function to remove.
- **Returns:** `bool` — `true` if removed successfully, otherwise `false`.

<div id="removeat"></div>

#### `RemoveAt(int index)`

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