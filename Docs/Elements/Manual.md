# ⚛️ Atomic.Elements

A library of atomic elements for constructing complex game objects and systems in `Unity` and `C#`.
The solution includes **constants, variables, reactive properties, collections, events, and actions**, enabling developers to quickly assemble any game entity like a `LEGO Constructor`.

## 📑 Table of Contents
- [Requirements](#-requirements)
- [Using Odin Inspector](#-using-odin-inspector)
- [API Reference](#-api-reference)
- [Performance](#-performance)

## 📝 Requirements
> [!IMPORTANT]  
> The Atomic.Elements requires **Unity 6** or **.NET 7+**.  
> Make sure your development environment meets these requirements before using the framework.

## 🧩 Using Odin Inspector
> [!TIP]  
> For better **debugging**, **configuration**, and **visualization** of game state, we **optionally recommend** using [Odin Inspector](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041).  
> The framework works **without Odin**, but Odin makes inspection and tweaking much easier.

## 🔍 API Reference

This section provides a complete reference to the core interfaces, classes, and utilities designed for reactive programming, event handling, Unity integration, and general-purpose development.

Here you will find detailed explanations, usage examples, and extension methods for:

- `Values` – interfaces and implementations for reactive and constant values.
- `Variables` – reactive variables, proxies, and Unity-specific variable types.
- `Actions` & `Functions` – reusable actions, predicates, and function abstractions.
- `Setters` – inline and interface-based property setters.
- `Events` & `Signals` – event abstractions, subscriptions, and reactive signals.
- `Requests` – encapsulated request objects for decoupled communication.
- `Expressions` – logical and arithmetic expressions for flexible evaluation.
- `Collections` – reactive collections such as arrays, lists, dictionaries, and sets.
- `Time` – timers, cooldowns, countdowns, stopwatches, and time sources.
- `Unity Components` – MonoBehaviour wrappers for animation, collision, and trigger events.
- `Utils` – helper classes including disposable management, optional values, and reference wrappers.

Use this documentation as a guide for integrating `Atomic.Elements` into your `Unity` projects or `C#` applications, leveraging reactive patterns, composable structures, and modular design.

- **Values**
  - [IValue](Values/IValue.md) <!-- + -->
  - [IReactiveValue](Values/IReactiveValue.md) <!-- + -->
  - [Const](Values/Const.md) <!-- + -->
  - [ScriptableConst](Values/ScriptableConst.md) <!-- + -->
  - [DefaultConstants](Values/DefaultConstants.md) <!-- + -->
  - [Extensions](Values/Extensions.md) <!-- + -->
- **Variables**
  - [IVariable](Variables/IVariable.md) <!-- + -->
  - [BaseVariable](Variables/BaseVariable.md) <!-- + -->
  - [IReactiveVariable](Variables/IReactiveVariable.md) <!-- + -->
  - [ReactiveVariable](Variables/ReactiveVariable.md) <!-- + -->
  - [ProxyVariable](Variables/ProxyVariable.md)  <!-- + -->
  - [ReactiveProxyVariable](Variables/ReactiveProxyVariable.md)  <!-- + -->
  - [Extensions](Variables/Extensions.md) <!-- + -->
- **Actions**
  - [IAction](Actions/IAction.md) <!-- + -->
  - [InlineAction](Actions/InlineAction.md)  <!-- + -->
  - [CompositeAction](Actions/CompositeAction.md) <!-- + -->
  - [PrintAction](Actions/PrintAction.md) <!-- + -->
  - [SceneActionAbstract](Actions/SceneActionAbstract.md)  <!-- + -->
  - [SceneActionDefault](Actions/SceneActionDefault.md) <!-- + -->
  - [SceneActionComposite](Actions/SceneActionComposite.md) <!-- + -->
  - [SceneActionReference](Actions/SceneActionReference.md) <!-- + -->
  - [Extensions](Actions/Extensions.md) <!-- + -->
- **Functions**
  - [IFunction](Functions/IFunction.md) <!-- + -->
  - [InlineFunction](Functions/InlineFunction.md)  <!-- + -->
  - [IPredicate](Functions/IPredicate.md)  <!-- + -->
  - [InlinePredicate](Functions/InlinePredicate.md)  <!-- + -->
  - [Extensions](Functions/Extensions.md) <!-- + -->
- **Setters**
  - [ISetter](Setters/ISetter.md) <!-- + -->
  - [InlineSetter](Setters/InlineSetter.md) <!-- + -->
- **Observables**
  - [ISignal](Signals/ISignal.md) <!-- + -->
  - [InlineSignal](Signals/InlineSignal.md) <!-- + -->
  - [IEvent](Events/IEvent.md) <!-- + -->
  - [BaseEvent](Events/BaseEvent.md) <!-- + -->
  - [Subscription](Signals/Subscription.md) <!-- + -->
  - [Extensions](Signals/Extensions.md) <!-- + -->
- **Requests**
  - [IRequest](Requests/IRequest.md) <!-- + -->
  - [BaseRequest](Requests/BaseRequest.md) <!-- + -->
- **Expressions**
  - [IExpression](Expressions/IExpression.md) <!-- + -->
  - [ExpressionBase](Expressions/ExpressionBase.md) <!-- + -->
  - [AndExpression](Expressions/AndExpression.md) <!-- + -->
  - [OrExpression](Expressions/OrExpression.md) <!-- + -->
  - [IntMulExpression](Expressions/IntMulExpression.md) <!-- + -->
  - [IntSumExpression](Expressions/IntSumExpression.md) <!-- + -->
  - [FloatMulExpression](Expressions/FloatMulExpression.md) <!-- + -->
  - [FloatSumExpression](Expressions/FloatSumExpression.md) <!-- + -->
  - [InlineExpression](Expressions/InlineExpression.md) <!-- + -->
- **Collections**
  - [IReadOnlyReactiveCollection](Collections/IReadOnlyReactiveCollection.md) 
  - 
  - [IReadOnlyReactiveArray](Collections/IReadOnlyReactiveArray.md)
  - [IReactiveArray](Collections/IReactiveArray.md)
  - [ReactiveArray](Collections/ReactiveArray.md) 
  - [IReadOnlyReactiveList](Collections/IReadOnlyReactiveList.md) 
  - [IReactiveList](Collections/IReactiveList.md) 
  - [ReactiveList](Collections/ReactiveList.md) 
  - [ReactiveLinkedList](Collections/ReactiveLinkedList.md)
  - [IReadOnlyReactiveDictionary](Collections/IReadOnlyReactiveDictionary.md)
  - [IReactiveDictionary](Collections/IReactiveDictionary.md)
  - [ReactiveDictionary](Collections/ReactiveDictionary.md)
  - [IReadOnlyReactiveHashSet](Collections/IReadOnlyReactiveHashSet.md)
  - [IReactiveHashSet](Collections/IReactiveHashSet.md)
  - [ReactiveHashSet](Collections/ReactiveHashSet.md)
- **Time**
  - [Sources](Time/Sources.md)  <!-- + -->
  - [ICooldown](Time/ICooldown.md) <!-- + -->
  - [Cooldown](Time/Cooldown.md) <!-- + -->
  - [RandomCooldown](Time/RandomCooldown.md) <!-- + -->
  - [ITimer](Time/ITimer.md) <!-- + -->
  - [UpTimer](Time/UpTimer.md) <!-- + -->
  - [DownTimer](Time/DownTimer.md) <!-- + -->
  - [TimerState](Time/TimerState.md) <!-- + -->
  - [IStopwatch](Time/IStopwatch.md) <!-- + -->
  - [Stopwatch](Time/Stopwatch.md) <!-- + -->
  - [StopwatchState](Time/StopwatchState.md) <!-- + -->
  - [IPeriod](Time/IPeriod.md) <!-- + -->
  - [Period](Time/Period.md) <!-- + -->
  - [PeriodState](Time/PeriodState.md) <!-- + -->
  - [ITimestamp](Time/ITimestamp.md) <!-- + -->
  - [FixedTimestamp](Time/FixedTimestamp.md) <!-- + -->
  - [Extensions](Time/Extensions.md) <!-- + -->
- **Unity Components**
  - [AnimationEvents](UnityComponents/AnimationEvents.md)
  - [TriggerEvents](UnityComponents/TriggerEvents.md)
  - [CollisionEvents](UnityComponents/CollisionEvents.md)
- **Utils**
  - [Delegates](Collections/Delegates.md) 
  - [DisposableComposite](Utils/DisposableComposite.md)
  - [Reference](Utils/Reference.md)
  - [Optional](Utils/Optional.md)

## 🔥 Performance

The performance comparison below was measured on a **MacBook with Apple M1** for collections containing **1000 elements of type `object`**.  

**Collections**
  - [ReactiveArray](Collections/ReactiveArray.md/#-performance) – performance benchmarks for reactive arrays.
  - [ReactiveList](Collections/ReactiveList.md/#-performance) – performance benchmarks for reactive lists.
  - [ReactiveLinkedList](Collections/ReactiveLinkedList.md/#-performance) – performance benchmarks for reactive linked lists.
  - [ReactiveDictionary](Collections/ReactiveDictionary.md/#-performance) – performance benchmarks for reactive dictionaries.
  - [ReactiveHashSet](Collections/ReactiveHashSet.md/#-performance) – performance benchmarks for reactive hash sets.