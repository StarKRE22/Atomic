# 🧩 FloatMulExpression

Represents an expression that computes the <b>product</b> of multiple <b>parameterless
float-returning</b> functions

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
            <li><a href="#floatmulexpressionint">FloatMulExpression(int)</a></li>
            <li><a href="#floatmulexpressionfuncfloat">FloatMulExpression(Func&lt;float&gt;[])</a></li>
            <li><a href="#floatmulexpressionienumerablefuncfloat">FloatMulExpression(IEnumerable&lt;Func&lt;float&gt;&gt;)</a></li>
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
            <li><a href="#add">Add(Func&lt;float&gt;)</a></li>
            <li><a href="#addrange">AddRange(IEnumerable&lt;Func&lt;float&gt;&gt;)</a></li>
            <li><a href="#clear">Clear()</a></li>
            <li><a href="#contains">Contains(Func&lt;float&gt;)</a></li>
            <li><a href="#copyto">CopyTo(Func&lt;float&gt;[], int)</a></li>
            <li><a href="#indexof">IndexOf(Func&lt;float&gt;)</a></li>
            <li><a href="#insert">Insert(int, Func&lt;float&gt;)</a></li>
            <li><a href="#remove">Remove(Func&lt;float&gt;)</a></li>
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
var multiply = new FloatMulExpression(
    () => 2,
    () => 3,
    () => 4
);
float result = multiply.Invoke(); // 24
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public class FloatMulExpression : ExpressionBase<float>
```

- **Description:** Represents an expression that computes the <b>product</b> of multiple <b>parameterless
  float-returning</b> functions
- **Inheritance:** [ExpressionBase&lt;R&gt;](ExpressionBase.md)
- **Note:** Supports Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `FloatMulExpression(int)`

```csharp
public FloatMulExpression(int capacity)
```

- **Description:** Initializes a new empty instance of the `FloatMulExpression` class.
- **Parameter:** `capacity` — Initial capacity for the function list. Default is `4`.

#### `FloatMulExpression(Func<float>[])`

```csharp
public FloatMulExpression(params Func<float>[] members)
```

- **Description:** Initializes the expression with an array of float-returning functions.
- **Parameter:** `members` — Array of `Func<float>` delegates.

#### `FloatMulExpression(IEnumerable<Func<float>>)`

```csharp
public FloatMulExpression(IEnumerable<Func<float>> members)
```

- **Description:** Initializes the expression with a collection of float-returning functions.
- **Parameter:** `members` — Enumerable collection of `Func<float>` delegates.

---

### ⚡ Events

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;
```

- **Description:** Occurs when the state of the expression changes.

#### `OnItemChanged`

```csharp
public event Action<int, Func<float>> OnItemChanged;
```

- **Description:** Occurs when an existing function in the expression is replaced or modified.

#### `OnItemInserted`

```csharp
public event Action<int, Func<float>> OnItemInserted;
```

- **Description:** Occurs when a new function is inserted floato the expression at a specific position.

#### `OnItemDeleted`

```csharp
public event Action<int, Func<float>> OnItemDeleted;
```

- **Description:** Occurs when a function is removed from the expression.

---

### 🔑 Properties

#### `Value`

```csharp
public float Value { get; }
```

- **Description:** Evaluates all functions and returns the product of their results.
  If no functions are present, returns 1 by default.
- **Returns:** `float` — The computed product.

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
public Func<float> this[int index] { get; set; }
```

- **Description:** Indexer to access a function at a specific position.
- **Parameter:** `index` — The position of the function.
- **Returns:** `Func<float>` — The function at the given index.

---

### 🏹 Methods

#### `Invoke()`

```csharp
public float Invoke()
```

- **Description:** Evaluates all function members of the expression and returns their product.
- **Returns:** `float` — The computed product.
- **Note:** -Returns `1` if no functions are present.

<div id="add"></div>

#### `Add(Func<float>)`

```csharp
public void Add(Func<float> item)
```

- **Description:** Adds a function to the expression.
- **Parameter:** `item` — The function to add.

<div id="addrange"></div>

#### `AddRange(IEnumerable<Func<float>>)`

```csharp
public void AddRange(IEnumerable<Func<float>> items)
```

- **Description:** Adds multiple functions to the expression at once.
- **Parameter:** `items` — An enumerable collection of `Func<float>` delegates to add.
- **Throws:** `ArgumentNullException` if `items` is `null`.

#### `Clear()`

```csharp
public void Clear()
```

- **Description:** Removes all functions from the expression.

<div id="contains"></div>

#### `Contains(Func<float>)`

```csharp
public bool Contains(Func<float> item)
```

- **Description:** Checks if the specified function exists in the expression.
- **Parameter:** `item` — The function to check.
- **Returns:** `bool` — `true` if the function exists, otherwise `false`.

<div id="copyto"></div>

#### `CopyTo(Func<float>[], int)`

```csharp
public void CopyTo(Func<float>[] array, int arrayIndex)
```

- **Description:** Copies all functions in the expression to the specified array starting at the given index.
- **Parameters:**
- `array` — The destination array.
- `arrayIndex` — The starting index in the array.

<div id="indexof"></div>

#### `IndexOf(Func<float>)`

```csharp
public float IndexOf(Func<float> item)
```

- **Description:** Returns the index of the specified function in the expression.
- **Parameter:** `item` — The function to locate.
- **Returns:** `float` — The index of the function, or `-1` if not found.

<div id="insert"></div>

#### `Insert(int, Func<float>)`

```csharp
public void Insert(int index, Func<float> item)
```

- **Description:** Inserts a function at the specified index.
- **Parameters:**
- `index` — The position at which to insert.
- `item` — The function to insert.

<div id="remove"></div>

#### `Remove(Func<float>)`

```csharp
public bool Remove(Func<float> item)
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
public IEnumerator<Func<float>> GetEnumerator()
```

- **Description:** Returns an enumerator for iterating over all function members in the expression.
- **Returns:** `IEnumerator<Func<float>>` — Enumerator over the functions.

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Releases all resources used by the expression and clears its content.  
  Also unsubscribes all event handlers.
- **Effects:**
    - Clears the function list.
    - Sets `OnItemChanged`, `OnItemInserted`, `OnItemDeleted`, and `OnStateChanged` to `null`.