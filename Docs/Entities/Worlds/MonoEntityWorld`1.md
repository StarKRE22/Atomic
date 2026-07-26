# 🧩 MonoEntityWorld\<E>

A **Unity-compatible world manager for scene-based entities** of type `E`.Integrates with Unity lifecycle events (
`Awake`, `Start`, `OnEnable`, etc.) to manage entity enabling, updating, and cleanup. Ideal for managing scene entities
dynamically, supporting both runtime and editor workflows, including automatic scanning and registration.

---

## 📑 Table of Contents

<ul>
  <li><a href="#-example-of-usage">Example of Usage</a></li>
  <li><a href="#-inspector-settings">Inspector Settings</a></li>
  <li>
    <a href="#-api-reference">API Reference</a>
    <ul>
      <li><a href="#-type">Type</a></li>
      <li>
        <details>
          <summary><a href="#-events">Events</a></summary>
          <ul>
            <li><a href="#onstatechanged">OnStateChanged</a></li>
            <li><a href="#onadded">OnAdded</a></li>
            <li><a href="#onremoved">OnRemoved</a></li>
            <li><a href="#onenabled">OnEnabled</a></li>
            <li><a href="#ondisabled">OnDisabled</a></li>
            <li><a href="#onticked">OnTicked</a></li>
            <li><a href="#onfixedticked">OnFixedTicked</a></li>
            <li><a href="#onlateticked">OnLateTicked</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-properties">Properties</a></summary>
          <ul>
            <li><a href="#name">Name</a></li>
            <li><a href="#enabled">Enabled</a></li>
            <li><a href="#isreadonly">IsReadOnly</a></li>
            <li><a href="#count">Count</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-methods">Methods</a></summary>
          <ul>
            <li><a href="#adde">Add(E)</a></li>
            <li><a href="#removee">Remove(E)</a></li>
            <li><a href="#clear">Clear()</a></li>
            <li><a href="#containse">Contains(E)</a></li>
            <li><a href="#copytoicollectione">CopyTo(ICollection&lt;E&gt;)</a></li>
            <li><a href="#enable">Enable()</a></li>
            <li><a href="#disable">Disable()</a></li>
            <li><a href="#tickfloat">Tick(float)</a></li>
            <li><a href="#fixedtickfloat">FixedTick(float)</a></li>
            <li><a href="#latetickfloat">LateTick(float)</a></li>
            <li><a href="#dispose">Dispose()</a></li>
            <li><a href="#create-tstring-bool-bool">Create&lt;T&gt;(string, bool, bool)</a></li>
            <li><a href="#destroysceneentityworldt-float">Destroy(MonoEntityWorld&lt;E&gt;, float)</a></li>
          </ul>
        </details>
      </li>
    </ul>
  </li>
</ul>

---

## 🗂 Example of Usage

Below is an example of creating and using `MonoEntityWorld<E>`

#### 1. Assume we have a GameEntity type

```csharp
public class GameEntity : MonoEntity
{
}
```

#### 2. Create `GameEntityWorld` extending from `MonoEntityWorld<GameEntity>`

```csharp
public class GameEntityWorld : MonoEntityWorld<GameEntity>
{
}
```

#### 3. Add and configure `GameEntityWorld` component

<img width="400" height="" alt="Entity component" src="../../Images/GameEntityWorld.png" />

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
// Create a MonoEntityWorld for GameEntity type
GameEntityWorld world = GameEntityWorld.Create("GameEntityWorld", scanEntities: true);

// Destroy world after use  
GameEntityWorld.Destroy(world);  
```

---

## 🛠 Inspector Settings

| Parameter                  | Description                                                                                                        |
|----------------------------|--------------------------------------------------------------------------------------------------------------------|
| `useUnityLifecycle`        | Enable automatic syncing with Unity MonoBehaviour lifecycle (`Start`, `OnEnable`, `OnDisable`). Default is `true`. |
| `dontDestroyOnLoad`        | Prevent this world from being destroyed when the scene changes. Default is `false`.                                |
| `collectOnAwake`           | If enabled, the world automatically adds all entities in the scene during `Awake()`. Default is `true`.            |
| `includeInactiveOnCollect` | If enabled, the world will also scan and register inactive entities in the scene. Default is `true`.               |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public abstract class MonoEntityWorld<E> : MonoBehaviour, IEntityWorld<E> where E : MonoEntity  
```

- **Type Parameter:** `E` — The type of scene entity this world manages. Must inherit
  from [MonoEntity](../Entities/MonoEntity.md).
- **Inheritance:** `MonoBehaviour`, [IEntityWorld\<E>](IEntityWorld%601.md)
- **See also:** [MonoEntityWorld](MonoEntityWorld.md), [MonoEntity](../Entities/MonoEntity.md)

---

### ⚡ Events

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

### 🔑 Properties

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
public int Count { get; }  
```

- **Description:** Returns the number of entities in the world.

---

### 🏹 Methods

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

<div id="create-tstring-bool-bool"></div>

#### `Create<T>(string, bool, bool)`

```csharp
public static T Create<T>(string name = null, bool scanEntities = true, bool useUnityLifecycle = true) where T : MonoEntityWorld<E>;  
```

- **Description:** Creates a new inactive GameObject with a `MonoEntityWorld<E>` component.
- **Parameters:**
    - `name` — Optional GameObject/world name.
    - `scanEntities` — Whether to automatically scan scene entities on Awake.
    - `useUnityLifecycle` — Whether to integrate with Unity lifecycle callbacks.
- **Returns:** The initialized world instance.

<div id="destroysceneentityworldt-float"></div>

#### `Destroy(MonoEntityWorld<E>, float)`

```csharp  
public static void Destroy(MonoEntityWorld<E> world, float t = 0);  
```

- **Description:** Destroys the world and its GameObject after an optional delay.