# 🧩 ThreadSafeCooldown

A **thread-safe cooldown timer** whose state changes are dispatched on the main thread. It tracks remaining time,
provides normalized progress, and raises events when its state changes. Useful for ability cooldowns, weapon reloads,
or any timed delays that may be updated from background threads.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [ThreadSafeCooldown(float)](#threadsafecooldownfloat)
        - [ThreadSafeCooldown(float, float)](#threadsafecooldownfloat-float)
    - [Events](#-events)
        - [OnTimeChanged](#ontimechanged)
        - [OnDurationChanged](#ondurationchanged)
        - [OnProgressChanged](#onprogresschanged)
        - [OnCompleted](#oncompleted)
    - [Properties](#-properties)
        - [Duration](#duration)
        - [CurrentTime](#currenttime)
        - [Progress](#progress)
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
        - [IsPlaying()](#isplaying)
        - [ToString()](#tostring)
- [Thread Safety](#-thread-safety)

---

## 🗂 Example of Usage

```csharp
// Create a thread-safe cooldown of 5 seconds
ThreadSafeCooldown cooldown = new ThreadSafeCooldown(5f);

// Subscribe to main-thread events
cooldown.OnTimeChanged += time =>
    Debug.Log($"Time remaining: {time:F2}s");

cooldown.OnCompleted += () =>
    Debug.Log("Cooldown complete!");

// Update from a background thread is safe
Task.Run(() => cooldown.Tick(1f));

// Flush events on the main thread
MainThreadDispatcher.Flush();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public sealed class ThreadSafeCooldown : ICooldown, MainThreadDispatcher.IFlushable
```

- **Description:** Thread-safe cooldown implementation with main-thread event flushing.
- **Inheritance:** [ICooldown](ICooldown.md), `MainThreadDispatcher.IFlushable`
- **Notes:** All reads and writes are protected by a `lock`. Events are raised during `MainThreadDispatcher.Flush()`.
- **See also:** [Cooldown](Cooldown.md), [ICooldown](ICooldown.md)

---

### 🏗️ Constructors

#### `ThreadSafeCooldown(float)`

```csharp
public ThreadSafeCooldown(float duration)
```

- **Description:** Initializes a new cooldown with the specified duration.
- **Parameter:** `duration` — The cooldown duration in seconds.
- **Remarks:** The remaining time is initialized to the full duration.

#### `ThreadSafeCooldown(float, float)`

```csharp
public ThreadSafeCooldown(float duration, float current)
```

- **Description:** Initializes a new cooldown with the specified duration and current remaining time.
- **Parameters:**
    - `duration` — The cooldown duration in seconds.
    - `current` — The initial remaining time, clamped to `[0, duration]`.

---

### ⚡ Events

#### `OnTimeChanged`

```csharp
public event Action<float> OnTimeChanged;
```

- **Description:** Invoked on the main thread whenever the current remaining time changes.

#### `OnDurationChanged`

```csharp
public event Action<float> OnDurationChanged;
```

- **Description:** Invoked on the main thread whenever the total duration changes.

#### `OnProgressChanged`

```csharp
public event Action<float> OnProgressChanged;
```

- **Description:** Invoked on the main thread whenever the normalized progress changes.

#### `OnCompleted`

```csharp
public event Action OnCompleted;
```

- **Description:** Invoked on the main thread when the cooldown reaches zero.

---

### 🔑 Properties

#### `Duration`

```csharp
public float Duration { get; set; }
```

- **Description:** Gets or sets the total cooldown duration in a thread-safe manner.
- **Access:** Read-write.
- **Thread Safety:** Both getter and setter are protected by a `lock`.

#### `CurrentTime`

```csharp
public float CurrentTime { get; set; }
```

- **Description:** Gets or sets the remaining cooldown time in a thread-safe manner.
- **Access:** Read-write.
- **Thread Safety:** Both getter and setter are protected by a `lock`.

#### `Progress`

```csharp
public float Progress { get; set; }
```

- **Description:** Gets or sets the normalized progress in the range `[0, 1]`.
- **Access:** Read-write.
- **Thread Safety:** Both getter and setter are protected by a `lock`.

---

### 🏹 Methods

#### `GetTime()`

```csharp
public float GetTime();
```

- **Description:** Returns the current remaining time of the cooldown.

#### `SetTime(float)`

```csharp
public void SetTime(float time);
```

- **Description:** Sets the current remaining time.
- **Parameter:** `time` — New time to set. Negative values throw `ArgumentException`.
- **Remarks:** Values are clamped between `0` and the total duration.

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

#### `SetDuration(float)`

```csharp
public void SetDuration(float duration);
```

- **Description:** Sets a new total duration.
- **Parameter:** `duration` — New duration value.

#### `Tick(float)`

```csharp
public void Tick(float deltaTime);
```

- **Description:** Advances the cooldown by a given time increment.
- **Parameter:** `deltaTime` — Time to subtract from the current remaining time.
- **Thread Safety:** Safe to call from any thread.

#### `GetProgress()`

```csharp
public float GetProgress();
```

- **Description:** Returns the normalized progress of the cooldown.

#### `SetProgress(float)`

```csharp
public void SetProgress(float progress);
```

- **Description:** Sets the normalized progress, updating remaining time accordingly.
- **Parameter:** `progress` — New progress value between `0` and `1`.

#### `IsCompleted()`

```csharp
public bool IsCompleted();
```

- **Description:** Returns whether the cooldown has finished.
- **Returns:** `true` if remaining time is zero; otherwise `false`.

#### `IsPlaying()`

```csharp
public bool IsPlaying();
```

- **Description:** Returns whether the cooldown is currently active.
- **Returns:** `true` if remaining time is greater than zero; otherwise `false`.

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string representation of the cooldown's state.
- **Returns:** Formatted string showing `duration` and `remaining time`.

---

## 🔒 Thread Safety

- All public members are protected by a private `lock`, making the class safe for concurrent access.
- Event handlers are captured under the lock, but invoked on the main thread during `MainThreadDispatcher.Flush()`.
- Remember to call `MainThreadDispatcher.Flush()` each frame on the main thread to deliver queued events.
- For single-threaded scenarios, consider using [Cooldown](Cooldown.md) instead.
