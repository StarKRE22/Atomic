



# 🧩 IEntityAspect Classes

Represents a reusable piece of behavior or logic that can be **applied to [entities](../Entities) and later discarded**.
Use `IEntityAspect` when you want modular, reusable behavior that can be dynamically applied or removed from entities
at runtime.

---

<details>
  <summary>
    <h2 id="ienity-aspect"> 🧩 IEntityAspect</h2>
    <br>Non-generic interface for applying/discarding behavior on any <code>IEntity</code>.
  </summary>

<br>

```csharp
public interface IEntityAspect : IEntityAspect<IEntity>
```

- **Inheritance:** Implements [IEntityAspect&lt;IEntity&gt;](#ienity-aspect-e)
- **Description:** Applies or discards reusable behavior for any entity implementing `IEntity`.

---

### 🏹 Methods

#### `Apply(IEntity)`

```csharp
void Apply(IEntity entity);
```

- **Description:** Applies the aspect to the specified entity.
- **Parameters:** `entity` – The entity to which the aspect will be applied.

#### `Discard(IEntity)`

```csharp
void Discard(IEntity entity);
```

- **Description:** Reverses the effects of `Apply` on the specified entity.
- **Parameters:** `entity` – The entity from which the aspect should be removed.

---

### 🗂 Example of Usage

The `SpeedBuff` temporarily multiplies an entity's speed and restores it when discarded.

```csharp
[Serializable]
public sealed class SpeedBuff : IEntityAspect
{
    [SerializeField]
    private float _factor = 2f;

    public void Apply(IEntity entity)
    {
        IVariable<float> speed = entity.GetValue<IVariable<float>>("Speed"); 
        speed.Value *= _factor;
    }

    public void Discard(IEntity entity)
    {
        IVariable<float> speed = entity.GetValue<IVariable<float>>("Speed"); 
        speed.Value /= _factor;
    }
}
```

</details>

---

<details>
  <summary>
    <h2 id="ienity-aspect-e"> 🧩 IEntityAspect&lt;E&gt;</h2>
    <br>Generic interface for applying/discarding behavior on a specific entity type.
  </summary>

<br>

```csharp
public interface IEntityAspect<in E> where E : IEntity
```

- **Type Parameter:** `E` – The specific entity type this aspect operates on.
- **Description:** Provides type-safe behavior application and discard for a specific entity type.

---

### 🏹 Methods

#### `Apply(E)`

```csharp
void Apply(E entity);
```

- **Description:** Applies the aspect to the strongly-typed entity.
- **Parameters:** `entity` – The entity of type `E` to which the aspect will be applied.

#### `Discard(E)`

```csharp
void Discard(E entity);
```

- **Description:** Reverses the effects of `Apply` on the strongly-typed entity.
- **Parameters:** `entity` – The entity of type `E` from which the aspect should be removed.

---

### 🗂 Example of Usage

The `JumpAspect` adds jump-related capabilities to entities implementing `IGameEntity`.

```csharp
[Serializable]
public sealed class JumpAspect : IEntityAspect<IGameEntity>
{
    [SerializeField]
    private float _jumpForce = 2;

    public void Apply(IGameEntity entity)
    {
        entity.AddTag("Jumpable");
        entity.AddValue("JumpForce", _jumpForce);
        entity.AddBehaviour<JumpBehaviour>();
    }

    public void Discard(IGameEntity entity)
    {
        entity.DelTag("Jumpable");
        entity.DelValue("JumpForce");
        entity.DelBehaviour<JumpBehaviour>();
    }
}
```

> Note: Using the generic `IGameEntity` version allows type-safe access to entity-specific properties without casting.

</details>

---

## 📝 Notes

- **Dynamic Logic** – Aspects encapsulate modular behavior that can be applied and discarded dynamically.
- **Generic Option** – `IEntityAspect<E>` allows compile-time type safety for specific entity types.
- **Non-Generic Option** – `IEntityAspect` works with any `IEntity` for convenience.
- **Implementation** – Can be implemented as `ScriptableObject` or `MonoBehaviour`.
- **Reusability** – The same aspect instance can be applied to multiple entities.
