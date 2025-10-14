# 🧩 IPauseSource

Represents a source that <b>can be paused and resumed</b>.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Events](#-events)
        - [OnPaused](#onpaused)
        - [OnResumed](#onresumed)
    - [Methods](#-methods)
        - [IsPaused()](#ispaused)
        - [Pause()](#pause)
        - [Resume()](#resume)

---

## 🗂 Example of Usage

```csharp
// Assume we have an IPauseSource instance
IPauseSource pauseSource = ...;

// Subscribe to events
pauseSource.OnPaused += () =>
{
    Console.WriteLine("Source paused!");
};

pauseSource.OnResumed += () =>
{
    Console.WriteLine("Source resumed!");
};

// Pause the source
pauseSource.Pause();

// Check if the source is paused
if (pauseSource.IsPaused())
{
    Console.WriteLine("Source is currently paused.");
}

// Resume the source
pauseSource.Resume();

// Verify it's no longer paused
if (!pauseSource.IsPaused())
{
    Console.WriteLine("Source is running again.");
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IPauseSource
```

---

### ⚡ Events

#### `OnPaused`

```csharp
public event Action OnPaused;  
```

- **Description:** Raised when the source is paused.

#### `OnResumed`

```csharp
public event Action OnResumed;  
```

- **Description:** Raised when the source is resumed.

---

### 🏹 Methods

#### `IsPaused()`

```csharp
public bool IsPaused();  
```

- **Description:** Returns true if the source is paused.
- **Returns:** `true` if paused; otherwise `false`.

#### `Pause()`

```csharp
public void Pause();  
```

- **Description:** Pauses the source.

#### `Resume()`

```csharp
public void Resume();  
```

- **Description:** Resumes the source.