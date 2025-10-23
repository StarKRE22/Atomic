# 🧩 SceneEntityBaker

Abstract base class for Unity scene bakers that convert GameObjects into native [IEntity](../Entities/IEntity.md)
instances. Provides a workflow to convert authored GameObjects into runtime entity representations.

> Derived classes must implement [Install(IEntity)](#installientity) to define entity configuration logic such as adding
> tags, values, or behaviours.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
    - [Baker Implementation](#ex1)
    - [Bake All](#ex2)
- [Inspector Settings](#-inspector-settings)
    - [Parameters](#-parameters)
    - [Context Menu](#-context-menu)
    - [Gizmos](#-gizmos)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Fields](#-fields)
        - [initialTagCapacity](#initialtagcapacity)
        - [initialValueCapacity](#initialvaluecapacity)
        - [initialBehaviourCapacity](#initialbehaviourcapacity)
    - [Methods](#-methods)
        - [Bake()](#bake)
        - [Create()](#create)
        - [Release()](#release)
        - [Install(IEntity)](#installientity)
        - [OnValidate()](#onvalidate)
        - [Reset()](#reset)

---

## 🗂 Example of Usage

<div id="ex1"></div>

### 1️⃣ Baker Implementation

```csharp
public class EnemyBaker : SceneEntityBaker
{
    protected override void Install(IEntity entity)
    {
        entity.AddTag("Enemy");
        entity.AddValue<int>("Health", 100);
        entity.AddValue<float>("Speed", 5f);
        entity.AddBehaviour<PatrolBehaviour>();
    }
}
```

Then at runtime:

```csharp
IEntity bakedEnemy = GetComponent<EnemyBaker>().Bake();
```

<div id="ex2"></div>

### 2️⃣ Bake All

```csharp
// Bake all IEntity bakers in all active scenes
IEntity[] enemies = EnemyBaker.BakeAll();

EntityWorld world = new EntityWorld();
world.AddRange(enemies);
```

---

## 🛠 Inspector Settings

### 🎛️ Parameters

| Parameter                  | Description                                                               |
|----------------------------|---------------------------------------------------------------------------|
| `autoCompile`              | Automatically precomputes capacities when values change in the Inspector. |
| `initialTagCapacity`       | Initial number of tags to allocate for the entity.                        |
| `initialValueCapacity`     | Initial number of values to allocate for the entity.                      |
| `initialBehaviourCapacity` | Initial number of behaviours to allocate for the entity.                  |

### ⚙️ Context Menu

| Option     | Description                                                  |
|------------|--------------------------------------------------------------|
| Precompile | Creates a temporary preview entity to precompute capacities. |
| Reset      | Resets all optimization parameters to default values.        |

---

### 🎨 Gizmos

| Setting              | Description                                       |
|----------------------|---------------------------------------------------|
| `onlySelectedGizmos` | Draw gizmos only when the GameObject is selected. |
| `onlyEditModeGizmos` | Disable gizmo drawing during Play mode.           |

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public abstract class SceneEntityBaker : SceneEntityBaker<IEntity>
```

- **Inheritance:** [SceneEntityBaker<E>](SceneEntityBaker%601.md)
- **Description:** Non-generic base class for scene bakers producing [IEntity](../Entities/IEntity.md).

---

### 🧱 Fields

#### `initialTagCapacity`

```csharp
[SerializeField]
protected int initialTagCount;
```

- **Description:** Initial number of tags for the entity. Used for editor optimization.

#### `initialValueCapacity`

```csharp
[SerializeField]
protected int initialValueCount;
```

- **Description:** Initial number of values for the entity.

#### `initialBehaviourCapacity`

```csharp
[SerializeField]
protected int initialBehaviourCount;
```

- **Description:** Initial number of behaviours for the entity.

---

### 🏹 Methods

#### `Bake()`

```csharp
public IEntity Bake();
```

- **Description:** Creates a new entity by calling [Create()](#create) and destroys the associated GameObject.
- **Returns:** New instance of type `IEntity`.

#### `Create()`

```csharp
protected sealed override IEntity Create();
```

- **Description:** Creates a new [IEntity](../Entities/IEntity.md) and applies custom logic
  via [Install(IEntity)](#installientity).
- **Remarks:** Sealed; cannot be overridden in derived classes.

#### `Release()`

```csharp
protected virtual void Release()
```
- **Description:** Handles cleanup after the entity has been created.
- **Remarks:**
  The default implementation destroys the GameObject this baker is attached to.
  Override this method if you need to preserve the GameObject
  or perform additional teardown logic.

#### `Install(IEntity)`

```csharp
protected abstract void Install(IEntity entity);
```

- **Description:** Applies custom configuration to the newly created entity (tags, values, behaviours).
- **Parameter:** `entity` — The entity to configure.

#### `OnValidate()`

```csharp
protected virtual void OnValidate();
```

- **Description:** Called when the component is loaded or modified in the Inspector. Triggers precompilation if enabled.

#### `Reset()`

```csharp
protected virtual void Reset();
```

- **Description:** Resets optimization parameters to default values.

<!--



# 🧩️ SceneEntityBaker

Represents an abstract Unity component that converts **GameObject** into [IEntity](../Entities/IEntity.md) instance. It
supports batch baking for entire scenes, GameObjects, or all objects in the scene.

---

## 🗂 Example of Usage

#### 1. Create a baker for a character entity

```csharp
public class CharacterBaker : SceneEntityBaker
{
    protected override void Install(IEntity entity)
    {
        entity.AddTag("Character");
        entity.AddValue<int>("Health", 200);
        entity.AddValue<int>("Damage", 10);
    }
}
```

#### 2. Attach this script to a GameObject

<img width="400" height="" alt="Entity component" src="../../Images/SceneEntityBaker.png" />

#### 3. Usage in a project

```csharp
// Create all entities associated with SceneEntityBaker including character
IEntity[] entities = SceneEntityBaker.BakeAll();

// Assume we have the entity world
EntityWorld world = new EntityWorld();

// Add enemies to entity world
world.AddRange(enemies);
```

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
public abstract class SceneEntityBaker : SceneEntityBaker<IEntity>, IEntityFactory
```

- **Inheritance:** [SceneEntityBaker\<E>](SceneEntityBaker%601.md), [IEntityFactory](IEntityFactory.md)
- **Notes:** Provides the `Install(IEntity)` method to inject custom configuration logic after entity creation.

---

### 🧱 Fields

#### `InitialTagCapacity`

```csharp
[SerializeField] 
protected int initialTagCount;
```

- **Description:** Initial number of tags to assign to the entity. Mainly used for **editor optimization** and scene
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

- **Description:** Unity callback invoked when script values change in the Inspector. Updates cached metadata by calling
  `Precompile()` by default.
- **Remarks:** Only executed in the Editor outside of Play mode.

#### `Reset()`

```csharp
protected virtual void Reset();
```

- **Description:** Unity callback that resets factory fields to default values.
- **Remarks:** Only affects editor workflows.


# 🧩️ SceneEntityBaker


---

---

## Classes

### Class SceneEntityBaker&lt;E&gt;

```csharp
public abstract class SceneEntityBaker<E> : MonoBehaviour where E : IEntity {...}
```

### Class SceneEntityBaker
A shortcut version bound to the base `IEntity` type.

```csharp
public abstract class SceneEntityBaker : SceneEntityBaker<IEntity> {...}
```

---

### Inspector Settings

| Field              | Type                         | Description                                                         |
|--------------------|------------------------------|---------------------------------------------------------------------|
| `destroyAfterBake` | `bool` (serialized)          | Whether to destroy the GameObject after baking. Defaults to `true`. |
| `factory`          | `ScriptableEntityFactory<E>` | The entity factory used to create entities.                         |

---

### Methods

| Method                                      | Description                                                                                                                          |
|---------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------|
| `E Bake()`                                  | Creates an entity using the assigned factory, installs it via `Install(E entity)`, and optionally destroys the GameObject.           |
| `protected abstract void Install(E entity)` | Must be implemented by subclasses to configure the baked entity with scene-specific or overridden properties.                        |
| `E IEntityFactory<E>.Create()`              | Implements the `IEntityFactory<E>` interface. Calls `Bake()` to produce a new entity, allowing the Baker itself to act as a factory. |

---
## Static Methods

| Method                                                                                         | Description                                                                                     |
|------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------|
| `static E[] BakeAll(bool includeInactive = true)`                                              | Finds and bakes **all bakers** in the scene. Returns an array of baked entities.                |
| `static void BakeAll(ICollection<E> destination, bool includeInactive)`                        | Finds and bakes **all bakers** in the scene, appending results to the provided collection.      |
| `static List<E> Bake(Scene scene, bool includeInactive = true)`                                | Bakes all bakers only in the specified scene. Returns a list of baked entities.                 |
| `static void Bake(Scene scene, ICollection<E> results, bool includeInactive = true)`           | Bakes all bakers in the specified scene, adding results to the provided collection.             |
| `static E[] Bake(GameObject gameObject, bool includeInactive = true)`                          | Bakes all bakers attached to or under the given GameObject. Returns an array of baked entities. |
| `static void Bake(GameObject gameObject, ICollection<E> results, bool includeInactive = true)` | Bakes all bakers under the given GameObject, adding results to the provided collection.         |

## Usage Example

Suppose you have a `UnitEntity` and want to bake it from a prefab or scene object:

```csharp
public sealed class UnitEntity : Entity
{
}
```

Create a factory asset for it:
```csharp
[CreateAssetMenu(menuName = "Factories/Unit Factory")]
public sealed class UnitFactory : ScriptableEntityFactory<UnitEntity>
{
    [SerializeField] private int _health = 100;
    [SerializeField] private int _damage = 25;
    
    public override UnitEntity Create() => new UnitEntity(
        this.name,
        new string[] { "Unit" }, 
        new Dictionary<string, object>
        {
            {"Health", _health},
            {"Damage", _damage}
        });
    } 
}
```

Now implement a baker:
```csharp
public class UnitBaker : SceneEntityBaker<UnitEntity>
{
    [SerializeField] private Optional<int> _health = 100;
    [SerializeField] private Optional<int> _damage = 25;

    protected override void Install(UnitEntity entity)
    {
        //Override params in the scene
        if (_health) entity.SetHealth("Health", _health);
        if (_damage) entity.SetHealth("Health", _damage);
    }
}
```

### Baking in code

Bake everything in the current scene:
```csharp
UnitEntity[] units = SceneEntityBaker<UnitEntity>.BakeAll();
```

Bake only under a given parent GameObject:
```csharp
UnitEntity[] squad = SceneEntityBaker<UnitEntity>.Bake(mySquadGameObject);
```

Bake into an existing collection:
```csharp
List<UnitEntity> buffer = new List<UnitEntity>();
SceneEntityBaker<UnitEntity>.BakeAll(buffer);
```

-->