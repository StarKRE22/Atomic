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

## Requirements
> [!IMPORTANT]
> The Atomic Framework requires **Unity 6+** or **.NET 7+**.  
> Make sure your development environment meets these requirements before using the framework.

## Installation

- _Option #1. Download source code with game examples_
- _Option #2. Download last [Atomic.unitypackage](https://github.com/StarKRE22/Atomic/releases/download/v.2.0.0/Atomic.unitypackage) from [release notes](https://github.com/StarKRE22/Atomic/releases)_ 

## Add Odin Inspector for Unity 6

> [!TIP]
> For better debugging, configuration, and visualization of the game state, we **optionally recommend** adding the [Odin Inspector](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041) plugin to your project. 
> While the Atomic Framework **works without Odin Inspector**, using it will enhance your workflow and make it easier to inspect and tweak data in Unity. 
> In the future, we plan to reduce dependency on Odin Inspector and implement our own tools for Unity integration.


## Framework modules
- **[Atomic.Elements](https://github.com/StarKRE22/Atomic/tree/main/Assets/Plugins/Atomic/Elements)** — solution with modular and reusable components for Unity development
- **[Atomic.Entities](https://github.com/StarKRE22/Atomic/tree/main/Assets/Plugins/Atomic/Entities)** — solution to manage and deploy entities across the architecture of a project
- **[Atomic.EventBus (Experimental)](https://github.com/StarKRE22/Atomic/tree/main/Assets/Plugins/Atomic/EventBus (Experimental))** — high-performance event handling system, flexible and efficient.

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
