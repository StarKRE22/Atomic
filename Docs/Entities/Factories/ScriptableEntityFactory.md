# 🧩️ ScriptableEntityFactory

Abstract class for ScriptableObject-based factories that create and
configure [Entity](../Entities/Entity.md) instances.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [Inspector Settings](#-inspector-settings)
    - [Parameters](#-parameters)
    - [Context Menu](#-context-menu)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Fields](#-fields)
    - [Methods](#-methods)
        - [Create()](#create)
        - [Install(IEntity)](#installientity)
        - [OnValidate()](#onvalidate)
        - [Reset()](#reset)
- [Notes](#-notes)

---

## 🗂 Example of Usage

Below is an example of a ScriptableObject factory that creates player entities:

```csharp
[CreateAssetMenu(
    fileName = "PlayerFactory",
    menuName = "Examples/PlayerFactory"
)]
public class PlayerScriptableFactory : ScriptableEntityFactory
{
    protected override void Install(IEntity entity)
    {
        entity.AddTag("Player");
        entity.AddValue<int>("Health", 200);
        entity.AddValue<float>("MoveSpeed", 10);
        entity.AddBehaviour<MoveBehaviour>();
    }
}
```

- **Note:** This pattern allows creating a fully configured `Entity` via ScriptableObject-based workflows, combining
  predefined capacities with custom logic via `Install()`.

---

## 🛠 Inspector Settings

<div id="-parameters"></div>

### 🎛️ Parameters

| Parameter                  | Description                                           | 
|----------------------------|-------------------------------------------------------|
| `autoCompile`              | Should precompute capacities when OnValidate happens? |
| `initialTagCapacity`       | Initial number of tags to assign to the entity        |
| `initialValueCapacity`     | Initial number of values to assign to the entity      |
| `initialBehaviourCapacity` | Initial number of behaviours to assign to the entity  |

- **Note:** These parameters are primarily used for **Editor optimization** and asset baking workflows.

---

<div id="-context-menu"></div>

### ⚙️ Context Menu

| Option       | Description                                                                                                                                                                                                                                                                   | 
|--------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Precompile` | Creates a temporary entity using [Create()](#create) and **precompiles capacities** such as tag count, value count, and behavior count. Useful for editor previews, asset baking, and optimization. Only executed in the Editor. Logs a warning if `Create()` returns `null`. |
| `Reset`      | Resets factory fields to default values.                                                                                                                                                                                                                                      |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public abstract class ScriptableEntityFactory : ScriptableEntityFactory<IEntity>, IEntityFactory
```

- **Inheritance:** [ScriptableEntityFactory\<E>](ScriptableEntityFactory%601.md),
  [IEntityFactory](IEntityFactory.md)

---

### 🧱 Fields

#### `InitialTagCapacity`

```csharp
[SerializeField]
protected int initialTagCount;
```

- **Description:** Initial number of tags to assign to the entity. Mainly used for **editor optimization** and asset
  baking.

#### `InitialValueCapacity`

```csharp
[SerializeField]
protected int initialValueCount;
```

- **Description:** Initial number of values to assign to the entity.

#### `InitialBehaviourCapacity`

```csharp
[SerializeField]
protected int initialBehaviourCount;
```

- **Description:** Initial number of behaviours to assign to the entity.

---

### 🏹 Methods

#### `Create()`

```csharp
public sealed override IEntity Create();
```

- **Description:** Creates a new [Entity](../Entities/Entity.md) using predefined initialization values and then applies
  custom logic via the `Install` method.
- **Returns:** A new instance of [IEntity](../Entities/IEntity.md).
- **Note:** This method is `sealed`; override `Install(IEntity)` for custom configuration.

#### `Install(IEntity)`

```csharp
protected abstract void Install(IEntity entity);
```

- **Description:** Called after entity creation to add tags, values, or behaviours.
- **Parameter:** `entity` — The [IEntity](../Entities/IEntity.md) instance to configure.
- **Note:** Must be implemented by derived classes to provide custom setup logic.

#### `OnValidate()`

```csharp
protected virtual void OnValidate();
```

- **Description:** ScriptableObject callback invoked when values change in the Inspector. Updates cached metadata by
  calling `Precompile()` by default.
- **Remarks:** Only executed in the Editor outside of Play mode.

#### `Reset()`

```csharp
protected virtual void Reset();
```

- **Description:** Resets factory fields to default values.
- **Remarks:** Only affects editor workflows.

---

## 📝 Notes

- Provides the `Install(IEntity)` method to inject custom configuration logic after entity creation.
- Can be reused across multiple objects without heavy dependencies.