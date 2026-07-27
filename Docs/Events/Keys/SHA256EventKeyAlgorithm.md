# 🧩 SHA256EventKeyAlgorithm

**SHA256EventKeyAlgorithm** generates deterministic integer IDs from event names using the SHA-256 hash algorithm.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [API Reference](#-api-reference)

---

## 🧩 Overview

SHA-256 provides strong hash distribution and collision resistance. This algorithm is slower than FNV-1a but produces
more robust hashes. It is suitable when deterministic, stable IDs are required.

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public sealed class SHA256EventKeyAlgorithm : IEventKeyAlgorithm
```

### 🏹 Methods

#### `NameToId(string)`

```csharp
public int NameToId(string name)
```

- **Description:** Computes a 32-bit integer from the SHA-256 hash of the event name.
- **Parameter:** `name` — The event name. Must not be `null`.

#### `Reset()`

```csharp
public void Reset()
```

- **Description:** No-op; the algorithm is stateless.
