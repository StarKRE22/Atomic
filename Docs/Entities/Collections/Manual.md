# 🧩️ Entity Collections

**Entity Collections** provide a high performance and structured ways to store, access, and manage instances
of [entity](../Entities/Manual.md) and its subclasses. They can be **read-only** or **mutable**, **generic** or
**non-generic**, and are designed for **high performance** and **reactive notifications**. Collections may also include
**utility extensions** for batch operations, initialization, disposal, and Unity-specific
entity handling.

---

## 📑 Table of Contents

- [Examples of Usage](#-example-of-usage)
    - [EntityCollection](#ex1)
    - [EntityCollection\<E>](#ex2)
    - [Extensions](#ex3)
- [API Reference](#-api-reference)
- [Performance](#-performance)
- [Best Practices](#-best-practices)
- [Notes](#-notes)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

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

<div id="ex2"></div>

### 2️⃣ EntityCollection\<E> Usage

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

<div id="ex3"></div>

### 3️⃣ Extensions Usage


```csharp
IEntityCollection<EnemyEntity> collection = ...;

// Add multiple entities
collection.AddRange(new EnemyEntity("A"), new EnemyEntity("B"));

// Add entities from enumerable
var moreEntities = new List<EnemyEntity> { new EnemyEntity("C"), new EnemyEntity("D") };
collection.AddRange(moreEntities);

// Initialize all entities
collection.InitEntities();

// Dispose all entities
collection.DisposeEntities();

// Unity-specific usage
var prefab = ...; // Some SceneEntity prefab
var entity = collection.CreateEntity(prefab, Vector3.zero, Quaternion.identity);
collection.DestroyEntity(entity, 1.0f); // destroys after 1 second
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

## 🔥 Performance

[EntityCollection\<E>](EntityCollection%601.md) is a **hybrid data structure** that combines the strengths of a
**Dictionary** and a **LinkedList**. It maintains **fast lookups** while preserving **insertion order**, making it ideal
for systems that require both efficiency and predictable iteration.

The performance measurements below were conducted on a <b>MacBook with Apple M1</b>,
using <b>1,000 elements</b> for each container type. All times are <b>median execution times</b>,
in microseconds (μs).

| Operation      | EntityCollection (μs) | HashSet (μs) |
|----------------|-----------------------|--------------|
| **Add**        | 18.91                 | 77.53        |
| **Clear**      | 13.24                 | 1.09         |
| **Contains**   | 6.00                  | 76.96        |
| **Enumerator** | 7.57                  | 26.44        |
| **Remove**     | 11.50                 | 64.53        |

While `HashSet` excels at bulk operations like `Clear`, `EntityCollection` offers **significantly faster Add, Contains,
and Remove** operations on average — making it a strong choice when **iteration order** and **low allocation overhead**
are essential.

---

## 📌 Best Practices

- [Iterating over EntityCollections, Worlds and Filters.](../../BestPractices/IteratingOverEntityCollections.md)  <!-- + -->

---

## 📝 Notes

- **Insertion & Removal** – Near constant-time operations thanks to internal hash table management.
- **Lookup** – O(1) average complexity for `Contains`.
- **Enumeration** – Iterates in insertion order with minimal overhead.
- Use **generic collections**
  ([EntityCollection\<E>](EntityCollection%601.md) / [IEntityCollection\<E>](IEntityCollection%601.md) for type
  safety.
- Use **non-generic collections** when working with heterogeneous entity types.
- Use **read-only interfaces** ([IReadOnlyEntityCollection](IReadOnlyEntityCollection.md)) to safely expose collections
  without allowing
  modifications.
- Use **Extensions** to simplify batch operations, initialization, and disposal.
- Collections are optimized for **fast lookup**, **ordered enumeration**, and **reactive notifications** (`OnAdded`,
  `OnRemoved`, `OnStateChanged`).
