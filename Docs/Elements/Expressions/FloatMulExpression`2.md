
<details>
 <summary>
 <h2>🧩 FloatMulExpression&lt;T1, T2&gt;</h2>
 <br> Represents an expression that computes the product of float values returned from functions with <b>two input parameters</b>
 </summary>

<br>

```csharp
[Serializable]
public class FloatMulExpression<T1, T2> : ExpressionBase<T1, T2, float>
```

- **Type Parameters:**
- `T1` — The first input parameter type.
- `T2` — The second input parameter type.

---

### 🏗️ Constructors

#### `FloatMulExpression()`

```csharp
public FloatMulExpression(int capacity)
```

- **Description:** Initializes a new empty instance of the `FloatMulExpression<T1, T2>` class.
- **Parameter:** `capacity` — Initial capacity for the function list. Default is `4`.

#### `FloatMulExpression(Func<T1, T2, float>[] members)`

```csharp
public FloatMulExpression(params Func<T1, T2, float>[] members)
```

- **Description:** Initializes the expression with an array of functions that take two parameters and return an float.
- **Parameter:** `members` — Array of `Func<T1, T2, float>` delegates.

#### `FloatMulExpression(IEnumerable<Func<T1, T2, float>> members)`

```csharp
public FloatMulExpression(IEnumerable<Func<T1, T2, float>> members)
```

- **Description:** Initializes the expression with a collection of functions that take two parameters and return an
  float.
- **Parameter:** `members` — Enumerable collection of `Func<T1, T2, float>` delegates.

---

### ⚡ Events

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;
```

- **Description:** Occurs when the state of the expression changes.

#### `OnItemChanged`

```csharp
public event Action<int, Func<T1, T2, float>> OnItemChanged;
```

- **Description:** Occurs when an existing function is replaced or modified.

#### `OnItemInserted`

```csharp
public event Action<int, Func<T1, T2, float>> OnItemInserted;
```

- **Description:** Occurs when a new function is inserted.

#### `OnItemDeleted`

```csharp
public event Action<int, Func<T1, T2, float>> OnItemDeleted;
```

- **Description:** Occurs when a function is removed.

---

### 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** Gets the number of functions in the expression.
- **Returns:** `float` — Number of function members.

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
public Func<T1, T2, float> this[int index] { get; set; }
```

- **Description:** Accesses a function at a specific position.
- **Parameter:** `index` — Position of the function.
- **Returns:** `Func<T1, T2, float>` — Function at the given index.

---

### 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
public float Invoke(T1 arg1, T2 arg2)
```

- **Description:** Evaluates all functions with provided arguments.
- **Parameters:**
    - `arg1` — First input argument.
    - `arg2` — Second input argument.
- **Returns:** `float` — Computed product.
- **Note:** -Returns `1` if no functions are present.

#### `Add(Func<T1, T2, float>)`

```csharp
public void Add(Func<T1, T2, float> item)
```

- **Description:** Adds a function to the expression.
- **Parameter:** `item` — Function to add.

#### `AddRange(IEnumerable<Func<T1, T2, float>> items)`

```csharp
public void AddRange(IEnumerable<Func<T1, T2, float>> items)
```

- **Description:** Adds multiple functions.
- **Parameter:** `items` — Collection of functions.
- **Throws:** `ArgumentNullException` if `items` is null.

#### `Clear()`

```csharp
public void Clear()
```

- **Description:** Removes all functions.

#### `Contains(Func<T1, T2, float>)`

```csharp
public bool Contains(Func<T1, T2, float> item)
```

- **Description:** Checks if a function exists.
- **Returns:** `bool` — True if found.

#### `CopyTo(Func<T1, T2, float>[], int)`

```csharp
public void CopyTo(Func<T1, T2, float>[] array, int arrayIndex)
```

- **Description:** Copies all functions to the specified array starting at the given index.
- **Parameters:**
    - `array` — Destination array.
    - `arrayIndex` — Starting index in the array.

#### `IndexOf(Func<T1, T2, float>)`

```csharp
public float IndexOf(Func<T1, T2, float> item)
```

- **Description:** Returns the index of the specified function.
- **Parameter:** `item` — Function to locate.
- **Returns:** `float` — Index of the function, or `-1` if not found.

#### `Insert(int, Func<T1, T2, float>)`

```csharp
public void Insert(int index, Func<T1, T2, float> item)
```

- **Description:** Inserts a function at the specified index.
- **Parameters:**
    - `index` — Position to insert.
    - `item` — Function to insert.

#### `Remove(Func<T1, T2, float>)`

```csharp
public bool Remove(Func<T1, T2, float> item)
```

- **Description:** Removes the specified function.
- **Parameter:** `item` — Function to remove.
- **Returns:** `bool` — True if removed successfully.

#### `RemoveAt(int)`

```csharp
public void RemoveAt(int index)
```

- **Description:** Removes the function at the specified index.
- **Parameter:** `index` — Position of the function to remove.

#### `GetEnumerator()`

```csharp
public IEnumerator<Func<T1, T2, float>> GetEnumerator()
```

- **Description:** Returns an enumerator for iterating over functions.
- **Returns:** `IEnumerator<Func<T1, T2, float>>` — Enumerator over functions.

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Releases resources and clears content.
- **Effects:**
    - Clears the function list.
    - Sets event handlers to null.

---

### 🗂 Example Usage

```csharp
var expression = new FloatMulExpression<float, float>(
    (a, b) => a,
    (a, b) => b,
    (a, b) => a + b
);
float result = expression.Invoke(2, 3); // 2 * 3 * (2 + 3) = 30
```

</details>