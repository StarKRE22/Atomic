# 🧩 IntMulExpression&lt;T&gt;

Represents an expression that computes the product of integer values returned from functions with
a <b>single input parameter</b>

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
            <li><a href="#ctor-1">IntMulExpression(int)</a></li>
            <li><a href="#ctor-2">IntMulExpression(Func&lt;T, int&gt;[])</a></li>
            <li><a href="#ctor-3">IntMulExpression(IEnumerable&lt;Func&lt;T, int&gt;&gt;)</a></li>
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
            <li><a href="#add">Add(Func&lt;T, int&gt;)</a></li>
            <li><a href="#addrange">AddRange(IEnumerable&lt;Func&lt;T, int&gt;&gt;)</a></li>
            <li><a href="#clear">Clear()</a></li>
            <li><a href="#contains">Contains(Func&lt;T, int&gt;)</a></li>
            <li><a href="#copyto">CopyTo(Func&lt;T, int&gt;[], int)</a></li>
            <li><a href="#indexof">IndexOf(Func&lt;T, int&gt;)</a></li>
            <li><a href="#insert">Insert(int, Func&lt;T, int&gt;)</a></li>
            <li><a href="#remove">Remove(Func&lt;T, int&gt;)</a></li>
            <li><a href="#removeat">RemoveAt(int)</a></li>
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
public class IntMulExpression<T> : ExpressionBase<T, int>
```

- **Description:** Represents an expression that computes the product of integer values returned from functions with
  a <b>single input parameter</b>
- **Type Parameter:** `T` — The input parameter type of the functions.
- **Inheritance:** [ExpressionBase&lt;T, R&gt;](ExpressionBase%601.md)
- **Note:** Supports Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

<div id="ctor-1"></div>

#### `IntMulExpression(int)`

```csharp
public IntMulExpression(int capacity)
```

- **Description:** Initializes a new empty instance of the `IntMulExpression<T>` class.
- **Parameter:** `capacity` — Initial capacity for the internal function list. Default is `4`.

<div id="ctor-2"></div>

#### `IntMulExpression(Func<T, int>[])`

```csharp
public IntMulExpression(params Func<T, int>[] members)
```

- **Description:** Initializes the expression with an array of functions that take a `T` and return an integer.
- **Parameter:** `members` — Array of `Func<T, int>` delegates.

<div id="ctor-3"></div>

#### `IntMulExpression(IEnumerable<Func<T, int>>)`

```csharp
public IntMulExpression(IEnumerable<Func<T, int>> members)
```

- **Description:** Initializes the expression with a collection of functions that take a `T` and return an integer.
- **Parameter:** `members` — Enumerable collection of `Func<T, int>` delegates.

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
public event Action<int, Func<T, int>> OnItemChanged;
```

- **Description:** Occurs when an existing function in the expression is replaced or modified.

#### `OnItemInserted`

```csharp
public event Action<int, Func<T, int>> OnItemInserted;
```

- **Description:** Occurs when a new function is inserted into the expression at a specific position.

#### `OnItemDeleted`

```csharp
public event Action<int, Func<T, int>> OnItemDeleted;
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
public Func<T, int> this[int index] { get; set; }
```

- **Description:** Indexer to access a function at a specific position.
- **Parameter:** `index` — The position of the function.
- **Returns:** `Func<T, int>` — The function at the given index.

---

### 🏹 Methods

#### `Invoke(T)`

```csharp
public int Invoke(T arg)
```

- **Description:** Evaluates all function members of the expression with the provided argument and returns their
  product.
- **Parameter:** `arg` — The input argument of type T.
- **Returns:** `int` — The computed product.
- **Note:** -Returns `1` if no functions are present.

<div id="add"></div>

#### `Add(Func<T, int>)`

```csharp
public void Add(Func<T, int> item)
```

- **Description:** Adds a function to the expression.
- **Parameter:** `item` — The function to add.

<div id="addrange"></div>

#### `AddRange(IEnumerable<Func<T, int>>)`

```csharp
public void AddRange(IEnumerable<Func<T, int>> items)
```

- **Description:** Adds multiple functions to the expression at once.
- **Parameter:** `items` — An enumerable collection of `Func<T, int>` delegates to add.
- **Throws:** `ArgumentNullException` if `items` is `null`.

#### `Clear()`

```csharp
public void Clear()
```

- **Description:** Removes all functions from the expression.

<div id="contains"></div>

#### `Contains(Func<T, int>)`

```csharp
public bool Contains(Func<T, int> item)
```

- **Description:** Checks if the specified function exists in the expression.
- **Parameter:** `item` — The function to check.
- **Returns:** `bool` — `true` if the function exists, otherwise `false`.

<div id="copyto"></div>

#### `CopyTo(Func<T, int>[], int)`

```csharp
public void CopyTo(Func<T, int>[] array, int arrayIndex)
```

- **Description:** Copies all functions in the expression to the specified array starting at the given index.
- **Parameters:**
    - `array` — The destination array.
    - `arrayIndex` — The starting index in the array.

<div id="indexof"></div>

#### `IndexOf(Func<T, int>)`

```csharp
public int IndexOf(Func<T, int> item)
```

- **Description:** Returns the index of the specified function in the expression.
- **Parameter:** `item` — The function to locate.
- **Returns:** `int` — The index of the function, or `-1` if not found.

<div id="insert"></div>

#### `Insert(int, Func<T, int>)`

```csharp
public void Insert(int index, Func<T, int> item)
```

- **Description:** Inserts a function at the specified index.
- **Parameters:**
    - `index` — The position at which to insert.
    - `item` — The function to insert.

<div id="remove"></div>

#### `Remove(Func<T, int>)`

```csharp
public bool Remove(Func<T, int> item)
```

- **Description:** Removes the specified function from the expression.
- **Parameter:** `item` — The function to remove.
- **Returns:** `bool` — `true` if removed successfully, otherwise `false`.

<div id="removeat"></div>

#### `RemoveAt(int)`

```csharp
public void RemoveAt(int index)
```

- **Description:** Removes the function at the specified index.
- **Parameter:** `index` — The position of the function to remove.

#### `GetEnumerator()`

```csharp
public IEnumerator<Func<T, int>> GetEnumerator()
```

- **Description:** Returns an enumerator for iterating over all function members in the expression.
- **Returns:** `IEnumerator<Func<T, int>>` — Enumerator over the functions.

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Releases all resources used by the expression and clears its content.  
  Also unsubscribes all event handlers.
- **Effects:**
    - Clears the function list.
    - Sets `OnItemChanged`, `OnItemInserted`, `OnItemDeleted`, and `OnStateChanged` to `null`.