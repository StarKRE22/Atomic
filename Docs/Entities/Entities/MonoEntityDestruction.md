# 🧩 MonoEntity Destruction

Provides methods of how to destroy entities at runtime.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Static Methods](#-static-methods)
        - [Destroy(IEntity, float)](#destroyientity-float)
        - [Destroy(MonoEntity, float)](#destroyMonoEntity-float)

---

## 🗂 Example of Usage

```csharp
// Destroys entity after 3 seconds
MonoEntity.Destroy(MonoEntity, 3f);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public partial class MonoEntity
```

---

### 🏹 Static Methods

#### `Destroy(IEntity, float)`

```csharp
public static void Destroy(IEntity entity, float t = 0)  
```

- **Description:** Destroys the associated `GameObject` of the specified `IEntity` if it can be cast to a `MonoEntity`.
- **Parameters:**
    - `entity` – The entity whose `GameObject` should be destroyed.
    - `t` – Optional delay in seconds before destruction. Defaults to `0`.
- **Note:** Internally casts the `IEntity` to `MonoEntity` before destroying.

#### `Destroy(MonoEntity, float)`

```csharp
public static void Destroy(MonoEntity entity, float t = 0)  
```

- **Description:** Destroys the specified `MonoEntity`'s `GameObject` after an optional delay.
- **Parameters:**
    - `entity` – The `MonoEntity` to destroy.
    - `t` – Optional delay in seconds before destruction. Defaults to `0`.
- **Note:** If `entity` is `null`, no action is taken.