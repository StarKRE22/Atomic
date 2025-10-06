# 🧩 EntityView\<E>

```csharp
public abstract class EntityView<E> : MonoBehaviour where E : class, IEntity
```

- **Description:** A visual representation of an entity in the Unity scene.  
  It provides a complete system for showing / hiding entities, installing, editor gizmos, custom
  naming, and safe creation / destruction.
- **Type Parameter:** `E` — The type of entity associated with this view. Must
  implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** `MonoBehaviour`
- **Usage:** Use as a foundation for UI or game objects that visually represent entity data.

---

## 🛠 Inspector Settings

| Parameter            | Description                                                                                                                                                       |
|----------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `controlGameObject`  | If `true`, `GameObject.SetActive(true/false)` will be automatically called when invoking `Show()` or `Hide()`.                                                    |
| `overrideName`       | If `true`, the view will use `customName` instead of the `GameObject.name`.                                                                                       |
| `customName`         | Custom name used for the view when `overrideName == true`.                                                                                                        |
| `installers`         | A list of **installers** that inject values and behaviors into the attached entity.<br>Each installer calls `Install()` when shown and `Uninstall()` when hidden. |
| `onlySelectedGizmos` | If true, gizmos will be drawn only when the object is selected.                                                                                                   |
| `onlyEditModeGizmos` | If true, gizmos will be drawn only in Edit Mode, even during play mode.                                                                                           |

## 🔑 Properties

#### `Name`

```csharp
public virtual string Name { get; }
```

- **Description:** Returns the display name of the view:
    - `customName`, if `overrideName == true`,
    - otherwise, `GameObject.name`.

#### `Entity`

```csharp
public E Entity { get; }
```

- **Description:** The entity currently bound to this view.
- **Note:** Only available after calling `Show()`.

#### `IsVisible`

```csharp
public bool IsVisible { get; }
```

- **Description:** Indicates whether the view is currently visible (`Entity != null`).

---

## 🏹 Public Methods

#### `Show(E entity)`

```csharp
public void Show(E entity);
```

- **Description:** Displays the view and binds it to the specified entity.
- **Parameter:** `entity` — The entity to associate with this view.
- **Throws:** `ArgumentNullException`, if `entity` is `null`.
- **Details:**
    - Activates the `GameObject` if `controlGameObject == true`.
    - Calls `OnShow(entity)` for custom logic.
    - Executes `Install()` on each `SceneEntityInstaller` in the list.

#### `Hide()`

```csharp
public void Hide();
```

- **Description:** Hides the view and removes the entity binding.
- **Details:**
    - Executes `Uninstall()` for all installers.
    - Calls `OnHide(entity)`.
    - Deactivates the `GameObject` if `controlGameObject == true`.
    - Clears the `Entity` reference.

---

## 🏹 Protected Methods

#### `OnShow(E entity)`

```csharp
protected virtual void OnShow(E entity);
```

- **Description:** Invoked when the view is shown. Override to add custom behavior  
  (e.g., updating UI or initializing components).

#### `OnHide(E entity)`

```csharp
protected virtual void OnHide(E entity);
```

- **Description:** Invoked when the view is hidden. Override to add custom cleanup logic  
  (e.g., stopping animations or unsubscribing from events).

---

## 🏹 Static Methods

#### `Create<T>(CreateArgs)`

```csharp
public static T Create<T>(in CreateArgs args = default) where T : EntityView<E>
```

- **Description:** Creates a new `EntityView` GameObject, applies configuration, and returns the instance.
- **Parameter:** `args` — Arguments for creating and configuring the new view.
- **Returns:** A new `EntityView` instance of type `T`.
- **Details:**
    - Creates a new inactive `GameObject` with the given `name`.
    - Attaches a component of type `T` (derived from `EntityView<E>`).
    - Applies provided `installers`, `controlGameObject`, and gizmo settings.
    - Activates the object before returning.

#### `Destroy(EntityView<E>, float)`

```csharp
public static void Destroy(EntityView<E> view, float time = 0);
```

- **Description:** Destroys the specified view and its `GameObject` after an optional delay.
- **Parameters:**
    - `view` — The `EntityView` instance to destroy.
    - `time` — Optional delay (in seconds) before destruction. Defaults to `0`.
- **Details:**
    - Calls `Hide()` before destroying.
    - Uses Unity's `Object.Destroy`.

---

## 🧩 CreateArgs

```csharp
[Serializable]
public struct CreateArgs
```

- **Description:** Arguments used to configure and create a new `EntityView<E>` GameObject instance.

| Field                | Description                                                                                  |
|----------------------|----------------------------------------------------------------------------------------------|
| `name`               | The name of the newly created `GameObject` for the `EntityView`.                             |
| `controlGameObject`  | If `true`, the created view will automatically call `GameObject.SetActive()` in `Show/Hide`. |
| `installers`         | A list of **installers** that configure the view upon creation.                              |
| `onlyEditModeGizmos` | If `true`, gizmos will only be drawn in **Edit Mode**.                                       |
| `onlySelectedGizmos` | If `true`, gizmos will only be drawn when the object is **selected**.                        |

---

## ▶️ Context Menu

#### `AssignCustomNameFromGameObject`

```csharp
[ContextMenu("Assign Custom Name From GameObject")]
private void AssignCustomNameFromGameObject();
```

- **Description**: Assigns the GameObject's current name to `customName`
- **Usage**: Accessible via context menu in the Unity Inspector

---

## 🗂 Examples of Usage

### 1️⃣ Installing `Entity View`

#### 1. Assume we have entity type `IGameEntity` and `GameEntityView`

```csharp
public interface IGameEntity : IEntity
{
}
```

```csharp
public class GameEntityView : EntityView<IGameEntity>
{
}
```

#### 2. Create `TankViewInstaller` script

```csharp
public sealed class TankViewInstaller : SceneEntityInstaller<IGameEntity>
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

#### 3. Attach `TankViewInstaller` script to the GameObject associated with `GameEntityView` and configure it

---

### 2️⃣ Gizmos Support

#### 1. Create custom gizmos for position and scale

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

#### 2. Attach it to `SceneEntityInstaller`

```csharp

public sealed class CharacterViewInstaller : SceneEntityInstaller<IGameEntity>
{
    private readonly TransformGizmos _transformGizmos = new();
    
    public override void Install(IGameEntity entity)
    {
        entity.AddBehaviour(_transformGizmos);
    }

    public override void Uninstall(IGameEntity entity)
    {
        entity.DelBehaviour(_transformGizmos);
    }
}
```

---

### 3️⃣ Runtime `EntityView` Creation

```csharp
var args = new EntityView<IGameEntity>.CreateArgs
{
    name = "PlayerView",
    controlGameObject = true,
    aspects = new List<SceneEntityAspect { jumpAspect, speedAspect },
    onlyEditModeGizmos = false,
    onlySelectedGizmos = true
};

var playerView = EntityView<IGameEntity>.Create(args);
```


---

### 4️⃣ Runtime `EntityView` Destruction

```csharp
EntityView<IGameEntity>.Destroy(playerView, 2f); // destroys after 2 seconds
```