# 🧩 EntityCollection

A **non-generic, high-performance mutable collection** for storing
unique [IEntity](../Entities/IEntity.md) instances. Provides the same functionality as the generic base class but
simplifies usage when generic typing is unnecessary.

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
          <summary><a href="#-constructors">Constructors</a></summary>
          <ul>
            <li><a href="#default-constructor">Default Constructor</a></li>
            <li><a href="#capacity-based-constructor">Capacity-based Constructor</a></li>
            <li><a href="#params-constructor">Params Constructor</a></li>
            <li><a href="#icollection-constructor">ICollection Constructor</a></li>
            <li><a href="#ienumerable-constructor">IEnumerable Constructor</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-events">Events</a></summary>
          <ul>
            <li><a href="#onadded">OnAdded</a></li>
            <li><a href="#onremoved">OnRemoved</a></li>
            <li><a href="#onstatechanged">OnStateChanged</a></li>
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
            <li><a href="#copytoientity-int">CopyTo(IEntity[], int)</a></li>
            <li><a href="#copytoicollectionientity">CopyTo(ICollection&lt;IEntity&gt;)</a></li>
            <li><a href="#dispose">Dispose()</a></li>
            <li><a href="#getenumerator">GetEnumerator()</a></li>
            <li><a href="#onaddientity">OnAdd(IEntity)</a></li>
            <li><a href="#onremoveientity">OnRemove(IEntity)</a></li>
          </ul>
        </details>
      </li>
    </ul>
  </li>
</ul>

---

## 🗂 Example of Usage

```csharp
var entities = new EntityCollection();

// Subscribe to events
entities.OnAdded += e => Console.WriteLine($"Added entity: {e.Name}");
entities.OnRemoved += e => Console.WriteLine($"Removed entity: {e.Name}");
entities.OnStateChanged += () => Console.WriteLine("Collection state changed");

// Add entities
entities.Add(new Entity("Entity1"));
entities.Add(new Entity("Entity2"));

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

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class EntityCollection : EntityCollection<IEntity>, IEntityCollection
```

- **Inheritance:** [EntityCollection\<E>](EntityCollection%601.md), [IEntityCollection](IEntityCollection.md).

---

### 🏗 Constructors

#### `Default Constructor`

```csharp
public EntityCollection();
```

- **Description:** Initializes a new **empty** instance of the non-generic `EntityCollection`.

#### `Capacity-based Constructor`

```csharp
public EntityCollection(int capacity);
```

- **Description:** Initializes a new instance with a **specified initial capacity**.
- **Parameter:** `capacity` — The number of entities the collection can initially store without resizing.

#### `Params Constructor`

```csharp
public EntityCollection(params IEntity[] entities);
```

- **Description:** Initializes a new instance with a **parameter array of entities**.
- **Parameter:** `entities` — Array of entities to populate the collection.
- **Behavior:** Sets capacity to `entities.Length` and adds all entities via `AddRange`.

#### `ICollection Constructor`

```csharp
public EntityCollection(IReadOnlyCollection<IEntity> elements);
```

- **Description:** Initializes a new instance using a **read-only collection of entities**.
- **Parameter:** `elements` — Collection of entities to populate the collection.
- **Behavior:** Sets capacity to `elements.Count` and adds all entities via `AddRange`.

#### `IEnumerable Constructor`

```csharp
public EntityCollection(IEnumerable<IEntity> elements);
```

- **Description:** Initializes a new instance using an **enumerable of entities**.
- **Parameter:** `elements` — Enumerable of entities to populate the collection.
- **Behavior:** Sets capacity to the number of elements (`elements.Count()`) and adds all entities via `AddRange`.

---

### ⚡ Events

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

### 🔑 Properties

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

### 🏹 Methods

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

#### `OnAdd(IEntity)`

```csharp
protected virtual void OnAdd(IEntity entity);
```

- **Description:** Called automatically when an entity is **added** to the collection.
- **Parameter:** `entity` — The entity that was added.
- **Remarks:** Can be **overridden** in derived classes to implement custom logic, such as enabling the entity, logging,
  or triggering events.
- **Default behavior:** Does nothing.

#### `OnRemove(IEntity)`

```csharp
protected virtual void OnRemove(IEntity entity);
```

- **Description:** Called automatically when an entity is **removed** from the collection.
- **Parameter:** `entity` — The entity that was removed.
- **Remarks:** Can be **overridden** in derived classes to implement custom logic, such as disabling the entity,
  logging, or triggering events.
- **Default behavior:** Does nothing.