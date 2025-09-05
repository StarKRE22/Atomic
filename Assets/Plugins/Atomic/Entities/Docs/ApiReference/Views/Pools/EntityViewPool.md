# 🧩 EntityViewPool

A **Unity-based pool manager** for reusing `EntityViewBase` instances based on their names. Reduces memory allocations and improves performance by avoiding frequent instantiations.

> [!NOTE]  
> Attach this component to a `GameObject` in Unity.  
> Supports preloading prefabs from catalogs and automatic reuse of views.

---

## EntityViewPool
```csharp
[DisallowMultipleComponent]
public class EntityViewPool : EntityViewPoolBase
```
- Inherits from `EntityViewPoolBase`.
- Manages pooling of `EntityViewBase` instances.
- Supports Unity editor features like `[SerializeField]` and optional Odin Inspector attributes.

---

## Inspector Settings

| Parameter   | Description                                                   |
|-------------|---------------------------------------------------------------|
| `container` | The parent transform under which all pooled views are stored. |
| `catalogs`  | Array of catalogs to preload view prefabs from on `Awake`.    |

---

## Methods

### Awake
```csharp
protected virtual void Awake();
```
- Called by Unity on initialization.
- Loads prefabs from the assigned `catalogs`.

### Rent
```csharp
public sealed override EntityViewBase Rent(string name);
```
- Retrieves a view by name from the pool.
- If the pool is empty, a new instance is created.
- Parameter:
    - `name` – The name of the view to retrieve.
- Returns: A reusable `EntityViewBase` instance.
- Throws `KeyNotFoundException` if the prefab was not registered.

### Return
```csharp
public sealed override void Return(string name, EntityViewBase view);
```
- Returns a view back to the pool for future reuse.
- Parameters:
    - `name` – The name of the view being returned.
    - `view` – The instance to return.
- Updates the parent of the view to `container`.

### Clear
```csharp
public sealed override void Clear();
```
- Clears all pooled instances and destroys their `GameObject`s.

### AddPrefab
```csharp
public void AddPrefab(string entityName, EntityViewBase prefab);
```
- Adds a single view prefab to the internal registry.
- Parameters:
    - `entityName` – Identifier for the prefab.
    - `prefab` – The prefab instance.

### RemovePrefab
```csharp
public void RemovePrefab(string entityName);
```
- Removes a single prefab from the internal registry.
- Parameter:
    - `entityName` – The name of the prefab to remove.

### AddPrefabs
```csharp
public void AddPrefabs(EntityViewCatalog catalog);
```
- Adds all prefabs from a catalog to the internal registry.
- Parameter:
    - `catalog` – The catalog containing prefabs.

### RemovePrefabs
```csharp
public void RemovePrefabs(EntityViewCatalog catalog);
```
- Removes all prefabs from a catalog from the internal registry.
- Parameter:
    - `catalog` – The catalog containing prefabs.
---
---

## Example Usage

### Create
//TODO:

### Using

```csharp
// Assume we have a reference to the pool component
EntityViewPool viewPool = FindObjectOfType<EntityViewPool>();

// Rent a view by name
EntityViewBase soldierView = viewPool.Rent("SoldierView");

// Use the view (e.g., show it in the scene)
soldierView.Show(someEntity);

// When done, return the view to the pool for reuse
viewPool.Return("SoldierView", soldierView);

// Adding a new prefab at runtime
viewPool.AddPrefab("TankView", tankViewPrefab);

// Removing a prefab from the registry
viewPool.RemovePrefab("OldView");

// Preloading all prefabs from a catalog
EntityViewCatalog someCatalog = Resources.Load<EntityViewCatalog>("UnitViewCatalog");
viewPool.AddPrefabs(someCatalog);

// Removing all prefabs from a catalog
viewPool.RemovePrefabs(someCatalog);
```

