# 🧩️ ScriptableEntityInstaller

`ScriptableEntityInstaller` is a Unity `ScriptableObject` that defines **reusable logic for installing or configuring an `IEntity`**.  
It is useful for shared configuration logic that can be applied to multiple entities, such as setting default values, tags, or attaching behaviors.  
Supports both runtime and edit-time contexts via utility methods.

---

## Key Features

- **Flyweight Pattern** – Define shared entity setup logic once and apply it to multiple entities.
- **Strongly-Typed Option** – `ScriptableEntityInstaller<E>` allows type-safe configuration for specific entity types.
- **Runtime & Edit-Time Support** – Can be used in both runtime and editor contexts.
- **Modular** – Can be combined with other installers or entity behaviors.

---

## Class: ScriptableEntityInstaller
```csharp
public abstract class ScriptableEntityInstaller : ScriptableObject, IEntityInstaller
{
    public abstract void Install(IEntity entity);
}
```
- Implements `IEntityInstaller` to allow configuration of entities via ScriptableObjects.
- Provides a reusable, declarative way to define entity setup logic.

---

## Class: ScriptableEntityInstaller&lt;E&gt;
```csharp
public abstract class ScriptableEntityInstaller<E> : ScriptableEntityInstaller where E : class, IEntity
{
    protected abstract void Install(E entity);
}
```
- Provides a **strongly-typed variant** for specific entity types.
- Eliminates the need for manual casting in derived installer classes.

---

## Example Usage

### Example #1. Non-Generic (IEntity)
```csharp
[CreateAssetMenu(fileName = "CharacterInstaller", menuName = "Game/Entities/CharacterInstaller")]
public sealed class CharacterInstaller : ScriptableEntityInstaller
{
    [SerializeField] private float _moveSpeed = 5f;

    public override void Install(IEntity entity)
    {
        entity.AddTag("Character");
        entity.AddValue("MoveSpeed", _moveSpeed);
        entity.AddBehaviour<MoveBehaviour>();
    }
}
```

### Example #2. Generic with UnitEntity (strongly-typed)
```csharp
[CreateAssetMenu(fileName = "CharacterInstaller", menuName = "Game/Entities/CharacterInstaller")]
public sealed class CharacterInstaller : ScriptableEntityInstaller<UnitEntity>
{
    [SerializeField] private float _moveSpeed = 5f;

    protected override void Install(UnitEntity entity)
    {
        entity.AddTag("Character");
        entity.AddValue("MoveSpeed", _moveSpeed);
        entity.AddBehaviour<MoveBehaviour>();
    }
}
```

> Note: Using the generic `UnitEntity` version allows type-safe access to entity-specific properties without casting.

---

## Remarks

- `ScriptableEntityInstaller` is intended for **shared and reusable entity configuration**.
- `ScriptableEntityInstaller<E>` is useful when the installer targets a specific entity type.
- Can be used in both runtime and editor workflows.
- Supports modular and composable entity setup logic for complex systems.