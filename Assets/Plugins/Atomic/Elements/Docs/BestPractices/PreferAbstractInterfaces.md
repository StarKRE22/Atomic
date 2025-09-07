# 📌 Prefer Interfaces to Concrete Classes 

Always use atomic interfaces such as `IValue`, `IVariable`, `ISignal`, etc., instead of concrete classes. This significantly improves the **maintainability** and **testability** of your project.

Following the **Dependency Inversion Principle** is especially important in multiplayer games, allowing developers to focus on **game logic** rather than networking code.

---

## 🗂 Example Usage

### ❌Bad Usage
```csharp
ReactiveVariable<Vector3> position = entity.GetPosition();
ReactiveVariable<Vector3> moveDirection = entity.GetMoveDirection();
Const<float> speed = entity.GetMoveSpeed();
position.Value += speed.Value * moveDirection.Value;
```

### ✅ Good Usage
```csharp
IVariable<Vector3> position = entity.GetPosition();
IValue<Vector3> moveDirection = entity.GetMoveDirection();
IValue<float> speed = entity.GetMoveSpeed();
position.Value += speed.Value * moveDirection.Value;
```

---
## 📝 Notes 

Using `Atomic.Elements` allows you to **abstract away from specific dependencies** like Unity and multiplayer frameworks such as Photon Fusion 2 or Mirror. This approach significantly improves **maintainability**, **testability**, and makes it easier to **port your project to other engines**.

We strongly recommend **maximizing abstraction from Unity wherever possible** to keep your game logic engine-agnostic.