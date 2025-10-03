# 🧩 IReadOnlyEntityCollection

```csharp
public interface IReadOnlyEntityCollection : IReadOnlyEntityCollection<IEntity>
```
- **Description:** A **non-generic, read-only, observable collection** of entities. This interface is a specialization for collections of [IEntity](../Entities/IEntity.md).
- **Inheritance:** [IReadOnlyEntityCollection\<E>](IReadOnlyEntityCollection%601.md).
- **Note:** Use this interface when you need a **read-only collection of entities** without specifying a generic type.

---

## ⚡ Events

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;
```

- **Description:** Occurs whenever the overall state of the collection changes (e.g., an entity is added or removed).

#### `OnAdded`

```csharp
public event Action<IEntity> OnAdded;
```

- **Description:** Triggered when an entity is added to the collection.
- **Parameter:** `entity` — The entity that was added.

#### `OnRemoved`

```csharp
public event Action<IEntity> OnRemoved;
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
public bool IsReadOnly { get; }
```

- **Description:** Indicates whether the collection is read-only.
- **Returns:** Always `true` for this interface.

---

## 🏹 Methods

#### `Contains(IEntity)`

```csharp
public bool Contains(IEntity entity);
```

- **Description:** Checks if a specific entity exists in the collection.
- **Parameter:** `entity` — The entity to search for.
- **Returns:** `true` if the entity is present; otherwise `false`.

#### `CopyTo(ICollection<IEntity>)`

```csharp
public void CopyTo(ICollection<IEntity> results);
```

- **Description:** Copies all entities into the specified collection.
- **Parameter:** `results` — The destination collection.

#### `CopyTo(IEntity[], int)`

```csharp
public void CopyTo(IEntity[] array, int arrayIndex);
```

- **Description:** Copies all entities into the specified array, starting at a given index.
- **Parameters:**
  - `array` — The destination array.
  - `arrayIndex` — The zero-based index in the array where copying begins.

#### `GetEnumerator()`

```csharp
public IEnumerator<IEntity> GetEnumerator();
```

- **Description:** Returns an enumerator for iterating over the collection.
- **Returns:** An `IEnumerator<IEntity>` for enumeration.

---

## 🗂 Example of Usage

```csharp
IReadOnlyEntityCollection entities = ...;

// Subscribe to events
entities.OnAdded += e => Console.WriteLine($"Added entity: {e.Name}");
entities.OnRemoved += e => Console.WriteLine($"Removed entity: {e.Name}");
entities.OnStateChanged += () => Console.WriteLine("Collection state changed");

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
```
