# 🧩 EntityCollection&lt;E&gt;

```csharp
public class EntityCollection<E> : IEntityCollection<E> where E : IEntity
```

- **Description:** A **high-performance, mutable collection** for storing unique entities of type `E`.  
  Combines **hash table semantics** for fast lookup and **linked list semantics** for ordered enumeration.  
  Supports standard collection operations, lifecycle management, and reactive notifications.
- **Type Parameter:** `E` — The type of entity stored in the collection, must implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** [IEntityCollection\<E>](IEntityCollection%601.md).

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
public void Dispose();
```

- **Description:** Clears the collection and releases resources.
- **Remarks:** Unsubscribes all event handlers.

#### `GetEnumerator()`

```csharp
public Enumerator GetEnumerator();
```

- **Description:** Returns a struct enumerator for iterating through the collection.
- **Returns:** `Enumerator` struct implementing `IEnumerator<E>`.

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