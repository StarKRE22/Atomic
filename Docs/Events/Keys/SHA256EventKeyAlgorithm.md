# 🧩 SHA256EventKeyAlgorithm

Generates deterministic integer IDs from event names using the SHA-256 hash algorithm.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Methods](#-methods)
    - [NameToId(string)](#nametoidstring)
    - [Reset()](#reset)

---

## 🗂 Example of Usage

Use SHA-256 for robust deterministic IDs:

```csharp
EventKeyStore.SetAlgorithm(new SHA256EventKeyAlgorithm());
int id = EventKeyStore.NameToId("PlayerTurnStarted");
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public sealed class SHA256EventKeyAlgorithm : IEventKeyAlgorithm
```

- **Description:** Provides a stateless algorithm that derives 32-bit integer IDs from SHA-256 hashes.
- **Inheritance:** [IEventKeyAlgorithm](IEventKeyAlgorithm.md)
- **Notes:**
  - Slower than [Fnv1AEventKeyAlgorithm](Fnv1AEventKeyAlgorithm.md) but provides stronger hash distribution.
  - The same event name always produces the same ID.
- **See also:** [EventKeyStore](EventKeyStore.md), [Fnv1AEventKeyAlgorithm](Fnv1AEventKeyAlgorithm.md)

---

### 🏹 Methods

#### `NameToId(string)`

```csharp
public int NameToId(string name)
```

- **Description:** Computes a 32-bit integer from the SHA-256 hash of the event name.
- **Parameter:** `name` – The event name. Must not be `null`.
- **Returns:** A 32-bit integer derived from the first four bytes of the SHA-256 hash.
- **Throws:** `ArgumentNullException` if `name` is `null`.

#### `Reset()`

```csharp
public void Reset()
```

- **Description:** No-op; the algorithm is stateless.
