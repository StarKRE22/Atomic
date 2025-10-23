# 📌 Using InlineFunctions with Entities

[InlineFunction](../Elements/Functions/InlineFunction.md) is ideal for creating **inline functions** using **lambda
expressions**. It allows developers to define **custom value computations** directly within entity setup — useful for
positions, rotations, calculations, reactive systems, and tests.

---

## 📑 Table of Contents

- [Example of Usage](#-examples-of-usage)
    - [Setting Transform via Rigidbody](#1-setting-transform-via-rigidbody)
    - [Setting Transform via Transform](#2-setting-transform-via-transform)
    - [Using Inline Functions in Code](#3-using-inline-functions-in-code)
- [Conclusion](#-conclusion)
- [Benefits](#-benefits)

---

## 🗂 Examples of Usage

### 1. Setting Position & Rotation via Rigidbody

```csharp
public class PhysicsTransformInstaller : SceneEntityInstaller
{
    [SerializeField]
    private Rigidbody _rigidbody;

    public void Install(IEntity entity)
    {
        entity.AddPosition(new InlineFunction<Vector3>(() => _rigidbody.position));
        entity.AddRotation(new InlineFunction<Quaternion>(() => _rigidbody.rotation));
    }
}
```

---

### 2. Setting Position & Rotation via Transform

```csharp
public class KinematicTransformInstaller : SceneEntityInstaller
{
    [SerializeField]
    private Transform _transform;

    public void Install(IEntity entity)
    {
        entity.AddPosition(new InlineFunction<Vector3>(() => _transform.position));
        entity.AddRotation(new InlineFunction<Quaternion>(() => _transform.rotation));
    }
}
```

---

### 3. Using Inline Functions in Code

```csharp
// Now it doesn’t matter where the object’s coordinates come from — it’s abstracted away
IFunction<Vector3> position = entity.GetPosition();
IFunction<Quaternion> rotation = entity.GetRotation();
```

> [!TIP]
> InlineFunction allows your entity logic to **remain agnostic** to the source of data — Rigidbody, Transform, or any
> custom computation.

---

## 🏁 Conclusion

- [InlineFunction](../Elements/Functions/InlineFunction.md) allows you to define **custom, inline value providers**
  directly within entity setup.
- Functions can be **parameterized, reactive, or computed dynamically**, providing flexibility in gameplay logic.
- This approach **abstracts away implementation details**, enabling components to work uniformly regardless of
  underlying data sources.
- Inline functions integrate seamlessly with the [Atomic.Entities](../Entities/Manual.md) framework, supporting 
  **positions, rotations, stats, or custom computations**.
- Promotes **concise, modular, and maintainable code** for game objects and reactive systems.

---

## ✅ Benefits

- Provides **flexible and reusable value computations** for entities.
- Keeps **gameplay logic decoupled** from concrete data sources.
- Simplifies **integration with multiple components** (e.g., Rigidbody, Transform).
- Supports **reactive and dynamic systems**, including AI, physics, and procedural content.
- Encourages **clean, maintainable code** by using lambda expressions directly within entity setup.

<!--

# 📌 Using Inline Functions

`InlineFunction` is ideal for creating functions for specific game objects using **lambda expressions**, making it
easy to define custom behavior inline for values, computations, reactive systems and tests.

Below is a demonstration of how to dynamically provide values for different types of transformations in
`AtomicEntities`:

### Setting transform via `Rigidbody`

```csharp
public class PhysicsTransformInstaller : SceneEntityInstaller
{
    [SerializeField]
    private Rigidbody _rigidbody;

    public void Install(IEntity entity)
    {
        entity.AddPosition(new InlineFunction<Vector3>(() => _rigidbody.position));
        entity.AddRotation(new InlineFunction<Quaternion>(() => _rigidbody.rotation));
    }
}
```

### Setting transform via `Transform`

```csharp
public class KinematicTransformInstaller : SceneEntityInstaller
{
    [SerializeField]
    private Transform _transform;

    public void Install(IEntity entity)
    {
        entity.AddPosition(new InlineFunction<Vector3>(() => _transform.position));
        entity.AddRotation(new InlineFunction<Quaternion>(() => _transform.rotation));
    }
}
```

### Usage in code

```csharp
// Now it doesn’t matter where the object’s coordinates come from — it’s abstracted away
IFunction<Vector3> position = entity.GetPosition();
IFunction<Quaternion> rotation = entity.GetRotation();
```

-->