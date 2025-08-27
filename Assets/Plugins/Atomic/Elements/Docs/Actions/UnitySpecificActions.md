# Unity-Specific Actions

`Atomic.Elements` provides specialized action classes designed for Unity workflows, making debugging, prototyping, and scene composition easier for game developers and designers.

## 🔹 UnityLogAction

`UnityLogAction` is a simple `IAction` implementation that logs messages to the Unity console.  
Useful for:

- Quick debugging of event flows or game logic.
- Visualizing when certain actions are invoked without needing to attach a MonoBehaviour.
- Testing sequences in a scene without affecting game objects.

**Example Usage:**

```csharp
var logAction = new UnityLogAction("Player has jumped!");
logAction.Invoke(); // Logs: "Player has jumped!" in the Unity console
```

## 🔹 SceneAction
SceneAction is a MonoBehaviour that represents actions that are designed to be placed and ordered in the Unity scene, allowing designers to visually control the sequence of events.
Ideal for:
- Level design workflows.
- Cutscene or gameplay event sequencing.
- Easily configurable in the Unity Inspector.

> **Note:** `SceneAction` uses `[SerializeReference]` for proper serialization of polymorphic actions in the Unity Inspector.  
> This requires **Odin Inspector** to work correctly.