# 🧩 IEntityViewPool

Defines a **pool for managing `EntityViewBase` instances**, allowing reuse of views to optimize memory and performance. Use this pool to rent and return views by name, reducing allocation overhead.

---

## IEntityViewPool
```csharp
public interface IEntityViewPool
```
- Manages pooling of `EntityViewBase` instances.
- Provides methods to rent, return, and clear views.

---

## Methods

### Rent
```csharp
IReadOnlyEntityView Rent(string name);
```
- Retrieves a view instance from the pool associated with the specified name.
- If no available instance exists, a new one may be created.
- Parameter:
    - `name` – The name identifying the type of view to rent.
- Returns: An active `EntityViewBase` instance.

### Return
```csharp
void Return(string name, IReadOnlyEntityView view);
```
- Returns a previously rented view instance back to the pool for reuse.
- Parameters:
    - `name` – The name identifying the type of view being returned.
    - `view` – The `EntityViewBase` instance to return to the pool.

### Clear
```csharp
void Clear();
```
- Clears all view instances from the pool.
- Releases any resources held by the pool.
