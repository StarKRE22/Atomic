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

---

## Example Usage

### Example #1. String-Keyed Registry
```csharp
IMultiEntityFactory factoryRegistry = new MultiEntityFactory();
factoryRegistry.Add("Orc", new InlineEntityFactory(() => new EnemyEntity("Orc")));
factoryRegistry.Add("Goblin", new InlineEntityFactory(() => new EnemyEntity("Goblin")));

var orc = factoryRegistry.Create("Orc");
var goblin = factoryRegistry.Create("Goblin");
