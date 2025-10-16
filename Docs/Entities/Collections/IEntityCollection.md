# 🧩 IEntityCollection

A **non-generic, mutable collection of entities**, specialized for the base [IEntity](../Entities/IEntity.md) type.
Provides a common interface for managing entity collections without specifying a generic entity type.

---

## 📑 Table of Contents

<ul>
  <li><a href="#-example-of-usage">Example of Usage</a></li>
  <li>
    <a href="#-api-reference">API Reference</a>
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
            <li><a href="#count">Count</a></li>
            <li><a href="#isreadonly">IsReadOnly</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-methods">Methods</a></summary>
          <ul>
            <li><a href="#addientity">Add(IEntity)</a></li>
            <li><a href="#removeientity">Remove(IEntity)</a></li>
            <li><a href="#clear">Clear()</a></li>
            <li><a href="#containsientity">Contains(IEntity)</a></li>
            <li><a href="#copytoicollectionientity">CopyTo(ICollection&lt;IEntity&gt;)</a></li>
            <li><a href="#copytoientity-int">CopyTo(IEntity[], int)</a></li>
            <li><a href="#dispose">Dispose()</a></li>
            <li><a href="#getenumerator">GetEnumerator()</a></li>
          </ul>
        </details>
      </li>
    </ul>
  </li>
</ul>

---

## 🗂 Example of Usage

```csharp
IEntityCollection entities = ...;

// Subscribe to events
entities.OnAdded += e => Console.WriteLine($"Added entity: {e.Name}");
entities.OnRemoved += e => Console.WriteLine($"Removed entity: {e.Name}");
entities.OnStateChanged += () => Console.WriteLine("Collection state changed");

// Add and remove entities
entities.Add(new Entity("Entity1"));
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

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEntityCollection : IEntityCollection<IEntity>, IReadOnlyEntityCollection
```
- **Inheritance:** 
  [IEntityCollection\<E>](IEntityCollection%601.md), 
  [IReadOnlyEntityCollection](IReadOnlyEntityCollection.md).
- **Note:** Use this interface when you need a **mutable entity collection** without generics, but still want access to
  events and utility methods from the base interfaces.

---

### ⚡ Events

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

### 🔑 Properties

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

### 🏹 Methods

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