# ⚛️ Atomic.Elements

A library of atomic elements for constructing complex game objects and systems in `Unity` and `C#`.
The solution includes **constants, variables, reactive properties, collections, events, and actions**, enabling
developers to quickly assemble any game entity like a `LEGO Constructor`.

## 📑 Table of Contents

- [Requirements](#-requirements)
- [Using Odin Inspector](#-using-odin-inspector)
- [API Reference](#-api-reference)
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

- [Values](Values/Manual.md)  <!-- + -->
- [Variables](Variables/Manual.md) <!-- + -->
- [Actions](Actions/Manual.md) <!-- + -->
- [Functions](Functions/Manual.md) <!-- + -->
- [Setters](Setters/Manual.md) <!-- + -->
- [Requests](Requests/Manual.md) <!-- + -->
- [Events](Events/Manual.md) <!-- + -->
- [Time](Time/Manual.md) <!-- + -->
- [Collections](Collections/Manual.md)
- [Expressions](Expressions/Manual.md)
- [Utilities](Utils/Manual.md) 

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
