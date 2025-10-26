![🚀 Official](https://img.shields.io/badge/official-871DAC)
![✅ Stable](https://img.shields.io/badge/stable-5FBA27)
[![📦 GitHub release (latest by date)](https://img.shields.io/github/v/release/starkre22/Atomic?color=red)](https://github.com/starkre22/Atomic/releases)
[![📄 GitHub license](https://img.shields.io/badge/license-MIT-blue.svg?style=flat)](https://github.com/StarKRE22/Atomic/blob/main/LICENSE.md)

<img width="4096" height="" alt="Banner" src="https://github.com/user-attachments/assets/bd596a97-4215-4fa6-8e5c-48da598b1e79" />

# ⚛️ What is Atomic?

Atomic is an architectural framework for game development in `Unity` and `C#`, built around the `Entity–State–Behaviour`
pattern and using `Atomic` elements for data organization.

---

# 📑 Table of Contents

- [Requirements](#-requirements)
- [Installation](#-installation)
- [Using Odin Inspector](#-using-odin-inspector)
- [Using Atomic Plugin for Rider](#-using-atomic-plugin-for-rider)
- [Key Concepts](#-key-concepts)
- [Documentation](#-documentation)
    - [Atomic.Elements](#atomic-elements)
    - [Atomic.Entities](#atomic-entities)
- [Unity Quick Start](#-unity-quick-start)
- [CSharp Quick Start](#-csharp-quick-start)
- [Game Examples](#-game-examples)
    - [Beginner Sample](#ex1)
    - [Top-Down Shooter Sample](#ex2)
    - [RTS Sample](#ex3)
- [Best Practices](#-best-practices)
- [Performance](#-performance)
- [License](#-license)
- [Contacts](#-contacts)

<!-- - [Tutorials](#-tutorials) -->

---

## 📝 Requirements

The Atomic Framework requires **Unity 6** or **.NET 7+**. Make sure your development environment meets these
requirements before using the framework.

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

## 🎛 Using Odin Inspector

For better **debugging**, **configuration**, and **visualization** of game state, we **optionally recommend**
using [Odin Inspector](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041). The framework **works without Odin**, but Odin makes inspection and tweaking much easier.

---

## 🔌 Using Plugin for Rider [(Read More)](Docs/Entities/EntityAPI/Manual.md#generating-api-via-rider-plugin)

For better **code generation** and more convenient workflow in `Rider IDE`, we **highly recommend** installing
the Atomic Rider Plugin from [Jetbrains Marketplace](https://plugins.jetbrains.com/plugin/28321-atomic)
or [GitHub Repository](https://github.com/Prylor/atomic-rider-plugin) . By default, the code generation works with
Unity, but with the plugin, development experience in `Rider IDE` become
smoother and more powerful than in Unity.

---

## 💡 Key Concepts

### 1️⃣ Entity–State–Behaviour Pattern

- **Entity** — a **container** holding a set of **data** (`State`) and **logic** (`Behaviour`), kept strictly separate.
- **State** — a collection of `atomic` components defining the entity's parameters.
- **Behaviour** — controllers that operate on the entity’s `State` they are attached to.

> Any game object, system, AI, or UI can be represented as a **composition of data and logic**, making systems modular
> and predictable.

### 2️⃣ Atomic Elements instead of Components

Complex systems should be built from **atomic elements**.
Instead of creating large, monolithic objects, entities’ `State` can be composed of **small, reusable atomic elements**.

> This ensures data remains modular, predictable, and reusable, while behaviours act on these well-defined building
> blocks.

### 3️⃣ Procedural Programming over OOP

Game development often involves **highly interactive systems**. Traditional Object-Oriented Programming (OOP) can
struggle to model these interactions cleanly, creating unnecessary complexity.

**Atomic Framework** encourages a **procedural approach**, leveraging `static methods` and a `centralized data registry`
instead of tightly coupled objects.

> This approach simplifies interactions, improves maintainability, and scales well for large entity-driven projects.

---

## 📚 Documentation

Atomic Framework consists of two main modules, each serving a distinct role in how you structure and build your
game:

<div id="atomic-elements"></div>

### ⚛️ Atomic.Elements [(Read More)](Docs/Elements/Manual.md)

**A library of atomic elements for constructing complex game objects and systems in Unity and C#.**
The solution includes **constants, variables, reactive properties, collections, events, and actions**, enabling
developers to quickly assemble any game entity **like a LEGO constructor**.

- [Values](Docs/Elements/Values/Manual.md)  <!-- + -->
- [Variables](Docs/Elements/Variables/Manual.md) <!-- + -->
- [Actions](Docs/Elements/Actions/Manual.md) <!-- + -->
- [Functions](Docs/Elements/Functions/Manual.md) <!-- + -->
- [Setters](Docs/Elements/Setters/Manual.md) <!-- + -->
- [Requests](Docs/Elements/Requests/Manual.md) <!-- + -->
- [Events](Docs/Elements/Events/Manual.md) <!-- + -->
- [Time](Docs/Elements/Time/Manual.md) <!-- + -->
- [Collections](Docs/Elements/Collections/Manual.md) <!-- + -->
- [Expressions](Docs/Elements/Expressions/Manual.md) <!-- + -->
- [Utilities](Docs/Elements/Utils/Manual.md) <!-- + -->

<div id="atomic-entities"></div>

---

### 🧩 Atomic.Entities [(Read More)](Docs/Entities/Manual.md)

**A framework implementing the `Entity–State–Behaviour` pattern in `Unity` and `C#`.** In addition to basic entities and
behaviours, the solution provides **factories, pools, worlds, filters**, and a separate **UI layer** if `Unity` is used
as the presentation layer.

- [Entities](Docs/Entities/Entities/Manual.md) <!-- + -->
- [Behaviours](Docs/Entities/Behaviours/Manual.md) <!-- + -->
- [Installers](Docs/Entities/Installers/Manual.md) <!-- + -->
- [Aspects](Docs/Entities/Aspects/Manual.md) <!-- + -->
- [Factories](Docs/Entities/Factories/Manual.md) <!-- + -->
- [Baking](Docs/Entities/Baking/Manual.md) <!-- + -->
- [Pooling](Docs/Entities/Pooling/Manual.md) <!-- + -->
- [Collections](Docs/Entities/Collections/Manual.md) <!-- + -->
- [Worlds](Docs/Entities/Worlds/Manual.md) <!-- + -->
- [Registry](Docs/Entities/Registry/EntityRegistry.md) <!-- + -->
- [Filters](Docs/Entities/Filters/Manual.md) <!-- + -->
- [Triggers](Docs/Entities/Filters/EntityTriggers.md) <!-- + -->
- [Lifecycle](Docs/Entities/Lifecycle/Manual.md) <!-- + -->
- [Views](Docs/Entities/UI/Manual.md) <!-- + -->
- [Names](Docs/Entities/Names/Manual.md) <!-- + -->
- [API Generation](Docs/Entities/EntityAPI/Manual.md) <!-- + -->

---

## 🚀 Unity Quick Start

**Below is the process for quickly creating a character entity in Unity**

#### 1. Create a new `GameObject` on a scene

<img width="400" height="" alt="GameObject creation" src="https://github.com/user-attachments/assets/463a721f-e50d-4cb7-86be-a5d50a6bfa17" />

#### 2. Add `Entity` Component to the GameObject

<img width="400" height="" alt="Entity component" src="Docs/Images/EntityComponent.png" />

#### 3. Create a movement mechanics for the entity

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

#### 4. Create a script that populates the entity with tags, values and behaviours

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

<img width="400" height="" alt="изображение" src="https://github.com/user-attachments/assets/1967b1d8-b6b7-41c7-85db-5d6935f6443e" />

#### 6. Drag & drop `CharacterInstaller` into `installers` field of the entity

<img width="400" height="" alt="изображение" src="Docs/Images/EntityInstalling.png" />

#### 7. Enter `PlayMode` and check your character movement!

---

## ⚡ CSharp Quick Start

**Below is the process for quickly creating an entity in plain C#**

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

#### 2. Create a movement mechanics for the entity

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

#### 3. Attach `MoveBehaviour` to the entity

```csharp
entity.AddBehaviour<MoveBehaviour>();
```

#### 4. Control lifecycle of your entity

```csharp
// Initialize the entity -> Calls IEntityInit
entity.Init();

// Enable the entity for updates -> Calls IEntityEnable 
entity.Enable(); 

// Update your entity while game is running
const float deltaTime = 0.016f; // 60 FPS
while(isGameRunning)
{
   entity.Tick(deltaTime); // Calls IEntityTick
   System.Threading.Thread.Sleep(16); // deltaTime * 1000 
}

// Disable entity for updates -> Calls IEntityDisable
entity.Disable();

// Dispose entity resources -> Calls IEntityDispose
entity.Dispose();
```

---

<!-- 

## 📖 Tutorials

To be added...

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

---

-->

## 🗂 Game Examples

This section presents **three sample projects**, each demonstrating a different level of complexity and use case of the
framework.  
All examples are available inside **[Assets/Examples](Assets/Examples)**.

- **[Beginner Sample](Assets/Examples/Beginner)** — a simple 2-player mini-game showcasing the core principles of the
  framework.
- **[Top-Down Shooter Sample](Assets/Examples/Shooter)** — a more advanced, modular game architecture suitable for
  mid-sized projects.
- **[RTS Sample](Assets/Examples/RTS)** — a large-scale simulation demonstrating high-performance entity management with
  thousands of units.

---

<div id="ex1"></div>

### 1️⃣ Beginner Sample

A **simple 2-player mini-game** designed to introduce the fundamental ideas behind the Atomic framework. [Link to the sample](Assets/Examples/Beginner).

<img width="400" height="" alt="Beginner sample preview" src="https://github.com/user-attachments/assets/99a64dce-557c-4008-bcc8-f7ce9aba9893" />

#### 🕹 Gameplay Overview

- **Players:** Two players share a single arena.
- **Goal:** Collect more coins than your opponent within a **limited time**.
- **Controls:**
    - Player (Blue): Arrow keys
    - Player (Red): `W`, `A`, `S`, `D`
- **UI Feedback:** A victory screen displays the winning player.
- **Restart:** Players can restart and compete again instantly.

#### 💡 This Sample Demonstrates

1. Creating and configuring **SceneEntity** objects in Unity.
2. Structuring a project using the **Entity–State–Behaviour** pattern.
3. Using **atomic values** and **reactive events**.
4. Implementing **entity pooling** for coins.
5. Writing **procedural gameplay logic** via static use-case methods.
6. Building an **Atomic UI** using the **MVP (Passive View)** pattern.
7. Creating **unit tests** to verify entity logic and behavior consistency.

---

<div id="ex2"></div>

### 2️⃣ Top-Down Shooter Sample

The **Top-Down Shooter** demonstrates a more sophisticated and scalable game architecture, suitable for **mid-size
projects**. [Link to the sample](Assets/Examples/Shooter).

<img width="400" height="" alt="Shooter sample preview" src="https://github.com/user-attachments/assets/30ce41ab-2958-4979-b7cb-7d124cb1b791" />

#### 🕹 Gameplay Overview

- **Players:** Two players in a shared arena.
- **Objective:** Eliminate your opponent more times than they eliminate you, within a **time limit**.
- **Controls:**
    - Player (Blue): Arrow keys to move, `Space` to shoot
    - Player (Red): `W`, `A`, `S`, `D` to move, `Q` to shoot
- **Mechanics:**
    - **Movement:** Kinematic character movement with `Rigidbody.SweepTest` collision handling.
    - **Combat:** Independent weapon entities firing physical projectiles.
    - **Projectile:** Kinematic object with trigger collisions and limited lifetime.
    - **Respawn:** Units reappear at random points after death.
    - **Time Limit:** The match ends when the timer expires.
- **Visualization:**
    - Animated characters with sound and VFX.
    - UI displays kills and time remaining.

#### 🧩 Application Structure

- **Scenes:**
    - `Bootstrap` — initializes and loads the game.
    - `Menu` — the main navigation scene.
    - **Levels:** three stages featuring player and enemy spawning.
- **Save System:** Remembers the last completed level.
- **Loading Tree:** Hierarchical scene-loading sequence for structured bootstrapping.

#### 💡 This Sample Demonstrates

1. Designing a complete, **scalable game architecture**.
2. Implementing an **application context** using the Entity–State–Behaviour pattern.
3. Building procedural **menu systems**.
4. Managing complex loading flows with a **Loading Tree**.
5. Saving and restoring **persistent game data**.
6. Turning entities into fully featured **game objects** with animation, VFX, and audio.
7. Managing **projectile pools** efficiently.
8. Structuring a **modular project file system**.

---

<div id="ex3"></div>

### 3️⃣ RTS Sample

<img width="400" height="" alt="RTS sample preview" src="https://github.com/user-attachments/assets/92d471ac-374a-4fc2-9bb6-86603107f16e" />

The **RTS Sample** showcases **high-performance entity management** — running thousands of active units in real time
with minimal overhead. [Link to the sample](Assets/Examples/RTS).

#### 🕹 Gameplay Overview

- **Armies:** Two large armies automatically engage in battle — each consisting of infantry, tanks, and buildings.
- **Buildings:** Have health points and serve as static defense or production units.
- **Infantry:** Possesses health, performs melee attacks, and seeks the nearest enemy.
- **Tanks:** Fire projectiles and detect enemies within range.
- **Projectiles:** Travel toward targets with limited lifetime and cause impact damage.
- **CameraControls:** 
  - Movement: WASD
  - Zoom: Mouse Scroll

#### 🧩 Scenes

- **5000 Units Scene** — 5,000 visualized GameObjects for real-time simulation.
- **10000 Units Scene** — 10,000 entities simulated **without visualization** for performance benchmarking.
- **Entity Baking Scene** — demonstrates converting Unity scene objects into pure C# entities for simulation.

#### 💡 This Sample Demonstrates

1. Running complete **game logic in pure C#**, using Unity solely for visualization.
2. Employing `EntityWorld`, `EntityFactory`, `EntityPool`, `EntityFilter`, and `EntityTriggers`.
3. Using `EntityView`, `EntityViewPool`, and `EntityCollectionView` for rendering and synchronization.
4. Managing **5,000–10,000 active objects** efficiently on a single thread.
5. Baking Unity objects into a **pure data-driven simulation** architecture.

---

> 💡 These samples progressively guide you from basic gameplay architecture to advanced large-scale simulations — 
> helping you master **Atomic.Entities** from small prototypes to high-performance real-world projects.


---

## 📌 Best Practices

This section outlines **recommended approaches and patterns** when working with the library. Following these
practices will help you write **modular, testable, and high-performance code**, whether you’re developing single-player
or multiplayer games.

- **Architecture**
    - [File System Organization](Docs/BestPractices/ProjectFolderOrganization.md) <!-- + -->
    - [Prefer Atomic Interfaces to Concrete Classes](Docs/BestPractices/PreferAbstractInterfaces.md) <!-- + -->
    - [Flyweight Pattern for Constants](Docs/BestPractices/SharedConstants.md) <!-- + -->
    - [Request-Condition-Action-Event (RCAE) Flow](Docs/BestPractices/RequestConditionActionEvent.md) <!-- + -->
    - [Modular EntityInstallers](Docs/BestPractices/ModularEntityInstallers.md) <!-- + -->
    - [Upgrading EntityFactory to the Builder](Docs/BestPractices/UpgradingEntityFactoryToBuilder.md) <!-- + -->
    - [Combine EntityPool with EntityFactory](Docs/BestPractices/UsingEntityPoolWithFactories.md) <!-- + -->
    - [Building Entity System with Model & View Separation](Docs/BestPractices/EntitySystem.md) <!-- + -->
    - [Overriding EntityFactories with EntityBakers](Docs/BestPractices/OverrideEntityFactoriesWithBakers.md) <!-- + -->
- **Optimization**
    - [Iterating over Reactive Collections](Docs/BestPractices/IteratingReactiveCollections.md) <!-- + -->
    - [Iterating over Entity Tags, Values and Behaviours](Docs/BestPractices/IteratingOverEntity.md) <!-- + -->
    - [Iterating over EntityCollections, Worlds and Filters.](Docs/BestPractices/IteratingOverEntityCollections.md) <!-- + -->
- **Features**
    - [InlineActions with Entities](Docs/BestPractices/UsingInlineActions.md) <!-- + -->
    - [InlineFunctions with Entities](Docs/BestPractices/UsingInlineFunctions.md) <!-- + -->
    - [Events with Entities](Docs/BestPractices/UsingEvents.md) <!-- + -->
    - [Requests with Entities](Docs/BestPractices/UsingRequests.md) <!-- + -->
    - [Requests vs Actions](Docs/BestPractices/RequestsVsActions.md) <!-- + -->
    - [Cooldown with Entities](Docs/BestPractices/UsingCooldownInGameMechanics.md) <!-- + -->
    - [Timer vs Cooldown](Docs/BestPractices/ChosingBetweenTimerAndCooldown.md) <!-- + -->
    - [Expressions with Entities](Docs/BestPractices/UsingExpressions.md) <!-- + -->
    - [Setters with Entities](Docs/BestPractices/UsingSetters.md) <!-- + -->
    - [Uninstall Method for EntityInstallers](Docs/BestPractices/UninstallEntityInstaller.md) <!-- + -->
    - [DisposeComposite in EntityInstallers](Docs/BestPractices/UsingSubscriptionsWithDisposeComposite.md) <!-- + -->
    - [PlayMode & EditMode for EntityInstallers](Docs/BestPractices/UsingUtilsForEntityInstallers.md) <!-- + -->
    - [Optional with EntityInstallers](Docs/BestPractices/UsingOptionalWithInstallers.md) <!-- + -->
- **Extensions**
    - [Observe Extension Method](Docs/BestPractices/UsingObserveWithReactiveValues.md) <!-- + -->
    - [Constants with AndExpressions](Docs/BestPractices/UsingConstantsWithAndExpressions.md) <!-- + -->
    - [[SerializeReference] for CompositeActions](Docs/BestPractices/UsingSerializeReferenceForCompositeActions.md) <!-- + -->
    - [[SerializeReference] for LogAction](Docs/BestPractices/UsingSerializeReferenceForPrintActions.md) <!-- + -->

---

## 🔥 Performance

This section focuses on **runtime efficiency** within the framework. It provides detailed benchmarks, comparisons, and
implementation notes that highlight how different systems and data structures perform under real-world conditions.

- **Atomic.Entities**
    - [Entity](Docs/Entities/Entities/Manual.md#-performance)
    - [EntityCollection](Docs/Entities/Collections/Manual.md#-performance)
- **Atomic.Elements**
    - [ReactiveArray](Docs/Elements/Performance/ReactiveArrayPerformance.md)
    - [ReactiveList](Docs/Elements/Performance/ReactiveListPerformance.md)
    - [ReactiveLinkedList](Docs/Elements/Performance/ReactiveLinkedListPerformance.md)
    - [ReactiveDictionary](Docs/Elements/Performance/ReactiveDictionaryPerformance.md)
    - [ReactiveHashSet](Docs/Elements/Performance/ReactiveHashSetPerformance.md)

---

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

---

## 📧 Contacts

**Author:** Igor Gulkin  
**Telegram:** [t.me/starkre22](https://t.me/starkre22)  
**Email:** [gulkin.igor.developer@gmail.com](mailto:gulkin.igor.developer@gmail.com)
