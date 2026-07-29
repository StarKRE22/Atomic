# 🧩 MonoEntity Casting

Provides methods for safe casting between [IEntity](IEntity.md) and [MonoEntity](MonoEntity.md).

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
  - [Simple cast to MonoEntity](#ex1)
  - [Generic cast to a specific MonoEntity type](#ex2)
  - [Safe cast using TryCast](#ex3)
  - [Safe generic cast using TryCast\<E>](#ex4)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Static Methods](#-static-methods)
    - [Cast(IEntity)](#castientity)
    - [Cast<E>(IEntity)](#casteientity)
    - [TryCast(IEntity, out MonoEntity)](#trycastientity-out-MonoEntity)
    - [TryCast<E>(IEntity, out E)](#trycasteientity-out-e)


---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Simple cast to MonoEntity

```csharp
IEntity entity = GetEntityFromRegistry();
MonoEntity MonoEntity = MonoEntity.Cast(entity);
```

> Throws an exception if `entity` is not a `MonoEntity`.

---

<div id="ex2"></div>

### 2️⃣ Generic cast to a specific MonoEntity type

```csharp
IEntity entity = GetEntityFromRegistry();
EnemyEntity enemy = MonoEntity.Cast<EnemyEntity>(entity);
```

> Throws an exception if entity is not of type `EnemyEntity` or a proxy of it.

---

<div id="ex3"></div>

### 3️⃣ Safe cast using TryCast

```csharp
IEntity entity = GetEntityFromRegistry();
if (MonoEntity.TryCast(entity, out MonoEntity MonoEntity))
    Debug.Log($"Successfully casted to MonoEntity: {MonoEntity.Name}");
else
    Debug.LogWarning("Entity is not a MonoEntity");
```

---

<div id="ex4"></div>

### 4️⃣ Safe generic cast using TryCast\<E>

```csharp
IEntity entity = GetEntityFromRegistry();
if (MonoEntity.TryCast<EnemyEntity>(entity, out EnemyEntity enemy))
    Debug.Log($"Successfully casted to EnemyEntity: {enemy.Name}");
else
    Debug.LogWarning("Entity is not of type EnemyEntity");
```

---


## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public partial class MonoEntity
```

---

### 🏹 Static Methods

#### `Cast(IEntity)`

```csharp
public static MonoEntity Cast(IEntity entity)  
```

- **Description:** Casts the specified `IEntity` to a `MonoEntity` if possible.
- **Parameter:** `entity` – The entity to cast.
- **Returns:** The entity cast to `MonoEntity`, or `null` if the input is `null`.
- **Exceptions:** Throws `InvalidCastException` if the entity cannot be cast to `MonoEntity`.
- **Note:** Uses `AggressiveInlining` for performance.

#### `Cast<E>(IEntity)`

```csharp
public static E Cast<E>(IEntity entity) where E : MonoEntity  
```

- **Description:** Casts the specified `IEntity` to the target type `E`. Supports direct `MonoEntity` instances and
  `MonoEntityProxy<E>` wrappers.
- **Type Parameter:** `E` – The type of `MonoEntity` to cast to.
- **Parameter:** `entity` – The entity to cast.
- **Returns:** The entity cast to type `E`, or `null` if the input is `null`.
- **Exceptions:** Throws `InvalidCastException` if the entity cannot be cast to the target type `E`.

#### `TryCast(IEntity, out MonoEntity)`

```csharp
public static bool TryCast(IEntity entity, out MonoEntity result)  
```

- **Description:** Attempts to cast the specified `IEntity` to a `MonoEntity`.
- **Parameters:**
    - `entity` – The entity to cast.
    - `result` – The cast result if successful; otherwise, `null`.
- **Returns:** `true` if the cast was successful; otherwise, `false`.

#### `TryCast<E>(IEntity, out E)`

```csharp
public static bool TryCast<E>(IEntity entity, out E result) where E : MonoEntity  
```

- **Description:** Attempts to cast the specified `IEntity` to the target type `E`. Supports direct `MonoEntity`
  instances and `MonoEntityProxy<E>` wrappers.
- **Type Parameter:** `E` – The type of `MonoEntity` to cast to.
- **Parameters:**
    - `entity` – The entity to cast.
    - `result` – The cast result if successful; otherwise, `null`.
- **Returns:** `true` if the cast was successful; otherwise, `false`.