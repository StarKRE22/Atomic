# 🧩 SceneEntity Creation

The following methods allow you to create entities at runtime, for example from prefabs or entirely new GameObjects.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
  - [Using Create Args](#ex1)
  - [Prefab Instantiation](#ex2)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Static Methods](#-static-methods)
    - [Create(in CreateArgs)](#createin-createargs)
    - [Create<E>(in CreateArgs)](#createe-in-createargs)
    - [Create<E>(...)](#createe)
    - [Create(SceneEntity, Transform)](#createsceneentity-transform)
    - [Create<E>(E, Transform)](#createe-e-transform)
    - [Create(SceneEntity, Vector3, Quaternion, Transform)](#createsceneentity-vector3-quaternion-transform)
    - [Create<E>(E, Vector3, Quaternion, Transform)](#createe-e-vector3-quaternion-transform)
    - [Create<E>(E, Transform, Transform)](#createe-e-transform-transform)


---

## 🗂 Examples of Usage

There are two ways of entity creation:

<div id="ex1"></div>

### 1️⃣ Using Create Args

The first way to create entities is through `CreateArgs`, which allows a developer to specify settings for creating a
new GameObject with a `SceneEntity` component.

```csharp
//Non-generic version
var args = new CreateArgs
{
    Name = "Enemy",
    TagCapacity = 2,
    ValueCapacity = 2,
    BehaviourCapacity = 2
};

SceneEntity enemy = SceneEntity.Create(args);
```

```csharp
//Generic version
WeaponEntity enemy = SceneEntity.Create<WeaponEntity>(
    new CreateArgs
    {
        Name = "MachineGun",
        TagCapacity = 3,
        ValueCapacity = 5
    }
);
```

---

<div id="ex2"></div>

### 2️⃣ Prefab Instantiation

Another approach is creating game entities from prefabs.

```csharp
// Instantiating a prefab at the origin
SceneEntity enemyPrefab = Resources.Load<SceneEntity>("Prefabs/Enemy");
SceneEntity instance = SceneEntity.Create(enemyPrefab);
```

```csharp
// Instantiating a prefab at a specific position and rotation
Vector3 spawnPos = new Vector3(0, 0, 0);
Quaternion rotation = Quaternion.Euler(0, 180, 0);
SceneEntity bossInstance = SceneEntity.Create(enemyPrefab, spawnPos, rotation);
```


---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public partial class SceneEntity
```

---

### 🏹 Static Methods

#### `Create(in CreateArgs)`

```csharp
public static SceneEntity Create(in CreateArgs args)  
```

- **Description:** Creates a new `SceneEntity` GameObject and configures it with optional tags, values, behaviours,
  installers, and children.
- **Parameter:** `args` – Configuration options in a `CreateArgs` structure.
- **Returns:** The newly created `SceneEntity` instance.
- **Exception:** Throws if `args` contains invalid references.
- **Note:** Skips null installers or children.

#### `Create<E>(in CreateArgs)`

```csharp
public static E Create<E>(in CreateArgs args) where E : SceneEntity  
```

- **Description:** Generic version of `Create` that returns a `SceneEntity` of type `<E>`.
- **Type Parameter:** `E` – The type of SceneEntity to create.
- **Parameter:** `args` – Configuration options in a `CreateArgs` structure.
- **Returns:** A newly created `SceneEntity` of type `E`.
- **Exception:** Throws if `args` contains invalid references.
- **Note:** Skips null installers or children.

#### `Create<E>(...)`

```csharp
public static E Create<E>(  
    string name = null,  
    IEnumerable<int> tags = null,  
    IReadOnlyDictionary<int, object> values = null,  
    IEnumerable<IEntityBehaviour> behaviours = null,  
    bool installOnAwake = true,  
    bool disposeValues = true,  
    bool useUnityLifecycle = true,  
    int initialTagCount = 1,  
    int initialValueCount = 1,  
    int initialBehaviourCount = 1  
) where E : SceneEntity  
```

- **Description:** Convenience overload that constructs a `CreateArgs` internally and calls
  `Create<E>(in CreateArgs args)`.
- **Parameters:**
    - `name` – optional GameObject name.
    - `tags` – optional collection of integer tags.
    - `values` – optional key-value pairs.
    - `behaviours` – optional behaviours to attach.
    - `installOnAwake` – if true, runs installers on Awake.
    - `uninstallOnDestroy` – if true, runs Uninstall on OnDestroy.
    - `disposeValues` – if true, disposes values on destruction.
    - `useUnityLifecycle` – if true, uses Unity lifecycle.
    - `initialTagCount` – initial tag capacity.
    - `initialValueCount` – initial value capacity.
    - `initialBehaviourCount` – initial behaviour capacity.

- **Returns:** A newly created `SceneEntity` of type `<E>`.
- **Exception:** Throws if provided values are invalid.
- **Notes:** Null references are skipped.

#### `Create(SceneEntity, Transform)`

```csharp
public static SceneEntity Create(SceneEntity prefab, Transform parent = null)  
```

- **Description:** Instantiates a prefab and installs the resulting entity under an optional parent.
- **Parameters:**
    - `prefab` – The prefab to instantiate.
    - `parent` – Optional parent transform.

- **Returns:** The newly instantiated `SceneEntity`.

#### `Create<E>(E, Transform)`

```csharp
public static E Create<E>(E prefab, Transform parent = null) where E : SceneEntity  
```

- **Description:** Generic version of prefab instantiation. Defaults position to `Vector3.zero` and rotation to
  `Quaternion.identity`.
- **Parameters:**
    - `prefab` – Prefab to instantiate.
    - `parent` – Optional parent transform.

- **Returns:** The newly instantiated SceneEntity of type `E`.

#### `Create(SceneEntity, Vector3, Quaternion, Transform)`

```csharp
public static SceneEntity Create(
    SceneEntity prefab,
    Vector3 position,
    Quaternion rotation,
    Transform parent = null
)  
```

- **Description:** Instantiates a prefab at a given position and rotation with an optional parent, then installs it.
- **Parameters:**
    - `prefab` – Prefab to instantiate.
    - `position` – Position for the new entity.
    - `rotation` – Rotation for the new entity.
    - `parent` – Optional parent transform.
- **Returns:** The newly instantiated `SceneEntity`.

#### `Create<E>(E, Vector3, Quaternion, Transform)`

```csharp
public static E Create<E>(
    E prefab,
    Vector3 position,
    Quaternion rotation,
    Transform parent = null
) where E : SceneEntity  
```

- **Description:** Generic version of prefab instantiation at a specific position and rotation.

- **Parameters:**
    - `prefab` – Prefab to instantiate.
    - `position` – Position for the new entity.
    - `rotation` – Rotation for the new entity.
    - `parent` – Optional parent transform.

- **Returns:** The newly instantiated SceneEntity of type `E`.
- **Notes:** Automatically calls `Install()` on the created entity.

#### `Create<E>(E, Transform, Transform)`

```csharp
public static E Create<E>(E prefab, Transform point, Transform parent) where E : SceneEntity  
```

- **Description:** Instantiates the prefab at the position and rotation of a reference transform (`point`) with an
  optional parent.
- **Parameters:**
    - `prefab` – Prefab to instantiate.
    - `point` – Reference transform for position and rotation.
    - `parent` – Optional parent transform.

- **Returns:** The newly instantiated SceneEntity of type `E`.
- **Note:** Automatically calls `Install()` on the created entity.