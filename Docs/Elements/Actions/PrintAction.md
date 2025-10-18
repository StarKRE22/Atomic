# 🧩 LogAction

Represents an action that **logs a message** and **works across platforms**. This allows printing
messages consistently in both Unity and non-Unity
environments, which is especially useful when debugging or logging events in cross-platform code.

> [!IMPORTANT]
> In **Unity**, it uses `Debug.Log`, `Debug.LogWarning`, or `Debug.LogError` depending on the specified `LogType`.
> <br> Outside of Unity, it uses `Console.WriteLine`.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [LogAction()](#logaction)
        - [LogAction(string)](#logactionstring)
        - [LogAction(string, LogType)](#logactionstring-logtype)
    - [Methods](#-methods)
        - [Invoke()](#invoke)
        - [ToString()](#tostring)

---

## 🗂 Example of Usage

Below is an example of using `LogAction` in a code:

```csharp
#if UNITY_5_3_OR_NEWER

IAction logAction = new LogAction("Warning message!", LogType.Warning);
logAction.Invoke(); // Logs a warning in Unity

#else

IAction logAction = new LogAction("Hello World!");
logAction.Invoke(); // Logs message to standard console

#endif
```

---

## 🛠 Inspector Settings

| Parameter | Description                                      |
|-----------|--------------------------------------------------|
| `logType` | The type of the log message in Debug.unityLogger |
| `message` | The text to log                                  |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public sealed class LogAction : IAction
```

- **Inherits:** [IAction](IAction.md)
- **Remarks:** Allows serialization in Unity and supports Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `LogAction()`

```csharp
public LogAction()
```

- **Description:** Initializes a new instance of the <see cref="LogAction"/> class.
- **Note:** This constructor is intended **only for use by the Unity Inspector** when using `[SerializeReference]`.
  It allows the inspector to create and serialize a default instance of <see cref="LogAction"/>.

#### `LogAction(string)`

```csharp
public LogAction(string message)
```

- **Description:** Initializes a new instance with the specified message.
- **Parameter:** `message` – The text to log.
- **Note:** Works outside of Unity.

#### `LogAction(string, LogType)`

```csharp
public LogAction(string message, LogType logType = LogType.Log)
```

- **Description:** Initializes a new instance with the specified message and log type.
- **Parameter:**
    - `message` – The text to log.
    - `logType` – The type of log (default is `LogType.Log`).
- **Note:** Works in Unity.

---

### 🏹 Methods

#### `Invoke()`

```csharp
public void Invoke()
```

- **Description:** Logs the configured message to the console.
    - In Unity, uses the specified `LogType`.
    - Outside Unity, prints to standard console.

#### `ToString()`

```csharp
public string ToString();
```

- **Description:** Returns a message representation.
- **Remarks:** The output depends on the Unity version:
    - **If compiled with `Unity`** then returns a string in the format: `{LogType}: {Message}`
    - **Otherwise**, Returns only the message.