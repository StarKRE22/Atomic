
  <summary>
    <h2>🧩 ReadOnlyValueCollection</h2>
  </summary>

```csharp
public readonly struct ReadOnlyValueCollection : ICollection<V>
```

- **Description:** <b>Represents a read-only collection of values</b>.
- **Inheritance:** `ICollection<T>`

---

### 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** Gets the number of values in the collection.

#### `IsReadOnly`

```csharp
public bool IsReadOnly { get; }
```

- **Description:** Gets a value indicating whether the collection is read-only. Always `true`.

---

### 🏹 Methods

#### `Contains(V)`

```csharp
public bool Contains(V item);
```

- **Description:** Determines whether the collection contains the specified value.
- **Parameter:** `item` — The value to locate.
- **Returns:** `true` if the value exists; otherwise `false`.

#### `CopyTo(V[] array, int arrayIndex)`

```csharp
public void CopyTo(V[] array, int arrayIndex);
```

- **Description:** Copies the values to the specified array, starting at the specified index.
- **Parameters:**
    - `array` — The destination array. Cannot be null.
    - `arrayIndex` — The zero-based index at which to begin copying. Must be non-negative.
- **Exceptions:**
    - Throws `ArgumentNullException` if `array` is null.
    - Throws `ArgumentOutOfRangeException` if `arrayIndex` is negative.

#### `GetEnumerator()`

```csharp
public Enumerator GetEnumerator();
```

- **Description:** Returns an enumerator that iterates through the values in the dictionary.
- **Returns:** An `Enumerator` struct for iterating over values.

---

### ⛔ Unsupported Methods

```csharp
void ICollection<V>.Add(V item);
void ICollection<V>.Clear();
bool ICollection<V>.Remove(V item);
```

- **Description:** All modification methods throw `NotSupportedException` because the collection is read-only.