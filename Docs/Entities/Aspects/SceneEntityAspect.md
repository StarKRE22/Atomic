# 🧩 SceneEntityAspect Classes

Represents a reusable `MonoBehaviour` that can **apply or discard behaviors on [entities](../Entities/IEntity.md) within
a Unity scene**. It comes
in two forms:

> [!TIP]
> Use `SceneEntityAspect` when you want modular, reusable behaviors that can be dynamically applied or removed from
> scene entities.

---

<details>
  <summary>
    <h2 id="scene-entity-aspect"> 🧩 SceneEntityAspect</h2>
    <br>Non-generic MonoBehaviour for applying/discarding behavior on any <code>IEntity</code> in the scene.
  </summary>

<br>

```csharp
public abstract class SceneEntityAspect : SceneEntityAspect<IEntity>, IEntityAspect
```

- **Inheritance:** Implements [SceneEntityAspect&lt;IEntity&gt;](#scene-entity-aspect-e)
- **Description:** Applies or discards reusable behavior for any entity implementing `IEntity` within a Unity scene.

---

### 🏹 Methods

#### `Apply(IEntity)`

```csharp
public abstract void Apply(IEntity entity);
```

- **Description:** Applies the aspect to the specified entity.
- **Parameters:** `entity` – The entity to which the aspect will be applied.

#### `Discard(IEntity)`

```csharp
public abstract void Discard(IEntity entity);
```

- **Description:** Reverses the effects of `Apply` on the specified entity.
- **Parameters:** `entity` – The entity from which the aspect should be removed.

---

### 🗂 Example of Usage

The `SpeedBoost` aspect temporarily multiplies an entity's speed and restores it when discarded.

```csharp
public sealed class SpeedBoost : SceneEntityAspect
{
    [SerializeField] 
    private float _multiplier = 1.5f;

    public override void Apply(IEntity entity)
    {
        entity.GetValue<IVariable<float>>("Speed").Value *= _multiplier;
    }

    public override void Discard(IEntity entity)
    {
        entity.GetValue<IVariable<float>>("Speed").Value /= _multiplier;
    }

}
```

</details>

---

<details>
  <summary>
    <h2 id="scene-entity-aspect-e"> 🧩 SceneEntityAspect&lt;E&gt;</h2>
    <br>Generic MonoBehaviour for applying/discarding behavior on a specific entity type.
  </summary>

<br>

```csharp
public abstract class SceneEntityAspect<E> : MonoBehaviour, IEntityAspect<E> where E : IEntity
```

- **Type Parameter:** `E` – The specific entity type this aspect operates on.
- **Description:** Provides type-safe behavior application and discard for a specific entity type.
- **Inheritance:** Implements [IEntityAspect&lt;E&gt;](IEntityAspect.md/#ienity-aspect-e)

---

### 🏹 Methods

#### `Apply(E)`

```csharp
public abstract void Apply(E entity);
```

- **Description:** Applies the aspect to the strongly-typed entity.
- **Parameters:** `entity` – The entity of type `E` to which the aspect will be applied.

#### `Discard(E)`

```csharp
public abstract void Discard(E entity);
```

- **Description:** Reverses the effects of `Apply` on the strongly-typed entity.
- **Parameters:** `entity` – The entity from which the aspect should be removed.

---

### 🗂 Example of Usage

The `JumpAspect` adds jump capabilities to entities implementing `IGameEntity`.

```csharp
public sealed class JumpAspect : SceneEntityAspect<IGameEntity>
{
    [SerializeField]
    private float _jumpForce = 3f;

    public override void Apply(IGameEntity entity)
    {
        entity.AddTag("Jumpable");
        entity.AddValue("JumpForce", _jumpForce);
        entity.AddBehaviour<JumpBehaviour>();
    }

    public override void Discard(IGameEntity entity)
    {
        entity.DelTag("Jumpable");
        entity.DelValue("JumpForce");
        entity.DelBehaviour<JumpBehaviour>();
    }

}
```

> Note: Using the generic `SceneEntityAspect<IGameEntity>` allows type-safe access to entity-specific properties without
> casting.

</details>

---

## 📝 Notes

- **Dynamic Logic** – Aspects encapsulate modular behavior that can be applied and discarded dynamically.
- **Generic Option** – `SceneEntityAspect<E>` allows compile-time type safety for specific entity types.
- **Non-Generic Option** – `SceneEntityAspect` works with any `IEntity` for convenience.
- **Implementation** – Implemented as `MonoBehaviour`, can be attached to GameObjects in the scene.
- **Reusability** – The same aspect instance can be applied to multiple entities.