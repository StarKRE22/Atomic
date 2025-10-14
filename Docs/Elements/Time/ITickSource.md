# 🧩 ITickSource

Represents a source that <b>can be updated over time through the ticks</b>.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Tick(float)](#tickfloat)

---

## 🗂 Example of Usage

```csharp
// Assume we have an ITickSource instance
ITickSource tickSource = ...;

// Advance the source by 1 frame (e.g., 16ms)
tickSource.Tick(0.016f);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface ITickSource
```

---

### 🏹 Methods

#### `Tick(float)`

```csharp
public void Tick(float deltaTime);  
```

- **Description:** Updates the source by a specified time increment.
- **Parameter:** `deltaTime` — The amount of time (in seconds) to advance the source.
- **Remarks:** This method is typically called repeatedly (e.g., once per frame) to progress time-dependent systems.