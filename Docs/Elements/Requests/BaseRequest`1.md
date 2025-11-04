# 🧩 Request&lt;T&gt;

Represents a request action with <b>one input argument</b>.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Properties](#-properties)
        - [Required](#required)
        - [Arg](#arg)
    - [Methods](#-methods)
        - [Invoke(T)](#invoket)
        - [Consume(out T)](#consumeout-t)
        - [TryGet(out T)](#trygetout-t)
---


## 🗂 Example of Usage

```csharp
var damageRequest = new Request<Character>();

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

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public class Request<T> : IRequest<T>
```

- **Description:** Represents a request action with <b>one input argument</b>.
- **Inheritance:** [IRequest&lt;T&gt;](IRequest%601.md)
- **Type parameter:** `T` — type of the argument.
- **Note:** Supports Unity serialization and Odin Inspector

---

### 🔑 Properties

#### `Required`

```csharp
public bool Required { get; }
```

- **Description:** Indicates whether the request is currently required.

#### `Arg`

```csharp
public T Arg { get; }
```

- **Description:** The stored argument.

---

### 🏹 Methods

#### `Invoke(T)`

```csharp
public void Invoke(T arg);
```

- **Description:** Marks the request as required and stores the argument.
- **Parameter:** `arg` — the input argument.

#### `Consume(out T)`

```csharp
public bool Consume(out T arg);
```

- **Description:** Attempts to consume the request and retrieve the argument.
- **Output:** `arg` — the argument if successfully consumed.
- **Returns:** `true` if the request was required and is now consumed.

#### `TryGet(out T)`

```csharp
public bool TryGet(out T arg);
```

- **Description:** Attempts to retrieve the argument without consuming the request.
- **Output:** `arg` — the stored argument.
- **Returns:** `true` if the request is currently required.