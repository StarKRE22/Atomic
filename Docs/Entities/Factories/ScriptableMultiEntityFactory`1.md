# 🧩 ScriptableMultiEntityFactory<K, E, F>

```csharp
public abstract class ScriptableMultiEntityFactory<K, E, F> : ScriptableObject, IMultiEntityFactory<K, E>
    where E : IEntity
    where F : ScriptableEntityFactory<E>
```

- **Description:** A Unity ScriptableObject-based abstract multi-entity factory that allows creating and managing entities by key.
- **Type Parameters:**
    - `K` — The type of key used to identify entities.
    - `E` — The type of entity to create, which must implement [IEntity](../Entities/IEntity.md).
    - `F` — The type of scriptable entity factory, which must inherit
      from [ScriptableEntityFactory\<E>](ScriptableEntityFactory%601.md).
- **Inheritance:** `ScriptableObject`, [IMultiEntityFactory<K, E>](IMultiEntityFactory%601.md)
- **Note:** Can be used as a Flyweight pattern across the entire project, sharing a single instance of this factory for
  efficient entity creation.
- **See also:** [ScriptableMultiEntityFactory](ScriptableMultiEntityFactory.md),
  [MultiEntityFactory<K, E>](MultiEntityFactory%601.md),

---

## 🛠 Inspector Settings

| Parameter   | Description                                                             |
|-------------|-------------------------------------------------------------------------|
| `factories` | Initial map of `ScriptableEntityFactory` to be used for entity creation |

---

## 🏹 Methods

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

## 🗂 Example of Usage

Below an example of using multi entity factory for enemy entities:

```csharp
// Enum of enemy types
public enum EnemyType 
{
    Orc,
    Goblin,
    Troll
}
```

```csharp
// Class of Enemy 
public class EnemyEntity : Entity {...}
```

```csharp
// Base class of Enemy Factory 
public abstract class EnemyFactory : ScriptableEntityFactory<EnemyEntity>
{
    public EnemyType Type { get; private set; } 
    
    public sealed override IGameEntity Create()
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

//Implementations
public class OrcFactory : EnemyFactory {...}

public class GoblinFactory : EnemyFactory {...}

public class TrollFactory : EnemyFactory {...}
```

```csharp
// Multi Enemy Factory
public class EnemyMultiFactory : ScriptableMultiEntityFactory<EnemyType, EnemyEntity, EnemyFactory>
{
    protected override EnemyType GetKey(EnemyFactory factory) => factory.Type;
}
```

```csharp
//Usage:
EnemyMultiFactory factory = Resources.Load<EnemyMultiFactory>(nameof(EnemyMultiFactory));

if (factory.Contains(EnemyType.Orc))  
{  
    IEntity orc = factory.Create((EnemyType.Orc));  
}

if (factory.TryCreate(EnemyType.Goblin, out IEntity goblin))  
{  
    // use goblin
}
```

---

## 📝 Notes

- This is an abstract Unity `ScriptableObject` implementation and cannot be instantiated directly.
- Derived classes must implement `GetKey(F)` to define how keys are extracted.
- The internal dictionary of entities is initialized lazily on first access.
- Duplicate keys are overwritten with a warning in the Unity console.
- Can be used as a Flyweight across the project