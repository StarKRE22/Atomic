# 🧩 Entity View System

**Entity View System** provides tools for visualizing and managing [entity](../Entities/Manual.md) instances in Unity
scenes. It includes **views**, **catalogs**, **pools**, and **collections** to create flexible, reusable, and efficient
UI representations of entities. Components can be **generic** or **non-generic**, depending on the use case.

---

## 📑 Table of Contents 

- [Examples of Usage](#-examples-of-usage)
  - [EntityView](#ex1)
  - [EntityViewCatalog](#ex2)
  - [EntityViewPool](#ex3)
  - [EntityCollectionView](#ex4)
- [API Reference](#-api-reference)
- [Notes](#-notes)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ EntityView

Below is an example of setting up `EntityView<` that represents a tank entity.

#### 1. Attach `Atomic/Entities/Entity View` to a GameObject

<img width="450" height="" alt="Entity component" src="../../Images/EntityView.png" />

#### 2. Create an entity installer for the view

```csharp
public sealed class TankViewInstaller : SceneEntityInstaller
{
    [SerializeField] private TakeDamageViewBehaviour _takeDamageBehaviour;
    [SerializeField] private PositionViewBehaviour _positionBehaviour;
    [SerializeField] private RotationViewBehaviour _rotationBehaviour;
    [SerializeField] private TeamColorViewBehaviour _teamColorBehaviour;
    [SerializeField] private WeaponRecoilViewBehaviour _weaponRecoilBehaviour;
    
    public override void Install(IEntity entity)
    {
        entity.AddBehaviour(_takeDamageBehaviour);
        entity.AddBehaviour(_positionBehaviour);
        entity.AddBehaviour(_rotationBehaviour);
        entity.AddBehaviour(_teamColorBehaviour);
        entity.AddBehaviour(_weaponRecoilBehaviour);
    }

    public override void Uninstall(IEntity entity)
    {
        entity.DelBehaviour(_takeDamageBehaviour);
        entity.DelBehaviour(_positionBehaviour);
        entity.DelBehaviour(_rotationBehaviour);
        entity.DelBehaviour(_teamColorBehaviour);
        entity.DelBehaviour(_weaponRecoilBehaviour);
    }
}
```

#### 3. Attach `TankViewInstaller` to the GameObject that contains the `EntityView` component

<img width="450" height="" alt="Entity component" src="../../Images/TankViewInstaller.png" />

#### 4. Drag and drop `TankViewInstaller` to the `installers` field of `EntityView`

<img width="450" height="" alt="Entity component" src="../../Images/EntityView%20(Installed).png" />

#### 5. Use this `EntityView` in the project

```csharp
// Get an instance of GameEntityView
EntityView view = ...;

// Get an instance of the entity
IEntity entity = ...;

// Start rendering the entity:
// The GameObject dynamically attaches all tags, values, and behaviours to the entity
view.Show(entity);

// Stop rendering the entity:
// The GameObject hides, and all view tags, values, and behaviours are detached from the entity
view.Hide(entity);
```

---

<div id="ex2"></div>

### 2️⃣ EntityViewCatalog

#### 1. Create Catalog Asset

Select in Unity menu: `Assets → Create → Atomic → Entities → New EntityViewCatalog`. Then add prefabs that contain
`EntityView` component.

<img width="400" height="" alt="Entity component" src="../../Images/EntityViewCatalog.png" />

#### 2. Use this `EntityViewCatalog` in the project

```csharp
// Load catalog from Resources
EntityViewCatalog catalog = Resources.Load<EntityViewCatalog>("EntityViewCatalog");

// Get prefab by index
KeyValuePair<string, EntityView> kv = catalog.GetPrefab(0);

// Get prefab by name
EntityView playerPrefab = catalog.GetPrefab("Player");
```

---

<div id="ex3"></div>

### 3️⃣ EntityViewPool

#### 1. Attach `Atomic/Entities/Entity View Pool` to an GameObject

<img width="450" height="" alt="Entity component" src="../../Images/EntityViewPool.png" />

- Assign a `Transform` to `container` to parent pooled views.
- Add one or more [EntityViewCatalog](EntityViewCatalog.md) assets to `catalogs` to preload prefabs.

#### 2. Usage in a project

```csharp
// Assume we have an instance of the pool
EntityViewPool pool = ...;

// Rent a view by name
EntityView view = pool.Rent("Player");

// Return the view to the pool
pool.Return("Player", view);

// Destroy all pooled views
pool.Clear();

// Register prefabs manually
EntityView orcPrefab, magePrefab = ...;
pool.RegisterPrefab("Orc", orcPrefab);
pool.RegisterPrefab("Mage", magePrefab);

// Unregister prefabs manually
pool.UnregisterPrefab("Orc");
pool.UnregisterPrefab("Mage");
```


---

<div id="ex4"></div>

### 4️⃣ EntityCollectionView

#### 1. Attach `Atomic/Entities/Entity Collection View` to an GameObject


<img width="450" height="" alt="Entity component" src="../../Images/EntityCollectionView.png" />

- Assign a `Transform` to `Viewport` field.
- Assign the [EntityViewPool](EntityViewPool.md) to `ViewPool` field.


#### 2. Usage in a project

```csharp
// Assume we have an instance of EntityCollectionView
EntityCollectionView collectionView = ...;

// Assume we have an instance of IReadOnlyEntityCollection
IReadOnlyEntityCollection collection = ...;

// Assume we have a single entity
IEntity someEntity = ...;

// ===== Basic Usage =====

// Bind this entity collection to the view collection 
collectionView.Show(collection);

// Unbind the current entity collection
collectionView.Hide();

// ===== Manual View Management =====

// Add a single entity view manually
collectionView.Add(someEntity);

// Remove a specific entity view manually
collectionView.Remove(someEntity);

// Clear all active entity views manually
collectionView.Clear();

// ===== Querying and Accessing =====

// Check if a view exists for a specific entity
bool exists = collectionView.Contains(someEntity);

// Try to get the view safely
if (collectionView.TryGet(someEntity, out EntityView view))
{
    Debug.Log($"Found view for {someEntity}: {view.name}");
}

// Or get it directly (throws if not found)
EntityView directView = collectionView.Get(someEntity);

// ===== Iterating Through All Views =====

// Iterate over all entity-view pairs
foreach (KeyValuePair<IEntity, EntityView> pair in collectionView)
{
    IEntity entity = pair.Key;
    EntityView unitView = pair.Value;
    Debug.Log($"Entity: {entity}, View: {unitView.name}");
}
```

---

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

- **Views** represent the visual element for an entity and can be generic ([EntityView\<E>](EntityView%601.md))
  or non-generic ([EntityView](EntityView.md)).
- **Catalogs** provide a registry for prefabs to select the correct view for an entity.
- **Pools** manage instantiation and recycling of views for performance.
- **Collections** bind a group of entities to their corresponding views, handling add/remove events automatically.
- **Generic versions** provide type-safety and avoid casting when working with specific entity types.

---

## 📌 Best Practices

- [Building Entity System with Model & View Separation](../../BestPractices/EntitySystem.md)  <!-- + -->


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