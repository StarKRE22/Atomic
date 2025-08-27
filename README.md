![Official](https://img.shields.io/badge/official-871DAC)
![Stable](https://img.shields.io/badge/stable-5FBA27)
[![GitHub release (latest by date)](https://img.shields.io/github/v/release/starkre22/Atomic?color=red)](https://github.com/starkre22/Atomic/releases)
[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg?style=flat)](https://github.com/StarKRE22/Atomic/blob/main/LICENSE.md)

<img width="4096" height="1024" alt="изображение" src="https://github.com/user-attachments/assets/bd596a97-4215-4fa6-8e5c-48da598b1e79" />

<!-- ![изображение](https://github.com/user-attachments/assets/bd9b13da-fed3-41dc-b84e-ef87b3301dfa)  -->

<!-- # Atomic Framework -->


What is Atomic?
---
Atomic is a reactive procedural framework for developing games in C# and Unity. The key idea is reducing code complexity by separating state from behaviour. To achieve the flexibility and reusability of game mechanics, it is necessary to look towards static methods and reactive properties instead of OOP.


# Table of Contents

- [Requirements](#Requirements)
- [Installation](#installation)
- [Using Odin Inspector](#using-odin-inspector)
- [Core Modules](#core-modules)
- [Quick Start](#quick-start)    
- [Theory](#theory)
- [Examples](#examples)
- [License](#license)
- [Contacts](#contacts)

## Requirements
> [!IMPORTANT]
> The Atomic Framework requires **Unity 6** or **.NET 7+**.  
> Make sure your development environment meets these requirements before using the framework.

## Installation
- _Option #1. Download source code with game examples_
- _Option #2. Download [Atomic.unitypackage](https://github.com/StarKRE22/Atomic/releases/download/v.2.0.0/Atomic.unitypackage) or [AtomicNonUnity.zip](https://github.com/StarKRE22/Atomic/releases/download/v.2.0.0/AtomicNonUnity.zip) from [release notes](https://github.com/StarKRE22/Atomic/releases)_ 
- _Option #3: Install via Unity Package Manager using the Git URL: https://github.com/StarKRE22/Atomic.git?path=Assets/Plugins/Atomic_

## Using Odin Inspector
> [!TIP]
> For better debugging, configuration, and visualization of the game state, we **optionally recommend** adding the [Odin Inspector](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041) plugin to your project. 
> While the Atomic Framework **works without Odin Inspector**, using it will enhance your workflow and make it easier to inspect and tweak data in Unity. 
> In the future, we plan to reduce dependency on Odin Inspector and implement our own tools for Unity integration.

## Core Modules
- **[Atomic.Elements](https://github.com/StarKRE22/Atomic/tree/main/Assets/Plugins/Atomic/Elements)** — solution with modular and reusable components for Unity development
- **[Atomic.Entities](https://github.com/StarKRE22/Atomic/tree/main/Assets/Plugins/Atomic/Entities)** — solution to manage and deploy entities across the architecture of a project
- **[Atomic.EventBus](https://github.com/StarKRE22/Atomic/tree/main/Assets/Plugins/Atomic/EventBus)** — high-performance event handling system, flexible and efficient.

## Quick Start

### With Unity

1. **Create a GameObject in a scene**
   
   <img width="360" height="255" alt="GameObject creation" src="https://github.com/user-attachments/assets/463a721f-e50d-4cb7-86be-a5d50a6bfa17" />

3. **Add the `Entity` component to the GameObject**
   
   <img width="464" height="346" alt="Entity component" src="https://github.com/user-attachments/assets/f74644ba-5858-4857-816e-ea47eed0e913" />

4. **Create a `CharacterInstaller` script and attach it to the GameObject**

 ```csharp
public sealed class CharacterInstaller : SceneEntityInstaller
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Const<float> _moveSpeed = 5.0f;
    [SerializeField] private ReactiveVariable<Vector3> _moveDirection;

    public override void Install(IEntity entity)
    {
        entity.AddTag("Character");
        entity.AddTag("Moveable");

        entity.AddValue("Transform", _transform);
        entity.AddValue("MoveSpeed", _moveSpeed);
        entity.AddValue("MoveDirection", _moveDirection);

        entity.AddBehaviour<MoveBehaviour>();
    }
}
```

5. **Write the `MoveBehaviour` class**

```csharp
public sealed class MoveBehaviour : IEntitySpawn, IEntityFixedUpdate
{
    private Transform _transform;
    private IValue<float> _moveSpeed;
    private IValue<Vector3> _moveDirection;

    public void OnSpawn(IEntity entity)
    {
        _transform = entity.GetValue<Transform>("Transform");
        _moveSpeed = entity.GetValue<IValue<float>>("MoveSpeed");
        _moveDirection = entity.GetValue<IValue<Vector3>>("MoveDirection");
    }

    public void OnFixedUpdate(IEntity entity, float deltaTime)
    {
        Vector3 direction = _moveDirection.Value;
        if (direction != Vector3.zero) 
            _transform.position += _moveSpeed.Value * deltaTime * direction;
    }
}
```
6. **Configure `CharacterInstaller`, Enter `PlayMode` and test your character movement**

<img width="464" height="153" alt="изображение" src="https://github.com/user-attachments/assets/1967b1d8-b6b7-41c7-85db-5d6935f6443e" />

### With C#

1. **Create a character**
```csharp
var character = new Entity("Character");

character.AddTag("Moveable");

character.AddValue("Position", new ReactiveVariable<Vector3>());
character.AddValue("MoveSpeed", new Const<float>(3.5f));
character.AddValue("MoveDirection", new ReactiveVariable<Vector3>());

character.AddBehaviour<MoveBehaviour>();
```

2. **Write `MoveBehaviour` for the character**
```csharp
public sealed class MoveBehaviour : IEntitySpawn, IEntityFixedUpdate
{
    private IVariable<Vector3> _position;
    private IValue<float> _moveSpeed;
    private IValue<Vector3> _moveDirection;

    public void OnSpawn(IEntity entity)
    {
        _position = entity.GetValue<IVariable<Vector3>>("Position");
        _moveSpeed = entity.GetValue<IValue<float>>("MoveSpeed");
        _moveDirection = entity.GetValue<IValue<Vector3>>("MoveDirection");
    }

    public void OnFixedUpdate(IEntity entity, float deltaTime)
    {
        Vector3 direction = _moveDirection.Value;
        if (direction != Vector3.zero) 
            _position.Value += _moveSpeed.Value * deltaTime * direction;
    }
}
```
3. **Activate the character one time**
```csharp
//Make entity spawned and calls IEntitySpawn
character.Spawn();

//Make entity active and calls IEntityActivate
character.Activate(); 
```

4. **Update the character multiple times**
```csharp
const float deltaTime = 0.02f;

//Calls IEntityFixedUpdate if entity is active
character.OnFixedUpdate(deltaTime); 
```

<!--
## Game Example
There is a game example in project. A mini game for two players in which you need to collect coins for a while. Which of the players collected the most, he won. Controls: WASD and keyboard arrows.

> Note: Made without Odin Inspector!

<img width="546" alt="изображение" src="https://github.com/user-attachments/assets/c7d114c8-f9bd-4c59-be36-2dc7ee3cdac9">

## Tutorial Walkthrough
There is a step-by-step tutorial in the project. It shows the key features of Atomic Framework and how mini-game was made step by step.

<img width="651" alt="изображение" src="https://github.com/user-attachments/assets/4a1655c4-475d-41e7-a699-21b917448b36">


## Table of Contents
Here you can see all the features that are in the framework. We will add documentation on github from time to time to make it easier for developers to learn the framework.
- [Atomic.Elements]
  - [Reactive Properties]
    - [ReactiveVariable]
    - [ReactiveInt]
    - [ReactiveFloat]
    - [ReactiveBool]
    - [ReactiveVector2]
    - [ReactiveVector3]
    - [ReactiveQuaternion]
    - [float3Reactive]
    - [quaternionReactive]    
  - [Reactive Collections]
    - [ReactiveArray]
    - [ReactiveDictionary]
    - [ReactiveHashSet]
    - [ReactiveList]
    - [ReactiveSortedDictionary]
    - [ReactiveSortedList]
    - [ReactiveSortedSet]   
  - [Event]
  - [Expressions]
    - [AndExpression]
    - [OrExpression]
    - [IntMulExpression]
    - [IntSumExpression]
    - [FloatMulExpression]
    - [FloatSumExpression]
  - [Time]
    - [Timer]
    - [Countdown]
    - [Stopwatch]
    - [Cycle]
- [Atomic.Entities]
  - [C#]
    - [Entity]
    - [Entity Behaviours]
    - [Entity World]
    - [Entity Filter]
  - [Unity]
    - [Configure Value Console]
    - [Configure Tag Console]
    - [SceneEntity]
    - [SceneEntityInstaller]
    - [SceneEntityController]
    - [SceneEntityProxy]
    - [SceneEntityGizmos]
    - [SceneEntityWorld]
    - [SceneEntityWorldController]
    - [Attributes]
  - [Performance]    
- [Atomic.Contexts]
  - [C#]
    - [Context]
    - [Context Systems]
    - [Dependency Injection]
  - [Unity]
    - [Configure Value Console]  
    - [SceneContext]
    - [SceneContextInstaller]
    - [SceneContextController]
    - [SceneContextGizmos]
    - [Attributes]
  - [Performance]     
- [Atomic.UI]
  - [SceneViewController]
  - [SceneViewGizmos]
  - [Behaviours]
  - [Performance]
- [Atomic.Extensions]
  - [Entity Aspects]
  - [Condition and Action Assets] 
-->
