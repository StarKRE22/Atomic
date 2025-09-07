# 📌 Request-Condition-Action-Event Flow

During the development of game mechanics using the atomic approach, I derived a convenient pattern that makes any mechanic highly flexible, observable, and safe. The `Request-Condition-Action-Event` pattern separates **intent**, **validation**, **execution**, and **notification**, making mechanics modular and robust.

---

## 💡 Key Components

1. **Request** — a deferred action, implemented via [IRequest](../Elements/Requests/IRequest.md).  
   Example: `MoveRequest`, `AttackRequest`, `JumpRequest` — triggered by player input or AI.

2. **Condition** — a logical check that determines if the requested action can be executed, often implemented with [AndExpression](../Elements/Expressions/AndExpression.md) or [IPredicate](../Elements/Functions/IPredicate.md).  
   Example: `IsGrounded`, `HasAmmo`, `CanJump`.

3. **Action** — executes the main effect if the condition passes, implemented via [IAction](../Elements/Actions/IAction.md).  
   Example: apply jump force, move the character, fire a weapon.

4. **Event** — triggers notifications or side effects after the action, implemented via [IEvent](../Elements/Events/IEvent.md).  
   Example: play animation, update UI, notify other systems.

---

## 🗂 Example of Usage

**A simple jump mechanic for an entity:**

```csharp
// Add jump mechanic
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

```csharp
// Jump controller that invokes Request-Condition-Action-Event flow
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