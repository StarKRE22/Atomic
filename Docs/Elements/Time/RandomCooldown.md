# 🧩 RandomCooldown

Represents a **cooldown timer with a random duration**. Each time
the cooldown is reset, it is assigned a new random value between a specified minimum and maximum duration. Useful for
game mechanics where cooldowns should be unpredictable, e.g., random attack delays, randomized event timers, or
procedural ability cooldowns.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [RandomCooldown(float, float)](#randomcooldownfloat-float)
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
        - [GetProgress()](#getprogress)
        - [SetProgress(float)](#setprogressfloat)
        - [IsCompleted()](#iscompleted)
        - [Tick(float)](#tickfloat)

---

## 🗂 Example of Usage

```csharp
// Create a random cooldown between 2 and 5 seconds
ICooldown cooldown = new RandomCooldown(2f, 5f);

cooldown.OnTimeChanged += t => Console.WriteLine($"Time: {t:F2}s");
cooldown.OnProgressChanged += p => Console.WriteLine($"Progress: {p:P0}");
cooldown.OnCompleted += () => Console.WriteLine("Cooldown complete!");

// Simulate ticking
float deltaTime = 1f;
while (!cooldown.IsCompleted())
{
    cooldown.Tick(deltaTime);
    System.Threading.Thread.Sleep(1000);
}

// Reset to new random duration
cooldown.ResetTime();
Console.WriteLine($"Cooldown reset. Duration: {cooldown.GetDuration():F2}s");
```

---

## 🛠 Inspector Settings

| Parameter     | Description                           |
|---------------|---------------------------------------|
| `minDuration` | The minimum duration of the cooldown. |
| `maxDuration` | The maximum duration of the cooldown. |

---


## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public class RandomCooldown : ICooldown
```

- **Description:** Represents a **cooldown timer with a random duration**. Each time
  the cooldown is reset, it is assigned a new random value between a specified minimum and maximum duration.
- **Inheritance:** [ICooldown](ICooldown.md)
- **Note:** Supports Unity serialization and Odin Inspector

---

<div id="-constructor"></div>

### 🏗️ Constructors

#### `RandomCooldown(float, float)`

```csharp
public RandomCooldown(float minDuration, float maxDuration);
```

- **Description:** Initializes a new instance of `RandomCooldown` with a specified range of durations.
- **Parameters:**
    - `minDuration` — minimum cooldown duration in seconds.
    - `maxDuration` — maximum cooldown duration in seconds.
- **Remarks:** The initial cooldown duration is randomly chosen within the given range.

---

### ⚡ Events

#### `OnTimeChanged`

```csharp
public event Action<float> OnTimeChanged;
```

- **Description:** Invoked whenever the remaining time of the cooldown changes.
- **Parameters:** `float` — the new remaining time in seconds.
- **Notes:** Triggered whenever `SetTime`, `Tick`, or `SetProgress` changes the current time.

#### `OnDurationChanged`

```csharp
public event Action<float> OnDurationChanged;
```

- **Description:** Invoked whenever the total duration of the cooldown changes.
- **Parameters:** `float` — the new total duration in seconds.
- **Notes:** Triggered when `SetDuration` is called or when `ResetTime` sets a new random duration.

#### `OnProgressChanged`

```csharp
public event Action<float> OnProgressChanged;
```

- **Description:** Raised when the normalized progress of the cooldown changes.
- **Parameters:** `float` — current progress between 0 and 1.
- **Notes:** Progress is updated whenever time or duration changes, e.g., via `Tick`, `SetTime`, `SetDuration`, or
  `SetProgress`.

#### `OnCompleted`

```csharp
public event Action OnCompleted;
```

- **Description:** Invoked when the cooldown has finished (remaining time reaches zero).
- **Notes:** Can be used to trigger actions like enabling abilities or firing events once the cooldown expires.

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
- **Parameters:** `float time` — the new time to set, clamped between `0` and the current duration.
- **Notes:** Triggers `OnTimeChanged` and `OnProgressChanged` if the value changes.

#### `ResetTime()`

```csharp
public void ResetTime();
```

- **Description:** Resets the cooldown to a new random duration between `minDuration` and `maxDuration`.
- **Notes:** Triggers `OnDurationChanged`, `OnTimeChanged`, `OnProgressChanged`, and may trigger `OnCompleted`.

#### `GetDuration()`

```csharp
public float GetDuration();
```

- **Description:** Returns the total duration of the current cooldown.
- **Returns:** `float` — current duration in seconds.

#### `SetDuration(float)`

```csharp
public void SetDuration(float duration);
```

- **Description:** Sets a specific duration for the cooldown.
- **Parameters:** `float duration` — new duration value.
- **Notes:** Triggers `OnDurationChanged` and `OnProgressChanged`.

#### `GetProgress()`

```csharp
public float GetProgress();
```

- **Description:** Returns the normalized progress of the cooldown.
- **Returns:** `float` — progress value between `0` and `1`.

#### `SetProgress(float)`

```csharp
public void SetProgress(float progress);
```

- **Description:** Sets the normalized progress of the cooldown, updating remaining time accordingly.
- **Parameters:** `float progress` — new progress value (0–1).
- **Notes:** Triggers `OnTimeChanged` and `OnProgressChanged`.

#### `IsCompleted()`

```csharp
public bool IsCompleted();
```

- **Description:** Returns whether the cooldown has finished.
- **Returns:** `true` if remaining time is zero; otherwise `false`.

#### `Tick(float)`

```csharp
public void Tick(float deltaTime);
```

- **Description:** Advances the cooldown by a given time increment.
- **Parameters:** `float deltaTime` — time to subtract from the current remaining time.
- **Notes:** Triggers `OnTimeChanged`, `OnProgressChanged`, and `OnCompleted` if the cooldown expires.