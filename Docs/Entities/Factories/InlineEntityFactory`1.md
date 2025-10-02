# 🧩️ InlineEntityFactory\<E>

```csharp
public class InlineEntityFactory<E> : IEntityFactory<E> where E : IEntity
```

- **Description:** A lightweight, inline factory implementation that wraps a **delegate (`Func<E>`)**, allowing fast
  entity creation without defining a full class.

- **Type Parameter:** `E` — The type of [IEntity](../Entities/IEntity.md) to produce.
- **Inheritance:** [IEntityFactory\<E>](IEntityFactory%601.md)
- **See also:** [InlineEntityFactory](InlineEntityFactory.md)

> [!TIP]
> **InlineFactory** can be used as great mock for Unit-tests.
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
