# 🧩 IRequest

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

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IRequest : IAction
```

- **Description:** Represents a <b>parameterless</b> request action.
- **Inheritance:** [IAction](../Actions/IAction.md)

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
- **Note:** This method derived from `IAction`

#### `Consume()`

```csharp
public bool Consume();
```

- **Description:** Attempts to consume the request.
- **Returns:** `true` if successfully consumed; otherwise `false`.