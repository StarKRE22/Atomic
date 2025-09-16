# 🧩 ReactiveArray&lt;T&gt;

`ReactiveArray<T>` represents a **fixed-size reactive array** that emits events when elements change. It provides indexed access, supports enumeration, and implements [IReactiveArray&lt;T&gt;](IReactiveArray.md) and `IDisposable`.

> [!NOTE]  
> Use this class when you need a read-write reactive array with change notifications and iteration support.

> [!TIP]
> For high-performance iterations, it is recommended to use a `for` loop instead of `foreach`.

---

## Constructors

#### `ReactiveArray(int)`
```csharp
public ReactiveArray(int capacity);
```
- **Description:** Creates a new reactive array with the specified capacity.
- **Parameters:** `capacity` — the size of the internal array. Must be non-negative.
- **Exceptions:** Throws `ArgumentOutOfRangeException` if `capacity` is negative.
- **Remarks:** The array is initialized with default values for type `T`.
- **Example of usage:**
  
  ```csharp
  var array = new ReactiveArray<int>(5); // Creates an array of length 5, all elements = 0
  ```

#### `ReactiveArray(params T[])`
```csharp
public ReactiveArray(params T[] elements);
```
- **Description:** Creates a reactive array initialized with the given elements.
- **Parameters:** `elements` — elements to initialize the array with.
- **Remarks:** The array length matches the number of provided elements.
- **Example of usage:**
  
  ```csharp
  var array = new ReactiveArray<int>(1, 2, 3, 4); // Creates an array with elements [1, 2, 3, 4]
  ```

## Events

#### `OnItemChanged`
```csharp
public event ChangeItemHandler<T> OnItemChanged;
```
- **Description:** Triggered when an element at a specific index changes.
- **Parameters:**
  - `index` — index of the changed element.
  - `newValue` — `T` the new value of the element.
- **Remarks:** See [ChangeItemHandler&lt;T&gt;](Delegates.md/#-changeitemhandlert)

#### `OnStateChanged`
```csharp
public event StateChangedHandler OnStateChanged;
```
- **Description:** Triggered when the array’s state changes globally (e.g., multiple items updated, cleared, resized, or populated).
- **Remarks:** See [StateChangedHandler](Delegates.md/#-statechangedhandler)

---

## Properties

#### `Length`
```csharp
public int Length { get; }
```
- **Description:** Gets the total number of elements in the array.

#### `Count`
```csharp
public int Count => Length;
```
- **Description:** Returns the same value as `Length`.

---

## Indexers

#### `[int index]`
```csharp
public T this[int index] { get; set; }
```
- **Description:** Gets or sets the element at the specified index.  
  Setting a value triggers `OnItemChanged` if the value changes.
- **Parameters:** `index` — zero-based index.
- **Returns:** `T` — the element at the specified index.

---

## Methods

#### `Clear()`
```csharp
public void Clear();
```
- **Description:** Resets all elements to their default values.
- **Remarks:**
  - Triggers `OnItemChanged` only for elements that were not already default.
  - Triggers `OnStateChanged` once at the end.
- **Example of usage:**
  
  ```csharp
  var array = new ReactiveArray<int>(1, 2, 3);
  array.Clear(); // All elements set to 0, OnItemChanged fired for all
  ```

#### `CopyTo(T[], int)`
```csharp
public void CopyTo(T[] array, int arrayIndex)
```
- **Description:** Copies all elements from this reactive array to the specified destination array, starting at the given index in the destination array.
- **Parameters:**
  - `array` — The destination array to copy elements into.
  - `arrayIndex` — The zero-based index in the destination array at which copying begins.
- **Exceptions:**
  - `ArgumentNullException` — Thrown if `array` is `null`.
  - `ArgumentOutOfRangeException` — Thrown if `arrayIndex` is negative.
  - `ArgumentException` — Thrown if the destination array does not have enough space to hold all elements starting at `arrayIndex`.
- **Example of Usage:**
  
  ```csharp
  var array = new ReactiveArray<int>(1, 2, 3, 4, 5);
  int[] target = new int[5];
  array.CopyTo(target, 0); // target = [1, 2, 3, 4, 5]
  ```
- **Remarks:**
  - This method ensures that all elements are copied safely to the destination array.
  - Throws descriptive exceptions if parameters are invalid.


#### `CopyTo(int, T[], int, int)`
```csharp
public void CopyTo(int sourceIndex, T[] destination, int destinationIndex, int length);
```
- **Description:** Copies a range of elements from this reactive array to a destination array.
- **Parameters:**
  - `sourceIndex` — start index in this array.
  - `destination` — the target array.
  - `destinationIndex` — start index in the destination array.
  - `length` — number of elements to copy.
- **Exceptions:** `ArgumentNullException`, `ArgumentOutOfRangeException`, `ArgumentException`.
- **Example of usage:**
  
  ```csharp
  var array = new ReactiveArray<int>(1, 2, 3, 4, 5);
  int[] target = new int[5];
  array.CopyTo(1, target, 0, 3); // target = [2, 3, 4, 0, 0]
  ```

#### `Populate(IEnumerable<T>)`
```csharp
public void Populate(IEnumerable<T> newItems);
```
- **Description:** Updates the array with new values.
- **Parameters:** `newItems` — collection of new elements.
- **Remarks:**
  - Updates existing elements that differ, triggering `OnItemChanged`.
  - Throws `ArgumentException` if collection size does not match array length.
  - Clears remaining elements if fewer items, triggering `OnItemChanged` for removed elements.
  - Always triggers `OnStateChanged` at the end.

#### `Fill(T)`
```csharp
public void Fill(T value);
```
- **Description:** Sets all elements to the specified value.
- **Parameters:** `value` — value to assign.
- **Remarks:**
  - Triggers `OnItemChanged` for each changed element.
  - Triggers `OnStateChanged` once at the end.
- **Example of usage:**
  
  ```csharp
  var array = new ReactiveArray<int>(3);
  array.Fill(42); // All elements set to 42, events triggered
  ```

#### `Resize(int)`
```csharp
public void Resize(int newSize);
```
- **Description:** Changes the size of the array.
- **Parameters:** `newSize` — new length (must be non-negative).
- **Remarks:**
  - Expands with default(T) if larger.
  - Discards excess elements if smaller.
  - Triggers `OnItemChanged` for all new or changed elements.
  - Triggers `OnStateChanged` once at the end.
- **Exceptions:** `ArgumentOutOfRangeException` if `newSize` is negative.

#### `GetEnumerator()`
```csharp
public Enumerator GetEnumerator();
```
- **Description:** Returns a struct-based enumerator for iterating over the elements of the reactive array.
- **Returns:** `Enumerator` — a lightweight struct enumerator that implements `IEnumerator<T>` for this array.
- **Remarks:**
  - The returned enumerator is value-type, avoiding heap allocations during iteration.
  - Use it in `foreach` loops or manually with `MoveNext()` and `Current`.
  - Does **not** trigger any events; it only reads the current state of the array.

#### `Dispose()`
```csharp
public void Dispose();
```
- **Description:** Clears event subscriptions and disposes the array.

---

## 🗂 Example of Usage

```csharp
// Create a reactive array with initial values
var reactiveArray = new ReactiveArray<int>(1, 2, 3, 4);

// Subscribe to events
reactiveArray.OnItemChanged += (index, value) => Console.WriteLine($"Item {index} changed to {value}");
reactiveArray.OnStateChanged += () => Console.WriteLine("Array state changed");

// Access and modify elements
reactiveArray[1] = 20; // Triggers OnItemChanged and OnStateChanged

// Fill all elements
reactiveArray.Fill(10);

// Populate new values
reactiveArray.Populate(new int[] { 5, 6, 7, 8 });

// Resize the array
reactiveArray.Resize(6); // Adds two default elements

// Clear the array
reactiveArray.Clear();

// Iterate through elements
foreach (var item in reactiveArray)
{
    Console.WriteLine(item);
}

// Copy to standard array
int[] target = new int[reactiveArray.Length];
reactiveArray.Copy(0, target, 0, reactiveArray.Length);
```

---

## 🔥 Performance
The performance comparison below was measured on a **MacBook with Apple M1** and for collections containing **1000 elements of type `object`**.  

The table shows median execution times of key operations, illustrating the overhead of the reactive wrapper.

| Operation | Array (Median μs) | ReactiveArray (Median μs) |
|-----------|-------------------|---------------------------|
| Get       | 1.10              | 1.20                      |
| Set       | 10.80             | 63.50                     |
| Copy      | 0.70              | 0.70                      |
| ForEach   | 0.80              | 7.00                      |
| For       | 0.70              | 0.70                      |
| Clear     | 0.40              | 41.50                     |

Thus, `ReactiveArray` performs almost as fast as a regular array for reading operations. It is well-suited for scenarios where element change notifications are needed.

However, **iterating** with `foreach` or **writing** to an element is **noticeably** **slower** due to event invocations.  
