# 🧩 PrintAction

```csharp
[Serializable]
public sealed class PrintAction : IAction
```

- **Description:** Represents an action that **logs a message** and **works across platforms**. This allows printing
messages consistently in both Unity and non-Unity
environments, which is especially useful when debugging or logging events in cross-platform code.
- **Inherits:** [IAction](IAction.md)
- **Remarks:** Allows serialization in Unity

> [!IMPORTANT]
> In **Unity**, it uses `Debug.Log`, `Debug.LogWarning`, or `Debug.LogError` depending on the specified `LogType`.
> <br> Outside of Unity, it uses `Console.WriteLine`.

---

## 🏗️ Constructors

#### `PrintAction(string)`

```csharp
public PrintAction(string message)
```

- **Description:** Initializes a new instance with the specified message.
- **Parameter:** `message` – The text to log.
- **Note:** Works outside of Unity.

#### `PrintAction(string, LogType)`

```csharp
public PrintAction(string message, LogType logType = LogType.Log)
```

- **Description:** Initializes a new instance with the specified message and log type.
- **Parameter:**
    - `message` – The text to log.
    - `logType` – The type of log (default is `LogType.Log`).
- **Note:** Works in Unity.

---

## 🏹 Methods

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

---

## 🗂 Example of Usage

```csharp
#if UNITY_5_3_OR_NEWER

IAction printAction = new PrintAction("Warning message!", LogType.Warning);
printAction.Invoke(); // Logs a warning in Unity

#else

IAction printAction = new PrintAction("Hello World!");
printAction.Invoke(); // Logs message to standard console

#endif
```