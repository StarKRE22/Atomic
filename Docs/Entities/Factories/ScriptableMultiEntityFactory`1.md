# 🧩 ScriptableMultiEntityFactory<K, E, F>

A Unity ScriptableObject-based abstract multi-entity factory that allows creating and managing entities by key.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Create(K)](#createk)
        - [TryCreate(K, out E)](#trycreatek-out-e)
        - [Contains(K)](#containsk)
        - [GetKey(F)](#getkeyf)
- [Notes](#-notes)

---

## 🗂 Example of Usage

#### 1. Assume we have an enum with enemy types

```csharp
public enum EnemyType 
{
    Orc,
    Goblin
}
```

#### 2. Assume we have an enemy type derived from [Entity](../Entities/Entity.md):

```csharp
public class EnemyEntity : Entity 
{
    public EnemyEntity(
        string name = null,
        int tagCapacity = 0,
        int valueCapacity = 0,
        int behaviourCapacity = 0
    ) : base(name, tagCapacity, valueCapacity, behaviourCapacity)
    {
    }
}
```

#### 3. Assume we have a base enemy factory derived from [ScriptableEntityFactory\<E>](ScriptableEntityFactory%601.md):

```csharp
public abstract class EnemyFactoryBase : ScriptableEntityFactory<EnemyEntity>
{
    public EnemyType Type { get; private set; } 
    
    public sealed override EnemyEntity Create()
    {
        var entity = new EnemyEntity(
            this.Type.ToString(),
            this.initialTagCapacity,
            this.initialValueCapacity,
            this.initialBehaviourCapacity
        );
        this.Install(entity);
        return entity;
    }

    protected abstract void Install(EnemyEntity entity);
}
```

#### 4. Assume we have some implementations of enemy factories derived from `EnemyFactoryBase`:

```csharp
[CreateAssetMenu(
    fileName = "OrcEntityFactory",
    menuName = "Example/New OrcEntityFactory"
)]
public class OrcEntityFactory : ScriptableEntityFactory<EnemyEntity>
{
    protected override void Install(EnemyEntity entity)
    {
        // Some code...
    }
}
```

```csharp
[CreateAssetMenu(
    fileName = "GnomeEntityFactory",
    menuName = "Example/New GnomeEntityFactory"
)]
public class GnomeEntityFactory : ScriptableEntityFactory<EnemyEntity>
{
    protected override void Install(EnemyEntity entity)
    {
        // Some code...
    }
}
```

#### 5. Create a multi-entity factory for the enemies

```csharp
// Multi Enemy Factory
public class EnemyMultiFactory : ScriptableMultiEntityFactory<EnemyType, EnemyEntity, EnemyFactoryBase>
{
    protected override EnemyType GetKey(EnemyFactory factory) => factory.Type;
}
```

#### 6. Drag and drop this entity factories to the multi-factory asset

<img width="400" height="" alt="Entity component" src="../../Images/ScriptableMultiEntityFactory%20(Full).png" />


#### 7. Use the multi-entity factory in your project

```csharp
// Load the multi-entity factory asset from Resources, for example:
EnemyMultiFactory factory = Resources.Load<EnemyMultiFactory>(nameof(EnemyMultiFactory));

if (factory.Contains(EnemyType.Orc))  
    IEntity orc = factory.Create((EnemyType.Orc));  

if (factory.TryCreate(EnemyType.Goblin, out IEntity goblin))  
    // use goblin
```

---

## 🛠 Inspector Settings

| Parameter   | Description                                                                |
|-------------|----------------------------------------------------------------------------|
| `factories` | Initial map of [ScriptableEntityFactory\<E>](ScriptableEntityFactory%601.md) to be used for entity creation |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public abstract class ScriptableMultiEntityFactory<K, E, F> : ScriptableObject, IMultiEntityFactory<K, E>
    where E : IEntity
    where F : ScriptableEntityFactory<E>
```

- **Type Parameters:**
    - `K` — The type of key used to identify entities.
    - `E` — The type of entity to create, which must implement [IEntity](../Entities/IEntity.md).
    - `F` — The type of scriptable entity factory, which must inherit
      from [ScriptableEntityFactory\<E>](ScriptableEntityFactory%601.md).
- **Inheritance:** `ScriptableObject`, [IMultiEntityFactory<K, E>](IMultiEntityFactory%601.md)
- **See also:** [ScriptableMultiEntityFactory](ScriptableMultiEntityFactory.md),
  [MultiEntityFactory<K, E>](MultiEntityFactory%601.md),

---

### 🏹 Methods

#### `Create(K)`

```csharp
public E Create(K key);
```

- **Description:** Creates a new entity associated with the specified key.
- **Parameter:** `key` — The key identifying the entity to create.
- **Returns:** A new instance of type `E`.

#### `TryCreate(K, out E)`

```csharp
public bool TryCreate(K key, out E entity);
```

- **Description:** Attempts to create a new entity associated with the specified key.
- **Parameters:**
    - `key` — The key identifying the entity to create.
    - `entity` — When the method returns, contains the created entity if the key exists; otherwise, the default value of
      `E`.
- **Returns:** `true` if the entity was created successfully; otherwise, `false`.

#### `Contains(K)`

```csharp
public bool Contains(K key);
```

- **Description:** Determines whether an entity associated with the specified key exists.
- **Parameter:** `key` — The key to check.
- **Returns:** `true` if an entity with the given key exists; otherwise, `false`.

#### `GetKey(F)`

```csharp
protected abstract K GetKey(F factory);
```

- **Description:** Extracts the key for a given entity. Must be implemented by derived classes.
- **Parameter:** `factory` — The entity instance to extract the key from.
- **Returns:** The key corresponding to the given entity.

---

## 📝 Notes

- This is an abstract Unity `ScriptableObject` implementation and cannot be instantiated directly.
- Derived classes must implement `GetKey(F)` to define how keys are extracted.
- Duplicate keys are overwritten with a warning in the Unity console.
- It can be used as a Flyweight pattern across the entire project, sharing a single instance of this factory for
  efficient entity creation.