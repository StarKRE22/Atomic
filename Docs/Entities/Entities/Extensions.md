# 🧩 Entity Extensions


---

<details>
  <summary>
    <h2 id="-values"> 🔑️ Values</h2>
    <br> Provides extension methods for values
  </summary>

</details>

---

<details>
  <summary>
    <h2 id="-behaviours"> ⚙️ Behaviours</h2>
    <br> Provides extension methods for behaviours
  </summary>

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

</details>

---

<details>
  <summary>
    <h2 id="-installing"> 🔧 Installing</h2>
    <br> Provides extension methods for entity installing
  </summary>

#### `Install(IEntityInstaller)`

```csharp
public static IEntity Install(this IEntity entity, IEntityInstaller installer)
```

- **Description:** Installs logic from a single `IEntityInstaller` into the specified entity.
- **Parameters:**
    - `entity` – The entity to install the logic into.
    - `installer` – The installer that provides logic to install.
- **Returns:** The same `entity` after installation (supports chaining).
- **Remarks:** Delegates installation to the `IEntityInstaller.Install(IEntity)` method.

#### `Install(IEnumerable<IEntityInstaller>)`

```csharp
public static void Install(this IEntity entity, IEnumerable<IEntityInstaller> installers)
```

- **Description:** Installs logic from multiple `IEntityInstaller` instances into the specified entity.
- **Parameters:**
    - `entity` – The entity to install the logic into.
    - `installers` – Collection of installers. Can be `null`, in which case nothing is installed.
- **Remarks:** Each installer in `installers` will invoke its `Install(IEntity)` method.

#### `InstallFromScene(Scene, bool)`

```csharp
public static void InstallFromScene(this IEntity entity, Scene scene, bool includeInactive = true)
```

- **Description:** Installs logic from all `SceneEntityInstaller` components found in the specified scene.
- **Parameters:**
    - `entity` – The entity to install the logic into.
    - `scene` – The scene in which to search for installers.
    - `includeInactive` – If `true`, installers on inactive GameObjects are included; otherwise only active
      installers are considered.
- **Remarks:** Iterates over all root GameObjects in the scene and applies each found `SceneEntityInstaller` to the
  entity.

#### `InstallFromScene<T>(Scene, bool)`

```csharp
public static void InstallFromScene<T>(this T entity, Scene scene, bool includeInactive = true)
    where T : class, IEntity
```

- **Description:** Installs logic from all `SceneEntityInstaller<T>` components found in the specified scene for a
  generic entity type.
- **Type Parameter:** `T` – The entity type that implements `IEntity`.
- **Parameters:**
    - `entity` – The entity to install the logic into.
    - `scene` – The scene in which to search for installers.
    - `includeInactive` – If `true`, installers on inactive GameObjects are included; otherwise only active
      installers are considered.
- **Remarks:** Iterates over all root GameObjects in the scene and applies each found `SceneEntityInstaller<T>` to the
  entity. Useful for generic entities or strongly-typed scenarios.

</details>

---

<details>
  <summary>
    <h2 id="-retrieval"> 🔍 Retrieval</h2>
    <br> Provides extension methods for entity searching and retrieving 
  </summary>

#### `TryGetEntity(GameObject, out IEntity)`

```csharp
public static bool TryGetEntity(this GameObject gameObject, out IEntity entity)
```

- **Description:** Tries to retrieve the `IEntity` component from the specified GameObject.
- **Parameter:** `gameObject` – The GameObject to search.
- **Output:** `entity` – The retrieved IEntity component if found.
- **Returns:** `true` if an IEntity component was found; otherwise, `false`.

#### `TryGetEntity(Component, out IEntity)`

```csharp
public static bool TryGetEntity(this Component component, out IEntity entity)
```

- **Description:** Tries to retrieve the `IEntity` component from the specified Component.
- **Parameter:** `component` – The Component to search.
- **Output:** `entity` – The retrieved IEntity component if found.
- **Returns:** `true` if an IEntity component was found; otherwise, `false`.

#### `TryGetEntity(Collision2D, out IEntity)`

```csharp
public static bool TryGetEntity(this Collision2D collision2D, out IEntity entity)
```

- **Description:** Tries to retrieve the `IEntity` component from a 2D collision.
- **Parameter:** `collision2D` – The 2D collision object.
- **Output:** `entity` – The retrieved IEntity component if found.
- **Returns:** `true` if an IEntity component was found; otherwise, `false`.

#### `TryGetEntity(Collision, out IEntity)`

```csharp
public static bool TryGetEntity(this Collision collision, out IEntity entity)
```

- **Description:** Tries to retrieve the `IEntity` component from a 3D collision.
- **Parameter:** `collision` – The 3D collision object.
- **Output:** `entity` – The retrieved IEntity component if found.
- **Returns:** `true` if an IEntity component was found; otherwise, `false`.

#### `FindEntityInParent(GameObject, out IEntity)`

```csharp
public static bool FindEntityInParent(this GameObject gameObject, out IEntity entity)
```

- **Description:** Finds an `IEntity` in the parent hierarchy of the GameObject.
- **Parameter:** `gameObject` – The GameObject to search.
- **Output:** `entity` – The retrieved IEntity component if found.
- **Returns:** `true` if an IEntity component was found in the parent hierarchy; otherwise, `false`.

#### `FindEntityInParent(Component, out IEntity)`

```csharp
public static bool FindEntityInParent(this Component component, out IEntity entity)
```

- **Description:** Finds an `IEntity` in the parent hierarchy of the Component.
- **Parameter:** `component` – The Component to search.
- **Output:** `entity` – The retrieved IEntity component if found.
- **Returns:** `true` if an IEntity component was found in the parent hierarchy; otherwise, `false`.

#### `FindEntityInParent(Collision2D, out IEntity)`

```csharp
public static bool FindEntityInParent(this Collision2D collision2D, out IEntity entity)
```

- **Description:** Finds an `IEntity` in the parent hierarchy from a 2D collision.
- **Parameter:** `collision2D` – The 2D collision object.
- **Output:** `entity` – The retrieved IEntity component if found.
- **Returns:** `true` if an IEntity component was found in the parent hierarchy; otherwise, `false`.

#### `FindEntityInParent(Collision, out IEntity)`

```csharp
public static bool FindEntityInParent(this Collision collision, out IEntity entity)
```

- **Description:** Finds an `IEntity` in the parent hierarchy from a 3D collision.
- **Parameter:** `collision` – The 3D collision object.
- **Output:** `entity` – The retrieved IEntity component if found.
- **Returns:** `true` if an IEntity component was found in the parent hierarchy; otherwise, `false`.

</details>