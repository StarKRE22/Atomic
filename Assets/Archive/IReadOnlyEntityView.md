# 🧩 IReadOnlyEntityView

Represents a **read-only view** of an `IEntity`.  
Provides basic information about the view without allowing modifications.

> [!NOTE]  
> This interface is intended for **read-only access** to entity data and should not be used to modify entity state.

---

## IReadOnlyEntityView
```csharp
public interface IReadOnlyEntityView
```

---

## Properties

```csharp
string Name { get; }
```
- Gets the display name or identifier of the view.
---
```csharp
IEntity Entity { get; }
```
- Gets the entity instance currently associated with this view.

---
```csharp
bool IsVisible { get; }
```
- Gets a value indicating whether the view is currently visible.
- Example: active in scene or UI.
