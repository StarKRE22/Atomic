# 🧩 Cooldown

Concrete implementation of the **cooldown** that tracks the remaining time, provides normalized
progress, and raises events when its state changes. Useful for game mechanics such as ability cooldowns, weapon reloads,
or timed delays.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [Cooldown()](#cooldown)
        - [Cooldown(float)](#cooldownfloat)
    - [Events](#-events)
        - [OnTimeChanged](#ontimechanged)
        - [OnDurationChanged](#ondurationchanged)
        - [OnProgressChanged](#onprogresschanged)
        - [OnCompleted](#oncompleted)
    - [Methods](#-methods)
        - [GetTime()](#gettime)
        - [SetTime(float)](#settimefloat)
        - [ResetTime()](#resettime)
        - [GetDuration()](#getduration)
        - [SetDuration(float)](#setdurationfloat)
        - [Tick(float)](#tickfloat)
        - [GetProgress()](#getprogress)
        - [SetProgress(float)](#setprogressfloat)
        - [IsCompleted()](#iscompleted)
        - [ToString()](#tostring)
    - [Operators](#-operators)
        - [Cooldown(float)](#operator-cooldownfloat)
        - [Cooldown(int)](#operator-cooldownint)

---

## 🗂 Example of Usage

```csharp
// Create a cooldown of 5 seconds
Cooldown cooldown = 5f;

// Subscribe to events
cooldown.OnTimeChanged += time => 
    Console.WriteLine($"Time remaining: {time:F2}s");

cooldown.OnProgressChanged += progress => 
    Console.WriteLine($"Progress: {progress:P0}");

cooldown.OnCompleted += () => 
    Console.WriteLine("Cooldown complete!");

// Simulate a game loop updating the cooldown
float deltaTime = 1f; // 1 second per tick
while (!cooldown.IsCompleted())
{
    cooldown.Tick(deltaTime);
    System.Threading.Thread.Sleep(1000); // wait 1 second
}

// Reset the cooldown to full duration
cooldown.ResetTime();
Console.WriteLine($"Cooldown reset. Time remaining: {cooldown.GetTime()}s");

// Set progress to 50%
cooldown.SetProgress(0.5f);
Console.WriteLine($"Cooldown progress set to 50%, time remaining: {cooldown.GetTime()}s");
```

---

## 🛠 Inspector Settings

| Parameter     | Description                            |
|---------------|----------------------------------------|
| `duration`    | Total duration of the cooldown         |
| `currentTime` | Current remaining time of the cooldown |

---


## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public class Cooldown : ICooldown
```

- **Description:** Concrete implementation of the **cooldown** that tracks the remaining time, provides normalized
  progress, and raises events when its state changes.
- **Inheritance:** [ICooldown](ICooldown.md)
- **Notes:** Supports Unity serialization and Odin Inspector

---

<div id="-constructor"></div>

### 🏗️ Constructors

#### `Cooldown()`

```csharp
public Cooldown();
```

- **Description:** Initializes a new instance of the `Cooldown` class with default values.
- **Remarks:** Duration defaults to `0` and remaining time is `0`. The cooldown must be set or reset before use.

#### `Cooldown(float)`

```csharp
public Cooldown(float duration);
```

- **Description:** Initializes a new instance of the `Cooldown` class with a specified duration.
- **Parameter:** `duration` — total duration of the cooldown in seconds.
- **Remarks:** The remaining time is initialized to the full duration.

---

### ⚡ Events

#### `OnTimeChanged`

```csharp
public event Action<float> OnTimeChanged;
```

- **Description:** Invoked whenever the current remaining time changes.
- **Parameters:** `float` — the new remaining time in seconds.

#### `OnDurationChanged`

```csharp
public event Action<float> OnDurationChanged;
```

- **Description:** Invoked whenever the total duration changes.
- **Parameters:** `float` — the new total duration in seconds.

#### `OnProgressChanged`

```csharp
public event Action<float> OnProgressChanged;
```

- **Description:** Raised when the normalized progress changes.
- **Parameters:** `float` — the current progress (0–1).

#### `OnCompleted`

```csharp
public event Action OnCompleted;
```

- **Description:** Invoked when the cooldown has finished (remaining time reaches zero).

---

### 🏹 Methods

#### `GetTime()`

```csharp
public float GetTime();
```

- **Description:** Returns the current remaining time of the cooldown.
- **Returns:** `float` — remaining time in seconds.

#### `SetTime(float)`

```csharp
public void SetTime(float time);
```

- **Description:** Sets the current remaining time.
- **Parameters:** `time` — new time to set, clamped between `0` and the total duration.
- **Notes:** Invokes `OnTimeChanged` and `OnProgressChanged` if the value changes.

#### `ResetTime()`

```csharp
public void ResetTime();
```

- **Description:** Resets the cooldown to its full duration.

#### `GetDuration()`

```csharp
public float GetDuration();
```

- **Description:** Returns the total duration of the cooldown.
- **Returns:** `float` — total duration in seconds.

#### `SetDuration(float)`

```csharp
public void SetDuration(float duration);
```

- **Description:** Sets a new total duration.
- **Parameters:** `duration` — new duration value.
- **Notes:** Invokes `OnDurationChanged` and `OnProgressChanged` if the value changes.

#### `Tick(float)`

```csharp
public void Tick(float deltaTime);
```

- **Description:** Advances the cooldown by a given time increment, reducing remaining time.
- **Parameters:** `deltaTime` — time to subtract from the current remaining time.
- **Notes:** Invokes `OnTimeChanged`, `OnProgressChanged`, and `OnCompleted` if the cooldown expires.

#### `GetProgress()`

```csharp
public float GetProgress();
```

- **Description:** Returns the normalized progress of the cooldown.
- **Returns:** `float` — progress between 0 and 1.

#### `SetProgress(float)`

```csharp
public void SetProgress(float progress);
```

- **Description:** Sets the normalized progress (0–1), updating remaining time accordingly.
- **Parameters:** `progress` — new progress value between 0 and 1.
- **Notes:** Invokes `OnTimeChanged` and `OnProgressChanged`.

#### `IsCompleted()`

```csharp
public bool IsCompleted();
```

- **Description:** Returns whether the cooldown has finished.
- **Returns:** `true` if remaining time is zero; otherwise `false`.

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string representation of the cooldown's state.
- **Returns:** Formatted string showing `duration` and `remaining time`.

---

### 🪄 Operators

#### `operator Cooldown(float)`

```csharp
public static implicit operator Cooldown(float duration) => new(duration);
```

- **Description:** Allows implicit conversion from a `float` to a `Cooldown`.
- **Parameter:** `duration` — The duration value in seconds.
- **Returns:** A new instance of `Cooldown` initialized with the given `duration`.

#### `operator Cooldown(int)`

```csharp
public static implicit operator Cooldown(int duration) => new(duration);
```

- **Description:** Allows implicit conversion from a `int` to a `Cooldown`.
- **Parameter:** `duration` — The duration value in seconds.
- **Returns:** A new instance of `Cooldown` initialized with the given `duration`.