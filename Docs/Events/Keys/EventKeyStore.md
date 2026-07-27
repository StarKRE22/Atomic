# 🧩 EventKeyStore

**EventKeyStore** is a static utility class that provides a bidirectional mapping between string-based event names and
unique integer identifiers. It is used internally by [EventKey](EventKey.md) to convert event names into IDs.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Examples of Usage](#-examples-of-usage)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🧩 Overview

Event bus keys are stored internally as integer IDs. `EventKeyStore` manages the conversion between strings and IDs:

- `NameToId(name)` — converts a string to a cached integer ID.
- `IdToName(id)` — converts a cached integer ID back to its original string.
- `SetAlgorithm(algorithm)` — changes the ID generation strategy.
- `Reset()` — clears the cache and resets the algorithm state.

The default algorithm is [SequentialEventKeyAlgorithm](SequentialEventKeyAlgorithm.md), which assigns IDs starting from `1`.

---

## 🗂 Examples of Usage

### Basic Mapping

```csharp
int playerTurnId = EventKeyStore.NameToId("PlayerTurnStarted");
string name = EventKeyStore.IdToName(playerTurnId); // "PlayerTurnStarted"
```

### Switch Algorithm

```csharp
EventKeyStore.SetAlgorithm(new Fnv1AEventKeyAlgorithm());
int id = EventKeyStore.NameToId("PlayerTurnStarted"); // deterministic hash
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public static class EventKeyStore
```

### 🏹 Methods

#### `SetAlgorithm(IEventKeyAlgorithm)`

```csharp
public static void SetAlgorithm(IEventKeyAlgorithm algorithm)
```

- **Description:** Sets the algorithm used for generating IDs from event names and clears the cache.
- **Parameter:** `algorithm` — The new strategy. Must not be `null`.

#### `NameToId(string)`

```csharp
public static int NameToId(string name)
```

- **Description:** Converts a string event name into a unique integer ID.
- **Parameter:** `name` — The event name. Must not be `null`.
- **Returns:** A unique integer ID.

#### `IdToName(int)`

```csharp
public static string IdToName(int id)
```

- **Description:** Retrieves the original event name for a given ID.
- **Returns:** The original name, or `#Unknown:{id}` if not registered.

#### `Reset()`

```csharp
public static void Reset()
```

- **Description:** Clears the cache and resets the current algorithm.

---

## 📌 Best Practices

- Do **not** serialize numeric IDs — they are generated at runtime.
- Use [EventKey](EventKey.md) instead of raw string names in hot paths.
- Call `Reset()` in tests to ensure isolation.
- For deterministic IDs across runs, use [Fnv1AEventKeyAlgorithm](Fnv1AEventKeyAlgorithm.md) or
  [SHA256EventKeyAlgorithm](SHA256EventKeyAlgorithm.md).
