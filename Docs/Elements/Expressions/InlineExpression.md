# 🧩 InlineExpression&lt;R&gt;

```csharp
[Serializable]
public class InlineExpression<R> : ExpressionBase<R>
```

- **Description:** A flexible expression that uses a <b>custom evaluation function</b> to compute a result from a list
  of parameterless functions
- **Type Parameter:** `R` — The return type of the expression.
- **Inheritance:** [ExpressionBase&lt;R&gt;](ExpressionBase.md)
- **Note:** Supports Odin Inspector

---

## 🏗️ Constructors

#### `InlineExpression(Func<Enumerator, R>, int)`

```csharp
public InlineExpression(Func<Enumerator, R> function, int capacity)
```

- **Description:** Initializes a new empty `InlineExpression` with a **custom evaluation function** and the given
  capacity.
- **Parameters:**
    - `function` — The function that defines how to evaluate the collection of functions.
    - `capacity` — Initial capacity for the internal function list. Default is `4`.

#### `InlineExpression(Func<Enumerator, R>, params Func<R>[])`

```csharp
public InlineExpression(Func<Enumerator, R> function, params Func<R>[] array)
```

- **Description:** Initializes a new instance with a **custom evaluation function** and initial array
- **Parameters:**
    - `function` — The evaluation logic to be applied to the functions.
    - `array` — An array of functions to add to the expression.

#### `InlineExpression(Func<Enumerator, R>, IEnumerable<Func<R>>)`

```csharp
public InlineExpression(Func<Enumerator, R> function, IEnumerable<Func<R>> enumerable)
```

- **Description:** Initializes a new instance with a **custom evaluation function** and initial collection of functions
- **Parameters:**
    - `function` — The evaluation logic to be applied to the functions.
    - `enumerable` — A collection of functions to add to the expression.

---

## ⚡ Events

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;
```

- **Description:** Occurs when the state of the expression changes (e.g., when functions are added, removed, or the list
  is cleared).

#### `OnItemChanged`

```csharp
public event Action<int, Func<R>> OnItemChanged;
```

- **Description:** Occurs when an existing function in the expression is replaced or modified.

#### `OnItemInserted`

```csharp
public event Action<int, Func<R>> OnItemInserted;
```

- **Description:** Occurs when a new function is inserted into the expression at a specific position.

#### `OnItemDeleted`

```csharp
public event Action<int, Func<R>> OnItemDeleted;
```

- **Description:** Occurs when a function is removed from the expression.

---

## 🔑 Properties

#### `Value`

```csharp
public R Value { get; }
```

- **Description:** Evaluates all functions and returns their computed result.
- **Returns:** `R` — The evaluated custom result.

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

## 🏷️ Indexers

#### `[int index]`

```csharp
public Func<R> this[int index] { get; set; }
```

- **Description:** Indexer to access a function at a specific position.
- **Parameter:** `index` — The position of the function.
- **Returns:** `Func<R>` — The function at the given index.

---

## 🏹 Methods

#### `Invoke()`

```csharp
public R Invoke()
```

- **Description:** Evaluates all function members of the expression.
- **Returns:** `R` — The evaluated custom result.

#### `Add(Func<R>)`

```csharp
public void Add(Func<R> item)
```

- **Description:** Adds a function to the expression.
- **Parameter:** `item` — The function to add.

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

#### `Contains(Func<R>)`

```csharp
public bool Contains(Func<R> item)
```

- **Description:** Checks if the specified function exists in the expression.
- **Parameter:** `item` — The function to check.
- **Returns:** `bool` — `true` if the function exists, otherwise `false`.

#### `CopyTo(Func<R>[], int)`

```csharp
public void CopyTo(Func<R>[] array, int arrayIndex)
```

- **Description:** Copies all functions in the expression to the specified array starting at the given index.
- **Parameters:**
    - `array` — The destination array.
    - `arrayIndex` — The starting index in the array.

#### `IndexOf(Func<R>)`

```csharp
public int IndexOf(Func<R> item)
```

- **Description:** Returns the index of the specified function in the expression.
- **Parameter:** `item` — The function to locate.
- **Returns:** `int` — The index of the function, or `-1` if not found.

#### `Insert(int, Func<R>)`

```csharp
public void Insert(int index, Func<R> item)
```

- **Description:** Inserts a function at the specified index.
- **Parameters:**
    - `index` — The position at which to insert.
    - `item` — The function to insert.

#### `Remove(Func<R>)`

```csharp
public bool Remove(Func<R> item)
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
public IEnumerator<Func<R>> GetEnumerator()
```

- **Description:** Returns an enumerator for iterating over all function members in the expression.
- **Returns:** `IEnumerator<Func<R>>` — Enumerator over the functions.

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Releases all resources used by the expression and clears its content.  
  Also unsubscribes all event handlers.
- **Effects:**
    - Clears the function list.
    - Sets `OnItemChanged`, `OnItemInserted`, `OnItemDeleted`, and `OnStateChanged` to `null`.

---

## 🗂 Example Usage

Below is an example of using `InlineExpression<R>` to extend a simple **SUM** expression:

```csharp
//Create an instance of "SUM" expression
var expression = new InlineExpression<int>(enumerator => {
    int sum = 0;
    while (enumerator.MoveNext())
        sum += enumerator.Current.Invoke();
    return sum;
});

//Add functions:
expression.Add(() => 1);
expression.Add(() => 2);
expression.Add(() => 3);

//Evaluate:
int sum = expression.Invoke(); // 1 + 2 + 3 = 6
```