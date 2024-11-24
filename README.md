![Official](https://img.shields.io/badge/official-871DAC)
![Stable](https://img.shields.io/badge/stable-5FBA27)
[![GitHub release (latest by date)](https://img.shields.io/github/v/release/starkre22/Atomic?color=red)](https://github.com/starkre22/Atomic/releases)
[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg?style=flat)](https://github.com/StarKRE22/Atomic/blob/main/LICENSE.md)

![изображение](https://github.com/user-attachments/assets/bd9b13da-fed3-41dc-b84e-ef87b3301dfa)
# Atomic Framework
Architectural framework for developing games in C# and Unity

What is Atomic?
---
Atomic Framework is a solution designed for developing games in C# and Unity. The main idea of framework is reduce code complexity by separating state and behaviour of game code. To achieve the flexibility and reusability of game mechanics, it is necessary to look towards procedural and reactive programming instead of OOP.

## Installation

_Option #1. Download source code with game example and tutorial_

_Option #2. Download last [Atomic.unitypackage](https://github.com/StarKRE22/Atomic/releases/download/v.1.0/Atomic.unitypackage) from [release notes](https://github.com/StarKRE22/Atomic/releases)_ 

## Add Odin Inspector
> [!IMPORTANT]
> There are many features that don't work by default. To make the most of the Atomic Framework, we recommend adding the [Odin Inspector](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041) plugin to your project. In the future, we plan to move away from dependence on Odin Inspector and write our own tools for integration with Unity

## Framework modules
- **[Atomic.Elements](https://github.com/StarKRE22/Atomic/tree/main/Assets/Plugins/Atomic/Elements)** — library with reactive data structures
- **[Atomic.Entities](https://github.com/StarKRE22/Atomic/tree/main/Assets/Plugins/Atomic/Entities)** — solution for developing game objects
- **[Atomic.Context](https://github.com/StarKRE22/Atomic/tree/main/Assets/Plugins/Atomic/Context)** — solution for developing game systems
- **[Atomic.UI](https://github.com/StarKRE22/Atomic/tree/main/Assets/Plugins/Atomic/UI)** — solution for developing UI controllers
- **[Atomic.Extensions](https://github.com/StarKRE22/Atomic/tree/main/Assets/Plugins/Atomic/Extensions)** — extra features

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




