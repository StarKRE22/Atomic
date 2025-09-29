# 🧩 Entities

An **Entity** is the fundamental element in the framework. Each entity is a container that holds **tags**,
**properties**, **behaviors**, and own **lifecycle**. This container is **dynamic** and strictly separates **data** from
**logic**, which allows for rapid development of game mechanics and their reuse. Thus, every game object, entity, and
system is a **composition**.

---

## 📑 Table of Contents

- [Core Concept](#-core-concept)
- [CSharp Quick Start](#-csharp-quick-start)
- [Unity Quick Start](#-unity-quick-start)
- [API Reference](#-api-reference)
- [Performance](#-performance)

---

## 💡 Core Concept

At the core of all entities lies the **Entity-State-Behaviour (ESB)** pattern.
The idea of the **ESB** pattern is that any object, system, or AI can be represented as an **Entity** with a 
**composition** of data (**State**) and logic (**Behaviour**), but with a strict separation between them.

Since State and Behaviour are strictly separated, this makes it possible to **reuse components** and **modify the
structure of a game object at runtime**. This approach provides great flexibility and allows for the rapid development
of gameplay interactions.

- **Entity** — a container that holds collections of data and logic.
- **State** — a data map that defines the parameters of an entity.
- **Behaviour** — a set of controllers that operate on the entity’s *State* they are attached to.

![EntityStateBehaviour.png](../../Images/ESBPattern.png)

---

## 🚀 CSharp Quick Start

#### 1. Create a new entity

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

#### 2. Create `MoveBehaviour` for the entity

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

#### 3. Add `MoveBehaviour` to the entity

```csharp
entity.AddBehaviour<MoveBehaviour>();
```

#### 4. Initialize the entity when game is loading

```csharp
//Calls IEntityInit
entity.Init();
```

#### 5. Enable the entity when game is started

```csharp
//Enable entity for updates
//Calls IEntityEnable
entity.Enable(); 
```

#### 6. Update the entity while a game is running

```csharp
const float deltaTime = 0.02f;

while(_isGameRunning)
{ 
   //Calls IEntityTick
   entity.Tick(deltaTime); 
}
```

#### 7. When game is finished disable the entity

```csharp
//Disable entity for updates
//Calls IEntityDisable
character.Disable();
```

#### 8. Dispose the entity when unloading game resources

```csharp
//Dispose entity resources
//Calls IEntityDispose
entity.Dispose();
```

---

## 🚀 Unity Quick Start

#### 1. Create a new `GameObject`

<img width="360" height="255" alt="GameObject creation" src="https://github.com/user-attachments/assets/463a721f-e50d-4cb7-86be-a5d50a6bfa17" />

#### 2. Add `Entity` Component to the GameObject

<img width="464" height="346" alt="Entity component" src="https://github.com/user-attachments/assets/f74644ba-5858-4857-816e-ea47eed0e913" />

#### 3. Create `MoveBehaviour` for your entity

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

#### 4. Create `CharacterInstaller` script

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

<img width="464" height="153" alt="изображение" src="https://github.com/user-attachments/assets/1967b1d8-b6b7-41c7-85db-5d6935f6443e" />

#### 6. Drag & drop `CharacterInstaller` into `installers` field of the entity

<img width="464" height="" alt="изображение" src="../../Images/SceneEntity%20Attach%20Installer.png" />

#### 7. Enter `PlayMode` and check your character movement!

---

## 🔍 API Reference

Below are the interfaces and classes for working with entities.

- [IEntity](IEntity.md)
- [Entity](Entity.md)
- [EntitySingleton](EntitySingleton.md)
- [SceneEntity](SceneEntity.md)
- [SceneEntityProxy](SceneEntityProxy.md)
- [SceneEntitySingleton](SceneEntitySingleton.md)
- [Extensions](Extensions.md)

---

## 🔥 Performance

The performance measurements below were conducted on a **MacBook with Apple M1**, using **1,000 elements** for each
container type. All times are **median execution times** in microseconds (μs).

### 🏷️ Tags

Tags are implemented as a **HashSet of integers**, optimized for fast lookups, additions, and removals.

| Operation  | HashSet (Median μs) | Tags (Median μs) |
|------------|---------------------|------------------|
| Contains   | 47.85               | 3.80             |
| Add        | 57.40               | 8.30             |
| Remove     | 50.45               | 5.40             |
| Clear      | 1.10                | 2.80             |
| Enumerator | 29.75               | 2.30             |

> Tags are extremely lightweight and provide **O(1) average time complexity** for key operations.

---

### 🔑 Values

Values act as a **Dictionary-like storage** mapping integer keys to objects or structs, supporting generic access and
unsafe references for high performance.

| Operation            | Dictionary (Median μs) | Values (Median μs)                 |
|----------------------|------------------------|------------------------------------|
| Get                  | 7.40                   | 4.10 (object)                      |
| Get + Cast           | 8.25                   | 12.00 (reference) / 4.70 (value)   |
| Get + Unsafe Cast    | 7.80                   | 4.20 (reference) / 4.50 (value)    |
| TryGet               | 34.20                  | 31.20 (object)                     |
| TryGet + Cast        | -                      | 50.75 (reference) / 4.90  (value)  |
| TryGet + Unsafe Cast | -                      | 30.50 (reference) / 6.90  (value)  |
| Add                  | 34.10                  | 62.15 (reference) / 178.45 (value) |
| Remove               | 6.70                   | 5.20 (reference) / 5.50 (value)    |
| Clear                | 1.30                   | 2.60                               |
| Contains             | 6.90                   | 4.00                               |
| Set                  | 37.50                  | 62.50 (reference) / 187.35 (value) |
| Enumerator           | 56.60                  | 56.80 (reference) / 171.75 (value) |

> Values provide flexible access patterns with **minimal overhead**, especially for primitives and unsafe references.

---

### ⚙️ Behaviours

Behaviours are stored in a **list-like container**, supporting multiple references to the same instance. Operations
include addition, removal, and indexed access.

| Operation  | List (Median μs) | Behaviours (Median μs) |
|------------|------------------|------------------------|
| Add        | 29.30            | 34.30                  |
| Clear      | 0.40             | 1.20                   |
| Contains   | 1825.95          | 650.60                 |
| Remove     | 312.63           | 243.91                 |
| Get At     | 1.60             | 2.30                   |
| Enumerator | 29.95            | 28.80                  |

> Behaviours combine fast index access with flexibility to store duplicate references, though some operations are 
> **O(n)** in the worst case.