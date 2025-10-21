# 🧩️ SceneEntity

Represents Unity implementation of the entity. It allows installation from the Unity
Scene and composition through the Inspector or installers. Supports Unity serialization and Odin Inspector.


---

## 📑 Table of Contents

- [Quick Start](#-quick-start)
- [API Reference](#-api-reference)
    - [Type](#-type)
- [Modules](#-modules)
- [Notes](#-notes)

---

## 🚀 Quick Start

Below is the process for quickly creating a character entity in Unity

#### 1. Create a new `GameObject` on a scene

<img width="400" height="" alt="GameObject creation" src="https://github.com/user-attachments/assets/463a721f-e50d-4cb7-86be-a5d50a6bfa17" />

#### 2. Add `Entity` Component to the GameObject

<img width="400" height="" alt="Entity component" src="../../Images/EntityComponent.png" />

#### 3. Create a movement mechanics for the entity

```csharp
// Controller that moves entity by its direction
public sealed class MoveBehaviour : IEntityInit, IEntityFixedTick
{
    private Transform _transform;
    private IValue<float> _moveSpeed;
    private IValue<Vector3> _moveDirection;

    // Called when MonoBehaviour.Start() is invoked
    public void Init(IEntity entity)
    {
        _transform = entity.GetValue<Transform>("Transform");
        _moveSpeed = entity.GetValue<IValue<float>>("MoveSpeed");
        _moveDirection = entity.GetValue<IValue<Vector3>>("MoveDirection");
    }

    // Called when MonoBehaviour.FixedUpdate() is invoked
    public void FixedTick(IEntity entity, float deltaTime)
    {
        Vector3 direction = _moveDirection.Value;
        if (direction != Vector3.zero) 
            _transform.position += _moveSpeed.Value * deltaTime * direction;
    }
}
```

#### 4. Create a script that populates the entity with tags, values and behaviours

 ```csharp
//Populates entity with tags, values and behaviours
public sealed class CharacterInstaller : SceneEntityInstaller
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Const<float> _moveSpeed = 5.0f; //Immutable variable
    [SerializeField] private ReactiveVariable<Vector3> _moveDirection; //Mutable variable with subscription

    public override void Install(IEntity entity)
    {
        //Add tags to a character
        entity.AddTag("Character");
        entity.AddTag("Moveable");

        //Add properties to a character
        entity.AddValue("Transform", _transform);
        entity.AddValue("MoveSpeed", _moveSpeed);
        entity.AddValue("MoveDirection", _moveDirection);
        
        //Add behaviours to a character
        entity.AddBehaviour<MoveBehaviour>();
    }
}
```

#### 5. Attach `CharacterInstaller` script to the GameObject

<img width="400" height="" alt="изображение" src="https://github.com/user-attachments/assets/1967b1d8-b6b7-41c7-85db-5d6935f6443e" />

#### 6. Drag & drop `CharacterInstaller` into `installers` field of the entity

<img width="400" height="" alt="изображение" src="../../Images/EntityInstalling.png" />

#### 7. Enter `PlayMode` and check your character movement!

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[AddComponentMenu("Atomic/Entities/Entity")]
[DisallowMultipleComponent]
[DefaultExecutionOrder(-1000)]
public partial class SceneEntity : MonoBehaviour, IEntity, ISerializationCallbackReceiver
```

- **Inheritance:** [IEntity](IEntity.md), `MonoBehaviour`, `ISerializationCallbackReceiver`

---

## 🧩 Modules

Each module represents a logical subset of the `SceneEntity` class. Click the links to dive deeper into each section:

- [Core](SceneEntityCore.md) — Represents the fundamental identity and state of the entity
- [Tags](SceneEntityTags.md) — Manage lightweight categorization and filtering of entities
- [Values](SceneEntityValues.md) — Manage dynamic key-value storage for the entity
- [Behaviours](SceneEntityBehaviours.md) — Manage modular logic attached to the entity
- [Lifecycle](SceneEntityLifecycle.md) — Manages the entity's state transitions and update phases
- [Installing](SceneEntityInstalling.md) — Provides entity configuration with tags, values and behaviours
- [Gizmos](SceneEntityGizmos.md) — Provides gizmo drawing functionality
- [Debug](SceneEntityDebug.md) — Represents debug properties
- [Editor](SceneEntityEditor.md) — Provides editor-time lifecycle support
- [Creation](SceneEntityCreation.md) — Allows you to create entities at runtime
- [Destruction](SceneEntityDestruction.md) — Destroys the game objects with entities
- [Casting](SceneEntityCasting.md) — Provides safe casting between `IEntity` to `SceneEntity`

---

## 📝 Notes

- **Event-Driven** – Reactive programming support via state change notifications.
- **Unique Identity** – Runtime-generated instance ID for entity tracking.
- **Tag System** – Lightweight categorization and filtering.
- **State Management** – Dynamic key-value storage for runtime data.
- **Behaviour Composition** – Attach or detach modular logic at runtime.
- **Lifecycle Control** – Built-in support for `Init`, `Enable`, `Update`, `Disable`, and `Dispose` phases.
- **Registry Integration** – Automatic registration with EntityRegistry
- **Memory Efficient** – Pre-allocation support for collections
- **Unity Component** – Attach directly to GameObjects.
- **Scene Installation** – Automatically installs child entities and configured installers.
- **Unity Lifecycle Integration** – Hooks into Awake, Start, OnEnable, OnDisable, and OnDestroy.
- **Gizmos Support** – Conditional drawing in Scene view.
- **Prefab & Factory Support** – Creation, instantiation, and destruction of entities.
- **Casting & Proxies** – Safe conversion between [IEntity](IEntity.md), `SceneEntity`
  and [SceneEntityProxy](SceneEntityProxies.md).
- **Scene-Wide Installation** – Can install all SceneEntities in a scene.
- **Odin Inspector Support** – Optional editor enhancements for configuration and debug.
- **Not Thread Safe** — All operations should be performed on the main Unity thread.
- `SceneEntity` is Unity-specific
- Default execution order is `-1000` (runs early)
- `[DisallowMultipleComponent]` prevents multiple entities per `GameObject`

