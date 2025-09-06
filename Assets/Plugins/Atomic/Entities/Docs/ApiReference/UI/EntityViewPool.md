# 🧩 EntityViewPool
A **Unity-based pool manager** for reusing `EntityView` instances by name. Reduces memory allocations and improves performance by avoiding frequent instantiations.

## Key Features

### Efficient Object Pooling
- Reuses `EntityView` instances to minimize memory allocations.
- Reduces performance overhead caused by frequent instantiation and destruction.

### Flexible Prefab Management
- Supports individual prefab registration via `RegisterPrefab`/`UnregisterPrefab`.
- Supports bulk registration from `EntityViewCatalog`s.
- Prefabs are keyed by name for easy retrieval.

### Automatic Scene Integration
- Can assign a parent container for pooled objects.
- Automatically re-parents returned views to the pool container.
- Optional activation/deactivation of `GameObject`s when rented or returned.

### Editor-Friendly
- Inspector-friendly arrays for catalogs and container assignment.
- Compatible with Odin Inspector for enhanced read-only and editor-mode display.

### Runtime Safety
- Throws informative exceptions when a requested prefab is not registered.
- Handles empty pools gracefully by instantiating new views if needed.

## EntityViewPool
```csharp
[DisallowMultipleComponent]
public class EntityViewPool : EntityViewPool<IEntity, EntityView>
```
- Inherits from `EntityViewPool<IEntity, EntityView>`.
- Manages pooling of `EntityView` instances.
- Supports Unity editor features like `[SerializeField]` and optional Odin Inspector attributes.

---

## EntityViewPool<E, V>
```csharp
public abstract class EntityViewPool<E, V> : MonoBehaviour
    where E : IEntity
    where V : EntityView<E>
```

- Generic base class for pooling `EntityView<E>` instances.
- `E` specifies the entity type (`IEntity` or a derived type).
- `V` specifies the view type (`EntityView<E>` or a derived type).
- Provides full prefab registration, rent, and return functionality.
- Supports Unity editor integration with `[SerializeField]` and optional Odin Inspector attributes.
- Can be extended for type-specific view pooling without modifying the core pool logic.

## Inspector Settings

| Parameter   | Type                | Description                                                       |
|-------------|---------------------|-------------------------------------------------------------------|
| `container` | Transform           | The parent transform under which all pooled views will be stored. |
| `catalogs`  | EntityViewCatalog[] | Array of catalogs to preload view prefabs from on `Awake`.        |

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
public V Rent(string name);
```
- Retrieves a view by name from the pool.
- Creates a new instance if the pool is empty.
- **Parameters:**
  - `name` – The name of the view to retrieve.
- **Returns:** A reusable `EntityView` instance.
- **Throws:** `KeyNotFoundException` if the prefab is not registered.

### Return
```csharp
public void Return(string name, V view);
```
- Returns a view back to the pool for future reuse.
- **Parameters:**
  - `name` – The name of the view being returned.
  - `view` – The instance to return.
- Updates the parent of the view to `container`.

### Clear
```csharp
public void Clear();
```
- Clears all pooled instances and destroys their `GameObject`s.

### RegisterPrefab
```csharp
public void RegisterPrefab(string entityName, V prefab);
```
- Adds a single prefab to the internal registry.
- **Parameters:**
  - `entityName` – Identifier for the prefab.
  - `prefab` – The prefab instance.

### UnregisterPrefab
```csharp
public void UnregisterPrefab(string entityName);
```
- Removes a single prefab from the internal registry.
- **Parameter:**
  - `entityName` – The name of the prefab to remove.

### RegisterPrefabs
```csharp
public void RegisterPrefabs(EntityViewCatalog<E, V> catalog);
```
- Adds all prefabs from a catalog to the internal registry.
- **Parameter:**
  - `catalog` – The catalog containing prefabs.

### UnregisterPrefabs
```csharp
public void UnregisterPrefabs(EntityViewCatalog<E, V> catalog);
```
- Removes all prefabs from a catalog from the internal registry.
- **Parameter:**
  - `catalog` – The catalog containing prefabs.

---

## Example Usage

### Renting and Returning a View
```csharp
// Assume we have a reference to the pool component
EntityViewPool viewPool = FindObjectOfType<EntityViewPool>();

// Rent a view by name
EntityView soldierView = viewPool.Rent("SoldierView");

// Use the view (e.g., show it in the scene)
soldierView.Show(someEntity);

// When done, return the view to the pool for reuse
viewPool.Return("SoldierView", soldierView);
```

### Adding and Removing Prefabs
```csharp
// Adding a new prefab at runtime
viewPool.RegisterPrefab("TankView", tankViewPrefab);

// Removing a prefab from the registry
viewPool.UnregisterPrefab("OldView");

// Preloading all prefabs from a catalog
EntityViewCatalog someCatalog = Resources.Load<EntityViewCatalog>("UnitViewCatalog");
viewPool.RegisterPrefabs(someCatalog);

// Removing all prefabs from a catalog
viewPool.UnregisterPrefabs(someCatalog);
```
