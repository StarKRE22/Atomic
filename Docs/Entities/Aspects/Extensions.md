# 🧩 Entity Extensions

Provides utility methods for **applying** and **discarding** [IEntityAspect](../Aspects/IEntityAspect.md) instances to
and from [IEntity](../Entities/IEntity.md) objects. These extensions simplify aspect management and are optimized with
aggressive inlining.

---

## 📑 Table of Contents

- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Apply(IEntity, IEntityAspect)](#applyientity-ientityaspect)
        - [Apply<E>(E, IEntityAspect<E>)](#applyee-ientityaspecte)
        - [Discard(IEntity, IEntityAspect)](#discardientity-ientityaspect)
        - [Discard<E>(E, IEntityAspect<E>)](#discardee-ientityaspecte)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public static class Extensions
```

---

### 🏹 Methods

#### `Apply(IEntity, IEntityAspect)`

```csharp
public static void Apply(this IEntity e, IEntityAspect entityAspect)
```

- **Description:** Applies a non-generic aspect to the specified entity.  
  Internally calls `entityAspect.Apply(e)`.
- **Parameters:**
    - `e` – The target entity.
    - `entityAspect` – The aspect instance to apply.
- **Remarks:** Marked with `MethodImplOptions.AggressiveInlining` to minimize call overhead.

---

#### `Apply<E>(E, IEntityAspect<E>)`

```csharp
public static void Apply<E>(this E e, IEntityAspect<E> entityAspect)
    where E : IEntity
````

- **Description:** Applies a type-safe generic aspect to an entity of type `E`.  
  Internally calls `entityAspect.Apply(e)`.
- **Type parameter:** `E` — The concrete entity type implementing `IEntity`.
- **Parameters:**
    - `e` – The target entity.
    - `entityAspect` – The generic aspect to apply.

---

#### `Discard(IEntity, IEntityAspect)`

```csharp
public static void Discard(this IEntity e, IEntityAspect entityAspect)
````

- **Description:** Discards (removes) a non-generic aspect from the specified entity.  
  Internally calls `entityAspect.Discard(e)`.
- **Parameters:**
    - `e` – The entity to remove the aspect from.
    - `entityAspect` – The aspect instance to discard.
- **Remarks:** Marked with `MethodImplOptions.AggressiveInlining` to reduce invocation overhead.

---

#### `Discard<E>(E, IEntityAspect<E>)`

```csharp
public static void Discard<E>(this E e, IEntityAspect<E> entityAspect)
    where E : IEntity
````

- **Description:** Discards a type-safe generic aspect from an entity of type `E`.  
  Internally calls `entityAspect.Discard(e)`.
- **Type parameter:** `E` — The concrete entity type implementing `IEntity`.
- **Parameters:**
    - `e` – The entity to remove the aspect from.
    - `entityAspect` – The generic aspect instance to discard.