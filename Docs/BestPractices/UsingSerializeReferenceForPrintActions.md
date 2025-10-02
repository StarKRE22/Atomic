
## ❗️Using [SerializeReference]

In Unity, **PrintAction** is perfect for **visualizing the occurrence of an action**—for example, when temporarily
replacing a real action or using it inside a composite. It can also be easily serialized using `[SerializeReference]` in
a `MonoBehaviour` and configured in the inspector, making it convenient for debugging or testing action pipelines.

> [!WARNING]
> Using `[SerializeReference]` should be considered a last resort. If possible, define actions through code instead for
> clarity and maintainability, because `[SerializeReference]` is very fragile during refactoring.

### Example of Usage

Create a `PlayerActionTrigger` that executes an action **when triggered by the player**. The specific action can be
assigned by the designer directly in the **Inspector**.

```csharp
using UnityEngine;
using Atomic.Elements;

public sealed class PlayerActionTrigger : MonoBehaviour
{
    private const string PlayerTag = "Player";
    
    [SerializeReference] 
    private IAction _action;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(PlayerTag))
            _action.Invoke();
    }
}
```

In the **Inspector**, we can assign the `PrintAction` value to the `Action` parameter.

<img src="../../Images/PlayerActionTrigger_PrintAction.png" alt="img.png" width="386" height="108">
