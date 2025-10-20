# 🧩 EntityViewPool<E, V>

A pool system for managing reusable [EntityView\<E>](EntityView%601.md) instances. Supports preloading from catalogs,
runtime instantiation, renting, and returning views to minimize runtime allocations. Use for efficient management of
frequently spawned or displayed entity views.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Pool Setup](#ex1)
    - [Pool Usage](#ex2)
- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
    - [Type](#type)
    - [Methods](#methods)
        - [Rent(string)](#rentstring)
        - [Return(string, V)](#returnstring-v)
        - [Clear()](#clear)
        - [RegisterPrefab(string, V)](#registerprefabstring-v)
        - [UnregisterPrefab(string)](#unregisterprefabstring)
        - [RegisterPrefabs(EntityViewCatalog<E, V>)](#registerprefabsentityviewcataloge-v)
        - [UnregisterPrefabs(EntityViewCatalog<E, V>)](#unregisterprefabsentityviewcataloge-v)
        - [Awake()](#awake)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Pool Setup

Below is an example of using generic entity view pool:

#### 1. Assume we have a concrete entity type

```csharp
public class IUnitEntity : IEntity
{
}
```

#### 2. Assume we have a concrete entity view type

```csharp
public class UnitView : EntityView<IUnitEntity>
{
}
```

#### 3. Assume we have a catalog for the unit views

```csharp
[CreateAssetMenu(
    fileName = "UnitViewCatalog", 
    menuName = "Example/UnitViewCatalog"
)]
public class UnitViewCatalog : EntityViewCatalog<IUnitEntity, UnitView> 
{
}
```

#### 4. Create a specific entity view pool for `UnitView`

```csharp
public sealed class UnitViewPool : EntityViewPool<IUnitEntity, UnitView>
{
}
```

#### 5. Attach this pool to an GameObject on a scene

<img width="450" height="" alt="Entity component" src="../../Images/UnitViewPool.png" />

- Assign a `Transform` to `container` to parent pooled views.
- Add one or more `UnitViewCatalog` assets to `catalogs` to preload prefabs.

---

<div id="ex2"></div>

### 2️⃣ Pool Usage

```csharp
// Assume we have an instance of UnitViewPool
UnitViewPool pool = ...;

// Rent a view by name
UnitView playerView = pool.Rent("Player");

// Use the view
playerView.Show(playerEntity);

// Return view to the pool
pool.Return("Player", playerView);

// Destroy all pooled instances
pool.Clear();

// Register prefabs manually
UnitView orcPrefab, magePrefab = ...;
pool.RegisterPrefab("Orc", orcPrefab);
pool.RegisterPrefab("Mage", magePrefab);

// Unregister prefabs manually
pool.UnregisterPrefab("Orc");
pool.UnregisterPrefab("Mage");
```

---

## 🛠 Inspector Settings

| Parameter   | Description                                                                               |
|-------------|-------------------------------------------------------------------------------------------|
| `container` | The parent transform under which all pooled views will be stored.                         |
| `catalogs`  | A list of `EntityViewCatalog<E, V>` assets to preload view prefabs from during `Awake()`. |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public abstract class EntityViewPool<E, V> : MonoBehaviour
    where E : class, IEntity
    where V : EntityView<E>
```

- **Type Parameters:**
    - `E` — The type of entity associated with the views. Must implement [IEntity](../Entities/IEntity.md).
    - `V` — The type of entity view stored in the pool.
- **Inheritance:** `MonoBehaviour`

---

### 🏹 Methods

#### `Rent(string)`

```csharp
public V Rent(string name);
````

- **Description:** Retrieves a view instance from the pool by name.
- **Parameter:** `name` — The name of the view to retrieve.
- **Returns:** A `EntityView` instance of type `V`.
- **Throws:** `KeyNotFoundException` if no prefab with the specified name was registered.

#### `Return(string, V)`

```csharp
public void Return(string name, V view);
```

- **Description:** Returns a view instance back to its corresponding pool for reuse.
- **Parameters:**
    - `name` — The name of the view being returned.
    - `view` — The view instance to return.

#### `Clear()`

```csharp
public void Clear();
```

- **Description:** Clears all pooled instances and destroys their GameObjects.

#### `RegisterPrefab(string, V)`

```csharp
public void RegisterPrefab(string entityName, V prefab);
```

- **Description:** Registers a single prefab under a specific name.
- **Parameters:**
    - `entityName` — The name key to identify the prefab.
    - `prefab` — The prefab instance to register.
- **Throws:** `ArgumentException` if a prefab with the same name already exists in the dictionary.

#### `UnregisterPrefab(string)`

```csharp
public void UnregisterPrefab(string entityName);
```

- **Description:** Removes a previously registered prefab from the pool.
- **Parameter:** `entityName` — The name key of the prefab to remove.

#### `RegisterPrefabs(EntityViewCatalog<E, V>)`

```csharp
public void RegisterPrefabs(EntityViewCatalog<E, V> catalog);
```

- **Description:** Registers all prefabs from the specified catalog.
- **Parameter:** `catalog` — The catalog containing prefabs to register.

#### `UnregisterPrefabs(EntityViewCatalog<E, V>)`

```csharp
public void UnregisterPrefabs(EntityViewCatalog<E, V> catalog);
```

- **Description:** Removes all prefabs from the specified catalog from the registry.
- **Parameter:** `catalog` — The catalog containing prefabs to unregister.

#### `Awake()`

```csharp
protected virtual void Awake();
```

- **Description:** Unity lifecycle callback invoked when the component is initialized. Loads prefabs from the assigned
  catalogs.