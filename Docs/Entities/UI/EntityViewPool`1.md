# 🧩 EntityViewPool<E, V>

```csharp
public abstract class EntityViewPool<E, V> : MonoBehaviour
    where E : class, IEntity
    where V : EntityView<E>
```

- **Description:** A pool system for managing reusable [EntityView\<E>](EntityView%601.md) instances.  
  Supports preloading from catalogs, runtime instantiation, renting, and returning views to minimize runtime
  allocations.
- **Type Parameters:**
    - `E` — The type of entity associated with the views. Must implement [IEntity](../Entities/IEntity.md).
    - `V` — The type of entity view ([EntityView\<E>](EntityView%601.md)) stored in the pool.
- **Inheritance:** `MonoBehaviour`
- **Usage:** Use for efficient management of frequently spawned or displayed entity views.

---

## 🛠 Inspector Settings

| Parameter   | Description                                                                               |
|-------------|-------------------------------------------------------------------------------------------|
| `container` | The parent transform under which all pooled views will be stored.                         |
| `catalogs`  | A list of `EntityViewCatalog<E, V>` assets to preload view prefabs from during `Awake()`. |

---

## 🏹 Public Methods

#### `Rent(string name)`

```csharp
public V Rent(string name);
````

- **Description:** Retrieves a view instance from the pool by name.
- **Parameter:** `name` — The name of the view to retrieve.
- **Returns:** A `EntityView` instance of type `V`.
- **Throws:** `KeyNotFoundException` if no prefab with the specified name was registered.

---

#### `Return(string name, V view)`

```csharp
public void Return(string name, V view);
```

- **Description:** Returns a view instance back to its corresponding pool for reuse.
- **Parameters:**
    - `name` — The name of the view being returned.
    - `view` — The view instance to return.

---

#### `Clear()`

```csharp
public void Clear();
```

- **Description:** Clears all pooled instances and destroys their GameObjects.

---

#### `RegisterPrefab(string entityName, V prefab)`

```csharp
public void RegisterPrefab(string entityName, V prefab);
```

- **Description:** Registers a single prefab under a specific name.
- **Parameters:**
    - `entityName` — The name key to identify the prefab.
    - `prefab` — The prefab instance to register.
- **Throws:** `ArgumentException` if a prefab with the same name already exists in the dictionary.

---

#### `UnregisterPrefab(string entityName)`

```csharp
public void UnregisterPrefab(string entityName);
```

- **Description:** Removes a previously registered prefab from the pool.
- **Parameter:** `entityName` — The name key of the prefab to remove.

---

#### `RegisterPrefabs(EntityViewCatalog<E, V> catalog)`

```csharp
public void RegisterPrefabs(EntityViewCatalog<E, V> catalog);
```

- **Description:** Registers all prefabs from the specified catalog.
- **Parameter:** `catalog` — The catalog containing prefabs to register.

---

#### `UnregisterPrefabs(EntityViewCatalog<E, V> catalog)`

```csharp
public void UnregisterPrefabs(EntityViewCatalog<E, V> catalog);
```

- **Description:** Removes all prefabs from the specified catalog from the registry.
- **Parameter:** `catalog` — The catalog containing prefabs to unregister.

---

## 🏹 Protected Methods

#### `Awake()`

```csharp
protected virtual void Awake();
```

- **Description:** Unity lifecycle callback invoked when the component is initialized.

---

## 🗂 Examples of Usage

### 1️⃣ Setup in Inspector

- Assign a `Transform` to `container` to parent pooled views.
- Add one or more `EntityViewCatalog` assets to `catalogs` to preload prefabs.

---

### 2️⃣ Renting and Returning Views

```csharp
// Rent a view by name
PlayerView playerView = pool.Rent("PlayerHUD");

// Use the view
playerView.Show(playerEntity);

// Return view to the pool
pool.Return("PlayerHUD", playerView);
```

---

### 3️⃣ Clearing All Pooled Views

```csharp
// Destroy all pooled instances
pool.Clear();
```
