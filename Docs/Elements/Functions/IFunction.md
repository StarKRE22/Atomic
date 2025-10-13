# 🧩 IFunction&lt;R&gt;

Represents a <b>parameterless</b> function that returns a result.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Invoke()](#invoke)

---

## 🗂 Example of Usage

Below is an example of implementing `IFunction<R>` interface for checking GameObject activation:

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

Usage:

```csharp
// Assume we have an instance of GameObject
GameObject gameObject = ...

// Create a new instance of the function
IFunction<bool> function = new IsGameObjectActiveFunction(gameObject);

// Invoke to get the result
bool activeSelf = function.Invoke();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IFunction<out R>
```

- **Description:** Represents a <b>parameterless</b> function that returns a result.
- **Type parameter:** `R` — the output result

---

### 🏹 Methods

#### `Invoke()`

```csharp
public R Invoke();
```

- **Description:** Invokes the function and returns the result
- **Returns:** The result of the function