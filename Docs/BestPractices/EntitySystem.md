# 📌 Entity System с разделением Model & View

---

## 📖 Overview

В этом разделе рассматривается создание **Entity системы**, где **C# модель отделена от Unity-окружения**, а Unity
используется исключительно для визуализации.

С помощью [Atomic.Entities](../Entities/Manual.md) это реализуется удобно и масштабируемо:

- Юниты представлены специфичным типом, например `UnitEntity`.
- Юниты создаются через [ScriptableEntityFactory\<E>](../Entities/Factories/ScriptableEntityFactory%601.md) и берутся из
  пула [EntityPool\<E>](../Entities/Pooling/EntityPool%601.md).
- Визуализация осуществляется
  через [EntityView\<E>](../Entities/UI/EntityView%601.md), [EntityViewCollection\<E>](../Entities/UI/EntityCollectionView%601.md)
  и [EntityViewPool\<E>](../Entities/UI/EntityViewPool%601.md).

---

## 🗂 Базовый тип сущности

Базовая сущность в **Atomic.Entities** — это фундамент, на котором строится вся система юнитов. Она объединяет
**данные**, **теги**, **значения** и **поведения**, оставаясь полностью абстрактной и не завися напрямую от Unity.

### 1️⃣ Интерфейс сущности

```csharp
// Контракт сущности
public interface IUnitEntity : IEntity
{
}
```

**Пояснения:**

- `IUnitEntity` наследуется от `IEntity` — базового контракта для всех сущностей в AtomArch.
- Интерфейс служит **точкой расширения**: через него можно добавлять методы и свойства, специфичные для юнитов, не меняя
  базовую архитектуру.

### 2️⃣ Реализация UnitEntity

```csharp
public sealed class UnitEntity : Entity, IUnitEntity
{
    public UnitEntity(
        string name = null,
        int tagCapacity = 0,
        int valueCapacity = 0,
        int behaviourCapacity = 0
    ) : base(name, tagCapacity, valueCapacity, behaviourCapacity)
    {
    }
}
```

**Пояснения:**

- Наследуемся от `Entity` — базового класса с поддержкой тегов, значений и поведения.
- **Capacity-based конструктор** позволяет заранее выделять память под теги, значения и поведения, что оптимизирует
  производительность на этапе редактора и при массовом создании юнитов.
- `name` используется для идентификации сущности и поиска в пулах и коллекциях.

> 💡 Такой подход отделяет **логику сущности** от визуализации и других компонентов Unity, облегчая тестирование и
> расширение системы.
---

## 🏭 Фабрика для сущности

Фабрика отвечает за **создание и конфигурацию сущностей**. Она позволяет централизованно задавать базовые параметры
юнита, устанавливать начальные значения, подключать поведения и теги. Использование фабрик делает код **масштабируемым,
повторно используемым и удобным для тестирования**.

### 1️⃣ Базовая фабрика UnitFactory

```csharp
public abstract class UnitFactory : ScriptableEntityFactory<IUnitEntity>
{
    public string Name => this.name;

    public sealed override IUnitEntity Create()
    {
        var entity = new UnitEntity(
            this.Name,
            this.initialTagCapacity,
            this.initialValueCapacity,
            this.initialBehaviourCapacity
        );
        this.Install(entity);
        return entity;
    }

    protected abstract void Install(IUnitEntity entity);
}
```

**Пояснения:**

- Наследуемся от `ScriptableEntityFactory<IUnitEntity>` — базовая фабрика для ScriptableObject, создающего сущности.
- `Create()` создаёт экземпляр `UnitEntity` с указанными capacity и именем.
- Метод `Install()` вызывается для **дополнительной конфигурации сущности**: добавление тегов, значений, поведения. Он
  абстрактный, чтобы каждая конкретная фабрика реализовала свою логику.
- Свойство `Name` удобно использовать для идентификации и поиска сущностей.

---

### 2️⃣ Примеры конкретных фабрик

#### 🔹 Фабрика для танка

```csharp
[CreateAssetMenu(
    fileName = "TankFactory", 
    menuName = "Example/New TankFactory" 
)]
public sealed class TankFactory : UnitFactory
{
    [SerializeField] private TransformEntityInstaller _transformInstaller;
    [SerializeField] private MoveEntityInstaller _moveInstaller;
    [SerializeField] private LifeEntityInstaller _lifeInstaller;
    [SerializeField] private RangeCombatEntityInstaller _combatInstaller;
    [SerializeField] private AIEntityInstaller _aiInstaller;

    protected override void Install(IUnitEntity entity)
    {
        entity.AddUnitTag(); // добавляем тег "Unit"
        entity.AddTeam(new ReactiveVariable<TeamType>()); // команда

        // Установка поведения через EntityInstallers
        entity.Install(_transformInstaller);
        entity.Install(_moveInstaller);
        entity.Install(_lifeInstaller);
        entity.Install(_combatInstaller);
        entity.Install(_aiInstaller);
    }
}
```

**Пояснения:**

- Фабрика создаёт танка и сразу настраивает его поведение: движение, жизнь, бой, AI и трансформацию.
- Использование **EntityInstallers** упрощает модульное подключение поведения к сущности.

#### 🔹 Фабрика для солдатика

```csharp
[CreateAssetMenu(
    fileName = "SoliderFactory", 
    menuName = "Example/New SoliderFactory" 
)]
public sealed class SoliderFactory : UnitFactory
{
    [SerializeField] private TransformEntityInstaller _transformInstaller;
    [SerializeField] private MoveEntityInstaller _moveInstaller;
    [SerializeField] private LifeEntityInstaller _lifeInstaller;
    [SerializeField] private MeleeCombatEntityInstaller _meleeCombatInstaller;
    [SerializeField] private AIEntityInstaller _aiInstaller;

    protected override void Install(IUnitEntity entity)
    {
        entity.AddUnitTag();
        entity.AddTeam(new ReactiveVariable<TeamType>());

        entity.Install(_transformInstaller);
        entity.Install(_moveInstaller);
        entity.Install(_lifeInstaller);
        entity.Install(_meleeCombatInstaller);
        entity.Install(_aiInstaller);
    }
}
```

**Пояснения:**

- Подобно фабрике танка, но подключает **ближний бой** (`MeleeCombatEntityInstaller`) вместо дальнего.
- Легко масштабируется для создания разных типов юнитов с минимальным дублированием кода.

---

### 🔧 Создание ассетов фабрик в Unity

После создания фабрик в проекте можно создать ассеты через `Create > Example > New TankFactory / New SoliderFactory`.  
Эти ассеты будут использоваться для **мультифабрики и пула сущностей**.

<img src="../Images/UnitsFactories.png" alt="Inspector setup example" width="200">

> 💡 Такой подход позволяет быстро создавать и конфигурировать новые типы юнитов без изменения кода, просто добавляя
> новые фабрики.

---

## 🗂 Мульти-Фабрика

Мульти-фабрика служит **каталогом для всех фабрик юнитов**, позволяя быстро искать нужную фабрику по ключу (например, по
имени юнита). Она объединяет несколько фабрик в единый интерфейс для удобного доступа и управления.

---

### Пример реализации

```
csharp
[CreateAssetMenu(fileName = "UnitCatalog", menuName = "Example/New UnitCatalog")]
public sealed class UnitMultiFactory : ScriptableMultiEntityFactory<string, IUnitEntity, UnitFactory>
{
protected override string GetKey(UnitFactory factory) => factory.Name;
}
```

**Пояснения:**

- Наследуемся от `ScriptableMultiEntityFactory<TKey, TEntity, TFactory>` — это позволяет хранить **несколько фабрик в
  одном ScriptableObject**.
- `GetKey` возвращает уникальный идентификатор для каждой фабрики (например, `Name`), который используется при
  извлечении сущности из пула.
- С помощью мульти-фабрики легко управлять большим количеством юнитов в игре, добавляя новые фабрики без изменения кода.

---

### Использование в Unity

После создания мульти-фабрики все ваши фабрики можно добавить в каталог через Inspector:

<img src="../Images/UnitCatalog.png" alt="Inspector setup example" width="400">

> 💡 Каталог мульти-фабрик облегчает работу с большим количеством типов юнитов, делая систему **масштабируемой и удобной
для дизайнеров**.

---

## 🏗 Развертка системы сущностей

В этом разделе рассматривается **инициализация и настройка системы юнитов** через `GameContext`. Здесь мы создаем пул
сущностей, мир для их управления и подключаем жизненный цикл через контекст игры.

### Пример реализации

```csharp
[Serializable]
public sealed class UnitSystemInstaller : IEntityInstaller<IGameContext>
{
    [SerializeField] 
    private UnitMultiFactory factoryCatalog;

    public void Install(IGameContext context)
    {
        // 1️⃣ Создаем мультипул для юнитов, используя наш каталог фабрик
        var entityPool = new MultiEntityPool<string, IUnitEntity>(factoryCatalog);
        context.AddEntityPool(entityPool);

        // 2️⃣ Создаем мир для активных сущностей
        var entityWorld = new EntityWorld<IUnitEntity>();
        context.AddEntityWorld(entityWorld);

        // 3️⃣ Подключаем жизненный цикл мира к контексту игры
        context.WhenInit(entityWorld.InitEntities);
        context.WhenEnable(entityWorld.Enable);
        context.WhenTick(entityWorld.Tick);
        context.WhenFixedTick(entityWorld.FixedTick);
        context.WhenLateTick(entityWorld.LateTick);
        context.WhenDisable(entityWorld.Disable);
        context.WhenDispose(entityWorld.DisposeEntities);
        context.WhenDispose(entityWorld.Dispose);
    }
}
```

---

### Пояснения:

- **MultiEntityPool** — пул для управления экземплярами юнитов, создаваемых фабриками. Позволяет переиспользовать
  объекты, минимизируя аллокации.
- **EntityWorld** — управляет жизненным циклом всех активных сущностей: их инициализацией, обновлением и отключением.
- **Контекст игры (`IGameContext`)** — связывает мир и пул с игровым циклом, подписывая методы `Init`, `Enable`, `Tick`,
  `FixedTick`, `LateTick`, `Disable` и `Dispose` на соответствующие события игры.
- Такой подход обеспечивает **полное разделение C# логики и Unity-визуализации**, позволяя масштабировать систему и
  легко интегрировать новые типы юнитов.

---

## ⚡ Спавн и деспаун сущностей

Одним из ключевых элементов системы является **спаун и деспаун сущностей**. Основная идея:  
берем сущность из пула, настраиваем её свойства и добавляем в `EntityWorld`. При этом:

- При первом добавлении вызываются `Entity.Init()` и `Entity.Enable()`.
- При удалении из мира вызывается `Entity.Disable()`.
- Все активные сущности управляются через `Update`/`FixedUpdate` и другие циклы `EntityWorld`.

---

### Пример реализации

```csharp
public static class UnitsUseCase
{
    public static IUnitEntity Spawn(
        IGameContext context,
        string name,
        Vector3 position,
        Quaternion rotation,
        TeamType team
    )
    {
        // Берем сущность из пула по имени
        var pool = context.GetEntityPool();
        var entity = pool.Rent(name);

        // Настраиваем положение, поворот и команду
        entity.GetPosition().Value = position;
        entity.GetRotation().Value = rotation;
        entity.GetTeam().Value = team;

        // Добавляем в мир, где будет обновляться
        context.GetEntityWorld().Add(entity);
        return entity;
    }
    
    public static bool Despawn(IGameContext context, IUnitEntity entity)
    {
        // Убираем из мира
        if (!context.GetEntityWorld().Remove(entity))
            return false;

        // Возвращаем в пул для переиспользования
        context.GetEntityPool().Return(entity);
        return true;
    }
}
```

---

### Пример использования

```csharp
// Спавн юнита для строительства
UnitsUseCase.Spawn(context, "Builder", position, rotation, TeamType.Player);

// Деспаун юнита при его уничтожении
UnitsUseCase.Despawn(context, unitEntity);
```

---

## Визуализация сущностей

Раздел посвящён визуализации юнитов и других сущностей в системе. Здесь мы используем **EntityView**,  
**EntityViewCatalog**, **EntityViewPool** и **EntityCollectionView** для удобного управления визуальными представлениями
объектов.

---

### 🗂 Базовый тип визуализации

Создадим базовый класс визуализации юнита, наследуемый от `EntityView<IUnitEntity>`:

```csharp
public class UnitView : EntityView<IUnitEntity>
{
}
```

> 💡 Этот класс связывает **C# модель** (`IUnitEntity`) с визуальной частью в Unity.

---

### 🏷 Каталог для визуализации

Для хранения всех визуальных представлений создаём каталог, наследуя `EntityViewCatalog`:

```csharp
using Atomic.Entities;
[CreateAssetMenu(
    fileName = "UnitViewCatalog",
    menuName = "Example/New UnitViewCatalog"
)]
public sealed class UnitViewCatalog : EntityViewCatalog<IUnitEntity, UnitView>
{
}
```

> 💡 Каталог позволяет **по ключу находить соответствующий визуальный объект** для сущности.

---

### 🏗 Пул визуальных объектов

Чтобы эффективно управлять визуальными объектами, создаём пул:

```csharp
public sealed class UnitViewPool : EntityViewPool<IUnitEntity, UnitView>
{
}
```

> 💡 `EntityViewPool` позволяет **переиспользовать объекты**, избегая постоянного создания и уничтожения GameObject-ов,
> что особенно важно для RTS или массовых сцен.

---

### 📦 Коллекция визуальных объектов

Для управления коллекцией юнитов и их визуализацией используется `EntityCollectionView`:

```csharp
public sealed class UnitCollectionView : EntityCollectionView<IUnitEntity, UnitView>
{
}
```

> ⚡ Коллекция автоматически синхронизирует объекты сцены с сущностями, добавляемыми или удаляемыми из `EntityWorld`.

---

## ✅ Итог

Используя комбинацию:

- `EntityView<IUnitEntity>`
- `EntityViewCatalog<IUnitEntity, UnitView>`
- `EntityViewPool<IUnitEntity, UnitView>`
- `EntityCollectionView<IUnitEntity, UnitView>`

мы получаем **эффективную и модульную систему визуализации**, которая:

- Автоматически синхронизирует модель и вид.
- Позволяет переиспользовать объекты через пул.
- Упрощает управление большим количеством юнитов в игре.
- Поддерживает расширяемость и легко интегрируется с `UnitFactory` и `EntityWorld`.

---

## ✅ Вывод

Используя **Atomic.Entities**, мы получаем:

- Разделение C# модели и Unity визуализации.
- Масштабируемые и тестируемые системы юнитов.
- Гибкую и модульную архитектуру с **пулами, фабриками и коллекциями EntityView**.
- Чистый код с минимальной зависимостью от Unity API.
- Возможность быстро добавлять новые типы юнитов, их поведение и визуализацию без дублирования кода.