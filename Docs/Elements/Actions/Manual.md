# 🧩 Actions

Provide a set of abstractions for defining and invoking logic with varying numbers of input parameters. These action
types are lightweight and flexible, making them ideal for use in **event systems, command patterns, or reactive
programming**. They allow developers to encapsulate behavior, combine multiple actions, or reference scene-specific
logic in a clean, reusable way.

There are several abstractions of actions, depending on the number of arguments the actions take:

- [IActions](IActions.md) <!-- + -->
    - [IAction](IAction.md)  <!-- + -->
    - [IAction&lt;T&gt;](IAction%601.md)  <!-- + -->
    - [IAction&lt;T1, T2&gt;](IAction%602.md)  <!-- + -->
    - [IAction&lt;T1, T2, T3&gt;](IAction%603.md) <!-- + -->
    - [IAction&lt;T1, T2, T3, T4&gt;](IAction%604.md) <!-- + -->
- [InlineActions](InlineActions.md) <!-- + -->
    - [InlineAction](InlineAction.md) <!-- + -->
    - [InlineAction&lt;T&gt;](InlineAction%601.md) <!-- + -->
    - [InlineAction&lt;T1, T2&gt;](InlineAction%602.md) <!-- + -->
    - [InlineAction&lt;T1, T2, T3&gt;](InlineAction%603.md) <!-- + -->
    - [InlineAction&lt;T1, T2, T3, T4&gt;](InlineAction%604.md) <!-- + -->
- [CompositeActions](CompositeActions.md) <!-- + -->
    - [CompositeAction](CompositeAction.md) <!-- + -->
    - [CompositeAction&lt;T&gt;](CompositeAction%601.md) <!-- + -->
    - [CompositeAction&lt;T1, T2&gt;](CompositeAction%602.md)  <!-- + -->
    - [CompositeAction&lt;T1, T2, T3&gt;](CompositeAction%603.md) <!-- + -->
    - [CompositeAction&lt;T1, T2, T3, T4&gt;](CompositeAction%604.md)  <!-- + -->
- [SceneActions Abstract](SceneActionsAbstract.md)  <!-- + -->
    - [SceneActionAbstract](SceneActionAbstract.md)  <!-- + -->
    - [SceneActionAbstract&lt;T&gt;](SceneActionAbstract%601.md)  <!-- + -->
    - [SceneActionAbstract&lt;T1, T2&gt;](SceneActionAbstract%602.md) <!-- + -->
    - [SceneActionAbstract&lt;T1, T2, T3&gt;](SceneActionAbstract%603.md)  <!-- + -->
    - [SceneActionAbstract&lt;T1, T2, T3, T4&gt;](SceneActionAbstract%604.md)  <!-- + -->
- [SceneActions Default](SceneActionsDefault.md) <!-- + -->
    - [SceneActionDefault](SceneActionDefault.md) <!-- + -->
    - [SceneActionDefault&lt;T&gt;](SceneActionDefault%601.md) <!-- + -->
    - [SceneActionDefault&lt;T1, T2&gt;](SceneActionDefault%602.md) <!-- + -->
    - [SceneActionDefault&lt;T1, T2, T3&gt;](SceneActionDefault%603.md) <!-- + -->
    - [SceneActionDefault&lt;T1, T2, T3, T4&gt;](SceneActionDefault%604.md) <!-- + -->
- [SceneActions Composite](SceneActionsComposite.md) <!-- + -->
    - [SceneActionComposite](SceneActionComposite.md) <!-- + -->
    - [SceneActionComposite&lt;T&gt;](SceneActionComposite%601.md) <!-- + -->
    - [SceneActionComposite&lt;T1, T2&gt;](SceneActionComposite%602.md) <!-- + -->
    - [SceneActionComposite&lt;T1, T2, T3&gt;](SceneActionComposite%603.md) <!-- + -->
    - [SceneActionComposite&lt;T1, T2, T3, T4&gt;](SceneActionComposite%604.md)  <!-- + -->
- [SceneActions Reference](SceneActionsReference.md) <!-- + -->
    - [SceneActionReference](SceneActionReference.md) <!-- + -->
    - [SceneActionReference&lt;T&gt;](SceneActionReference%601.md) <!-- + -->
    - [SceneActionReference&lt;T1, T2&gt;](SceneActionReference%602.md) <!-- + -->
    - [SceneActionReference&lt;T1, T2, T3&gt;](SceneActionReference%603.md) <!-- + -->
    - [SceneActionReference&lt;T1, T2, T3, T4&gt;](SceneActionReference%604.md) <!-- + -->
- [PrintAction](PrintAction.md)
- Extensions
  - [InvokeRange](ExtensionsInvokeRange.md)