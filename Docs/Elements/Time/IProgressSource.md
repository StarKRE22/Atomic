# 🧩 IProgressSource

Represents a source that <b>tracks progress (0–1) and notifies listeners</b>.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Events](#-events)
        - [OnProgressChanged](#onprogresschanged)
    - [Methods](#-methods)
        - [GetProgress()](#getprogress)
        - [SetProgress(float)](#setprogressfloat)

---

## 🗂 Example of Usage

```csharp
// Assume we have an IProgressSource instance
IProgressSource progressSource = ...;

// Subscribe to the event
progressSource.OnProgressChanged += (progress) =>
{
    Console.WriteLine($"Progress changed: {progress:P0}");
};

// Get current progress
float currentProgress = progressSource.GetProgress();

// Set new progress value (e.g., 50%)
progressSource.SetProgress(0.5f);

// Set progress to complete (100%)
progressSource.SetProgress(1.0f);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IProgressSource
```

---

### ⚡ Events

#### `OnProgressChanged`

```csharp
public event Action<float> OnProgressChanged;  
```

- **Description:** Raised when the progress changes.

---

### 🏹 Methods

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