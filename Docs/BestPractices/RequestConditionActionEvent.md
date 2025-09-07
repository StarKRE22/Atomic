## 🏆 Request-Condition-Action-Event Pattern

A practical implementation of the **Request → Condition → Action → Event** pattern for gameplay systems.  
This pattern separates **intent**, **validation**, **execution**, and **notification**, making mechanics modular, testable, and safe for multiplayer scenarios.

---

### Components

1. **Request** — a deferred intention, e.g., a `BaseRequest` or `IRequest`.  
   Represents the player's or AI's desire to perform an action.

2. **Condition** — a boolean check that validates whether the request can be executed.  
   Example: `IsGrounded`, `HasAmmo`, `CanJump`.

3. **Action** — executes the main effect if the condition passes.  
   Example: apply jump force, move character, fire weapon.

4. **Event** — triggers notifications or side effects after the action.  
   Example: play animation, update UI, notify other systems.

---

### Example

```csharp
public sealed class JumpBehaviour
{
    private readonly IRequest _jumpRequest;       // BaseRequest
    private readonly IValue<bool> _jumpCondition; // Condition check
    private readonly IAction _jumpAction;         // InlineAction
    private readonly IEvent _jumpEvent;           // BaseEvent

    public JumpBehaviour(
        IRequest jumpRequest,
        IValue<bool> jumpCondition,
        IAction jumpAction,
        IEvent jumpEvent
    )
    {
        _jumpRequest = jumpRequest;
        _jumpCondition = jumpCondition;
        _jumpAction = jumpAction;
        _jumpEvent = jumpEvent;
    }

    public void Update()
    {
        // Process the jump only if requested and condition passes
        if (_jumpRequest.Consume() && _jumpCondition.Value)
        {
            _jumpAction.Invoke(); // Execute action
            _jumpEvent.Invoke();  // Trigger any linked events
        }
    }
}
```
> Using the Request-Condition-Action-Event pattern helps keep gameplay mechanics modular, unit-testable, and network-safe, especially in multiplayer projects.