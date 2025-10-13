# 🧩 OrExpression&lt;T1, T2&gt;

Represents a <b>logical OR expression</b> aggregating multiple <code>Func&lt;T1, T2, bool&gt;</code>
members.

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
            <li><a href="#orexpressionint">OrExpression(int)</a></li>
            <li><a href="#orexpressionparams-funct1-t2-bool">OrExpression(params Func&lt;T1, T2, bool&gt;[])</a></li>
            <li><a href="#orexpressionienumerable-funct1-t2-bool">OrExpression(IEnumerable&lt;Func&lt;T1, T2, bool&gt;&gt;)</a></li>
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
            <li><a href="#int-index">[int index]</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-methods">Methods</a></summary>
          <ul>
            <li><a href="#invoket1-t2">Invoke(T1, T2)</a></li>
            <li><a href="#addfunct1-t2-bool">Add(Func&lt;T1, T2, bool&gt;)</a></li>
            <li><a href="#addrangeienumerablefunct1-t2-bool">AddRange(IEnumerable&lt;Func&lt;T1, T2, bool&gt;&gt;)</a></li>
            <li><a href="#clear">Clear()</a></li>
            <li><a href="#containsfunct1-t2-bool">Contains(Func&lt;T1, T2, bool&gt;)</a></li>
            <li><a href="#copytofunct1-t2-bool-int">CopyTo(Func&lt;T1, T2, bool&gt;[], int)</a></li>
            <li><a href="#indexoffunct1-t2-bool">IndexOf(Func&lt;T1, T2, bool&gt;)</a></li>
            <li><a href="#insertint-funct1-t2-bool">Insert(int, Func&lt;T1, T2, bool&gt;)</a></li>
            <li><a href="#removefunct1-t2-bool">Remove(Func&lt;T1, T2, bool&gt;)</a></li>
            <li><a href="#removeatint">RemoveAt(int)</a></li>
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
public class OrExpression<T1, T2> : ExpressionBase<T1, T2, bool>, IPredicate<T1, T2>
```

- **Description:** Represents a <b>logical OR expression</b> aggregating multiple <code>Func&lt;T1, T2, bool&gt;</code>
  members
- **Type Parameters:**
    - `T1` — The first input type of the functions.
    - `T2` — The second input type of the functions.
- **Inheritance:** [ExpressionBase&lt;T1, T2, R&gt;](ExpressionBase%602.md),
  [IPredicate&lt;T1, T2&gt;](../Functions/IPredicate%602.md)
- **Note:** Supports Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

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

<div id="orexpressionienumerable-funct1-t2-bool"></div>

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