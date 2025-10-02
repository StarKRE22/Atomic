


## 🗂 Example of Usage

### Entity State

An entity may have health, damage, and speed values as part of its **State**:

```csharp
Entity entity = new Entity();

entity.AddValue("Health", 10);
entity.AddValue("Damage", 1);
entity.AddValue("Speed", 5.0f);
```

You can also define coordinates and direction — also part of the **State**:

```csharp
entity.AddValue("Position", new Vector3(10, 0, 10));
entity.AddValue("MoveDirection", Vector3.forward);
```

---

### Entity Tags

Entities can have **tags** that define which categories they belong to:

```csharp
entity.AddTag("Moveable");
entity.AddTag("Damageable");
entity.AddTag("NPC");
```

---

### Entity Behaviour

In addition to *State*, we can add **Behaviour**, such as movement logic, which remains strictly separated from the
entity’s data:

```csharp
public class MoveBehaviour : IEntityFixedTick
{
    public void FixedTick(IEntity entity, float deltaTime)
    {
        Vector3 position = entity.GetValue<Vector3>("Position");
        Vector3 direction = entity.GetValue<Vector3>("MoveDirection");
        float speed = entity.GetValue<float>("Speed");
        
        Vector3 newPosition = position + speed * deltaTime * direction;
        entity.SetValue("Position", newPosition);
    }
}
```

This behaviour can then be dynamically attached to the entity:

```csharp
entity.AddBehaviour<MoveBehaviour>();
```

---

### Entity Lifecycle

**Activate the entity** so that its `FixedTick` logic starts working:

```csharp
entity.Enable();
```

**Update the entity** while the game is running:

```csharp
while(GameIsRunning)
    entity.FixedTick();
```

**Dispose of the entity** when the game is finished:

```csharp
entity.Dispose();
```

---









<details>
  <summary>
    <h2 id="-optimization"> 📈 Optimization</h2>
    <br>
    </summary>

<br>


Provides a simple workflow for <b>precomputing entity capacities</b> in the Unity Editor.
You can optimize your entity’s size by precomputing the capacity of <b>tags</b>, <b>values</b>, and <b>behaviours</b>.


- [Core Concept](#-core-concept)
- [Unity Quick Start](#-unity-quick-start)
- [CSharp Quick Start](#-csharp-quick-start)
- [Examples of Usage](#-examples-of-usage)
- [Performance](#-performance)


- 
- [Theory]()
    - [Entity]
    - [Tags]
    - [Values]
    - [Behaviours]
    - [Lifecycle]

OPTIMIZATION





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




## 🚀 Unity Quick Start

Below is the process for quickly creating a character entity in Unity:

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

## ⚡ CSharp Quick Start

Below is the process for quickly creating an entity in plain C#

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



## 🏗 Key Concepts

### IEntityInstaller

- Базовый интерфейс для конфигурации сущности.
- Определяет метод:

!!!
void Install(IEntity entity);
!!!

- Используется для добавления тегов, значений и поведения в сущность.

### SceneEntityInstaller

- Абстрактный `MonoBehaviour`.
- Применяется, если сущность существует в **сцене Unity**.
- Позволяет привязывать сценовые зависимости (`Transform`, `Animator`, `AudioSource` и т.д.).
- Поддерживает `OnValidate` для обновления конфигурации в редакторе.

### ScriptableEntityInstaller

- Абстрактный `ScriptableObject`.
- Предназначен для **переиспользуемых конфигураций**, которые можно применять к множеству сущностей.
- Не зависит от конкретной сцены и может использоваться как "глобальный шаблон" установки.

### Generic Installers

- `SceneEntityInstaller<E>` и `ScriptableEntityInstaller<E>`
- Обеспечивают **строгую типизацию** и устраняют необходимость ручного кастинга.
- Применяются, если требуется доступ к специфическим свойствам конкретного типа сущности.

---

## 🔄 Lifecycle

1. **Install**

- Вызывается при инициализации сущности.
- Добавляет теги, значения, поведение или подписки.
- Пример:

!!!
entity.AddValue("MoveSpeed", 5.0f);
!!!

2. **Uninstall** *(опционально)*

- Вызывается при уничтожении или отключении сущности.
- Используется для очистки ресурсов, отписки от событий и высвобождения зависимостей.
- По умолчанию не реализован.

---



## 📝 Notes

- **Entity Configuration** – Encapsulates setup routines for entities.
- **Strongly-Typed Option** – `IEntityInstaller<E>` allows type-safe configuration.
- **Composable** – Multiple installers can be applied to the same entity.
- **Integration** – Works in both runtime and editor simulation workflows.
- `IEntityInstaller` is intended for configuring or initializing entities before or during their lifecycle.
- `IEntityInstaller<E>` is useful when the installer is specific to a particular entity type.

## 📝 Notes

- **Scene Configuration** – Attach to a GameObject to configure entities in the scene.
- **Editor Support** – Automatically refreshes when properties are changed in the Inspector.
- **Runtime Installation** – Applies configuration and behaviors during runtime.
- **Strongly-Typed Option** – `SceneEntityInstaller<E>` ensures type-safe installation for specific entity types.
- Supports editor workflows via `OnValidate` to refresh previews or dependent systems.
- Can be combined with other installers or entity behaviors to modularly set up complex entities.
- `SceneEntityInstaller` is intended for configuring or initializing entities **directly in the Unity scene**.
- `SceneEntityInstaller<E>` is useful when the installer is specific to a particular entity type.

## 📝 Notes

- **Shared Configuration** – Use `ScriptableEntityInstaller` for reusable entity setup logic across multiple entities.
- **Strongly-Typed Option** – `ScriptableEntityInstaller<E>` ensures type-safe installation for specific entity types.
- **Runtime & Edit-Time Support** – Can be used in both runtime and editor contexts.
- **Modular** – Can be combined with other installers or entity behaviors to create complex, composable setups.
- `ScriptableEntityInstaller` is intended for **shared and reusable entity configuration**.
- `ScriptableEntityInstaller<E>` is useful when the installer targets a specific entity type.



Each behaviour can handle different events of the entity:

## Lifecycle Events

| Event       | Purpose                                                    |
|-------------|------------------------------------------------------------|
| `Init`      | Initialization of the behaviour when the entity is created |
| `Enable`    | Activating the entity on the scene or in a pool            |
| `Disable`   | Deactivating the entity and returning it to the pool       |
| `Tick`      | Updates every frame (logic, state)                         |
| `FixedTick` | Physics and game mechanics updates with a fixed timestep   |
| `LateTick`  | Updates after rendering (e.g., UI)                         |
| `Dispose`   | Releasing entity resources when it is destroyed            |
