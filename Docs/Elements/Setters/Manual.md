# 🧩 Setters

Provides interfaces and classes for **assigning values**. It offers a lightweight and consistent way to encapsulate
value assignment logic, enabling clean, reusable, and decoupled code for modifying data.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Basic Usage](#ex1)
    - [Controller Usage](#ex2)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Basic Usage

```csharp
// Assume we have an ISetter<Vector3> instance
ISetter<Vector3> moveDirection = ...;

// Assign the forward direction
moveDirection.Value = Vector3.forward;
```

---

<div id="ex2"></div>

### 2️⃣ Controller Usage

Below is an example of using `ISetter<Vector3>` inside a movement input controller. This
approach cleanly separates **input handling** from the **entity’s movement logic**, while relying only on the `ISetter`
interface.

```csharp
public sealed class MoveController
{
    private readonly ISetter<Vector3> _moveDirection;
    
    public MoveController(ISetter<Vector3> moveDirection)
    {
        _moveDirection = moveDirection;
    }
    
    public void Update()
    {
        float dx = Input.GetAxis("Horizontal");
        float dz = Input.GetAxis("Vertical");
        _moveDirection.Value = new Vector3(dx, 0, dz);
    }
}
```

---

## 🔍 API Reference

- [ISetter](ISetter.md) <!-- + -->
- [InlineSetter](InlineSetter.md) <!-- + -->

---

## 📌 Best Practices

- [Using Setters with Entities](../../BestPractices/UsingSetters.md)
