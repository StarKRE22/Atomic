# 🧩 IEventKeyAlgorithm

**IEventKeyAlgorithm** defines a deterministic algorithm for converting string-based event names into integer
identifiers.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [API Reference](#-api-reference)
- [Implementations](#-implementations)

---

## 🧩 Overview

The algorithm is intentionally stateless and does not cache mappings. Caching and reverse lookup are handled by
[EventKeyStore](EventKeyStore.md). Implementations must produce the same integer output for the same input string.

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public interface IEventKeyAlgorithm
```

### 🏹 Methods

#### `NameToId(string)`

```csharp
int NameToId(string name)
```

- **Description:** Converts an event name into a deterministic integer ID.
- **Parameter:** `name` — The event name. Must not be `null`.

#### `Reset()`

```csharp
void Reset()
```

- **Description:** Resets internal state. Stateless algorithms may leave this as a no-op.

---

## 🔍 Implementations

- [SequentialEventKeyAlgorithm](SequentialEventKeyAlgorithm.md)
- [Fnv1AEventKeyAlgorithm](Fnv1AEventKeyAlgorithm.md)
- [SHA256EventKeyAlgorithm](SHA256EventKeyAlgorithm.md)
