# 🧩 SequentialEventKeyAlgorithm

Generates unique integer IDs in sequential order starting from `1`.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Constructors](#-constructors)
    - [SequentialEventKeyAlgorithm(int)](#sequentialeventkeyalgorithmint)
  - [Methods](#-methods)
    - [NameToId(string)](#nametoidstring)
    - [Reset()](#reset)

---

## 🗂 Example of Usage

Use the default sequential algorithm:

```csharp
EventKeyStore.SetAlgorithm(new SequentialEventKeyAlgorithm());
int id1 = EventKeyStore.NameToId("PlayerTurnStarted"); // 1
int id2 = EventKeyStore.NameToId("PlayerTurnEnded");   // 2
```

Start from a custom value:

```csharp
EventKeyStore.SetAlgorithm(new SequentialEventKeyAlgorithm(100));
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public sealed class SequentialEventKeyAlgorithm : IEventKeyAlgorithm
```

- **Description:** Provides a stateful algorithm that returns the next integer in sequence for each event name.
- **Inheritance:** [IEventKeyAlgorithm](IEventKeyAlgorithm.md)
- **Notes:**
  - This is the default algorithm for [EventKeyStore](EventKeyStore.md).
  - IDs depend on the order of calls, so they are not deterministic across runs.
- **See also:** [EventKeyStore](EventKeyStore.md)

---

### 🏗️ Constructors

#### `SequentialEventKeyAlgorithm(int)`

```csharp
public SequentialEventKeyAlgorithm(int nextId = 1)
```

- **Description:** Creates the algorithm with an optional starting ID.
- **Parameter:** `nextId` – The first ID to assign. Defaults to `1`.

---

### 🏹 Methods

#### `NameToId(string)`

```csharp
public int NameToId(string name)
```

- **Description:** Returns the next sequential integer ID.
- **Parameter:** `name` – Ignored by this algorithm.
- **Returns:** A unique integer ID and advances the internal counter by `1`.

#### `Reset()`

```csharp
public void Reset()
```

- **Description:** Resets the internal counter back to the initial value.
