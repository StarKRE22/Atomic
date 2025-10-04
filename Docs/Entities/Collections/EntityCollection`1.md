# 🧩 EntityCollection&lt;E&gt;

```csharp
public class EntityCollection<E> : IEntityCollection<E> where E : IEntity
```

- **Description:** A **high-performance, mutable collection** for storing unique entities of type `E`.  
  Combines **hash table semantics** for fast lookup and **linked list semantics** for ordered enumeration.  
  Supports standard collection operations, lifecycle management, and reactive notifications.
- **Type Parameter:** `E` — The type of entity stored in the collection, must
  implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** [IEntityCollection\<E>](IEntityCollection%601.md).

---

## 🏗 Constructors

#### `EntityCollection()`

```csharp
public EntityCollection();
```

- **Description:** Initializes a new instance of the `EntityCollection<E>` class with a **default capacity**.
- **Default Capacity:** 3 entities.

#### `EntityCollection(int)`

```csharp
public EntityCollection(int capacity);
```

- **Description:** Initializes a new instance of the `EntityCollection<E>` class with a **predefined capacity**.
- **Parameter:** `capacity` — Initial capacity of the collection.
- **Exceptions:**
    - `ArgumentOutOfRangeException` if `capacity` is negative.

#### `EntityCollection(params E[])`

```csharp
public EntityCollection(params E[] entities);
```

- **Description:** Initializes a new instance with the provided array of entities.
- **Parameter:** `entities` — Array of entities to populate the collection.
- **Behavior:** Internally sets capacity to `entities.Length` and adds all entities via `AddRange`.

---

#### `EntityCollection(IReadOnlyCollection<E>)`

```csharp
public EntityCollection(IReadOnlyCollection<E> elements);
```

- **Description:** Initializes a new instance with a collection of entities.
- **Parameter:** `elements` — Collection of entities to populate the collection.
- **Behavior:** Sets capacity to `elements.Count` and adds all entities via `AddRange`.

#### `EntityCollection(IEnumerable<E>)`

```csharp
public EntityCollection(IEnumerable<E> elements);
```

- **Description:** Initializes a new instance with an enumerable of entities.
- **Parameter:** `elements` — Enumerable of entities to populate the collection.
- **Behavior:** Sets capacity to the number of elements (`elements.Count()`) and adds all entities via `AddRange`.

---

## ⚡ Events

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;
```

- **Description:** Occurs whenever the state of the collection changes, e.g., after `Add`, `Remove`, or `Clear`.

#### `OnAdded`

```csharp
public event Action<E> OnAdded;
```

- **Description:** Triggered when an entity is successfully added to the collection.
- **Parameter:** `entity` — The entity that was added.

#### `OnRemoved`

```csharp
public event Action<E> OnRemoved;
```

- **Description:** Triggered when an entity is removed from the collection.
- **Parameter:** `entity` — The entity that was removed.

---

## 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** Gets the number of entities in the collection.

#### `IsReadOnly`

```csharp
public bool IsReadOnly => false;
```

- **Description:** Indicates that the collection is mutable. Always returns `false`.

---

## 🏹 Methods

#### `Add(E)`

```csharp
public bool Add(E item);
```

- **Description:** Adds an entity to the collection.
- **Parameter:** `item` — The entity to add.
- **Returns:** `true` if successfully added; `false` if the entity already exists.
- **Events:** Triggers `OnAdded` and `OnStateChanged`.

#### `Remove(E)`

```csharp
public bool Remove(E item);
```

- **Description:** Removes a specific entity from the collection.
- **Parameter:** `item` — The entity to remove.
- **Returns:** `true` if removed; otherwise `false`.
- **Events:** Triggers `OnRemoved` and `OnStateChanged`.

#### `Clear()`

```csharp
public void Clear();
```

- **Description:** Removes all entities from the collection.
- **Events:** Triggers multiple `OnRemoved` events (one per entity) and `OnStateChanged`.

#### `Contains(E)`

```csharp
public bool Contains(E item);
```

- **Description:** Checks whether a specific entity exists in the collection.
- **Returns:** `true` if found; otherwise `false`.

#### `CopyTo(E[], int)`

```csharp
public void CopyTo(E[] array, int arrayIndex);
```

- **Description:** Copies all entities into the specified array starting at the given index.
- **Parameters:**
    - `array` — The destination array.
    - `arrayIndex` — The zero-based index at which copying begins.

#### `CopyTo(ICollection<E>)`

```csharp
public void CopyTo(ICollection<E> results);
```

- **Description:** Copies all entities into the provided collection.
- **Parameter:** `results` — The target collection.

#### `Dispose()`

```csharp
public virtual void Dispose();
```

- **Description:** Clears the collection and releases events.
- **Remarks:** Unsubscribes all event handlers.

#### `GetEnumerator()`

```csharp
public Enumerator GetEnumerator();
```

- **Description:** Returns a struct enumerator for iterating through the collection.
- **Returns:** `Enumerator` struct implementing `IEnumerator<E>`.

#### `OnAdd(E)`

```csharp
protected virtual void OnAdd(E entity);
```

- **Description:** Called automatically when an entity is **added** to the collection.
- **Parameter:** `entity` — The entity that was added.
- **Remarks:** Can be **overridden** in derived classes to implement custom logic, such as enabling the entity, logging,
  or triggering events.
- **Default behavior:** Does nothing.

#### `OnRemove(E)`

```csharp
protected virtual void OnRemove(E entity);
```

- **Description:** Called automatically when an entity is **removed** from the collection.
- **Parameter:** `entity` — The entity that was removed.
- **Remarks:** Can be **overridden** in derived classes to implement custom logic, such as disabling the entity,
  logging, or triggering events.
- **Default behavior:** Does nothing.

---

## 🧩 Enumerator

```csharp
public struct Enumerator : IEnumerator<E>
```

- **Description:** Struct-based enumerator for iterating over `EntityCollection<E>` without heap allocations.
- **Properties:** `Current` — The current entity.
- **Methods:** `MoveNext()`, `Reset()`, `Dispose()`.

---

## 🗂 Example of Usage

```csharp
var entities = new EntityCollection<GameEntity>();

// Subscribe to events
entities.OnAdded += e => Console.WriteLine($"Added entity: {e.Name}");
entities.OnRemoved += e => Console.WriteLine($"Removed entity: {e.Name}");
entities.OnStateChanged += () => Console.WriteLine("Collection state changed");

// Add entities
entities.Add(new GameEntity("Entity1"));
entities.Add(new GameEntity("Entity2"));

// Remove an entity
entities.Remove(someEntity);

// Copy to array
var array = new GameEntity[entities.Count];
entities.CopyTo(array, 0);

// Iterate over collection
foreach (var entity in entities)
{
    Console.WriteLine(entity.Name);
}

// Dispose when done
entities.Dispose();
```