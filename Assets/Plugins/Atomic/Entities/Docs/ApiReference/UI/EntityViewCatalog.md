# 🗂 EntityViewCatalog

The `EntityViewCatalog` provides a centralized collection of `EntityView` prefabs that can be accessed by name or index. It comes in two forms:

* **Non-generic** version (`EntityViewCatalog`) for working with `IEntity` and `EntityView`.
* **Generic** version (`EntityViewCatalog<E, V>`) for specific entity and view types.

---

## Key Features

### Centralized Prefab Storage
- Stores all `EntityView` prefabs in a single `ScriptableObject`.
- Supports access by name or index.
- Ensures consistent prefab management across scenes and systems.

### Type Safety
- Generic version allows compile-time type checking.
- Non-generic version provides convenience when strong typing is not needed.

### Easy Retrieval
- Provides `GetPrefab(int index)` for index-based access.
- Provides `GetPrefab(string name)` for name-based access.
- Supports custom naming strategies via `GetName`.

---

## EntityViewCatalog

**A shorthand class for `EntityViewCatalog<IEntity, EntityView>`.**

```csharp
[CreateAssetMenu(
    fileName = "EntityViewCatalog",
    menuName = "Atomic/Entities/New EntityViewCatalog"
)]
public class EntityViewCatalog : EntityViewCatalog<IEntity, EntityView>
{
}
```

## EntityViewCatalog<E, V>

**A generic catalog for storing entity views of a specific type.**
```csharp
public abstract class EntityViewCatalog<E, V> : ScriptableObject
    where E : IEntity
    where V : EntityView<E>
{
}
```
---

## Inspector Settings

This section describes the configurable fields of the `EntityViewCatalog` as they appear in the Unity Inspector. These settings allow you to manage and organize your entity view prefabs.

| Field Name | Type             | Description                                            | Notes                                                                 |
|------------|------------------|--------------------------------------------------------|-----------------------------------------------------------------------|
| `prefabs`  | List<EntityView> | The list of entity view prefabs stored in the catalog. | Add, remove, or reorder prefabs to control which views are available. |

### Usage Notes
- The `prefabs` list is serialized and editable in the inspector.
- Each prefab in the list should be a valid `EntityView` instance.
- Prefabs can be accessed at runtime using `GetPrefab(int index)` or `GetPrefab(string name)`.

## Methods

### GetPrefab (by index)
```csharp
KeyValuePair<string, V> GetPrefab(int index);
```
- **Purpose**: Retrieves a prefab along with its name by index.
- **Parameter**: `index` — The zero-based index of the prefab.
- **Returns**: A `KeyValuePair` where the key is the prefab's name and the value is the prefab.
- **Exceptions**: Throws `ArgumentOutOfRangeException` if the index is invalid.

### GetPrefab (by name)
```csharp
V GetPrefab(string name);
```
- **Purpose**: Retrieves a prefab by its name.
- **Parameter**: `name` — The name of the prefab to find.
- **Returns**: The matching prefab instance of type `V`.
- **Exceptions**: Throws `Exception` if no prefab with the given name exists.

### GetName
```csharp
protected internal virtual string GetName(V prefab);
```
- **Purpose**: Returns the display name of a prefab.
- **Parameter**: `prefab` — The prefab to extract the name from.
- **Returns**: The name used for identification, defaults to `EntityView.Name`.
- **Notes**: Override to implement custom naming strategies (e.g., tags, metadata, or localization).


## Example Usage
```csharp
var catalog = Resources.Load<EntityViewCatalog>("EntityViewCatalog");

// Get prefab by index
EntityView prefabEntry = catalog.GetPrefab(0);


// Get prefab by name
EntityView soldierView = catalog.GetPrefab("SoldierView");
```
