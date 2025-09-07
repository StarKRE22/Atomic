# 🧩 IReadOnlyEntityCollectionView

Represents a **read-only view of an entity collection**.  
Provides notifications when entity views are added or removed and allows retrieving the view associated with a specific entity.

> [!NOTE]  
> Use this interface when you only need to observe entities without modifying the collection.

---

## IReadOnlyEntityCollectionView
```csharp
public interface IReadOnlyEntityCollectionView : IReadOnlyCollection<KeyValuePair<IEntity, IReadOnlyEntityView>>
```
- Inherits from `IReadOnlyCollection` of key-value pairs (`IEntity` → `IReadOnlyEntityView`).
- Provides events for added and removed views.
- Allows retrieving the view associated with a specific entity.

---

## Events

### OnAdded
```csharp
event Action<IEntity, IReadOnlyEntityView> OnAdded;
```
- Raised when a view is spawned for a newly added entity.
- Subscribers can use this event to react to entity instantiation in the view.

### OnRemoved
```csharp
event Action<IEntity, IReadOnlyEntityView> OnRemoved;
```
- Raised when a view is removed for a despawned or removed entity.
- Subscribers can use this event to react to entity destruction or removal from the view.

---

## Methods

### GetView
```csharp
IReadOnlyEntityView GetView(IEntity entity);
```
- Retrieves the view instance associated with the specified entity.
- Parameter:
    - `entity` – The entity whose view is requested.
- Returns: The active `IReadOnlyEntityView` instance associated with the entity, or `null` if no view exists.
