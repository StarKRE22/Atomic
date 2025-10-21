# 🧩 IEntity Behaviour Extensions

Provide extension methods for [IEntity](IEntity.md) to simplify operations with behaviours.

---

## 📑 Table of Contents

- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [AddBehaviour<T>()](#addbehaviourt)
        - [AddBehaviours(IEntityBehaviour[], int, int)](#addbehavioursientitybehaviour-int-int)
        - [AddBehaviours(IEnumerable<IEntityBehaviour>)](#addbehavioursienumerableientitybehaviour)
        - [DelBehaviours(IEnumerable<IEntityBehaviour>)](#delbehavioursienumerableientitybehaviour)
        - [DelBehaviours(IEntityBehaviour[], int, int)](#delbehavioursientitybehaviour-int-int)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public partial class Extensions
```

---

### 🏹 Methods

#### `AddBehaviour<T>()`

```csharp
public static void AddBehaviour<T>(this IEntity entity) where T : IEntityBehaviour, new()
```

- **Description:** Adds a behaviour of the specified type to the entity.
- **Type Parameter:** `T` – The type of behaviour to add, must implement `IEntityBehaviour` and have a parameterless
  constructor.

#### `AddBehaviours(IEntityBehaviour[], int, int)`

```csharp
public static void AddBehaviours(this IEntity entity, IEntityBehaviour[] behaviours, int startIndex, int count)
```

- **Description:** Adds a subset of behaviours from an array to the specified entity.
- **Parameter:** `behaviours` – An array of behaviours to add. Can be `null`, in which case nothing is added.
- **Parameter:** `startIndex` – The starting index in the `behaviours` array.
- **Parameter:** `count` – The number of behaviours to add from `startIndex`.
- **Remarks:** Behaviours are added in order from `startIndex` up to `startIndex + count`.

#### `AddBehaviours(IEnumerable<IEntityBehaviour>)`

```csharp
public static void AddBehaviours(this IEntity entity, IEnumerable<IEntityBehaviour> behaviours)
```

- **Description:** Adds multiple behaviours to the entity.
- **Parameter:** `behaviours` – A collection of behaviours to add. Can be `null`, in which case nothing is added.

#### `DelBehaviours(IEnumerable<IEntityBehaviour>)`

```csharp
public static void DelBehaviours(this IEntity entity, IEnumerable<IEntityBehaviour> behaviours)
```

- **Description:** Removes multiple behaviours from the entity.
- **Parameter:** `behaviours` – A collection of behaviours to remove. Can be `null`, in which case nothing is removed.

#### `DelBehaviours(IEntityBehaviour[], int, int)`

```csharp
public static void DelBehaviours(this IEntity entity, IEntityBehaviour[] behaviours, int startIndex, int count)
```

- **Description:** Removes a subset of behaviours from an array in the entity.
- **Parameter:** `behaviours` – An array of behaviours to remove. Can be `null`, in which case nothing is removed.
- **Parameter:** `startIndex` – The starting index in the `behaviours` array.
- **Parameter:** `count` – The number of behaviours to remove from `startIndex`.