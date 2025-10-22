# 📌 Override Entity Factories With Bakers

---

## Overview

When developing games in **C#** using **Unity**, it’s common to need **base entity creation via factories** while allowing game designers to **tweak entity parameters directly in the scene**.

To achieve this, two main tools are used:

- [ScriptableEntityFactory](../Entities/Factories/ScriptableEntityFactory.md) — creates new entities in memory based on a template.
- [SceneEntityBaker](../Entities/Baking/Manual.md) — allows “baking” or overriding entity parameters directly in the Unity scene.

---

## Why use this approach

Factories create entities with predefined configurations, e.g.:

- A tank with 10 health points
- A unit with base damage and movement

However, designers often need to **modify parameters directly in the scene**:

- Reduce health to `5`
- Change damage
- Set color or team
- Enable/disable specific components

**SceneEntityBakers** allow you to layer overrides on top of the factory-created entities.

---

## Overriding Factory

The **base factory** is responsible for creating entities in memory with a predefined structure.  
It separates **entity creation** from **entity configuration**, allowing Scene Bakers to later override specific parts.

```csharp
// Assume we have a specific type of IEntity
public interface IUnitEntity : IEntity
{
}
```

```csharp
// Factory that create a new Unit Entities
public abstract class UnitFactory : ScriptableEntityFactory<IUnitEntity>
{
    public string Name => this.name;

    public sealed override IUnitEntity Create()
    {
        var entity = new UnitEntity(
            this.Name,
            this.initialTagCapacity,
            this.initialValueCapacity,
            this.initialBehaviourCapacity
        );
        this.Install(entity);
        return entity;
    }
    
    protected abstract void Install(IUnitEntity entity);
}
```

To override unit configuration on a scene we can define a specific type of the baker 

```csharp

public abstract class UnitBaker : SceneEntityBaker<IUnitEntity>
{
    [SerializeField]
    private UnitFactory _factory;
    
    protected sealed override IUnitEntity Create()
    {
        IUnitEntity entity = _factory.Create();
        this.Install(entity);
        return entity;
    }

    protected abstract void Install(IUnitEntity entity);
}
```

---

## Example of Usage

Below is an example of implementation `UnitFactory` & `UnitBaker` как танк 

```csharp
[CreateAssetMenu(
    fileName = "TankFactory",
    menuName = "RTSGame/GameEntities/New TankFactory"
)]
public sealed class TankFactory : UnitFactory
{
    [SerializeField] private TransformEntityInstaller _transformInstaller;
    [SerializeField] private MoveEntityInstaller _moveInstaller;
    [SerializeField] private LifeEntityInstaller _lifeInstaller;
    [SerializeField] private RangeCombatEntityInstaller _combatInstaller;
    [SerializeField] private AIEntityInstaller _aiInstaller;

    protected override void Install(IUnitEntity entity)
    {
        entity.AddUnitTag();
        entity.AddTeam(new ReactiveVariable<TeamType>());

        entity.Install(_transformInstaller);
        entity.Install(_moveInstaller);
        entity.Install(_lifeInstaller);
        entity.Install(_combatInstaller);
        entity.Install(_aiInstaller);
    }
}

```

```csharp
public sealed class TankBaker : UnitBaker
{
    [SerializeField] private LifeEntityBaker _lifeBaker;
    [SerializeField] private MoveEntityBaker _moveBaker;
    [SerializeField] private CombatEntityBaker _combatBaker;
    [SerializeField] private TeamEntityBaker _teamBaker;
    [SerializeField] private TransformEntityBaker _transformBaker;

    protected override void Install(IUnitEntity entity)
    {
        entity.Install(_moveBaker);
        entity.Install(_lifeBaker);
        entity.Install(_combatBaker);
        entity.Install(_teamBaker);
        entity.Install(_transformBaker);
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        _teamBaker.OnValidate();
    }
}
```

Ниже приведены примеры в чем отличие Installer и Baker



```csharp
[Serializable]
public sealed class LifeEntityInstaller : IEntityInstaller<IUnitEntity>
{
    [SerializeField] private int _health;

    public void Install(IUnitEntity entity)
    {
        IGameContext gameContext = GameContext.Instance;
        entity.AddDamageableTag();
        entity.AddHealth(new Health(_health));
        entity.AddTakeDamageEvent(new BaseEvent<int>());
        entity.AddBehaviour(new LifeBehaviour(gameContext));
    }
}
```

```csharp
[Serializable]
public sealed class LifeEntityBaker : IEntityInstaller<IUnitEntity>
{
[SerializeField] private bool _active;

    [Space]
#if ODIN_INSPECTOR
    [EnableIf(nameof(_active))]
#endif
    [SerializeField] private int _current;

#if ODIN_INSPECTOR
    [EnableIf(nameof(_active))]
#endif
    [SerializeField] private int _max;

    public void Install(IUnitEntity entity)
    {
        if (_active)
        {
            entity.GetHealth().SetCurrent(_current);
            entity.GetHealth().SetMax(_max);
        }
    }
}
```

Также в Atomic.Elements есть компонент `Optional`, который позволяет переопределять по необходимости галочки в инспекторе

```csharp
[Serializable]
public sealed class MoveEntityBaker : IEntityInstaller<IUnitEntity>
{
    [SerializeField]
    private Optional<float> _moveSpeed;

    [SerializeField]
    private Optional<float> _rotationSpeed;

    public void Install(IUnitEntity entity)
    {
        if (_moveSpeed) entity.SetMoveSpeed(new Const<float>(_moveSpeed));
        if (_rotationSpeed) entity.SetRotationSpeed(new Const<float>(_rotationSpeed));
    }
}
```

---

## Summary

| Component                        | Where used | Purpose |
|----------------------------------|------------|---------|
| **ScriptableEntityFactory**      | In code / ScriptableObject | Creates entities based on a base template |
| **SceneEntityBaker**          | Unity scene | Overrides entity parameters |
| **IEntityInstaller / Bakers** | Part of Baker | Sets specific entity parameters and behaviors |
| **Scene configuration**       | Unity Inspector | Allows designers to tweak entity characteristics |

---

### Conclusion

Using **SceneEntityBakers** allows you to build a **flexible, scalable entity creation system**:

- Developers write **base factories**
- Designers can **override parameters in the scene** without touching code
- Increases **iteration speed**, **scalability**, and **ease of adjusting game objects**