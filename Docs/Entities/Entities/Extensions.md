# 🧩 Entity Extensions

Provides extension methods for [IEntity](IEntity.md) to simplify common operations like adding / removing tags, values,
and behaviours, as well as installing installers and retrieving entities from GameObjects or collisions.

---

<details>
  <summary>
    <h2 id="-common"> 💠️ Core</h2>
    <br> Provides extension methods for base features
  </summary>

#### `Clear()`

```csharp
public static void Clear(this IEntity entity)
```

- **Description:** Clears all data from the entity, including tags, values, and behaviours.

</details>

---

<details>
  <summary>
    <h2 id="-tags"> 🏷️ Tags</h2>
    <br> Provides extension methods for tags
  </summary>

#### `AddTag(string)`

```csharp
public static bool AddTag(this IEntity entity, string key)
```

- **Description:** Adds a tag to the entity by name.
- **Parameter:** `key` – The name of the tag to add.
- **Returns:** `true` if the tag was successfully added; otherwise, `false`.

#### `AddTag(string, out int)`

```csharp
public static bool AddTag(this IEntity entity, string key, out int id)
```

- **Description:** Adds a tag to the entity and returns its numeric ID.
- **Parameter:** `key` – The name of the tag to add.
- **Output:** `id` – The numeric ID assigned to the tag.
- **Returns:** `true` if the tag was successfully added; otherwise, `false`.

#### `AddTags(IEnumerable<int>)`

```csharp
public static void AddTags(this IEntity entity, IEnumerable<int> tags)
```

- **Description:** Adds multiple tags to the entity.
- **Parameter:** `tags` – Collection of numeric tag IDs to add.

#### `AddTags(IEnumerable<string>)`

```csharp
public static void AddTags(this IEntity entity, IEnumerable<string> tags)
```

- **Description:** Adds multiple tags to the entity by string identifiers.
- **Parameter:** `tags` – Collection of tag names to add.

#### `DelTag(string)`

```csharp
public static bool DelTag(this IEntity entity, string tag)
```

- **Description:** Removes a tag from the entity.
- **Parameter:** `tag` – The name of the tag to remove.
- **Returns:** `true` if the tag was successfully removed; otherwise, `false`.

#### `HasTag(string)`

```csharp
public static bool HasTag(this IEntity entity, string key)
```

- **Description:** Checks if the entity has the specified tag.
- **Parameter:** `key` – The name of the tag to check.
- **Returns:** `true` if the entity has the tag; otherwise, `false`.

#### `HasAllTags(params int[])`

```csharp
public static bool HasAllTags(this IEntity entity, params int[] tags)
```

- **Description:** Checks if the entity contains all the specified numeric tags.
- **Parameter:** `tags` – Array of numeric tag IDs.
- **Returns:** `true` if the entity has all the tags; otherwise, `false`.

#### `HasAllTags(params string[])`

```csharp
public static bool HasAllTags(this IEntity entity, params string[] tags)
```

- **Description:** Checks if the entity has all the specified tags by name.
- **Parameter:** `tags` – Array of tag names.
- **Returns:** `true` if the entity has all the tags; otherwise, `false`.

#### `HasAnyTag(params string[])`

```csharp
public static bool HasAnyTag(this IEntity entity, params string[] tags)
```

- **Description:** Checks if the entity has any of the specified tags by name.
- **Parameter:** `tags` – Array of tag names.
- **Returns:** `true` if the entity has at least one of the tags; otherwise, `false`.

#### `HasAnyTag(params int[])`

```csharp
public static bool HasAnyTag(this IEntity entity, params int[] tags)
```

- **Description:** Checks if the entity contains any of the specified numeric tags.
- **Parameter:** `tags` – Array of numeric tag IDs.
- **Returns:** `true` if the entity has at least one of the tags; otherwise, `false`.

</details>

---

<details>
  <summary>
    <h2 id="-values"> 🔑️ Values</h2>
    <br> Provides extension methods for values
  </summary>

#### `AddValue(string, object)`

```csharp
public static void AddValue(this IEntity entity, string key, object value)
```

- **Description:** Adds a value to the entity.
- **Parameters:**
    - `key` – The name of the value.
    - `value` – The value to add.

#### `AddValue(string, object, out int)`

```csharp
public static void AddValue(this IEntity entity, string key, object value, out int id)
```

- **Description:** Adds a value to the entity and returns the corresponding ID.
- **Parameters:**
    - `key` – The name of the value.
    - `value` – The value to add.
- **Output:** `id` – The numeric ID assigned to the key.

#### `AddValue<T>(string, T)`

```csharp
public static void AddValue<T>(this IEntity entity, string key, T value) where T : struct
```

- **Description:** Adds a strongly-typed value to the entity.
- **Parameters:**
    - `key` – The name of the value.
    - `value` – The strongly-typed value to add.

#### `AddValue<T>(string, T, out int)`

```csharp
public static void AddValue<T>(this IEntity entity, string key, T value, out int id) where T : struct
```

- **Description:** Adds a strongly-typed value and retrieves its ID.
- **Parameters:**
    - `key` – The name of the value.
    - `value` – The strongly-typed value to add.
- **Output:** `id` – The numeric ID assigned to the key.

#### `AddValues(IEnumerable<KeyValuePair<int, object>>)`

```csharp
public static void AddValues(this IEntity entity, IEnumerable<KeyValuePair<int, object>> values)
```

- **Description:** Adds multiple values to the entity.
- **Parameter:** `values` – Collection of key-value pairs (numeric keys) to add.

#### `AddValues(IEnumerable<KeyValuePair<string, object>>)`

```csharp
public static void AddValues(this IEntity entity, IEnumerable<KeyValuePair<string, object>> values)
```

- **Description:** Adds multiple values to the entity using string keys.
- **Parameter:** `values` – Collection of key-value pairs (string keys) to add.

#### `DelValue(string)`

```csharp
public static bool DelValue(this IEntity entity, string key)
```

- **Description:** Removes a value from the entity.
- **Parameter:** `key` – The name of the value to remove.
- **Returns:** `true` if the value was removed; otherwise, `false`.

#### `GetValue<T>(string)`

```csharp
public static T GetValue<T>(this IEntity entity, string key)
```

- **Description:** Retrieves a value of type `T` associated with the given key.
- **Parameter:** `key` – The name of the value.
- **Returns:** The value of type `T`.

#### `TryGetValue<T>(string, out T)`

```csharp
public static bool TryGetValue<T>(this IEntity entity, string key, out T value)
```

- **Description:** Tries to retrieve a value of type `T` associated with the given key.
- **Parameter:** `key` – The name of the value.
- **Output:** `value` – The retrieved value if successful.
- **Returns:** `true` if the value exists and was retrieved; otherwise, `false`.

#### `SetValue(string, object)`

```csharp
public static void SetValue(this IEntity entity, string key, object value)
```

- **Description:** Sets a value in the entity.
- **Parameters:**
    - `key` – The name of the value.
    - `value` – The value to set.

#### `SetValue<T>(string, T)`

```csharp
public static void SetValue<T>(this IEntity entity, string key, T value) where T : struct
```

- **Description:** Sets a strongly-typed value in the entity.
- **Parameters:**
    - `key` – The name of the value.
    - `value` – The strongly-typed value to set.

#### `HasValue(string)`

```csharp
public static bool HasValue(this IEntity entity, string key)
```

- **Description:** Checks if the entity has a value with the given key.
- **Parameter:** `key` – The name of the value.
- **Returns:** `true` if the value exists; otherwise, `false`.

#### `DisposeValues()`

```csharp
public static void DisposeValues(this IEntity entity)
```

- **Description:** Disposes all disposable values stored in the entity.

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