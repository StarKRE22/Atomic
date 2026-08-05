# 🧩 Entity Systems

**Entity Systems** process collections of entities every frame. They are useful for updating logic such as movement,
AI, animation, or physics across many entities efficiently. Systems support adaptive batching to stay within a frame
budget, and priority-based systems can focus updates on the most important entities.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
  - [Round-Robin System](#round-robin-system)
  - [Priority System](#priority-system)
  - [Wiring to a Context](#wiring-to-a-context)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

### Round-Robin System

`EntitySystem<E>` updates entities in a round-robin fashion, spreading work across frames.

```csharp
public sealed class MovementSystem : EntitySystem<IGameEntity>
{
    public MovementSystem(IReadOnlyEntityCollection<IGameEntity> source, Settings settings)
        : base(source, settings)
    {
    }

    protected override void Update(IGameEntity entity, float deltaTime)
    {
        // movement logic
        MoveUseCase.MoveTowardsDirection(entity, deltaTime);
    }
}
```

### Priority System

`PriorityEntitySystem<E>` updates high-priority entities more often. Priority is evaluated periodically.

```csharp
public sealed class AIPrioritySystem : PriorityEntitySystem<IGameEntity>
{
    public AIPrioritySystem(
        IReadOnlyEntityCollection<IGameEntity> source,
        Settings settings,
        params IEntityTrigger<IGameEntity>[] triggers)
        : base(source, settings, triggers)
    {
    }

    protected override EntityUpdatePriority EvaluatePriority(IGameEntity entity)
    {
        return CameraUseCase.GetDistanceToMainCamera(entity) < 20f
            ? EntityUpdatePriority.High
            : EntityUpdatePriority.Low;
    }

    protected override void Update(IGameEntity entity, float deltaTime)
    {
        AIUseCase.UpdateAIBehaviour(entity, deltaTime);
    }
}
```

### Wiring to a Context

Use the system extension methods to attach systems to a context entity's lifecycle:

```csharp
IEntityWorld<IGameEntity> world = ...;

var settings = new MovementSystem.Settings
{
    frameBudget = 0.016f,
    batching = { minSize = 64, maxSize = 512 }
};

MovementSystem movement = new MovementSystem(world, settings);

IEntity gameContext = ...;
gameContext.AddTickSystem(movement); // Update in gameContext.Tick()
gameContext.AddFixedTickSystem(movement); // Update in gameContext.FixedTick()
gameContext.AddLateTickSystem(movement); // Update in gameContext.LateTick()
```

---

## 🔍 API Reference

- [EntitySystemBase<E>](EntitySystemBase.md) — abstract base class with shared system infrastructure
- [EntitySystem<E>](EntitySystem.md) — round-robin system; updates all entities evenly
- [PriorityEntitySystem<E>](PriorityEntitySystem.md) — priority-based system; updates high-priority entities more often
- [EntityUpdatePriority](EntityUpdatePriority.md) — enum defining `Low`, `Medium`, `High` priorities
- [Systems Extensions](Extensions.md) — extension methods for wiring systems to entity lifecycle callbacks

---

## 📌 Best Practices

- Keep per-entity update logic fast to allow adaptive batching to scale.
- Use `PriorityEntitySystem<E>` when entity importance varies (e.g., distance to camera or player).
- Dispose systems when the owning context is destroyed.
- Tune `frameBudget` and `batching` settings per platform.
- Use triggers with `PriorityEntitySystem<E>` to avoid recalculating priorities every frame.
