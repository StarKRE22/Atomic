# 🧩 CompositeActions

Represent **groups of actions** that implement the corresponding [IAction](IActions.md)
interfaces. Its follow the [Composite Pattern](https://en.wikipedia.org/wiki/Composite_pattern) — an action both **groups actions**
and itself **acts as a single action**, preserving a uniform interface.
This allows combining multiple actions into a sequence, which will be invoked **sequentially** when triggered. This is
especially important when game objects and scripts need to execute complex action scenarios.

There are several implementations of composite actions, depending on the number of arguments the actions take:

- [CompositeAction](CompositeAction.md) — Non-generic version; works without parameters.
- [CompositeAction&lt;T&gt;](CompositeAction%601.md) — Holds actions that take one argument.
- [CompositeAction&lt;T1, T2&gt;](CompositeAction%602.md) — Holds actions that take two arguments .
- [CompositeAction&lt;T1, T2, T3&gt;](CompositeAction%603.md) — Holds actions that take three arguments.
- [CompositeAction&lt;T1, T2, T3, T4&gt;](CompositeAction%604.md) — Holds actions that take four arguments.

---

## 🗂 Example of Usage

Below is example of using composite action for game startup:

```csharp
var startupAction = new CompositeAction(
    new ActivatePlayerAction(),
    new ActivateEnemiesAction(),
    new ActivateWeapons(),
    new ActivateGameTimerAction(),
);

```

```csharp
//Usage
startupAction.Invoke();
```

---

## ❗️Using [SerializeReference]

For **narrative or scenario-driven games**, where designers need to configure a lot of actions directly on the scene,
`CompositeAction` combined with `[SerializeReference]` is very convenient. It allows designers to visually chain
multiple actions in the inspector without writing extra code. This is especially useful for quickly iterating on game
logic or events.

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

<img src="../../Images/PlayerActionTrigger_Composite.png" alt="Inspector setup example" width="390" height="164">

#### 3. Assign Composite Action

In the **Inspector**, we can assign the [CompositeAction]() value to the `Action` parameter. For example, we can
add [PrintAction](PrintAction.md) to the action array.


> [!WARNING]
> Using `[SerializeReference]` should be considered a last resort. If possible, define actions through code instead for
> clarity and maintainability, because `[SerializeReference]` is very fragile during refactoring.