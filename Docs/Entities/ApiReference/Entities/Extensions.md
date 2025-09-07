# IEntity Extensions

Provides extension methods for `IEntity` to simplify common operations like adding/removing tags, values, and behaviours, as well as installing installers and retrieving entities from GameObjects or collisions.

---

## Clearing

| Method                       | Description                                    |
|------------------------------|------------------------------------------------|
| `Clear(this IEntity entity)` | Clears all data: tags, values, and behaviours. |

---

## Tags

| Method                                                   | Description                            |
|----------------------------------------------------------|----------------------------------------|
| `AddTag(this IEntity entity, int key)`                   | Adds a tag by numeric ID.              |
| `AddTag(this IEntity entity, string key)`                | Adds a tag by string name.             |
| `AddTag(this IEntity entity, string key, out int id)`    | Adds a tag and returns its numeric ID. |
| `DelTag(this IEntity entity, int key)`                   | Removes a tag by numeric ID.           |
| `DelTag(this IEntity entity, string key)`                | Removes a tag by string name.          |
| `HasTag(this IEntity entity, int key)`                   | Checks for a tag by numeric ID.        |
| `HasTag(this IEntity entity, string key)`                | Checks for a tag by string name.       |
| `HasAllTags(this IEntity entity, params int[] tags)`     | Checks if all numeric tags exist.      |
| `HasAllTags(this IEntity entity, params string[] tags)`  | Checks if all named tags exist.        |
| `HasAnyTag(this IEntity entity, params int[] tags)`      | Checks if any numeric tag exists.      |
| `HasAnyTag(this IEntity entity, params string[] tags)`   | Checks if any named tag exists.        |
| `AddTags(this IEntity entity, IEnumerable<int> tags)`    | Adds multiple numeric tags.            |
| `AddTags(this IEntity entity, IEnumerable<string> tags)` | Adds multiple named tags.              |

---

## Values

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

## Behaviours

| Method                                                                                         | Description                                                  |
|------------------------------------------------------------------------------------------------|--------------------------------------------------------------|
| `AddBehaviour(this IEntity entity, IEntityBehaviour behaviour)`                                | Adds a behaviour instance.                                   |
| `AddBehaviour<T>(this IEntity entity)`                                                         | Adds a behaviour of type `T` (default constructor required). |
| `AddBehaviours(this IEntity entity, IEnumerable<IEntityBehaviour> behaviours)`                 | Adds multiple behaviours.                                    |
| `AddBehaviours(this IEntity entity, IEntityBehaviour[] behaviours, int startIndex, int count)` | Adds a subset of behaviours from an array.                   |
| `DelBehaviours(this IEntity entity, IEnumerable<IEntityBehaviour> behaviours)`                 | Removes multiple behaviours.                                 |
| `DelBehaviours(this IEntity entity, IEntityBehaviour[] behaviours, int startIndex, int count)` | Removes a subset of behaviours from an array.                |

---

## Entity Installation

| Method                                                                            | Description                                                      |
|-----------------------------------------------------------------------------------|------------------------------------------------------------------|
| `Install(this IEntity entity, IEntityInstaller installer)`                        | Installs a single installer.                                     |
| `Install(this IEntity entity, IEnumerable<IEntityInstaller> installers)`          | Installs multiple installers.                                    |
| `InstallFromScene(this IEntity entity, Scene scene, bool includeInactive = true)` | Installs all `SceneEntityInstaller` components in a scene.       |
| `InstallFromScene<T>(this T entity, Scene scene, bool includeInactive = true)`    | Generic version installing `SceneEntityInstaller<T>` components. |

---

## Entity Retrieval (Unity Only)

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
