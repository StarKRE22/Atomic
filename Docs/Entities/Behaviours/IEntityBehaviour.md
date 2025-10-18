# 🧩️ IEntityBehaviour

Represents a base contract of logic that can be attached to an [entity](../Entities/Manual.md). Don't implement this
interface. It is only used for framework behaviour contracts.

---

## 📑 Table of Contents

- [API Reference](#-api-reference)
  - [Type](#-type)
- [Derived Interfaces](#-derived-interfaces)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEntityBehaviour
``` 

---

## 🧬 Derived Interfaces

The following interfaces extend `IEntityBehaviour` and manage different stages of the entity lifecycle.

- [IEntityInit](IEntityInit.md) — handles initialization.
- [IEntityDispose](IEntityDispose.md) — handles resource clean-up.
- [IEntityEnable](IEntityEnable.md) — handles activation.
- [IEntityDisable](IEntityDisable.md) — handles deactivation.
- [IEntityTick](IEntityTick.md) — handles per-frame updates.
- [IEntityFixedTick](IEntityFixedTick.md) — handles fixed-timestep updates (physics, mechanics).
- [IEntityLateTick](IEntityLateTick.md) — handles post-render updates.
- [IEntityGizmos](IEntityGizmos.md) — handles debug visualization and gizmos in the scene.