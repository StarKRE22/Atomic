# 🧩 Requests

The **Request** classes provide **concrete implementations** of the [IRequest](IRequests.md) interfaces. They are
designed to store request state and optionally one to four arguments. These classes **track whether a request is
required** and allow **deferred consumption**.

> [!IMPORTANT]
> Unlike regular actions, requests are meant for **deferred execution**. You can call `Invoke` to create a request, and
> process it later using `Consume`. Repeated `Invoke` calls before `Consume` do not create duplicates.

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
var shootRequest = new Request();

// Somewhere in the UI system we mark it as required
shootRequest.Invoke();

// Later in the game loop or system update
if (shoot.Required)
{
    Debug.Log("Shoot request detected!");
    
    // Consume it so it's not triggered again
    if (shoot.Consume())
    {
        Debug.Log("Shoot request consumed successfully.");
    }
}

```

<div id="ex-2"></div>

### 2️⃣ Single-Argument Request

```csharp
var damageRequest = new Request<Character>();

// Trigger the request from gameplay logic
damageRequest.Invoke(targetCharacter);

// Somewhere in a system that processes damage
if (damageRequest.TryGet(out Character target))
{
    Debug.Log($"Applying damage to {target.Name}");

    if (damageRequest.Consume(out target))
    {
        Debug.Log("Damage request handled and consumed.");
    }
}
```

---

## 🔍 API Reference

There are several implementations of requests, depending on the number of arguments they take:

- [Request](BaseRequest.md) — Non-generic version; works without parameters.
- [Request&lt;T&gt;](BaseRequest%601.md) — Request that takes one argument.
- [Request&lt;T1, T2&gt;](BaseRequest%602.md) — Request that takes two arguments.
- [Request&lt;T1, T2, T3&gt;](BaseRequest%603.md) — Request that takes three arguments.
- [Request&lt;T1, T2, T3, T4&gt;](BaseRequest%604.md) — Request that takes four arguments.
