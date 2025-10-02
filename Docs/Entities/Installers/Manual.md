# 🧩 Entity Installers

**Entity Installer** is a component that installs tags, values, and behaviors into an entity instance. It provides a **declarative mechanism** for configuring entities during initialization or runtime.

Below are the different types of installers depending on the usage scenario:

- [IEntityInstaller](IEntityInstaller.md) <!-- + -->
- [IEntityInstaller&lt;E&gt;](IEntityInstaller%601.md) <!-- + -->
- [SceneEntityInstaller](SceneEntityInstaller.md) <!-- + -->
- [SceneEntityInstaller&lt;E&gt;](SceneEntityInstaller%601.md) <!-- + -->
- [ScriptableEntityInstaller](ScriptableEntityInstaller.md) <!-- + -->
- [ScriptableEntityInstaller&lt;E&gt;](ScriptableEntityInstaller%601.md)

---

## 🗂 Example of Usage

```csharp
public sealed class CharacterInstaller : SceneEntityInstaller
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _moveSpeed = 5.0f;

    public override void Install(IEntity entity)
    {
        entity.AddTag("Character");
        entity.AddTag("Moveable");

        entity.AddValue("Transform", _transform);
        entity.AddValue("MoveSpeed", _moveSpeed);
        entity.AddValue("MoveDirection", Vector3.zero);

        entity.AddBehaviour<MoveBehaviour>();
    }

    public override void Uninstall(IEntity entity)
    {
        // Cleanup or unsubscribe from events when the entity is destroyed
    }
}
```

---

## 📝 Notes

- **Installer** = declarative way of configuring entities.
- **SceneEntityInstaller** = configuration via `MonoBehaviour`, bound to the scene.
- **ScriptableEntityInstaller** = configuration via `ScriptableObject`, reusable logic.
- **Generic Installers** = strongly typed variant for improved safety and readability.
- Keep installers focused on **entity configuration only**; avoid embedding business logic.
- Always override `Uninstall` when working with subscriptions or `IDisposable` objects to ensure proper cleanup.  
