# 🧩 EntityKeyStore

Provides a bidirectional mapping between string-based entity keys and unique integer identifiers, with internal caching for fast reverse lookups.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Methods](#-methods)
    - [SetAlgorithm(IEntityKeyAlgorithm)](#setalgorithmientitykeyalgorithm)
    - [NameToId(string)](#nametoidstring)
    - [IdToName(int)](#idtonameint)
    - [Reset()](#reset)

---

## 🗂 Example of Usage

```csharp
// Convert names to IDs
int playerId = EntityKeyStore.NameToId("Player");
int enemyId = EntityKeyStore.NameToId("Enemy");

// Retrieve names from IDs
Debug.Log(EntityKeyStore.IdToName(playerId)); // "Player"
Debug.Log(EntityKeyStore.IdToName(999));      // "#Unknown:999"

// Switch the ID generation strategy
EntityKeyStore.SetAlgorithm(new SequentialEntityKeyAlgorithm());

// Clear cache and reset algorithm state
EntityKeyStore.Reset();
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public static class EntityKeyStore
```

- **Description:** Provides a bidirectional mapping between string-based entity keys and unique integer identifiers, with internal caching for fast reverse lookups.
- **Inheritance:** `object`
- **Notes:** Static dictionaries are not thread-safe; synchronize externally if used from multiple threads. The default algorithm is [SequentialEntityKeyAlgorithm](SequentialEntityKeyAlgorithm.md).
- **See also:** [TagKey](TagKey.md), [ValueKey](ValueKey.md), [IEntityKeyAlgorithm](IEntityKeyAlgorithm.md)

### 🏹 Methods

#### `SetAlgorithm(IEntityKeyAlgorithm)`

```csharp
public static void SetAlgorithm(IEntityKeyAlgorithm algorithm)
```

- **Description:** Sets the algorithm used for generating IDs from entity keys and clears the current cache.
- **Parameter:** `algorithm` — The new strategy to use.
- **Throws:** `ArgumentNullException` if `algorithm` is `null`.
- **Remarks:** Changing the algorithm resets all existing mappings.

#### `NameToId(string)`

```csharp
public static int NameToId(string name)
```

- **Description:** Converts a string entity key into a unique integer ID.
- **Parameter:** `name` — The entity key. Must not be `null`.
- **Returns:** A unique integer ID corresponding to the name.
- **Throws:** `ArgumentNullException` if `name` is `null`.
- **Behavior:**
  - Returns a cached ID if the name was already converted.
  - Otherwise computes the ID using the current algorithm, caches it, and returns it.

#### `IdToName(int)`

```csharp
public static string IdToName(int id)
```

- **Description:** Retrieves the original entity key associated with a given ID.
- **Parameter:** `id` — The integer ID to look up.
- **Returns:** The original name if registered; otherwise `#Unknown:{id}`.

#### `Reset()`

```csharp
public static void Reset()
```

- **Description:** Clears all cached mappings and resets the current algorithm.
- **Remarks:** Automatically called in the Unity Editor when entering Play Mode.
