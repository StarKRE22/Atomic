# 🧩 SceneActions Abstract

Define **scene-based actions** in Unity that implement the corresponding [IAction](IActions.md) interfaces.
These abstract classes inherit from `MonoBehaviour`, allowing actions to be attached to GameObjects in a scene.
They serve as a base for **custom scene logic** and are designed to be subclassed to implement specific behavior.

> [!TIP]
> Extremely useful for cutscenes, trigger-based actions, level initialization, and similar scene-driven logic.

There are several implementations of abstract scene actions, depending on the number of arguments the actions take:

- [SceneActionAbstract](SceneActionAbstract.md) — Non-generic version; works without parameters.
- [SceneActionAbstract&lt;T&gt;](SceneActionAbstract%601.md) — Action that take one argument.
- [SceneActionAbstract&lt;T1, T2&gt;](SceneActionAbstract%602.md) — Action that take two arguments.
- [SceneActionAbstract&lt;T1, T2, T3&gt;](SceneActionAbstract%603.md) — Action that take three arguments.
- [SceneActionAbstract&lt;T1, T2, T3, T4&gt;](SceneActionAbstract%604.md) — Action that take four arguments.