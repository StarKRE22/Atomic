# 🧩 EntityRegistry

A **global registry** responsible for tracking and managing all [IEntity](../Entities/IEntity.md)
instances. Provides **unique ID assignment**, **lookup by ID**, and **name-based search utilities**. Automatically
reused IDs for unregistered entities to avoid ID overflow.

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

//Retrieving entity by id
IEntity someEntity = registry.Get(id);

//Attempts to retrieve an entity by id
if (registry.TryGet(id, out IEntity found))  
    Debug.Log($"Found entity: {found}");  

//Check for entity existance
if (registry.Contains(someEntity))
    //Do something...

//Iterating over all entities
foreach (IEntity entity in registry)  
    Debug.Log($"Entity: {entity}");  
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp  
public sealed class EntityRegistry : IReadOnlyEntityCollection<IEntity>  
```

- **Inheritance:** [IReadOnlyEntityCollection\<E>](../Collections/IReadOnlyEntityCollection%601.md).
- **See also:** [EntityWorld](../Worlds/EntityWorld.md), [EntityCollection](../Collections/EntityCollection.md).

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

- **Description:** Checks whether an entity with the given unique ID is currently registered in the registry.
- **Parameter:** `id` — The unique identifier of the entity.
- **Returns:** `true` if the entity exists, otherwise `false`.

#### `Contains(IEntity)`

```csharp  
public bool Contains(IEntity entity);  
```

- **Description:** Checks whether the provided entity instance is currently registered in the registry.
- **Parameter:** `entity` — The entity instance to check.
- **Returns:** `true` if the entity is found, otherwise `false`.

#### `CopyTo(ICollection<IEntity>)`

```csharp  
public void CopyTo(ICollection<IEntity> results);  
```

- **Description:** Copies all registered entities into the provided collection.
- **Parameter:** `results` — The target collection. The method will clear it before adding entities.

#### `CopyTo(IEntity[], int)`

```csharp  
public void CopyTo(IEntity[] array, int arrayIndex);  
```

- **Description:** Copies all registered entities into the specified array, starting at the given index.
- **Parameters:**
    - `array` — Target array to copy entities into.
    - `arrayIndex` — The starting position in the target array.
- **Exception:** `ArgumentException` — If the array is too small.

#### `TryGet(int, out IEntity)`

```csharp  
public bool TryGet(int id, out IEntity entity);  
```

- **Description:** Attempts to retrieve an entity by its unique ID without throwing exceptions.
- **Parameters:**
    - `id` — The unique identifier of the entity.
    - `entity` — Output parameter that contains the found entity if successful; otherwise `null`.
- **Returns:** `true` if the entity was found, otherwise `false`.

#### `Get(int)`

```csharp  
public IEntity Get(int id);  
```

- **Description:** Retrieves an entity by its unique ID.
- **Parameter:** `id` — The unique identifier of the entity.
- **Return:** The entity instance.
- **Exception:**`KeyNotFoundException` — If no entity with the specified ID exists.


---

## 📝 Notes

- Acts as a **global tracker** — unlike `EntityWorld`, which is scoped per world.
- Provides **fast lookup** by ID using dictionaries.
- Uses **stack recycling** for IDs to avoid fragmentation.
- Events (`OnAdded`, `OnRemoved`) make it easy to **react to entity lifecycle** globally.
- Automatically reset in **Unity Editor** before Play Mode starts.