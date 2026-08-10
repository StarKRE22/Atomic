# 🧩 EntityView\<E>

A visual representation of an entity in the Unity scene. It provides a complete system for showing / hiding entities,
installing behaviours, editor gizmos, custom naming, and safe activation / deactivation. Use as a foundation for UI or game objects
that visually represent entity data.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [View Setup](#ex1)
    - [Entity Rendering](#ex2)
    - [Gizmos Support](#ex3)
- [Inspector Settings](#-inspector-settings)
    - [Parameters](#-parameters)
    - [Gizmos](#-gizmos)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Properties](#-properties)
        - [Entity](#entity)
        - [IsActive](#isactive)
    - [Methods](#-methods)
        - [Activate(E)](#activatee)
        - [Deactivate()](#deactivate)
        - [OnActivate(E)](#onactivatee)
        - [OnDeactivate(E)](#ondeactivatee)
        - [FormateName(E)](#formatenamee)
    - [Extension Methods](#extension-methods)
        - [GetValue&lt;E, T&gt;](#getvaluee-t)
- [See Also](#-see-also)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ View Setup

Below is an example of setting up `EntityView<E>` that represents a tank entity.

#### 1. Assume we have entity type `IGameEntity` derived from [IEntity](../Entities/IEntity.md)

```csharp
public interface IGameEntity : IEntity
{
}
```

#### 2. Create custom view type derived from `EntityView<E>`

```csharp
public class GameEntityView : EntityView<IGameEntity>
{
}
```

#### 3. Attach `GameEntityView` to a GameObject

<img width="450" height="" alt="Entity component" src="../../Images/GameEntityView.png" />

#### 4. Create an entity installer for `GameEntityView`

```csharp
public sealed class TankViewInstaller : MonoEntityInstaller<IGameEntity>
{
    [SerializeField] private TakeDamageViewBehaviour _takeDamageBehaviour;
    [SerializeField] private PositionViewBehaviour _positionBehaviour;
    [SerializeField] private RotationViewBehaviour _rotationBehaviour;
    [SerializeField] private TeamColorViewBehaviour _teamColorBehaviour;
    [SerializeField] private WeaponRecoilViewBehaviour _weaponRecoilBehaviour;
    
    public override void Install(IGameEntity entity)
    {
        entity.AddBehaviour(_takeDamageBehaviour);
        entity.AddBehaviour(_positionBehaviour);
        entity.AddBehaviour(_rotationBehaviour);
        entity.AddBehaviour(_teamColorBehaviour);
        entity.AddBehaviour(_weaponRecoilBehaviour);
    }

    public override void Uninstall(IGameEntity entity)
    {
        entity.DelBehaviour(_takeDamageBehaviour);
        entity.DelBehaviour(_positionBehaviour);
        entity.DelBehaviour(_rotationBehaviour);
        entity.DelBehaviour(_teamColorBehaviour);
        entity.DelBehaviour(_weaponRecoilBehaviour);
    }
}
```

#### 5. Attach `TankViewInstaller` to the GameObject that contains the `GameEntityView` component

<img width="450" height="" alt="Entity component" src="../../Images/TankViewInstaller.png" />

#### 6. Drag and drop `TankViewInstaller` to the `installers` field of `GameEntityView`

<img width="450" height="" alt="Entity component" src="../../Images/GameEntityView%20(Installed).png" />

#### 7. Now your `GameEntityView` contains all behaviours that will be attached to a rendering entity

---

<div id="ex2"></div>

### 2️⃣ Entity Rendering

```csharp
// Get an instance of GameEntityView
GameEntityView view = ...;

// Get an instance of the entity
IGameEntity entity = ...;

// Start rendering the entity:
// The GameObject dynamically attaches all behaviours to the entity
view.Activate(entity);

// Stop rendering the entity:
// The GameObject is disabled, and all view behaviours are detached from the entity
view.Deactivate();
```

- **Notes:**
    - `Activate(entity)` — activates rendering and links the view with the entity.
    - `Deactivate()` — disables rendering and detaches the view from the entity.

---

<div id="ex3"></div>

### 3️⃣ Gizmos Support

You can create gizmo behaviours that implement [IEntityGizmos\<E>](../Behaviours/IEntityGizmos%601.md) and attach them via an installer.

#### 1. Create a custom gizmo for position and scale

```csharp
public sealed class TransformGizmos : IEntityGizmos<IGameEntity>
{
    public void DrawGizmos(IGameEntity entity)
    {
        Vector3 center = entity.GetValue<Vector3>("Position");
        float scale = entity.GetValue<float>("Scale");
        Handles.DrawWireDisc(center, Vector3.up, scale);
    }
}
```

#### 2. Attach the gizmo to a `MonoEntityInstaller<E>`

```csharp
public sealed class CharacterViewInstaller : MonoEntityInstaller<IGameEntity>
{
    private readonly TransformGizmos _transformGizmos = new();
    
    public override void Install(IGameEntity entity)
    {
        // Other bindings...
        entity.AddBehaviour(_transformGizmos);
    }

    public override void Uninstall(IGameEntity entity)
    {
        // Other bindings...
        entity.DelBehaviour(_transformGizmos);
    }
}
```

Use the inspector toggles to control when gizmos are drawn:

- `_onlySelectedGizmos` — draw gizmos only when the GameObject is selected.
- `_onlyEditModeGizmos` — disable gizmo drawing during Play mode.

---

## 🛠 Inspector Settings

### 🎛️ Parameters

| Parameter           | Description                                                                                                                                                       |
|---------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `installers`        | A list of **installers** that inject values and behaviors into the attached entity.<br>Each installer calls `Install()` when activated and `Uninstall()` when deactivated. |

---

### 🎨 Gizmos

| Setting              | Description                                       |
|----------------------|---------------------------------------------------|
| `_onlySelectedGizmos` | Draw gizmos only when the GameObject is selected. |
| `_onlyEditModeGizmos` | Disable gizmo drawing during Play mode.           |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public abstract class EntityView<E> : MonoBehaviour where E : class, IEntity
```

- **Type Parameter:** `E` — The type of entity associated with this view. Must
  implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** `MonoBehaviour`

---

### 🔑 Properties

#### `Entity`

```csharp
public E Entity { get; }
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

#### `Activate(E)`

```csharp
public void Activate(E entity);
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

- **Description:** Deactivates the view and removes the entity binding.
- **Details:**
    - Executes `Uninstall()` for all installers.
    - Calls `OnDeactivate(entity)`.
    - Clears the `Entity` reference.

#### `OnActivate(E)`

```csharp
protected virtual void OnActivate(E entity);
```

- **Description:** Invoked when the view is activated. Override to add custom behavior
  (e.g., updating UI or initializing components).

#### `OnDeactivate(E)`

```csharp
protected virtual void OnDeactivate(E entity);
```

- **Description:** Invoked when the view is deactivated. Override to add custom cleanup logic
  (e.g., stopping animations or unsubscribing from events).

#### `FormateName(E)`

```csharp
protected virtual string FormateName(E entity);
```

- **Description:** Formats the GameObject name for the view.
- **Default:** `{entity.Name}:{entity.InstanceID}`.
- **Note:** Override to customize naming behaviour.

---

### Extension Methods

#### `GetValue<E, T>`

```csharp
public static T GetValue<E, T>(this EntityView<E> view, ValueKey<E, T> key) where E : class, IEntity;
```

- **Description:** Shorthand for reading a typed value from the entity attached to the view.
- **Parameters:**
    - `view` — The view whose entity will be queried.
    - `key` — A typed `ValueKey<E, T>` for the requested value.
- **Returns:** The value stored in the entity.

Example:

```csharp
ValueKey<IGameEntity, int> healthKey = ...;
int health = view.GetValue(healthKey);
```

---

## 🔗 See Also

- [EntityView](EntityView.md) — non-generic wrapper.
- [IEntityGizmos\<E>](../Behaviours/IEntityGizmos%601.md) — gizmo behaviour interface.
- [Entity UI Manual](Manual.md)
