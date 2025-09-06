# 🧩 EntityViewPoolBase

Base class for implementing a **pool of `EntityViewBase` instances**.  
Provides abstract methods to rent, return, and clear views.  
Inherit from this class to implement custom pooling logic.

> [!NOTE]  
> This class implements `IEntityViewPool` and can be attached to a Unity `GameObject` as a `Component`.

---

## EntityViewPoolBase
```csharp
public abstract class EntityViewPoolBase : MonoBehaviour, IEntityViewPool
```
- Base class for custom `EntityViewBase` pooling.
- Abstract methods must be implemented in derived classes.
- Inherits from `MonoBehaviour` for Unity integration.

---

## Methods

### Rent
```csharp
public abstract EntityViewBase Rent(string name);
```
- Retrieves a view instance from the pool associated with the specified name.
- If no available instance exists, a new one may be created.
- Parameter:
    - `name` – The name identifying the type of view to rent.
- Returns: An active `EntityViewBase` instance.
- Implements `IEntityViewPool.Rent` via explicit interface implementation.

### Return
```csharp
public abstract void Return(string name, EntityViewBase view);
```
- Returns a previously rented view instance back to the pool for reuse.
- Parameters:
    - `name` – The name identifying the type of view being returned.
    - `view` – The `EntityViewBase` instance to return to the pool.
- Implements `IEntityViewPool.Return` via explicit interface implementation.

### Clear
```csharp
public abstract void Clear();
```
- Clears all view instances from the pool.
- Releases any resources held.
- Must be implemented in derived classes.
