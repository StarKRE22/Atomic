# 🧩️ CollisionEvents

`CollisionEvents` and `CollisionEvents2D` are Unity `MonoBehaviour` components that expose collision callbacks as C# events.  
They allow you to subscribe to collision start, stay, and end events without writing custom `MonoBehaviour` methods.

---

## CollisionEvents

Exposes 3D physics collision events.

### Events

- `event Action<Collision> OnEntered` – invoked when a collision begins (`OnCollisionEnter`).
- `event Action<Collision> OnExited` – invoked when a collision ends (`OnCollisionExit`).
- `event Action<Collision> OnStay` – invoked every frame while a collision persists (`OnCollisionStay`).

### Behavior

- Automatically triggers the corresponding event when Unity's physics system calls `OnCollisionEnter`, `OnCollisionExit`, or `OnCollisionStay`.
- Allows multiple subscribers to react to collisions without overriding MonoBehaviour methods.
- Decouples collision response logic from GameObject scripts.

### Example Usage

```csharp
using UnityEngine;
using Atomic.Elements;

public class CollisionExample : MonoBehaviour
{
    private CollisionEvents collisionEvents;

    private void Awake()
    {
        collisionEvents = gameObject.AddComponent<CollisionEvents>();

        collisionEvents.OnEntered += collision => 
            Debug.Log($"Collision started with {collision.gameObject.name}");

        collisionEvents.OnStay += collision =>
            Debug.Log($"Collision ongoing with {collision.gameObject.name}");

        collisionEvents.OnExited += collision =>
            Debug.Log($"Collision ended with {collision.gameObject.name}");
    }
}
```
---
## CollisionEvents2D

`CollisionEvents2D` exposes Unity 2D physics collision events as C# events, allowing you to react to collisions without writing custom `MonoBehaviour` methods.

### Events

- `event Action<Collision2D> OnEntered` – Invoked when a 2D collision begins (`OnCollisionEnter2D`).
- `event Action<Collision2D> OnExited` – Invoked when a 2D collision ends (`OnCollisionExit2D`).
- `event Action<Collision2D> OnStay` – Invoked every frame while a 2D collision persists (`OnCollisionStay2D`).

### Behavior

- Automatically triggers the corresponding event when Unity’s 2D physics system invokes `OnCollisionEnter2D`, `OnCollisionExit2D`, or `OnCollisionStay2D`.
- Supports multiple external subscribers without modifying the `MonoBehaviour`.
- Keeps collision handling modular, decoupled, and reusable across multiple GameObjects.

### Example Usage

```csharp
using UnityEngine;
using Atomic.Elements;

public class Collision2DExample : MonoBehaviour
{
    private CollisionEvents2D collisionEvents;

    private void Awake()
    {
        collisionEvents = gameObject.AddComponent<CollisionEvents2D>();

        collisionEvents.OnEntered += collision =>
            Debug.Log($"2D Collision started with {collision.gameObject.name}");

        collisionEvents.OnStay += collision =>
            Debug.Log($"2D Collision ongoing with {collision.gameObject.name}");

        collisionEvents.OnExited += collision =>
            Debug.Log($"2D Collision ended with {collision.gameObject.name}");
    }
}
```