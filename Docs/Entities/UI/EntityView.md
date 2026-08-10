# 🧩 EntityView

A visual representation of an entity in the Unity scene. It provides a complete system for showing / hiding entities,
installing behaviours, editor gizmos, custom naming, and safe activation / deactivation. Use as a foundation for UI or game objects
that visually represent entity data. A **non-generic wrapper** around [EntityView\<E>](EntityView%601.md) fixed
to [IEntity](../Entities/IEntity.md).

---

## 📑 Table of Contents

- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Properties](#-properties)
        - [Name](#name)
        - [Entity](#entity)
        - [IsActive](#isactive)
    - [Methods](#-methods)
        - [Activate(IEntity)](#activateientity)
        - [Deactivate()](#deactivate)
- [See Also](#-see-also)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[AddComponentMenu("Atomic/Entities/Entity View")]
[DisallowMultipleComponent] 
public class EntityView : EntityView<IEntity>
```

- **Description:** Default entity view component.
- **Inheritance:** [EntityView\<E>](EntityView%601.md), `MonoBehaviour`
- **Usage:** Useful when the exact entity type is unknown or irrelevant (e.g., working with heterogeneous entities).

---

### 🔑 Properties

#### `Name`

```csharp
[field: SerializeField]
public string Name { get; private set; }
```

- **Description:** Display name of the view, editable in the Inspector.

#### `Entity`

```csharp
public IEntity Entity { get; }
```

- **Description:** The entity currently bound to this view.
- **Note:** Only available after calling `Activate()`.

#### `IsActive`

```csharp
public bool IsActive { get; }
```

- **Description:** Indicates whether the view is currently active (`Entity != null`).

---

### 🏹 Methods

#### `Activate(IEntity)`

```csharp
public void Activate(IEntity entity);
```

- **Description:** Displays the view and binds it to the specified entity.
- **Parameter:** `entity` — The entity to associate with this view.
- **Throws:** `ArgumentNullException`, if `entity` is `null`.
- **Details:**
    - Calls `Deactivate()` to clean up any previous entity.
    - Formats the GameObject name as `{entity.Name}:{entity.InstanceID}`.
    - Calls `OnActivate(entity)` for custom logic.
    - Executes `Install()` on each `MonoEntityInstaller` in the list.

#### `Deactivate()`

```csharp
public void Deactivate();
```

- **Description:** Hides the view and removes the entity binding.
- **Details:**
    - Executes `Uninstall()` for all installers.
    - Calls `OnDeactivate(entity)`.
    - Clears the `Entity` reference.

---

## 🔗 See Also

- [EntityView\<E>](EntityView%601.md) — typed base class with full API and gizmos support.
- [Entity UI Manual](Manual.md)
