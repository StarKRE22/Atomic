# 🧩 IRequests

Represents a **deferred action** that can be executed at a later time. It is particularly useful for scenarios where
input is collected in one phase (e.g., `Update`) but processed in another (e.g., `FixedUpdate`).
Requests also help **prevent duplicate commands** by ensuring the same request is not processed multiple times while
active.

It extends the [IAction](../Actions/IActions.md) interface and provides **required flags** and **argument retrieval /
consumption** functionality.

There are several interfaces of requests, depending on the number of arguments they take:

- [IRequest](IRequest.md) — Non-generic version; works without parameters.
- [IRequest&lt;T&gt;](IRequest%601.md) — Request that takes one argument.
- [IRequest&lt;T1, T2&gt;](IRequest%602.md) — Request that takes two arguments.
- [IRequest&lt;T1, T2, T3&gt;](IRequest%603.md) — Request that takes three arguments.
- [IRequest&lt;T1, T2, T3, T4&gt;](IRequest%604.md) — Request that takes four arguments.
