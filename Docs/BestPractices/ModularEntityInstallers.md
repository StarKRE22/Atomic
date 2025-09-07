# 📌 Modular Entity Installers

When developing game entities, a common question arises: **how to organize [EntityInstallers](../Entities/Installers/IEntityInstaller.md) for maintainability and scalability**? Here are several approaches.

---

## 🧩 Method 1: Split by Comments

**This approach organizes installation by logical sections within a single installer.**
```csharp
public sealed class CharacterInstaller : SceneEntityInstaller<IGameEntity>
{
    [SerializeField] private Const<float> _moveSpeed = 3;
    [SerializeField] private Const<float> _rotationSpeed = 360 * 4;
    [SerializeField] private Const<TeamType> _teamType = TeamType.Blue;
    [SerializeField] private TriggerEvents _triggerEvents;

    public override void Install(IGameEntity entity)
    {
        // Base
        entity.AddCharacterTag();
        entity.AddTransform(this.transform);
        entity.AddTeamType(_teamType);
        entity.AddTriggerEvents(_triggerEvents);

        // Movement
        entity.AddMoveSpeed(_moveSpeed);
        entity.AddMoveDirection(new BaseVariable<Vector3>());
        entity.AddBehaviour<MovementBehaviour>();

        // Rotation
        entity.AddRotationSpeed(_rotationSpeed);
        entity.AddRotationDirection(new BaseVariable<Vector3>());
        entity.AddBehaviour<RotationBehaviour>();
    }
}
```

> This works well when entities have **unique, non-overlapping mechanics**.

---

## 🧩 Method 2: Split by Methods

**You can split installation into multiple methods for clarity:**
```csharp
public sealed class CharacterInstaller : SceneEntityInstaller<IGameEntity>
{
    [SerializeField] private Const<float> _moveSpeed = 3;
    [SerializeField] private Const<float> _rotationSpeed = 360 * 4;
    [SerializeField] private Const<TeamType> _teamType = TeamType.Blue;
    [SerializeField] private TriggerEvents _triggerEvents;

    public override void Install(IGameEntity entity)
    {
        this.InstallBaseFeature(entity);
        this.InstallMovementFeature(entity);
        this.InstallRotationFeature(entity);
    }
    
    private void InstallBaseFeature(IGameEntity entity)
    {
        entity.AddCharacterTag();
        entity.AddTransform(this.transform);
        entity.AddTeamType(_teamType);
        entity.AddTriggerEvents(_triggerEvents);
    }
    
    private void InstallMovementFeature(IGameEntity entity)
    {
        entity.AddMoveSpeed(_moveSpeed);
        entity.AddMoveDirection(new BaseVariable<Vector3>());
        entity.AddBehaviour<MovementBehaviour>();
    }
    
    private void InstallRotationFeature(IGameEntity entity)
    {
        entity.AddRotationSpeed(_rotationSpeed);
        entity.AddRotationDirection(new BaseVariable<Vector3>());
        entity.AddBehaviour<RotationBehaviour>();
    }
}
```

> This approach improves readability but can be slightly less performant due to additional method calls.

---

## 🧩 Method 3: Modular Installers

**For entities that share mechanics (e.g., characters and enemies), create **modular installers**:**

```csharp
[Serializable]
public class TransformEntityInstaller : IEntityInstaller<IGameEntity>
{
    [SerializeField] private Transform _transform;
    
    public void Install(IGameEntity entity)
    {
        entity.AddTransform(_transform);
    }
}
```

```csharp
[Serializable]
public class TeamEntityInstaller : IEntityInstaller<IGameEntity>
{
    [SerializeField] private Const<TeamType> _teamType = TeamType.Blue;

    public void Install(IGameEntity entity)
    {
        entity.AddTeamType(_teamType);
    }
}
```
```csharp
[Serializable]
public class MovementEntityInstaller : IEntityInstaller<IGameEntity>
{
    [SerializeField] private Const<float> _moveSpeed = 3;

    public void Install(IGameEntity entity)
    {
        entity.AddMoveSpeed(_moveSpeed);
        entity.AddMoveDirection(new BaseVariable<Vector3>());
        entity.AddBehaviour<MovementBehaviour>();
    }
}
```

```csharp
[Serializable]
public class RotationEntityInstaller : IEntityInstaller<IGameEntity>
{
    [SerializeField] private Const<float> _rotationSpeed = 360 * 4;

    public void Install(IGameEntity entity)
    {
        entity.AddRotationSpeed(_rotationSpeed);
        entity.AddRotationDirection(new BaseVariable<Vector3>());
        entity.AddBehaviour<RotationBehaviour>();
    }
}
```
  

### Example: Character Installation

```csharp
public sealed class CharacterInstaller : SceneEntityInstaller<IGameEntity>
{
    [SerializeField] private TransformEntityInstaller _transformInstaller;
    [SerializeField] private MovementEntityInstaller _movementInstaller;
    [SerializeField] private RotationEntityInstaller _rotationInstaller;
    [SerializeField] private TeamEntityInstaller _teamInstaller;
    [SerializeField] private TriggerEvents _triggerEvents;

    public override void Install(IGameEntity entity)
    {
        entity.AddCharacterTag();
        entity.AddTriggerEvents(_triggerEvents);

        entity.Install(_transformInstaller);
        entity.Install(_movementInstaller);
        entity.Install(_rotationInstaller);
        entity.Install(_teamInstaller);
    }
}
```

### Example: Enemy Installation

```csharp
public sealed class EnemyInstaller : SceneEntityInstaller<IGameEntity>
{
    [SerializeField] private TransformEntityInstaller _transformInstaller;
    [SerializeField] private MovementEntityInstaller _movementInstaller;
    [SerializeField] private RotationEntityInstaller _rotationInstaller;
    [SerializeField] private TeamEntityInstaller _teamInstaller;
    [SerializeField] private TriggerEvents _triggerEvents;

    public override void Install(IGameEntity entity)
    {
        entity.AddOrkTag();
        entity.AddTriggerEvents(_triggerEvents);
        
        entity.Install(_transformInstaller);
        entity.Install(_movementInstaller);
        entity.Install(_rotationInstaller);
        entity.Install(_teamInstaller);
    }
}
```

> Modular installers allow **reuse of mechanics** across many entities while keeping code clean and scalable.
