# 🧩 IntSumExpression

Represents an expression that computes the sum of multiple <b>parameterless integer-returning</b>
functions

---

## 📑 Table of Contents

<ul>
  <li><a href="#-example-of-usage">Example of Usage</a></li>
  <li><a href="#-api-reference">API Reference</a>
    <ul>
      <li><a href="#-type">Type</a></li>
      <li>
        <details>
          <summary><a href="#-constructors">Constructors</a></summary>
          <ul>
            <li><a href="#ctor-1">IntSumExpression(int)</a></li>
            <li><a href="#ctor-2">IntSumExpression(params Func&lt;int&gt;[])</a></li>
            <li><a href="#ctor-3">IntSumExpression(IEnumerable&lt;Func&lt;int&gt;&gt;)</a></li>
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
            <li><a href="#value">Value</a></li>
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
            <li><a href="#invoke">Invoke()</a></li>
            <li><a href="#add">Add(Func&lt;int&gt;)</a></li>
            <li><a href="#addrange">AddRange(IEnumerable&lt;Func&lt;int&gt;&gt;)</a></li>
            <li><a href="#clear">Clear()</a></li>
            <li><a href="#contains">Contains(Func&lt;int&gt;)</a></li>
            <li><a href="#copyto">CopyTo(Func&lt;int&gt;[], int)</a></li>
            <li><a href="#indexof">IndexOf(Func&lt;int&gt;)</a></li>
            <li><a href="#insert">Insert(int, Func&lt;int&gt;)</a></li>
            <li><a href="#remove">Remove(Func&lt;int&gt;)</a></li>
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

## 🗂 Example of Usage

```csharp
var expression = new IntSumExpression(
    () => 2,
    () => 3,
    () => 4
);

int result = expression.Invoke(); // 9
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public class IntSumExpression : ExpressionBase<int>
```

- **Description:** Represents an expression that computes the sum of multiple <b>parameterless integer-returning</b>
  functions
- **Inheritance:** [ExpressionBase&lt;R&gt;](ExpressionBase.md)
- **Note:** Supports Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

<div id="ctor-1"></div>

#### `IntSumExpression(int)`

```csharp
public IntSumExpression(int capacity)
```

- **Description:** Initializes a new empty instance of the `IntSumExpression` class.
- **Parameter:** `capacity` — Initial capacity for the internal function list. Default is `4`.

<div id="ctor-2"></div>

#### `IntSumExpression(params Func<int>[])`

```csharp
public IntSumExpression(params Func<int>[] members)
```

- **Description:** Initializes the expression with an array of integer-returning functions.
- **Parameter:** `members` — Array of `Func<int>` delegates.

<div id="ctor-3"></div>

#### `IntSumExpression(IEnumerable<Func<int>>)`

```csharp
public IntSumExpression(IEnumerable<Func<int>> members)
```

- **Description:** Initializes the expression with a collection of integer-returning functions.
- **Parameter:** `members` — Enumerable collection of `Func<int>` delegates.

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
public event Action<int, Func<int>> OnItemChanged;
```

- **Description:** Occurs when an existing function in the expression is replaced or modified.

#### `OnItemInserted`

```csharp
public event Action<int, Func<int>> OnItemInserted;
```

- **Description:** Occurs when a new function is inserted into the expression at a specific position.

#### `OnItemDeleted`

```csharp
public event Action<int, Func<int>> OnItemDeleted;
```

- **Description:** Occurs when a function is removed from the expression.

---

### 🔑 Properties

#### `Value`

```csharp
public int Value { get; }
```

- **Description:** Evaluates all functions and returns the sum of their results.
- **Returns:** `int` — The computed sum.
- **Note:** — If no functions are present, returns `0` by default.

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
public Func<int> this[int index] { get; set; }
```

- **Description:** Indexer to access a function at a specific position.
- **Parameter:** `index` — The position of the function.
- **Returns:** `Func<int>` — The function at the given index.

---

### 🏹 Methods

#### `Invoke()`

```csharp
public int Invoke()
```

- **Description:** Evaluates all function members of the expression and returns their sum.
- **Returns:** `int` — The computed sum.
- **Note:** Returns `0` if no functions are present.

<div id="add"></div>

#### `Add(Func<int>)`

```csharp
public void Add(Func<int> item)
```

- **Description:** Adds a function to the expression.
- **Parameter:** `item` — The function to add.

<div id="addrange"></div>

#### `AddRange(IEnumerable<Func<int>>)`

```csharp
public void AddRange(IEnumerable<Func<int>> items)
```

- **Description:** Adds multiple functions to the expression at once.
- **Parameter:** `items` — An enumerable collection of `Func<int>` delegates to add.
- **Throws:** `ArgumentNullException` if `items` is `null`.

#### `Clear()`

```csharp
public void Clear()
```

- **Description:** Removes all functions from the expression.

<div id="contains"></div>

#### `Contains(Func<int>)`

```csharp
public bool Contains(Func<int> item)
```

- **Description:** Checks if the specified function exists in the expression.
- **Parameter:** `item` — The function to check.
- **Returns:** `bool` — `true` if the function exists, otherwise `false`.

<div id="copyto"></div>

#### `CopyTo(Func<int>[], int)`

```csharp
public void CopyTo(Func<int>[] array, int arrayIndex)
```

- **Description:** Copies all functions in the expression to the specified array starting at the given index.
- **Parameters:**
- `array` — The destination array.
- `arrayIndex` — The starting index in the array.

<div id="indexof"></div>

#### `IndexOf(Func<int>)`

```csharp
public int IndexOf(Func<int> item)
```

- **Description:** Returns the index of the specified function in the expression.
- **Parameter:** `item` — The function to locate.
- **Returns:** `int` — The index of the function, or `-1` if not found.

<div id="insert"></div>

#### `Insert(int, Func<int>)`

```csharp
public void Insert(int index, Func<int> item)
```

- **Description:** Inserts a function at the specified index.
- **Parameters:**
- `index` — The position at which to insert.
- `item` — The function to insert.

<div id="remove"></div>

#### `Remove(Func<int>)`

```csharp
public bool Remove(Func<int> item)
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
public IEnumerator<Func<int>> GetEnumerator()
```

- **Description:** Returns an enumerator for iterating over all function members in the expression.
- **Returns:** `IEnumerator<Func<int>>` — Enumerator over the functions.

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Releases all resources used by the expression and clears its content.  
  Also unsubscribes all event handlers.
- **Effects:**
    - Clears the function list.
    - Sets `OnItemChanged`, `OnItemInserted`, `OnItemDeleted`, and `OnStateChanged` to `null`.