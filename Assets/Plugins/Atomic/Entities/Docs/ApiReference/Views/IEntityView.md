# 🧩 IEntityView

Represents a **modifiable visual view** of an `IEntity`.  
Extends `IReadOnlyEntityView` by allowing the view to be **shown or hidden**.

> [!NOTE]  
> This interface is intended for **visual representation** of entities and allows modifying the view state.  
> Use `IReadOnlyEntityView` when you only need read-only access.

---

## IEntityView
```csharp
public interface IEntityView : IReadOnlyEntityView
```
---

## Methods
```csharp
void Show(IEntity entity)
```
- Displays the view for the specified entity.
- Associates the view with the provided entity.
- **Parameters:**
    - `entity` — The entity to associate with and display through this view.

---
```csharp
void Hide()
```
- Hides or deactivates the current view.
- Removes the association with the entity.
