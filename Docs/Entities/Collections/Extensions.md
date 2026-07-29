# 🧩 EntityCollection Extensions

Provides **extension methods** for working with [IEntityCollection\<E>](IEntityCollection%601.md). These methods
simplify adding multiple entities, creating/destroying scene entities (Unity), and initializing or
disposing collections.

---

## 📑 Table of Contents

<ul>
  <li><a href="#-example-usage">Example Usage</a></li>
  <li>
    <a href="#-api-reference">API Reference</a>
    <ul>
      <li><a href="#-type">Type</a></li>
      <li>
        <details>
          <summary><a href="#-methods">Methods</a></summary>
          <ul>
            <li><a href="#addrangeparams-e">AddRange(params E[])</a></li>
            <li><a href="#addrangeienumerablee">AddRange(IEnumerable&lt;E&gt;)</a></li>
            <li><a href="#createentity">CreateEntity()</a></li>
            <li><a href="#destroyentity">DestroyEntity()</a></li>
            <li><a href="#initentities">InitEntities()</a></li>
            <li><a href="#disposeentities">DisposeEntities()</a></li>
            <li><a href="#collectallentities">CollectAllEntities()</a></li>
          </ul>
        </details>
      </li>
    </ul>
  </li>
</ul>


---

## 🗂 Example Usage

```csharp
IEntityCollection<EnemyEntity> collection = new EntityCollection<EnemyEntity>();

// Add multiple entities
collection.AddRange(new EnemyEntity("A"), new EnemyEntity("B"));

// Add entities from enumerable
var moreEntities = new List<EnemyEntity> { new EnemyEntity("C"), new EnemyEntity("D") };
collection.AddRange(moreEntities);

// Initialize all entities
collection.InitEntities();

// Dispose all entities
collection.DisposeEntities();

// Unity-specific usage
var prefab = ...; // Some MonoEntity prefab
var entity = collection.CreateEntity(prefab, Vector3.zero, Quaternion.identity);
collection.DestroyEntity(entity, 1.0f); // destroys after 1 second
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public static class Extensions
```

---

### 🏹 Methods

#### `AddRange(params E[])`

```csharp
public static void AddRange<E>(this IEntityCollection<E> it, params E[] entities) 
    where E : IEntity;
```

- **Description:** Adds a range of entities to the collection.
- **Parameters:**
    - `it` — The target entity collection.
    - `entities` — Array of entities to add.
- **Exceptions:** Throws `ArgumentNullException` if `entities` is `null`.
- **Behavior:** Iterates over the array and calls `Add` for each entity.

#### `AddRange(IEnumerable<E>)`

```csharp
public static void AddRange<E>(this IEntityCollection<E> it, IEnumerable<E> entities) 
    where E : IEntity;
```

- **Description:** Adds a range of entities from an enumerable.
- **Parameters:**
    - `it` — The target entity collection.
    - `entities` — Enumerable of entities to add.
- **Exceptions:** Throws `ArgumentNullException` if `entities` is `null`.
- **Behavior:** Iterates over the enumerable and calls `Add` for each entity.

#### `CreateEntity()`

```csharp
public static E CreateEntity<E>(
    this IEntityCollection<E> it,
    E prefab,
    Vector3 position,
    Quaternion rotation,
    Transform parent = null
) where E : MonoEntity;
```

- **Description:** Instantiates a new `MonoEntity` based on a prefab and adds it to the collection.
- **Parameters:**
    - `it` — Target collection.
    - `prefab` — Prefab to instantiate.
    - `position` — Spawn position.
    - `rotation` — Spawn rotation.
    - `parent` — Optional parent transform.
- **Returns:** The newly created entity.
- **Behavior:** Calls `MonoEntity.Create`, adds the entity to the collection, and returns it.

#### `DestroyEntity()`

```csharp
public static void DestroyEntity<E>(this IEntityCollection<E> it, E entity, float delay = 0) 
    where E : MonoEntity;
```

- **Description:** Removes an entity from the collection and destroys its GameObject.
- **Parameters:**
    - `it` — Target collection.
    - `entity` — Entity to remove and destroy.
    - `delay` — Optional delay before destruction in seconds.
- **Behavior:** Calls `Remove` on the collection; if successful, destroys the entity using `GameObject.Destroy`.

#### `InitEntities()`

```csharp
public static void InitEntities<E>(this IEntityCollection<E> it) where E : IEntity;
```

- **Description:** Calls `Init` on all entities in the collection.
- **Parameters:**
    - `it` — The collection of entities to initialize.
- **Behavior:** Iterates over the collection and invokes `IEntity.Init` on each entity.

#### `DisposeEntities()`

```csharp
public static void DisposeEntities<E>(this IEntityCollection<E> it) where E : IEntity;
```

- **Description:** Calls `Dispose` on all entities in the collection.
- **Parameters:**
    - `it` — The collection of entities to dispose.
- **Behavior:** Iterates over the collection and invokes `IDisposable.Dispose` on each entity.

#### `CollectAllEntities()`

```csharp  
public static void CollectAllEntities<E>(this IEntityCollection<E> collection, bool includeInactive = false)
```

- **Description:** Scans the scene for all entities of type `E` and adds them to the world.
- **Type Parameter:** `E` — Type of scene entity to collect.
- **Parameters:**
  - `collection` — The target collection where entities will be added
  - `includeInactive` — Whether to include inactive GameObjects
- **Behavior:** Automatically calls `Install()` on entities before adding.
- **Notes:** Honors `includeInactiveOnCollect` to optionally include inactive GameObjects.