# 🧩 EntityRegistry

A **global registry** responsible for tracking and managing all [IEntity](../Entities/IEntity.md) instances. Provides *
*unique ID assignment**, **lookup by ID**, and **name-based search utilities**. Automatically reuses IDs for
unregistered entities to avoid ID overflow.

---

## 📑 Table of Contents

<ul>
  <li><a href="#-example-of-usage">Example of Usage</a></li>
  <li><a href="#-api-reference">API Reference</a>
    <ul>
      <li><a href="#-type">Type</a></li>
      <li>
        <details>
          <summary><a href="#-events">Events</a></summary>
          <ul>
            <li><a href="#onstatechanged">OnStateChanged</a></li>
            <li><a href="#onadded">OnAdded</a></li>
            <li><a href="#onremoved">OnRemoved</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-properties">Properties</a></summary>
          <ul>
            <li><a href="#instance">Instance</a></li>
            <li><a href="#count">Count</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-methods">Methods</a></summary>
          <ul>
            <li><a href="#containsint">Contains(int)</a></li>
            <li><a href="#containsientity">Contains(IEntity)</a></li>
            <li><a href="#copytoicollectionientity">CopyTo(ICollection&lt;IEntity&gt;)</a></li>
            <li><a href="#copytoientity-int">CopyTo(IEntity[], int)</a></li>
            <li><a href="#trygetint-out-ientity">TryGet(int, out IEntity)</a></li>
            <li><a href="#getint">Get(int)</a></li>
            <li><a href="#getunsafe">GetUnsafe&lt;T&gt;(int)</a></li>
            <li><a href="#trygetunsafe">TryGetUnsafe&lt;T&gt;(int, out T)</a></li>
          </ul>
        </details>
      </li>
    </ul>
  </li>
  <li><a href="#-notes">Notes</a></li>
</ul>

---

## 🗂 Example of Usage

```csharp  
EntityRegistry registry = EntityRegistry.Instance;

// Retrieving entity by id
IEntity someEntity = registry.Get(id);

// Attempts to retrieve an entity by id
if (registry.TryGet(id, out IEntity found))  
    Debug.Log($"Found entity: {found}");

// Check for entity existence
if (registry.Contains(someEntity))
    // Do something...

// Iterating over all entities
foreach (IEntity entity in registry)  
    Debug.Log($"Entity: {entity}");

// Using unsafe generic retrieval
PlayerEntity playerEntity = registry.GetUnsafe<PlayerEntity>(id);

if (registry.TryGetUnsafe(id, out PlayerEntity maybeEntity))
    Debug.Log($"Unsafe found: {maybeEntity}");

// Clearing all entities
registry.Clear();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp  
public sealed class EntityRegistry : IReadOnlyEntityCollection<IEntity>  
```

- **Inheritance:** [IReadOnlyEntityCollection\<E>](../Collections/IReadOnlyEntityCollection%601.md)
- **See also:** [EntityWorld](../Worlds/EntityWorld.md), [EntityCollection](../Collections/EntityCollection.md)

---

### ⚡ Events

#### `OnStateChanged`

```csharp  
public event Action OnStateChanged;  
```

- **Description:** Raised whenever the registry state changes (entity added/removed).

#### `OnAdded`

```csharp  
public event Action<IEntity> OnAdded;  
```

- **Description:** Raised when a new entity is registered.
- **Parameter:** `entity` — The entity that was added.

#### `OnRemoved`

```csharp  
public event Action<IEntity> OnRemoved;  
```

- **Description:** Raised when an entity is removed from the registry.
- **Parameter:** `entity` — The entity that was removed.

---

### 🔑 Properties

#### `Instance`

```csharp  
public static EntityRegistry Instance { get; }  
```

- **Description:** Gets the global singleton instance of the registry.

#### `Count`

```csharp  
public int Count { get; }  
```

- **Description:** Gets the number of currently registered entities.

---

### 🏹 Methods

#### `Contains(int)`

```csharp  
public bool Contains(int id);  
```

- **Description:** Checks whether an entity with the given unique ID is currently registered.
- **Parameter:** `id` — The unique identifier of the entity.
- **Returns:** `true` if the entity exists, otherwise `false`.

#### `Contains(IEntity)`

```csharp  
public bool Contains(IEntity entity);  
```

- **Description:** Checks whether the provided entity instance is currently registered.
- **Parameter:** `entity` — The entity instance to check.
- **Returns:** `true` if the entity is found, otherwise `false`.

#### `CopyTo(ICollection<IEntity>)`

```csharp  
public void CopyTo(ICollection<IEntity> results);  
```

- **Description:** Copies all registered entities into the provided collection.
- **Parameter:** `results` — The target collection.

#### `CopyTo(IEntity[], int)`

```csharp  
public void CopyTo(IEntity[] array, int arrayIndex);  
```

- **Description:** Copies all registered entities into the specified array, starting at the given index.
- **Parameters:**
    - `array` — Target array to copy entities into.
    - `arrayIndex` — The starting position in the target array.
- **Exception:** `ArgumentOutOfRangeException` — If the index is invalid.

#### `TryGet(int, out IEntity)`

```csharp  
public bool TryGet(int id, out IEntity entity);  
```

- **Description:** Attempts to retrieve an entity by its unique ID without throwing exceptions.
- **Parameters:**
    - `id` — The unique identifier of the entity.
    - `entity` — Output parameter containing the found entity if successful; otherwise `null`.
- **Returns:** `true` if the entity was found, otherwise `false`.

#### `Get(int)`

```csharp  
public IEntity Get(int id);  
```

- **Description:** Retrieves an entity by its unique ID.
- **Parameter:** `id` — The unique identifier of the entity.
- **Returns:** The entity instance.
- **Exception:** `KeyNotFoundException` — If no entity with the specified ID exists.

<div id="getunsafe"></div>

#### `GetUnsafe<T>(int)`

```csharp  
public T GetUnsafe<T>(int id) where T : class, IEntity;
```

- **Description:** Retrieves an entity by its unique ID using an unsafe cast for maximum performance.
- **Parameter:** `id` — The unique identifier of the entity.
- **Returns:** The entity cast to `<T>` if found.
- **Exception:** `KeyNotFoundException` — If no entity with the specified ID exists.

<div id="trygetunsafe"></div>

#### `TryGetUnsafe<T>(int, out T)`

```csharp  
public bool TryGetUnsafe<T>(int id, out T entity) where T : class, IEntity;
```

- **Description:** Attempts to retrieve an entity by its unique ID using an unsafe cast for maximum performance.
- **Parameters:**
    - `id` — The unique identifier of the entity.
    - `entity` — Output parameter containing the found entity if successful; otherwise `null`.
- **Returns:** `true` if the entity was found, otherwise `false`.
---

## 📝 Notes

- Acts as a **global tracker** — unlike [EntityWorld](../Worlds/EntityWorld.md), which is scoped per world.
- Provides **fast lookup** by ID using an internal hash-based structure.
- Uses **stack recycling** for IDs to avoid fragmentation.
- Events (`OnAdded`, `OnRemoved`) allow reacting to entity lifecycle globally.
- Automatically reset in **Unity Editor** before entering Play Mode.
