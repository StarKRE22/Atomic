# 🧩 Entity Installers


- [Installers]()
    - [IEntityInstaller](Installers/IEntityInstaller.md)
    - [SceneEntityInstaller](Installers/SceneEntityInstaller.md)
    - [ScriptableEntityInstaller](Installers/ScriptableEntityInstaller.md)


## 📝 Notes

- **Entity Configuration** – Encapsulates setup routines for entities.
- **Strongly-Typed Option** – `IEntityInstaller<E>` allows type-safe configuration.
- **Composable** – Multiple installers can be applied to the same entity.
- **Integration** – Works in both runtime and editor simulation workflows.
- `IEntityInstaller` is intended for configuring or initializing entities before or during their lifecycle.
- `IEntityInstaller<E>` is useful when the installer is specific to a particular entity type.


## 📝 Notes

- **Scene Configuration** – Attach to a GameObject to configure entities in the scene.
- **Editor Support** – Automatically refreshes when properties are changed in the Inspector.
- **Runtime Installation** – Applies configuration and behaviors during runtime.
- **Strongly-Typed Option** – `SceneEntityInstaller<E>` ensures type-safe installation for specific entity types.
- Supports editor workflows via `OnValidate` to refresh previews or dependent systems.
- Can be combined with other installers or entity behaviors to modularly set up complex entities.
- `SceneEntityInstaller` is intended for configuring or initializing entities **directly in the Unity scene**.
- `SceneEntityInstaller<E>` is useful when the installer is specific to a particular entity type.


## 📝 Notes

- **Shared Configuration** – Use `ScriptableEntityInstaller` for reusable entity setup logic across multiple entities.
- **Strongly-Typed Option** – `ScriptableEntityInstaller<E>` ensures type-safe installation for specific entity types.
- **Runtime & Edit-Time Support** – Can be used in both runtime and editor contexts.
- **Modular** – Can be combined with other installers or entity behaviors to create complex, composable setups.
- `ScriptableEntityInstaller` is intended for **shared and reusable entity configuration**.
- `ScriptableEntityInstaller<E>` is useful when the installer targets a specific entity type.
