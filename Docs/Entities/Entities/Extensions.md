# 🧩Entity Extensions

Provides extension methods for [IEntity](IEntity.md) to simplify common operations like adding / removing tags, values,
and behaviours, as well as installing installers and retrieving entities from GameObjects or collisions.

---


<details>
  <summary>
    <h2 id="-tags">🏷️ Tags</h2>
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

---

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

## 🏹 Values

| Method                                                                             | Description                                             |
|------------------------------------------------------------------------------------|---------------------------------------------------------|
| `AddValue(this IEntity entity, int key, object value)`                             | Adds a value by numeric key.                            |
| `AddValue(this IEntity entity, string key, object value)`                          | Adds a value by string key.                             |
| `AddValue<T>(this IEntity entity, int key, T value)`                               | Adds a strongly-typed value.                            |
| `AddValue<T>(this IEntity entity, string key, T value)`                            | Adds a strongly-typed value by string key.              |
| `AddValue(this IEntity entity, string key, object value, out int id)`              | Adds a value and returns its numeric ID.                |
| `AddValue<T>(this IEntity entity, string key, T value, out int id)`                | Adds a strongly-typed value and returns its ID.         |
| `SetValue(this IEntity entity, int key, object value)`                             | Sets a value by numeric key.                            |
| `SetValue<T>(this IEntity entity, string key, T value)`                            | Sets a strongly-typed value by string key.              |
| `DelValue(this IEntity entity, int key)`                                           | Removes a value by numeric key.                         |
| `DelValue(this IEntity entity, string key)`                                        | Removes a value by string key.                          |
| `HasValue(this IEntity entity, string key)`                                        | Checks if value exists by string key.                   |
| `GetValue<T>(this IEntity entity, string key)`                                     | Retrieves a value by string key.                        |
| `TryGetValue<T>(this IEntity entity, string key, out T value)`                     | Tries to retrieve a value by string key.                |
| `AddValues(this IEntity entity, IEnumerable<KeyValuePair<int, object>> values)`    | Adds multiple numeric-keyed values.                     |
| `AddValues(this IEntity entity, IEnumerable<KeyValuePair<string, object>> values)` | Adds multiple string-keyed values.                      |
| `DisposeValues(this IEntity entity)`                                               | Disposes all `IDisposable` values stored in the entity. |

---

## 🏹 Behaviours

| Method                                                                                         | Description                                                  |
|------------------------------------------------------------------------------------------------|--------------------------------------------------------------|
| `AddBehaviour(this IEntity entity, IEntityBehaviour behaviour)`                                | Adds a behaviour instance.                                   |
| `AddBehaviour<T>(this IEntity entity)`                                                         | Adds a behaviour of type `T` (default constructor required). |
| `AddBehaviours(this IEntity entity, IEnumerable<IEntityBehaviour> behaviours)`                 | Adds multiple behaviours.                                    |
| `AddBehaviours(this IEntity entity, IEntityBehaviour[] behaviours, int startIndex, int count)` | Adds a subset of behaviours from an array.                   |
| `DelBehaviours(this IEntity entity, IEnumerable<IEntityBehaviour> behaviours)`                 | Removes multiple behaviours.                                 |
| `DelBehaviours(this IEntity entity, IEntityBehaviour[] behaviours, int startIndex, int count)` | Removes a subset of behaviours from an array.                |

---


---

## 🏹 Clearing

| Method                       | Description                                    |
|------------------------------|------------------------------------------------|
| `Clear(this IEntity entity)` | Clears all data: tags, values, and behaviours. |

## 🏹 Entity Installation

| Method                                                                            | Description                                                      |
|-----------------------------------------------------------------------------------|------------------------------------------------------------------|
| `Install(this IEntity entity, IEntityInstaller installer)`                        | Installs a single installer.                                     |
| `Install(this IEntity entity, IEnumerable<IEntityInstaller> installers)`          | Installs multiple installers.                                    |
| `InstallFromScene(this IEntity entity, Scene scene, bool includeInactive = true)` | Installs all `SceneEntityInstaller` components in a scene.       |
| `InstallFromScene<T>(this T entity, Scene scene, bool includeInactive = true)`    | Generic version installing `SceneEntityInstaller<T>` components. |

---

## 🏹 Entity Retrieval (Unity Only)

| Method                                                                 | Description                                  |
|------------------------------------------------------------------------|----------------------------------------------|
| `TryGetEntity(this GameObject go, out IEntity entity)`                 | Attempts to get `IEntity` from GameObject.   |
| `TryGetEntity(this Component component, out IEntity entity)`           | Attempts to get `IEntity` from Component.    |
| `TryGetEntity(this Collision collision, out IEntity entity)`           | Attempts to get `IEntity` from 3D collision. |
| `TryGetEntity(this Collision2D collision2D, out IEntity entity)`       | Attempts to get `IEntity` from 2D collision. |
| `FindEntityInParent(this GameObject go, out IEntity entity)`           | Searches parent hierarchy for `IEntity`.     |
| `FindEntityInParent(this Component component, out IEntity entity)`     | Searches parent hierarchy for `IEntity`.     |
| `FindEntityInParent(this Collision collision, out IEntity entity)`     | Searches parent hierarchy from 3D collision. |
| `FindEntityInParent(this Collision2D collision2D, out IEntity entity)` | Searches parent hierarchy from 2D collision. |

---
