# 🧩 IPredicate

```csharp
public interface IPredicate : IFunction<bool>
```

- **Description:** Represents a <b>parameterless</b> predicate that returns a boolean result.
- **Inheritance:** [IFunction&lt;R&gt;](IFunction.md)

---

## 🏹 Methods

#### `Invoke()`

```csharp
public bool Invoke();
```

- **Description:** Evaluates the predicate and returns a boolean result.
- **Returns:** `true` or `false` based on the predicate logic.

---

## 🗂 Example of Usage

```csharp
public class IsGameActivePredicate : IPredicate
{
    private readonly GameManager _manager;

    public IsGameActivePredicate(GameManager manager) 
    {
        _manager = manager;  
    } 
    
    public bool Invoke() 
    {
        return _manager.IsActive;   
    } 
}
```

```csharp
GameManager gameManager = ...
IPredicate predicate = new IsGameActivePredicate(gameManager);
bool isActive = predicate.Invoke();
```
