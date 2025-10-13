# 🧩 ExpressionBase&lt;R&gt;

Represents a <b>parameterless expression</b> aggregating multiple functions returning a value of
type <code>R</code>

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
            <li><a href="#invokeenumerator">Invoke(Enumerator)</a></li>
            <li><a href="#add">Add(Func&lt;R&gt;)</a></li>
            <li><a href="#addrange">AddRange(IEnumerable&lt;Func&lt;R&gt;&gt;)</a></li>
            <li><a href="#clear">Clear()</a></li>
            <li><a href="#contains">Contains(Func&lt;R&gt;)</a></li>
            <li><a href="#copyto">CopyTo(Func&lt;R&gt;[], int)</a></li>
            <li><a href="#indexof">IndexOf(Func&lt;R&gt;)</a></li>
            <li><a href="#insert">Insert(int, Func&lt;R&gt;)</a></li>
            <li><a href="#remove">Remove(Func&lt;R&gt;)</a></li>
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

Below is an example of using [ExpressionBase](ExpressionBase.md) to extend a simple **logical AND** expression with multiple parameterless
boolean functions.

```csharp
// Define a concrete implementation of "ExpressionBase<bool>"
public sealed class AndExpression : ExpressionBase<bool>
{
    public AndExpression(params Func<bool>[] members) : base(members) 
    {
    }

    protected override bool Invoke(Enumerator enumerator)
    {
        while (enumerator.MoveNext())
              if (!enumerator.Current.Invoke())
                  return false;

        return true;
    }
}
```

```csharp
// "AndExpression" Usage
var expression = new AndExpression(
    () => true,
    () => true,
    () => false
);

// Evaluate the expression
bool finalResult = expression.Invoke(); // false
Console.WriteLine($"AND Expression result: {finalResult}");

// You can add more functions dynamically
expression.Add(() => true);
finalResult = expression.Invoke(); // still false
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public abstract class ExpressionBase<R> : ReactiveLinkedList<Func<R>>, IExpression<R>
```

- **Description:** Represents a <b>parameterless expression</b> aggregating multiple functions returning a value of
  type <code>R</code>
- **Inheritance:** [ReactiveLinkedList&lt;T&gt;](../Collections/ReactiveLinkedList.md),
  [IExpression&lt;R&gt;](IExpression.md)
- **Type parameter**: `R` — The return type of the expression.
- **Note:** Supports Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `Capacity-based Constructor`

```csharp
public ExpressionBase(int capacity)
```

- **Description:** Initializes a new empty expression with the specified capacity.
- **Parameter:** `capacity` — initial capacity of the underlying list. Default value is `4`.

#### `Params Constructor`

```csharp
public ExpressionBase(params Func<R>[] members)
```

- **Description:** Initializes a new expression containing the specified function members.
- **Parameter:** `members` — array of function delegates to add to the expression.
- **Throws:** `ArgumentNullException` if `members` is null.

#### `IEnumerable Constructor`

```csharp
public ExpressionBase(IEnumerable<Func<R>> members)
```

- **Description:** Initializes a new expression containing function members from the provided enumerable.
- **Parameter:** `members` — enumerable of function delegates to add to the expression.
- **Throws:** `ArgumentNullException` if `members` is null.

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
public event Action<int, Func<R>> OnItemChanged;
```

- **Description:** Occurs when an existing function delegate in the expression is replaced or modified.

#### `OnItemInserted`

```csharp
public event Action<int, Func<R>> OnItemInserted;
```

- **Description:** Occurs when a new function delegate is inserted into the expression at a specific position.

#### `OnItemDeleted`

```csharp
public event Action<int, Func<R>> OnItemDeleted;
```

- **Description:** Occurs when a function delegate is removed from the expression.

---

### 🔑 Properties

#### `Value`

```csharp
public R Value { get; }
```

- **Description:** Evaluates all functions and returns the aggregated result.
- **Returns:** `R` — The evaluated result of the expression.

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
public Func<R> this[int index] { get; set; }
```

- **Description:** Indexer to access a function at a specific position.
- **Parameter:** `index` — The position of the function.
- **Returns:** `Func<R>` — The function at the given index.

---

### 🏹 Methods

#### `Invoke()`

```csharp
public R Invoke()
```

- **Description:** Evaluates all function members of the expression and returns the aggregated result.
- **Returns:** `R` — The evaluated result of the expression.

#### `Invoke(Enumerator)`

```csharp
protected abstract R Invoke(Enumerator enumerator)
```

- **Description:** Abstract template method. Derived classes define how the parameterless functions are aggregated and
  evaluated.
- **Parameter:** `enumerator` — Enumerator over the function members.
- Returns: `R` — The aggregated result of the expression.


<div id="add"></div>

#### `Add(Func<R> item)`

```csharp
public void Add(Func<R> item)
```

- **Description:** Adds a function to the expression.
- **Parameter:** `item` — The function to add.

<div id="addrange"></div>

#### `AddRange(IEnumerable<Func<R>>)`

```csharp
public void AddRange(IEnumerable<Func<R>> items)
```

- **Description:** Adds multiple functions to the expression at once.
- **Parameter:** `items` — An enumerable collection of `Func<R>` delegates to add.
- **Throws:** `ArgumentNullException` if `items` is `null`.

#### `Clear()`

```csharp
public void Clear()
```

- **Description:** Removes all functions from the expression.

<div id="contains"></div>

#### `Contains(Func<R>)`

```csharp
public bool Contains(Func<R> item)
```

- **Description:** Checks if the specified function exists in the expression.
- **Parameter:** `item` — The function to check.
- **Returns:** `bool` — `true` if the function exists, otherwise `false`.

<div id="copyto"></div>

#### `CopyTo(Func<R>[] array, int arrayIndex)`

```csharp
public void CopyTo(Func<R>[] array, int arrayIndex)
```

- **Description:** Copies all functions in the expression to the specified array starting at the given index.
- **Parameters:**
    - `array` — The destination array.
    - `arrayIndex` — The starting index in the array.


<div id="indexof"></div>

#### `IndexOf(Func<R>)`

```csharp
public int IndexOf(Func<R> item)
```

- **Description:** Returns the index of the specified function in the expression.
- **Parameter:** `item` — The function to locate.
- **Returns:** `int` — The index of the function, or `-1` if not found.


<div id="insert"></div>

#### `Insert(int, Func<R>)`

```csharp
public void Insert(int index, Func<R> item)
```

- **Description:** Inserts a function at the specified index.
- **Parameters:**
    - `index` — The position at which to insert.
    - `item` — The function to insert.

<div id="remove"></div>

#### `Remove(Func<R>)`

```csharp
public bool Remove(Func<R> item)
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
public IEnumerator<Func<R>> GetEnumerator()
```

- **Description:** Returns an enumerator for iterating over all function members in the expression.
- **Returns:** `IEnumerator<Func<R>>` — Enumerator over the functions.

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Releases all resources used by the expression and clears its content. Also unsubscribes all event
  handlers.
- **Effects:**
    - Clears the function list.
    - Sets `OnItemChanged`, `OnItemInserted`, `OnItemDeleted`, and `OnStateChanged` to `null`.