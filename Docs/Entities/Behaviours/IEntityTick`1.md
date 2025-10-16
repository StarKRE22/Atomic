# 🧩 IEntityTick&lt;E&gt;

```csharp
public interface IEntityTick<in E> : IEntityTick where E : IEntity
```

- **Description:** Provides a strongly-typed version of `IEntityTick` for handling update logic on a specific entity
  type.
- **Type Parameter:** `E` – The concrete entity type this behavior is associated with.
- **Inherits:** [IEntityTick](IEntityTick.md)

---

## 🏹 Methods

#### `Tick(E, float)`

```csharp
public void Tick(E entity, float deltaTime);
```

- **Description:** Called during the main update phase of the frame for the strongly-typed entity.
- **Parameters:**
    - `entity` – The strongly-typed entity being updated.
    - `deltaTime` – Elapsed time since the last frame.
- **Remarks:** Implements the base `IEntityTick.Tick(IEntity, float)` explicitly by casting the entity to type `E`.