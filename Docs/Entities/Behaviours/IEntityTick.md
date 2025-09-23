# 🧩️ IEntityTick Interfaces

Represents a behavior interface that executes logic during the regular update cycle of
an [IEntity](../Entities/IEntity.md). It is automatically invoked once per frame by the entity’s `Tick`.

<details>
  <summary>
    <h2 id="entity-tick"> 🧩 IEntityTick</h2>
    <br>Defines a behavior that executes logic during the main update phase of an <code>IEntity</code>.
  </summary>

<br>

```csharp
public interface IEntityTick : IEntityBehaviour
```

- **Inheritance:** implements [IEntityBehaviour](IEntityBehaviour.md)

---

### 🏹 Methods

#### `Tick(IEntity, float)`

```csharp
public void Tick(IEntity entity, float deltaTime);
```

- **Description:** Called during the main update phase of the frame.
- **Parameters:**
    - `entity` – The entity being updated.
    - `deltaTime` – Elapsed time since the last frame.
- **Remarks:** Automatically called once per frame by `IEntity.Tick()`.

---

### 🗂 Example of Usage

Update a `Transform` component every frame for an entity

```csharp
public class MoveBehaviour : IEntityTick
{
    public void Tick(IEntity entity, float deltaTime)
    {
        Transform transform = entity.GetValue<Transform>("Transform");
        float speed = entity.GetValue<float>("Speed");
        Vector3 direction = entity.GetValue<Vector3>("MoveDirection");
        transform.position += direction * (speed * deltaTime);
    }
}
```

> Note: `GetValue<T>` assumes the entity has the relevant components and values already set.

</details>

---

<details>
  <summary>
    <h2 id="entity-tick-t"> 🧩 IEntityTick&lt;E&gt;</h2>
    <br>Defines a strongly-typed behavior that executes update logic on an <code>IEntity</code> of type <code>E</code>.
  </summary>

<br>

```csharp
public interface IEntityTick<in E> : IEntityTick where E : IEntity
```

- **Description:** Provides a strongly-typed version of `IEntityTick` for handling update logic on a specific entity
  type.
- **Type Parameter:** `E` – The concrete entity type this behavior is associated with.
- **Inherits:** [IEntityTick](#entity-tick)

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

---

### 🗂 Example of Usage

Update the position of a `UnitEntity` every frame

```csharp
public class UnitEntity : Entity
{
}
```

```csharp
public class MoveBehaviour : IEntityTick<UnitEntity>
{
    public void Tick(UnitEntity entity, float deltaTime)
    {
        float speed = entity.GetValue<float>("Speed");
        Vector3 position = entity.GetValue<Vector3>("Position");
        Vector3 direction = entity.GetValue<Vector3>("MoveDirection");
        
        Vector3 newPosition = position + direction * (speed * deltaTime);
        entity.SetValue("Position", newPosition);
    }
}
```

> Note: Uses the strongly-typed `UnitEntity`, so no casting from `IEntity` is required.

</details>

---

## 📝 Notes

- **Update Logic** – Encapsulates routines executed every frame.
- **Strongly-Typed Option** – `IEntityTick<E>` allows type-specific update logic.
- **Integration** – Called automatically by `IEntity.Tick()`.
- **Composable** – Can be combined with other behaviours for modular entity logic.

- `IEntityTick` is intended for per-frame update logic.
- `IEntityTick<E>` is useful when the behaviour is specific to a particular entity type.