# 🧩 EntityCollection

```csharp
public class EntityCollection : EntityCollection<IEntity>, IEntityCollection
```

- **Description:** A **non-generic, high-performance mutable collection** for storing
  unique [IEntity](../Entities/IEntity.md) instances.  
  Provides the same functionality as the generic base class but simplifies usage when generic typing is unnecessary.
- **Inheritance:** [EntityCollection\<E>](EntityCollection%601.md), [IEntityCollection](IEntityCollection.md).

---

## 🏗 Constructors

#### `EntityCollection()`

```csharp
public EntityCollection();
```

- **Description:** Initializes a new **empty** instance of the non-generic `EntityCollection`.

---

#### `EntityCollection(int)`

```csharp
public EntityCollection(int capacity);
```

- **Description:** Initializes a new instance with a **specified initial capacity**.
- **Parameter:** `capacity` — The number of entities the collection can initially store without resizing.

---

#### `EntityCollection(params IEntity[])`

```csharp
public EntityCollection(params IEntity[] entities);
```

- **Description:** Initializes a new instance with a **parameter array of entities**.
- **Parameter:** `entities` — Array of entities to populate the collection.
- **Behavior:** Sets capacity to `entities.Length` and adds all entities via `AddRange`.

---

#### `EntityCollection(IReadOnlyCollection<IEntity>)`

```csharp
public EntityCollection(IReadOnlyCollection<IEntity> elements);
```

- **Description:** Initializes a new instance using a **read-only collection of entities**.
- **Parameter:** `elements` — Collection of entities to populate the collection.
- **Behavior:** Sets capacity to `elements.Count` and adds all entities via `AddRange`.

---

#### `EntityCollection(IEnumerable<IEntity>)`

```csharp
public EntityCollection(IEnumerable<IEntity> elements);
```

- **Description:** Initializes a new instance using an **enumerable of entities**.
- **Parameter:** `elements` — Enumerable of entities to populate the collection.
- **Behavior:** Sets capacity to the number of elements (`elements.Count()`) and adds all entities via `AddRange`.

## ⚡ Events

#### `OnAdded`

```csharp
public event Action<IEntity> OnAdded;
```

- **Description:** Triggered whenever a new entity is successfully added to the collection.
- **Parameter:** `entity` — The entity that was added.
- **Remarks:** Use this event to react to additions without iterating over the collection.

#### `OnRemoved`

```csharp
public event Action<IEntity> OnRemoved;
```

- **Description:** Triggered whenever an entity is removed from the collection.
- **Parameter:** `entity` — The entity that was removed.
- **Remarks:** Use this event to react to removals without iterating over the collection.

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;
```

- **Description:** Occurs whenever the overall state of the collection changes, e.g., after `Add`, `Remove`, or `Clear`.
- **Remarks:** Useful for UI updates or reactive systems that need to track any modification in the collection.

---

## 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** Gets the number of entities currently stored in the collection.

#### `IsReadOnly`

```csharp
public bool IsReadOnly => false;
```

- **Description:** Indicates whether the collection is read-only.
- **Remarks:** Always returns `false` for `EntityCollection` because it is mutable.

---

## 🏹 Methods

#### `Add(IEntity)`

```csharp
public bool Add(IEntity item);
```

- **Description:** Adds an entity to the collection.
- **Parameter:** `item` — The entity to add.
- **Returns:** `true` if successfully added; `false` if the entity already exists.
- **Events:** Triggers `OnAdded` and `OnStateChanged`.

#### `Remove(IEntity)`

```csharp
public bool Remove(IEntity item);
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

#### `Contains(IEntity)`

```csharp
public bool Contains(IEntity item);
```

- **Description:** Checks whether a specific entity exists in the collection.
- **Returns:** `true` if the entity is found; otherwise `false`.

#### `CopyTo(IEntity[], int)`

```csharp
public void CopyTo(IEntity[] array, int arrayIndex);
```

- **Description:** Copies all entities into the specified array starting at the given index.
- **Parameters:**
    - `array` — The destination array.
    - `arrayIndex` — The zero-based index at which copying begins.

#### `CopyTo(ICollection<IEntity>)`

```csharp
public void CopyTo(ICollection<IEntity> results);
```

- **Description:** Copies all entities into the provided collection.
- **Parameter:** `results` — The target collection.

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Clears the collection and releases resources.
- **Remarks:** Unsubscribes all event handlers.

#### `GetEnumerator()`

```csharp
public Enumerator GetEnumerator();
```

- **Description:** Returns a struct enumerator for iterating through the collection.
- **Returns:** `Enumerator` struct implementing `IEnumerator<IEntity>`.

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
var entities = new EntityCollection();

// Subscribe to events
entities.OnAdded += e => Console.WriteLine($"Added entity: {e.Name}");
entities.OnRemoved += e => Console.WriteLine($"Removed entity: {e.Name}");
entities.OnStateChanged += () => Console.WriteLine("Collection state changed");

// Add entities
entities.Add(new MyEntity("Entity1"));
entities.Add(new MyEntity("Entity2"));

// Remove an entity
entities.Remove(someEntity);

// Copy to array
var array = new IEntity[entities.Count];
entities.CopyTo(array, 0);

// Iterate over collection
foreach (var entity in entities)
{
    Console.WriteLine(entity.Name);
}

// Dispose when done
entities.Dispose();
```