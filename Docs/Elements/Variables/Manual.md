# 🧩 Variables

Provides a set of interfaces and classes for working with **reactive variables, proxy variables, and Unity-specific
variable types**. It builds on the concept of reactive values but adds more flexibility by allowing variables to act as
intermediaries or proxies, which can observe, modify, or synchronize underlying data.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Variable\<T>](#ex1)
    - [ReactiveVariable\<T>](#ex2)
    - [InlineVariable\<T>](#ex3)
- [API Reference](#-api-reference)
- [Specialized Types](#-specialized-types)
    - [Base Variables](#-base-variables)
    - [Reactive Variables](#-reactive-variables)
    - [Proxy Variables](#-proxy-variables)
    - [Thread-Safe Variables](#-thread-safe-variables)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Variable\<T>

```csharp
 // Create a new variable
IVariable<int> score = new Variable<int>(10);

// Read value
Console.WriteLine(score.Value);  // Output: 10

// Write value
score.Value = 20;
Console.WriteLine(score.Value);  // Output: 20
```

<div id="ex2"></div>

### 2️⃣ ReactiveVariable\<T>

```csharp
// Initialize with a starting value
var score = new ReactiveVariable<int>(10);

// Subscribe to changes
score.Subscribe(newValue => Console.WriteLine("Score updated: " + newValue));

// Change the value
score.Value = 20; // Triggers subscription callback

 // Dispose to clear subscriptions
score.Dispose();
```

<div id="ex3"></div>

### 3️⃣ InlineVariable\<T>

```csharp
//Create a new proxy of Transform.position
IVariable<Vector3> position = new InlineVariable<Vector3>(
    getter: () => transform.position,
    setter: value => transform.position = value
);

//Move position:
position.Value += Vector3.forward; 
```

---

## 🔍 API Reference

- **Variables**
    - [IVariable&lt;T&gt;](IVariable.md) <!-- + -->
    - [Variable&lt;T&gt;](BaseVariable.md) <!-- + -->
- **ReactiveVariables**
    - [IReactiveVariable&lt;T&gt;](IReactiveVariable.md) <!-- + -->
    - [ReactiveVariable&lt;T&gt;](ReactiveVariable.md) <!-- + -->
- **InlineVariables**
    - [InlineVariable&lt;T&gt;](ProxyVariable.md) <!-- + -->
      - [Builder](ProxyVariableBuilder.md)
    - [InlineReactiveVariable&lt;T&gt;](ReactiveProxyVariable.md)  <!-- + -->
      - [Builder](ReactiveProxyVariableBuilder.md)
- **ThreadSafeVariables**
    - [ThreadSafeVariable&lt;T&gt;](ThreadSafeVariable.md) 
    - [ThreadSafeReactiveVariable&lt;T&gt;](ThreadSafeReactiveVariable.md)
- [Extensions](Extensions.md)

---

<div id="-specialized-types"></div>

## 🏛️ Specialized Types

### 🧩 Base Variables

For convenience, several specialized implementations of base variables are provided. It is recommended to use them, as
they compare values without relying on `EqualityComparer`, which makes them slightly faster than the generic
[Variable&lt;T&gt;](BaseVariable.md) version.

- **Common**
    - `BoolVariable` — Boolean variable
    - `IntVariable` — Integer variable
    - `FloatVariable` — Float variable
- **Unity**
    - `QuaternionVariable` — Stores a Quaternion
    - `Vector2Variable` — Stores a Vector2
    - `Vector3Variable` — Stores a Vector3
    - `Vector4Variable` — Stores a Vector4
    - `Vector2IntVariable` — Stores a Vector2Int
    - `Vector3IntVariable` — Stores a Vector3Int

---

### 🧩 Reactive Variables

For convenience, several specialized implementations of reactive variables are provided. It is recommended to use them,
as they compare values without relying on `EqualityComparer`, which makes them slightly faster than the generic
[ReactiveVariable&lt;T&gt;](ReactiveVariable.md) version.

- **Common**
    - `ReactiveBool` — Boolean reactive variable
    - `ReactiveInt` — Integer reactive variable
    - `ReactiveFloat` — Float reactive variable
- **Unity**
    - `ReactiveQuaternion` — Stores a Quaternion
    - `ReactiveVector2` — Stores a Vector2
    - `ReactiveVector3` — Stores a Vector3
    - `ReactiveVector4` — Stores a Vector4
    - `ReactiveVector2Int` — Stores a Vector2Int
    - `ReactiveVector3Int` — Stores a Vector3Int

---

### 🧩 Proxy Variables

For convenience, several specialized proxy variable implementations are provided.

- **Player Prefs**
    - `BoolPrefsVariable` — Boolean variable stored in PlayerPrefs
    - `IntPrefsVariable` — Integer variable stored in PlayerPrefs
    - `FloatPrefsVariable` — Float variable stored in PlayerPrefs
    - `StringPrefsVariable` — String variable stored in PlayerPrefs
- **Transform**
    - `TransformParentVariable` — Stores a Transform parent reference
    - `TransformPositionVariable` — Stores a Vector3 position
    - `TransformRotationVariable` — Stores a Quaternion rotation
    - `TransformScaleVariable` — Stores a Vector3 scale

---

### 🧩 Thread-Safe Variables

For multi-threaded scenarios, thread-safe variable implementations are provided that protect their internal state with locking and dispatch events on the main thread.

- **Thread-Safe Variables**
    - `ThreadSafeVariable<T>` — Thread-safe value container without change notifications
    - `ThreadSafeReactiveVariable<T>` — Thread-safe reactive variable with main-thread event dispatch
