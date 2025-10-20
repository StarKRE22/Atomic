# 🧩 Entity View System

**Entity View System** provides tools for visualizing and managing [entity](../Entities/Manual.md) instances in Unity scenes.
It includes **views**, **catalogs**, **pools**, and **collections** to create flexible, reusable, and efficient UI
representations
of entities. Components can be **generic** or **non-generic**, depending on the use case.

---

<!--


## 🗂 Example of Usage

```csharp
// Create a default entity view without knowing the specific entity type
var args = new EntityView.CreateArgs
{
    name = "GenericEntityView",
    controlGameObject = true,
    installers = new List<SceneEntityInstaller>()
};

EntityView view = EntityView.Create(args);

// Show any IEntity instance
IEntity entity = new SomeEntity();
view.Show(entity);

// Later, hide or destroy
view.Hide();
EntityView.Destroy(view);
```


## 🗂 Examples of Usage

Below are examples demonstrating practical usage of the main **Entity UI** components.

### 1️⃣ Creating and Showing an Entity View

```csharp  
// Rent a view from the pool and bind it to an entity
EntityView<IEntity> view = entityViewPool.Rent("Enemy");
view.Show(enemyEntity);
```

- **Description:** Retrieves a view instance from the pool and associates it with an entity.
- **Use Case:** Display dynamic entities in the scene or UI efficiently without creating new GameObjects each time.

### 2️⃣ Using a View Catalog

```csharp  
// Get a prefab from a catalog and register it in a pool
EntityViewCatalog<IEntity, EntityView<IEntity>> catalog = myCatalog;
EntityView<IEntity> prefab = catalog.GetPrefab("Enemy");
entityViewPool.RegisterPrefab("Enemy", prefab);
```

- **Description:** A catalog stores reusable prefabs for different entity types, allowing centralized management.
- **Use Case:** Dynamically select which prefab to instantiate based on the entity type.

### 3️⃣ Managing an Entity View Pool

```csharp  
// Create a pool for a specific prefab
EntityViewPool<IEntity, EntityView<IEntity>> pool = new EntityViewPool<IEntity, EntityView<IEntity>>();
pool.RegisterPrefab("Enemy", enemyPrefab);

// Rent a view
EntityView<IEntity> view = pool.Rent("Enemy");

// Return the view for reuse
pool.Return("Enemy", view);
```

- **Description:** The pool manages instantiation and reuse of entity views to improve performance.
- **Use Case:** Optimize scenes where entities frequently appear and disappear (e.g., enemies, UI lists).

### 4️⃣ Using a Collection View

```csharp  
// Bind a collection of entities to a collection view
EntityCollectionView collectionView = gameObject.AddComponent<EntityCollectionView>();
collectionView.Show(entityCollection); // entityCollection is IReadOnlyEntityCollection<IEntity>

// The collection view automatically handles adding/removing views for entities
```

- **Description:** A collection view automatically creates views for entities in a collection and handles lifecycle
  events.
- **Use Case:** Display lists, grids, or groups of entities, with automatic view management for add/remove/clear
  operations.

### 5️⃣ Responding to View Events

```csharp  
collectionView.OnAdded += (entity, view) =>
{
    Debug.Log($"Entity {entity.Name} added with view {view.name}");
};

collectionView.OnRemoved += (entity, view) =>
{
    Debug.Log($"Entity {entity.Name} removed along with view {view.name}");
};
```

- **Description:** Use events to react to entities being added or removed from a collection.
- **Use Case:** Play animations, update UI, or trigger logic when views appear or disappear.

### 6️⃣ Clearing All Views

```csharp  
// Remove all active views and return them to the pool
collectionView.Clear();
```

- **Description:** Efficiently hides all entity views and returns them to the pool for future reuse.
- **Use Case:** Reset a UI panel, clear a list, or clean up a scene section.

---

-->

## 🔍 API Reference

Below is a list of available Entity UI modules:

- **EntityViews**
  - [EntityView](EntityView.md) <!-- + -->
  - [EntityView&lt;E&gt;](EntityView%601.md) <!-- + -->
- **Catalogs**
  - [EntityViewCatalog](EntityViewCatalog.md) <!-- + -->
  - [EntityViewCatalog&lt;E&gt;](EntityViewCatalog%601.md) <!-- + -->
- **Pools**
  - [EntityViewPool](EntityViewPool.md) <!-- + -->
  - [EntityViewPool&lt;E&gt;](EntityViewPool%601.md) <!-- + -->
- **Collections**
  - [EntityCollectionView](EntityCollectionView.md) <!-- + -->
  - [EntityCollectionView&lt;E&gt;](EntityCollectionView%601.md) <!-- + -->

---

## 📝 Notes

- **Views** represent the visual element for an entity and can be generic ([EntityView\<E>](EntityView%601.md) 
  or non-generic ([EntityView](EntityView.md)).
- **Catalogs** provide a registry for prefabs to select the correct view for an entity.
- **Pools** manage instantiation and recycling of views for performance.
- **Collections** bind a group of entities to their corresponding views, handling add/remove events automatically.
- **Generic versions** provide type-safety and avoid casting when working with specific entity types.
