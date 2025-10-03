# 🧩 SceneEntityBaker<E>

```csharp
public abstract partial class SceneEntityBaker<E> : MonoBehaviour, IEntityFactory<E>
    where E : IEntity
```

- **Description:** An abstract Unity `MonoBehaviour` that serves as a scene-based entity baker. It creates entities of
  type `E` using a [ScriptableEntityFactory\<E>](../Factories/ScriptableEntityFactory%601.md) and optionally destroys
  its GameObject after
  baking.

- **Type Parameter:** `E` — The type of entity to bake, must implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** `MonoBehaviour`, [IEntityFactory<E>](../Factories/IEntityFactory%601.md)

- **Note:** Can be used to spawn or initialize entities in a Unity scene and immediately transfer them to runtime logic.

- **See also:** [ScriptableEntityFactory<E>](../Factories/ScriptableEntityFactory%601.md),
  [IEntityFactory<E>](../Factories/IEntityFactory%601.md)

---

## 🛠 Inspector Settings

| Parameter          | Description                                                           |
|--------------------|-----------------------------------------------------------------------|
| `destroyAfterBake` | Should destroy this GameObject after baking? Default is `true`.       |
| `factory`          | The `ScriptableEntityFactory<E>` that this baker will use / override. |

---

## 🏹 Methods

#### `Bake()`

```csharp
public E Bake();
```

- **Description:** Creates a new entity using the assigned factory, installs it, and optionally destroys the baker's
  GameObject.
- **Returns:** The created entity of type `E`.

#### `Install(E)`

```csharp
protected abstract void Install(E entity);
```

- **Description:** Abstract method to perform custom installation or initialization logic on the baked entity.
- **Parameter:** `entity` — The entity instance being installed.

#### `IEntityFactory<E>.Create()`

```csharp
E IEntityFactory<E>.Create();
```

- **Description:** Interface implementation that simply calls `Bake()`.

---

## 🗂 Example of Usage

```
public class EnemyBaker : SceneEntityBaker<EnemyEntity>
{
protected override void Install(EnemyEntity entity)
{
// Custom initialization for EnemyEntity
entity.Health = 100;
entity.SetPosition(this.transform.position);
}
}

// Usage in scene
EnemyBaker baker = FindObjectOfType<EnemyBaker>();
EnemyEntity enemy = baker.Bake();
```

---

## 📝 Notes

- The baker uses the assigned `ScriptableEntityFactory<E>` to create entities.
- If `_destroyAfterBake` is `true`, the GameObject with the baker will be destroyed immediately after baking.
- Derived classes must implement `Install(E)` to define custom initialization logic for the baked entity.
- Can be used as a **scene-level factory** to pre-instantiate or configure entities in the Unity Editor.
