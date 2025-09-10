# 🧩 Predicate Interfaces

The **IPredicate** interfaces are specialized forms of [Functions](IFunction.md) that evaluate a condition and return `true` or `false`, useful in filtering, validation, and decision-making logic.

---

## 🧩 IPredicate
```csharp
public interface IPredicate : IFunction<bool>
```
- **Description:** Represents a **parameterless predicate** that returns a boolean result.

### Methods

#### `Invoke()`
```csharp
bool Invoke();
```
- **Description:** Evaluates the predicate and returns a boolean result.
- **Returns:** `true` or `false` based on the predicate logic.

### 🗂 Example of Usage
```csharp
public class IsGameActivePredicate : IPredicate
{
    private readonly GameManager _manager;

    public IsGameActivePredicate(GameManager manager) => _manager = manager;
    
    public bool Invoke() => _manager.IsActive;
}
```
```csharp
IPredicate predicate = new IsGameActivePredicate(gameManager);
bool isActive = predicate.Invoke();
```
---

## 🧩 IPredicate&lt;T&gt;
```csharp
public interface IPredicate<in T> : IFunction<T, bool>
```
- **Description:** Represents a predicate with **one input argument**.
- **Type parameter:** `T` — the input argument type.

### Methods

#### `Invoke(T)`
```csharp
bool Invoke(T arg);
```
- **Description:** Evaluates the predicate with the specified argument.
- **Parameter:** `arg` — the input argument.
- **Returns:** `true` or `false` based on the predicate logic.

### 🗂 Example of Usage
```csharp
public class IsEnemyPredicate : IPredicate<Character>
{
    private readonly Character _source;

    public IsEnemyPredicate(Character source) => _source = source;
    
    public bool Invoke(Character other) => _source.Team != other.Team;
}
```
```csharp
IPredicate<Character> predicate = new IsEnemyPredicate(character);
bool isEnemy = predicate.Invoke(otherCharacter);
```
---

## 🧩 IPredicate&lt;T1, T2&gt;
```csharp
public interface IPredicate<in T1, in T2> : IFunction<T1, T2, bool>
```
- **Description:** Represents a predicate with **two input arguments**.
- **Type parameters:**
    - `T1` — the first input argument type
    - `T2` — the second input argument type

### Methods

#### `Invoke(T1, T2)`
```csharp
bool Invoke(T1 arg1, T2 arg2);
```
- **Description:** Evaluates the predicate with the specified arguments.
- **Parameters:**
    - `arg1` — the first input argument
    - `arg2` — the second input argument
- **Returns:** `true` or `false` based on the predicate logic.

### 🗂 Example of Usage
```csharp
public class AreAlliesPredicate : IPredicate<Character, Character>
{
    public bool Invoke(Character a, Character b) => a.Team == b.Team;
}
```
```csharp
IPredicate<Character, Character> predicate = new AreAlliesPredicate();
bool areAllies = predicate.Invoke(characterA, characterB);
```