# 🧩️ SceneEntityBaker

The `SceneEntityBaker` is an abstract Unity component that converts **GameObjects** into **entities** using a specified `ScriptableEntityFactory<E>`.  
It supports batch baking for entire scenes, GameObjects, or all objects in the scene.

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