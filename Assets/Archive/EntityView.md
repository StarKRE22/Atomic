# 🧩 EntityView

Extends **`EntityViewBase`** to provide **automatic installation of behaviours**, gizmo drawing, and management of entity-specific logic.

> [!NOTE]  
> This class manages `IEntity` behaviours and installers automatically when shown or hidden.  
> Gizmo drawing can be configured to appear only in edit mode or only when selected.

---

## EntityView
```csharp
public class EntityView : EntityViewBase
```
- **`[DisallowMultipleComponent]`** – Only one instance per GameObject.

---

## Inspector Settings

### installers
```csharp
[SerializeField] private List<EntityViewInstaller> _installers;
```
- List of installers used to configure and set up the view when shown.

### onlySelectedGizmos
```csharp
[SerializeField] private bool _onlySelectedGizmos;
```
- If `true`, gizmos are drawn only when the object is selected in the editor.

### onlyEditModeGizmos
```csharp
[SerializeField] private bool _onlyEditModeGizmos;
```
- If `true`, gizmos are drawn only in edit mode, even during play mode.

---

## Methods

### OnShow
```csharp
protected override void OnShow(IEntity entity)
```
- Called when the view is shown.
- Installs all installers (once) and applies registered behaviours to the entity.
- Activates the GameObject.

### OnHide
```csharp
protected override void OnHide(IEntity entity)
```
- Called when the view is hidden.
- Removes previously added behaviours from the entity.
- Deactivates the GameObject.

### AddBehaviour
```csharp
public void AddBehaviour(IEntityBehaviour behaviour)
```
- Adds a behaviour to the view and, if visible, immediately applies it to the entity.
- Throws `ArgumentNullException` if `behaviour` is null.

### HasBehaviour
```csharp
public bool HasBehaviour(IEntityBehaviour behaviour)
```
- Returns `true` if the view contains the specified behaviour; otherwise, `false`.

### DelBehaviour
```csharp
public void DelBehaviour(IEntityBehaviour behaviour)
```
- Removes the behaviour from the view and, if visible, removes it from the entity.

### GetBehaviourAt
```csharp
public IEntityBehaviour GetBehaviourAt(int index)
```
- Returns the behaviour at the given index.
- Throws `ArgumentOutOfRangeException` if index is invalid.

### Install
```csharp
private void Install()
```
- Executes all `installers` once.
- Marks `installed = true` after first execution.

---

## Static

### CreateArgs
```csharp
[Serializable]
public struct CreateArgs
{
    public string name;
    public List<EntityViewInstaller> installers;
    public IEnumerable<IEntityBehaviour> behaviours;
    public bool onlyEditModeGizmos;
    public bool onlySelectedGizmos;
}
```
- Arguments for creating a new `EntityView`.

### Create
```csharp
public static EntityView Create(CreateArgs args = default)
```
- Creates a new `GameObject` with `EntityView`.
- Sets installers, behaviours, and gizmo options.
- Activates the GameObject after setup.

---

## Example Usage

```csharp
var view = EntityView.Create(new EntityView.CreateArgs
{
    name = "SoldierView",
    installers = new List<EntityViewInstaller> { new SoldierInstaller() },
    behaviours = new IEntityBehaviour[] { new HealthBehaviour() },
    onlyEditModeGizmos = true,
    onlySelectedGizmos = false
});

view.Show(someEntity);
```