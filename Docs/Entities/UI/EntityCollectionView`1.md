# 🧩 EntityCollectionView<E, V>

```csharp
public abstract class EntityCollectionView<E, V> : MonoBehaviour, IEnumerable<KeyValuePair<E, V>>
    where E : class, IEntity
    where V : EntityView<E>
```

- **Description:** A base class for managing collections of entity views in a Unity scene.  
  Provides functionality to show, hide, add, remove, and clear entity views, backed by a pool of reusable instances.
- **Type Parameters:**
    - `E` — The type of entity managed by this collection. Must implement [IEntity](../Entities/IEntity.md).
    - `V` — The type of entity view associated with the entities: [EntityView\<E>](EntityView%601.md).
- **Inheritance:** `MonoBehaviour`
- **Usage:** Use for visualizing dynamic collections of entities (like UI lists, enemy groups, or pooled game objects)
  efficiently.
- **See also:** [EntityViewPool<E, V>](EntityViewPool%601.md)

---

## 🛠 Inspector Settings

| Parameter  | Description                                                                   |
|------------|-------------------------------------------------------------------------------|
| `viewport` | The Transform under which all entity views will be parented in the hierarchy. |
| `viewPool` | The `EntityViewPool<E, V>` responsible for instantiating and reusing views.   |

---

## ⚡ Events

#### `OnAdded`

```csharp  
public event Action<E, V> OnAdded;  
```

- **Description:** This event is triggered **every time** a new view is created and displayed for an entity. This
  happens:
    - When calling `Show(source)` for all existing entities in the source.
    - When manually calling `Add(entity)` for a new entity.

- **Parameters:**
    - `E entity` — The entity for which the view was created.
    - `V view` — The created view instance now tracked by the collection.
- **Note:** The event **does not fire** if the entity is already displayed in the collection (duplicates are ignored).

#### `OnRemoved`

```csharp
public event Action<E, V> OnRemoved;  
```

- **Description:** This event is triggered **every time** a view is removed or returned to the pool. This happens:
    - When calling `Hide()` (removes all active views).
    - When calling `Remove(entity)` for a specific entity.
    - When calling `Clear()` (removes all entities).

- **Parameters:**
    - `E entity` — The entity whose view was removed.
    - `V view` — The view that was hidden and returned to the pool.

- **Note:** The event is called **before** the view is actually returned to the pool, giving you a chance to temporarily
  use it (e.g., play an animation).

---

## 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** The number of active entity views currently tracked by this collection.

#### `IsVisible`

```csharp
public bool IsVisible { get; }
```

- **Description:** Indicates whether this collection is currently bound to a source entity collection.

---

## 🏹 Methods

#### `Show(IReadOnlyEntityCollection<E> source)`

```csharp
public void Show(IReadOnlyEntityCollection<E> source);
```

- **Description:** Binds the collection view to a source of entities and creates views for all current entities.
- **Parameter:** `source` — The collection of entities to visualize.
- **Throws:** `ArgumentNullException` if `source` is null.

#### `Hide()`

```csharp
public void Hide();
```

- **Description:** Unbinds the collection from the source and removes all active views.

#### `Get(E entity)`

```csharp
public V Get(E entity);
```

- **Description:** Returns the view associated with a specific entity.
- **Parameter:** `entity` — The entity whose view is requested.
- **Returns:** The active view instance.
- **Throws:** `KeyNotFoundException` if the entity is not in the collection.

#### `TryGet(E entity, out V view)`

```csharp
public bool TryGet(E entity, out V view);
```

- **Description:** Tries to retrieve the view for a given entity.
- **Returns:** `true` if a view exists, `false` otherwise.

#### `Contains(E entity)`

```csharp
public bool Contains(E entity);
```

- **Description:** Checks whether a view exists for the specified entity.

#### `Add(E entity)`

```csharp
public void Add(E entity);
```

- **Description:** Creates and shows a view for the specified entity if it does not already exist.
- **Parameter:** `entity` — The entity to visualize.

#### `Remove(E entity)`

```csharp
public void Remove(E entity);
```

- **Description:** Hides and returns the view associated with the specified entity to the pool.
- **Parameter:** `entity` — The entity to remove.

#### `Clear()`

```csharp
public void Clear();
```

- **Description:** Removes all active views and returns them to the pool.

#### `GetEnumerator()`

```csharp
public IEnumerator<KeyValuePair<E, V>> GetEnumerator();
```

- **Description:** Returns an enumerator that iterates through all active entity-view pairs in the collection.
  This allows you to use foreach to iterate over each entity and its associated view.
- **Returns:** IEnumerator<KeyValuePair<E, V>> — An enumerator for the entity-view pairs.

#### `GetName(E entity)`

```csharp
protected virtual string GetName(E entity);
```

- **Description:** Determines the name of the prefab to use for the given entity.
- **Parameter:** `entity` — The entity to evaluate.
- **Returns:** The name used to rent a view from the pool.
- **Default Behavior:** Returns `entity.Name`.
- **Override:** Can be overridden to provide custom logic for prefab selection, grouping, or localization.

---

## 🗂 Example of Usage

### 1️⃣ Binding a Collection

```csharp
var collection = ... // IReadOnlyEntityCollection<MyEntity>
myCollectionView.Show(collection);
```

---

### 2️⃣ Adding/Removing Views Manually

```csharp
myCollectionView.Add(entity);
myCollectionView.Remove(entity);
```

---

### 3️⃣ Clearing All Views

```csharp
myCollectionView.Clear();
```
