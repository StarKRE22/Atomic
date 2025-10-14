# 🧩 IStartSource

Represents a source that <b>can be started, stopped, and notify start/stop events</b>.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Events](#-events)
        - [OnStarted](#onstarted)
        - [OnStopped](#onstopped)
    - [Methods](#-methods)
        - [IsStarted()](#isstarted)
        - [Start(float)](#startfloat)
        - [Start()](#start)
        - [Stop()](#stop)

---

## 🗂 Example of Usage

```csharp
// Assume we have an IStartSource instance
IStartSource startSource = ...;

// Subscribe to events
startSource.OnStarted += () =>
{
    Console.WriteLine("Source started!");
};

startSource.OnStopped += () =>
{
    Console.WriteLine("Source stopped!");
};

// Start the source from the default time
startSource.Start();

// Start the source from a specific time (e.g., 5 seconds)
startSource.Start(5.0f);

// Check if the source is running
if (startSource.IsStarted())
{
    Console.WriteLine("Source is currently running.");
}

// Stop the source
startSource.Stop();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IStartSource
```

- **Description:** Represents a source that <b>can be started, stopped, and notify start/stop events</b>.

---

### ⚡ Events

#### `OnStarted`

```csharp
public event Action OnStarted;  
```

- **Description:** Raised when the source starts.

#### `OnStopped`

```csharp
public event Action OnStopped;  
```

- **Description:** Raised when the source stops.

---

### 🏹 Methods

#### `IsStarted()`

```csharp
public bool IsStarted();  
```

- **Description:** Returns `true` if the source is running.

#### `Start(float)`

```csharp
public void Start(float time);  
```

- **Description:** Starts the source from a specific time.
- **Parameter:** `time` — Time (in seconds) to start from.

#### `Start()`

```csharp
public void Start();  
```

- **Description:** Starts the source from the default start time.

#### `Stop()`

```csharp
public void Stop();  
```

- **Description:** Stops the source and resets its time.