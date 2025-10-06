# 🧩 Entity API

Представляет собой модуль расширения для работы с тэгами и значениями для сущностей

## Оглавление

- Проблематика
- Генерация API
    - Генерация через Unity Editor
    - Генерация через Rider Plugin
- API Reference

## Проблематика

Когда разработчик работает с тегами и значениями, то ему нужно обращаться через `int` ключ или строки `string`. Пример
ниже:

```csharp
//Define tag keys
const int Player tag = 1;
const int NPC tag = 2;
const int Ally ally = 3;
const int Merchant ally = 4;

Entity entity = new Entity();

entity.AddTag(Player);
entity.AddTag(NPC);
```

```csharp
//Define value keys 
const int Health = 1;
const int Speed = 2;
const int Inventory = 3;

// Create a new instance of entity
Entity entity = new Entity();

// Subscribe to value events
entity.OnValueChanged += (e, key) => Console.WriteLine($"Value {key} changed");

//Add health property
entity.AddValue(Health, 100);

//Add speed property
entity.AddValue(Speed, 12.5f);

//Add inventory property
entity.AddValue(Inventory, new Inventory());

// Get a value
int health = entity.GetValue<int>(Health);
Console.WriteLine($"Health: {health}");

// Update a Health
entity.SetValue(Health, 150);

// Remove a Speed value
entity.DelValue(Speed);
```

```csharp
// Add tags by string name
entity.AddTag("Player");
entity.AddTag("NPC");

// Remove a tag
entity.DelTag("NPC");
```

```csharp
// Create a new instance of entity
Entity entity = new Entity();

// Add values by string key
entity.AddValue("Health", 100);
entity.AddValue("Speed", 12.5f);
entity.AddValue("Inventory", new Inventory());

// Get a value
int health = entity.GetValue<int>("Health");
Console.WriteLine($"Health: {health}");

// Update a value
entity.SetValue("Health", 150);

// Remove a value
entity.DelValue("Inventory");
```

Все это порождает хард код и магические константы, которые жестко прибиты в коде. И конечно, поддерживать и рефакторить
такой код становится крайне сложно. К тому же разработчик должен откуда-то знать какого типа выступает то или иное
значение, что могло тоже приводить к ошибкам во время выполнения программы

Таким образом, было принято решение использовать кодогенерацию, которая бы генерировала Extension методы, которые бы
давали подсказки разработчику в IDE и избавляли его от магических констант, хардкода и знание возвращаемого типа со
строгой типизацией. Выглядит это вот так

```csharp
/**
* Code generation. Don't modify! 
**/

using Atomic.Entities;
using static Atomic.Entities.EntityNames;
using System.Runtime.CompilerServices;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Atomic.Elements;
using BeginnerGame;

namespace BeginnerGame
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public static class GameEntityAPI
	{

		///Tags
		public static readonly int Character;
		public static readonly int Coin;

		///Values
		public static readonly int Position; // IVariable<Vector3>
		public static readonly int Rotation; // IVariable<Quaternion>
		public static readonly int MoveSpeed; // IValue<float>
		public static readonly int MoveDirection; // IVariable<Vector3>
		public static readonly int RotationSpeed; // IValue<float>
		public static readonly int RotationDirection; // IVariable<Vector3>
		public static readonly int TeamType; // IReactiveVariable<TeamType>
		public static readonly int TriggerEvents; // TriggerEvents
		public static readonly int Money; // IValue<int>
		public static readonly int Renderer; // Renderer
		public static readonly int MoneyView; // MoneyView

		static GameEntityAPI()
		{
			//Tags
			Character = NameToId(nameof(Character));
			Coin = NameToId(nameof(Coin));

			//Values
			Position = NameToId(nameof(Position));
			Rotation = NameToId(nameof(Rotation));
			MoveSpeed = NameToId(nameof(MoveSpeed));
			MoveDirection = NameToId(nameof(MoveDirection));
			RotationSpeed = NameToId(nameof(RotationSpeed));
			RotationDirection = NameToId(nameof(RotationDirection));
			TeamType = NameToId(nameof(TeamType));
			TriggerEvents = NameToId(nameof(TriggerEvents));
			Money = NameToId(nameof(Money));
			Renderer = NameToId(nameof(Renderer));
			MoneyView = NameToId(nameof(MoneyView));
		}


		///Tag Extensions

		#region Character

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCharacterTag(this IGameEntity entity) => entity.HasTag(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCharacterTag(this IGameEntity entity) => entity.AddTag(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCharacterTag(this IGameEntity entity) => entity.DelTag(Character);

		#endregion

		#region Coin

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCoinTag(this IGameEntity entity) => entity.HasTag(Coin);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCoinTag(this IGameEntity entity) => entity.AddTag(Coin);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCoinTag(this IGameEntity entity) => entity.DelTag(Coin);

		#endregion


		///Value Extensions

		#region Position

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IVariable<Vector3> GetPosition(this IGameEntity entity) => entity.GetValue<IVariable<Vector3>>(Position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetPosition(this IGameEntity entity, out IVariable<Vector3> value) => entity.TryGetValue(Position, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddPosition(this IGameEntity entity, IVariable<Vector3> value) => entity.AddValue(Position, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasPosition(this IGameEntity entity) => entity.HasValue(Position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelPosition(this IGameEntity entity) => entity.DelValue(Position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPosition(this IGameEntity entity, IVariable<Vector3> value) => entity.SetValue(Position, value);

		#endregion

		#region Rotation

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IVariable<Quaternion> GetRotation(this IGameEntity entity) => entity.GetValue<IVariable<Quaternion>>(Rotation);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRotation(this IGameEntity entity, out IVariable<Quaternion> value) => entity.TryGetValue(Rotation, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRotation(this IGameEntity entity, IVariable<Quaternion> value) => entity.AddValue(Rotation, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRotation(this IGameEntity entity) => entity.HasValue(Rotation);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRotation(this IGameEntity entity) => entity.DelValue(Rotation);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotation(this IGameEntity entity, IVariable<Quaternion> value) => entity.SetValue(Rotation, value);

		#endregion

		#region MoveSpeed

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetMoveSpeed(this IGameEntity entity) => entity.GetValue<IValue<float>>(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoveSpeed(this IGameEntity entity, out IValue<float> value) => entity.TryGetValue(MoveSpeed, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoveSpeed(this IGameEntity entity, IValue<float> value) => entity.AddValue(MoveSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveSpeed(this IGameEntity entity) => entity.HasValue(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveSpeed(this IGameEntity entity) => entity.DelValue(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoveSpeed(this IGameEntity entity, IValue<float> value) => entity.SetValue(MoveSpeed, value);

		#endregion

		#region MoveDirection

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IVariable<Vector3> GetMoveDirection(this IGameEntity entity) => entity.GetValue<IVariable<Vector3>>(MoveDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoveDirection(this IGameEntity entity, out IVariable<Vector3> value) => entity.TryGetValue(MoveDirection, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoveDirection(this IGameEntity entity, IVariable<Vector3> value) => entity.AddValue(MoveDirection, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveDirection(this IGameEntity entity) => entity.HasValue(MoveDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveDirection(this IGameEntity entity) => entity.DelValue(MoveDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoveDirection(this IGameEntity entity, IVariable<Vector3> value) => entity.SetValue(MoveDirection, value);

		#endregion

		#region RotationSpeed

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetRotationSpeed(this IGameEntity entity) => entity.GetValue<IValue<float>>(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRotationSpeed(this IGameEntity entity, out IValue<float> value) => entity.TryGetValue(RotationSpeed, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRotationSpeed(this IGameEntity entity, IValue<float> value) => entity.AddValue(RotationSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRotationSpeed(this IGameEntity entity) => entity.HasValue(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRotationSpeed(this IGameEntity entity) => entity.DelValue(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotationSpeed(this IGameEntity entity, IValue<float> value) => entity.SetValue(RotationSpeed, value);

		#endregion

		#region RotationDirection

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IVariable<Vector3> GetRotationDirection(this IGameEntity entity) => entity.GetValue<IVariable<Vector3>>(RotationDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRotationDirection(this IGameEntity entity, out IVariable<Vector3> value) => entity.TryGetValue(RotationDirection, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRotationDirection(this IGameEntity entity, IVariable<Vector3> value) => entity.AddValue(RotationDirection, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRotationDirection(this IGameEntity entity) => entity.HasValue(RotationDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRotationDirection(this IGameEntity entity) => entity.DelValue(RotationDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotationDirection(this IGameEntity entity, IVariable<Vector3> value) => entity.SetValue(RotationDirection, value);

		#endregion

		#region TeamType

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<TeamType> GetTeamType(this IGameEntity entity) => entity.GetValue<IReactiveVariable<TeamType>>(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTeamType(this IGameEntity entity, out IReactiveVariable<TeamType> value) => entity.TryGetValue(TeamType, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTeamType(this IGameEntity entity, IReactiveVariable<TeamType> value) => entity.AddValue(TeamType, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTeamType(this IGameEntity entity) => entity.HasValue(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTeamType(this IGameEntity entity) => entity.DelValue(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTeamType(this IGameEntity entity, IReactiveVariable<TeamType> value) => entity.SetValue(TeamType, value);

		#endregion

		#region TriggerEvents

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TriggerEvents GetTriggerEvents(this IGameEntity entity) => entity.GetValue<TriggerEvents>(TriggerEvents);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTriggerEvents(this IGameEntity entity, out TriggerEvents value) => entity.TryGetValue(TriggerEvents, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTriggerEvents(this IGameEntity entity, TriggerEvents value) => entity.AddValue(TriggerEvents, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTriggerEvents(this IGameEntity entity) => entity.HasValue(TriggerEvents);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTriggerEvents(this IGameEntity entity) => entity.DelValue(TriggerEvents);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTriggerEvents(this IGameEntity entity, TriggerEvents value) => entity.SetValue(TriggerEvents, value);

		#endregion

		#region Money

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<int> GetMoney(this IGameEntity entity) => entity.GetValue<IValue<int>>(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoney(this IGameEntity entity, out IValue<int> value) => entity.TryGetValue(Money, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoney(this IGameEntity entity, IValue<int> value) => entity.AddValue(Money, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoney(this IGameEntity entity) => entity.HasValue(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoney(this IGameEntity entity) => entity.DelValue(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoney(this IGameEntity entity, IValue<int> value) => entity.SetValue(Money, value);

		#endregion

		#region Renderer

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Renderer GetRenderer(this IGameEntity entity) => entity.GetValue<Renderer>(Renderer);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRenderer(this IGameEntity entity, out Renderer value) => entity.TryGetValue(Renderer, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRenderer(this IGameEntity entity, Renderer value) => entity.AddValue(Renderer, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRenderer(this IGameEntity entity) => entity.HasValue(Renderer);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRenderer(this IGameEntity entity) => entity.DelValue(Renderer);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRenderer(this IGameEntity entity, Renderer value) => entity.SetValue(Renderer, value);

		#endregion

		#region MoneyView

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static MoneyView GetMoneyView(this IGameEntity entity) => entity.GetValue<MoneyView>(MoneyView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoneyView(this IGameEntity entity, out MoneyView value) => entity.TryGetValue(MoneyView, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoneyView(this IGameEntity entity, MoneyView value) => entity.AddValue(MoneyView, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoneyView(this IGameEntity entity) => entity.HasValue(MoneyView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoneyView(this IGameEntity entity) => entity.DelValue(MoneyView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoneyView(this IGameEntity entity, MoneyView value) => entity.SetValue(MoneyView, value);

		#endregion
    }
}
```

Такая генерация позволяет использовать методы расширения для указанного типа сущности, при этом эти методы встраиваются
агрессивно компилятором, что не добавляет дополнительных вызовов методов в стеке вызова. Дополнительно разработчик
получает подсказки в IDE и гарантирует правильный тип и правильный ключ

И используется так для тэгов и значений:

```csharp
// Create a new instance of entity
Entity entity = new Entity();

// Add tags
entity.AddPlayerTag();
entity.AddNPCTag();

// Check tag
if (entity.HasPlayerTag())
    Console.WriteLine("Entity is a Player");

// Remove a tag
entity.DelNPCTag();
```

```csharp
// Create a new instance of entity
Entity entity = new Entity();

// Add values
entity.AddHealth(100);
entity.AddSpeed(12.5f);
entity.AddInventory(new GridInventory());

// Get a value
int health = entity.GetHealth();
Console.WriteLine($"Health: {health}");

// Update a value
entity.SetHealth(150);

// Remove a value
entity.DelInventory();
```

## Генерация API

Для того, чтобы сгенерировать Extension методы для сущности, существует специальный синтаксис, который храниться в
отдельном файле
который например выглядит вот так

```yaml
directory: Assets/Examples/Beginner/Scripts/GameEntity/
className: GameEntityAPI
namespace: BeginnerGame
entityType: IGameEntity
aggressiveInlining: true
unsafe: false


imports:
  - UnityEngine
  - Atomic.Elements
  - BeginnerGame

tags:
  - Character
  - Coin

values:

  #Transform
  - Position: IVariable<Vector3>
  - Rotation: IVariable<Quaternion>

  #Movement
  - MoveSpeed: IValue<float>
  - MoveDirection: IVariable<Vector3>

  #Rotation
  - RotationSpeed: IValue<float>
  - RotationDirection: IVariable<Vector3>

  #Other
  - TeamType: IReactiveVariable<TeamType>
  - TriggerEvents: TriggerEvents
  - Money: IValue<int>

  #View
  - Renderer: Renderer
  - MoneyView: MoneyView
```

Пройдемся по настройкам:

- directory — путь, где будет сгенерирован файл
- className — имя сгенерированного класса и файла
- namespace — домен класса
- entityType — тип сущности, может быть базовым IEntity по умолчанию, но также можно и указать свой тип, который будет
  наследоваться от IEntity
- aggressiveInlining — нужно ли ставить аттрибут `[MethodImpl(MethodImplOptions.AggressiveInlining)]` над extension
  методами. Значение true/false. По умолчанию false
- unsafe — оптимизационный флаг, который говорит о том, нужно использовать GetValue или GetValueUnsafe, второй вариант
  позволяет делать небезопасный каст, но это может привести к вылету приложения, так что лучше использовать с
  осторожностью. В качестве значения. по умолчанию false
- imports — все using неймспейсы и зависимости, которые нужны для кодогенерации
- tags — все тэги, которые нужно сгенерировать. Пишется просто название тэга
- values — все значения, которые нужно сгенерировать. Пишется через название переменной и второе ее тип

Существует два способа генерации api по такой конфигурации: через Unity Editor и через Rider Plugin

### Генерация через Unity Editor

Для генерации через Unity Editor 


Примечание, можно делать несколько таких .yaml файлов 


#### Пример использования


Sometimes managing tags by raw `int` keys or `string` names can get messy and error-prone, especially in big projects.
To
make this process easier and **type-safe**, the Atomic Framework supports **code generation**. This means you describe
all your tags (and values) once in a small config file, and the framework will automatically generate C# helpers. You
can learn more about this in the Manual under
the [Entity API Generation](../Manual.md/#-generate-entity-api) section.

**Step 1:** Create a `.yaml` file where you list all your tags and values:

```yaml
header: EntityAPI
entityType: IEntity
aggressiveInlining: true
namespace: PROJECT_NAMESPACE
className: EntityAPI
directory: CODE_GENERATION_PATH

imports:
  - UnityEngine
  - Atomic.Entities
  - Atomic.Elements

tags:
  - Player
  - NPC

values:
```

- `namespace` — the namespace of the generated code
- `tags` — list of tags that will be turned into constants
- `values` — same for values (empty in this example)

---

**Step 2:** Based on this config, the framework creates a **static API class**:

```csharp
/**
 * Code generation. Don't modify! 
 **/

public static class EntityAPI
{

    ///Tags
    public static readonly int Player;
    public static readonly int NPC;

    ///Values

    static GameEntityAPI()
    {
        //Tags
        Player = NameToId(nameof(Player));
        NPC = NameToId(nameof(NPC));

        //Values
    }


    ///Tag Extensions

    #region Player
    public static bool HasPlayerTag(this IGameEntity entity) => entity.HasTag(Player);
    public static bool AddPlayerTag(this IGameEntity entity) => entity.AddTag(Player);
    public static bool DelPlayerTag(this IGameEntity entity) => entity.DelTag(Player);
    #endregion
    
    #region NPC
    public static bool HasNPCTag(this IGameEntity entity) => entity.HasTag(NPC);
    public static bool AddNPCTag(this IGameEntity entity) => entity.AddTag(NPC);
    public static bool DelNPCTag(this IGameEntity entity) => entity.DelTag(NPC);
    #endregion
}
```

**Step 3:** Now you get ready-to-use methods for each tag: `AddPlayerTag()`, `HasPlayerTag()`, `DelPlayerTag()`, etc. No
more “magic
strings” or manual ID lookups.
```csharp
// Create a new entity
IEntity entity = new Entity();

// Add tags by string name
entity.AddPlayerTag();
entity.AddNPCTag(); // Get numeric ID

// Check tags
if (entity.HasPlayerTag())
    Console.WriteLine("Entity is a Player");

// Remove a tag
entity.DelNPCTag();
```

### Генерация через Rider Plugin

По умолчанию существует способ генерации через .yaml файл с помощью игрового движка Unity, а точнее Unity Editor,
который выглядит вот так.

Далее расписаны все настройки:

- header —

Для генерации

## API Reference

В качестве использования Entity API существует единственный класс, который хранит в себе для каждой строки свой ключ. Это класс EntityNames.

- [EntityNames](EntityNames.md)
