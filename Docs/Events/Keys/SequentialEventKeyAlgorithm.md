# 🧩 SequentialEventKeyAlgorithm

**SequentialEventKeyAlgorithm** generates unique integer IDs in sequential order starting from `1`.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [API Reference](#-api-reference)

---

## 🧩 Overview

This is the default algorithm for [EventKeyStore](EventKeyStore.md). It is stateful: each call to `NameToId` increments
an internal counter. IDs are not deterministic across runs because they depend on the order of calls.

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public sealed class SequentialEventKeyAlgorithm : IEventKeyAlgorithm
```

### 🏗️ Constructor

```csharp
public SequentialEventKeyAlgorithm(int nextId = 1)
```

- **Description:** Creates the algorithm with an optional starting ID.

### 🏹 Methods

#### `NameToId(string)`

```csharp
public int NameToId(string name)
```

- **Description:** Returns the next sequential integer ID.
- **Parameter:** `name` — Ignored.

#### `Reset()`

```csharp
public void Reset()
```

- **Description:** Resets the counter to the initial value.
