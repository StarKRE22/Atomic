# 🧩 EventKeyStore

A static utility class that provides a bidirectional mapping between string-based event names and unique integer
identifiers. It is used internally by [EventKey](EventKey.md) to convert event names into IDs.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Methods](#-methods)
    - [SetAlgorithm(IEventKeyAlgorithm)](#setalgorithmieventkeyalgorithm)
    - [NameToId(string)](#nametoidstring)
    - [IdToName(int)](#idtonameint)
    - [Reset()](#reset)

---

## 🗂 Example of Usage

Convert a name to an ID and back:

```csharp
int playerTurnId = EventKeyStore.NameToId("PlayerTurnStarted");
string name = EventKeyStore.IdToName(playerTurnId); // "PlayerTurnStarted"
```

Switch to a deterministic hash algorithm:

```csharp
EventKeyStore.SetAlgorithm(new Fnv1AEventKeyAlgorithm());
int id = EventKeyStore.NameToId("PlayerTurnStarted");
```

Reset mappings in tests:

```csharp
EventKeyStore.Reset();
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public static class EventKeyStore
```

- **Description:** Stores the mapping between event names and integer IDs.
- **Notes:**
  - The default algorithm is [SequentialEventKeyAlgorithm](SequentialEventKeyAlgorithm.md).
  - `Reset` is automatically invoked on enter play mode in the Unity Editor.
- **See also:** [IEventKeyAlgorithm](IEventKeyAlgorithm.md), [EventKey](EventKey.md)

---

### 🏹 Methods

#### `SetAlgorithm(IEventKeyAlgorithm)`

```csharp
public static void SetAlgorithm(IEventKeyAlgorithm algorithm)
```

- **Description:** Sets the algorithm used for generating IDs from event names and clears the current cache.
- **Parameter:** `algorithm` – The new strategy. Must not be `null`.
- **Throws:** `ArgumentNullException` if `algorithm` is `null`.

#### `NameToId(string)`

```csharp
public static int NameToId(string name)
```

- **Description:** Converts a string event name into a unique integer ID, caching the result.
- **Parameter:** `name` – The event name. Must not be `null`.
- **Returns:** A unique integer ID.
- **Throws:** `ArgumentNullException` if `name` is `null`.

#### `IdToName(int)`

```csharp
public static string IdToName(int id)
```

- **Description:** Retrieves the original event name for a given ID.
- **Parameter:** `id` – The integer ID.
- **Returns:** The original name, or `#Unknown:{id}` if not registered.

#### `Reset()`

```csharp
public static void Reset()
```

- **Description:** Clears the cached mappings and resets the current algorithm state.
- **Remarks:** In the Unity Editor this method is marked with `[InitializeOnEnterPlayMode]` so it runs automatically
  when entering play mode.
