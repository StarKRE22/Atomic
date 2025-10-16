# 🧩 IEntityNameAlgorithm

A **deterministic, stateless interface** for converting string-based entity names into integer identifiers. This
interface defines the core contract for algorithms that generate numeric IDs from entity names.

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

Below is an example of implementation that generates sequential IDs starting from a configurable initial value.

```csharp
public sealed class SequentialEntityNameAlgorithm : IEntityNameAlgorithm
{
    private const int INITIAL_ID = 1;

    private int _nextId;

    public SequentialEntityNameAlgorithm(int nextId = INITIAL_ID) => _nextId = nextId;

    public int NameToId(string name) => _nextId++;

    public void Reset() => _nextId = INITIAL_ID;
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEntityNameAlgorithm
```

- **Description:** Defines a deterministic method for converting entity names into integer IDs.
- **Thread-safety:** Implementations may or may not be thread-safe. Stateless algorithms are usually safe.

---

### 🏹 Methods

<div id="nametoid"></div>

#### `NameToId(string)`

```csharp
int NameToId(string name)
```

- **Description:** Converts a string entity name into a deterministic integer identifier.
- **Parameter:** `name` — The entity name to convert. Must not be `null`.
- **Returns:** A deterministic integer corresponding to the given entity name. Same input must always produce the same
  output.
- **Exceptions:** Throws `ArgumentNullException` if `name` is `null`.
- **Remarks:** This method is the core of any algorithm implementing this interface. Can be based on sequential IDs,
  hashes, or other deterministic schemes.

#### `Reset()`

```csharp
void Reset()
```

- **Description:** Resets the internal state of the algorithm.
- **Remarks:** Primarily useful for stateful implementations, such as sequential ID generators. Stateless algorithms can
  leave this method as a no-op.

---



## 📝 Notes

### - Deterministic Behavior

- Every algorithm implementing this interface **must produce the same integer output for the same input string**.
- Ensures consistency across the system when generating IDs for entity names.

### - Statelessness Design

- This interface is designed to support **stateless algorithms**.
- It does **not store any mappings or caches**. Caching and reverse lookups are handled externally, e.g., in
  [EntityNames](EntityNames.md).

### - Stateful Implementations

- Some implementations, like sequential ID generators, may maintain internal state.
- The `Reset()` method allows such algorithms to be reset to their initial state.