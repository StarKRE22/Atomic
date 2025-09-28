# 🧩 FloatMulExpression

```csharp
[Serializable]
public class FloatMulExpression : ExpressionBase<float>
```

- **Description:** Represents an expression that computes the <b>product</b> of multiple <b>parameterless
  float-returning</b> functions
- **Inheritance:** [ExpressionBase&lt;R&gt;](ExpressionBase.md)
- **Note:** Supports Odin Inspector

---

## 🏗️ Constructors

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

## ⚡ Events

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

## 🔑 Properties

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

## 🏷️ Indexers

#### `[int index]`

```csharp
public Func<float> this[int index] { get; set; }
```

- **Description:** Indexer to access a function at a specific position.
- **Parameter:** `index` — The position of the function.
- **Returns:** `Func<float>` — The function at the given index.

---

## 🏹 Methods

#### `Invoke()`

```csharp
public float Invoke()
```

- **Description:** Evaluates all function members of the expression and returns their product.
- **Returns:** `float` — The computed product.
- **Note:** -Returns `1` if no functions are present.

#### `Add(Func<float>)`

```csharp
public void Add(Func<float> item)
```

- **Description:** Adds a function to the expression.
- **Parameter:** `item` — The function to add.

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

#### `Contains(Func<float>)`

```csharp
public bool Contains(Func<float> item)
```

- **Description:** Checks if the specified function exists in the expression.
- **Parameter:** `item` — The function to check.
- **Returns:** `bool` — `true` if the function exists, otherwise `false`.

#### `CopyTo(Func<float>[], int)`

```csharp
public void CopyTo(Func<float>[] array, int arrayIndex)
```

- **Description:** Copies all functions in the expression to the specified array starting at the given index.
- **Parameters:**
- `array` — The destination array.
- `arrayIndex` — The starting index in the array.

#### `IndexOf(Func<float>)`

```csharp
public float IndexOf(Func<float> item)
```

- **Description:** Returns the index of the specified function in the expression.
- **Parameter:** `item` — The function to locate.
- **Returns:** `float` — The index of the function, or `-1` if not found.

#### `Insert(int, Func<float>)`

```csharp
public void Insert(int index, Func<float> item)
```

- **Description:** Inserts a function at the specified index.
- **Parameters:**
- `index` — The position at which to insert.
- `item` — The function to insert.

#### `Remove(Func<float>)`

```csharp
public bool Remove(Func<float> item)
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

---

## 🗂 Example Usage

```csharp
var multiply = new FloatMulExpression(
    () => 2,
    () => 3,
    () => 4
);
float result = multiply.Invoke(); // 24
```
