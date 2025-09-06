# 🧩 SceneEntityWorld

A Unity-compatible, scene-bound entity world that manages entities derived from `SceneEntity`.  
Supports automatic integration with Unity lifecycle events and runtime management of entities.

### Type Parameters

- `E` – The specific type of scene entity managed by the world. Must inherit from [`SceneEntity`](#).

---

## Key Features

- **Scene-bound management** – Automatically tracks and manages entities in a Unity scene.
- **Lifecycle integration** – Hooks into Unity's `Awake`, `Start`, `OnEnable`, `OnDisable`, and `OnDestroy`.
- **Entity scanning** – Optionally scans the scene for entities on awake, including inactive ones.
- **Reactive events** – Supports `OnAdded`, `OnRemoved`, `OnEnabled`, `OnDisabled`, `OnUpdated`, `OnFixedUpdated`, and
  `OnLateUpdated`.
- **Generic and non-generic support** – Use `SceneEntityWorld` for base `SceneEntity` or `SceneEntityWorld<E>` for
  specialized types.
- **Unity-friendly** – Can be attached as a component on a `GameObject` and automatically synced with Unity's update
  loop.

---

## Classes

### `SceneEntityWorld`

A **non-generic version** of [`SceneEntityWorld<E>`](#) specialized for `SceneEntity`.  
Use this component when you do not need to specify a particular entity type.

---

### `SceneEntityWorld<E>`

A **generic component** representing a world that manages a collection of entities of type `E`.

#### Type Parameters
- `E` – The type of entity managed by this world. Must implement [`SceneEntity`](#).

---

## Inspector Settings

| Field                        | Description                                                                     |
|------------------------------|---------------------------------------------------------------------------------|
| `bool scanEntitiesOnAwake`   | Whether to automatically scan entities in the scene during `Awake()`.           |
| `bool includeInactiveOnScan` | Whether to include inactive entities when scanning the scene.                   |
| `bool useUnityLifecycle`     | Whether to sync with Unity lifecycle events (`Start`, `OnEnable`, `OnDisable`). |
| `bool dontDestroyOnLoad`     | Whether the world GameObject should persist across scene loads.                 |

## Events

| Event            | Description                                                                                |
|------------------|--------------------------------------------------------------------------------------------|
| `OnStateChanged` | Raised when the underlying `EntityWorld` state changes (add/remove/clear).                 |
| `OnAdded`        | Raised when an entity is added to the world. Passes the added entity as an argument.       |
| `OnRemoved`      | Raised when an entity is removed from the world. Passes the removed entity as an argument. |
| `OnEnabled`      | Raised when the world is enabled, triggering `Enable()` on all contained entities.         |
| `OnDisabled`     | Raised when the world is disabled, triggering `Disable()` on all contained entities.       |
| `OnUpdated`      | Raised during the regular update phase (`OnUpdate`) with `deltaTime`.                      |
| `OnFixedUpdated` | Raised during the fixed update phase (`OnFixedUpdate`) with `deltaTime`.                   |
| `OnLateUpdated`  | Raised during the late update phase (`OnLateUpdate`) with `deltaTime`.                     |

---

## Properties

| Property                     | Description                                                                     |
|------------------------------|---------------------------------------------------------------------------------|
| `string Name`                | The name of the GameObject/world instance.                                      |
| `bool Enabled`               | Indicates whether the world and all contained entities are currently enabled.   |
| `bool IsReadOnly`            | Always false; the collection is mutable.                                        |
| `int Count`                  | Number of entities in the world.                                                |
| `bool scanEntitiesOnAwake`   | Whether to automatically scan entities in the scene during `Awake()`.           |
| `bool includeInactiveOnScan` | Whether to include inactive entities when scanning the scene.                   |
| `bool useUnityLifecycle`     | Whether to sync with Unity lifecycle events (`Start`, `OnEnable`, `OnDisable`). |
| `bool dontDestroyOnLoad`     | Whether the world GameObject should persist across scene loads.                 |

---

## Methods

| Method                                | Description                                                           |
|---------------------------------------|-----------------------------------------------------------------------|
| `bool Add(E entity)`                  | Adds a new entity to the world. Returns `false` if it already exists. |
| `bool Remove(E entity)`               | Removes an entity from the world. Returns `true` if removed.          |
| `void Clear()`                        | Removes all entities from the world.                                  |
| `void CopyTo(ICollection<E> results)` | Copies all entities into a provided collection.                       |
| `IEnumerator<E> GetEnumerator()`      | Returns an enumerator over entities in insertion order.               |
| `void Enable()`                       | Enables the world and all contained entities.                         |
| `void Disable()`                      | Disables the world and all contained entities.                        |
| `void OnUpdate(float deltaTime)`      | Updates all enabled entities during the regular update phase.         |
| `void OnFixedUpdate(float deltaTime)` | Updates all enabled entities during the fixed update phase.           |
| `void OnLateUpdate(float deltaTime)`  | Updates all enabled entities during the late update phase.            |
| `void Dispose()`                      | Disposes all entities and clears the world.                           |

## Static Methods

| Method                                                                       | Description                                                               |
|------------------------------------------------------------------------------|---------------------------------------------------------------------------|
| `static T Create<T>(string name, bool scanEntities, bool useUnityLifecycle)` | Creates a new GameObject with a `SceneEntityWorld<E>` component attached. |
| `static void Destroy(SceneEntityWorld<E> world, float t = 0)`                | Destroys the GameObject hosting the world after an optional delay.        |

---

## Example Usage

```csharp
// Create a SceneEntityWorld in code
SceneEntityWorld world = SceneEntityWorld.Create(
    "GameplayWorld",
    scanEntities: true,
    useUnityLifecycle: true
);

// Add entities
world.Add(new MyEntity("Player"));
world.AddRange(new MyEntity("Enemy1"), new MyEntity("Enemy2"));

// Subscribe to events
world.OnAdded += entity => Debug.Log($"Entity added: {entity.name}");
world.OnRemoved += entity => Debug.Log($"Entity removed: {entity.name}");
world.OnEnabled += () => Debug.Log("World enabled");
world.OnDisabled += () => Debug.Log("World disabled");

// Update methods are called automatically if useUnityLifecycle = true
```

## Remarks
- Integrates Unity’s scene and GameObject lifecycle with runtime entity management.
- Supports both automatic scene scanning and manual entity management.
- Events provide reactive hooks for gameplay, UI, or system updates.
- Generic version allows specialized entity types, while non-generic is convenient for SceneEntity only.

## Performance