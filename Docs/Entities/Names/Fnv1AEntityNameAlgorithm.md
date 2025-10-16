# 🧩 Fnv1AEntityNameAlgorithm

A **deterministic, stateless algorithm** for converting string-based entity names into 32-bit integer IDs using the
**FNV-1a hash**. This algorithm is lightweight, fast, and produces reproducible IDs for the same entity name.

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

Set this algorithm to the [EntityNames](EntityNames.md) system:

```csharp
public static void Main(string[] args)
{
    // Use FNV-1a hashing for entity name IDs
    EntityNames.SetAlgorithm(new Fnv1AEntityNameAlgorithm());
}
```

Usage:

```csharp
// Generate deterministic IDs
int playerId = EntityNames.NameToId("Player");
int enemyId  = EntityNames.NameToId("Enemy");

// Retrieve original name is not possible since algorithm is stateless
string name = EntityNames.IdToName(playerId);  // Fallback: "#Unknown:{playerId}"
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public sealed class Fnv1AEntityNameAlgorithm : IEntityNameAlgorithm
```

- **Description:** Provides a stateless 32-bit FNV-1a hash mapping from entity names to integer IDs.
- **Thread-safety:** Thread-safe. Stateless, no shared state.
- **Statefulness:** Stateless — does not store mappings or counters.

---

### 🏹 Methods

<div id="nametoid"></div>

#### `NameToId(string)`

```csharp
public int NameToId(string name)
```

- **Description:** Converts an entity name into a deterministic 32-bit integer ID using the FNV-1a hash algorithm.
- **Parameter:** `name` — The entity name to convert. Must not be `null`.
- **Returns:** A 32-bit integer derived from the FNV-1a hash of the input name.
- **Exceptions:** Throws `ArgumentNullException` if `name` is `null`.
- **Remarks:**
    - Produces the same ID for the same input string.
    - Stateless: reverse lookup is not supported.

<div id="reset"></div>

#### `Reset()`

```csharp
public void Reset()
```

- **Description:** Performs no operation.
- **Remarks:**  
  This algorithm is stateless; `Reset()` exists only to satisfy the `IEntityNameAlgorithm` interface.

---

## 📝 Notes

- Every input name will always produce the same integer ID.
- Ensures consistent ID generation across sessions or systems.
- No internal state is maintained.
- Reverse lookup is not possible; any ID-to-name mapping must be handled externally.
- Extremely fast, suitable for runtime generation of many entity IDs.
- Lightweight and suitable for memory-constrained or high-performance applications.
- Systems where deterministic ID generation is needed without maintaining internal mappings.
- Scenarios where hashing is sufficient and reverse lookup is not required.
- Networking or serialization scenarios requiring consistent numeric IDs for entity names.