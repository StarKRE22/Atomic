![🚀 Official](https://img.shields.io/badge/official-871DAC)
![✅ Stable](https://img.shields.io/badge/stable-5FBA27)
[![📦 GitHub release (latest by date)](https://img.shields.io/github/v/release/starkre22/Atomic?color=red)](https://github.com/starkre22/Atomic/releases)
[![📄 GitHub license](https://img.shields.io/badge/license-MIT-blue.svg?style=flat)](https://github.com/StarKRE22/Atomic/blob/main/LICENSE.md)

<img width="4096" height="1024" alt="Banner" src="https://github.com/user-attachments/assets/bd596a97-4215-4fa6-8e5c-48da598b1e79" />

# ⚛️ What is Atomic?

Atomic is an architectural framework for game development in `Unity` and `C#`, built around the `Entity–State–Behaviour`
pattern and using `Atomic` elements for data organization.

# 📑 Table of Contents

- [Requirements](#-requirements)
- [Installation](#-installation)
- [Using Odin Inspector](#-using-odin-inspector)
- [Using Atomic Plugin for Rider](#-using-atomic-plugin-for-rider)
- [Key Concepts](#-key-concept)
- [Framework Structure](#-framework-structure)
- [Unity Quick Start](#unity-quick-start)
- [C Sharp Quick Start](#c-sharp-quick-start)
- [Tutorial](#tutorial)
- [Examples](#examples)
- [Best Practices](#best-practices)
- [License](#license)
- [Contacts](#contacts)

## 📝 Requirements

> [!IMPORTANT]  
> The Atomic Framework requires **Unity 6** or **.NET 7+**.  
> Make sure your development environment meets these requirements before using the framework.

## 📦 Installation

- _Option #1. Download source code with game examples_
- _Option #2.
  Download [Atomic.unitypackage](https://github.com/StarKRE22/Atomic/releases/download/v.2.0.0/Atomic.unitypackage)
  or [AtomicNonUnity.zip](https://github.com/StarKRE22/Atomic/releases/download/v.2.0.0/AtomicNonUnity.zip)
  from [release notes](https://github.com/StarKRE22/Atomic/releases)_
- _Option #3: Install via Unity Package Manager using the Git
  URL: `https://github.com/StarKRE22/Atomic.git?path=Assets/Plugins/Atomic`_

## 🧩 Using Odin Inspector

> [!TIP]  
> For better **debugging**, **configuration**, and **visualization** of game state, we **optionally recommend**
> using [Odin Inspector](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041).  
> The framework **works without Odin**, but Odin makes inspection and tweaking much easier.

## 🔌 Using Atomic Plugin for Rider

> [!TIP]  
> For better **code generation** and more convenient workflow in `Rider`, we **optionally recommend** installing
> the [Atomic Plugin](https://github.com/Prylor/atomic-rider-plugin).  
> By default the code generation works with Unity, but with the plugin, development experience in `Rider` become
> smoother and more powerful than in Unity.

## 💡 Key Concepts

**This section describes the key concepts promoted by the Atomic Framework**

### 1. Entity–State–Behaviour Pattern

- **Entity** — a **container** that contains set of **data** (`State`) and **logic** (`Behaviour`), strictly separated
  from each other.
- **State** — a set of `atomic` components that define the parameters of an entity.
- **Behaviour** — a set of controllers that operate on the entity’s `State` they are attached to.

> Thus, any game object, system, AI or UI can be described as a `composition` of data and logic.

### 2. Atomic Elements instead of Components

Complex systems should be built from `atomic elements`.  
Instead of creating large, monolithic objects and components, you can compose entity’s `State` from **simple, reusable
atomic elements**.

> Thus ensures that data remains modular, predictable, and reusable, while behaviours operate on these atomic building
> blocks.

### 3. Procedural Programming over OOP

Game development differs from traditional software development because of the **high number of interactions** between
systems. Object-Oriented Programming (OOP) often struggles to model these interactions effectively, leading to
unnecessary complexity. **Atomic Framework** encourages a **procedural approach**, promoting the use of `static methods`
and a `centralized data registry` instead of decentralized objects.

### 4. Reactive Programming
   The framework uses **reactive properties and collections** to observe entity state and respond to data changes in real time. Games naturally fit the **event-chain model**, where changes in one entity trigger reactions in others. Using reactive programming, these interactions can be expressed clearly and efficiently, reducing boilerplate and keeping the flow of game logic consistent.


## 🏗️ Framework Structure

Atomic Framework consists of two main modules, each serving a distinct role in how you structure and build your
game:

- **[Atomic.Elements](https://github.com/StarKRE22/Atomic/tree/main/Assets/Plugins/Atomic/Elements/Docs/Manual.md)**  
**A library of atomic elements for constructing complex game objects and systems in Unity and C#.**  
  The solution includes **constants, variables, reactive properties, collections, events, and actions**, enabling developers to quickly assemble any game entity **like a LEGO constructor**.


- **[Atomic.Entities](https://github.com/StarKRE22/Atomic/tree/main/Assets/Plugins/Atomic/Entities/Docs/Manual.md)**  
  **A framework implementing the `Entity–State–Behaviour` pattern in `Unity` and `C#`.**  
  In addition to basic entities and behaviours, the solution provides **factories, pools, worlds, filters**, and a separate **UI layer** if `Unity` is used as the presentation layer.

> To explore the documentation for each module in more detail, click the links above.


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

## C Sharp Quick Start

1. **Create a new entity**

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

2. **Write `MoveBehaviour` for the entity**

```csharp
//Controller that moves entity by its direction
public sealed class MoveBehaviour : IEntityInit, IEntityUpdate
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

    //Calls when Entity.OnUpdate()
    public void Update(IEntity entity, float deltaTime)
    {
        Vector3 direction = _moveDirection.Value;
        if (direction != Vector3.zero) 
            _position.Value += _moveSpeed.Value * deltaTime * direction;
    }
}
```

3. **Add `MoveBehaviour` to the entity**

```csharp
character.AddBehaviour<MoveBehaviour>();
```

4. **Initiaize the character when game is loading**

```csharp
//Initialize entity, that will call IEntityInit
character.Init();
```

5. **Enable the character when game started**

```csharp
//Enable entity for updates, that will call IEntityEnable
character.Enable(); 
```

6. **Update the character while a game is running**

```csharp
const float deltaTime = 0.02f;

while(_isGameRunning)
{
   character.Update(deltaTime); //Calls IEntityUpdate
}
```

7. **When game is finished dispose entity**

```csharp
//Disables and disposes entity state
character.Dispose();
```

## Tutorial

- **What is Entity**
- **Create an Entity**
    - **CSharp Guide**
    - **Unity Guide**
- **Entity Behaviour Lifecycle** + Gizmos и аттрибут в Edit Mode
- **Using Entity Pools**
- **Using Entity Worlds**
- **Using Entity Filters**
- **Separating Game from Unity** //Factories, Baking, Views
- **Designing Architecture with Entities** //Contexts

- **Using Code generation**
    - **Unity Guide**
    - **Rider Plugin Guide**

[//]: # ()

[//]: # (Guides)

[//]: # (├── Introduction)

[//]: # (│   ├── What is Atomic?)

[//]: # (│   ├── Requirements & Installation)

[//]: # (│   └── Using Odin Inspector &#40;optional&#41;)

[//]: # (│)

[//]: # (├── Core Concepts)

[//]: # (│   ├── Entities & EntityStateBehaviour Pattern)

[//]: # (│   ├── Reactive Values & Variables)

[//]: # (│   ├── Events & Signals)

[//]: # (│   └── Requests & Actions)

[//]: # (│)

[//]: # (├── Tutorials)

[//]: # (│   ├── Getting Started with Entities &#40;C# example&#41;)

[//]: # (│   ├── Unity Quick Start &#40;SceneEntity, Installers&#41;)

[//]: # (│   ├── Character Example &#40;MoveBehaviour&#41;)

[//]: # (│   └── Building UI Contexts)

[//]: # (│)

[//]: # (├── Best Practices)

[//]: # (│   ├── Prefer Abstract Interfaces)

[//]: # (│   ├── Shared Constants)

[//]: # (│   ├── Iterating Reactive Collections)

[//]: # (│   ├── Request-Condition-Action-Event Pattern)

[//]: # (│   ├── Requests vs Actions)

[//]: # (│   └── Performance Tips)

## Theory

This section explains the **core concepts** behind the Atomic Framework and how they work together.  
Understanding these principles will help you design flexible and reusable game mechanics.

### 🛑 Problem

Game development differs from typical software development because **games involve a large number of interactions**
between objects and systems.

The key problem is that **interactions are difficult to model using traditional object-oriented approaches**. For
example:

- Imagine a character and a ladder in a game.

<img width="225" height="225" alt="изображение" src="https://github.com/user-attachments/assets/d6d36566-2c8f-4cc8-b208-3e586d45be98" />

- Where should the interaction logic live?
    - In the character?
    - In the ladder?
    - In a separate controller object?

Each choice creates new challenges:

- Naming becomes confusing.
- The code structure becomes more complex.
- Developers spend a lot of time just organizing interactions instead of implementing gameplay mechanics.

As a result, game projects can quickly become hard to maintain, extend, and debug, especially as the number of
interactions grows.

### 💡 Solution

To address the problem of complex interactions in game development, the Atomic Framework uses a **procedural programming
approach**, which enforces a strict separation between **data** and **logic**.

```csharp
public static void InteractWithLadder(IEntity character, IEntity ladder)
{
   Vector3 characterPosition = character.GetValue<Vector3>("Position");
   Vector3 ladderPosition = ladder.GetValue<Vector3>("Position");
   //Some code...
}
```

- **Data** is represented by **structures** or **objects** that only store state.
- **Logic** is implemented as **pure static methods**, responsible for handling interactions between objects.

This approach provides several advantages:

- Simplifies code structure and naming conventions.
- Reduces time spent deciding where interaction code should live.
- Improves **performance**, **reliability**, and **speed of development**, especially for small and medium projects.
- Ideal for **prototyping**, where fast iteration and clear data flow are critical.

By clearly separating state and behaviour, developers can focus on gameplay mechanics without getting bogged down in
organizational overhead.

### ⚡ Key Concepts

- **Everything is Entities**  
  Everything in Atomic is represented as **entities** that hold values (state) and behaviours (logic).  
  Entities can represent anything: characters, UI, contexts, or systems.

- **Entity-State-Behaviour Pattern**  
  Each entity acts as a **container** that holds **data** and **behaviours**, keeping them strictly separated.
    - **Data** consists of structures or objects that represent the state of the entity.
    - **Behaviour** consists of pure controllers or logic methods that operate on the data.

  Entities can have multiple behaviours bound to them, which can be **activated or deactivated dynamically** depending
  on the state.  
  This strict separation of state and logic allows for **clearer architecture**, reducing complexity, improving
  testability, easier debugging, and more flexible interactions between game objects.

- **Static and Procedural Approach**  
  Atomic promotes the use of static methods and reactive values for entity interactions.

- **Reactive Programming**  
  The framework uses **reactive properties** to observe entity state and respond to data changes in real time.

- **Atomic Data Composition**  
  Each entity's **data** is composed of **primitive, atomic elements** that can be combined like building blocks.  
  This allows developers to **construct complex entities** by assembling simple, reusable pieces, similar to using a
  constructor pattern.  
  The approach ensures that data remains **modular, predictable, and easy to manage**, while behaviours operate on these
  atomic elements.

## Examples

The repository includes **three sample projects** demonstrating different use cases of the Atomic Framework:

### 1️⃣ Beginner Sample

<img width="347" height="267" alt="изображение" src="https://github.com/user-attachments/assets/99a64dce-557c-4008-bcc8-f7ce9aba9893" />

The Beginner Sample is a **simple 2-player mini-game** designed to introduce the core concepts of the Atomic Framework.

**Gameplay:**

- **Players:** Two players share the same scene.
- **Controls:**
    - Player 1: `W`, `A`, `S`, `D`
    - Player 2: Arrow keys
- **Objective:** Collect more coins than the opponent within a **limited time**.
- **Win Condition:** When time runs out, the player with the most coins wins.
- **UI Feedback:** Victory screen appears showing the winning player.
- **Restart:** Players can restart the game to try again.

This sample demonstrates:

- Basic **entity creation** and behaviour binding
- **Reactive properties** for tracking player scores
- Simple **UI integration**
- Handling of **game state transitions** (playing → victory → restart)
- **Testing Support** — includes tests proving that this architecture is **fully testable**, even with thousands of
  entities.

### 2️⃣ Top-Down Shooter Sample

<img width="357" height="188" alt="изображение" src="https://github.com/user-attachments/assets/30ce41ab-2958-4979-b7cb-7d124cb1b791" />

The Top-Down Shooter Sample demonstrates a more **complex game architecture**, suitable for mid-sized games.

#### Project Structure

- **Scenes:**
    - `Bootstrap` — starting scene that initializes the game.
    - `Menu` — separate scene for main menu and navigation.
- **Contexts:**
    - **Application Context** — handles global systems and persistent data.
    - **Game Context** — manages gameplay-specific entities and logic.

#### Gameplay Mechanics

- **Levels:** Three separate levels where players and enemies spawn.
- **Combat:** Core mechanic is **shooting bullets**.
- **Physics:** Both bullets and characters use **Unity physics and colliders**.
- **Animations & VFX:** Characters and bullets have animations, visual effects, and sound effects for feedback.
- **Respawning:** Units respawn dynamically after being defeated.
- **Win Condition:** Player or team with the **most kills** at the end of the match wins.
- **Controls:**
    - Player 1 (Blue): `W`, `A`, `S`, `D` to move, `Space` to shoot
    - Player 2 (Red): Arrow keys to move, `Q` to shoot
- **Configuration:** All controls and settings are defined in the **game configuration** and can be inspected there.
- **Start:** Game is launched through the `Bootstrap` scene.

#### Purpose

This sample serves as a **mini-prototype for a top-down shooter**, demonstrating:

- How **Atomic Framework** can manage entities, behaviours, and reactive properties
- Separation of **Application** and **Game** contexts for clean architecture
- How to **scale and extend** a project for more complex gameplay scenarios

### 3️⃣ RTS Sample

<img width="416" height="192" alt="изображение" src="https://github.com/user-attachments/assets/92d471ac-374a-4fc2-9bb6-86603107f16e" />

The RTS Sample is designed to demonstrate **high-performance entity management** with a large number of units.

#### Core Idea

- Unity serves purely as a **visualization layer**.
- The actual game logic runs entirely in **C#**, decoupled from Unity's MonoBehaviour system.
- This architecture allows simulation of **thousands of entities** efficiently.

#### Project Structure

- **Scenes:**
    1. **Entity Baking Scene** — converts Unity objects into pure C# entities for the game simulation.
    2. **5000 Units Scene** — 5,000 game objects with full Unity visualization.
    3. **10000 Units Scene** — 10,000 game objects running without visualization for maximum performance testing.

#### Features Demonstrated

- **Model-View Separation** — demonstrates how Atomic separates the **data/model layer** from the **visual layer**.
- **EntityWorld, Filters, and Triggers** — advanced tools for managing entities efficiently and reacting to changes.
- **EntityCollectionView** — shows how to synchronize entity data with views.

#### Purpose

This sample illustrates:

- **Scalability** of the Atomic Framework for large-scale RTS projects
- Efficient **C#-based entity simulation** independent of Unity
- Advanced **entity management patterns** using Atomic concepts
- Techniques for **high-speed updates, filtering, and reactive triggers**

## Best Practices

TODO:

## License

This project is licensed under the [MIT License](./LICENSE.md).

```
MIT License

Copyright (c) 2025 Igor Gulkin

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

## Contacts

**Author:** Igor Gulkin  
**Telegram:** [@starkre22](https://t.me/starkre22)  
**Email:** [gulkin.igor.developer@gmail.com](mailto:gulkin.igor.developer@gmail.com)



<!-- 

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
