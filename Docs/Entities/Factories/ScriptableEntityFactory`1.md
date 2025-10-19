# 🧩️ ScriptableEntityFactory\<E>

Abstract class for ScriptableObject-based factories that create entities with customizable initial settings.
Can be reused across multiple objects without heavy dependencies.

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
        - [OnValidate()](#onvalidate)
        - [Reset()](#reset)

---

## 🗂 Example of Usage

Below is an example of a ScriptableObject factory that creates enemy entities of an `EnemyEntity` type:

```csharp
[CreateAssetMenu(
    fileName = "EnemyFactory",
    menuName = "Examples/EnemyFactory"
)]
public class EnemyScriptableFactory : ScriptableEntityFactory<EnemyEntity>
{
    public override EnemyEntity Create()
    {
        //Create an instance of entity with precomputed capacities
        var enemy = new EnemyEntity(
            this.name,
            this.initialTagCount,
            this.initialValueCount,
            this.initialBehaviourCount
        );
    
        enemy.AddTag("Enemy");
        enemy.AddValue<int>("Health", 100);
        enemy.AddValue<int>("Damage", 15);
        enemy.AddBehaviour<AttackBehaviour>();

        return enemy;
    }
}
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
public abstract class ScriptableEntityFactory<E> : ScriptableObject, IEntityFactory<E> where E : IEntity
```

- **Type Parameter:** `E` — The type of entity to create. Must implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** `ScriptableObject`, [IEntityFactory\<E>](IEntityFactory%601.md)
- **Notes:** Stores initial tag, value, and behaviour capacities for optimization.
- **See also:** [ScriptableEntityFactory](ScriptableEntityFactory.md)

---

### 🧱 Fields

#### `initialTagCapacity`

```csharp
[SerializeField]
protected int initialTagCount;
```

- **Description:** Initial number of tags to assign to the entity. Mainly used for **editor optimization** and asset
  baking.

#### `initialValueCapacity`

```csharp
[SerializeField]
protected int initialValueCount;
```

- **Description:** Initial number of values to assign to the entity.

#### `initialBehaviourCapacity`

```csharp
[SerializeField]
protected int initialBehaviourCount;
```

- **Description:** Initial number of behaviours to assign to the entity.

---

### 🏹 Methods

#### `Create()`

```csharp
public abstract E Create();
```

- **Description:** Creates and returns a new instance of the entity type `E`.
- **Returns:** A new instance of type `E`.
- **Note:** Override this method to implement custom instantiation logic.

#### `OnValidate()`

```csharp
protected virtual void OnValidate();
```

- **Description:** ScriptableObject callback invoked when script values change in the Inspector. Updates cached metadata
  by calling `Precompile()` by default.
- **Remarks:** Only executed in the Editor outside of Play mode.

#### `Reset()`

```csharp
protected virtual void Reset();
```

- **Description:** Resets factory fields to default values.
- **Remarks:** Only affects editor workflows.