# 🧩 Values

Provides a set of interfaces and classes for working with **reactive values and constants**. It allows developers to
handle both immutable constants and dynamic, reactive data in a consistent way. This is particularly useful in scenarios
where values need to be observed or updated in real-time, such as game development, UI bindings, or simulation systems.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [IValue\<T>](#ex1)
    - [IReactiveValue\<T>](#ex2)
    - [Const\<T>](#ex3)
    - [ScriptableConst\<T>](#ex4)
- [API Reference](#-api-reference)
- [Default Consts](#default-consts)
  - [Boolean](#-boolean-constants)
  - [Mathematical](#-mathematical-constants)
  - [Time](#-time-constants)
  - [Common](#-common-values)
  - [Physics](#-physics-constants)
  - [Vectors](#-unity-specific-vectors)
  - [Colors](#-unity-specific-colors)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ IValue\<T>

For example, create a class that wraps transform position implementing [IValue\<T>](IValue.md) interface:

```csharp
public class TransformPositionProvider : IValue<Vector3>
{
    private readonly Transform _transform;
    
    public Vector3 Value => _transform.position;
    
    public TransformPositionProvider(Transform transform) 
    {
        _transform = transform ?? throw new ArgumentNullExeption(nameof(transform));
    }
}
```

Usage:

```csharp
// Assume we have a transform instance
Transform transform = ...;

// Create a new instance of position wrapper
IValue<Vector3> position = new TransformPositionProvider(transform);

// Get value
float result = movementSpeed.Value;
```

---

<div id="ex2"></div>

### 2️⃣ IReactiveValue\<T>

Also, you can use [IReactiveValue\<T](IReactiveValue.md) interface for detect changes:

```csharp
// Assume we have an instance of IReactiveValue .;
IReactiveValue<Vector3> moveDirection = ...;

// Subscribe on value changing
moveDirection.OnEvent += value => 
    Console.WriteLine($"Move Direction changed {value}")

// Get current value
float result = movementSpeed.Value;
```

---

<div id="ex3"></div>

### 3️⃣ Const\<T>

The example below demonstrates **shared movement speed** across multiple characters using [Const\<T>](Const.md):

```csharp
//Shared config
public sealed class CharacterConfig : ScriptableObject
{
    [SerializeField] 
    public Const<float> moveSpeed = 5.0f;
}
```

```csharp
//Many instances
public sealed class Character : MonoBehaviour
{
    [SerializeField] 
    private CharacterConfig _config;

    public void MoveStep(Vector3 direction, float deltaTime) 
    {
        this.transform.position += direction * (_config.moveSpeed.Value * deltaTime);
    }
}
```

---

<div id="ex4"></div>

### 4️⃣ ScriptableConst\<T>

The example below demonstrates how a speed parameter can be **shared across multiple characters** using
[ScriptableConst\<T>](ScriptableConst.md):

```csharp
[CreateAssetMenu(
    fileName = "FloatConst",
    menuName = "Game/Elements/FloatConst"
)]
public sealed class FloatScriptableConst : ScriptableConst<float>
{
}
```

```csharp
public sealed class Character : MonoBehaviour
{
    [SerializeField] 
    private FloatScriptableConst _moveSpeed;

    public void MoveStep(Vector3 direction, float deltaTime) 
    {
        this.transform.position += direction * (_moveSpeed.Invoke() * deltaTime);
    }
}
```

---

## 🔍 API Reference

- [IValue&lt;T&gt;](IValue.md) <!-- + -->
- [IReactiveValue&lt;T&gt;](IReactiveValue.md)  <!-- + -->
- [Const&lt;T&gt;](Const.md) <!-- + -->
- [ScriptableConst&lt;T&gt;](ScriptableConst.md)  <!-- + -->
- [Extensions](Extensions.md)  <!-- + -->

---

## Default Consts

The **Default Constants** collection provides a centralized set of commonly used values across multiple domains,
including Boolean logic, mathematics, time, physics, and Unity-specific utilities.

These constants are designed to improve **code readability**, **reduce magic numbers**, and ensure **consistency**
throughout your projects.

---

### 🔷 Boolean Constants

| Name    | Value   | Description        |
|---------|---------|--------------------|
| `True`  | `true`  | Represents `true`  |
| `False` | `false` | Represents `false` |

---

### 🔷 Mathematical Constants

| Name          | Value      | Description                         |
|---------------|------------|-------------------------------------|
| `PI`          | 3.1415927f | π (pi)                              |
| `TwoPI`       | 2 * PI     | 2π, for circular math               |
| `HalfPI`      | PI / 2     | π/2, for trigonometry               |
| `E`           | 2.7182818f | Euler's number                      |
| `GoldenRatio` | 1.6180339f | Golden ratio                        |
| `Deg2Rad`     | 0.01745    | Degrees to radians (Unity specific) |
| `Rad2Deg`     | 57.2958    | Radians to degrees (Unity specific) |

---

### 🔷 Time Constants

| Name             | Value    | Description           |
|------------------|----------|-----------------------|
| `Second`         | 1f       | One second            |
| `Minute`         | 60f      | One minute in seconds |
| `Hour`           | 3600f    | One hour in seconds   |
| `FrameTime60FPS` | 1f / 60f | Frame time at 60 FPS  |

---

### 🔷 Common Values

| Name          | Value | Description        |
|---------------|-------|--------------------|
| `ZeroInt`     | 0     | Integer zero       |
| `OneInt`      | 1     | Integer one        |
| `Zero`        | 0f    | Float zero         |
| `One`         | 1f    | Float one          |
| `NegativeOne` | -1f   | Float negative one |
| `Half`        | 0.5f  | Float one half     |

---

### 🔷 Physics Constants

| Name           | Value | Description               |
|----------------|-------|---------------------------|
| `GravityEarth` | 9.81f | Standard gravity on Earth |
| `DefaultMass`  | 1f    | Default mass              |

---

### 🔷 Unity-Specific Vectors

| Name         | Value    | Description         |
|--------------|----------|---------------------|
| `Up`         | (0,1,0)  | Unit vector up      |
| `Down`       | (0,-1,0) | Unit vector down    |
| `Left`       | (-1,0,0) | Unit vector left    |
| `Right`      | (1,0,0)  | Unit vector right   |
| `Forward`    | (0,0,1)  | Unit vector forward |
| `Back`       | (0,0,-1) | Unit vector back    |
| `ZeroVector` | (0,0,0)  | Zero vector         |
| `OneVector`  | (1,1,1)  | One vector          |

---

### 🔷 Unity-Specific Colors

| Name          | Value     | Description       |
|---------------|-----------|-------------------|
| `White`       | (1,1,1,1) | White color       |
| `Black`       | (0,0,0,1) | Black color       |
| `Red`         | (1,0,0,1) | Red color         |
| `Green`       | (0,1,0,1) | Green color       |
| `Blue`        | (0,0,1,1) | Blue color        |
| `Transparent` | (0,0,0,0) | Fully transparent |

---

## 📌 Best Practices

- [Prefer Interfaces to Concrete Classes](../../BestPractices/PreferAbstractInterfaces.md)
- [Flyweight Pattern for Constants](../../BestPractices/SharedConstants.md)
- [Constants for Boolean Expressions](../../BestPractices/UsingConstantsWithAndExpressions.md)
- [Observe Extension for Reactive Values](../../BestPractices/UsingObserveWithReactiveValues.md)