# 🧩 IReadOnlyEntityCollection&lt;E&gt;

Represents a **read-only, observable collection** of entities. Provides enumeration, presence checks,
and notifications when entities are added or removed.

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
            <li><a href="#containse">Contains(E)</a></li>
            <li><a href="#copytoicollectione">CopyTo(ICollection&lt;E&gt;)</a></li>
            <li><a href="#copytoe-int">CopyTo(E[], int)</a></li>
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
IReadOnlyEntityCollection<GameEntity> entities = ...;

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
var array = new GameEntity[entities.Count];
entities.CopyTo(array, 0);

// Iterate over entities
foreach (var entity in entities)
{
    Console.WriteLine(entity.Name);
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IReadOnlyEntityCollection<E> : IReadOnlyCollection<E> where E : IEntity
```

- **Inheritance:** `IReadOnlyCollection<E>`
- **Type Parameter:** `E` — The type of entity stored in the collection, must
  implement [IEntity](../Entities/IEntity.md).
- **Note:** Use this interface when you need **read-only access** to a collection of entities but still want to **react
  to changes** via events.

---

### ⚡ Events

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;
```

- **Description:** Occurs whenever the overall state of the collection changes (e.g., an entity is added or removed).
- **Remarks:** Useful for UI updates or reactive workflows when the collection changes.

#### `OnAdded`

```csharp
public event Action<E> OnAdded;
```

- **Description:** Triggered when an entity is added to the collection.
- **Parameter:** `entity` — The entity that was added.
- **Remarks:** Allows subscribers to react specifically to new entities.

#### `OnRemoved`

```csharp
public event Action<E> OnRemoved;
```

- **Description:** Triggered when an entity is removed from the collection.
- **Parameter:** `entity` — The entity that was removed.
- **Remarks:** Allows subscribers to react specifically to removed entities.

---

### 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** Gets the number of entities in the collection.
- **Returns:** An `int` representing the total number of entities.

#### `IsReadOnly`

```csharp
public bool IsReadOnly { get; }
```

- **Description:** Indicates whether the collection is read-only.
- **Returns:** Always `true` for `IReadOnlyEntityCollection<E>`.

---

### 🏹 Methods

#### `Contains(E)`

```csharp
public bool Contains(E entity);
```

- **Description:** Checks if a specific entity exists in the collection.
- **Parameter:** `entity` — The entity to search for.
- **Returns:** `true` if the entity is present; otherwise `false`.

#### `CopyTo(ICollection<E>)`

```csharp
public void CopyTo(ICollection<E> results);
```

- **Description:** Copies all entities into the specified collection.
- **Parameter:** `results` — The destination collection.

#### `CopyTo(E[], int)`

```csharp
public void CopyTo(E[] array, int arrayIndex);
```

- **Description:** Copies all entities into the specified array, starting at a given index.
- **Parameters:**
    - `array` — The destination array.
    - `arrayIndex` — The zero-based index in the array where copying begins.

#### `GetEnumerator()`

```csharp
public IEnumerator<E> GetEnumerator();
```

- **Description:** Returns an enumerator for iterating over the collection.
- **Returns:** An `IEnumerator<E>` for enumeration.