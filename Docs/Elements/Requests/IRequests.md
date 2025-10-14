# 🧩 IRequests

Represent **deferred actions** that can be executed at a later time. It is particularly useful for scenarios where
input is collected in one phase (e.g., `Update`) but processed in another (e.g., `FixedUpdate`).
Requests also help **prevent duplicate commands** by ensuring the same request is not processed multiple times while
active. It extends the [IAction](../Actions/IActions.md) interface and provides **required flags** and **argument retrieval /
consumption** functionality.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Parameterless Request](#ex-1)
    - [Single-Argument Request](#ex-2)
- [API Reference](#-api-reference)

---

## 🗂 Examples of Usage

<div id="ex-1"></div>

### 1️⃣ Parameterless Request

```csharp
IRequest shootRequest = ...

// Somewhere in the UI system we mark it as required
shootRequest.Invoke();

// Later in the game loop or system update
if (shoot.Required)
{
    Debug.Log("Shoot request detected!");
}
 
// Handle it
if (shoot.Consume())
{
    Debug.Log("Shoot request consumed successfully.");
}
```

<div id="ex-2"></div>

### 2️⃣ Single-Argument Request

```csharp
IRequest<Character> damageRequest = ...

// Trigger the request from gameplay logic
damageRequest.Invoke(targetCharacter);

// Somewhere in a system that processes damage
if (damageRequest.TryGet(out Character target))
{
    Debug.Log($"Applying damage is required to {target.Name}");
}

if (damageRequest.Consume(out target))
{
    Debug.Log("Damage request handled and consumed.");
}
```

---

## 🔍 API Reference

There are several interfaces of requests, depending on the number of arguments they take:

- [IRequest](IRequest.md) — Non-generic version; works without parameters.
- [IRequest&lt;T&gt;](IRequest%601.md) — Request that takes one argument.
- [IRequest&lt;T1, T2&gt;](IRequest%602.md) — Request that takes two arguments.
- [IRequest&lt;T1, T2, T3&gt;](IRequest%603.md) — Request that takes three arguments.
- [IRequest&lt;T1, T2, T3, T4&gt;](IRequest%604.md) — Request that takes four arguments.
