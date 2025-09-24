# 🧩 IActions

Define a family of fundamental contracts for executing parameterized actions. They provide a lightweight
abstraction for invoking logic, often used in game mechanics and command patterns.

There are several implementations of actions, depending on the number of arguments the actions take:

- [IAction](IAction.md) — Non-generic version; works without parameters.
- [IAction&lt;T&gt;](IAction%601.md) — Action that take one argument.
- [IAction&lt;T1, T2&gt;](IAction%602.md) — Action that take two arguments.
- [IAction&lt;T1, T2, T3&gt;](IAction%603.md) — Action that take three arguments.
- [IAction&lt;T1, T2, T3, T4&gt;](IAction%604.md) — Action that take four arguments.
