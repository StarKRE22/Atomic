# Entities
- [Requirements](#requirements)
- [Using Odin Inspector](#using-odin-inspector)
- [Общая парадигма]
- [Что такое сущность]
- [Создание сущности на C#]
- [Создание сущности в Unity]
- [Documentation](#documentation)
- [Performance](#performance)
- [Best Practices](#best-practices)


## Общая парадигма

В атомарном фреймворке все игровые объекты и модели представляют собой **сущности** (*Entities*).  
Сущность — это базовая единица описания, через которую формируется любая логика приложения.

С помощью сущностей можно описывать:
- **Игровые объекты** (например, персонажи, оружие, предметы);
- **Игровые системы** (состояния мира, правила, механики);
- **UI-контексты** (окна, меню, экраны);
- **Контексты приложения** (сохранения, настройки, конфигурации).

Иными словами, все главные модели игры — это сущности.


## Что такое сущность

- **Everything is Entities**  
  Everything in Atomic is represented as **entities** that hold values (state) and behaviours (logic).  
  Entities can represent anything: characters, UI, contexts, or systems.

- **Entity-State-Behaviour Pattern**  
  Each entity acts as a **container** that holds **data** and **behaviours**, keeping them strictly separated.
    - **Data** consists of structures or objects that represent the state of the entity.
    - **Behaviour** consists of pure controllers or logic methods that operate on the data.

  Entities can have multiple behaviours bound to them, which can be **activated or deactivated dynamically** depending on the state.  
  This strict separation of state and logic allows for **clearer architecture**, reducing complexity, improving testability, easier debugging, and more       flexible interactions between game objects.


Сущность (**Entity**) в атомарном фреймворке представляет собой **контейнер**, в котором раздельно хранятся:

- **Данные** — реактивные структуры и объекты, описывающие состояние.
- **Логика** — контроллеры (*Behaviours*), которые обрабатывают данные и управляют ими.

Такое разделение образует паттерн **Entity–State–Behaviour**:

- **Entity** — контейнер, связывающий данные и логику;
- **State (данные)** — реактивное состояние, описывающее модель;
- **Behaviour (логика)** — прикреплённые контроллеры, управляющие состоянием в рамках жизненного цикла сущности.

Таким образом, сущность — это не просто объект, а **связка состояния и поведения**, которая живёт, обновляется и утилизируется в соответствии со своим жизненным циклом.
 


## Пример: создание сущности персонажа на С#

Ниже показан простой пример, как можно описать игрового персонажа с помощью сущности.

```csharp
// Создаём новую сущность
var character = new Entity("Character");

// Добавляем теги
character.AddTag("Moveable");

// Добавляем свойства
character.AddValue("Position", new ReactiveVariable<Vector3>());
character.AddValue("MoveSpeed", new Const<float>(3.5f));
character.AddValue("MoveDirection", new ReactiveVariable<Vector3>());

// Контроллер, который отвечает за движение сущности
public sealed class MoveBehaviour : IEntityInit, IEntityUpdate
{
    private IVariable<Vector3> _position;
    private IValue<float> _moveSpeed;
    private IValue<Vector3> _moveDirection;

    // Вызывается при Entity.Init()
    public void Init(IEntity entity)
    {
        _position = entity.GetValue<IVariable<Vector3>>("Position");
        _moveSpeed = entity.GetValue<IValue<float>>("MoveSpeed");
        _moveDirection = entity.GetValue<IValue<Vector3>>("MoveDirection");
    }

    // Вызывается при Entity.OnUpdate()
    public void Update(IEntity entity, float deltaTime)
    {
        Vector3 direction = _moveDirection.Value;
        if (direction != Vector3.zero) 
            _position.Value += _moveSpeed.Value * deltaTime * direction;
    }
}

// Подключаем поведение к сущности
character.AddBehaviour<MoveBehaviour>();


более подробно про сущность Entity можно прочитать тут!
```

## Жизненный цикл сущности

Сущность существует в рамках определённого жизненного цикла.  
Основные стадии:

1. **Init** — инициализация сущности и её контроллеров;
2. **Enable** — активация, включение в игровой процесс;
3. **Update** — обновление на каждом игровом кадре;
4. **Disable** — временная деактивация;
5. **Dispose** — освобождение ресурсов и завершение работы.

Пример использования:

```csharp

// 1. Инициализация (вызывает IEntitySpawn, IEntityInit и т.д.)
character.Init();

// 2. Активация (разрешает обновления и события)
character.Enable();

// 3. Обновление в игровом цикле
const float deltaTime = 0.02f;

while (_isGameRunning)
{
    // Вызывает IEntityUpdate у всех behaviours
    character.Update(deltaTime);
}

// 4. Деактивация (сущность временно выключена из игры)
character.Disable();

// 5. Освобождение ресурсов (сущность уничтожена)
character.Dispose();
```

## Unity Quick Start

1. **Create a GameObject in a scene**

   <img width="360" height="255" alt="GameObject creation" src="https://github.com/user-attachments/assets/463a721f-e50d-4cb7-86be-a5d50a6bfa17" />

2. **Add the `Entity` component to the GameObject**

   <img width="464" height="346" alt="Entity component" src="https://github.com/user-attachments/assets/f74644ba-5858-4857-816e-ea47eed0e913" />

3. **Create a `CharacterInstaller` script**

 ```csharp
//Populates entity with data and behaviours
public sealed class CharacterInstaller : SceneEntityInstaller
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Const<float> _moveSpeed = 5.0f; //Immutable variable
    [SerializeField] private ReactiveVariable<Vector3> _moveDirection; //Mutable variable with subscription

    public override void Install(IEntity entity)
    {
        //Add tags to the character
        entity.AddTag("Character");
        entity.AddTag("Moveable");

        //Add properties to the character
        entity.AddValue("Transform", _transform);
        entity.AddValue("MoveSpeed", _moveSpeed);
        entity.AddValue("MoveDirection", _moveDirection);
    }
}
```
4. **Attach the `CharacterInstaller` to the `GameObject` and configure it**  
   <img width="464" height="153" alt="изображение" src="https://github.com/user-attachments/assets/1967b1d8-b6b7-41c7-85db-5d6935f6443e" />

5. **Create a `MoveBehaviour` class**
```csharp
//Controller that moves entity by its direction
public sealed class MoveBehaviour : IEntityInit, IEntityFixedUpdate
{
    private Transform _transform;
    private IValue<float> _moveSpeed;
    private IValue<Vector3> _moveDirection;

    //Calls when MonoBehaviour.Start() is called
    public void Init(IEntity entity)
    {
        _transform = entity.GetValue<Transform>("Transform");
        _moveSpeed = entity.GetValue<IValue<float>>("MoveSpeed");
        _moveDirection = entity.GetValue<IValue<Vector3>>("MoveDirection");
    }

    //Calls when MonoBehaviour.FixedUpdate() is called
    public void FixedUpdate(IEntity entity, float deltaTime)
    {
        Vector3 direction = _moveDirection.Value;
        if (direction != Vector3.zero) 
            _transform.position += _moveSpeed.Value * deltaTime * direction;
    }
}
```
6. **Add `MoveBehaviour` to `CharacterInstaller`**
 ```csharp
//Populates entity with data and behaviours
public sealed class CharacterInstaller : SceneEntityInstaller
{
     ...previous code

    public override void Install(IEntity entity)
    {
        ...previous code

        //+
        entity.AddBehaviour<MoveBehaviour>();
    }
}
```
7. **Enter `PlayMode` and test your character movement**


более подробно можно про SceneEntity прочитать тут!

## Теперь давай поговорим про жизненный цикл









