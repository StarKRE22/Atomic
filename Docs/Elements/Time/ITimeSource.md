# 🧩 ITimeSource

Represents a source that tracks the <b>current time</b> and <b>notifies listeners when the time
changes</b>.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Events](#-events)
        - [OnTimeChanged](#ontimechanged)
    - [Methods](#-methods)
        - [GetTime()](#gettime)
        - [SetTime(float)](#settimefloat)
        - [ResetTime()](#resettime)

---

## 🗂 Example of Usage

```csharp
// Assume we have an ITimeSource instance
ITimeSource timeSource = ...;

// Subscribe to the event
timeSource.OnTimeChanged += (time) =>
{
    Console.WriteLine($"Time changed: {time:0.00} sec");
};

// Get current time
float currentTime = timeSource.GetTime();

// Set current time
timeSource.SetTime(1.5f);

// Reset current time to default
timeSource.ResetTime();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface ITimeSource
```

---

### ⚡ Events

#### `OnTimeChanged`

```csharp
public event Action<float> OnTimeChanged;
```

- **Description:** Raised whenever the current time changes.
- **Parameters:** `float` — the new current time in seconds.

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
- **Parameter:** `time` — The new time to set, expected to be in the range `0` to the duration of the source.

#### `ResetTime()`

```csharp
public void ResetTime();  
```

- **Description:** Resets the time source to its initial state.
- **Remarks:** After resetting, the current time will be the initial time, and any listeners may be notified via
  `OnTimeChanged`.