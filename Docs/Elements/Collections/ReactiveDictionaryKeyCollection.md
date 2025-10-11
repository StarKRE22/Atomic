
<details>
  <summary>
    <h2>🧩 ReadOnlyKeyCollection</h2>
  </summary>

```csharp
public readonly struct ReadOnlyKeyCollection : ICollection<K>
```

- **Description:** Represents a read-only collection of keys
- **Inheritance:** `ICollection<T>`

---

### 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** Gets the number of keys in the collection.

#### `IsReadOnly`

```csharp
public bool IsReadOnly { get; }
```

- **Description:** Gets a value indicating whether the collection is read-only. Always `true`.

---

### 🏹 Methods

#### `Contains(K)`

```csharp
public bool Contains(K item);
```

- **Description:** Determines whether the collection contains the specified key.
- **Parameter:** `item` — The key to locate. Cannot be null.
- **Returns:** `true` if the key exists; otherwise `false`.

#### `CopyTo(K[] array, int arrayIndex)`

```csharp
public void CopyTo(K[] array, int arrayIndex);
```

- **Description:** Copies the keys to the specified array, starting at the specified index.
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

- **Description:** Returns an enumerator that iterates through the keys in the dictionary.
- **Returns:** An `Enumerator` struct for iterating over keys.

---

### ⛔ Unsupported Methods

```csharp
void ICollection<K>.Add(K item);
void ICollection<K>.Clear();
bool ICollection<K>.Remove(K item);
```

- **Description:** All modification methods throw `NotSupportedException` because the collection is read-only.

</details>