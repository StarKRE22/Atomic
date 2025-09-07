# 🧩 Reactive Array

A reactive array is designed to provide a fixed set of elements that can be observed.  
This is especially useful for UI rendering and event handling.  
The class follows reactive programming principles, allowing subscribers to be notified of changes.  
Instead of just storing data, `ReactiveArray<T>` supports observation and the observer pattern, making it easy to react
to element updates and global state changes.

---

## IReadOnlyReactiveArray\<T\>

Represents a **read-only** reactive array that notifies about element changes and global state changes.

### Properties

- `int Length` – the size of the array.
- `T this[int index]` – access an element by index.
- `int Count` – the size of array (`IReadOnlyCollection<T>` implementation).

### Events

- `event ChangeItemHandler<T> OnItemChanged` – triggered when an element at a specific index is changed.
- `event StateChangedHandler OnStateChanged` – triggered when the array’s **global state** changes (e.g., cleared,
  replaced, or reset).

### Methods

- `void Copy(int sourceIndex, T[] destination, int destinationIndex, int length)`  
  Copies a range of elements from this reactive array to a specified destination array.
  - `sourceIndex` – zero-based index in this array where copying starts.
  - `destination` – the target array.
  - `destinationIndex` – zero-based index in the target array where storing begins.
  - `length` – number of elements to copy.
  - **Example**:
    ```csharp
    var array = new ReactiveArray<int>(1, 2, 3, 4, 5);
    int[] target = new int[5];
    array.Copy(1, target, 0, 3); // target = [2, 3, 4, 0, 0]
    ```
---

## IReactiveArray\<T\>

Implements `IReadOnlyReactiveArray<T>` and provides write access to the array.

### Additional Members

- `new T this[int index] { get; set; }` – read / write element access.
- `void Clear()` – clears the array and triggers `OnStateChanged`.

---

## ReactiveArray\<T\>
A fixed-size reactive array that emits events when elements change.  
Supports indexed access, enumeration, and event notifications.

### Constructors

- `ReactiveArray(int capacity)`  
  Creates a new reactive array with the specified capacity.
    - Throws `ArgumentOutOfRangeException` if `capacity < 0`.


- `ReactiveArray(params T[] elements)`  
  Creates a reactive array initialized with the provided elements.

### Properties

- `int Length` – the size of the array.
- `T this[int index]` – indexed access with bounds checking.
    - Throws `IndexOutOfRangeException` if index is out of range.

### Events

- `event ChangeItemHandler<T> OnItemChanged` – raised when an element changes.
- `event StateChangedHandler OnStateChanged` – raised when the array's global state changes (clear, replace, reset).

### Methods

- `void Clear()`  
  Resets all elements to `default(T)`.
    - `OnItemChanged` is triggered only for elements that changed.
    - `OnStateChanged` is triggered once at the end.


- `void Populate(IEnumerable<T> newItems)`  
  Updates the array elements with the values from `newItems`.
  - Any existing elements that differ from the new values are replaced and `OnItemChanged` is fired for each updated element.
  - If `newItems` has fewer elements than the array length, the remaining elements are cleared (set to default) and `OnItemDeleted` is fired for each cleared element.
  - If `newItems` has more elements than the array length, an `ArgumentException` is thrown.
  - Throws `ArgumentNullException` if `newItems` is `null`.
  - `OnStateChanged` is fired once at the end.


- `void Fill(T value)`  
  Sets all elements to the specified value.
  - `OnItemChanged` is triggered for each element that changes.
  - `OnStateChanged` is triggered once at the end.


- `void Resize(int newSize)`  
  Changes the array length to `newSize`.
  - If new size is larger, new elements are initialized with `default(T)`.
  - If new size is smaller, excess elements are discarded.
  - `OnItemChanged` is triggered for all new or changed elements.
  - `OnStateChanged` is triggered once at the end.
  - Throws `ArgumentOutOfRangeException` if `newSize` is negative.

- `Enumerator GetEnumerator()`  
  Returns a lightweight struct-based enumerator.

- `void Dispose()`  
  Clears all event subscriptions.

### Example of Usage
```csharp
var array = new ReactiveArray<int>(3);

array.OnItemChanged += (index, value) => Console.WriteLine($"Item {index} changed to {value}");
array.OnStateChanged += () => Console.WriteLine("Array state changed");

// Setting an individual element
array[0] = 10; 

// Clearing the array
array.Clear();  

// Replacing all elements
array.Replace(new[] {1, 2, 3}); 

// Filling the array with a single value
array.Fill(42); 

// Resizing the array to a larger size
array.Resize(5); 

//Lightweight struct-based enumerator for efficient iteration:
foreach (var item in array)
    Console.WriteLine(item);
```

### 🔥 Performance
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

Thus, `ReactiveArray` performs almost as fast as a regular array for reading operations.  
It is well-suited for scenarios where element change notifications are needed.

However, writing to an element or iterating with `foreach` is noticeably slower due to event invocations.  
For high-performance iterations, it is recommended to use a `for` loop instead of `foreach`.
