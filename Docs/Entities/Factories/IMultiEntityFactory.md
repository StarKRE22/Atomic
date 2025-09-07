# 🧩 IMultiEntityFactory

`IMultiEntityFactory` is a **registry interface** for storing and retrieving entity factories by key.  
It allows dynamic registration, removal, and creation of entities in a type-safe and structured way.

---

## Key Features

- **Dynamic Registration** – Factories can be added or removed at runtime.
- **Generic & Non-Generic** – Supports typed keys (`TKey`) and a convenient string-keyed shortcut.
- **Factory Integration** – Factories implement `IEntityFactory<E>` and encapsulate entity creation logic.
- **Type Safety** – Ensures that entities created by factories conform to `IEntity`.

---

## Interface: IMultiEntityFactory
```csharp
public interface IMultiEntityFactory : IMultiEntityFactory<string, IEntity>
{
}
```
- Shortcut for the common case of **string-keyed factory registry**.
- Useful for managing collections of entity factories by name.

---

## Interface: IMultiEntityFactory<TKey, E>

```csharp
public interface IMultiEntityFactory<in TKey, E> where E : IEntity
{
    void Add(TKey key, IEntityFactory<E> factory);
    
    void Remove(TKey key);
    
    E Create(TKey key);
}
```

| Method                                     | Type   | Description                                                               |
|--------------------------------------------|--------|---------------------------------------------------------------------------|
| `Add(TKey key, IEntityFactory<E> factory)` | `void` | Registers a new entity factory under the specified key.                   |
| `Remove(TKey key)`                         | `void` | Removes the entity factory associated with the specified key.             |
| `Create(TKey key)`                         | `E`    | Creates a new entity instance using the factory registered under the key. |

---

## Example Usage

### Example #1. String-Keyed Registry
```csharp
IMultiEntityFactory factoryRegistry = new MultiEntityFactory();
factoryRegistry.Add("Orc", new InlineEntityFactory(() => new EnemyEntity("Orc")));
factoryRegistry.Add("Goblin", new InlineEntityFactory(() => new EnemyEntity("Goblin")));

var orc = factoryRegistry.Create("Orc");
var goblin = factoryRegistry.Create("Goblin");

```
- Registers factories by **string key**.
- Creates entities dynamically using their keys.

---

### Example #2. Typed Key Registry

```csharp
public enum EnemyType 
{
    Orc,
    Goblin,
    Troll
}

IMultiEntityFactory<EnemyType, EnemyEntity> typedRegistry = new MultiEntityFactory<EnemyType, EnemyEntity>();
typedRegistry.Add(EnemyType.Orc, new InlineEntityFactory<EnemyEntity>(() => new EnemyEntity("Orc")));
typedRegistry.Add(EnemyType.Goblin, new InlineEntityFactory<EnemyEntity>(() => new EnemyEntity("Goblin")));

var orc = typedRegistry.Create(EnemyType.Orc);
var goblin = typedRegistry.Create(EnemyType.Goblin);

```
- Uses a **typed enum key** for safe and clear access.
- Ensures type consistency for created entities.

---

## Remarks

- Use **`IMultiEntityFactory`** for quick string-keyed factory registries.
- Use **`IMultiEntityFactory<TKey, E>`** for more advanced scenarios with typed keys.
- Supports **runtime modifications**: adding, removing, and creating entities dynamically.