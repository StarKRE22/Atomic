# 🧩 Entity Behaviours

**Entity Behaviour** is a modular controller that handles [IEntity](../Entities/IEntity.md) lifecycle events and
attached to concrete instance of entity. It allows entities to dynamically compose functionality at runtime, following
the **Entity-State-Behaviour** pattern.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [Lifecycle Events](#-lifecycle-events)
- [API Reference](#-api-reference)
- [Notes](#-notes)

---

## 🗂 Example Usage

Below is an example of using `MoveBehaviour` for the entity:

#### 1. Create a new instance of entity

```csharp
//Create a new entity
var character = new Entity("Character");

//Add tags
character.AddTag("Moveable");

//Add properties
character.AddValue("Position", new ReactiveVariable<Vector3>());
character.AddValue("MoveSpeed", new Const<float>(3.5f));
character.AddValue("MoveDirection", new ReactiveVariable<Vector3>());
```

#### 2. Create `MoveBehaviour` for the entity

```csharp
//Controller that moves entity by its direction
public sealed class MoveBehaviour : IEntityInit, IEntityFixedTick
{
    private IVariable<Vector3> _position;
    private IValue<float> _moveSpeed;
    private IValue<Vector3> _moveDirection;

    //Calls when Entity.Init()
    public void Init(IEntity entity)
    {
        _position = entity.GetValue<IVariable<Vector3>>("Position");
        _moveSpeed = entity.GetValue<IValue<float>>("MoveSpeed");
        _moveDirection = entity.GetValue<IValue<Vector3>>("MoveDirection");
    }

    //Calls when Entity.FixedTick()
    public void FixedTick(IEntity entity, float deltaTime)
    {
        Vector3 direction = _moveDirection.Value;
        if (direction != Vector3.zero) 
            _position.Value += _moveSpeed.Value * deltaTime * direction;
    }
}
```

#### 3. Attach `MoveBehaviour` to the entity

```csharp
//Add behaviour
character.AddBehaviour<MoveBehaviour>();
```

---

## 🔄 Lifecycle Events

Each behaviour can handle different events of the entity:

| Event       | Purpose                                                    |
|-------------|------------------------------------------------------------|
| `Init`      | Initialization of the behaviour when the entity is created |
| `Dispose`   | Releasing entity resources when it is destroyed            |
| `Enable`    | Activating the entity on the scene or in a pool            |
| `Disable`   | Deactivating the entity and returning it to the pool       |
| `Tick`      | Updates every frame (logic, state)                         |
| `FixedTick` | Physics and game mechanics updates with a fixed timestep   |
| `LateTick`  | Updates after rendering (e.g., UI)                         |

---

## 🔍 API Reference

There are separate interfaces that handles the corresponding lifecycle events of the entity.

<details>
  <summary><b>Common</b></summary>
  <ul>
    <li><a href="IEntityBehaviour.md">IEntityBehaviour</a></li>
    <li><a href="../Attributes/RunInEditModeAttribute.md">RunInEditModeAttribute</a></li>
  </ul>
</details>

<details>
  <summary><b>Init</b></summary>
  <ul>
    <li><a href="IEntityInit.md">IEntityInit</a></li>
    <li><a href="IEntityInit%601.md">IEntityInit&lt;E&gt;</a></li>
  </ul>
</details>

<details>
  <summary><b>Dispose</b></summary>
  <ul>
    <li><a href="IEntityDispose.md">IEntityDispose</a></li>
    <li><a href="IEntityDispose%601.md">IEntityDispose&lt;E&gt;</a></li>
  </ul>
</details>

<details>
  <summary><b>Enable</b></summary>
  <ul>
    <li><a href="IEntityEnable.md">IEntityEnable</a></li>
    <li><a href="IEntityEnable%601.md">IEntityEnable&lt;E&gt;</a></li>
  </ul>
</details>

<details>
  <summary><b>Disable</b></summary>
  <ul>
    <li><a href="IEntityDisable.md">IEntityDisable</a></li>
    <li><a href="IEntityDisable%601.md">IEntityDisable&lt;E&gt;</a></li>
  </ul>
</details>

<details>
  <summary><b>Tick</b></summary>
  <ul>
    <li><a href="IEntityTick.md">IEntityTick</a></li>
    <li><a href="IEntityTick%601.md">IEntityTick&lt;E&gt;</a></li>
  </ul>
</details>

<details>
  <summary><b>FixedTick</b></summary>
  <ul>
    <li><a href="IEntityFixedTick.md">IEntityFixedTick</a></li>
    <li><a href="IEntityFixedTick%601.md">IEntityFixedTick&lt;E&gt;</a></li>
  </ul>
</details>

<details>
  <summary><b>LateTick</b></summary>
  <ul>
    <li><a href="IEntityLateTick.md">IEntityLateTick</a></li>
    <li><a href="IEntityLateTick%601.md">IEntityLateTick&lt;E&gt;</a></li>
  </ul>
</details>

<details>
  <summary><b>Gizmos</b></summary>
  <ul>
    <li><a href="IEntityGizmos.md">IEntityGizmos</a></li>
    <li><a href="IEntityGizmos%601.md">IEntityGizmos&lt;E&gt;</a></li>
  </ul>
</details>


---

## 📝 Notes

- **Modular Logic** – Encapsulates a single responsibility or feature of an entity
- **Dynamic Composition** – Can be attached or removed from an entity at runtime
- **Integration with Entity Lifecycle** – Behaviours can respond to Init, Enable, Disable, and Dispose events
- **Lightweight** – Interface only, implementation is left to the developer
- Behaviours are typically stateless or encapsulate entity-specific state.
- They can be used to implement features such as movement, attack, game systems, AI, input, or UI controllers.


[//]: # ()
[//]: # (- **Init**)

[//]: # (    - [IEntityInit]&#40;IEntityInit.md&#41; <!-- + -->)

[//]: # (    - [IEntityInit&lt;E&gt;]&#40;IEntityInit%601.md&#41; <!-- + -->)

[//]: # (- **Dispose**)

[//]: # (    - [IEntityDispose]&#40;IEntityDispose.md&#41; <!-- + -->)

[//]: # (    - [IEntityDispose&lt;E&gt;]&#40;IEntityDispose%601.md&#41; <!-- + -->)

[//]: # (- **Enable**)

[//]: # (    - [IEntityEnable]&#40;IEntityEnable.md&#41; <!-- + -->)

[//]: # (    - [IEntityEnable&lt;E&gt;]&#40;IEntityEnable%601.md&#41; <!-- + -->)

[//]: # (- **Disable**)

[//]: # (    - [IEntityDisable]&#40;IEntityDisable.md&#41; <!-- + -->)

[//]: # (    - [IEntityDisable&lt;E&gt;]&#40;IEntityDisable%601.md&#41; <!-- + -->)

[//]: # (- **Tick**)

[//]: # (    - [IEntityTick]&#40;IEntityTick.md&#41; <!-- + -->)

[//]: # (    - [IEntityTick&lt;E&gt;]&#40;IEntityTick%601.md&#41; <!-- + -->)

[//]: # (- **FixedTick**)

[//]: # (    - [IEntityFixedTick]&#40;IEntityFixedTick.md&#41; <!-- + -->)

[//]: # (    - [IEntityFixedTick&lt;E&gt;]&#40;IEntityFixedTick%601.md&#41; <!-- + -->)

[//]: # (- **LateTick**)

[//]: # (    - [IEntityLateTick]&#40;IEntityLateTick.md&#41; <!-- + -->)

[//]: # (    - [IEntityLateTick&lt;E&gt;]&#40;IEntityLateTick%601.md&#41; <!-- + -->)

[//]: # (- **Gizmos**)

[//]: # (    - [IEntityGizmos]&#40;IEntityGizmos.md&#41; <!-- + -->)

[//]: # (    - [IEntityGizmos&lt;E&gt;]&#40;IEntityGizmos%601.md&#41; <!-- + -->)
