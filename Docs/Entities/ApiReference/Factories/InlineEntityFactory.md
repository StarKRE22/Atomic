# 🧩️ InlineEntityFactory

`InlineEntityFactory` is a **lightweight, inline factory** for creating `IEntity` instances.  
It wraps a **delegate (`Func<E>`)**, allowing fast, disposable, or inline entity creation without defining a full class.

---

## Key Features

- **Lightweight** – Minimal overhead, stores only a delegate reference.
- **Flexible** – Can be used for quick inline factories or lambda-based creation.
- **Generic & Non-Generic** – Supports both `IEntity` and strongly-typed entities.
- **Reusable** – Can be instantiated wherever a factory is required, without creating a separate class.
- **Type-Safe** – Generic version ensures correct entity type is produced without casting.

---

## Class: InlineEntityFactory (Non-Generic)
```csharp
public class InlineEntityFactory : InlineEntityFactory<IEntity>, IEntityFactory
{
    public InlineEntityFactory(Func<IEntity> createFunc) : base(createFunc)
    {
    }
}
```
- Wraps a `Func<IEntity>` delegate to create entities inline.
- Useful when a quick non-generic factory is needed without extra class definitions.

---

## Class: InlineEntityFactory&lt;E&gt; (Generic)
```csharp
public class InlineEntityFactory<E> : IEntityFactory<E> where E : IEntity
{
    private readonly Func<E> createFunc;

    public InlineEntityFactory(Func<E> createFunc) =>
        _creator = createFunc ?? throw new ArgumentNullException(nameof(creator));

    public T Create() => createFunc.Invoke();
}
```
- Wraps a `Func<E>` delegate to create strongly-typed entities.
- Throws `ArgumentNullException` if the creator delegate is null.
- Ensures type safety for `IEntity` subclasses.

---

## Example Usage

### Example #1. Non-Generic Inline Factory
```csharp
//Create a factory
var factory = new InlineEntityFactory(() =>
{
    var entity = new Entity();
    entity.AddValue<int>("Health", 100);
    return entity;
});

//Create an entity
IEntity myEntity = factory.Create();
```
- Quick and simple entity creation without defining a separate class.
- Useful for temporary or disposable entity setups.

---

### Example #2. Generic Inline Factory
```csharp
var unitFactory = new InlineEntityFactory<UnitEntity>(() =>
{
    var unit = new UnitEntity();
    unit.AddValue<int>("Health", 150);
    unit.AddValue<int>("Attack", 25);
    return unit;
});

UnitEntity myUnit = unitFactory.Create();
```
- Type-safe creation for specific entity types.
- No casting needed when retrieving the entity instance.

---

## Remarks
- `InlineEntityFactory` is ideal for **lightweight, reusable, and inline** entity creation.
- Particularly useful in tests, procedural content generation, or temporary scene entities.  
