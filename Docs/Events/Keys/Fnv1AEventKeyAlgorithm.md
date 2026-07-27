# 🧩 Fnv1AEventKeyAlgorithm

**Fnv1AEventKeyAlgorithm** generates deterministic 32-bit integer IDs from event names using the FNV-1a hash algorithm.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [API Reference](#-api-reference)

---

## 🧩 Overview

FNV-1a is a fast, non-cryptographic hash function. This algorithm produces the same ID for the same event name across
runs, making it useful when stable IDs are required.

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public sealed class Fnv1AEventKeyAlgorithm : IEventKeyAlgorithm
```

### 🏹 Methods

#### `NameToId(string)`

```csharp
public int NameToId(string name)
```

- **Description:** Computes a 32-bit FNV-1a hash for the event name.
- **Parameter:** `name` — The event name. Must not be `null`.

#### `Reset()`

```csharp
public void Reset()
```

- **Description:** No-op; the algorithm is stateless.
