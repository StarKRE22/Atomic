# 🧩 Functions

Provides a set of abstractions for defining logic that **returns a value** and can accept varying numbers of input
parameters. These function types are lightweight and flexible, making them ideal for **callbacks, computations, or
functional programming patterns**. They allow developers to encapsulate reusable logic, implement predicates, and create
inline or composable functions for clean and maintainable code.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Basic Usage](#ex-1)
    - [Inline Function](#ex-2)
    - [Predicate with single argument](#ex-3)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

<div id="ex-1"></div>

### 1️⃣ Basic Usage

Below is an example of implementing [IFunction\<R>](IFunction.md) interface:
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

// Create a new instance of the function:
IFunction<bool> function = new IsGameObjectActiveFunction(gameObject);

// Get result:
bool activeSelf = function.Invoke();
```

---

<div id="ex-2"></div>

### 2️⃣ Inline Function

This example demonstrates built-in function using [InlineFunction\<R>](InlineFunction.md) class:

```csharp
// Assume we have an instance of GameObject
GameObject gameObject = ...
    
// Create a new instance of the function:
IFunction<bool> function = new InlineFunction<bool>(
    () => gameObject.activeSelf
);

// Get result:
function.Invoke();
```

---

<div id="ex-3"></div>

### 3️⃣ Predicate with single argument

For functions that return a boolean result you can use [IPredicate](IPredicates.md) interfaces:

```csharp
public class IsEnemyPredicate : IPredicate<Character>
{
    private readonly Character _source;

    public IsEnemyPredicate(Character source) => _source = source;
    
    public bool Invoke(Character other) => _source.Team != other.Team;
}
```

---

## 🔍 API Reference

Below are interfaces and their implementations, depending on the concrete scenario:

<details>
  <summary><a href="IFunctions.md">IFunctions</a></summary>
  <ul>
    <li><a href="IFunction.md">IFunction&lt;R&gt;</a></li>
    <li><a href="IFunction%601.md">IFunction&lt;T, R&gt;</a></li>
    <li><a href="IFunction%602.md">IFunction&lt;T1, T2, R&gt;</a></li>
  </ul>
</details>

<details>
  <summary><a href="IPredicates.md">IPredicates</a></summary>
  <ul>
    <li><a href="IPredicate.md">IPredicate</a></li>
    <li><a href="IPredicate%601.md">IPredicate&lt;T&gt;</a></li>
    <li><a href="IPredicate%602.md">IPredicate&lt;T1, T2&gt;</a></li>
  </ul>
</details>

<details>
  <summary><a href="InlineFunctions.md">InlineFunctions</a></summary>
  <ul>
    <li><a href="InlineFunction.md">InlineFunction&lt;R&gt;</a></li>
    <li><a href="InlineFunction%601.md">InlineFunction&lt;T, R&gt;</a></li>
    <li><a href="InlineFunction%602.md">InlineFunction&lt;T1, T2, R&gt;</a></li>
  </ul>
</details>

<details>
  <summary><a href="InlinePredicates.md">InlinePredicates</a></summary>
  <ul>
    <li><a href="InlinePredicate.md">InlinePredicate</a></li>
    <li><a href="InlinePredicate%601.md">InlinePredicate&lt;T&gt;</a></li>
    <li><a href="InlinePredicate%602.md">InlinePredicate&lt;T1, T2&gt;</a></li>
  </ul>
</details>

<details>
  <summary>Extensions</summary>
  <ul>
    <li><a href="ExtensionsInvert.md">Invert</a></li>
    <li><a href="ExtensionsCollections.md">Collections</a></li>
  </ul>
</details>

---

## 📌 Best Practices

- [Using InlineFunctions with Entities](../../BestPractices/UsingInlineFunctions.md)