
<!--

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







## 🏗 Key Concepts

### IEntityInstaller

- Базовый интерфейс для конфигурации сущности.
- Определяет метод:
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






BAKING

-->