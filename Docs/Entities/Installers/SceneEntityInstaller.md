# 🧩️ SceneEntityInstaller Classes

Represents a Unity `MonoBehaviour` that can be attached to a GameObject to **perform installation logic on an `IEntity`** during runtime or initialization. 
It allows declarative configuration of entities placed in a scene. In the Editor, it supports automatic refresh via `OnValidate`.

> [!TIP]
> Use `SceneEntityInstaller` only if there are scene dependencies or if entity instances in the scene need to differ.  
> In other cases, use [ScriptableEntityInstaller](ScriptableEntityInstaller.md) as a shared installer.

---

<details>
  <summary>
    <h2 id="scene-entity-installer"> 🧩 SceneEntityInstaller</h2>
    <br>Abstract MonoBehaviour for configuring an <code>IEntity</code> in a Unity scene.
  </summary>

<br>

```csharp
public abstract class SceneEntityInstaller : MonoBehaviour, IEntityInstaller
{
    public abstract void Install(IEntity entity);
}
```
- **Description:** Implements `IEntityInstaller` to allow entity configuration via Unity components.
- **Remarks:** Supports editor refresh through `OnValidate` without entering Play Mode.

</details>























---

## Key Features

- **Scene Configuration** – Attach to a GameObject to configure entities in the scene.
- **Editor Support** – Automatically refreshes when properties are changed in the Inspector.
- **Runtime Installation** – Applies configuration and behaviors during runtime.
- **Strongly-Typed Option** – `SceneEntityInstaller<E>` ensures type-safe installation for specific entity types.

---

## Class: SceneEntityInstaller
```csharp
public abstract class SceneEntityInstaller : MonoBehaviour, IEntityInstaller
{
    public abstract void Install(IEntity entity);
}
```
- Implements `IEntityInstaller` to allow entity configuration via Unity components.
- Supports editor refresh through `OnValidate` without entering Play Mode.

---

## Class: SceneEntityInstaller&lt;E&gt;

```csharp
public abstract class SceneEntityInstaller<E> : SceneEntityInstaller where E : class, IEntity
{
    protected abstract void Install(E entity);
}
```
- Provides a **strongly-typed variant** for specific entity types.
- Eliminates the need for manual casting in derived installer classes.

---

## Example Usage

### Example #1. Non-Generic (IEntity)
```csharp
public sealed class CharacterInstaller : SceneEntityInstaller
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _moveSpeed = 5.0f;

    public override void Install(IEntity entity)
    {
        entity.AddTag("Character");
        entity.AddValue("Transform", _transform);
        entity.AddValue("MoveSpeed", _moveSpeed);
        entity.AddBehaviour<MoveBehaviour>();
    }
}
```

### Example #2. Generic with UnitEntity (strongly-typed)
```csharp
public sealed class CharacterInstaller : SceneEntityInstaller<UnitEntity>
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _moveSpeed = 5.0f;

    protected override void Install(UnitEntity entity)
    {
        entity.AddTag("Character");
        entity.AddValue("Transform", _transform);
        entity.AddValue("MoveSpeed", _moveSpeed);
        entity.AddBehaviour<MoveBehaviour>();
    }
}
```

> Note: Using the generic `UnitEntity` version allows type-safe access to entity-specific properties without casting.

---

## Remarks

- `SceneEntityInstaller` is intended for configuring or initializing entities **directly in the Unity scene**.
- `SceneEntityInstaller<E>` is useful when the installer is specific to a particular entity type.
- Supports editor workflows via `OnValidate` to refresh previews or dependent systems.
- Can be combined with other installers or entity behaviors to modularly set up complex entities.
