# 🧩️ InlineEntityFactory\<E>

A lightweight, inline factory implementation that wraps a **delegate (`Func<E>`)**, allowing fast
entity creation without defining a full class. It can be used as a lightweight mock for unit tests, allowing quick
creation of test entities without implementing a full factory class.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructor](#-constructor)
    - [Methods](#-methods)
        - [Create()](#create)

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

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class InlineEntityFactory<E> : IEntityFactory<E> where E : IEntity
```

- **Type Parameter:** `E` — The type of [IEntity](../Entities/IEntity.md) to produce.
- **Inheritance:** [IEntityFactory\<E>](IEntityFactory%601.md)
- **See also:** [InlineEntityFactory](InlineEntityFactory.md)

---

<div id="-constructor"></div>

### 🏗️ Constructor

```csharp
public InlineEntityFactory(Func<E> createFunc);
```

- **Description:** Initializes a new inline factory that uses the specified creation function.
- **Parameter:** `createFunc` — The delegate used to instantiate the entity. Cannot be `null`.
- **Throws:** `ArgumentNullException` if `createFunc` is `null`.

---

### 🏹 Methods

#### `Create()`

```csharp
public E Create();
```

- **Description:** Invokes the wrapped creation delegate to produce a new instance of type `E`.
- **Returns:** A new instance of [IEntity](../Entities/IEntity.md).