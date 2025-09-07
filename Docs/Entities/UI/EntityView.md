# 🧩 EntityView

`EntityView<E>` represents a visual representation of an entity in the Unity scene.  
It provides a complete system for showing / hiding entities, applying / discarding aspects, editor gizmos, custom naming, and safe creation/destruction.

It comes in two forms:

* **Non-generic** (`EntityView`) for `IEntity`
* **Generic** (`EntityView<E>`) for specific entity types

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

## Key Features

### Visibility Management
- Show and hide entities dynamically using `Show(E)` and `Hide()`
- Optional automatic activation / deactivation of GameObject (`controlGameObject`)

### Custom Naming
- Override the GameObject name with `customName`
- Context menu option to assign GameObject name to `customName`

### Aspect Handling
- Automatically applies and discards `SceneEntityAspect<E>` components
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

---

## Properties

### Name
```csharp
public virtual string Name => overrideName ? customName : this.name;
```
- **Purpose**: Gets the display name of the view
- **Behavior**: Returns `customName` if override is enabled; otherwise returns GameObject's name

### Entity

```csharp
public E Entity { get; }
```
- **Purpose**: Gets the entity currently associated with this view

### IsVisible
```csharp
public bool IsVisible { get; }
```
- **Purpose**: Indicates whether the view is currently showing an entity

---

## Methods

### Show
```csharp
void Show(E entity);
```
- **Purpose**: Displays the view and associates it with the specified entity
- **Parameter**: `entity` — The entity to show
- **Behavior**: Activates the GameObject (if `controlGameObject` is true) and applies all aspects

### Hide
```csharp
void Hide();
```
- **Purpose**: Hides the view and removes the associated entity
- **Behavior**: Discards all applied aspects and optionally deactivates the GameObject

### OnShow
```csharp
protected virtual void OnShow(E entity);
```
- **Purpose**: Called when the view is shown
- **Parameter**: `entity` — The entity being displayed
- **Usage**: Override to add custom logic on show

### OnHide
```csharp
protected virtual void OnHide(E entity);
```
- **Purpose**: Called when the view is hidden
- **Parameter**: `entity` — The entity that was being displayed
- **Usage**: Override to add custom logic on hide

---

## Inspector Settings

| Field / Property    | Type                         | Description                                                                     |
|---------------------|------------------------------|---------------------------------------------------------------------------------|
| `controlGameObject` | `bool`                       | If true, activates / deactivates the GameObject when `Show` / `Hide` is called. |
| `overrideName`      | `bool`                       | If true, the view will use `customName` instead of the GameObject's name.       |
| `customName`        | `string`                     | Custom display name for the view (used only if `overrideName` is true).         |
| `aspects`           | `List<SceneEntityAspect<E>>` | List of aspects that provide values and behaviors to the entity when shown.     |

**Notes:**

- `controlGameObject`, `overrideName`, and `customName` control runtime behavior and naming in the inspector.
- `aspects` allows designers to assign multiple behaviors directly in the inspector.

### AssignCustomNameFromGameObject
```csharp
[ContextMenu("Assign Custom Name From GameObject")]
private void AssignCustomNameFromGameObject();
```
- **Purpose**: Assigns the GameObject's current name to `customName`
- **Usage**: Accessible via context menu in the Unity Inspector

## Installing

`EntityView` is configured using **aspects**, which are applied automatically when the view is spawned and discarded when the view is despawned.  
Each aspect encapsulates a set of behaviors or properties that should be applied to the entity during its lifecycle.

### How it works
- When the view is **spawned** (`Show`), each aspect’s `Apply` method is called.
- When the view is **despawned** (`Hide`), each aspect’s `Discard` method is called.
- This ensures that all entity-specific behaviors are properly initialized and cleaned up.

### Example: TankViewAspect

The following aspect configures a tank entity view with multiple behaviors:

```csharp
public sealed class TankViewAspect : SceneEntityAspect<IGameEntity>
{
    [SerializeField] private TakeDamageViewBehaviour _takeDamageBehaviour;
    [SerializeField] private PositionViewBehaviour _positionBehaviour;
    [SerializeField] private RotationViewBehaviour _rotationBehaviour;
    [SerializeField] private TeamColorViewBehaviour _teamColorBehaviour;
    [SerializeField] private WeaponRecoilViewBehaviour _weaponRecoilBehaviour;
    
    public override void Apply(IGameEntity entity)
    {
        entity.AddBehaviour(_takeDamageBehaviour);
        entity.AddBehaviour(_positionBehaviour);
        entity.AddBehaviour(_rotationBehaviour);
        entity.AddBehaviour(_teamColorBehaviour);
        entity.AddBehaviour(_weaponRecoilBehaviour);
    }

    public override void Discard(IGameEntity entity)
    {
        entity.DelBehaviour(_takeDamageBehaviour);
        entity.DelBehaviour(_positionBehaviour);
        entity.DelBehaviour(_rotationBehaviour);
        entity.DelBehaviour(_teamColorBehaviour);
        entity.DelBehaviour(_weaponRecoilBehaviour);
    }
}
```

**Key Points:**
- Encapsulates all visual behaviors for the tank entity.
- Safe to apply and discard multiple times, keeping the entity consistent.
- Makes it easy to add or remove behaviors without modifying the main `EntityView` logic.

## Gizmos Support

| Parameter             | Type   | Description                                                         |
|-----------------------|--------|---------------------------------------------------------------------|
| `_onlySelectedGizmos` | `bool` | If true, gizmos are drawn only when the object is selected.         |
| `_onlyEditModeGizmos` | `bool` | If true, gizmos are drawn only in Edit Mode, even during Play Mode. |

**Notes:**
- `_onlySelectedGizmos` and `_onlyEditModeGizmos` are editor-only settings affecting gizmo drawing in the Scene view.

### OnDrawGizmos
```csharp
protected virtual void OnDrawGizmos();
```
- **Purpose**: Unity callback to draw gizmos for this component
- **Behavior**: Defers to `OnDrawGizmosSelected` if `_onlySelectedGizmos` is false

### OnDrawGizmosSelected
```csharp
protected virtual void OnDrawGizmosSelected();
```
- **Purpose**: Unity callback invoked when the object is selected
- **Behavior**: Draws gizmos using entity behaviours that implement `IEntityGizmos<E>`
- **Editor-Specific**: Respects `_onlyEditModeGizmos` to avoid drawing during Play Mode if desired

### OnGizmosDraw
```csharp
internal void OnGizmosDraw();
```
- **Purpose**: Internal method for drawing gizmos
- **Behavior**: Iterates over all entity behaviours and calls `DrawGizmos` on those implementing `IEntityGizmos<E>`
- **Error Handling**: Catches exceptions and logs warnings

---

### Example Usage

Draws custom gizmos for position and scale
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
---

## Creation & Destruction

**Key Points:**
- `Create` centralizes GameObject and aspect setup
- `Destroy` ensures aspects are discarded and GameObject is safely removed

### CreateArgs

```csharp
[Serializable]
public struct CreateArgs
{
    [Tooltip("The name of the new GameObject to create for the EntityView.")]
    public string name;

    [Tooltip("Should activate and deactivate GameObject when Show / Hide are invoked?")]
    public bool controlGameObject;

    [Tooltip("Aspects that will configure the EntityView upon creation.")]
    public List<SceneEntityAspect<E>> aspects;

    [Header("Gizmos")]
    [Tooltip("If true, gizmos will be drawn only in Edit Mode.")]
    public bool onlyEditModeGizmos;

    [Tooltip("If true, gizmos will be drawn only when the object is selected.")]
    public bool onlySelectedGizmos;
}
```
**Key Points:**
- Encapsulates all configuration options for creating a new `EntityView`
- Makes setup consistent and flexible for different entity types

### Create<T>

```csharp
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static T Create<T>(in CreateArgs args = default) where T : EntityView<E>;
```
- **Purpose**: Instantiates a new `EntityView` GameObject and configures it
- **Parameter**: `args` — Arguments defining the GameObject name, aspects, activation, and gizmos behavior
- **Returns**: The created `EntityView` instance
- **Behavior**:
    1. Creates a new GameObject
    2. Adds the specified `EntityView` component
    3. Assigns aspects and editor settings
    4. Activates the GameObject

#### Example Usage
```csharp
var args = new EntityView.CreateArgs
{
    name = "PlayerView",
    controlGameObject = true,
    aspects = new List<SceneEntityAspect { jumpAspect, speedAspect },
    onlyEditModeGizmos = false,
    onlySelectedGizmos = true
};

var playerView = EntityView.Create(args);
```

### Destroy

```csharp
public static void Destroy(EntityView<E> view, float time = 0);
```
- **Purpose**: Safely destroys an `EntityView` and its GameObject
- **Parameters**:
    - `view` — The `EntityView` instance to destroy
    - `time` — Optional delay before destruction (default = 0)
- **Behavior**:
    1. Hides the view
    2. Destroys the associated GameObject after the optional delay


#### Example Usage
```csharp
EntityView.Destroy(playerView, 2f); // destroys after 2 seconds
```

---

## Example Usage
//TODO: