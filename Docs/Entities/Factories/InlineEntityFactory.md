# 🧩️ InlineEntityFactory

A lightweight, inline implementation of the non-generic entity factory. It can be used as a lightweight mock for unit
tests, allowing quick creation of test entities without implementing a full factory class.

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
//Create the inline factory
var factory = new InlineEntityFactory(() =>
{
    var entity = new Entity();
    entity.AddValue<int>("Health", 100);
    return entity;
});

//Usage:
IEntity myEntity = factory.Create();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class InlineEntityFactory : InlineEntityFactory<IEntity>, IEntityFactory
```

- **Inheritance:** [InlineEntityFactory\<E>](InlineEntityFactory%601.md), [IEntityFactory](IEntityFactory.md)

---

<div id="-constructor"></div>

### 🏗️ Constructor

```csharp
public InlineEntityFactory(Func<IEntity> createFunc);
```

- **Description:** Initializes a new inline factory using the specified creation function.
- **Parameter:** `createFunc` — The delegate used to instantiate the entity. Cannot be `null`.
- **Throws:** `ArgumentNullException` if `createFunc` is `null`.

---

### 🏹 Methods

#### `Create()`

```csharp
public override IEntity Create();
```

- **Description:** Invokes the wrapped creation delegate to produce a new instance of [IEntity](../Entities/IEntity.md).
- **Returns:** A new [IEntity](../Entities/IEntity.md) instance.
- **Notes:** Inherited from [InlineEntityFactory\<IEntity>](InlineEntityFactory%601.md).