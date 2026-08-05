![🚀 Official](https://img.shields.io/badge/official-871DAC)
![✅ Stable](https://img.shields.io/badge/stable-5FBA27)
[![📦 GitHub release (latest by date)](https://img.shields.io/github/v/release/starkre22/Atomic?color=red)](https://github.com/starkre22/Atomic/releases)
[![📄 GitHub license](https://img.shields.io/badge/license-MIT-blue.svg?style=flat)](https://github.com/StarKRE22/Atomic/blob/main/LICENSE.md)

<img width="475" height="250" alt="изображение" src="https://github.com/user-attachments/assets/0f6d1c99-f3ed-479c-8139-cd5676e5bf9c" />

<!-- <img width="4096" height="" alt="Banner" src="https://github.com/user-attachments/assets/bd596a97-4215-4fa6-8e5c-48da598b1e79" /> -->
<!-- <img width="500" height="" alt="Banner" src="https://github.com/user-attachments/assets/74ec9316-bccb-4458-8b82-73d3d57c8ac8" />  -->

# ⚛️ What is Atomic?

**Atomic** is an object-oriented framework for building core gameplay systems in **Unity** and **C#**. 

The core idea is to simplify core gameplay development by making game logic **modular**, **explicit**, and **easy to maintain**. 

Instead of organizing gameplay around components, Atomic separates **state** and **behavior** into independent, reusable building blocks. To achieve this, it provides a set of composable primitives including **constants**, **variables**, **events**, **actions**, and **functions** that can be combined to build flexible gameplay systems with significantly less boilerplate code.

---

# 📑 Table of Contents

- [Requirements](#-requirements)
- [Installation](#-installation)
- [Unity Quick Start](#-unity-quick-start)
  - [Code Generation Setup](#i-code-generation-setup)
  - [Creating a Character](#ii-creating-a-character)
  - [Adding Keyboard Input](#iii-adding-keyboard-input)
- [Tutorials](#-tutorials)
- [API Reference](#-api-reference)
- [Sample Projects](#-sample-projects)
- [Best Practices](#-best-practices)
- [Performance](#-performance)
- [Useful Links](#-useful-links)
- [License](#-license)
- [Contact](#-contact)

---

## 📝 Requirements

The Atomic Framework requires **Unity 6+**.  
Make sure your development environment meets these requirements before using the framework.

### Recommended Tools

Although not required, the following tool significantly improves the development experience with Atomic:

#### • [Odin Inspector](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041)

For better **debugging**, **configuration**, and **visualization** of your game state in the Unity Editor.  
Atomic works perfectly **without Odin**, but using it makes data inspection and live tweaking much easier.

> [!TIP]
> The source generators (`EntityAPIGenerator`, `EventAPIGenerator`, and the corresponding analyzers) are included as
> precompiled DLLs in `Assets/Plugins/Atomic/SourceGenerators/`. They are ready to use after importing the package.
> For advanced scenarios or to build them yourself, see the source repository at
> https://github.com/dre0dru/Atomic.SourceGenerators.


---

## 📦 Installation

- _Option #1. Download source code with game examples_
- _Option #2.
  Download [Atomic.unitypackage](https://github.com/StarKRE22/Atomic/releases/download/v.2.0.1/Atomic.v.2.0.unitypackage)
  from [release notes](https://github.com/StarKRE22/Atomic/releases)_
- _Option #3: Install via Unity Package Manager using the Git
  URL: `https://github.com/StarKRE22/Atomic.git?path=Assets/Plugins/Atomic`_

---

## 🚀 Unity Quick Start

This section provides a hands-on introduction to using the Atomic Framework inside Unity.
You’ll learn how to set up the source generators, create your first entity, and implement a simple
movement mechanic — no IDE plugin required.

### I. Code Generation Setup

Before you start creating gameplay mechanics, configure the source generators. They turn declarative API classes into
strongly-typed extension methods and catch missing key initializers at compile time.

#### Step 1. Verify the Generator DLLs

Make sure the following assemblies are present:

```
Assets/Plugins/Atomic/SourceGenerators/
├── EntityAPIGenerator.dll
├── EntityAPIAnalyzer.dll
├── EventAPIGenerator.dll
└── EventAPIAnalyzer.dll
```

#### Step 2. Configure Import Settings

Select each DLL in the Unity Project window and:

1. Add the asset label `RoslynAnalyzer`.
2. Under **Select platforms for plugin**, uncheck **Any Platform** and every individual platform.
3. Click **Apply**.

> [!IMPORTANT]
> Leaving all platforms unchecked is correct — generators and analyzers run only at compile time and must not ship in
> player builds.

After applying the settings, restart Unity or choose `Assets → Reimport All`.

### II. Creating a Character

In this section, we’ll create a character entity in Unity, declare its data with the source generator, and implement a
simple movement mechanic. By the end of this section, you’ll have a working character that moves in the specified direction.

#### Step 1. Creating a Game Object

In the Scene Hierarchy, right-click and choose `3D Object → Capsule` to create a new game object.

<img width="400" height="" alt="GameObject creation" src="https://github.com/user-attachments/assets/463a721f-e50d-4cb7-86be-a5d50a6bfa17" />

#### Step 2. Adding the Entity Component

In the Inspector window of the created object, go to `Atomic → Entities → Entity` to add the Entity component.

<img width="400" height="" alt="Entity component" src="Docs/Images/EntityComponent.png" />

Make sure the following checkboxes are enabled:

- `useUnityLifecycle` — the entity updates along with the **MonoBehaviour** lifecycle.
- `installOnAwake` — the entity is constructed during the **Awake** phase.

#### Step 3. Declare an Entity API

Create a `public static partial` class and decorate it with [`[GenerateEntityExtensionsAPI]`](https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/EntityAPI/Manual.md). Declare the character data as [`ValueKey<E, T>`](https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/KeyStore/ValueKey.md)
fields:

```csharp
using Atomic.Entities;
using UnityEngine;

namespace SampleGame
{
    [GenerateEntityExtensionsAPI]
    public static partial class CharacterAPI
    {
        public static readonly ValueKey<Transform> Transform = new(nameof(Transform));
        public static readonly ValueKey<IValue<float>> MoveSpeed = new(nameof(MoveSpeed));
        public static readonly ValueKey<IVariable<Vector3>> MoveDirection = new(nameof(MoveDirection));
    }
}
```

After the first compilation, the generator creates extension methods such as:

```csharp
// Register propeties
entity.AddTransform(transform);
entity.AddMoveSpeed(moveSpeed);
entity.AddMoveDirection(moveDirection);

// Usage
Transform t = entity.GetTransform();
IValue<float> speed = entity.GetMoveSpeed();
IVariable<Vector3> direction = entity.GetMoveDirection();
```

For more details, see the [Entity API Generator](Docs/CodeGeneration/EntityAPI/EntityAPIGenerator.md) documentation.

#### Step 4. Creating the Movement Mechanic

Let’s write a behaviour that moves the entity in the direction of its movement:

```csharp
// Controller that moves entity by its direction
public sealed class MoveBehaviour : IEntityInit, IEntityFixedTick
{
    private Transform _transform;
    private IValue<float> _moveSpeed;
    private IVariable<Vector3> _moveDirection;

    public void Init(IEntity entity)
    {
        _transform = entity.GetTransform();
        _moveSpeed = entity.GetMoveSpeed();
        _moveDirection = entity.GetMoveDirection();
    }

    public void FixedTick(IEntity entity, float deltaTime)
    {
        Vector3 direction = _moveDirection.Value;
        if (direction != Vector3.zero)
            _transform.position += _moveSpeed.Value * deltaTime * direction;
    }
}
```

> [!IMPORTANT]
> In the Atomic approach, the developer **always works with data abstractions represented as reference-type wrappers.**
>
> This design greatly simplifies project maintenance, testing, and multiplayer development, as it removes the tight
> coupling to data storage methods that is typical for component-based ECS architectures.
>
> A simple example:
> Under the [`IValue<T>`](https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Values/Manual.md) interface, you can substitute either a [`Variable<T>`](https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Variables/Manual.md) or a [`Const<T>`](https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Values/Const.md) implementation.

#### Step 5. Creating the Installer

Create an installer that injects the data and the movement logic into the entity:

```csharp
// Populates entity with tags, values and behaviours
public sealed class CharacterInstaller : MonoEntityInstaller
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Const<float> _moveSpeed = 5.0f;
    [SerializeField] private Variable<Vector3> _moveDirection = Vector3.forward;

    public override void Install(IEntity entity)
    {
        entity.AddTransform(_transform);
        entity.AddMoveSpeed(_moveSpeed);
        entity.AddMoveDirection(_moveDirection);

        entity.AddBehaviour<MoveBehaviour>();
    }
}
```

#### Step 6. Configuring the Game Object

Add the `CharacterInstaller` component to your entity through the Inspector and configure its settings.

<img width="400" height="" alt="изображение" src="https://github.com/user-attachments/assets/1967b1d8-b6b7-41c7-85db-5d6935f6443e" />

#### Step 7. Connecting the Installer to the Entity

To link the `CharacterInstaller` to the `Entity` component, drag and drop it into the **Scene Installers** field.

<img width="400" height="" alt="изображение" src="Docs/Images/EntityInstalling.png" />

#### Step 8. Running the Character

In the Unity Editor, press Play to verify that the character starts moving forward.

### III. Adding Keyboard Input

Next, we’ll add movement control with the WASD or arrow keys.

#### Step 1. Create an Input Controller

```csharp
public class InputBehaviour : IEntityInit, IEntityTick
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private ISetter<Vector3> _moveDirection;

    public void Init(IEntity entity)
    {
        _moveDirection = entity.GetMoveDirection();
    }

    public void Tick(IEntity entity, float deltaTime)
    {
        float dx = Input.GetAxis(HORIZONTAL);
        float dz = Input.GetAxis(VERTICAL);
        _moveDirection.Value = new Vector3(dx, 0, dz);
    }
}
```

> [!IMPORTANT]
> To change the entity’s data, modify the value through its reference. No `SetMoveDirection` on the `IEntity` is required.

#### Step 2. Add the InputBehaviour

Register the `InputBehaviour` inside the `CharacterInstaller`:

```csharp
public sealed class CharacterInstaller : MonoEntityInstaller
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Const<float> _moveSpeed = 5.0f;
    [SerializeField] private Variable<Vector3> _moveDirection = Vector3.zero;

    public override void Install(IEntity entity)
    {
        entity.AddTransform(_transform);
        entity.AddMoveSpeed(_moveSpeed);
        entity.AddMoveDirection(_moveDirection);

        entity.AddBehaviour<MoveBehaviour>();
        entity.AddBehaviour<InputBehaviour>();
    }
}
```

#### Step 3. Running the Character

In the Unity Editor, press Play to verify that the character moves when pressing the WASD or arrow keys.

---

## 📖 Tutorials

Get hands-on with the Atomic Framework through practical guides and examples. Each tutorial introduces a specific
concept — from the fundamental Atomic approach to advanced topics such as entity
interaction and simulation without Unity.

1. [What is Atomic Approach](Docs/Tutorials/1.%20What%20is%20Atomic%20Approach.md)
2. [Entity API Generation](Docs/Tutorials/2.%20Entity%20API%20Generation.md)
3. [Creating the Entity in Unity](Docs/Tutorials/3.%20Creating%20the%20Entity%20in%20Unity.md)
4. [Architectural Consistency](Docs/Tutorials/4.%20Architectural%20Consistency.md)
5. [Interaction Between Entities](Docs/Tutorials/5.%20Interaction%20Between%20Entities.md)
6. [Minimizing Unity](Docs/Tutorials/6.%20Minimizing%20Unity.md)
7. [Summary](Docs/Tutorials/7.%20Summary.md)

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

---

## 🔍 API Reference

Explore the full Atomic API documentation. The framework is divided into four main modules — Elements, Entities, Events
and Code Generation, each responsible for a different level of abstraction.

### [⚛️ Atomic.Elements](Docs/Elements/Manual.md)

Low-level, composable building blocks — the atomic “elements” that power everything in the framework.
Use them to define data, state, logic, and interaction between systems.

- [Values](Docs/Elements/Values/Manual.md)  — immutable constants and parameters
- [Variables](Docs/Elements/Variables/Manual.md) — reactive mutable data containers
- [Actions](Docs/Elements/Actions/Manual.md) — callable commands and procedures
- [Commands](Docs/Elements/Commands/Manual.md) — request-like objects that encapsulate an action with optional undo
- [Functions](Docs/Elements/Functions/Manual.md) — encapsulated logic returning results
- [Setters](Docs/Elements/Setters/Manual.md) — controlled state mutation interfaces
- [Requests](Docs/Elements/Requests/Manual.md) — deferred actions that can be executed at a later time
- [Events](Docs/Elements/Events/Manual.md) — reactive broadcast-based communication
- [Time](Docs/Elements/Time/Manual.md) — frame-based and delta-time utilities
- [Collections](Docs/Elements/Collections/Manual.md) — reactive collections such as arrays, lists, dictionaries, and
  sets.
- [Utilities](Docs/Elements/Utils/Manual.md) —utility classes and components

### [🧬 Atomic.Entities](Docs/Entities/Manual.md)

The high-level architecture layer that combines atomic elements into functional entities. All game objects,
systems, UI elements, and application contexts can be represented as entities, each containing state and behaviour.

- [Entities](Docs/Entities/Entities/Manual.md) — base unit of game logic
- [Behaviours](Docs/Entities/Behaviours/Manual.md) — reusable logic modules
- [Installers](Docs/Entities/Installers/Manual.md) — dependency injection and setup scripts
- [Aspects](Docs/Entities/Aspects/Manual.md) — applies or discards tags, values, and behaviours on an entity
- [Factories](Docs/Entities/Factories/Manual.md) — entity creation and configuration
- [Baking](Docs/Entities/Baking/Manual.md) — converting Unity GameObjects into entity
- [Pooling](Docs/Entities/Pooling/Manual.md) — entity reuse and performance optimization
- [Collections](Docs/Entities/Collections/Manual.md) — high performance set for entities
- [Worlds](Docs/Entities/Worlds/Manual.md) — high performance entity collection with automatic lifecycle
- [Registry](Docs/Entities/Registry/EntityRegistry.md) — central access and lookup
- [Filters](Docs/Entities/Filters/Manual.md) — runtime querying and filtering
- [Triggers](Docs/Entities/Filters/EntityTriggers.md) — reactive filtering events
- [Lifecycle](Docs/Entities/Lifecycle/Manual.md) — initialization and update stages
- [Views](Docs/Entities/UI/Manual.md) — UI integration and visualization
- [KeyStore](Docs/Entities/KeyStore/Manual.md) — converting string-based names into unique integer identifiers
- [Systems](Docs/Entities/Systems/Manual.md) — entity update and processing systems
- [Inspector](Docs/Entities/Inspector/Manual.md) — editor-only attributes for visualizing entity data
- [Bootstrap](Docs/Entities/Bootstrap/Manual.md) — scene and scriptable bootstrap setup
- [API Generation](Docs/Entities/EntityAPI/Manual.md) — type-safe extension methods via source generators

### [📣 Atomic.Events](Docs/Events/Manual.md)

Lightweight, strongly-typed event bus system for decoupled communication between systems.

- [Bus](Docs/Events/Bus/Manual.md) — event bus implementations
- [Keys](Docs/Events/Keys/Manual.md) — strongly-typed event identifiers
- [Subscriptions](Docs/Events/Subscriptions/Manual.md) — disposable event subscriptions
- [Extensions](Docs/Events/Extensions.md) — bus extension methods

### [🧬 Code Generation](Docs/CodeGeneration/Manual.md)

Roslyn source generators and analyzers that turn declarative API classes into strongly-typed extension methods.

- [Setup](Docs/CodeGeneration/Setup.md) — adding generators/analyzers to a Unity project
- [Entity API Generator](Docs/CodeGeneration/EntityAPI/EntityAPIGenerator.md) — `[GenerateEntityExtensionsAPI]` usage
- [Entity API Analyzer](Docs/CodeGeneration/EntityAPI/EntityAPIAnalyzer.md) — key initializer validation
- [Event API Generator](Docs/CodeGeneration/EventAPI/EventAPIGenerator.md) — `[GenerateEventExtensionsAPI]` usage
- [Event API Analyzer](Docs/CodeGeneration/EventAPI/EventAPIAnalyzer.md) — event key initializer validation

---

## 🗂 Sample Projects

The framework includes **four sample projects** in a [separate repository](https://github.com/StarKRE22/Atomic.Samples),
each demonstrating a different level of complexity:

- **[Beginner Sample](https://github.com/StarKRE22/Atomic.Samples)** — a simple 2-player mini-game showcasing the core principles.
- **[Top-Down Shooter Sample](https://github.com/StarKRE22/Atomic.Samples)** — a modular game architecture for mid-sized projects.
- **[RTS Sample](https://github.com/StarKRE22/Atomic.Samples)** — a large-scale simulation with thousands of entities.
- **[Turn-Based Sample](https://github.com/StarKRE22/Atomic.Samples)** — an event-driven turn-based tactics game.

[→ Go to Atomic.Samples](https://github.com/StarKRE22/Atomic.Samples)

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

- Atomic.Entities
    - [Entity](Docs/Entities/Entities/Manual.md#-performance)
    - [EntityCollection](Docs/Entities/Collections/Manual.md#-performance)
- Atomic.Elements
    - [ReactiveArray](Docs/Elements/Performance/ReactiveArrayPerformance.md)
    - [ReactiveList](Docs/Elements/Performance/ReactiveListPerformance.md)
    - [ReactiveLinkedList](Docs/Elements/Performance/ReactiveLinkedListPerformance.md)
    - [ReactiveDictionary](Docs/Elements/Performance/ReactiveDictionaryPerformance.md)
    - [ReactiveHashSet](Docs/Elements/Performance/ReactiveHashSetPerformance.md)

---

## 🔗 Useful Links

- [Стрим: Введение в фреймворк Atomic](https://www.youtube.com/live/AWNOzbGKg3Y?si=yF4Cipyrmx8L7bcm) (прошлая версия)
- [Хабр: Atomic — свежий взгляд на разработку игр Unity и C#](https://habr.com/ru/articles/959834/) (прошлая версия)
- [Medium: Atomic — a fresh architecture on game development with Unity and C#](https://medium.com/@gulkin.igor.developer/atomic-a-fresh-architecture-on-game-development-with-unity-and-c-c587fcf9e266) (прошлая версия)

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

## 📧 Contact

**Author:** Igor Gulkin  
**Telegram:** [t.me/starkre22](https://t.me/starkre22)  
**Email:** [gulkin.igor.developer@gmail.com](mailto:gulkin.igor.developer@gmail.com)

---

<p align="center">
<a href="#-table-of-contents">Back to top</a> •
<a href="https://github.com/StarKRE22/Atomic/issues">Report Issue</a> •
<a href="https://github.com/StarKRE22/Atomic/discussions">Join Discussion</a>
</p>
