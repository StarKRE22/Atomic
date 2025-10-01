# 🧩 Behaviours

**Behaviour** is a modular unit of logic that can be attached to an [IEntity](../Entities/IEntity.md).  
It allows entities to dynamically compose functionality at runtime, following the **Entity-State-Behaviour** pattern.

Each behaviour can handle different events of the entity:

| Event        | Purpose |
|--------------|---------|
| `Init`       | Initialization of the behaviour when the entity is created |
| `Enable`     | Activating the entity on the scene or in a pool |
| `Disable`    | Deactivating the entity and returning it to the pool |
| `Tick`       | Updates every frame (logic, state) |
| `FixedTick`  | Physics and game mechanics updates with a fixed timestep |
| `LateTick`   | Updates after rendering (e.g., UI) |
| `Dispose`    | Releasing entity resources when it is destroyed |

Each phase has a separate interface that handles the corresponding lifecycle stage of the entity.

---

### Behaviour Interfaces

For each event, there is a dedicated interface that represents that phase:

- [IEntityBehaviour](IEntityBehaviour.md) — base behaviour interface.
- [IEntityInit](IEntityInit.md) — handles initialization.
- [IEntityDispose](IEntityDispose.md) — handles resource cleanup.
- [IEntityEnable](IEntityEnable.md) — handles activation.
- [IEntityDisable](IEntityDisable.md) — handles deactivation.
- [IEntityTick](IEntityTick.md) — handles per-frame updates.
- [IEntityFixedTick](IEntityFixedTick.md) — handles fixed-timestep updates (physics, mechanics).
- [IEntityLateTick](IEntityLateTick.md) — handles post-render updates.
- [IEntityGizmos](IEntityGizmos.md) — handles debug visualization and gizmos in the scene.

> This approach makes entities flexible and extensible, allowing new behaviours to be added easily without modifying the core code.
