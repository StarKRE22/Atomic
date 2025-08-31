# 🏆 Prefer Abstract Interfaces

Always use abstract interfaces (`IValue`, `IVariable`, `ISignal`, etc.)  
instead of concrete classes. This is **especially important in multiplayer projects**,  
where decoupling logic from specific implementations helps prevent hard-to-debug issues.

---

## Why?

- Decouples your logic from specific implementations.
- Simplifies unit testing by allowing mocks and stubs.
- Makes your systems modular and reusable across multiple GameObjects or projects.
- Prevents tight coupling that can lead to network desyncs or unintended shared state in multiplayer.

---

## Example

```csharp
// Good: uses interfaces
void Move(
  IVariable<Vector3> position,
  IValue<Vector3> moveDirection,
  IValue<float> speed,
  float deltaTime 
) => position.Value += speed.Value * deltaTime * moveDirection.Value;


// Bad: depends on concrete implementation
void Move(
  ReactiveVariable<Vector3> position,
  BaseVariable<Vector3> moveDirection,
  Const<float> speed,
  float deltaTime 
) => position.Value += speed.Value * deltaTime * moveDirection.Value;
```
---
## Unity and Framework Dependencies

Avoid direct dependencies on Unity components or external frameworks whenever possible.  
Wrapping these systems behind interfaces makes your game logic **much easier to unit-test**,  
especially in multiplayer projects where deterministic and isolated behavior is critical.