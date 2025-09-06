# 🧩️ IEntityInstaller

`IEntityInstaller` defines a **generic mechanism for configuring or injecting data** into an `IEntity` instance.  
It allows setting up entity data, components, or behaviors during initialization or configuration phases.

---

## Key Features

- **Entity Configuration** – Install data, components, or behaviors into entities.
- **Strongly-Typed Option** – `IEntityInstaller<E>` allows type-safe configuration for specific entity types.
- **Composable** – Multiple installers can be applied to a single entity.
- **Automatic Casting** – Generic interface automatically implements the non-generic `Install` method.

---

## Interface: IEntityInstaller
```csharp
public interface IEntityInstaller
{
    void Install(IEntity entity);
}
```

---

## Interface: IEntityInstaller&lt;E&gt;
```csharp
public interface IEntityInstaller<in E> : IEntityInstaller where E : IEntity
{
    void Install(E entity);
}
```
- Implements `IEntityInstaller.Install(IEntity)` automatically by casting to `E`.
- Ensures type-safe installation logic for specific entity types.

---

## Example Usage

### Example #1. Non-Generic (IEntity)
```csharp
[Serializable]
public sealed class CharacterInstaller : IEntityInstaller
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _moveSpeed = 5.0f; 
    [SerializeField] private Vector3 _moveDirection; 

    public override void Install(IEntity entity)
    {
        //Add tags to the character
        entity.AddTag("Character");
        entity.AddTag("Moveable");

        //Add properties to the character
        entity.AddValue("Transform", _transform);
        entity.AddValue("MoveSpeed", _moveSpeed);
        entity.AddValue("MoveDirection", _moveDirection);
        
        //Add behaviours to the character
        entity.AddBehaviour<MoveBehaviour>();
        entity.AddBehaviour<LookBehaviour>();
    }
}
```

### Example #2. Generic with UnitEntity (strongly-typed)
```csharp
[Serializable]
public sealed class CharacterInstaller : IEntityInstaller<ICharacterEntity>
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _moveSpeed = 5.0f; 
    [SerializeField] private Vector3 _moveDirection; 

    public override void Install(ICharacterEntity entity)
    {
        //Add tags to the character
        entity.AddTag("Character");
        entity.AddTag("Moveable");

        //Add properties to the character
        entity.AddValue("Transform", _transform);
        entity.AddValue("MoveSpeed", _moveSpeed);
        entity.AddValue("MoveDirection", _moveDirection);
        
        //Add behaviours to the character
        entity.AddBehaviour<MoveBehaviour>();
        entity.AddBehaviour<LookBehaviour>();
    }
}
```

> Note: Using the generic `ICharacterEntity` version allows type-safe access to entity-specific properties without casting.

---

## Remarks

- `IEntityInstaller` is intended for configuring or initializing entities before or during their lifecycle.
- `IEntityInstaller<E>` is useful when the installer is specific to a particular entity type.
- Multiple installers can be combined to modularly configure entities.
- Works with both runtime and editor simulation workflows.
