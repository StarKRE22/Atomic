# 🧩️ SceneEntity

```csharp
[AddComponentMenu("Atomic/Entities/Entity")]
[DisallowMultipleComponent]
[DefaultExecutionOrder(-1000)]
public class SceneEntity : MonoBehaviour, IEntity, ISerializationCallbackReceiver
```

- **Description:** Represents the Unity implementation of the entity. It allows installation from the Unity
  Scene and composition through the Inspector or installers.

- **Inheritance:** `MonoBehaviour`, [IEntity](IEntity.md)
- **Notes:** Supports Unity serialization and Odin Inspector

---


---


---


---


----


---


---


---


---


---

<details>
  <summary>
    <h2 id="-entity-creation"> 🏗️ Creation</h2>
    <br> The following methods allow you to create entities at runtime, for example from prefabs or entirely new GameObjects.
  </summary>

### 🔹 Parameterized Instantiation

The first way to create entities is through `CreateArgs`, which allows a developer to specify settings for creating a
new GameObject with a `SceneEntity` component.

---

#### `CreateArgs`

```csharp
[Serializable]  
public struct CreateArgs
{
      public string name;
      public IEnumerable<int> tags;
      public IReadOnlyDictionary<int, object> values;
      public IEnumerable<IEntityBehaviour> behaviours;
      public List<SceneEntityInstaller> sceneInstallers;
      public List<ScriptableEntityInstaller> scriptableInstallers;
      public List<SceneEntity> children;

      public int initialTagCapacity;
      public int initialValueCapacity;
      public int initialBehaviourCapacity;

      public bool installOnAwake;
      public bool disposeValues;
      public bool useUnityLifecycle;
}
```

- **Description:** Defines a set of parameters for creating a dynamic entity.
- **Fields:**
    - `string name` – optional name for the GameObject.
    - `IEnumerable<int> tags` – optional tags to assign.
    - `IReadOnlyDictionary<int, object> values` – optional key-value pairs.
    - `IEnumerable<IEntityBehaviour> behaviours` – optional behaviours to attach.
    - `List<SceneEntityInstaller> sceneInstallers` – optional MonoBehaviour installers to run.
    - `List<SceneEntityInstaller> scriptableInstallers` – optional ScriptableObject installers to run.
    - `List<SceneEntity> children` – optional child entities.
    - `int initialTagCapacity` – initial capacity for tags.
    - `int initialValueCapacity` – initial capacity for values.
    - `int initialBehaviourCapacity` – initial capacity for behaviours.
    - `bool installOnAwake` – if true, installs automatically on Awake.
    - `bool uninstallOnDestroy` – if true, uninstalls automatically on Destroy.
    - `bool disposeValues` – if true, disposes values on destruction.
    - `bool useUnityLifecycle` – if true, uses Unity lifecycle methods.

---

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

#### 🗂 Examples of Usage

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

### 🔹 Prefab Instantiation

Another approach is creating game entities from prefabs.

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

#### 🗂 Examples of Usage

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

</details>

---

<details>
  <summary>
    <h2 id="-entity-destruction"> 🗑️ Destruction</h2>
    <br> This section provides methods of how to destroy entities at runtime.
  </summary>

### 🏹 Methods

#### `Destroy(IEntity, float)`

```csharp
public static void Destroy(IEntity entity, float t = 0)  
```

- **Description:** Destroys the associated `GameObject` of the specified `IEntity` if it can be cast to a `SceneEntity`.
- **Parameters:**
    - `entity` – The entity whose `GameObject` should be destroyed.
    - `t` – Optional delay in seconds before destruction. Defaults to `0`.
- **Note:** Internally casts the `IEntity` to `SceneEntity` before destroying.

#### `Destroy(SceneEntity, float)`

```csharp
public static void Destroy(SceneEntity entity, float t = 0)  
```

- **Description:** Destroys the specified `SceneEntity`'s `GameObject` after an optional delay.
- **Parameters:**
    - `entity` – The `SceneEntity` to destroy.
    - `t` – Optional delay in seconds before destruction. Defaults to `0`.
- **Note:** If `entity` is `null`, no action is taken.

---

### 🗂 Example of Usage

```csharp
// Destroys entity after 3 seconds
SceneEntity.Destroy(sceneEntity, 3f);
```

</details>

---

<details>
  <summary>
    <h2 id="-entity-casting"> 🪄 Casting</h2>
    <br> This section provides methods for safe casting between <code>IEntity</code> and <code>SceneEntity</code>.
  </summary>

### 🏹 Methods

#### `Cast(IEntity)`

```csharp
public static SceneEntity Cast(IEntity entity)  
```

- **Description:** Casts the specified `IEntity` to a `SceneEntity` if possible.
- **Parameter:** `entity` – The entity to cast.
- **Returns:** The entity cast to `SceneEntity`, or `null` if the input is `null`.
- **Exceptions:** Throws `InvalidCastException` if the entity cannot be cast to `SceneEntity`.
- **Note:** Uses `AggressiveInlining` for performance.

#### `Cast<E>(IEntity)`

```csharp
public static E Cast<E>(IEntity entity) where E : SceneEntity  
```

- **Description:** Casts the specified `IEntity` to the target type `E`. Supports direct `SceneEntity` instances and
  `SceneEntityProxy<E>` wrappers.
- **Type Parameter:** `E` – The type of `SceneEntity` to cast to.
- **Parameter:** `entity` – The entity to cast.
- **Returns:** The entity cast to type `E`, or `null` if the input is `null`.
- **Exceptions:** Throws `InvalidCastException` if the entity cannot be cast to the target type `E`.

#### `TryCast(IEntity, out SceneEntity)`

```csharp
public static bool TryCast(IEntity entity, out SceneEntity result)  
```

- **Description:** Attempts to cast the specified `IEntity` to a `SceneEntity`.
- **Parameters:**
    - `entity` – The entity to cast.
    - `result` – The cast result if successful; otherwise, `null`.
- **Returns:** `true` if the cast was successful; otherwise, `false`.

#### `TryCast<E>(IEntity, out E)`

```csharp
public static bool TryCast<E>(IEntity entity, out E result) where E : SceneEntity  
```

- **Description:** Attempts to cast the specified `IEntity` to the target type `E`. Supports direct `SceneEntity`
  instances and `SceneEntityProxy<E>` wrappers.
- **Type Parameter:** `E` – The type of `SceneEntity` to cast to.
- **Parameters:**
    - `entity` – The entity to cast.
    - `result` – The cast result if successful; otherwise, `null`.
- **Returns:** `true` if the cast was successful; otherwise, `false`.

---

### 🗂 Examples of Usage

#### Simple cast to `SceneEntity`

```csharp
IEntity entity = GetEntityFromRegistry();
SceneEntity sceneEntity = SceneEntity.Cast(entity);
```

> Throws an exception if `entity` is not a `SceneEntity`.

#### Generic cast to a specific `SceneEntity` type

```csharp
IEntity entity = GetEntityFromRegistry();
EnemyEntity enemy = SceneEntity.Cast<EnemyEntity>(entity);
```

> Throws an exception if entity is not of type `EnemyEntity` or a proxy of it.

#### Safe cast using `TryCast`

```csharp
IEntity entity = GetEntityFromRegistry();
if (SceneEntity.TryCast(entity, out SceneEntity sceneEntity))
    Debug.Log($"Successfully casted to SceneEntity: {sceneEntity.Name}");
else
    Debug.LogWarning("Entity is not a SceneEntity");
```

#### Safe generic cast using TryCast<E>

```csharp
IEntity entity = GetEntityFromRegistry();
if (SceneEntity.TryCast<EnemyEntity>(entity, out EnemyEntity enemy))
    Debug.Log($"Successfully casted to EnemyEntity: {enemy.Name}");
else
    Debug.LogWarning("Entity is not of type EnemyEntity");
```

</details>

---

## 🗂 Example Usage

Below is the process for quickly creating a character entity in Unity

#### 1. Create a new `GameObject`

<img width="360" height="255" alt="GameObject creation" src="https://github.com/user-attachments/assets/463a721f-e50d-4cb7-86be-a5d50a6bfa17" />

#### 2. Add `Entity` Component to the GameObject

<img width="464" height="346" alt="Entity component" src="https://github.com/user-attachments/assets/f74644ba-5858-4857-816e-ea47eed0e913" />

#### 3. Create `MoveBehaviour` for your entity

```csharp
// Controller that moves entity by its direction
public sealed class MoveBehaviour : IEntityInit, IEntityFixedTick
{
    private Transform _transform;
    private IValue<float> _moveSpeed;
    private IValue<Vector3> _moveDirection;

    // Called when MonoBehaviour.Start() is invoked
    public void Init(IEntity entity)
    {
        _transform = entity.GetValue<Transform>("Transform");
        _moveSpeed = entity.GetValue<IValue<float>>("MoveSpeed");
        _moveDirection = entity.GetValue<IValue<Vector3>>("MoveDirection");
    }

    // Called when MonoBehaviour.FixedUpdate() is invoked
    public void FixedTick(IEntity entity, float deltaTime)
    {
        Vector3 direction = _moveDirection.Value;
        if (direction != Vector3.zero) 
            _transform.position += _moveSpeed.Value * deltaTime * direction;
    }
}
```

#### 4. Create `CharacterInstaller` script

 ```csharp
//Populates entity with tags, values and behaviours
public sealed class CharacterInstaller : SceneEntityInstaller
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Const<float> _moveSpeed = 5.0f; //Immutable variable
    [SerializeField] private ReactiveVariable<Vector3> _moveDirection; //Mutable variable with subscription

    public override void Install(IEntity entity)
    {
        //Add tags to a character
        entity.AddTag("Character");
        entity.AddTag("Moveable");

        //Add properties to a character
        entity.AddValue("Transform", _transform);
        entity.AddValue("MoveSpeed", _moveSpeed);
        entity.AddValue("MoveDirection", _moveDirection);
        
        //Add behaviours to a character
        entity.AddBehaviour<MoveBehaviour>();
    }
}
```

#### 5. Attach `CharacterInstaller` script to the GameObject

<img width="464" height="153" alt="изображение" src="https://github.com/user-attachments/assets/1967b1d8-b6b7-41c7-85db-5d6935f6443e" />

#### 6. Drag & drop `CharacterInstaller` into `installers` field of the entity

<img width="464" height="" alt="изображение" src="../../Images/SceneEntity%20Attach%20Installer.png" />

#### 7. Enter `PlayMode` and check your character movement!

---

<details>
  <summary>
    <h2 id="-notes">📝 Notes</h2>
  </summary>

- **Event-Driven** – Reactive programming support via state change notifications.
- **Unique Identity** – Runtime-generated instance ID for entity tracking.
- **Tag System** – Lightweight categorization and filtering.
- **State Management** – Dynamic key-value storage for runtime data.
- **Behaviour Composition** – Attach or detach modular logic at runtime.
- **Lifecycle Control** – Built-in support for `Init`, `Enable`, `Update`, `Disable`, and `Dispose` phases.
- **Registry Integration** – Automatic registration with EntityRegistry
- **Memory Efficient** – Pre-allocation support for collections
- **Unity Component** – Attach directly to GameObjects.
- **Scene Installation** – Automatically installs child entities and configured installers.
- **Unity Lifecycle Integration** – Hooks into Awake, Start, OnEnable, OnDisable, and OnDestroy.
- **Gizmos Support** – Conditional drawing in Scene view.
- **Prefab & Factory Support** – Creation, instantiation, and destruction of entities.
- **Casting & Proxies** – Safe conversion between `IEntity`, `SceneEntity` and `SceneEntityProxy`.
- **Scene-Wide Installation** – Can install all SceneEntities in a scene.
- **Odin Inspector Support** – Optional editor enhancements for configuration and debug.
- **Not Thread Safe** — All operations should be performed on the main Unity thread.
- `SceneEntity` is Unity-specific
- Default execution order is `-1000` (runs early)
- `[DisallowMultipleComponent]` prevents multiple entities per `GameObject`

</details>