# 📌 Using SerializeReference for LogAction

In Unity, **LogAction** is perfect for **visualizing the occurrence of an action**—for example, when temporarily
replacing a real action or using it inside a composite. It can also be easily serialized using `[SerializeReference]` in
a `MonoBehaviour` and configured in the inspector, making it convenient for debugging or testing action pipelines.

---

## 🗂 Example of Usage

Create a `PlayerActionTrigger` that executes an action **when triggered by the player**. The specific action can be
assigned by the designer directly in the **Inspector**.

```csharp
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

In the **Inspector**, we can assign the `LogAction` value to the `Action` parameter.

<img src="../Images/PlayerActionTrigger_PrintAction.png" alt="img.png" width="386" height="108">
