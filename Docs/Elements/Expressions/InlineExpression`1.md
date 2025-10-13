# 🧩 InlineExpression&lt;T, R&gt;

Represents an expression that uses a <b>custom evaluation function</b> to compute a result from a
list of functions of type <code>Func&lt;T, R&gt;</code>.

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
            <li><a href="#invoket">Invoke(T)</a></li>
            <li><a href="#addfunct-r">Add(Func&lt;T, R&gt;)</a></li>
            <li><a href="#addrange">AddRange(IEnumerable&lt;Func&lt;T, R&gt;&gt;)</a></li>
            <li><a href="#clear">Clear()</a></li>
            <li><a href="#containsfunct-r">Contains(Func&lt;T, R&gt;)</a></li>
            <li><a href="#copytofunct-r-int">CopyTo(Func&lt;T, R&gt;[], int)</a></li>
            <li><a href="#indexoffunct-r">IndexOf(Func&lt;T, R&gt;)</a></li>
            <li><a href="#insertint-funct-r">Insert(int, Func&lt;T, R&gt;)</a></li>
            <li><a href="#removefunct-r">Remove(Func&lt;T, R&gt;)</a></li>
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

## 🗂 Example Usage

Below is an example of using `InlineExpression<T, R>` to extend a simple **PRODUCT** expression:

```csharp
//Create an instance of "PRODUCT" expression
var expression = new InlineExpression<int, int>((enumerator, x) => {
    int product = 1;
    while (enumerator.MoveNext())
        product *= enumerator.Current.Invoke(x);
    return product;
});

//Add functions:
expression.Add(x => x + 1);
expression.Add(x => x + 2);

//Evaluate:
int product = expression.Invoke(2); // (2 + 1) * (2 + 2) = 12
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public class InlineExpression<T, R> : ExpressionBase<T, R>
```

- **Description:** Represents an expression that uses a <b>custom evaluation function</b> to compute a result from a
  list of functions of type <code>Func&lt;T, R&gt;</code>.
- **Type Parameters:**
    - `T` — The input type of the expression.
    - `R` — The return type of the expression.
- **Inheritance:** [ExpressionBase&lt;T, R&gt;](ExpressionBase%601.md)
- **Note:** Supports Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `Capacity-based Constructor`

```csharp
public InlineExpression(Func<Enumerator, T, R> function, int capacity)
```

- **Description:** Initializes a new empty `InlineExpression` with a **custom evaluation function** and the given
  capacity.
- **Parameters:**
    - `function` — The function that defines how to evaluate the collection of functions.
    - `capacity` — Initial capacity for the internal function list. Default is `4`.

#### `Params Constructor`

```csharp
public InlineExpression(Func<Enumerator, T, R> function, params Func<T, R>[] array)
```

- **Description:** Initializes a new instance with a **custom evaluation function** and initial array
- **Parameters:**
    - `function` — The evaluation logic to be applied to the functions.
    - `array` — An array of functions to add to the expression.

#### `IEnumerable Constructor`

```csharp
public InlineExpression(Func<Enumerator, T, R> function, IEnumerable<Func<T, R>> enumerable)
```

- **Description:** Initializes a new instance with a **custom evaluation function** and initial collection of functions
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
public event Action<int, Func<T, R>> OnItemChanged;
```

- **Description:** Occurs when an existing function in the expression is replaced or modified.

#### `OnItemInserted`

```csharp
public event Action<int, Func<T, R>> OnItemInserted;
```

- **Description:** Occurs when a new function is inserted into the expression at a specific position.

#### `OnItemDeleted`

```csharp
public event Action<int, Func<T, R>> OnItemDeleted;
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
public Func<T, R> this[int index] { get; set; }
```

- **Description:** Indexer to access a function at a specific position.
- **Parameter:** `index` — The position of the function.
- **Returns:** `Func<T, R>` — The function at the given index.

---

### 🏹 Methods

#### `Invoke(T)`

```csharp
public R Invoke(T arg)
```

- **Description:** Evaluates all function members of the expression with the given input.
- **Parameter:** `T` — The input parameter.
- **Returns:** `R` — The evaluated custom result.

#### `Add(Func<T, R>)`

```csharp
public void Add(Func<T, R> item)
```

- **Description:** Adds a function to the expression.
- **Parameter:** `item` — The function to add.

<div id="addrange"></div>

#### `AddRange(IEnumerable<Func<T, R>> items)`

```csharp
public void AddRange(IEnumerable<Func<T, R>> items)
```

- **Description:** Adds multiple functions to the expression at once.
- **Parameter:** `items` — An enumerable collection of `Func<T, R>` delegates to add.
- **Throws:** `ArgumentNullException` if `items` is `null`.

#### `Clear()`

```csharp
public void Clear()
```

- **Description:** Removes all functions from the expression.

#### `Contains(Func<T, R>)`

```csharp
public bool Contains(Func<T, R> item)
```

- **Description:** Checks if the specified function exists in the expression.
- **Parameter:** `item` — The function to check.
- **Returns:** `bool` — `true` if the function exists, otherwise `false`.

#### `CopyTo(Func<T, R>[], int)`

```csharp
public void CopyTo(Func<T, R>[] array, int arrayIndex)
```

- **Description:** Copies all functions in the expression to the specified array starting at the given index.
- **Parameters:**
    - `array` — The destination array.
    - `arrayIndex` — The starting index in the array.

#### `IndexOf(Func<T, R>)`

```csharp
public int IndexOf(Func<T, R> item)
```

- **Description:** Returns the index of the specified function in the expression.
- **Parameter:** `item` — The function to locate.
- **Returns:** `int` — The index of the function, or `-1` if not found.

#### `Insert(int, Func<T, R>)`

```csharp
public void Insert(int index, Func<T, R> item)
```

- **Description:** Inserts a function at the specified index.
- **Parameters:**
    - `index` — The position at which to insert.
    - `item` — The function to insert.

#### `Remove(Func<T, R>)`

```csharp
public bool Remove(Func<T, R> item)
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
public IEnumerator<Func<T, R>> GetEnumerator()
```

- **Description:** Returns an enumerator for iterating over all function members in the expression.
- **Returns:** `IEnumerator<Func<T, R>>` — Enumerator over the functions.

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Releases all resources used by the expression and clears its content.  
  Also unsubscribes all event handlers.
- **Effects:**
    - Clears the function list.
    - Sets `OnItemChanged`, `OnItemInserted`, `OnItemDeleted`, and `OnStateChanged` to `null`.