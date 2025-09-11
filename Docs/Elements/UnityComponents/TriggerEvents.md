# 🧩️ TriggerEvents Classes

`TriggerEvents` and `TriggerEvents2D` are Unity `MonoBehaviour` components that expose trigger callbacks as C# events.  
They allow you to subscribe to trigger enter, stay, and exit events without writing custom `MonoBehaviour` methods.

---

## TriggerEvents

Exposes 3D physics trigger events.

### Events

- `event Action<Collider> OnEntered` – Invoked when a collider enters the trigger (`OnTriggerEnter`).
- `event Action<Collider> OnExited` – Invoked when a collider exits the trigger (`OnTriggerExit`).
- `event Action<Collider> OnStay` – Invoked every frame while a collider remains inside the trigger (`OnTriggerStay`).

### Behavior

- Automatically triggers the corresponding event when Unity's 3D physics system calls `OnTriggerEnter`, `OnTriggerExit`, or `OnTriggerStay`.
- Allows multiple subscribers to react to triggers without overriding `MonoBehaviour` methods.
- Decouples trigger response logic from GameObject scripts.

### Example Usage

```csharp
using UnityEngine;
using Atomic.Elements;

public class TriggerExample : MonoBehaviour
{
    private TriggerEvents triggerEvents;

    private void Awake()
    {
        triggerEvents = gameObject.AddComponent<TriggerEvents>();

        triggerEvents.OnEntered += col => 
            Debug.Log($"3D Trigger entered by {col.gameObject.name}");

        triggerEvents.OnStay += col =>
            Debug.Log($"3D Trigger staying with {col.gameObject.name}");

        triggerEvents.OnExited += col =>
            Debug.Log($"3D Trigger exited by {col.gameObject.name}");
    }
}
```
---
## TriggerEvents2D

`TriggerEvents2D` exposes Unity 2D physics trigger events as C# events, allowing you to react to triggers without writing custom `MonoBehaviour` methods.

---

### Events

- `event Action<Collider2D> OnEntered` – Invoked when a 2D collider enters the trigger (`OnTriggerEnter2D`).
- `event Action<Collider2D> OnExited` – Invoked when a 2D collider exits the trigger (`OnTriggerExit2D`).
- `event Action<Collider2D> OnStay` – Invoked every frame while a 2D collider remains inside the trigger (`OnTriggerStay2D`).

---

### Behavior

- Automatically triggers the corresponding event when Unity’s 2D physics system invokes `OnTriggerEnter2D`, `OnTriggerExit2D`, or `OnTriggerStay2D`.
- Supports multiple external subscribers without modifying the `MonoBehaviour`.
- Keeps trigger handling modular, decoupled, and reusable across multiple GameObjects.

---

### Example Usage

```csharp
using UnityEngine;
using Atomic.Elements;

public class Trigger2DExample : MonoBehaviour
{
    private TriggerEvents2D triggerEvents;

    private void Awake()
    {
        triggerEvents = gameObject.AddComponent<TriggerEvents2D>();

        triggerEvents.OnEntered += col =>
            Debug.Log($"2D Trigger entered by {col.gameObject.name}");

        triggerEvents.OnStay += col =>
            Debug.Log($"2D Trigger staying with {col.gameObject.name}");

        triggerEvents.OnExited += col =>
            Debug.Log($"2D Trigger exited by {col.gameObject.name}");
    }
}
```