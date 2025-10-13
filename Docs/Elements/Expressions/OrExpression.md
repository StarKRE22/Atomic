# 🧩 OrExpression

Represents a <b>parameterless logical OR expression</b> aggregating multiple
<code>Func&lt;bool&gt;</code> members.

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
            <li><a href="#orexpressionint">OrExpression(int)</a></li>
            <li><a href="#orexpressionparams-funcbool">OrExpression(params Func&lt;bool&gt;[])</a></li>
            <li><a href="#orexpressionienumerable-funcbool">OrExpression(IEnumerable&lt;Func&lt;bool&gt;&gt;)</a></li>
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
            <li><a href="#addfuncbool">Add(Func&lt;bool&gt;)</a></li>
            <li><a href="#addrangeienumerablefuncbool">AddRange(IEnumerable&lt;Func&lt;bool&gt;&gt;)</a></li>
            <li><a href="#clear">Clear()</a></li>
            <li><a href="#containsfuncbool">Contains(Func&lt;bool&gt;)</a></li>
            <li><a href="#copytofuncbool-int">CopyTo(Func&lt;bool&gt;[], int)</a></li>
            <li><a href="#indexoffuncbool">IndexOf(Func&lt;bool&gt;)</a></li>
            <li><a href="#insertint-funcbool">Insert(int, Func&lt;bool&gt;)</a></li>
            <li><a href="#removefuncbool">Remove(Func&lt;bool&gt;)</a></li>
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

## 🗂 Example of Usage

In the example, we define three discount conditions for a store customer — checking if they are a VIP member, have a
discount coupon, or if there is a holiday sale.

```csharp
// Create an OrExpression for discount eligibility
OrExpression discountCondition = new OrExpression();

// Define separate conditions for applying a discount
Func<bool> isVIPCustomer     = () => customer.IsVIP;
Func<bool> hasCoupon         = () => customer.HasCoupon;
Func<bool> isHolidaySale     = () => DateTime.Now.DayOfWeek == DayOfWeek.Friday;

// Add conditions to the expression
discountCondition.Add(isVIPCustomer);
discountCondition.Add(hasCoupon);
discountCondition.Add(isHolidaySale);

// Evaluate the combined condition
bool canApplyDiscount = discountCondition.Invoke();
Console.WriteLine($"Can apply discount: {canApplyDiscount}");

// Check if a condition exists
bool contains = discountCondition.Contains(hasCoupon);
Console.WriteLine($"Contains coupon condition: {contains}");

// Remove a specific condition
discountCondition.RemoveAt(isHolidaySale);

// Insert a new one at position 1
discountCondition.Insert(1, () => customer.HasMembership);

// Enumerate all conditions
foreach (Func<bool> func in discountCondition)
    Console.WriteLine($"Condition result: {func()}");
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public class OrExpression : ExpressionBase<bool>, IPredicate
```

- **Description:** Represents a <b>parameterless logical OR expression</b> aggregating multiple
  <code>Func&lt;bool&gt;</code> members
- **Inheritance:** [ExpressionBase&lt;R&gt;](ExpressionBase.md), [IPredicate](../Functions/IPredicate.md)
- **Note:** Supports Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `OrExpression(int)`

```csharp
public OrExpression(int capacity)
```

- **Description:** Initializes a new empty `OrExpression` with the given capacity.
- **Parameter:** `capacity` — Initial capacity for the internal function list. Default is `4`.

#### `OrExpression(params Func<bool>[])`

```csharp
public OrExpression(params Func<bool>[] members)
```

- **Description:** Initializes the expression with an array of parameterless boolean-returning functions.
- **Parameter:** `members` — Array of `Func<bool>` delegates.

<div id="orexpressionienumerable-funcbool"></div>

#### `OrExpression(IEnumerable<Func<bool>>)`

```csharp
public OrExpression(IEnumerable<Func<bool>> members)
```

- **Description:** Initializes the expression with a collection of parameterless boolean-returning functions.
- **Parameter:** `members` — Enumerable of `Func<bool>` delegates.

### ⚡ Events

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;
```

- **Description:** Occurs when the state of the expression changes (e.g., when functions are added, removed, or the list
  is cleared).

#### `OnItemChanged`

```csharp
public event Action<int, Func<bool>> OnItemChanged;
```

- **Description:** Occurs when an existing function in the expression is replaced or modified.

#### `OnItemInserted`

```csharp
public event Action<int, Func<bool>> OnItemInserted;
```

- **Description:** Occurs when a new function is inserted into the expression at a specific position.

#### `OnItemDeleted`

```csharp
public event Action<int, Func<bool>> OnItemDeleted;
```

- **Description:** Occurs when a function is removed from the expression.

---

### 🔑 Properties

#### `Value`

```csharp
public bool Value { get; }
```

- **Description:** Evaluates all functions and returns `true` if all functions return `true`.  
  If no functions are present, returns `true` by default.
- **Returns:** `bool` — The evaluated logical AND result.

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
public Func<bool> this[int index] { get; set; }
```

- **Description:** Indexer to access a function at a specific position.
- **Parameter:** `index` — The position of the function.
- **Returns:** `Func<bool>` — The function at the given index.

---

### 🏹 Methods

#### `Invoke()`

```csharp
public bool Invoke()
```

- **Description:** Evaluates all function members of the expression.  
  Returns `false` immediately if any function evaluates to `false`; otherwise returns `true`.
- **Returns:** `bool` — The aggregated logical AND result.

#### `Add(Func<bool>)`

```csharp
public void Add(Func<bool> item)
```

- **Description:** Adds a function to the expression.
- **Parameter:** `item` — The function to add.

#### `AddRange(IEnumerable<Func<bool>>)`

```csharp
public void AddRange(IEnumerable<Func<bool>> items)
```

- **Description:** Adds multiple functions to the expression at once.
- **Parameter:** `items` — An enumerable collection of `Func<bool>` delegates to add.
- **Throws:** `ArgumentNullException` if `items` is `null`.

#### `Clear()`

```csharp
public void Clear()
```

- **Description:** Removes all functions from the expression.

#### `Contains(Func<bool>)`

```csharp
public bool Contains(Func<bool> item)
```

- **Description:** Checks if the specified function exists in the expression.
- **Parameter:** `item` — The function to check.
- **Returns:** `bool` — `true` if the function exists, otherwise `false`.

#### `CopyTo(Func<bool>[], int)`

```csharp
public void CopyTo(Func<bool>[] array, int arrayIndex)
```

- **Description:** Copies all functions in the expression to the specified array starting at the given index.
- **Parameters:**
    - `array` — The destination array.
    - `arrayIndex` — The starting index in the array.

#### `IndexOf(Func<bool>)`

```csharp
public int IndexOf(Func<bool> item)
```

- **Description:** Returns the index of the specified function in the expression.
- **Parameter:** `item` — The function to locate.
- **Returns:** `int` — The index of the function, or `-1` if not found.

#### `Insert(int, Func<bool>)`

```csharp
public void Insert(int index, Func<bool> item)
```

- **Description:** Inserts a function at the specified index.
- **Parameters:**
    - `index` — The position at which to insert.
    - `item` — The function to insert.

#### `Remove(Func<bool>)`

```csharp
public bool Remove(Func<bool> item)
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
public IEnumerator<Func<bool>> GetEnumerator()
```

- **Description:** Returns an enumerator for iterating over all function members in the expression.
- **Returns:** `IEnumerator<Func<bool>>` — Enumerator over the functions.

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Releases all resources used by the expression and clears its content.  
  Also unsubscribes all event handlers.
- **Effects:**
    - Clears the function list.
    - Sets `OnItemChanged`, `OnItemInserted`, `OnItemDeleted`, and `OnStateChanged` to `null`.