# 🧩 InlineExpression&lt;T1, T2, R&gt;

Represents an expression that uses a <b>custom evaluation function</b> to compute a result from a
list of functions of type <code>Func&lt;T1, T2, R&gt;</code>.

---

## 📑 Table of Contents

<ul>
  <li><a href="#-example-usage">Example Usage</a></li>
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
            <li><a href="#int-index">[int index]</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-methods">Methods</a></summary>
          <ul>
            <li><a href="#invoket1-t2">Invoke(T1, T2)</a></li>
            <li><a href="#add">Add(Func&lt;T1, T2, R&gt;)</a></li>
            <li><a href="#addrange">AddRange(IEnumerable&lt;Func&lt;T1, T2, R&gt;&gt;)</a></li>
            <li><a href="#clear">Clear()</a></li>
            <li><a href="#contains">Contains(Func&lt;T1, T2, R&gt;)</a></li>
            <li><a href="#copyto">CopyTo(Func&lt;T1, T2, R&gt;[], int)</a></li>
            <li><a href="#indexof">IndexOf(Func&lt;T1, T2, R&gt;)</a></li>
            <li><a href="#insert">Insert(int, Func&lt;T1, T2, R&gt;)</a></li>
            <li><a href="#remove">Remove(Func&lt;T1, T2, R&gt;)</a></li>
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

## 🗂 Example Usage

Below is an example of using `InlineExpression<T1, T2, R>` to extend a simple **SUM** expression:

```csharp
var expression = new InlineExpression<int, int, int>((enumerator, x, y) => {
    int sum = 0;
    while (enumerator.MoveNext())
        sum += enumerator.Current.Invoke(x, y);
    return sum;
});

//Add functions:
expression.Add((a, b) => a + b);
expression.Add((a, b) => a * b);

//Evaluate:
int result = expression.Invoke(2, 3); // (2 + 3) + (2 * 3) = 11
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public class InlineExpression<T1, T2, R> : ExpressionBase<T1, T2, R>
```

- **Description:** Represents an expression that uses a <b>custom evaluation function</b> to compute a result from a
  list of functions of type <code>Func&lt;T1, T2, R&gt;</code>.
- **Type Parameters:**
    - `T1` — The first input type of the expression.
    - `T2` — The second input type of the expression.
    - `R` — The return type of the expression.
- **Inheritance:** [ExpressionBase&lt;T1, T2, R&gt;](ExpressionBase%602.md)
- **Note:** Supports Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `Capacity-based Constructor`

```csharp
public InlineExpression(Func<Enumerator, T1, T2, R> function, int capacity)
```

- **Description:** Initializes a new empty `InlineExpression` with a **custom evaluation function** and the given
  capacity.
- **Parameters:**
    - `function` — The function that defines how to evaluate the collection of functions.
    - `capacity` — Initial capacity for the internal function list. Default is `4`.

#### `Params Constructor`

```csharp
public InlineExpression(Func<Enumerator, T1, T2, R> function, params Func<T1, T2, R>[] array)
```

- **Description:** Initializes a new instance with a **custom evaluation function** and initial array of functions.
- **Parameters:**
    - `function` — The evaluation logic to be applied to the functions.
    - `array` — An array of functions to add to the expression.

#### `IEnumerable Constructor`

```csharp
public InlineExpression(Func<Enumerator, T1, T2, R> function, IEnumerable<Func<T1, T2, R>> enumerable)
```

- **Description:** Initializes a new instance with a **custom evaluation function** and an initial collection of
  functions.
- **Parameters:**
    - `function` — The evaluation logic to be applied to the functions.
    - `enumerable` — A collection of functions to add to the expression.

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
public event Action<int, Func<T1, T2, R>> OnItemChanged;
```

- **Description:** Occurs when an existing function in the expression is replaced or modified.

#### `OnItemInserted`

```csharp
public event Action<int, Func<T1, T2, R>> OnItemInserted;
```

- **Description:** Occurs when a new function is inserted into the expression at a specific position.

#### `OnItemDeleted`

```csharp
public event Action<int, Func<T1, T2, R>> OnItemDeleted;
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

- **Description:** Evaluates all function members of the expression with the given input parameters.
- **Parameters:**
    - `arg1` — The first input parameter.
    - `arg2` — The second input parameter.
- **Returns:** `R` — The evaluated custom result.

            
<div id="add"></div>

#### `Add(Func<T1, T2, R> item)`

```csharp
public void Add(Func<T1, T2, R> item)
```

- **Description:** Adds a function to the expression.
- **Parameter:** `item` — The function to add.

<div id="addrange"></div>

#### `AddRange(IEnumerable<Func<T1, T2, R>> items)`

```csharp
public void AddRange(IEnumerable<Func<T1, T2, R>> items)
```

- **Description:** Adds multiple functions to the expression at once.
- **Parameter:** `items` — An enumerable collection of `Func<T1, T2, R>` delegates to add.
- **Throws:** `ArgumentNullException` if `items` is `null`.

#### `Clear()`

```csharp
public void Clear()
```

- **Description:** Removes all functions from the expression.

<div id="contains"></div>

#### `Contains(Func<T1, T2, R> item)`

```csharp
public bool Contains(Func<T1, T2, R> item)
```

- **Description:** Checks if the specified function exists in the expression.
- **Parameter:** `item` — The function to check.
- **Returns:** `bool` — `true` if the function exists, otherwise `false`.

<div id="copyto"></div>

#### `CopyTo(Func<T1, T2, R>[] array, int arrayIndex)`

```csharp
public void CopyTo(Func<T1, T2, R>[] array, int arrayIndex)
```

- **Description:** Copies all functions in the expression to the specified array starting at the given index.
- **Parameters:**
    - `array` — The destination array.
    - `arrayIndex` — The starting index in the array.

<div id="indexof"></div>

#### `IndexOf(Func<T1, T2, R> item)`

```csharp
public int IndexOf(Func<T1, T2, R> item)
```

- **Description:** Returns the index of the specified function in the expression.
- **Parameter:** `item` — The function to locate.
- **Returns:** `int` — The index of the function, or `-1` if not found.

<div id="insert"></div>

#### `Insert(int index, Func<T1, T2, R> item)`

```csharp
public void Insert(int index, Func<T1, T2, R> item)
```

- **Description:** Inserts a function at the specified index.
- **Parameters:**
    - `index` — The position at which to insert.
    - `item` — The function to insert.

<div id="remove"></div>

#### `Remove(Func<T1, T2, R> item)`

```csharp
public bool Remove(Func<T1, T2, R> item)
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
public IEnumerator<Func<T1, T2, R>> GetEnumerator()
```

- **Description:** Returns an enumerator for iterating over all function members in the expression.
- **Returns:** `IEnumerator<Func<T1, T2, R>>` — Enumerator over the functions.

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Releases all resources used by the expression and clears its content.  
  Also unsubscribes all event handlers.
- **Effects:**
    - Clears the function list.
    - Sets `OnItemChanged`, `OnItemInserted`, `OnItemDeleted`, and `OnStateChanged` to `null`.