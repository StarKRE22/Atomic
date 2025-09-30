# 🧩 SceneEntity Creation

The following methods allow you to create entities at runtime, for example from prefabs or entirely new GameObjects.

---

## 🏹 Methods

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

---



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

---

<details>
  <summary>
    <h2 id="create-args"> 🧩 CreateArgs</h2>
    <br> Defines a set of parameters for creating a dynamic entity.
  </summary>
<br>

```csharp
[Serializable]  
public struct CreateArgs
```

### 🧱 Fields

#### `Name`

```csharp
public string name;
```

- **Description:** Name of the entity (Unity object name).

#### `Tags`

```csharp
public IEnumerable<int> tags;
```

- **Description:** Optional tags to assign to the entity.

#### `Values`

```csharp
public IReadOnlyDictionary<int, object> values;
```

- **Description:** Optional key-value pairs assigned to the entity.

#### `Behaviours`

```csharp
public IEnumerable<IEntityBehaviour> behaviours;
```

- **Description:** Optional behaviours attached to the entity.

#### `SceneInstallers`

```csharp
public List<SceneEntityInstaller> sceneInstallers;
```

- **Description:** Optional **MonoBehaviour installers** to run in the scene.

#### `ScriptableInstallers`

```csharp
public List<ScriptableEntityInstaller> scriptableInstallers;
```

- **Description:** Optional **ScriptableObject installers** to run.

#### `Children`

```csharp
public List<SceneEntity> children;
```

- **Description:** Optional child entities attached to this entity.

#### `InitialTagCapacity`

```csharp
public int initialTagCapacity;
```

- **Description:** Initial capacity for tags.

#### `InitialValueCapacity`

```csharp
public int initialValueCapacity;
```

- **Description:** Initial capacity for values.

#### `InitialBehaviourCapacity`

```csharp
public int initialBehaviourCapacity;
```

- **Description:** Initial capacity for behaviours.

#### `InstallOnAwake`

```csharp
public bool installOnAwake;
```

- **Description:** If true, the entity installs automatically on **Awake**.

#### `UninstallOnDestroy`

```csharp
public bool uninstallOnDestroy;
```

- **Description:** If true, the entity uninstalls automatically on **Destroy**.

#### `DisposeValues`

```csharp
public bool disposeValues;
```

- **Description:** If true, values are disposed when the entity is destroyed.

#### `UseUnityLifecycle`

```csharp
public bool useUnityLifecycle;
```

- **Description:** If true, uses Unity lifecycle methods (**Awake**, **OnEnable**, **OnDisable**, **OnDestroy**).

</details>

---

## 🗂 Examples of Usage

There are two ways of entity creation:
1. The first way to create entities is through `CreateArgs`, which allows a developer to specify settings for creating a
   new GameObject with a `SceneEntity` component.
2. Another approach is creating game entities from prefabs.

---

### 1️⃣ Using Create Args

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

### 2️⃣ Prefab Instantiation

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
