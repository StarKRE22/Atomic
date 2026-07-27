# 🧩 Entity Key Store

**EntityKeyStore** provides a centralized, cached mapping between string-based entity keys and unique integer
identifiers. It is used by [TagKey](TagKey.md) and [ValueKey](ValueKey.md) to enable fast, type-safe access to entity
tags and values without runtime string lookups.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Examples of Usage](#-examples-of-usage)
  - [Sequential Algorithm](#sequential-usage)
  - [Using ValueKey and TagKey](#using-valuekey-and-tagkey)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🧩 Overview

All entity tags and values are stored internally as integer IDs. The `EntityKeyStore` converts string names to IDs and
back, caching results for performance:

- `NameToId(name)` — converts a string to an integer ID
- `IdToName(id)` — converts an integer ID back to a string name
- `SetAlgorithm(algorithm)` — changes the ID generation strategy
- `Reset()` — clears the cache and resets the algorithm

The default algorithm is [SequentialEntityKeyAlgorithm](SequentialEntityKeyAlgorithm.md), which assigns IDs in order.

---

## 🗂 Examples of Usage

<div id="sequential-usage"></div>

### 1️⃣ Sequential Algorithm

```csharp
EntityKeyStore.SetAlgorithm(new SequentialEntityKeyAlgorithm());

// Generate IDs
int playerId = EntityKeyStore.NameToId("Player"); // 1
int enemyId  = EntityKeyStore.NameToId("Enemy");  // 2

// Retrieve original name by ID
string name = EntityKeyStore.IdToName(playerId); // "Player"
```

<div id="valuekey-tagkey-usage"></div>

### 2️⃣ Using ValueKey and TagKey

The preferred way to access entity values and tags is through strongly-typed keys. Keys are typically defined once in a
static API class and reused throughout the project.

#### Define value keys

```csharp
public static partial class GameContextAPI
{
    public static readonly ValueKey<IGameContext, IMultiEntityPool<GameEntityType, IGameEntity>> EntityPool =
        new(nameof(EntityPool));

    public static readonly ValueKey<IGameContext, IEntityWorld<IGameEntity>> EntityWorld =
        new(nameof(EntityWorld));

    public static readonly ValueKey<IGameContext, EntitySpawnInfo[]> InitialEntities =
        new(nameof(InitialEntities));
}
```

#### Access values via extension methods

```csharp
public static class EntityUseCase
{
    public static void SpawnInitialUnits(this IGameContext gameContext)
    {
        EntitySpawnInfo[] spawnDataSet = gameContext.GetValue(GameContextAPI.InitialEntities);
        foreach (EntitySpawnInfo spawnInfo in spawnDataSet)
        {
            GameEntityType entityType = spawnInfo.entityType;
            foreach (Vector2Int point in spawnInfo.points)
                gameContext.Spawn(entityType, point, out _, notify: false);
        }
    }
}
```

#### Define tag keys

Tag keys work the same way:

```csharp
public static partial class GameEntityAPI
{
    public static readonly TagKey IsEnemy = new(nameof(IsEnemy));
    public static readonly TagKey<IGameEntity> IsSelectable = new(nameof(IsSelectable));
}
```

#### Access tags via extension methods

```csharp
public static void MarkAsEnemy(this IGameEntity entity) =>
    entity.AddTag(GameEntityAPI.IsEnemy);

public static bool IsEnemy(this IGameEntity entity) =>
    entity.HasTag(GameEntityAPI.IsEnemy);
```

These extension methods (`AddTag`, `HasTag`, `GetValue`, `SetValue`, etc.) are provided by
`Extensions_Tags.cs` and `Extensions_Values.cs`. They accept either `string`, `int`, or strongly-typed keys.

---

## 🔍 API Reference

### EntityKeyStore

- [EntityKeyStore](EntityKeyStore.md)

### Keys

- [TagKey](TagKey.md)
- [TagKey\<E\>](TagKey%601.md)
- [ValueKey\<T\>](ValueKey%601.md)
- [ValueKey\<E, T\>](ValueKey%602.md)

### Algorithms

- [IEntityKeyAlgorithm](IEntityKeyAlgorithm.md)
- [SequentialEntityKeyAlgorithm](SequentialEntityKeyAlgorithm.md)

---

## 📌 Best Practices

- Define keys once in a static API class and reuse them everywhere.
- Use `ValueKey<E, T>` or `TagKey<E>` for type safety when multiple entity types share the same project.
- Do **not** rely on specific numeric IDs across sessions — they are generated at runtime.
- Call `EntityKeyStore.Reset()` in tests to ensure isolation between test runs.
- Keep key names stable; changing a name changes the generated ID unless a deterministic algorithm is used.
