# 🧩 ReactiveDictionary\<K, V>.ReadOnlyKeyCollection

Represents a read-only collection of keys for [ReactiveDictionary\<K, V>](ReactiveDictionary.md)

---

## 📑 Table of Contents

<ul>
  <li>
    <a href="#-api-reference">API Reference</a>
<ul>
  <li><a href="#-type">Type</a></li>

  <li>
    <details>
      <summary><a href="#-properties">Properties</a></summary>
      <ul>
        <li><a href="#count">Count</a></li>
        <li><a href="#isreadonly">IsReadOnly</a></li>
      </ul>
    </details>
  </li>

  <li>
    <details>
      <summary><a href="#-methods">Methods</a></summary>
      <ul>
        <li><a href="#containsk">Contains(K)</a></li>
        <li><a href="#copytok-int">CopyTo(K[], int)</a></li>
        <li><a href="#getenumerator">GetEnumerator()</a></li>
      </ul>
    </details>
  </li>

  <li>
    <details>
      <summary><a href="#-unsupported-methods">Unsupported Methods</a></summary>
      <ul>
        <li><a href="#addk">Add(K)</a></li>
        <li><a href="#clear">Clear()</a></li>
        <li><a href="#removek">Remove(K)</a></li>
      </ul>
    </details>
  </li>
</ul>
  </li>
</ul>


<!--
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Properties](#-properties)
        - [Count](#count)
        - [IsReadOnly](#isreadonly)
    - [Methods](#-methods)
        - [Contains(K)](#containsk)
        - [CopyTo(K[], int)](#copytok-int)
        - [GetEnumerator()](#getenumerator)
    - [Unsupported Methods](#-unsupported-methods)
        - [Add(K)](#addk)
        - [Clear()](#clear)
        - [Remove(K)](#removek)
-->
---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

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

#### `CopyTo(K[], int)`

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

### ❌ Unsupported Methods

All modification methods throw `NotSupportedException` because the collection is read-only.

#### `Add(K)`

```csharp
void ICollection<K>.Add(K item);
```

#### `Remove(K)`

```csharp
bool ICollection<K>.Remove(K item);
```

#### `Clear()`

```csharp
void ICollection<K>.Clear();
```