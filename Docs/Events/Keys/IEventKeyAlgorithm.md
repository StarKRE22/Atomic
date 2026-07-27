# 🧩 IEventKeyAlgorithm

Defines a deterministic algorithm for converting string-based event names into integer identifiers.

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

Implement a custom algorithm:

```csharp
public sealed class LengthEventKeyAlgorithm : IEventKeyAlgorithm
{
    public int NameToId(string name) => name.Length;
    public void Reset() { }
}
```

Apply it to the store:

```csharp
EventKeyStore.SetAlgorithm(new LengthEventKeyAlgorithm());
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public interface IEventKeyAlgorithm
```

- **Description:** Defines a deterministic, stateless algorithm for converting event names into integer IDs.
- **Notes:**
  - Implementations must produce the same integer output for the same input string.
  - Caching and reverse lookup are handled by [EventKeyStore](EventKeyStore.md).
- **See also:** [EventKeyStore](EventKeyStore.md), [SequentialEventKeyAlgorithm](SequentialEventKeyAlgorithm.md),
  [Fnv1AEventKeyAlgorithm](Fnv1AEventKeyAlgorithm.md), [SHA256EventKeyAlgorithm](SHA256EventKeyAlgorithm.md)

---

### 🏹 Methods

#### `NameToId(string)`

```csharp
int NameToId(string name)
```

- **Description:** Converts an event name into a deterministic integer ID.
- **Parameter:** `name` – The event name. Must not be `null`.
- **Returns:** A deterministic integer ID.
- **Throws:** `ArgumentNullException` if `name` is `null`.

#### `Reset()`

```csharp
void Reset()
```

- **Description:** Resets internal state. Stateless algorithms may leave this as a no-op.
