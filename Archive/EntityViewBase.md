# 🧩 EntityViewBase

Base class for all **entity views**. Implements `IEntityView` to provide basic functionality for **showing, hiding, and naming views** associated with `IEntity`.

> [!NOTE]  
> This class is intended to be a base for Unity MonoBehaviour views.  
> Override `OnShow(IEntity)` and `OnHide(IEntity)` to implement custom visual behavior.

---

## EntityViewBase
```csharp
public class EntityViewBase : MonoBehaviour, IEntityView
```
- **`[DisallowMultipleComponent]`**: Only one instance per GameObject.

---

## Inspector Settings

### overrideName
```csharp
[SerializeField] private bool _overrideName;
```
- If `true`, the view uses `_customName` instead of the GameObject's name.

### customName
```csharp
[SerializeField] private string _customName;
```
- Custom name for the view when `_overrideName` is enabled.
---

## Properties

### Name
```csharp
public virtual string Name { get; }

```
- Returns `_customName` if `_overrideName` is true; otherwise, returns the GameObject's name.

### Entity
```csharp
public IEntity Entity { get; }
```
- Gets the entity currently associated with this view.

### IsVisible
```csharp
public bool IsVisible { get; }
```
- Returns `true` if the view is currently visible (active in scene).

---

## Methods

### Show
```csharp
public void Show(IEntity entity)
```
- Displays the view and associates it with the specified entity.
- **Parameters:**
    - `entity` — The entity to associate with and display through this view.
- Throws `ArgumentNullException` if `entity` is null.

### Hide
```csharp
public void Hide()
```
- Hides the view and removes its association with the entity.
- Safe to call multiple times; no effect if already hidden.

### OnShow
```csharp
protected virtual void OnShow(IEntity entity)
```
- Called when the view is shown.
- Override to implement custom behavior (e.g., enabling visuals).
- Default implementation sets `gameObject.SetActive(true)`.

### OnHide
```csharp
protected virtual void OnHide(IEntity entity)
```
- Called when the view is hidden.
- Override to implement custom behavior (e.g., disabling visuals).
- Default implementation sets `gameObject.SetActive(false)`.

### Destroy
```csharp
public static void Destroy(EntityViewBase view, float time = 0)
```
- Destroys the view and its associated GameObject after an optional delay.
- Hides the view before destruction.
- **Parameters:**
    - `view` — The `EntityViewBase` instance to destroy.
    - `time` — Optional delay in seconds (default 0).
- Safe to call if `view` is null.

### AssignCustomNameFromGameObject
```csharp
[ContextMenu("Assign Custom Name From GameObject")]
private void AssignCustomNameFromGameObject()
```
- Assigns the GameObject's name to `_customName`.
- Useful for quickly setting the custom name in the editor.

---

## Debug

### DebugEntity
```csharp
private IEntity DebugEntity { get; set; }
```
- Debug property to inspect or modify the associated entity in the editor.

---

## Example Usage
Extends a custom `SoldierView` from `EntityViewBase`

```csharp
//Import library with elements
using Atomic.Elements;

public class SoldierView : EntityViewBase
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private TextMeshPro _healthText;
    [SerializeField] private Animator _animator;

    private readonly DisposableComposite _disposableComposite = new();
    
    protected override void OnShow(IEntity entity)
    {
        base.OnShow(entity); 
        
        _renderer.color = entity.GetValue<TeamType>() == TeamType.Blue ? Color.blue : Color.red;
        _healthText.text = entity.GetValue<int>("Health").ToString();
        
        entity
            .GetValue<IReactiveVariable<bool>>("IsMoving")
            .Observe(isMoving => _animator.SetBool("IsMoving", isMoving))
            .AddTo(_disposableComposite);
    }

    protected override void OnHide(IEntity entity)
    {
        base.OnHide(entity);
        
        _disposableComposite.Dispose(); // unsubscribes from all reactive observables
    }
}
```