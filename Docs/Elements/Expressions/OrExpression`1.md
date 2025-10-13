# 🧩 OrExpression&lt;T&gt;

Represents a <b>logical OR expression</b> with <b>single parameter</b> aggregating multiple
<code>Func&lt;T, bool&gt;</code> members

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
            <li><a href="#orexpressionparams-funct-bool">OrExpression(params Func&lt;T, bool&gt;[])</a></li>
            <li><a href="#orexpressionienumerable-funct-bool">OrExpression(IEnumerable&lt;Func&lt;T, bool&gt;&gt;)</a></li>
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
            <li><a href="#invoket">Invoke(T)</a></li>
            <li><a href="#addfunct-bool">Add(Func&lt;T, bool&gt;)</a></li>
            <li><a href="#addrangeienumerablefunct-bool">AddRange(IEnumerable&lt;Func&lt;T, bool&gt;&gt;)</a></li>
            <li><a href="#clear">Clear()</a></li>
            <li><a href="#containsfunct-bool">Contains(Func&lt;T, bool&gt;)</a></li>
            <li><a href="#copytofunct-bool-int">CopyTo(Func&lt;T, bool&gt;[], int)</a></li>
            <li><a href="#indexoffunct-bool">IndexOf(Func&lt;T, bool&gt;)</a></li>
            <li><a href="#insertint-funct-bool">Insert(int, Func&lt;T, bool&gt;)</a></li>
            <li><a href="#removefunct-bool">Remove(Func&lt;T, bool&gt;)</a></li>
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
public class OrExpression<T> : ExpressionBase<T, bool>, IPredicate<T>
```

- **Description:** Represents a <b>logical OR expression</b> with <b>single parameter</b> aggregating multiple
  <code>Func&lt;T, bool&gt;</code> members
- **Type Parameter:** `T` — The input type of the functions.
- **Inheritance:** [ExpressionBase&lt;T, R&gt;](ExpressionBase%601.md),
  [IPredicate&lt;T&gt;](../Functions/IPredicate%601.md)
- **Note:** Supports Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

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

<div id="orexpressionienumerable-funct-bool"></div>

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