# 🧩 EntityViewCatalog

A **ScriptableObject** that serves as a catalog of `EntityViewBase` prefabs.  
Allows retrieving prefabs by index or name and provides a centralized collection for entity view assets.

> [!NOTE]  
> Useful for managing multiple entity view prefabs in a single asset and accessing them at runtime or in the editor.

---

## EntityViewCatalog
```csharp
[CreateAssetMenu(
    fileName = "EntityViewCatalog",
    menuName = "Atomic/Entities/New EntityViewCatalog"
)]
public class EntityViewCatalog : ScriptableObject
```
- Stores a collection of `EntityViewBase` prefabs in `_prefabs`.
- Can be created via **Assets → Create → Atomic → Entities → New EntityViewCatalog**.

---

## Inspector Settings

### prefabs
```csharp
[SerializeField] internal List<EntityViewBase> _prefabs;
```
- The list of entity view prefabs included in the catalog.
- Serialized to appear in the inspector for easy editing.

---

## Properties

### Count
```csharp
public int Count => _prefabs.Count;
```
- Returns the number of prefabs stored in the catalog.

---

## Methods

### GetPrefab (by index)
```csharp
public KeyValuePair<string, EntityViewBase> GetPrefab(int index)
```
- Retrieves a prefab at the specified index along with its name.
- **Parameters:**
    - `index` — Index of the prefab in `_prefabs`.
- **Returns:** `KeyValuePair<string, EntityViewBase>` where the key is the prefab’s name and the value is the prefab instance.

### GetPrefab (by name)
```csharp
public EntityViewBase GetPrefab(string name)
```
- Retrieves a prefab matching the specified name.
- **Parameters:**
    - `name` — The name of the prefab to find.
- **Returns:** The corresponding `EntityViewBase` prefab.
- **Throws:** `Exception` if no prefab with the specified name exists.

### GetName
```csharp
protected internal virtual string GetName(EntityViewBase prefab) => prefab.Name;
```
- Retrieves the name of a given prefab.
- Can be overridden to provide custom naming logic.

---

## Example Usage
```csharp
var catalog = Resources.Load<EntityViewCatalog>("EntityViewCatalog");

// Get prefab by index
EntityViewBase prefabEntry = catalog.GetPrefab(0);


// Get prefab by name
EntityViewBase soldierView = catalog.GetPrefab("SoldierView");
```
