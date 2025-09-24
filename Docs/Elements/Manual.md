# ⚛️ Atomic.Elements

A library of atomic elements for constructing complex game objects and systems in `Unity` and `C#`.
The solution includes **constants, variables, reactive properties, collections, events, and actions**, enabling
developers to quickly assemble any game entity like a `LEGO Constructor`.

## 📑 Table of Contents

- [Requirements](#-requirements)
- [Using Odin Inspector](#-using-odin-inspector)
- [API Reference](#-api-reference)
    - [Values](#-values)
    - [Variables](#-variables)
    - [Actions](#-actions)
    - [Functions](#-functions)
    - [Setters](#-setters)
    - [Events](#-events)
    - [Requests](#-requests)
    - [Expressions](#-expressions)
    - [Collections](#-collections)
    - [Time](#-time)
    - [Utilities](#-utilities)
- [Best Practices](#-best-practices)
- [Performance](#-performance)

---

## 📝 Requirements

The Atomic.Elements requires **Unity 6** or **.NET 7+**. Make sure your development environment meets these requirements
before using the framework.

---

## 🎛 Using Odin Inspector

For better **debugging**, **configuration**, and **visualization** of game state, we **optionally recommend**
using [Odin Inspector](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041). The
framework **works without Odin**, but Odin makes inspection and tweaking much easier.

---

## 🔍 API Reference

This section provides a comprehensive overview of the core interfaces, classes, and utilities in `Atomic.Elements`. It
covers tools for **reactive programming, event handling, Unity integration, and general-purpose development**. Use this
section as a guide for integrating `Atomic.Elements` into your Unity or C# projects, taking advantage of **reactive
patterns, composable structures, and modular design**.

### 🧩 Values

Provides a set of interfaces and classes for working with **reactive values and constants**. It allows developers to
handle both immutable constants and dynamic, reactive data in a consistent way. This is particularly useful in scenarios
where values need to be observed or updated in real-time, such as game development, UI bindings, or simulation systems.

- [IValue](Values/IValue.md)
- [IReactiveValue](Values/IReactiveValue.md)
- [Const](Values/Const.md)
- [ScriptableConst](Values/ScriptableConst.md)
- [DefaultConstants](Values/DefaultConstants.md)
- [Extensions](Values/Extensions.md)

### 🧩 Variables

Provides a set of interfaces and classes for working with **reactive variables, proxy variables, and Unity-specific
variable types**. It builds on the concept of reactive values but adds more flexibility by allowing variables to act as
intermediaries or proxies, which can observe, modify, or synchronize underlying data.

- [IVariable](Variables/IVariable.md)
- [BaseVariable](Variables/BaseVariable.md)
- [IReactiveVariable](Variables/IReactiveVariable.md)
- [ReactiveVariable](Variables/ReactiveVariable.md)
- [ProxyVariable](Variables/ProxyVariable.md)
- [ReactiveProxyVariable](Variables/ReactiveProxyVariable.md)
- [Extensions](Variables/Extensions.md)

### 🧩 Actions

Provides a set of abstractions for defining and invoking logic with varying numbers of input parameters. These action
types are lightweight and flexible, making them ideal for use in **event systems, command patterns, or reactive
programming**. They allow developers to encapsulate behavior, combine multiple actions, or reference scene-specific
logic in a clean, reusable way.

There are several abstractions of actions, depending on the number of arguments the actions take:

- [IActions](Actions/IActions.md) <!-- + -->
    - [IAction](Actions/IAction.md)  <!-- + -->
    - [IAction&lt;T&gt;]()  <!-- + -->
    - [IAction&lt;T1, T2&gt;]()  <!-- + -->
    - [IAction&lt;T1, T2, T3&gt;]() <!-- + -->
    - [IAction&lt;T1, T2, T3, T4&gt;]() <!-- + -->
- [InlineActions]() <!-- + -->
    - [InlineAction]() <!-- + -->
    - [InlineAction&lt;T&gt;]() <!-- + -->
    - [InlineAction&lt;T1, T2&gt;]() <!-- + -->
    - [InlineAction&lt;T1, T2, T3&gt;]() <!-- + -->
    - [InlineAction&lt;T1, T2, T3, T4&gt;]() <!-- + -->
- [CompositeActions](Actions/CompositeActions.md)
    - [CompositeAction](Actions/CompositeAction.md)
    - [CompositeAction&lt;T&gt;](Actions/CompositeAction%601.md)
    - [CompositeAction&lt;T1, T2&gt;](Actions/CompositeAction%602.md)
    - [CompositeAction&lt;T1, T2, T3&gt;](Actions/CompositeAction%603.md)
    - [CompositeAction&lt;T1, T2, T3, T4&gt;](Actions/CompositeAction%604.md)
- [SceneActions Abstract]()
    - [SceneActionAbstract]()
    - [SceneActionAbstract&lt;T&gt;]()
    - [SceneActionAbstract&lt;T1, T2&gt;]()
    - [SceneActionAbstract&lt;T1, T2, T3&gt;]()
    - [SceneActionAbstract&lt;T1, T2, T3, T4&gt;]()
- [SceneActions Default]()
    - [SceneActionDefault]()
    - [SceneActionDefault&lt;T&gt;]()
    - [SceneActionDefault&lt;T1, T2&gt;]()
    - [SceneActionDefault&lt;T1, T2, T3&gt;]()
    - [SceneActionDefault&lt;T1, T2, T3, T4&gt;]()
- [SceneActions Composite]()
    - [SceneActionComposite]()
    - [SceneActionComposite&lt;T&gt;]()
    - [SceneActionComposite&lt;T1, T2&gt;]()
    - [SceneActionComposite&lt;T1, T2, T3&gt;]()
    - [SceneActionComposite&lt;T1, T2, T3, T4&gt;]()
- [SceneActions Reference]()
    - [SceneActionReference]()
    - [SceneActionReference&lt;T&gt;]()
    - [SceneActionReference&lt;T1, T2&gt;]()
    - [SceneActionReference&lt;T1, T2, T3&gt;]()
    - [SceneActionReference&lt;T1, T2, T3, T4&gt;]()
- [PrintAction](Actions/PrintAction.md)
- [Extensions](Actions/Extensions.md)

### 🧩 Functions

Provides a set of abstractions for defining logic that **returns a value** and can accept varying numbers of input
parameters. These function types are lightweight and flexible, making them ideal for **callbacks, computations, or
functional programming patterns**. They allow developers to encapsulate reusable logic, implement predicates, and create
inline or composable functions for clean and maintainable code.

- [IFunction](Functions/IFunction.md)
- [InlineFunction](Functions/InlineFunction.md)
- [IPredicate](Functions/IPredicate.md)
- [InlinePredicate](Functions/InlinePredicate.md)
- [Extensions](Functions/Extensions.md)

### 🧩 Setters

Provides interfaces and classes for **assigning values**. It offers a lightweight and consistent way to encapsulate
value assignment logic, enabling clean, reusable, and decoupled code for modifying data.

- [ISetter](Setters/ISetter.md)
- [InlineSetter](Setters/InlineSetter.md)

### 🧩 Events

Provides a set of abstractions for **events, subscriptions, and signals**. It allows developers to define, subscribe to,
and trigger events in a decoupled and reactive manner, making it ideal for event-driven architectures and real-time
systems.

- [ISignal](Signals/ISignal.md)
- [InlineSignal](Signals/InlineSignal.md)
- [IEvent](Events/IEvent.md)
- [BaseEvent](Events/BaseEvent.md)
- [Subscription](Signals/Subscription.md)
- [Extensions](Signals/Extensions.md)

### 🧩 Requests

Represents **deferred actions** that can be executed at a later time. It is particularly useful for scenarios where
input is collected in one phase (e.g., `Update`) but processed in another (e.g., `FixedUpdate`). Requests also help *
*prevent duplicate commands** by ensuring the same request is not processed multiple times while active.

- [IRequest](Requests/IRequest.md)
- [BaseRequest](Requests/BaseRequest.md)

### 🧩 Expressions

Represents **expressions composed of function members** that can be dynamically added, removed, and evaluated. It
supports both parameterless functions and functions with one or more parameters, enabling flexible and reusable logic
composition.

- [IExpression](Expressions/IExpression.md)
- [ExpressionBase](Expressions/ExpressionBase.md)
- [AndExpression](Expressions/AndExpression.md)
- [OrExpression](Expressions/OrExpression.md)
- [IntMulExpression](Expressions/IntMulExpression.md)
- [IntSumExpression](Expressions/IntSumExpression.md)
- [FloatMulExpression](Expressions/FloatMulExpression.md)
- [FloatSumExpression](Expressions/FloatSumExpression.md)
- [InlineExpression](Expressions/InlineExpression.md)

### 🧩 Collections

Provides a set of **reactive collection types** such as arrays, lists, dictionaries, and sets. These collections
automatically notify subscribers of changes, making them ideal for **data binding, UI updates, and reactive programming
**. Both read-only and fully mutable reactive collections are supported, allowing fine-grained control over data access
and modification.

- [IReadOnlyReactiveCollection](Collections/IReadOnlyReactiveCollection.md)
- [IReactiveCollection](Collections/IReactiveCollection.md)
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
- [IReactiveSet](Collections/IReactiveHashSet.md)
- [ReactiveHashSet](Collections/ReactiveHashSet.md)

### 🧩 Time

Provides a set of tools for managing **timers, cooldowns, countdowns, stopwatches, and time sources**. It allows
developers to track and control time-related events in a consistent and reactive manner, making it useful for gameplay
mechanics, scheduling, and periodic updates. The module supports flexible time representations, including fixed and
variable intervals, as well as reactive notifications for state changes.

- [Time Sources](Time/Sources.md)
- [ICooldown](Time/ICooldown.md)
- [Cooldown](Time/Cooldown.md)
- [RandomCooldown](Time/RandomCooldown.md)
- [ITimer](Time/ITimer.md)
- [UpTimer](Time/UpTimer.md)
- [DownTimer](Time/DownTimer.md)
- [TimerState](Time/TimerState.md)
- [IStopwatch](Time/IStopwatch.md)
- [Stopwatch](Time/Stopwatch.md)
- [StopwatchState](Time/StopwatchState.md)
- [IPeriod](Time/IPeriod.md)
- [Period](Time/Period.md)
- [PeriodState](Time/PeriodState.md)
- [ITimestamp](Time/ITimestamp.md)
- [FixedTimestamp](Time/FixedTimestamp.md)
- [Extensions](Time/Extensions.md)

### 🧩 Utilities

Provides a collection of **utility classes and components** that simplify common tasks in Unity and C# development. This
includes handling animation and collision events, trigger detection, disposable actions, optional references, and
various helper extensions. These utilities help reduce boilerplate code and make systems more modular and maintainable.

- [AnimationEvents](UnityComponents/AnimationEvents.md)
- [CollisionEvents](UnityComponents/CollisionEvents.md)
- [CollisionEvents2D](UnityComponents/CollisionEvents2D.md)
- [TriggerEvents](UnityComponents/TriggerEvents.md)
- [TriggerEvents2D](UnityComponents/TriggerEvents2D.md)
- [DisposableAction](Utils/DisposableAction.md)
- [DisposableComposite](Utils/DisposableComposite.md)
- [Reference](Utils/Reference.md)
- [Optional](Utils/Optional.md)
- [Extensions](Utils/Extensions.md)

---

## 📌 Best Practices

This section outlines **recommended approaches and patterns** when working with the `Atomic` framework. Following these
practices will help you write **modular, testable, and high-performance code**, whether you’re developing single-player
or multiplayer games.

- [Prefer Atomic Interfaces to Concrete Classes](../../Docs/BestPractices/PreferAbstractInterfaces.md)
- [Use Shared Constants](../../Docs/BestPractices/SharedConstants.md)
- [Iterating over Reactive Collections](../../Docs/BestPractices/IteratingReactiveCollections.md)
- [Requests vs Actions](../../Docs/BestPractices/RequestsVsActions.md)
- [Request-Condition-Action-Event Flow](../../Docs/BestPractices/RequestConditionActionEvent.md)
- [Using InlineActions](Actions/InlineAction.md/#-best-practice)
- [Using InlineFunctions](Functions/InlineFunction.md/#-best-practice)
- [Using Cooldowns](Time/Cooldown.md/#-best-practice)
- [Insert Constant to AndExpression](Expressions/AndExpression.md/#-best-practice)
- [Choosing Between Timer and Cooldown](Time/ITimer.md/#-best-practice)
- [Using Observe Extension Method](Values/IReactiveValue.md/#-best-practice)
- [Using Subscriptions with DisposeComposite](Signals/Subscription.md/#-best-practice)
- [Using Optional Wrappers](Utils/Optional.md/#-example-of-usage)

---

## 🔥 Performance

The performance comparison below was measured on a **MacBook with Apple M1** for collections containing **1000 elements
of type `object`**.

### Collections

- [ReactiveArray](Collections/ReactiveArray.md/#-performance) – performance benchmarks for reactive arrays.
- [ReactiveList](Collections/ReactiveList.md/#-performance) – performance benchmarks for reactive lists.
- [ReactiveLinkedList](Collections/ReactiveLinkedList.md/#-performance) – performance benchmarks for reactive linked
  lists.
- [ReactiveDictionary](Collections/ReactiveDictionary.md/#-performance) – performance benchmarks for reactive
  dictionaries.
- [ReactiveHashSet](Collections/ReactiveHashSet.md/#-performance) – performance benchmarks for reactive hash sets.