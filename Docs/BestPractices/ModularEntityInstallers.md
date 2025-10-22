# 📌 Modular EntityInstallers

When developing game [entities](../Entities/Entities/Manual.md), a common question arises: **how to
organize [EntityInstallers](../Entities/Installers/IEntityInstaller.md) for maintainability and scalability**? Here are
several approaches.

---

## 📑 Table of Contents

- [Method 1: Split by Comments](#ex1)
- [Method 2: Split by Methods](#ex2)
- [Method 3: Modular Installers](#ex3)
- [Summary](#summary)

---

<div id="ex1"></div>

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

<div id="ex2"></div>

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

<div id="ex3"></div>

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

Below are examples of using modular installers

### 🗂 Example #1: Character Installation

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

### 🗂 Example #2: Enemy Installation

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

---

## Summary

| Approach               | Description                               | Best For                       |
|------------------------|-------------------------------------------|--------------------------------|
| **Split by Comments**  | Organize by code regions in one installer | Small, unique entities         |
| **Split by Methods**   | Improves readability and structure        | Mid-size entities              |
| **Modular Installers** | Reusable, composable features             | Large or shared entity systems |

---

**In short:**
> Use **modular installers** to maximize code reuse and maintainability while keeping your entity setup clean and
> scalable.