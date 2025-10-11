# 🧩 CompositeActions

Represent **groups of actions** that implement the corresponding [IAction](IActions.md)
interfaces. Its follow the [Composite Pattern](https://en.wikipedia.org/wiki/Composite_pattern) — an action both
**groups actions** and itself **acts as a single action**, preserving a uniform interface.
This allows combining multiple actions into a sequence, which will be invoked **sequentially** when triggered. This is
especially important when game objects and scripts need to execute complex action scenarios.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)

---

## 🗂 Example of Usage

Below is an example of using composite action for game startup:

```csharp
IAction startupAction = new CompositeAction(
    new ActivatePlayerAction(), //IAction
    new ActivateEnemiesAction(), //IAction
    new ActivateWeapons(), //IAction
    new ActivateGameTimerAction(), //IAction
);

```

Usage:

```csharp
startupAction.Invoke();
```

---

## 🔍 API Reference

There are several implementations of composite actions, depending on the number of arguments the actions take:

- [CompositeAction](CompositeAction.md) — Non-generic version; works without parameters.
- [CompositeAction&lt;T&gt;](CompositeAction%601.md) — Holds actions that take one argument.
- [CompositeAction&lt;T1, T2&gt;](CompositeAction%602.md) — Holds actions that take two arguments .
- [CompositeAction&lt;T1, T2, T3&gt;](CompositeAction%603.md) — Holds actions that take three arguments.
- [CompositeAction&lt;T1, T2, T3, T4&gt;](CompositeAction%604.md) — Holds actions that take four arguments.