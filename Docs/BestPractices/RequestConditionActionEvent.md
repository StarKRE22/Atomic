# 📌 Request-Condition-Action-Event (RCAE) Flow

When developing game mechanics using an **atomic approach**, I found the `Request-Condition-Action-Event` pattern
extremely useful.
It separates **intent**, **validation**, **execution**, and **notification**, making mechanics **modular, flexible,
observable, and safe**.

---

## 📑 Table of Contents

- [Key Components](#-key-components)
- [Example of Usage](#-example-of-usage)
- [Summary](#-summary)

---

## 💡 Key Components

1. **Request**
    - Represents a **deferred action**.
    - Typically implemented via [IRequest](../Elements/Requests/IRequest.md).
    - Example: `MoveRequest`, `AttackRequest`, `JumpRequest` — triggered by player input or AI.

2. **Condition**
    - A **logical check** that determines if the request can be executed.
    - Often implemented via [AndExpression](../Elements/Expressions/AndExpression.md) or
      [IPredicate](../Elements/Functions/IPredicate.md).
    - Example: `IsGrounded`, `HasAmmo`, `CanJump`.

3. **Action**
    - Executes the **main effect** if the condition passes.
    - Typically implemented via [IAction](../Elements/Actions/IAction.md).
    - Example: apply jump force, move the character, fire a weapon.

4. **Event**
    - Triggers **notifications or side effects** after the action.
    - Typically implemented via [IEvent](../Elements/Events/IEvent.md).
    - Example: play animation, update UI, notify other systems.

---

## 🗂 Example of Usage

Below is an example of usage the RCAE flow for a jump mechanics:  

1. Adding a jump mechanic to an entity:

```csharp
var entity = ...;

entity.AddJumpRequest(new BaseRequest());
entity.AddJumpCondition(new AndExpression(
   entity.GetHealth().Exists,
   entity.GetEnergy().Exists
));
entity.AddJumpAction(new InlineAction(
    () => entity.GetRigidbody().AddForce(Vector3.up, ForceMode.Impulse)
));
entity.AddJumpEvent(new BaseEvent());

entity.AddBehaviour<JumpBehaviour>();
```

2. Jump controller that executes the RCAE flow:

```csharp
public sealed class JumpBehaviour : IEntityInit, IEntityFixedTick
{
    private IRequest _jumpRequest;       
    private IFunction<bool> _jumpCondition;
    private IAction _jumpAction;         
    private IEvent _jumpEvent;

    public void Init(IEntity entity)
    {
        _jumpRequest = entity.GetJumpRequest();
        _jumpCondition = entity.GetJumpCondition();
        _jumpAction = entity.GetJumpAction();
        _jumpEvent = entity.GetJumpEvent();
    }

    public void FixedTick(IEntity entity, float deltaTime)
    {
        // Execute jump if requested and condition passes
        if (_jumpRequest.Consume() && _jumpCondition.Value)
        {
            _jumpAction.Invoke();
            _jumpEvent.Invoke();
        }
    }
}
```

---

## ✅ Summary

This pattern ensures:

- **Modularity** — each component (Request, Condition, Action, Event) can be reused independently.
- **Safety** — conditions prevent invalid actions.
- **Observability** — events make mechanics easy to monitor and react to.
- **Flexibility** — new mechanics can be added without changing existing systems.