# 📌 Entity System с разделением C# Model & Unity Visualization

---

## 📖 Overview

Раздел посвящён созданию **Entity системы**, где **C# модель отделена от Unity-окружения**, а Unity используется
исключительно для визуализации. С помощью **AtomArch** это реализуется легко и удобно.

Идея проста: у нас есть **специфичный тип `UnitEntity`**, представляющий всех юнитов в игре. Юниты берутся из пула
через [EntityPool\<E>](../Entities/Pooling/EntityPool%601.md), создаются с
помощью [ScriptableEntityFactory\<E>](../Entities/Factories/ScriptableEntityFactory%601.md), а для визуализации
используются [EntityView\<E>](../Entities/UI/EntityView%601.md), [EntityViewCollection\<E>](../Entities/UI/EntityCollectionView%601.md)
и [EntityViewPool\<E>](../Entities/UI/EntityViewPool%601.md).

---

## Базовый тип сущности

Допустим у нас есть интерфейс сущности и ее реализация на C#. Расширим интерфейс [IEntity](../Entities/Entities/IEntity.md)

```csharp
// Контракт сущности
public interface IUnitEntity : IEntity
{
}
```

Есть реализация сущности c capacity-based конструктором, чтобы можно было делать предпросчет размера памяти на этапе редактора
Унаследуемся от [Entity](../Entities/Entities/Entity.md):

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

---

## Фабрика для сущности

Далее нам понадобиться реализовать базовый тип фабрики для наших юнитов, чтобы потом можно было создавать конкретные объекты
Для этого унаследуемся от [ScriptableEntityFactory\<E>](../Entities/Factories/ScriptableEntityFactory%601.md)


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

Ниже приведу, пару примеров фабрик, которые будут создавать танк и солдатика:

1. Фабрика для танка

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
        entity.AddUnitTag();
        entity.AddTeam(new ReactiveVariable<TeamType>());
        
        entity.Install(_transformInstaller);
        entity.Install(_moveInstaller);
        entity.Install(_lifeInstaller);
        entity.Install(_combatInstaller);
        entity.Install(_aiInstaller);
    }
}
```

2. Фабрика для солдатика

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

Теперь создадим ассеты фабрик в проекте:

<img src="../Images/UnitsFactories.png" alt="Inspector setup example" width="200" height="">

---

## Мульти-Фабрика для сущностей

Теперь нужен каталог, куда мы можем положить все это
Для этого создадим мультифабрику

```csharp
[CreateAssetMenu(
    fileName = "UnitCatalog",
    menuName = "Example/New UnitCatalog"
)]
public sealed class UnitMultiFactory : ScriptableMultiEntityFactory<string, IUnitEntity, UnitFactory>
{
    protected override string GetKey(UnitFactory factory) => factory.Name;
}
```

Добавляем все наши фабрики с юнитами

<img src="../Images/UnitCatalog.png" alt="Inspector setup example" width="400" height="">

---

## Развертка системы сущностей

Теперь настало время развертки системы. Это можно сделать в `GameContext`'е



```csharp
[Serializable]
public sealed class UnitSystemInstaller : IEntityInstaller<IGameContext>
{
    [SerializeField]
    private UnitMultiFactory factoryCatalog;

    public void Install(IGameContext context)
    {
        // Создадим мультипул для наших юнитов, в который положим нашу фабрику
        var entityPool = new MultiEntityPool<string, IUnitEntity>(factoryCatalog)
        context.AddEntityPool(entityPool);

        // Создадим мир, где будут жить наши активные сущности
        EntityWorld<IUnitEntity> entityWorld = new EntityWorld<IUnitEntity>();
        context.AddEntityWorld(entityWorld);

        // Чтобы крутить мир, через контекст игры мы сделаем вот так:
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

## Реализация спауна и деспауна сущностей

Теперь самая главная фишка, спаун и деспаун сущнсостей, по сути нужно просто брать сущности из пула, настраивать их и добавлять в мир! Особенность заключается в том, что когда мы добавляем наши сущностти в мир, то у них вызывается Entity.Enable() (и Entity.Init()) в первы     раз. И при деспауне Entity.Disable(), таким образом в Update у нас крутяться сущности, которые находястя в мире

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
        IMultiEntityPool<string, IUnitEntity> pool = context.GetEntityPool();
        IUnitEntity entity = pool.Rent(name);
        entity.GetPosition().Value = position;
        entity.GetRotation().Value = rotation;
        entity.GetTeam().Value = team;
        context.GetEntityWorld().Add(entity);
        return entity;
    }
    
    public static bool Despawn(IGameContext gameContext, IUnitEntity entity)
    {
        if (!gameContext.GetEntityWorld().Remove(entity))
            return false;
    
        gameContext.GetEntityPool().Return(entity);
        return true;
    }
}
```

Теперь использование

```csharp
Постройка здания через UnitsUseCase.Spawn(), 
    
Уничтожение юнита при его гибели.
```

Дополнительные фичи для изучения EntityBakers & EntityFilters

---






Раздел 2. Визуализация сущностей

### Визуализация юнита базовфй тип сущности

public class UnitView : EntityView<IUnitEntity>
{
}

### Каталог для сущностей


```csharp
[CreateAssetMenu(
    fileName = "UnitViewCatalog",
    menuName = "Example/New UnitViewCatalog"
)]
public sealed class UnitViewCatalog : EntityViewCatalog<IUnitEntity, UnitView>
{
}
```

```csharp
using Atomic.Entities;

namespace RTSGame
{
    public sealed class UnitViewPool : EntityViewPool<IUnitEntity, UnitView>
    {
    }
}
```

```csharp
using Atomic.Entities;

namespace RTSGame
{
    public sealed class UnitCollectionView : EntityCollectionView<IUnitEntity, UnitView>
    {
    }
}
```

---

### 3. Запекание и установка через SceneEntityBaker

```csharp
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public abstract class UnitBaker : SceneEntityBaker<IUnitEntity>
    {
        [SerializeField]
        private UnitFactory _factory;

        protected sealed override IUnitEntity Create()
        {
            IUnitEntity entity = _factory.Create();
            this.Install(entity);
            return entity;
        }

        protected abstract void Install(IUnitEntity entity);
    }
}
```

---

### 4. Интерфейс UnitEntity

```csharp
using Atomic.Entities;

namespace RTSGame
{
    public interface IUnitEntity : IEntity
    {
    }
}
```

---

## 🗂 Примеры использования

### 4.1 TankFactory — установка компонентов логики

```csharp
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
   
}
```

### 4.2 TankViewInstaller — установка визуальных компонентов

```csharp
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class TankViewInstaller : SceneEntityInstaller<IUnitEntity>
    {
        [SerializeField] private TakeDamageViewBehaviour _takeDamageBehaviour;
        [SerializeField] private PositionViewBehaviour _positionBehaviour;
        [SerializeField] private RotationViewBehaviour _rotationBehaviour;
        [SerializeField] private TeamColorViewBehaviour _teamColorBehaviour;
        [SerializeField] private WeaponRecoilViewBehaviour _weaponRecoilBehaviour;

        public override void Install(IUnitEntity entity)
        {
            entity.AddBehaviour(_takeDamageBehaviour);
            entity.AddBehaviour(_positionBehaviour);
            entity.AddBehaviour(_rotationBehaviour);
            entity.AddBehaviour(_teamColorBehaviour);
            entity.AddBehaviour(_weaponRecoilBehaviour);
        }

        public override void Uninstall(IUnitEntity entity)
        {
            entity.DelBehaviour(_takeDamageBehaviour);
            entity.DelBehaviour(_positionBehaviour);
            entity.DelBehaviour(_rotationBehaviour);
            entity.DelBehaviour(_teamColorBehaviour);
            entity.DelBehaviour(_weaponRecoilBehaviour);
        }
    }
}
```

---

## 🏁 Вывод

Используя **AtomArch**, мы можем:

- Разделить **чистую C# модель** от Unity-окружения.
- Работать с юнитами через **EntityPool** и **ScriptableEntityFactory**, создавая **масштабируемые игровые системы**.
- Настраивать визуализацию через **EntityView, EntityViewPool и EntityCollectionView**, не смешивая логику и рендеринг.
- Использовать **SceneEntityBaker** для удобного “запекания” сущностей и компонентов в сцену.
- Создавать **расширяемые, поддерживаемые и тестируемые игровые системы** с минимальной зависимостью от Unity API.

Эта архитектура идеально подходит для **RTS, RPG и стратегических игр**, где необходимо управлять большим количеством
игровых объектов, сохраняя при этом чистоту и модульность кода.
