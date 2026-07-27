# 🧩 Fnv1AEventKeyAlgorithm

Generates deterministic 32-bit integer IDs from event names using the FNV-1a hash algorithm.

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

Use FNV-1a for stable IDs across runs:

```csharp
EventKeyStore.SetAlgorithm(new Fnv1AEventKeyAlgorithm());
int id = EventKeyStore.NameToId("PlayerTurnStarted");
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public sealed class Fnv1AEventKeyAlgorithm : IEventKeyAlgorithm
```

- **Description:** Provides a stateless algorithm that computes 32-bit FNV-1a hash IDs.
- **Inheritance:** [IEventKeyAlgorithm](IEventKeyAlgorithm.md)
- **Notes:** The same event name always produces the same ID, making it suitable for deterministic key generation.
- **See also:** [EventKeyStore](EventKeyStore.md), [SHA256EventKeyAlgorithm](SHA256EventKeyAlgorithm.md)

---

### 🏹 Methods

#### `NameToId(string)`

```csharp
public int NameToId(string name)
```

- **Description:** Computes a 32-bit FNV-1a hash for the event name.
- **Parameter:** `name` – The event name. Must not be `null`.
- **Returns:** A 32-bit integer corresponding to the FNV-1a hash of the name.
- **Throws:** `ArgumentNullException` if `name` is `null`.

#### `Reset()`

```csharp
public void Reset()
```

- **Description:** No-op; the algorithm is stateless.
