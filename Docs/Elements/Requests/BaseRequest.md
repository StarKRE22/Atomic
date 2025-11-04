# 🧩 Request

Represents a <b>parameterless</b> request action.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Properties](#-properties)
        - [Required](#required)
    - [Methods](#-methods)
        - [Invoke()](#invoke)
        - [Consume()](#consume)

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
public class Request : IRequest
```

- **Description:** Represents a <b>parameterless</b> request action.
- **Inheritance:** [IRequest](IRequest.md)
- **Note:** Supports Unity serialization and Odin Inspector

---

### 🔑 Properties

#### `Required`

```csharp
public bool Required { get; }
```

- **Description:** Indicates whether the request must be handled.

---

### 🏹 Methods

#### `Invoke()`

```csharp
public void Invoke();
```

- **Description:** Executes the request.

#### `Consume()`

```csharp
public bool Consume();
```

- **Description:** Attempts to consume the request.
- **Returns:** `true` if successfully consumed; otherwise `false`.