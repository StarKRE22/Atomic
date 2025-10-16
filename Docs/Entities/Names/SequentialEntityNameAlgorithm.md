# 🧩 SequentialEntityNameAlgorithm

A **stateful, sequential algorithm** for generating unique integer IDs from string-based entity names. This algorithm
maintains an internal counter and returns IDs in increasing order.

---

## 📑 Table of Contents

<ul>
  <li><a href="#-example-of-usage">Example of Usage</a></li>
  <li>
    <a href="#-api-reference">API Reference</a>
    <ul>
      <li><a href="#-type">Type</a></li>
      <li><a href="#-methods">Methods</a></li>
          <ul>
            <li><a href="#nametoid">NameToId(string)</a></li>
            <li><a href="#reset">Reset()</a></li>
          </ul>
    </ul>
  </li>
  <li><a href="#-notes">Notes</a></li>
</ul>

---

## 🗂 Example of Usage

Set this algorithm to the [EntityNames](EntityNames.md) when your application starts:

```csharp
public static void Main(string[] args)
{
    // Use sequential ID generation for entity names
    EntityNames.SetAlgorithm(new SequentialEntityNameAlgorithm());
}
```

Usage:

```csharp
// Generate IDs for some example entity names
int playerId = EntityNames.NameToId("Player"); // 1
int enemyId  = EntityNames.NameToId("Enemy");  // 2

// Retrieve original name by ID
string name = EntityNames.IdToName(playerId);  // "Player"
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public sealed class SequentialEntityNameAlgorithm : IEntityNameAlgorithm
```

- **Description:** Provides a sequential mapping from entity names to integer IDs.
- **Thread-safety:** Not thread-safe. If used in multi-threaded scenarios, external synchronization is required.
- **Statefulness:** Maintains an internal counter that increments with each call to `NameToId`.

---

### 🏹 Methods

<div id="nametoid"></div>

#### `NameToId(string)`

```csharp
public int NameToId(string name)
```

- **Description:** Returns the next integer ID in the sequence.
- **Parameter:** `name` — The entity name to convert. Ignored in this algorithm.
- **Returns:** A unique sequential integer ID. Each call increments the internal counter by 1.
- **Remarks:** Useful when deterministic hash-based IDs are not required.  
  Sequence starts from the initial value provided in the constructor or defaults to 1.

<div id="reset"></div>

#### `Reset()`

```csharp
public void Reset()
```

- **Description:** Resets the internal counter back to the initial value.
- **Remarks:** Allows restarting the sequence from the initial ID, useful for tests or new sessions.

---

## 📝 Notes

- This algorithm is **stateful** and keeps track of the next ID internally.
- Unlike hash-based algorithms, it does **not produce deterministic IDs** for a given name; IDs depend on call order.
- Simple systems where sequential numeric identifiers are sufficient.
- Situations where reverse lookup or caching is handled externally.
- Testing or prototyping where deterministic mapping is not required.
