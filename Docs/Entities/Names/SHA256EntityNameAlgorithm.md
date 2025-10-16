# 🧩 SHA256EntityNameAlgorithm

A **deterministic, stateless algorithm** for converting string-based entity names into integer IDs using the **SHA-256**
hash function. This algorithm guarantees that the same entity name always produces the same integer ID.

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

Set this algorithm to the [EntityNames](EntityNames.md) system during initialization:

```csharp
public static void Main(string[] args)
{
    // Use SHA-256 hashing for entity name IDs
    EntityNames.SetAlgorithm(new SHA256EntityNameAlgorithm());
}
```

Usage example:

```csharp
// Generate deterministic IDs for entity names
int playerId = EntityNames.NameToId("Player");
int enemyId  = EntityNames.NameToId("Enemy");

// Retrieve original name by ID
string name = EntityNames.IdToName(playerId);  // "Player"
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public sealed class SHA256EntityNameAlgorithm : IEntityNameAlgorithm
```

- **Description:** Provides a hash-based conversion from entity names to integer IDs using the **SHA-256** cryptographic
  hash.
- **Thread-safety:** Thread-safe due to use of static SHA256 instance with local buffer usage.
- **Statefulness:** Stateless — no internal counters or caches are maintained.

---

### 🏹 Methods

<div id="nametoid"></div>

#### `NameToId(string)`

```csharp
public int NameToId(string name)
```

- **Description:** Converts an entity name into a deterministic integer ID by computing its SHA-256 hash and taking the
  first 4 bytes as a 32-bit integer.
- **Parameter:** `name` — The entity name to convert. Must not be `null`.
- **Returns:** A deterministic 32-bit integer derived from the entity name.
- **Exceptions:** Throws `ArgumentNullException` if `name` is `null`.
- **Remarks:**
    - Produces the same ID for the same input name across sessions.
    - Collisions are possible since only the first 4 bytes of the hash are used.

<div id="reset"></div>

#### `Reset()`

```csharp
public void Reset()
```

- **Description:** Performs no operation.
- **Remarks:**  
  This algorithm is **stateless** — there is no internal state to reset.  
  The method exists only to comply with the `IEntityNameAlgorithm` interface.

---

## 📝 Notes

- The same input name will **always** yield the same output ID.
- Useful for consistent, reproducible mappings across sessions or systems.
- Unlike sequential algorithms, `SHA256EntityNameAlgorithm` does **not** maintain or depend on internal state.
- Perfect for deterministic systems where ID order doesn’t matter.
- Slightly slower than sequential algorithms due to cryptographic hashing.
- Suitable when stability and reproducibility are more important than raw performance.
- Useful for persistent or networked systems requiring consistent IDs across runs.
- Useful for scenarios where entity names should always resolve to the same ID value (e.g., serialization, debugging, or
  syncing).
