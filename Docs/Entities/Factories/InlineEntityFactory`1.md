# 🧩️ InlineEntityFactory\<E>

```csharp
public class InlineEntityFactory<E> : IEntityFactory<E> where E : IEntity
```

- **Description:** A lightweight, inline implementation that wraps a
  creation delegate.
- **Inheritance:** [IEntityFactory\<E>](IEntityFactory%601.md)
- **Type Parameter:** `E` — The type of [IEntity](../Entities/IEntity.md) to produce.
- **Notes:** Useful for quick, on-the-fly entity creation without creating a full factory class.

---

## 🏗️ Constructors

#### `InlineEntityFactory(Func<E> createFunc)`

```csharp
public InlineEntityFactory(Func<E> createFunc);
```

- **Description:** Initializes a new inline factory that uses the specified creation function.
- **Parameter:** `createFunc` — The delegate used to instantiate the entity. Cannot be `null`.
- **Throws:** `ArgumentNullException` if `createFunc` is `null`.

---

## 🏹 Methods

#### `Create()`

```csharp
public E Create();
```

- **Description:** Invokes the wrapped creation delegate to produce a new instance of type `E`.
- **Returns:** A new instance of [IEntity](../Entities/IEntity.md).

---

## 🗂 Example of Usage

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
