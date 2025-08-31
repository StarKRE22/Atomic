# ⚛️ Atomic.Elements

`Atomic.Elements` is a modular and reusable component library for Unity and C#, designed to simplify and accelerate game development. It provides a set of reactive components and data structures that can be easily integrated into different projects, offering flexibility and scalability.

## 🔍 Table of Contents
- [Requirements](#requirements)
- [Using Odin Inspector](#using-odin-inspector)
- [Documentation](#documentation)
- [Performance](#performance)
- [Best Practices](#best-practices)

## Requirements
> [!IMPORTANT]  
> The Atomic Framework requires **Unity 6** or **.NET 7+**.  
> Make sure your development environment meets these requirements before using the framework.

## Using Odin Inspector
> [!TIP]  
> For better **debugging**, **configuration**, and **visualization** of game state, we **optionally recommend** using [Odin Inspector](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041).  
> The framework **works without Odin**, but Odin makes inspection and tweaking much easier.

## Documentation

This section provides a complete reference to the core interfaces, classes, and utilities designed for reactive programming, event handling, Unity integration, and general-purpose development.

Here you will find detailed explanations, usage examples, and extension methods for:

- **Values** – interfaces and implementations for reactive and constant values.
- **Variables** – reactive variables, proxies, and Unity-specific variable types.
- **Actions & Functions** – reusable actions, predicates, and function abstractions.
- **Setters** – inline and interface-based property setters.
- **Events & Signals** – event abstractions, subscriptions, and reactive signals.
- **Requests** – encapsulated request objects for decoupled communication.
- **Expressions** – logical and arithmetic expressions for flexible evaluation.
- **Collections** – reactive collections such as arrays, lists, dictionaries, and sets.
- **Time** – timers, cooldowns, countdowns, stopwatches, and time sources.
- **Unity Components** – MonoBehaviour wrappers for animation, collision, and trigger events.
- **Utils** – helper classes including disposable management, optional values, and reference wrappers.

Use this documentation as a guide for integrating Atomic.Elements into your Unity projects or C# applications, leveraging reactive patterns, composable structures, and modular design.

- **Values**
  - [IValue](Values/IValue.md)
  - [IReactiveValue](Values/IReactiveValue.md)
  - [Const](Values/Const.md)
  - [DefaultConstants](Values/DefaultConstants.md)
  - [Extensions](Values/Extensions.md)
- **Variables**
  - [IVariable](Variables/IVariable.md)
  - [BaseVariable](Variables/BaseVariable.md)
  - [ReactiveVariable](Variables/ReactiveVariable.md)
  - [ProxyVariable](Variables/ProxyVariable.md)
  - [ReactiveProxyVariable](Variables/ReactiveProxyVariable.md)
  - [UnitySpecificVariables](Variables/UnitySpecificVariables.md)
  - [Extensions](Variables/Extensions.md)
- **Actions & Functions**
  - [IAction](Actions/IAction.md)
  - [InlineAction](Actions/InlineAction.md)
  - [CompositeAction](Actions/CompositeAction.md)
  - [UnitySpecificActions](Actions/UnitySpecificActions.md)
  - [Extensions](Actions/Extensions.md)
  - [IFunction](Functions/IFunction.md)
  - [InlineFunction](Functions/InlineFunction.md)
  - [IPredicate](Functions/IPredicate.md)
  - [InlinePredicate](Functions/InlinePredicate.md)
  - [Extensions](Functions/Extensions.md)
- **Setters**
  - [ISetter](Setters/ISetter.md)
  - [InlineSetter](Setters/InlineSetter.md)
- **Events & Signals**
  - [IEvent](Events/IEvent.md)
  - [BaseEvent](Events/BaseEvent.md)
  - [ISignal](Signals/ISignal.md)
  - [InlineSignal](Signals/InlineSignal.md)
  - [Subscription](Signals/Subscription.md)
  - [Extensions](Signals/Extensions.md)
- **Requests**
  - [IRequest](Requests/IRequest.md)
  - [BaseRequest](Requests/BaseRequest.md)
- **Expressions**
  - [IExpression](Expressions/IExpression.md)
  - [ExpressionBase](Expressions/ExpressionBase.md)
  - [InlineExpression](Expressions/InlineExpression.md)
  - [AndExpression](Expressions/AndExpression.md)
  - [OrExpression](Expressions/OrExpression.md)
  - [IntMulExpression](Expressions/IntMulExpression.md)
  - [IntSumExpression](Expressions/IntSumExpression.md)
  - [FloatMulExpression](Expressions/FloatMulExpression.md)
  - [FloatSumExpression](Expressions/FloatSumExpression.md)
  - [Extensions](Expressions/Extensions.md)
- **Collections**
  - [ReactiveArray](Collections/ReactiveArray.md)
  - [ReactiveList](Collections/ReactiveList.md)
  - [ReactiveLinkedList](Collections/ReactiveLinkedList.md)
  - [ReactiveDictionary](Collections/ReactiveDictionary.md)
  - [ReactiveHashSet](Collections/ReactiveHashSet.md)
- **Time**
  - [Cooldown](Time/Cooldown.md)
  - [Timer](Time/Timer.md)
  - [Countdown](Time/Countdown.md)
  - [Stopwatch](Time/Stopwatch.md)
  - [Period](Time/Period.md)
  - [Timestamp](Time/Timestamp.md)
  - [Extensions](Time/Extensions.md)
  - [Source Interfaces](Time/SourceInterfaces.md)
- **Unity Components**
  - [AnimationEvents](UnityComponents/AnimationEvents.md)
  - [TriggerEvents](UnityComponents/TriggerEvents.md)
  - [CollisionEvents](UnityComponents/CollisionEvents.md)
- **Utils**
  - [DisposableComposite](Utils/DisposableComposite.md)
  - [Reference](Utils/Reference.md)
  - [Optional](Utils/Optional.md)

## Performance

The performance comparison below was measured on a **MacBook with Apple M1** for collections containing **1000 elements of type `object`**.  
The table shows median execution times of key operations, illustrating the overhead of the reactive wrapper compared to a standard `HashSet<T>`.

**Collections**
  - [ReactiveArray](Collections/ReactiveArray.md/#performance) – performance benchmarks for reactive arrays.
  - [ReactiveList](Collections/ReactiveList.md/#performance) – performance benchmarks for reactive lists.
  - [ReactiveLinkedList](Collections/ReactiveLinkedList.md/#performance) – performance benchmarks for reactive linked lists.
  - [ReactiveDictionary](Collections/ReactiveDictionary.md/#performance) – performance benchmarks for reactive dictionaries.
  - [ReactiveHashSet](Collections/ReactiveHashSet.md/#performance) – performance benchmarks for reactive hash sets.

## Best Practices






