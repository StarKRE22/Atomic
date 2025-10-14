# 🧩 IDurationSource

Represents a source that <b>has a total duration and can notify changes</b>.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Events](#-events)
        - [OnDurationChanged](#ondurationchanged)
    - [Methods](#-methods)
        - [GetDuration()](#getduration)
        - [SetDuration(float)](#setdurationfloat)

---

## 🗂 Example of Usage

```csharp
// Assume we have an IDurationSource instance
IDurationSource durationSource = ...;

// Subscribe to the event
durationSource.OnDurationChanged += (duration) =>
{
    Console.WriteLine($"Duration changed: {duration:0.00} sec");
};

// Get current duration
float currentDuration = durationSource.GetDuration();

// Set a new duration
durationSource.SetDuration(10.0f);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IDurationSource
```

---

### ⚡ Events

#### `OnDurationChanged`

```csharp
public event Action<float> OnDurationChanged;
```

- **Description:** Invoked when the duration value changes.

---

### 🏹 Methods

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