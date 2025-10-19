# 🧩 SceneEntityFactory

Abstract base class for Unity-based factories that create and configure [IEntity](../Entities/IEntity.md) instances.
Designed for scene-based workflows where entities need to be created at runtime from serialized MonoBehaviours.
Extends [SceneEntityFactory\<E>](SceneEntityFactory%601.md) and adds `Install()` for custom entity setup.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [Inspector Settings](#-inspector-settings)
    - [Parameters](#-parameters)
    - [Context Menu](#-context-menu)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Fields](#-fields)
        - [initialTagCapacity](#initialtagcapacity)
        - [initialValueCapacity](#initialvaluecapacity)
        - [initialBehaviourCapacity](#initialbehaviourcapacity)
    - [Methods](#-methods)
        - [Create()](#create)
        - [Install()](#install)
        - [OnValidate()](#onvalidate)
        - [Reset()](#reset)

---

## 🗂 Example of Usage

Below is an example of a MonoBehaviour factory that creates and configures an [IEntity](../Entities/IEntity.md):

```csharp
public class EnemySceneFactory : SceneEntityFactory
{
    protected override void Install(IEntity entity)
    {
        entity.AddTag("Enemy");
        entity.AddValue<int>("Health", 100);
        entity.AddValue<int>("Damage", 15);
        entity.AddBehaviour<AttackBehaviour>();
    }
}
```

- **Usage:** When `Create()` is called, a new `Entity` is instantiated with the factory's initial capacities, then
  `Install()` applies custom configuration.

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

- **Note:** These parameters are primarily used for **Editor optimization** and runtime initialization.

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
public abstract class SceneEntityFactory : SceneEntityFactory<IEntity>, IEntityFactory
```

- **Inheritance:** `SceneEntityFactory<IEntity>`, `IEntityFactory`
- **Notes:** Provides a non-generic base for factories that produce `Entity` objects. Handles creation and delegates
  custom configuration to `Install()`.

---

### 🧱 Fields

#### `initialTagCapacity`

```csharp
[SerializeField]
protected int initialTagCapacity;
```

- **Description:** Initial number of tags to assign to the entity. Used for editor previews and optimization.

#### `initialValueCapacity`

```csharp
[SerializeField]
protected int initialValueCapacity;
```

- **Description:** Initial number of values to assign to the entity.

#### `initialBehaviourCapacity`

```csharp
[SerializeField]
protected int initialBehaviourCapacity;
```

- **Description:** Initial number of behaviours to assign to the entity.

---

### 🏹 Methods

#### `Create()`

```csharp
public sealed override IEntity Create();
```

- **Description:** Creates a new `Entity` using the factory's initial capacities and applies custom configuration via
  `Install()`.
- **Returns:** A new instance of `IEntity`.
- **Remarks:** Cannot be overridden; custom logic should be implemented in `Install()`.

#### `Install()`

```csharp
protected abstract void Install(IEntity entity);
```

- **Description:** Applies custom configuration to the newly created `Entity`, such as adding tags, values, or
  behaviours.
- **Remarks:** Must be implemented by derived classes to customize entity setup after creation.

#### `OnValidate()`

```csharp
protected virtual void OnValidate();
```

- **Description:** Unity callback invoked when script values change in the Inspector. Calls `Precompile()` automatically
  if `autoCompile` is enabled.
- **Remarks:** Only executed in the Editor outside of Play mode.

#### `Reset()`

```csharp
protected virtual void Reset();
```

- **Description:** Resets factory fields to default values.
- **Remarks:** Only affects editor workflows.