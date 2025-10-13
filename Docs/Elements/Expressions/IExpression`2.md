# 🧩 IExpression&lt;T1, T2, R&gt;

Represents an expression with <b>two input parameters</b> of types <code>T1</code> and <code>T2</code> that aggregates
multiple functions returning a value of type <code>R</code>

---

## 📑 Table of Contents

<ul>
  <li><a href="#-example-of-usage">Example of Usage</a></li>
  <li>
    <a href="#-api-reference">API Reference</a>
    <ul>
      <li><a href="#-type">Type</a></li>
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
            <li><a href="#invokeenumerator-t1-t2">Invoke(Enumerator, T1, T2)</a></li>
            <li><a href="#addfunct1-t2-r">Add(Func&lt;T1, T2, R&gt;)</a></li>
            <li><a href="#clear">Clear()</a></li>
            <li><a href="#containsfunct1-t2-r">Contains(Func&lt;T1, T2, R&gt;)</a></li>
            <li><a href="#copytofunct1-t2-r-int">CopyTo(Func&lt;T1, T2, R&gt;[], int)</a></li>
            <li><a href="#indexoffunct1-t2-r">IndexOf(Func&lt;T1, T2, R&gt;)</a></li>
            <li><a href="#insertint-funct1-t2-r">Insert(int, Func&lt;T1, T2, R&gt;)</a></li>
            <li><a href="#removefunct1-t2-r">Remove(Func&lt;T1, T2, R&gt;)</a></li>
            <li><a href="#removeatint">RemoveAt(int)</a></li>
            <li><a href="#getenumerator">GetEnumerator()</a></li>
          </ul>
        </details>
      </li>
    </ul>
  </li>
</ul>

---

## 🗂 Example of Usage

```csharp
IExpression<int, int, int> expression = ...
    
// Add some functions
expression.Add((a, b) => a + b);
expression.Add((a, b) => a * 2 + b);

// Evaluate expression
int result = sumExpression.Invoke(3, 5);
Console.WriteLine($"Result: {result}"); // Output depends on how the expression combines functions
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IExpression<T1, T2, R> : IList<Func<T1, T2, R>>, IFunction<T1, T2, R>
```

- **Description:** Represents an expression with <b>two input parameters</b> of types <code>T1</code> and <code>
  T2</code> that aggregates multiple functions returning a value of type <code>R</code>
- **Inheritance:** `IList<T>`, [IFunction&lt;T1, T2, R&gt;](../Functions/IFunction%602.md)
- **Type Parameters:**
    - `T1` — The first input parameter type.
    - `T2` — The second input parameter type.
    - `R` — The return type of the expression.

---

### ⚡ Events

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;
```

- **Description:** Occurs when the state of the expression changes (e.g., when items are added, removed, or the list is
  cleared).

#### `OnItemChanged`

```csharp
public event Action<int, Func<T1, T2, R>> OnItemChanged;
```

- **Description:** Occurs when an existing function delegate in the expression is replaced or modified.

#### `OnItemInserted`

```csharp
public event Action<int, Func<T1, T2, R>> OnItemInserted;
```

- **Description:** Occurs when a new function delegate is inserted into the expression at a specific position.

#### `OnItemDeleted`

```csharp
public event Action<int, Func<T1, T2, R>> OnItemDeleted;
```

- **Description:** Occurs when a function delegate is removed from the expression.

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

- **Description:** Evaluates all functions using the provided arguments and returns the aggregated result.
- **Parameters:**
    - `arg1` — The first input argument of type `T1`.
    - `arg2` — The second input argument of type `T2`.
- **Returns:** `R` — The aggregated result.

#### `Invoke(Enumerator, T1, T2)`

```csharp
protected abstract R Invoke(Enumerator enumerator, T1 arg1, T2 arg2)
```

- **Description:** Abstract template method. Derived classes define how the functions are aggregated.
- **Parameters:**
    - `enumerator` — Enumerator over the function members.
    - `arg1` — The first input argument.
    - `arg2` — The second input argument.
- **Returns:** `R` — The aggregated result.

#### `Add(Func<T1, T2, R>)`

```csharp
public void Add(Func<T1, T2, R> item)
```

- **Description:** Adds a function to the expression.
- **Parameter:** `item` — The function to add.

#### `Clear()`

```csharp
public void Clear()
```

- **Description:** Removes all functions from the expression.

#### `Contains(Func<T1, T2, R>)`

```csharp
public bool Contains(Func<T1, T2, R> item)
```

- **Description:** Checks if the function exists in the expression.
- **Returns:** `bool` — `true` if the function is present.

#### `CopyTo(Func<T1, T2, R>[], int)`

```csharp
public void CopyTo(Func<T1, T2, R>[] array, int arrayIndex)
```

- **Description:** Copies the functions to an array.
- **Parameters:**
    - `array` — Destination array.
    - `arrayIndex` — Starting index in the array.

#### `IndexOf(Func<T1, T2, R>)`

```csharp
public int IndexOf(Func<T1, T2, R> item)
```

- **Description:** Gets the index of a function.
- **Returns:** `int` — The index of the function, or -1 if not found.

#### `Insert(int, Func<T1, T2, R>)`

```csharp
public void Insert(int index, Func<T1, T2, R> item)
```

- **Description:** Inserts a function at a specific index.
- **Parameters:**
    - `index` — Position at which to insert.
    - `item` — Function to insert.

#### `Remove(Func<T1, T2, R>)`

```csharp
public bool Remove(Func<T1, T2, R> item)
```

- **Description:** Removes the specified function.
- **Returns:** `bool` — `true` if the function was successfully removed.

#### `RemoveAt(int)`

```csharp
public void RemoveAt(int index)
```

- **Description:** Removes the function at a specific index.
- **Parameter:** `index` — Position of the function to remove.

#### `GetEnumerator()`

```csharp
public IEnumerator<Func<T1, T2, R>> GetEnumerator()
```

- **Description:** Returns an enumerator for iterating the functions.
- **Returns:** `IEnumerator<Func<T1, T2, R>>` — Enumerator for the function members.