# 🧩 Atomic.Elements

Represents a library of **atomic elements** for constructing complex game objects and systems in `Unity` and `C#`.
The solution includes **constants, variables, reactive properties, collections, events, and actions**, enabling
developers to quickly assemble any game entity like a **LEGO**.

---

## 📑 Table of Contents

- [Requirements](#-requirements)
- [Using Odin Inspector](#-using-odin-inspector)
- [API Reference](#-api-reference)
- [Performance](#-performance)

---

## 📝 Requirements

The library requires **Unity 6** or **.NET 7+**. Make sure your development environment meets these requirements
before using the framework.

---

## 🎛 Using Odin Inspector

For better **debugging**, **configuration**, and **visualization** of game state, we **optionally recommend**
using [Odin Inspector](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041). The
framework **works without Odin**, but Odin makes inspection and tweaking much easier.

---

## 🔍 API Reference

Provides a comprehensive overview of the core interfaces, classes, and utilities in the library. It
covers tools for **reactive programming, event handling, Unity integration, and general-purpose development**. Use this
section as a guide for integrating features into your Unity or C# projects, taking advantage of **reactive
patterns, composable structures, and modular design**.

- [Values](Values/Manual.md)  <!-- + -->
- [Variables](Variables/Manual.md) <!-- + -->
- [Actions](Actions/Manual.md) <!-- + -->
- [Functions](Functions/Manual.md) <!-- + -->
- [Setters](Setters/Manual.md) <!-- + -->
- [Requests](Requests/Manual.md) <!-- + -->
- [Events](Events/Manual.md) <!-- + -->
- [Time](Time/Manual.md) <!-- + -->
- [Collections](Collections/Manual.md) <!-- + -->
- [Expressions](Expressions/Manual.md) <!-- + -->
- [Utilities](Utils/Manual.md) <!-- + -->

---

## 🔥 Performance

The performance comparison below was measured on a **MacBook with Apple M1** for collections containing **1000 elements
of type `object`**.

- [ReactiveArray](Performance/ReactiveArrayPerformance.md) – performance benchmarks for reactive arrays.
- [ReactiveList](Performance/ReactiveListPerformance.md) – performance benchmarks for reactive lists.
- [ReactiveLinkedList](Performance/ReactiveLinkedListPerformance.md) – performance benchmarks for reactive linked
  lists.
- [ReactiveDictionary](Performance/ReactiveDictionaryPerformance.md) – performance benchmarks for reactive
  dictionaries.
- [ReactiveHashSet](Performance/ReactiveHashSetPerformance.md) – performance benchmarks for reactive hash sets.
