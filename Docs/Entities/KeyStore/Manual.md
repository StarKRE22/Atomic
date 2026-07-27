# 🧩 Entity Key Store

**EntityKeyStore** provides a centralized, cached mapping between string-based entity keys and unique integer
identifiers. It is used by [TagKey](TagKey.md) and [ValueKey](ValueKey.md) to enable fast, type-safe access to entity
tags and values without runtime string lookups.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
  - [Sequential Algorithm](#sequential-algorithm)
  - [Using TagKey and ValueKey](#using-tagkey-and-valuekey)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

### Sequential Algorithm

The default algorithm assigns IDs in order. This is the simplest strategy and is sufficient for most projects.

```csharp
EntityKeyStore.SetAlgorithm(new SequentialEntityKeyAlgorithm());

// Generate IDs
int playerId = EntityKeyStore.NameToId("Player"); // 1
int enemyId  = EntityKeyStore.NameToId("Enemy");  // 2

// Retrieve the original name by ID
string name = EntityKeyStore.IdToName(playerId); // "Player"
```

### Using TagKey and ValueKey

The preferred way to access entity values and tags is through strongly-typed keys. Keys are typically defined once in a
static API class and reused throughout the project.

#### Define keys

```csharp
[EntityAPI]
public static partial class GameEntityAPI
{
    public static readonly TagKey IsEnemy = new(nameof(IsEnemy));
    public static readonly TagKey<IGameEntity> IsSelectable = new(nameof(IsSelectable));
    public static readonly ValueKey<IGameEntity, int> Health = new(nameof(Health));
    public static readonly ValueKey<IGameEntity, float> MoveSpeed = new(nameof(MoveSpeed));
}
```

#### Access via extension methods

```csharp
entity.AddTag(GameEntityAPI.IsEnemy);
entity.AddValue(GameEntityAPI.Health, 100);

int health = entity.GetValue(GameEntityAPI.Health);
float moveSpeed = entity.GetMoveSpeed();
bool selectable = entity.HasTag(GameEntityAPI.IsSelectable);
```

These extension methods are generated automatically when the class is marked with `[EntityAPI]`.

---

## 🔍 API Reference

### Store

- [EntityKeyStore](EntityKeyStore.md)

### Keys

- [TagKey](TagKey.md)
- [ValueKey](ValueKey.md)

### Algorithms

- [IEntityKeyAlgorithm](IEntityKeyAlgorithm.md)
- [SequentialEntityKeyAlgorithm](SequentialEntityKeyAlgorithm.md)

---

## 📌 Best Practices

- Define keys once in a static API class and reuse them everywhere.
- Use `ValueKey<E, T>` or `TagKey<E>` for type safety when multiple entity types share the same project.
- Do **not** rely on specific numeric IDs across sessions unless a deterministic algorithm is used.
- Call `EntityKeyStore.Reset()` in tests to ensure isolation between test runs.
- Keep key names stable; changing a name changes the generated ID unless a deterministic algorithm is used.
