# 🧩 EntityCollectionView

`EntityCollectionView` is a Unity `MonoBehaviour` that manages a collection of `EntityView` instances in the scene. It supports dynamically showing, hiding, adding, and removing entity views, with efficient reuse via a view pool.

This system comes in two forms:

* **Non-generic** version (`EntityCollectionView`) for working with `IEntity`.
* **Generic** version (`EntityCollectionView<E, V>`) for type-specific entities and views.

---

## Key Features

### Dynamic View Management
- Automatically creates views for newly added entities.
- Removes views when entities are removed or despawned.
- Supports clearing all active views at once.

### Efficient Pooling
- Uses a reusable pool of `EntityView` instances.
- Reduces runtime allocations and improves performance.
- Automatically returns views to the pool when removed.

### Type Safety
- Generic version allows compile-time type checking for entity and view types.
- Non-generic version provides convenience for general-purpose usage.

### Flexibility
- Custom entity naming logic via `GetEntityName`.
- Bind to any `IReadOnlyEntityCollection<E>` source.
- Supports Unity scene hierarchy organization through a viewport transform.

---

## EntityCollectionView
**A shorthand for `EntityCollectionView<IEntity, EntityView>`.**

```csharp
[DisallowMultipleComponent]
public class EntityCollectionView : EntityCollectionView<IEntity, EntityView>
{
}
```

## EntityCollectionView<E, V>
**Generic version for managing entity-specific views.**

```csharp
public abstract class EntityCollectionView<E, V> : MonoBehaviour, IEnumerable<KeyValuePair<E, V>>
    where E : IEntity
    where V : EntityView<E>
{
}
```

---

## Methods

### Show
```csharp
void Show(IReadOnlyEntityCollection<E> source);
```
- **Purpose**: Binds the collection to a source and displays all entity views.
- **Parameter**: `source` — The collection of entities to visualize.
- **Throws**: `ArgumentNullException` if `source` is null.

### Hide
```csharp
void Hide();
```
- **Purpose**: Hides all entity views and detaches from the source.

### Add
```csharp
void Add(E entity);
```
- **Purpose**: Creates and displays a view for the specified entity.
- **Behavior**: If a view already exists, it will not create a duplicate.

### RemoveView
```csharp
void Remove(E entity);
```
- **Purpose**: Hides and returns the view to the pool.

### Clear
```csharp
void Clear();
```
- **Purpose**: Removes all views from the collection and returns them to the pool.

### Get
```csharp
V Get(E entity);
```
- **Purpose**: Retrieves the view associated with a specific entity.
- **Throws**: `KeyNotFoundException` if the entity has no active view.

### GetEnumerator
```csharp
IEnumerator<KeyValuePair<E, V>> GetEnumerator();
```
- **Purpose**: Iterates through all entity-view pairs in the collection.

### GetName
```csharp
protected virtual string GetEntityName(E entity);
```
- **Purpose**: Determines the name used to retrieve the view prefab for a given entity.
- **Default Behavior**: Returns `entity.Name`.
- **Override**: Supports custom naming logic for categories or localization.

---

## Example Usage

### Example #1: Binding Entity Collection (Show / Hide)

```csharp
// Find the collection view in the scene
var collectionView = FindObjectOfType<EntityCollectionView>();

// Bind the collection view to a source of entities
collectionView.Show(entityCollection);

// The collection view automatically creates views for all entities in the collection
// and will keep them synchronized as entities are added or removed

// When done, hide all views
collectionView.Hide();
```

**Description:**
- Demonstrates binding a collection to `EntityCollectionView`.
- Views are automatically created and destroyed based on the bound entity collection.
- Useful for automatic synchronization of entity views without manual intervention.

### Example #2: Manual View Management
```csharp
// Access the collection view manually
var collectionView = FindObjectOfType<EntityCollectionView>();

// Show a view for a specific entity
collectionView.Add(someEntity);

// Retrieve the view for custom logic
EntityView view = collectionView.Get(someEntity);

// Remove the view when needed
collectionView.Remove(someEntity);
```

**Description:**
- Shows manual control over individual views.
- Useful when you want to add or remove entity views outside the automatic collection binding.
- Provides flexibility for one-off operations on specific entities.