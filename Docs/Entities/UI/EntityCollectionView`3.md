# 🧩 EntityCollectionView\<K, E, V>

A generic component that manually manages active [EntityView\<E>](EntityView%601.md) instances for a specific entity type.
It rents views from an [EntityViewPool\<K, E, V>](EntityViewPool%603.md), tracks entity-view mappings, and returns
views to the pool when they are removed.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Custom Collection View](#ex1)
    - [Managing Views](#ex2)
- [Inspector Settings](#-inspector-settings)
    - [Parameters](#-parameters)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Events](#-events)
        - [OnAdded](#onadded)
        - [OnRemoved](#onremoved)
    - [Properties](#-properties)
        - [Count](#count)
    - [Methods](#-methods)
        - [Get(E)](#gete)
        - [TryGet(E, out V)](#trygete-out-v)
        - [Contains(E)](#containse)
        - [Add(E)](#adde)
        - [Remove(E)](#removee)
        - [Remove(V)](#removev)
        - [Clear()](#clear)
        - [GetEnumerator()](#getenumerator)
- [See Also](#-see-also)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Custom Collection View

Create a typed collection view for a specific entity/view pair:

```csharp
public interface IUnitEntity : IEntity
{
}

public class UnitView : EntityView<IUnitEntity>
{
}

public sealed class UnitCollectionView : EntityCollectionView<string, IUnitEntity, UnitView>
{
    protected override string GetKey(IUnitEntity entity) => entity.Name;
}
```

Attach it to a GameObject and assign a pool.

---

<div id="ex2"></div>

### 2️⃣ Managing Views

```csharp
UnitCollectionView collectionView = ...;
IUnitEntity someEntity = ...;

// Add a view for an entity
UnitView createdView = collectionView.Add(someEntity);

// Remove a specific entity view
collectionView.Remove(someEntity);

// Remove by view instance
collectionView.Remove(createdView);

// Clear all active views
collectionView.Clear();

// Querying
bool exists = collectionView.Contains(someEntity);
if (collectionView.TryGet(someEntity, out UnitView view))
{
    Debug.Log($"Found view for {someEntity}: {view.name}");
}
UnitView directView = collectionView.Get(someEntity);

// Iteration
foreach (KeyValuePair<IUnitEntity, UnitView> pair in collectionView)
{
    Debug.Log($"Entity: {pair.Key}, View: {pair.Value.name}");
}
```

---

## 🛠 Inspector Settings

| Parameter  | Description                                                                    |
|------------|--------------------------------------------------------------------------------|
| `viewport` | The transform under which spawned views will be placed in the scene hierarchy. |
| `pool`     | The [EntityViewPool\<K, E, V>](EntityViewPool%603.md) used to rent/return views. |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public abstract class EntityCollectionView<K, E, V> : MonoBehaviour, IReadOnlyCollection<KeyValuePair<E, V>>
    where E : class, IEntity
    where V : EntityView<E>
```

- **Type Parameters:**
    - `K` — The key type used to select a prefab from the pool.
    - `E` — The entity type. Must implement [IEntity](../Entities/IEntity.md).
    - `V` — The view type. Must inherit from [EntityView\<E>](EntityView%601.md).
- **Inheritance:** `MonoBehaviour`, `IReadOnlyCollection<KeyValuePair<E, V>>`

---

### ⚡ Events

#### `OnAdded`

```csharp
public event Action<E, V> OnAdded;
```

- **Description:** Raised after a view is spawned and activated for a newly added entity.

#### `OnRemoved`

```csharp
public event Action<E, V> OnRemoved;
```

- **Description:** Raised before a view is deactivated and returned to the pool for a removed entity.

---

### 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** The number of active entity views currently tracked.

---

### 🏹 Methods

#### `Get(E)`

```csharp
public V Get(E entity);
```

- **Description:** Gets the view associated with the entity.
- **Parameter:** `entity` — The entity whose view is requested.
- **Returns:** The active view for the entity.
- **Throws:** `KeyNotFoundException` if the entity is not tracked.

#### `TryGet(E, out V)`

```csharp
public bool TryGet(E entity, out V view);
```

- **Description:** Attempts to retrieve the view for the entity without throwing.
- **Parameter:** `entity` — The entity whose view is requested.
- **Returns:** `true` if a view exists, `false` otherwise.

#### `Contains(E)`

```csharp
public bool Contains(E entity);
```

- **Description:** Checks whether a view exists for the entity.

#### `Add(E)`

```csharp
public V Add(E entity);
```

- **Description:** Rents a view from the pool, activates it for the entity, and tracks the pair.
- **Parameter:** `entity` — The entity to visualize.
- **Returns:** The spawned and activated view.
- **Details:** If a view already exists for the entity, returns the existing view.

#### `Remove(E)`

```csharp
public void Remove(E entity);
```

- **Description:** Deactivates the view and returns it to the pool.
- **Parameter:** `entity` — The entity whose view should be removed.

#### `Remove(V)`

```csharp
public void Remove(V view);
```

- **Description:** Removes the view by resolving its attached entity.
- **Parameter:** `view` — The view to remove.

#### `Clear()`

```csharp
public void Clear();
```

- **Description:** Removes all active views and returns them to the pool.

#### `GetEnumerator()`

```csharp
public Dictionary<E, V>.Enumerator GetEnumerator();
```

- **Description:** Returns an enumerator over all entity-view pairs.

---

## 🔗 See Also

- [EntityCollectionView](EntityCollectionView.md) — non-generic wrapper keyed by entity name.
- [EntityWorldView\<K, E, V>](EntityWorldView%603.md) — auto-synchronizing version bound to an entity collection.
- [Entity UI Manual](Manual.md)
