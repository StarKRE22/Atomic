# 🧩 IFunctions

The **IFunction** interfaces define a family of contracts for representing functions with varying numbers of input
parameters. They provide a lightweight abstraction for defining logic that returns a value, making them useful for
callbacks, computations, and functional programming patterns.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Function without arguments](#ex-1)
    - [Function with one argument](#ex-2)
    - [Function with two arguments](#ex-3)
- [API Reference](#-api-reference)

---

## 🗂 Examples of Usage

<div id="ex-1"></div>

### 1️⃣ Function without arguments

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

<div id="ex-2"></div>

### 2️⃣ Function with one argument

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

<div id="ex-3"></div>

### 3️⃣ Function with two arguments

```csharp
public class SumFunction : IFunction<int, int, int>
{
    public int Invoke(int a, int b) 
    {
        return a + b; 
    } 
}
```

---

## 🔍 API Reference

There are several interfaces of functions, depending on the number of arguments they take:

- [IFunction&lt;R&gt;](IFunction.md) — Function without parameters.
- [IFunction&lt;T, R&gt;](IFunction%601.md) — Function that takes one argument.
- [IFunction&lt;T1, T2, R&gt;](IFunction%602.md) — Function that takes two arguments.