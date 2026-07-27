# 🧩 EntityKeyStore

**EntityKeyStore** is a static utility class that provides a bidirectional mapping between string-based entity keys and
unique integer identifiers. It caches mappings for fast reverse lookups and allows switching the underlying ID
generation algorithm.

It is used internally by [TagKey](TagKey.md) and [ValueKey](ValueKey.md) to convert string names into IDs.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Examples of Usage](#-examples-of-usage)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🧩 Overview

Entity tags and values are stored internally as integer IDs. `EntityKeyStore` manages the conversion between strings and
IDs:

- `NameToId(name)` — converts a string to a cached integer ID.
- `IdToName(id)` — converts a cached integer ID back to its original string.
- `SetAlgorithm(algorithm)` — switches the ID generation strategy.
- `Reset()` — clears the cache and resets the algorithm state.

The default algorithm is [SequentialEntityKeyAlgorithm](SequentialEntityKeyAlgorithm.md), which assigns IDs starting from `1`.

---

## 🗂 Examples of Usage

### Basic Name-to-ID Mapping

```csharp
// Convert names to IDs
int playerId = EntityKeyStore.NameToId("Player");
int enemyId = EntityKeyStore.NameToId("Enemy");

// Retrieve names from IDs
string name1 = EntityKeyStore.IdToName(playerId); // "Player"
string name2 = EntityKeyStore.IdToName(enemyId);  // "Enemy"
string unknown = EntityKeyStore.IdToName(999);     // "#Unknown:999"

// Reset cache and algorithm state
EntityKeyStore.Reset();
```

### Switching the Algorithm

```csharp
// Use a sequential ID generator
EntityKeyStore.SetAlgorithm(new SequentialEntityKeyAlgorithm());

int id1 = EntityKeyStore.NameToId("A"); // 1
int id2 = EntityKeyStore.NameToId("B"); // 2
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public static class EntityKeyStore
```

- **Thread-safety:** Static dictionaries are **not thread-safe**; synchronize externally if used from multiple threads.

---

### 🏹 Methods

#### `SetAlgorithm(IEntityKeyAlgorithm)`

```csharp
public static void SetAlgorithm(IEntityKeyAlgorithm algorithm)
```

- **Description:** Sets the algorithm used for generating IDs from entity keys and **clears the current cache**.
- **Parameter:** `algorithm` — The new strategy to use. Must not be `null`.
- **Exception:** Throws `ArgumentNullException` if `algorithm` is `null`.
- **Remarks:** Changing the algorithm resets all existing mappings.

#### `NameToId(string)`

```csharp
public static int NameToId(string name)
```

- **Description:** Converts a string entity key into a unique integer ID.
- **Parameter:** `name` — The entity key. Must not be `null`.
- **Returns:** A unique integer ID corresponding to the name.
- **Exception:** Throws `ArgumentNullException` if `name` is `null`.
- **Behavior:**
  - Returns a cached ID if the name was already converted.
  - Otherwise, computes the ID using the current algorithm, caches it, and returns it.

#### `IdToName(int)`

```csharp
public static string IdToName(int id)
```

- **Description:** Retrieves the original entity key associated with a given ID.
- **Parameter:** `id` — The integer ID to look up.
- **Returns:**
  - The original name if registered.
  - Otherwise, a fallback string in the format `#Unknown:{id}`.

#### `Reset()`

```csharp
public static void Reset()
```

- **Description:** Clears all cached mappings and resets the current algorithm.
- **Remarks:** Automatically called in the Unity Editor when entering Play Mode.

---

## 📌 Best Practices

- Do **not** serialize or save numeric IDs — they are generated at runtime.
- Use strongly-typed [TagKey](TagKey.md) and [ValueKey](ValueKey.md) instead of raw string names in hot paths.
- Call `Reset()` in tests to ensure isolation between test runs.
- Avoid switching algorithms at runtime unless necessary; it clears all cached mappings.
