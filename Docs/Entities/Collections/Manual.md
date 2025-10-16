# 🧩️ Entity Collections

**Entity Collections** provide structured ways to store, access, and manage instances
of [entity](../Entities/Manual.md) and its subclasses. They can be **read-only** or **mutable**, **generic** or
**non-generic**, and are designed for **high performance** and **reactive notifications**. Collections may also include
**utility extensions** for batch operations, initialization, disposal, and Unity-specific
entity handling.

---

## 🗂 Examples of Usage

### 1️⃣ EntityCollection Usage

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

// Check an entity for existance
entities.Contains(someEntity);

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

### 2️⃣ EntityCollection\<E>

Assume we have a concrete entity type:

```csharp
public class GameEntity : Entity
{
}
```

Use generic version of the entity collection:

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

// Check an entity for existance
entities.Contains(someEntity);

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

---

### 3️⃣ Extensions

```csharp
entities.AddRange(new GameEntity("Goblin"), new GameEntity("Orc"));
entities.InitEntities();
entities.DisposeEntities();
```

---

## 🔍 API Reference

Below is a list of available collection types:

- [IReadOnlyEntityCollection](IReadOnlyEntityCollection.md) <!-- + -->
- [IReadOnlyEntityCollection&lt;E&gt;](IReadOnlyEntityCollection%601.md) <!-- + -->
- [IEntityCollection](IEntityCollection.md) <!-- + -->
- [IEntityCollection&lt;E&gt;](IEntityCollection%601.md) <!-- + -->
- [EntityCollection](EntityCollection.md) <!-- + -->
- [EntityCollection&lt;E&gt;](EntityCollection%601.md) <!-- + -->
- [Extensions](Extensions.md) <!-- + -->

---

## ⚙️ Key Characteristics

- **Insertion & Removal** – Near constant-time operations thanks to internal hash table management.
- **Lookup** – O(1) average complexity for `Contains`.
- **Enumeration** – Iterates in insertion order with minimal overhead.
- **Memory Efficiency** – Uses pooled arrays for temporary operations, avoiding unnecessary allocations.
- **Use Case** – Suitable for **real-time applications** (e.g., games, simulations) managing large sets of entities
  efficiently.

---

## 📝 Notes

- Use **generic collections** (`EntityCollection<E>` / `IEntityCollection<E>`) for type safety.
- Use **non-generic collections** when working with heterogeneous entity types.
- Use **read-only interfaces** (`IReadOnlyEntityCollection`) to safely expose collections without allowing
  modifications.
- Use **Extensions** to simplify batch operations, initialization, and disposal.
- Collections are optimized for **fast lookup**, **ordered enumeration**, and **reactive notifications** (`OnAdded`,
  `OnRemoved`, `OnStateChanged`).
