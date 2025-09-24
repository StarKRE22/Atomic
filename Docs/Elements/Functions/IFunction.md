# 🧩 IFunction&lt;R&gt;

```csharp
public interface IFunction<out R>
```

- **Description:** Represents a <b>parameterless</b> function that returns a result.
- **Type parameter:** `R` — the output result

---

## 🏹 Methods

#### `Invoke()`

```csharp
public R Invoke();
```

- **Description:** Invokes the function and returns the result
- **Returns:** The result of the function

---

## 🗂 Example of Usage

```csharp
public class IsGameObjectActiveFunction : IFunction<bool>
{
    private readonly GameObject _go;
    
    public IsGameObjectActiveFunction(GameObject go) 
    {
        _go = go;
    }
    
    public bool Invoke() 
    {
        return _go.activeSelf;
    } 
}

```

```csharp
GameObject gameObject = ...
IFunction<bool> function = new IsGameObjectActiveFunction(gameObject);
bool activeSelf = function.Invoke();
```