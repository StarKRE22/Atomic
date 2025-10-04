# 🧩 SceneEntityWorld\<E>

```csharp
public abstract class SceneEntityWorld<E> : MonoBehaviour, IEntityWorld<E> where E : SceneEntity  
```

- **Description:** A **Unity-compatible world manager for scene-based entities** of type `E`.  
  Integrates with Unity lifecycle events (`Awake`, `Start`, `OnEnable`, etc.) to manage entity enabling, updating, and
  cleanup.
- **Type Parameter:** `E` — The type of scene entity this world manages. Must inherit
  from [SceneEntity](../Entities/SceneEntity.md).
- **Inheritance:** `MonoBehaviour`, [IEntityWorld\<E>](IEntityWorld%601.md)
- **Note:** Ideal for managing scene entities dynamically, supporting both runtime and editor workflows, including
  automatic scanning and registration.
- **See also:** [SceneEntityWorld](SceneEntityWorld.md), [SceneEntity](../Entities/SceneEntity.md)

---

## 🛠 Inspector Settings

| Parameter                   | Description                                                                                                        |
|-----------------------------|--------------------------------------------------------------------------------------------------------------------|
| `useUnityLifecycle`         | Enable automatic syncing with Unity MonoBehaviour lifecycle (`Start`, `OnEnable`, `OnDisable`). Default is `true`. |
| `dontDestroyOnLoad`         | Prevent this world from being destroyed when the scene changes. Default is `false`.                                |
| `registerOnAwake`           | If enabled, the world automatically adds all entities in the scene during `Awake()`. Default is `true`.            |
| `includeInactiveOnRegister` | If enabled, the world will also scan and register inactive entities in the scene. Default is `true`.               |

---

## ⚡ Events

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;  
```

- **Description:** Raised when entities are added or removed.

#### `OnAdded`

```csharp
public event Action<E> OnAdded;  
```

- **Description:** Raised when an entity is added.
- **Parameter:** `entity` — The entity that was added.

#### `OnRemoved`

```csharp
public event Action<E> OnRemoved;  
```

- **Description:** Raised when an entity is removed.
- **Parameter:** `entity` — The entity that was removed.

#### `OnEnabled`

```csharp  
public event Action OnEnabled;  
```

- **Description:** Raised when the world is enabled.

#### `OnDisabled`

```csharp  
public event Action OnDisabled;  
```

- **Description:** Raised when the world is disabled.

#### `OnTicked`

```csharp  
public event Action<float> OnTicked;  
```

- **Description:** Raised every `Tick`.
- **Parameter:** `deltaTime` — Time in seconds since the last frame.

#### `OnFixedTicked`

```csharp  
public event Action<float> OnFixedTicked;  
```

- **Description:** Raised every `FixedTick`.
- **Parameter:** `deltaTime` — Fixed time step used by the physics engine.

#### `OnLateTicked`

```csharp  
public event Action<float> OnLateTicked;  
```

- **Description:** Raised every `LateTick`.
- **Parameter:** `deltaTime` — Time in seconds since the last frame.

---

## 🔑 Properties

#### `Name`

```csharp  
public string Name { get; set; }  
```

- **Description:** Gets or sets the name of the world.
- **Note:** Automatically tied to the `GameObject` name.

#### `Enabled`

```csharp  
public bool Enabled { get; }  
```

- **Description:** Indicates whether the world is currently enabled.

#### `IsReadOnly`

```csharp  
public bool IsReadOnly { get; }  
```

- **Description:** Indicates whether the entity collection is read-only.

#### `Count`

```csharp  
int Count { get; }  
```

- **Description:** Returns the number of entities in the world.

---

## 🏹 Public Methods

#### `RegisterAllEntities()`

```csharp  
public void RegisterAllEntities();  
```

- **Description:** Scans the scene for all entities of type `E` and adds them to the world.
- **Behavior:** Automatically calls `Install()` on entities before adding.
- **Notes:** Honors `includeInactiveOnRegister` to optionally include inactive GameObjects.

#### `Add(E)`

```csharp  
public bool Add(E entity);  
```

- **Description:** Adds an entity to the world.
- **Returns:** `true` if added, `false` if it already exists.

#### `Remove(E)`

```csharp  
public bool Remove(E entity);  
```

- **Description:** Removes an entity from the world.
- **Returns:** `true` if removed.

#### `Clear()`

```csharp  
public void Clear();  
```

- **Description:** Removes all entities from the world.

#### `Contains(E)`

```csharp  
public bool Contains(E entity);  
```

- **Description:** Checks whether the entity exists in the world.

#### `CopyTo(ICollection<E>)`

```csharp  
public void CopyTo(ICollection<E> results);  
```

- **Description:** Copies all entities into a provided collection.

#### `Enable()`

```csharp  
public void Enable();  
```

- **Description:** Enables the world and all contained entities.

#### `Disable()`

```csharp  
public void Disable();  
```

- **Description:** Disables the world and all contained entities.

#### `Tick(float)`

```csharp  
public void Tick(float deltaTime);  
```

- **Description:** Performs the regular update for all enabled entities.

#### `FixedTick(float)`

```csharp  
public void FixedTick(float deltaTime);  
```

- **Description:** Performs the fixed update (usually for physics) for all enabled entities.

#### `LateTick(float)`

```csharp  
public void LateTick(float deltaTime);  
```

- **Description:** Performs the late update for all enabled entities.

#### `Dispose()`

```csharp  
public void Dispose();  
```

- **Description:** Disposes the world and all entities, unsubscribing events.

## 🏹 Static Methods 

#### `Create<T>(string, bool, bool)`

```csharp
public static T Create<T>(string name = null, bool scanEntities = true, bool useUnityLifecycle = true) where T : SceneEntityWorld<E>;  
```

- **Description:** Creates a new inactive GameObject with a `SceneEntityWorld<E>` component.
- **Parameters:**
    - `name` — Optional GameObject/world name.
    - `scanEntities` — Whether to automatically scan scene entities on Awake.
    - `useUnityLifecycle` — Whether to integrate with Unity lifecycle callbacks.
- **Returns:** The initialized world instance.

#### `Destroy(SceneEntityWorld<E>, float)`

```csharp  
public static void Destroy(SceneEntityWorld<E> world, float t = 0);  
```

- **Description:** Destroys the world and its GameObject after an optional delay.

---

## 🗂 Example of Usage

Below is an example of creating and using `SceneEntityWorld<E>`

#### 1. Assume we have a GameEntity type 

```csharp
public class GameEntity : SceneEntity
{
}
```

#### 2. Create `GameEntityWorld` extending from `SceneEntityWorld<GameEntity>` 
```csharp
public class GameEntityWorld : SceneEntityWorld<GameEntity>
{
}
```

#### 3. Add and configure `GameEntityWorld` component 

#### 4. Use `GameEntityWorld` in runtime

```csharp
GameEntityWorld world = ...

// Subscribe to events  
world.OnAdded += e => Debug.Log($"Added entity: {e.name}");  
world.OnRemoved += e => Debug.Log($"Removed entity: {e.name}");  
world.OnEnabled += () => Debug.Log("World enabled");

// Enable world  
world.Enable();

// Add entity manually  
GameEntity entity = GameEntity.Create("Entity1");  
world.Add(entity);

// Tick updates  
world.Tick(Time.deltaTime);  
world.FixedTick(Time.fixedDeltaTime);  
world.LateTick(Time.deltaTime);
```

Also, you can create and destroy an instance of `GameEntityWorld` dynamically

```csharp
// Create a SceneEntityWorld for GameEntity type
GameEntityWorld world = GameEntityWorld.Create("GameEntityWorld", scanEntities: true);

// Destroy world after use  
GameEntityWorld.Destroy(world);  
```