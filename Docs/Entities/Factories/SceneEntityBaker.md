# 🧩️ SceneEntityBaker

The `SceneEntityBaker` is an abstract Unity component that converts **GameObjects** into **entities** using a specified `ScriptableEntityFactory<E>`.  
It supports batch baking for entire scenes, GameObjects, or all objects in the scene.

---

### What is Baking?

In the context of `SceneEntityBaker`, **Baking** is the process of taking a **GameObject** from a Unity scene and converting it into a fully configured **entity**.

Baking can be thought of as **overriding factory defaults**. The `ScriptableEntityFactory<E>` provides default properties for an entity (e.g., health, damage, stats). During baking, a `SceneEntityBaker` can:

- Customize these properties for the specific GameObject.
- Apply scene-specific values.
- Optionally destroy the GameObject after baking.

Additionally, **the Baker itself implements the `IEntityFactory<E>` interface**, meaning it can act as a factory directly, allowing code to create entities either via the assigned factory or via baking logic.

This allows you to reuse factory defaults while still customizing entities directly in the scene without manually changing the factory itself.

## Key Features

- **Generic and non-generic versions**: Supports both specific entity types (`SceneEntityBaker<E>`) and a shortcut for general `IEntity` (`SceneEntityBaker`).
- **Factory-based entity creation**: Uses `ScriptableEntityFactory<E>` to instantiate entities, ensuring consistent creation logic.
- **Baker as a factory**: Can be used as an `IEntityFactory<E>` itself, providing flexible entity creation directly from the Baker.
- **Customizable entity setup**: Subclasses implement `Install(E entity)` to configure baked entities with scene-specific data.
- **Automatic GameObject cleanup**: Optionally destroys the GameObject after baking to avoid leftover objects in the scene.
- **Batch baking support**:
    - Bake all entities in the current scene.
    - Bake all entities under a specific `GameObject`.
    - Bake all entities in a specific `Scene`.
    - Bake directly into an existing `ICollection<E>` to reuse memory or append results.
- **Active/inactive inclusion**: Can optionally include inactive GameObjects when searching for bakers.
- **Editor-friendly**: Inspector fields allow configuring destruction behavior and linking the factory directly in the Unity Editor.

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