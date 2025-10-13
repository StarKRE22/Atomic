# 🧩 IFunction&lt;T, R&gt;

Represents a function with <b>one input argument</b> that returns a result.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Invoke(T)](#invoket)

---

## 🗂 Example of Usage

Below is an example of using this interface for enemy checking:

```csharp
public sealed class IsEnemyFunction : IFunction<Character, bool>
{
    private readonly Character _source;
    
    public IsEnemyFunction(Character source) 
    {
        _source = source;  
    } 
    
    public bool Invoke(Character other) 
    {
        return _source.Team != other.Team; 
    } 
}
```

Usage:

```csharp
// Assume we have two instances of some characters 
Character myCharacter, otherCharacter = ...

// Create our function
IFunction<Character, bool> func = new IsEnemyFunction(myCharacter);

// Get result
bool isEnemies = func.Invoke(otherCharacter);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IFunction<in T, out R>
```

- **Description:** Represents a function with <b>one input argument</b> that returns a result.
- **Type parameters:**
    - `T` — the input argument type
    - `R` — the return type

---

### 🏹 Methods

#### `Invoke(T)`

```csharp
public R Invoke(T arg);
```

- **Description:** Executes the function with the specified input argument.
- **Parameter:** `arg` — the input argument.
- **Returns:** The result of type `R`.