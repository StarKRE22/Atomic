# 📌 Using SerializeReference for CompositeActions

For **narrative or scenario-driven games**, where designers need to configure a lot of actions directly on the scene,
`CompositeAction` combined with `[SerializeReference]` is very convenient. It allows designers to visually chain
multiple actions in the inspector without writing extra code. This is especially useful for quickly iterating on game
logic or events.

---

## 🗂 Example of Usage

#### 1. Create an Action Trigger

Create a component that executes an action **when triggered by the player**. The specific action can be assigned by the
designer directly in the **Inspector**.

```csharp
using UnityEngine;
using Atomic.Elements;

public sealed class PlayerActionTrigger : MonoBehaviour
{
    private const string PlayerTag = "Player";
    
    [SerializeReference] private IAction _action;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(PlayerTag))
            _action.Invoke();
    }
}
```

#### 2. Add `PlayerActionTrigger` to a GameObject

<img src="../Images/PlayerActionTrigger_Composite.png" alt="Inspector setup example" width="390" height="164">

#### 3. Assign Composite Action

In the **Inspector**, we can assign the [CompositeAction]() value to the `Action` parameter. For example, we can
add [PrintAction](PrintAction.md) to the action array.