# 🧩️ Entity Name

**Entity Name** provides structured, deterministic algorithms to convert string-based entity names into unique
integer identifiers. The algorithms can be **stateful** (like sequential counters) or **stateless** (like hash-based algorithms)
and are designed for **high performance**, with optional caching handled externally, e.g.,
via [EntityNames](EntityNames.md).

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Sequential Algorithm](#sequential-usage)
    - [SHA256 Algorithm](#hash-usage)
    - [Fnv1A Algorithm](#fnv-usage)
- [Algorithm Comparison](#-algorithm-comparison)
- [API Reference](#-api-reference)
- [Notes](#-notes)

---

## 🗂 Examples of Usage

<div id="sequential-usage"></div>

### 1️⃣ Sequential Algorithm

```csharp
EntityNames.SetAlgorithm(new SequentialEntityNameAlgorithm());

// Generate IDs
int playerId = EntityNames.NameToId("Player"); // 1
int enemyId  = EntityNames.NameToId("Enemy");  // 2

// Retrieve original name by ID
string name = EntityNames.IdToName(playerId); // "Player"
```

<div id="hash-usage"></div>

### 2️⃣ SHA256 Algorithm

```csharp
EntityNames.SetAlgorithm(new SHA256EntityNameAlgorithm());

// Generate deterministic IDs
int playerId = EntityNames.NameToId("Player");
int enemyId  = EntityNames.NameToId("Enemy");

// Retrieve original name
string name = EntityNames.IdToName(playerId);
```

<div id="fnv-usage"></div>

### 3️⃣ Fnv1A Algorithm

```csharp
EntityNames.SetAlgorithm(new Fnv1AEntityNameAlgorithm());

// Generate deterministic IDs
int playerId = EntityNames.NameToId("Player");
int enemyId  = EntityNames.NameToId("Enemy");

// Retrieve original name
string name = EntityNames.IdToName(playerId);
```

---

## ⚖️ Algorithm Comparison

| Algorithm  | Deterministic | Stateful | Typical Use Case                     |
|------------|---------------|----------|--------------------------------------|
| Sequential | No            | Yes      | Simple sequential mapping            |
| SHA256     | Yes           | No       | Deterministic hash IDs (SHA-256)     |
| Fnv1A      | Yes           | No       | Fast deterministic hash IDs (FNV-1a) |

- **SHA-256**: Cryptographically strong, slower, more collision-resistant.
- **FNV-1a**: Non-cryptographic, extremely fast, good for small to medium sets of names.
- **Sequential**: Fastest for small sequences, not deterministic across runs.

---

## 🔍 API Reference

- [EntityNames](EntityNames.md)
- **Algorithms**
    - [IEntityNameAlgorithm](IEntityNameAlgorithm.md)
    - [SequentialEntityNameAlgorithm](SequentialEntityNameAlgorithm.md)
    - [SHA256EntityNameAlgorithm](SHA256EntityNameAlgorithm.md)
    - [Fnv1AEntityNameAlgorithm](Fnv1AEntityNameAlgorithm.md)

---

## 📝 Notes

- [EntityNames](EntityNames.md) handles **caching** and **reverse lookup**; algorithms themselves may be stateless.
- Stateless algorithms ([SHA256EntityNameAlgorithm](SHA256EntityNameAlgorithm.md), [Fnv1AEntityNameAlgorithm](Fnv1AEntityNameAlgorithm.md)) rely on [EntityNames](EntityNames.md) for caching.
- Sequential algorithm is **stateful**, so order of calls affects IDs.
- FNV-1a is extremely fast but less collision-resistant than SHA-256.
- SHA-256 is slower but produces stronger hashes, better for deterministic scenarios with large datasets.
- Algorithms can be swapped at runtime, but resetting [EntityNames](EntityNames.md) is recommended.