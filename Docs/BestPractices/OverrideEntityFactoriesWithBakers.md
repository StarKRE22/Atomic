# 📌 Overriding EntityFactories with EntityBakers

---

## 📑 Table of Contents

- [Overview](#overview)
- [Why Use This Approach](#why-use-this-approach)
- [Overriding the Factory](#overriding-the-factory)
- [Example of Usage](#example-of-usage)
- [Installer vs Baker](#installer-vs-baker)
- [Summary](#summary)
    - [Conclusion](#conclusion)

---

## Overview

When developing games in **C#** using **Unity**, it’s common to need **base entity creation via factories** while
allowing **designers to tweak entity parameters directly in the scene**.

Two main tools are used for this:

- [ScriptableEntityFactory](../Entities/Factories/ScriptableEntityFactory.md) — creates new entities in memory based on
  a template.
- [SceneEntityBaker](../Entities/Baking/Manual.md) — allows “baking” or overriding entity parameters directly in the
  Unity scene.

---

## Why Use This Approach

Factories create entities with predefined configurations, for example:

- A tank with 10 health points
- A unit with base damage and movement

However, designers often need to **modify parameters directly in the scene**:

- Reduce health to `5`
- Change damage
- Set color or team
- Enable/disable specific components

**SceneEntityBakers** allow you to layer overrides on top of factory-created entities.

---

## Overriding the Factory

The **base factory** is responsible for creating entities in memory with a predefined structure.  
It separates **entity creation** from **entity configuration**, allowing Scene Bakers to later override specific parts.

```csharp
// Interface for a specific type of IEntity
public interface IUnitEntity : IEntity
{
}
```

```csharp
// Factory for creating new Unit Entities
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

To override unit configuration in a scene, a specific type of Baker can be defined:

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

Example implementation of `UnitFactory` and `UnitBaker` for a tank:

```csharp
[CreateAssetMenu(
    fileName = "TankFactory",
    menuName = "Example/New TankFactory"
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

---

## Installer vs Baker

The difference between Installer and Baker is shown below.

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
    [SerializeField] 
    private bool _active;

    [Space]
    [SerializeField] 
    private int _current;

    [SerializeField] 
    private int _max;

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

In **Atomic.Elements**, there’s also an [Optional](../Elements/Utils/Optional.md) component, which allows overriding
values directly in the inspector:

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

| Component                   | Where used              | Purpose                                       |
|-----------------------------|-------------------------|-----------------------------------------------|
| **ScriptableEntityFactory** | Code / ScriptableObject | Creates entities based on a base template     |
| **SceneEntityBaker**        | Unity scene             | Overrides entity parameters                   |
| **EntityInstaller**         | Part of Baker           | Sets specific entity parameters and behaviors |

---

### Conclusion

Using **SceneEntityBakers** allows you to build a **flexible, scalable entity creation system**:

- Developers write **base factories**
- Designers can **override parameters in the scene** without touching code
- Increases **iteration speed**, **scalability**, and **ease of adjusting game objects**