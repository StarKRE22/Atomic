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
- [Key Concepts](#-key-concepts)
- [Documentation](#-documentation)
- [Unity Quick Start](#-unity-quick-start)
- [CSharp Quick Start](#-csharp-quick-start)
- [Tutorials](#-tutorials)
- [Game Examples](#-game-examples)
- [Best Practices](#-best-practices)
- [License](#-license)
- [Contacts](#-contacts)

## 📝 Requirements

> [!IMPORTANT]  
> The Atomic Framework requires **Unity 6** or **.NET 7+**.  
> Make sure your development environment meets these requirements before using the framework.
---

## 📦 Installation

- _Option #1. Download source code with game examples_
- _Option #2.
  Download [Atomic.unitypackage](https://github.com/StarKRE22/Atomic/releases/download/v.2.0.0/Atomic.unitypackage)
  or [AtomicNonUnity.zip](https://github.com/StarKRE22/Atomic/releases/download/v.2.0.0/AtomicNonUnity.zip)
  from [release notes](https://github.com/StarKRE22/Atomic/releases)_
- _Option #3: Install via Unity Package Manager using the Git
  URL: `https://github.com/StarKRE22/Atomic.git?path=Assets/Plugins/Atomic`_
---

## 🧩 Using Odin Inspector

> [!TIP]  
> For better **debugging**, **configuration**, and **visualization** of game state, we **optionally recommend**
> using [Odin Inspector](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041).  
> The framework **works without Odin**, but Odin makes inspection and tweaking much easier.
---

## 🔌 Using Atomic Plugin for Rider

> [!TIP]  
> For better **code generation** and more convenient workflow in `Rider`, we **optionally recommend** installing
> the [Atomic Plugin](https://github.com/Prylor/atomic-rider-plugin).  
> By default the code generation works with Unity, but with the plugin, development experience in `Rider` become
> smoother and more powerful than in Unity.
---

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

---

## 📚 Documentation

Atomic Framework consists of two main modules, each serving a distinct role in how you structure and build your
game:

### `Atomic.Elements` [(Read More)](https://github.com/StarKRE22/Atomic/tree/main/Assets/Plugins/Atomic/Elements/Docs/Manual.md)  
**A library of atomic elements for constructing complex game objects and systems in Unity and C#.**  
  The solution includes **constants, variables, reactive properties, collections, events, and actions**, enabling developers to quickly assemble any game entity **like a LEGO constructor**.


### `Atomic.Entities` [(Read More)](https://github.com/StarKRE22/Atomic/tree/main/Assets/Plugins/Atomic/Entities/Docs/Manual.md)  
  **A framework implementing the `Entity–State–Behaviour` pattern in `Unity` and `C#`.** In addition to basic entities and behaviours, the solution provides **factories, pools, worlds, filters**, and a separate **UI layer** if `Unity` is used as the presentation layer.

---

## 🚀 Unity Quick Start
**Below is the process for quickly creating a character entity in Unity**

### 1. Create a new `GameObject`
<img width="360" height="255" alt="GameObject creation" src="https://github.com/user-attachments/assets/463a721f-e50d-4cb7-86be-a5d50a6bfa17" />

### 2. Add `Entity` Component to the GameObject
<img width="464" height="346" alt="Entity component" src="https://github.com/user-attachments/assets/f74644ba-5858-4857-816e-ea47eed0e913" />

### 3. Create `CharacterInstaller` script
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
    }
}
```

### 4. Attach `CharacterInstaller` script to the GameObject
<img width="464" height="153" alt="изображение" src="https://github.com/user-attachments/assets/1967b1d8-b6b7-41c7-85db-5d6935f6443e" />

### 5. Create `MoveBehaviour` class
```csharp
// Controller that moves entity by its direction
public sealed class MoveBehaviour : IEntityInit, IEntityFixedUpdate
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
    public void FixedUpdate(IEntity entity, float deltaTime)
    {
        Vector3 direction = _moveDirection.Value;
        if (direction != Vector3.zero) 
            _transform.position += _moveSpeed.Value * deltaTime * direction;
    }
}
```
### 6. Add `MoveBehaviour` to `CharacterInstaller`

 ```csharp
public sealed class CharacterInstaller : SceneEntityInstaller
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Const<float> _moveSpeed = 5.0f; //Immutable variable
    [SerializeField] private ReactiveVariable<Vector3> _moveDirection; //Mutable variable with subscription

    public override void Install(IEntity entity)
    {
        //Add tags to a character 
        {...}
        
        //Add properties to a character
        {...}
        
        //Add behaviours to a character
        entity.AddBehaviour<MoveBehaviour>();
    }
}
```
### 7. Enter `PlayMode` and check your character movement!

---

## ⚡ CSharp Quick Start
**Below is the process for quickly creating an entity in plain C#**

### 1. Create a new entity

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

### 2. Create `MoveBehaviour` class

```csharp
//Controller that moves entity by its direction
public sealed class MoveBehaviour : IEntityInit, IEntityUpdate
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
    public void Update(IEntity entity, float deltaTime)
    {
        Vector3 direction = _moveDirection.Value;
        if (direction != Vector3.zero) 
            _position.Value += _moveSpeed.Value * deltaTime * direction;
    }
}
```

### 3. Add `MoveBehaviour` to the entity

```csharp
entity.AddBehaviour<MoveBehaviour>();
```

### 4. Initialize the entity when game is loading
```csharp
//Calls IEntityInit
entity.Init();
```

### 5. Enable the entity when game is started

```csharp
//Enable entity for updates
//Calls IEntityEnable
entity.Enable(); 
```

### 6. Update the entity while a game is running

```csharp
const float deltaTime = 0.02f;

while(_isGameRunning)
{ 
   //Calls IEntityUpdate
   entity.Update(deltaTime); 
}
```

### 7. When game is finished disable the entity

```csharp
//Disable entity for updates
//Calls IEntityDisable
character.Disable();
```

### 8. Dispose the entity when unloading game resources 
```csharp
//Dispose entity resources
//Calls IEntityDispose
entity.Dispose();
```

## 📖 Tutorials
Coming Soon

<!-- 
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
-->

## ✅ Game Examples
This section includes **three sample projects** demonstrating different use cases of the `Atomic Framework`

### 1️⃣ Beginner Sample
<img width="347" height="267" alt="изображение" src="https://github.com/user-attachments/assets/99a64dce-557c-4008-bcc8-f7ce9aba9893" />

The `Beginner Sample` is a **simple 2-player mini-game** designed to introduce the core concepts of the framework

> **Gameplay:**
> - **Players:** Two players share the same scene.
> - **Objective:** Collect more coins than the opponent within a **limited time**.
> - **Controls:**
>    - Player (Blue): Arrow keys
>    - Player (Red): `W`, `A`, `S`, `D`
> - **UI Feedback:** Victory screen appears showing the winning player.
> - **Restart:** Players can restart the game to try again.

#### This Sample Demonstrates
1. How to create and configure **SceneEntity** in Unity.
2. Demonstrates project architecture based on the **Entity–State–Behaviour** pattern.
3. Usage of **atomic properties** and **events**.
4. **Entity pooling** of coins.
5. Applying **procedural programming** and **static methods** for game logic.
6. Developing **Atomic UI** using the **MVP-Passive View** pattern.
7. Writing **Unit tests** to validate entity logic and behaviors.


<!-- 
Тут!
-->
### 2️⃣ Top-Down Shooter Sample
<img width="357" height="188" alt="изображение" src="https://github.com/user-attachments/assets/30ce41ab-2958-4979-b7cb-7d124cb1b791" />

The Top-Down Shooter Sample demonstrates a more **complex game architecture**, suitable for mid-sized games.

> **Gameplay**
> - **Players:** Two players share the same scene.
> - **Objective:** Kill your opponent more times than he does within a **limited time**.
> - **Controls:**
>   - Player (Blue): Arrow keys to move, `Space` to shoot 
>   - Player (Red): `W`, `A`, `S`, `D` to move, `Q` to shoot
> - **Mechanics**
>   - **Character Movement:** Kinematic movement using `Transform`, with collision handling using `Rigidbody.SweepTest`.
>   - **Combat:** Characters use weapon, which are separate entities, and these weapons fire physical projectiles.
>   - **Projectile:** A kinematic object that interacts via trigger collisions and has a limited lifetime.
>   - **Respawning:** Units respawn at random point dynamically after being defeated.
>   - **Limited Time:**  The game ends when the time limit is reached.
> - **Visualization**
>   - **Character:** Equipped with canvas, animations, VFX, and sound effects. 
>   - **UI:** Displays the kill count for each player and a session timer.

> **Application**
> - **Scenes:**
>  - `Bootstrap` — starting scene that initializes the game.
>  - `Menu` — separate scene for main menu and navigation.
> - **Start:** Game is launched through the `Bootstrap` scene.
> - **Levels:** Three separate levels where players and enemies spawn.
> - **Save System:** Stores the last completed level.
> - **Game Load Flow:** Hierarchical structure of game loading sequence.

#### This Sample Demonstrates
1. How to structure a full-fledged **game architecture** for seamless scalability
2. How to build an **application context** leveraging the `Entity-State-Behaviour` pattern
3. How to design a **menu interface** in a procedural style
4. How to orchestrate scene loading using a `Loading Tree`
5. How to persist and manage **game data**
6. How to transform an entity into a fully-featured **game object with animations, VFX, and audio**
7. How to create and manage a **projectile pool** efficiently

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

---

## 📌 Best Practices

TODO:

## ⚖️ License

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

## 📧 Contacts

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
