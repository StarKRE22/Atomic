# 🧩️ ICooldown

Represents a contract of **cooldown timer** that tracks remaining time, provides progress feedback and raises events
when its state changes. It is useful for game mechanics such as ability cooldowns, weapon reloads, and timed delays.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
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

---

## 🗂 Example of Usage

```csharp
// Assume we have a ICooldown instance
ICooldown cooldown = ...;

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

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface ICooldown : ITimeSource, IDurationSource, ITickSource, IProgressSource, ICompleteSource;
```

- **Description:** Represents a contract of **cooldown timer** that tracks remaining time, provides progress feedback
  and raises events
  when its state changes.
- **Inheritance:**
  [ITimeSource](ITimeSource.md), [IDurationSource](IDurationSource.md), [ITickSource](ITickSource.md), [IProgressSource](IProgressSource.md), [ICompleteSource](ICompleteSource.md)

---

### ⚡ Events

#### `OnTimeChanged`

```csharp
public event Action<float> OnTimeChanged;
```

- **Description:** Raised whenever the current time changes.
- **Parameters:** `float` — the new current time in seconds.

#### `OnDurationChanged`

```csharp
public event Action<float> OnDurationChanged;
```

- **Description:** Invoked when the duration value changes.

#### `OnProgressChanged`

```csharp
public event Action<float> OnProgressChanged;  
```

- **Description:** Raised when the progress changes.

#### `OnCompleted`

```csharp
public event Action OnCompleted;  
```

- **Description:** Invoked when the source has completed.

---

### 🏹 Methods

#### `GetTime()`

```csharp
public float GetTime();
```

- **Description:** Gets the current time from the source.
- **Returns:** `float` — current time in seconds.

#### `SetTime(float)`

```csharp
public void SetTime(float time);
```

- **Description:** Sets the current time.
- **Parameters:**
    - `time` — The new time to set, expected to be in the range `0` to the duration of the source.

#### `ResetTime()`

```csharp
public void ResetTime();  
```

- **Description:** Resets the time source to its initial state.
- **Remarks:** After resetting, the current time will be the initial time, and any listeners may be notified via
  `OnTimeChanged`.

#### `GetDuration()`

```csharp
public float GetDuration();  
```

- **Description:** Gets the total duration.
- **Returns:** The duration in seconds.

#### `SetDuration(float)`

```csharp
public void SetDuration(float duration);  
```

- **Description:** Sets the total duration.
- **Parameter:** `duration` — The new duration value in seconds.

#### `Tick(float)`

```csharp
public void Tick(float deltaTime);  
```

- **Description:** Updates the source by a specified time increment.
- **Parameter:** `deltaTime` — The amount of time (in seconds) to advance the source.
- **Remarks:** This method is typically called repeatedly (e.g., once per frame) to progress time-dependent systems.

#### `GetProgress()`

```csharp
public float GetProgress();  
```

- **Description:** Gets the current progress.
- **Returns:** Normalized progress (0–1).

#### `SetProgress(float)`

```csharp
public void SetProgress(float progress);  
```

- **Description:** Sets the current progress.
- **Parameter:** `progress` — Progress value (0–1).

#### `IsCompleted()`

```csharp
public bool IsCompleted();  
```

- **Description:** Returns whether the source has completed.
- **Returns:** `true` if completed; otherwise `false`.