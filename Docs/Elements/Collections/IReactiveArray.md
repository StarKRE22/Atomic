# 🧩 IReactiveArray&lt;T&gt;

`IReactiveArray<T>` represents a **reactive array with writable access** that provides notifications when elements are modified. It extends [IReadOnlyReactiveArray&lt;T&gt;](IReadOnlyReactiveArray.md) and allows controlled modifications while triggering reactive events.

> [!NOTE]  
> Use this interface when you need read-write access and reactive updates for array elements.

---

## Events

#### `OnStateChanged`
```csharp
public event StateChangedHandler OnStateChanged;
```
- **Description:** Triggered when the array's state changes globally (e.g., multiple items updated, cleared, or reset).

#### `OnItemChanged`
```csharp
public event Action<int, T> OnItemChanged;
```
- **Description:** Triggered when an item at a specific index changes.
- **Parameters:**
  - `index` — index of the changed element.
  - `value` — `T` the new value of the element.

---

## Properties

#### `Length`
```csharp
public int Length { get; }
```
- **Description:** Gets the total number of elements in the array.

#### `Count`
```csharp
public int Count { get; }
```
- **Description:** Gets the number of elements in the collection.
- **Notes:** Implemented explicitly from `IReadOnlyCollection<T>`. Returns the same value as `Length`.

---

## Indexer

#### `[int index]`
```csharp
public T this[int index] { get; set; }
```
- **Description:** Gets or sets the element at the specified index.  
- **Parameters:** `index` — zero-based index of the element.
- **Returns:** `T` — the element at the specified index.
- **Remarks:** Setting a new value triggers the `OnItemChanged` event if the value changes.

---

## Methods

#### `Clear()`
```csharp
public void Clear();
```
- **Description:** Removes all elements from the array.
- **Remarks:** Triggers the `OnStateChanged` event.

#### `Populate(IEnumerable<T>)`
```csharp
public void Populate(IEnumerable<T> newItems);
```
- **Description:** Updates the contents of the array with values from the specified collection.
- **Parameters:** `newItems` — collection of new elements to populate the array with.
- **Remarks:** Triggers the `OnStateChanged` event.

#### `Fill(T)`
```csharp
public void Fill(T value);
```
- **Description:** Sets all elements of the array to the specified value.
- **Parameters:** `value` — the value to assign to each element.
- **Remarks:** Triggers the `OnStateChanged` event.

#### `Resize(int)`
```csharp
public void Resize(int newSize);
```
- **Description:** Changes the size of the array to the specified length.
- **Parameters:** `newSize` — new length of the array. Must be non-negative.
- **Remarks:** Triggers the `OnStateChanged` event.

#### `Contains(T)`
```csharp
public bool Contains(T item);
```
- **Description:** Determines whether the array contains a specific element.
- **Parameter:** `item` — The object to locate in the array.
- **Returns:** `true` if the item is found; otherwise, `false`.

#### `IndexOf(T)`
```csharp
public int IndexOf(T item);
```
- **Description:** Returns the index of a specific item in the array.
- **Parameter:** `item` — The object to locate in the array.
- **Returns:** The index of the item if found; otherwise, `-1`.

#### `CopyTo(T[] array, int arrayIndex)`
```csharp
public void CopyTo(T[] array, int arrayIndex)
```
- **Description:** Copies all items in the array to the specified array starting at the given index.
- **Parameters:**
    - `array` — The destination array.
    - `arrayIndex` — The starting index in the array.

#### `CopyTo(int sourceIndex, T[] destination, int destinationIndex, int length)`
```csharp
public void Copy(int sourceIndex, T[] destination, int destinationIndex, int length);
```
- **Description:** Copies a range of elements from this array to a destination array.
- **Parameters:**
    - `int sourceIndex` — starting index in this array.
    - `T[] destination` — array to copy elements to.
    - `int destinationIndex` — starting index in the destination array.
    - `int length` — number of elements to copy.
- **Remarks:** Throws exceptions if indices or lengths are invalid, or if the destination array is too small.

#### `GetEnumerator()`
```csharp
public IEnumerator<T> GetEnumerator();
```
- **Description:** Returns an enumerator that iterates through the collection.
- **Remarks:** Inherited from `IEnumerable<T>`.

---

## 🗂 Example of Usage
```csharp
// Assume we have a reactive array
IReactiveArray<int> reactiveArray = ...; // your implementation

// Subscribe to item changes
reactiveArray.OnItemChanged += (index, newValue) =>
{
    Console.WriteLine($"Item at index {index} changed to {newValue}");
};

// Subscribe to global state changes
reactiveArray.OnStateChanged += () =>
{
    Console.WriteLine("Array state changed");
};

// Access and modify elements
for (int i = 0; i < reactiveArray.Length; i++)
{
    Console.WriteLine($"Element {i}: {reactiveArray[i]}");
    reactiveArray[i] = reactiveArray[i] + 10; // triggers OnItemChanged if value changes
}

// Clear the array
reactiveArray.Clear(); // triggers OnStateChanged

// Populate the array with new values
reactiveArray.Populate(new List<int> { 1, 2, 3, 4 }); // triggers OnStateChanged

// Fill all elements with the same value
reactiveArray.Fill(99); // triggers OnStateChanged

// Resize the array
reactiveArray.Resize(6); // triggers OnStateChanged

// Check if array contains a value
if (reactiveArray.Contains(99))
{
    Console.WriteLine("Array contains 99");
}

// Get index of a value
int indexOfValue = reactiveArray.IndexOf(2);
Console.WriteLine($"Index of 2: {indexOfValue}");

// Copy elements to a standard array
int[] target = new int[reactiveArray.Length];
reactiveArray.Copy(0, target, 0, reactiveArray.Length);
Console.WriteLine("Elements copied to target array");
```