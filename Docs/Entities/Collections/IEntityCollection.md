# 🧩 IEntityCollection

```csharp
public interface IEntityCollection : IEntityCollection<IEntity>, IReadOnlyEntityCollection
```

- **Description:** A **non-generic, mutable collection of entities**, specialized for the base [IEntity](../Entities/IEntity.md) type.
  Provides a common interface for managing entity collections without specifying a generic entity type.
- **Inheritance:** [IEntityCollection\<E>](IEntityCollection%601.md), 
  [IReadOnlyEntityCollection](IReadOnlyEntityCollection.md).
- **Note:** Use this interface when you need a **mutable entity collection** without generics, but still want access to
  events and utility methods from the base interfaces.

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

#### `Add(IEntity)`

```csharp
public bool Add(IEntity entity);
```

- **Description:** Adds an entity to the collection.
- **Parameter:** `entity` — The entity to add.
- **Returns:** `true` if the entity was successfully added; `false` if it already existed.
- **Events:** Triggers `OnAdded` and `OnStateChanged`.

#### `Remove(IEntity)`

```csharp
public bool Remove(IEntity entity);
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

#### `Contains(IEntity)`

```csharp
public bool Contains(IEntity entity);
```

- **Description:** Determines whether the collection contains a specific entity.
- **Parameter:** `entity` — The entity to locate.
- **Returns:** `true` if the entity is found; otherwise `false`.

#### `CopyTo(IEntity[], int)`

```csharp
public void CopyTo(IEntity[] array, int arrayIndex);
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
public IEnumerator<IEntity> GetEnumerator();
```

- **Description:** Returns an enumerator that iterates through the collection.
- **Returns:** An `IEnumerator<IEntity>` for enumeration.

---

## 🗂 Example of Usage

```csharp
IEntityCollection entities = ...;

// Subscribe to events
entities.OnAdded += e => Console.WriteLine($"Added entity: {e.Name}");
entities.OnRemoved += e => Console.WriteLine($"Removed entity: {e.Name}");
entities.OnStateChanged += () => Console.WriteLine("Collection state changed");

// Add and remove entities
entities.Add(new MyEntity("Entity1"));
entities.Remove(someEntity);

// Check for existence
if (entities.Contains(someEntity))
{
    Console.WriteLine("Entity exists in the collection");
}

// Copy to array
var array = new IEntity[entities.Count];
entities.CopyTo(array, 0);

// Iterate over entities
foreach (var entity in entities)
{
    Console.WriteLine(entity.Name);
}

// Dispose when done
entities.Dispose();
```
