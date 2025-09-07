# 🧩 IEntityTrigger

The `IEntityTrigger` interface defines a mechanism for monitoring specific aspects of an entity's state and signaling when an entity should be re-evaluated by a filter. It comes in two forms:

* **Non-generic** version (`IEntityTrigger`) for working with `IEntity`
* **Generic** version (`IEntityTrigger<E>`) for specific entity types

---

## Key Features

### Action-Based Callbacks
- Configurable callback system for re-evaluation notifications
- Generic action support for type-safe entity handling
- Flexible trigger response patterns

### Entity Tracking
- Track/untrack lifecycle for entity monitoring
- Multiple entity support per trigger instance
- Clean resource management patterns

### Type Safety
- Generic interface for specific entity types
- Compile-time type checking for callbacks
- Non-generic convenience interface available

---

## IEntityTrigger
**A shorthand interface for `IEntityTrigger<IEntity>`.**

```csharp
public interface IEntityTrigger : IEntityTrigger<IEntity>
{
}
```

## IEntityTrigger&lt;E&gt;
**A generic interface for tracking specific entity types.**

```csharp
public interface IEntityTrigger<E> where E : IEntity
{
    void SetAction(Action<E> action);
    void Track(E entity);
    void Untrack(E entity);
}
```
---

## Methods

### SetAction
```csharp
void SetAction(Action<E> action);
```
- **Purpose**: Sets the callback for re-evaluation
- **Parameter**: 
  - `action` — The action to invoke when re-evaluation is needed
- **Usage**: Defines what happens when a tracked condition is met

### Track
```csharp
void Track(E entity);
```
- **Purpose**: Begins monitoring the specified entity
- **Parameter:**
  - `entity` — The entity to track
- **Behavior**: Starts observing changes relevant to the trigger

### Untrack
```csharp
void Untrack(E entity);
```
- **Purpose:** Stops monitoring the specified entity
- **Parameter:**
  - `entity` — The entity to stop tracking
- **Behavior**: Removes the entity from monitoring

---

## Example Usage

```csharp
//Create a simple tag trigger
public class TagEntityTrigger : IEntityTrigger
{
    private Action<IEntity> _callback;

    public void SetAction(Action<IEntity> action) =>
        _callback = action ?? throw new ArgumentNullException(nameof(action));
    
    public void Track(IEntity entity) => 
        entity.OnTagAdded += _callback.Invoke;
    
    public void Untrack(IEntity entity) =>
         entity.OnTagAdded -= _callback.Invoke;
}
```