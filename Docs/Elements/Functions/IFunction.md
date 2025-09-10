#  🧩 IFunction  Interfaces

The **IFunction** interfaces define a family of contracts for representing functions with varying numbers of input parameters. They provide a lightweight abstraction for defining logic that returns a value, making them useful for callbacks, computations, and functional programming patterns.

---

## 🧩 IFunction&lt;R&gt;
```csharp
public interface IFunction<out R>
```
- **Description:** Represents a **parameterless function that returns a result**.
- **Type parameter:** `R` — the output result

### Methods

#### `Invoke()`

```csharp
R Invoke();
```
- **Description:** Invokes the function and returns the result
- **Returns:** The result of the function

### 🗂 Example of Usage

```csharp
public class IsGameObjectActiveFunction : IFunction<bool>
{
    private readonly GameObject _go;
    
    public IsGameObjectActiveFunction(GameObject go) => _go = go;
    
    public bool Invoke() => _go.activeSelf;
}

```
```csharp
IFunction<bool> function = new IsGameObjectActiveFunction(gameObject);
function.Invoke();
```

---

## 🧩 IFunction&lt;T, R&gt;
```csharp
public interface IFunction<in T, out R>
```
- **Description:** Represents a function with **one input argument** that returns a result.
- **Type parameters:**
    - `T` — the input argument type
    - `R` — the return type

### Methods

#### `Invoke(T)`
```csharp
R Invoke(T arg);
```
- **Description:** Executes the function with the specified input argument.
- **Parameter:** `arg` — the input argument.
- **Returns:** The result of type `R`.

### 🗂 Example of Usage

```csharp
public sealed class IsEnemyFunction : IFunction<Character, bool>
{
    private readonly Character _source;
    
    public IsEnemyFunction(Character source) => _source = source;
    
    public bool Invoke(Character other) => _source.Team != other.Team;
}
```
```csharp
IFunction<Character, bool> func = new IsEnemyFunction(character);
bool isEnemies = func.Invoke(otherCharacter);
```

## 🧩 IFunction&lt;T1, T2, R&gt;

```csharp
public interface IFunction<in T1, in T2, out R>
```
- **Description:** Represents a function with **two input arguments** that returns a result.
- **Type parameters:**
    - `T1` — the first input argument type
    - `T2` — the second input argument type
    - `R` — the return type

### Methods

#### `Invoke(T1, T2)`
```csharp
R Invoke(T1 arg1, T2 arg2);
```
- **Description:** Executes the function with the specified input arguments.
- **Parameters:**
    - `arg1` — the first input argument
    - `arg2` — the second input argument
- **Returns:** The result of type `R`.

### 🗂 Example of Usage

```csharp
public class SumFunction : IFunction<int, int, int>
{
    public int Invoke(int a, int b) => a + b;
}
```
```csharp
IFunction<int, int, int> func = new SumFunction();
int sum = func.Invoke(3, 4); // sum = 7
```