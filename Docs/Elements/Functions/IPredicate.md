# 🧩 IPredicate

Represents a <b>parameterless</b> predicate that returns a boolean result.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Invoke()](#invoke)

---

## 🗂 Example of Usage

Below is an example of creating a predicate that checks activation of some GameManager instance:

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

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IPredicate : IFunction<bool>
```

- **Description:** Represents a <b>parameterless</b> predicate that returns a boolean result.
- **Inheritance:** [IFunction&lt;R&gt;](IFunction.md)

---

### 🏹 Methods

#### `Invoke()`

```csharp
public bool Invoke();
```

- **Description:** Evaluates the predicate and returns a boolean result.
- **Returns:** `true` or `false` based on the predicate logic.