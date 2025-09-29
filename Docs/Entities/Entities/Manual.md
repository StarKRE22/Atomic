# 🧩 Entities

An **Entity** is the fundamental element in the framework. Each entity is a container that holds **tags**,
**properties**, **behaviors**, and own **lifecycle**. This container is **dynamic** and strictly separates **data** from
**logic**, which allows for rapid development of game mechanics and their reuse. Thus, every game object, entity, and
system is a **composition**.

- [IEntity](Entities/IEntity.md)
- [Entity](Entities/Entity.md)
- [EntitySingleton](Entities/EntitySingleton.md)
- [SceneEntity](Entities/SceneEntity.md)
- [SceneEntityProxy](Entities/SceneEntityProxy.md)
- [SceneEntitySingleton](Entities/SceneEntitySingleton.md)
- [Extensions](Entities/Extensions.md)


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

#### 2. Create `MoveBehaviour` for your entity

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








## 📑 Table of Contents

- [Unity Quick Start]()
- [CSharp Quick Start]()
- [API Reference](#-api-reference)

---


## 🔍 API Reference
-->
