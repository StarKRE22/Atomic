# 🧩 ScriptableEntityInstaller&lt;E&gt;

```csharp
public abstract class ScriptableEntityInstaller<E> : ScriptableEntityInstaller, IEntityInstaller<E>
    where E : class, IEntity
```

- **Description:** Strongly-typed variant of <code>ScriptableEntityInstaller</code>.
- **Type Parameter:** `E` – The specific entity type this installer supports.
- **Inheritance:** [ScriptableEntityInstaller](SceneEntityInstaller.md), [IEntityInstaller&lt;E&gt;](IEntityInstaller%601.md).

---

## 🏹 Methods

#### `Install(E)`

```csharp
public abstract void Install(E entity);
```

- **Description:** Installs data, values, or behaviors into the strongly-typed entity.
- **Parameters:** `entity` – The entity of type `E` to install configuration or components into.
- **Remarks:** Must be implemented by derived classes.

#### `Uninstall(E)`

```csharp
public virtual void Uninstall(E entity)
```

- **Description:** Removes previously installed data or behavior from the strongly-typed entity.
- **Parameters:** `entity` – The entity of type `E` to uninstall configuration, components, or behavior from.
- **Remarks:** Default implementation does nothing. Override to provide custom uninstall behavior.

---

## 🗂 Example of Usage

```csharp
[CreateAssetMenu(
    fileName = "MoveInstaller",
    menuName = "SampleGame/New MoveInstaller"
)]
public sealed class MoveInstaller<UnitEntity> : ScriptableEntityInstaller<UnitEntity>
{
    [SerializeField] private Const<float> _moveSpeed = 5.0f; 

    public override void Install(UnitEntity entity)
    {
        entity.AddTag("Moveable");
        entity.AddValue("MoveSpeed", _moveSpeed);
        entity.AddValue("MoveDirection", new ReactiveVariable<Vector3>());
    }
}
```

> Note: Using the generic `UnitEntity` version allows type-safe access to entity-specific properties without casting.