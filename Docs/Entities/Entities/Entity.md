# 🧩️ Entity

Represents the base implementation of the [IEntity](IEntity.md). It provides a modular container for **dynamic state**,
**tags**,
**values**, **behaviours**, and **lifecycle management**.

---

## 📑 Table of Contents

- [Quick Start](#-quick-start)
- [API Reference](#-api-reference)
  - [Type](#-type)
- [Modules](#-modules)
- [Notes](#-notes)

---

## 🚀 Quick Start

Below is the process for quickly creating an entity in plain C#

#### 1. Create a new instance of the entity

```csharp
//Create a new entity
IEntity entity = new Entity("Character");

//Add tags
entity.AddTag("Moveable");

//Add properties
entity.AddValue("Position", new ReactiveVariable<Vector3>());
entity.AddValue("MoveSpeed", new Const<float>(3.5f));
entity.AddValue("MoveDirection", new ReactiveVariable<Vector3>());
```

#### 2. Create a movement mechanics for the entity

```csharp
//Controller that moves entity by its direction
public sealed class MoveBehaviour : IEntityInit, IEntityTick
{
    private IVariable<Vector3> _position;
    private IValue<float> _moveSpeed;
    private IValue<Vector3> _moveDirection;

    //Called when Entity.Init()
    public void Init(IEntity entity)
    {
        _position = entity.GetValue<IVariable<Vector3>>("Position");
        _moveSpeed = entity.GetValue<IValue<float>>("MoveSpeed");
        _moveDirection = entity.GetValue<IValue<Vector3>>("MoveDirection");
    }

    //Called when Entity.OnUpdate()
    public void Tick(IEntity entity, float deltaTime)
    {
        Vector3 direction = _moveDirection.Value;
        if (direction != Vector3.zero) 
            _position.Value += _moveSpeed.Value * deltaTime * direction;
    }
}
```

#### 3. Attach `MoveBehaviour` instance to the entity

```csharp
entity.AddBehaviour<MoveBehaviour>();
```

#### 4. Control lifecycle of your entity

```csharp
// Initialize the entity -> Calls IEntityInit
entity.Init();

// Enable the entity for updates -> Calls IEntityEnable 
entity.Enable(); 

// Update your entity while game is running
const float deltaTime = 0.016f; // 60 FPS
while(isGameRunning)
{
   entity.Tick(deltaTime); // Calls IEntityTick
   System.Threading.Thread.Sleep(16); // deltaTime * 1000 
}

// Disable entity for updates -> Calls IEntityDisable
character.Disable();

// Dispose entity resources -> Calls IEntityDispose
entity.Dispose();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public partial class Entity : IEntity
```

- **Inheritance:** [IEntity](IEntity.md)

---

## 🧩 Modules

Each module represents a logical subset of the `Entity` class. Click the links to dive deeper into each section:

- [Core](EntityCore.md) — Represents the fundamental identity and state of the entity
- [Tags](EntityTags.md) — Manage lightweight categorization and filtering of entities
- [Values](EntityValues.md) — Manage dynamic key-value storage for the entity
- [Behaviours](EntityBehaviours.md) — Manage modular logic attached to the entity
- [Lifecycle](EntityLifecycle.md) — Manages the entity's state transitions and update phases
- [Debug](EntityDebug.md) — Represents debug properties

---

# 📝 Notes

- **Event-Driven** – Reactive programming support via state change notifications.
- **Unique Identity** – Runtime-generated instance ID for entity tracking.
- **Tag System** – Lightweight categorization and filtering.
- **State Management** – Dynamic key-value storage for runtime data.
- **Behaviour Composition** – Attach or detach modular logic at runtime.
- **Lifecycle Control** – Built-in support for `Init`, `Enable`, `Tick`, `Disable`, and `Dispose` phases.
- **Registry Integration** – Automatic registration with EntityRegistry
- **Memory Efficient** – Pre-allocation support for collections
- **Odin Inspector Support** – Optional editor enhancements for configuration and debug.
- **Debug Support** – When used with Unity Editor and Odin Inspector, debug properties provide quick insight into
  the
  entity state, tags, values, and behaviours.
- **Thread Safety** – `Entity` is **NOT thread-safe**; all interactions should occur on the main thread or be
  synchronized externally.
- **Composition** – Behaviours, tags, and values can be added dynamically at runtime without modifying the core
  entity class.