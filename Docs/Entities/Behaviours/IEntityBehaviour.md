# 🧩️ IEntityBehaviour

Represents a base contract of logic that can be attached to an [IEntity](../Entities/IEntity.md). Don't implement this
interface. It is only used for framework behaviour contracts.

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public interface IEntityBehaviour
``` 

---

## 🔗 Useful Links

- [IEntityInit](IEntityInit.md) — handles initialization.
- [IEntityDispose](IEntityDispose.md) — handles resource cleanup.
- [IEntityEnable](IEntityEnable.md) — handles activation.
- [IEntityDisable](IEntityDisable.md) — handles deactivation.
- [IEntityTick](IEntityTick.md) — handles per-frame updates.
- [IEntityFixedTick](IEntityFixedTick.md) — handles fixed-timestep updates (physics, mechanics).
- [IEntityLateTick](IEntityLateTick.md) — handles post-render updates.
- [IEntityGizmos](IEntityGizmos.md) — handles debug visualization and gizmos in the scene.

