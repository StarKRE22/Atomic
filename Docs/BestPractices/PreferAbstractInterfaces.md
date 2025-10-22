# 📌 Prefer Atomic Interfaces to Concrete Classes

## 📑 Table of Contents

- [Overview](#-overview)
- [Example of Usage](#-example-of-usage)
- [Why This Matters](#why-this-matters)
- [Notes](#-notes)

---

## 📖 Overview

When developing with [Atomic.Elements](../Elements/Manual.md), always prefer using **atomic interfaces** such
as [IValue](../Elements/Values/IValue.md), [IVariable](../Elements/Variables/IVariable.md), [ISignal](../Elements/Events/ISignal.md),
etc., instead of concrete implementations. This practice greatly enhances **maintainability**, **testability**, and *
*scalability** of your project.

Following
the [Dependency Inversion Principle](https://en.wikipedia.org/wiki/Dependency_inversion_principle) is
especially crucial in **multiplayer environments**, allowing developers to focus on **game logic** rather than low-level
networking details.

---

## 🗂 Example of Usage

### ❌ Incorrect

```csharp
ReactiveVariable<Vector3> position = ...;
ReactiveVariable<Vector3> moveDirection = ...;
Const<float> speed = ...;
```

### ✅ Correct

```csharp
IVariable<Vector3> position = ...;
IValue<Vector3> moveDirection = ...;
IValue<float> speed = ...;
```

---

## Why This Matters

By programming against **interfaces**, you achieve:

- **Flexibility** – easy to swap implementations without touching game logic.
- **Testability** – simple to mock or stub dependencies during unit testing.
- **Decoupling** – your logic becomes independent of Unity or specific networking frameworks.
- **Portability** – easier to migrate to other engines or platforms.

---

## 📝 Notes

Using [Atomic.Elements](../Elements/Manual.md) allows you to **abstract away from Unity dependencies**
and external multiplayer frameworks like **Photon Fusion 2** or **Mirror**.

This approach significantly improves **code longevity** and **cross-platform flexibility**,
keeping your **game logic engine-agnostic** and future-proof.

> 💡 **Tip:** Always maximize abstraction from Unity-specific classes whenever possible —
> it keeps your systems cleaner, modular, and ready for reuse in any context.
