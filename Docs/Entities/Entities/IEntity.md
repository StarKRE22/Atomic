# 🧩 IEntity

```csharp
public interface IEntity : IInitLifecycle, IEnableLifecycle, ITickLifecycle
``` 

- **Description:** Represents the fundamental interface of entity in the framework.
  It provides a modular container for **dynamic state**, **tags**, **values**, **behaviours**
  and **lifecycle management**.

- **Inheritance:**
    - [IInitLifecycle](../Lifecycle/Sources/IInitLifecycle.md) – Supports explicit initialization and disposal.
    - [IEnableLifecycle](../Lifecycle/Sources/IEnableLifecycle.md) – Supports runtime enabling and disabling.
    - [ITickLifecycle](../Lifecycle/Sources/ITickLifecycle.md) – Supports `Tick`, `FixedTick`, and `LateTick` callbacks.

- **Sections:**
  - [Core]
  - [Tags]
  - [Values]
  - [Behaviours]
  - [Lifecycle]

---


---


---


---

<details>
  <summary>
    <h2 id="-example-of-usage"> 🗂 Example of Usage</h2>
    <br> The example below demonstrates quick entity creation and configuration with <code>Atomic.Elements</code>:
  </summary>
<br>

```csharp
// Create a new entity in C#
IEntity entity = new Entity();
entity.Name = "Player Character";

// Add tags
entity.AddTag("Character");
entity.AddTag("Moveable");
entity.AddTag("Damageable");

// Add values
entity.AddValue("Health", new ReactiveVariable<int>(100));
entity.AddValue("MoveSpeed", new ReactiveVariable<float>(5));
entity.AddValue("MoveDirection", new ReactiveVariable<Vector3>());

// Add a behaviour
entity.AddBehaviour<MovementBehaviour>();

// Initialize entity after configuration
entity.Init();

// Enable entity for updates (e.g., when retrieved from a pool)
entity.Enable();

// Update manually (e.g., in a game loop)
entity.Tick(Time.deltaTime);

// Disable entity (e.g., if it moved back into a pool)
entity.Disable();

// Dispose entity when game is unloading
entity.Dispose();
```

</details>

---

<details>
  <summary>
    <h2 id="-notes">📝 Notes</h2>
  </summary>

- **Event-Driven** – Reactive programming support via state change notifications.
- **Unique Identity** – Runtime-generated instance ID for entity tracking.
- **Tag System** – Lightweight categorization and filtering.
- **State Management** – Dynamic key-value storage for runtime data.
- **Behaviour Composition** – Attach or detach modular logic at runtime.
- **Lifecycle Control** – Built-in support for `Init`, `Enable`, `Tick`, `Disable`, and `Dispose` phases.
</details>