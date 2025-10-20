<!--

# 🧩 EntityViews

A visual representation of an entity in the Unity scene. It provides a complete system for showing / hiding entities,
installing, editor gizmos, custom naming, and safe creation / destruction. Use as a foundation for UI or game objects
that visually represent entity data. 

---


## 💡 Key Features

### Visibility Management
- Show and hide entities dynamically using `Show(E)` and `Hide()`
- Optional automatic activation / deactivation of GameObject (`controlGameObject`)

### Custom Naming
- Override the GameObject name with `customName`
- Context menu option to assign GameObject name to `customName`

### Install Handling
- Automatically installs and uninstalls `SceneEntityInstaller<E>` components
- Ensures entity-specific behaviors are applied when visible and cleaned up when hidden

### Editor Gizmos
- Draw gizmos for selected or all objects
- Draw only in Edit Mode if desired
- Uses entity behaviours implementing `IEntityGizmos<E>`

### Creation & Destruction
- `Create<T>` instantiates a new `EntityView` GameObject with configuration via `CreateArgs`
- `Destroy` safely hides and destroys the view, with optional delay

### Type Safety
- Generic interface allows compile-time type checking
- Non-generic interface provides convenience for general `IEntity` usage




## 📚 Content

- [Key Features](#key-features)
- [Properties](#properties)
- [Methods](#methods)
- [Inspector Settings](#inspector-settings)
- [Installing](#installing)
- [Gizmos Support](#gizmos-support)
- [Creation & Destruction](#creation--destruction)
- [Example Usage](#example-usage)

---

-->