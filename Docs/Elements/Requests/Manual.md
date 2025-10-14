# 🧩 Requests

Represents **deferred actions** that can be executed at a later time. It is particularly useful for scenarios where
input is collected in one phase (e.g., `Update`) but processed in another (e.g., `FixedUpdate`).
Requests also help **prevent duplicate commands** by ensuring the same request is not processed multiple times while
active.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
  - [Parameterless Request](#ex-1)
  - [Single-Argument Request](#ex-2)
  - [Component Usage](#ex-3)
- [API Reference](#-api-reference)
- [Notes](#-notes)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

<div id="ex-1"></div>

### 1️⃣ Parameterless Request

```csharp
// Assume this request signals a simple UI action
IRequest shootRequest = new BaseRequest();

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
// Create a request to damage a specific character
IRequest<Character> damageRequest = new BaseRequest<Character>();

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

<div id="ex-3"></div>

### 3️⃣ Component Usage

```csharp
public class AttackComponent : MonoBehaviour
{
    private readonly IRequest<GameObject> _request = new BaseRequest<GameObject>();
    
    public bool IsAttack => _request.Required;

    //Calls from Update — Input System or UI System
    public void Attack(GameObject target)
    {
        _request.Invoke(target);
    }
    
    private void FixedUpdate()
    {
        if (_request.Consume(out GameObject target))
        {
            // Deal damage to target or fire bullet...
        }
    }
}
```

---

## 🔍 API Reference

There are several interfaces and implementations of requests, depending on the concrete scenario and the number of
arguments:

- [IRequests](IRequests.md) <!-- + -->
    - [IRequest](IRequest.md) <!-- + -->
    - [IRequest&lt;T&gt;](IRequest%601.md) <!-- + -->
    - [IRequest&lt;T1, T2&gt;](IRequest%602.md) <!-- + -->
    - [IRequest&lt;T1, T2, T3&gt;](IRequest%603.md)  <!-- + -->
    - [IRequest&lt;T1, T2, T3, T4&gt;](IRequest%604.md) <!-- + -->
- [BaseRequests](BaseRequests.md) <!-- + -->
    - [BaseRequest](BaseRequest.md) <!-- + -->
    - [BaseRequest&lt;T&gt;](BaseRequest%601.md) <!-- + -->
    - [BaseRequest&lt;T1, T2&gt;](BaseRequest%602.md) <!-- + -->
    - [BaseRequest&lt;T1, T2, T3&gt;](BaseRequest%603.md) <!-- + -->
    - [BaseRequest&lt;T1, T2, T3, T4&gt;](BaseRequest%604.md) <!-- + -->

---

## 📝 Notes

- **Deferred execution** – Requests can be stored and processed later via `Consume()`.
- **Duplicate prevention** – Multiple identical requests can be avoided because `Consume()` only processes requests that
  are still required.
- **Required** – Indicates whether the request currently needs handling.
- **TryGet** – Method to safely inspect the request arguments.
- **Consume** – Method to process the request.

---

## 📌 Best Practices

- [Using Requests with Entities](../../BestPractices/UsingRequests.md)
- [Requests vs Actions](../../BestPractices/RequestsVsActions.md)

