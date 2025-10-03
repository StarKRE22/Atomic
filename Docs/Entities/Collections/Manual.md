# 🧩️ Entity Collections

**Entity Collections** provide structured ways to store, access, and manage instances
of [IEntity](../Entities/IEntity.md) and its subclasses. They can be **read-only** or **mutable**, **generic** or
**non-generic**, and are designed for **high performance** and **reactive notifications**. Collections may also include
**utility extensions** for batch operations, initialization, disposal, and Unity-specific
entity handling.

Below is a list of available collection types:

- [IReadOnlyEntityCollection](IReadOnlyEntityCollection.md) <!-- + -->
- [IReadOnlyEntityCollection&lt;E&gt;](IReadOnlyEntityCollection%601.md) <!-- + -->
- [IEntityCollection](IEntityCollection.md) <!-- + -->
- [IEntityCollection&lt;E&gt;](IEntityCollection%601.md) <!-- + -->
- [EntityCollection](EntityCollection.md) <!-- + -->
- [EntityCollection&lt;E&gt;](EntityCollection%601.md) <!-- + -->
- [Extensions](Extensions.md) <!-- + -->

---

## 🗂 Examples of Usage

### 1️⃣ Creating a Generic Collection

```csharp
IEntityCollection<GameEntity> entities = new EntityCollection<GameEntity>();
entities.Add(new GameEntity("Hero"));
entities.Add(new GameEntity("Enemy"));
```

- **Description:** Standard mutable collection using generic typing.
- **Use Case:** When you need type-safe access to entities and full collection operations.

---

### 2️⃣ Using a Non-Generic Collection

```csharp
IEntityCollection allEntities = new EntityCollection();
allEntities.Add(new Entity("NPC"));
allEntities.Add(new Entity("Object"));
```

- **Description:** Non-generic collection for scenarios where entity type specificity is not required.
- **Use Case:** Managing heterogeneous sets of entities in a single collection.

---

### 3️⃣ Using Read-Only Collections

```csharp
IReadOnlyEntityCollection<GameEntity> readOnly = entities;
bool hasEnemy = readOnly.Contains(enemyEntity);
```

- **Description:** Provides safe, **read-only access** with change notifications.
- **Use Case:** Exposing entity collections to systems that should not modify them.

---

### 4️⃣ Batch Operations with Extensions

```csharp
entities.AddRange(new GameEntity("Goblin"), new GameEntity("Orc"));
entities.InitEntities();
entities.DisposeEntities();
```

- **Description:** Use extension methods to **add multiple entities**, **initialize**, or **dispose** them.
- **Unity-Specific:** Extensions also support `CreateEntity` and `DestroyEntity` for `SceneEntity` objects.

---

## 📝 Notes

- Use **generic collections** (`EntityCollection<E>` / `IEntityCollection<E>`) for type safety.
- Use **non-generic collections** when working with heterogeneous entity types.
- Use **read-only interfaces** (`IReadOnlyEntityCollection`) to safely expose collections without allowing
  modifications.
- Use **Extensions** to simplify batch operations, initialization, and disposal.
- Collections are optimized for **fast lookup**, **ordered enumeration**, and **reactive notifications** (`OnAdded`,
  `OnRemoved`, `OnStateChanged`).
