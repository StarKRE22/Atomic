# 🧩 IEntityCollection&lt;E&gt;

```csharp
public interface IEntityCollection<E> : IReadOnlyEntityCollection<E>, ICollection<E>, IDisposable where E : IEntity
```

- **Description:** Represents a **mutable collection of entities** of type `E`. Supports standard collection operations
  and provides utility methods for working with entity lifecycles.
- **Inheritance:** [IReadOnlyEntityCollection\<E>](IReadOnlyEntityCollection%601.md), `ICollection<E>`, `IDisposable`.
- **Type Parameter:** `E` — The type of entity stored in the collection, must
  implement [IEntity](../Entities/IEntity.md).
- **Note:** Use this interface when you need **both read / write access** to a collection of entities **and** reactive
  notifications from the base read-only interface.

---

## 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** Gets the number of entities in the collection.

#### `IsReadOnly`

```csharp
public bool IsReadOnly { get; }
```

- **Description:** Indicates whether the collection is read-only.
- **Returns:** Usually `false` for mutable collections.

---

## 🏹 Methods

#### `Add(E)`

```csharp
public bool Add(E entity);
```

- **Description:** Adds an entity to the collection.
- **Parameter:** `entity` — The entity to add.
- **Returns:** `true` if the entity was successfully added; `false` if it already existed.
- **Events:** Triggers `OnAdded` and `OnStateChanged` from the read-only interface.

#### `Remove(E)`

```csharp
public bool Remove(E entity);
```

- **Description:** Removes the first occurrence of a specific entity from the collection.
- **Parameter:** `entity` — The entity to remove.
- **Returns:** `true` if the entity was removed; otherwise `false`.
- **Events:** Triggers `OnRemoved` and `OnStateChanged`.

#### `Clear()`

```csharp
public void Clear();
```

- **Description:** Removes all entities from the collection.
- **Events:** Triggers multiple `OnRemoved` events (one per entity) and `OnStateChanged`.

#### `Contains(E)`

```csharp
public bool Contains(E entity);
```

- **Description:** Determines whether the collection contains a specific entity.
- **Parameter:** `entity` — The entity to locate.
- **Returns:** `true` if the entity is found; otherwise `false`.

#### `CopyTo(E[], int)`

```csharp
public void CopyTo(E[] array, int arrayIndex);
```

- **Description:** Copies all entities into the specified array, starting at a particular index.
- **Parameters:**
    - `array` — The destination array.
    - `arrayIndex` — The zero-based index in the array where copying begins.

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Releases resources used by the collection.
- **Remarks:** Call this when the collection is no longer needed to clean up internal resources or subscriptions.

#### `GetEnumerator()`

```csharp
public IEnumerator<E> GetEnumerator();
```

- **Description:** Returns an enumerator that iterates through the collection.
- **Returns:** An `IEnumerator<E>` for enumeration.

---

## 🗂 Example of Usage

```csharp
IEntityCollection<GameEntity> entities = ...;

// Subscribe to events
entities.OnAdded += e => Console.WriteLine($"Added entity: {e.Name}");
entities.OnRemoved += e => Console.WriteLine($"Removed entity: {e.Name}");
entities.OnStateChanged += () => Console.WriteLine("Collection state changed");

// Add and remove entities
entities.Add(new GameEntity("Entity1"));
entities.Remove(someEntity);

// Check for existence
if (entities.Contains(someEntity))
{
    Console.WriteLine("Entity exists in the collection");
}

// Copy to array
var array = new GameEntity[entities.Count];
entities.CopyTo(array, 0);

// Iterate over entities
foreach (var entity in entities)
{
    Console.WriteLine(entity.Name);
}

// Dispose when done
entities.Dispose();
```