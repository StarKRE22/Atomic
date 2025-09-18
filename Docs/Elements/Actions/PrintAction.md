# 🧩 PrintAction

The **PrintAction** class represents an action that **logs a message**. It implements the [IAction](IAction.md) interface and works across platforms. This allows printing messages consistently in both Unity and non-Unity environments, which is especially useful when debugging or logging events in cross-platform code.


> [!NOTE]
> In **Unity**, it uses `Debug.Log`, `Debug.LogWarning`, or `Debug.LogError` depending on the specified `LogType`. Outside of Unity, it uses `Console.WriteLine`.

---

## 🏗️ Constructors

#### `PrintAction(string)`
```csharp
public PrintAction(string message)
```
- **Description:** Initializes a new instance with the specified message.
- **Parameter:** `message` – The text to log.
- **Note:** Works outside of Unity.

#### `PrintAction(string, LogType)`
```csharp
public PrintAction(string message, LogType logType = LogType.Log)
```
- **Description:** Initializes a new instance with the specified message and log type.
- **Parameter:**
    - `message` – The text to log.
    - `logType` – The type of log (default is `LogType.Log`).
- **Note:** Works in Unity.

## 🏹 Methods

#### `Invoke()`
```csharp
public void Invoke()
```
- **Description:** Logs the configured message to the console.
    - In Unity, uses the specified `LogType`.
    - Outside Unity, prints to standard console.

#### `ToString()`
```csharp
public string ToString();
```
- **Description:** Returns a message representation.
- **Remarks:** The output depends on the Unity version:
  - **If compiled with `Unity`** then returns a string in the format: `{LogType}: {Message}`
  - **Otherwise**, Returns only the message.

### 🗂 Example of Usage
```csharp
#if UNITY_5_3_OR_NEWER

IAction printAction = new PrintAction("Warning message!", LogType.Warning);
printAction.Invoke(); // Logs a warning in Unity

#else

IAction printAction = new PrintAction("Hello World!");
printAction.Invoke(); // Logs message to standard console

#endif
```

---

## ❗️Using [SerializeReference]

In Unity, **PrintAction** is perfect for **visualizing the occurrence of an action**—for example, when temporarily replacing a real action or using it inside a composite.  It can also be easily serialized using `[SerializeReference]` in a `MonoBehaviour` and configured in the inspector, making it convenient for debugging or testing action pipelines.

> [!WARNING]
> Using `[SerializeReference]` should be considered a last resort. If possible, define actions through code instead for clarity and maintainability, because `[SerializeReference]` is very fragile during refactoring.

### 🗂 Example of Usage

Create a `PlayerActionTrigger` that executes an action **when triggered by the player**. The specific action can be assigned by the designer directly in the **Inspector**.

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
