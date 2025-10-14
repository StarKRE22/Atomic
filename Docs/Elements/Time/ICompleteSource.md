# 🧩 ICompleteSource

Represents a source that <b>can complete and notify listeners</b>.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Events](#-events)
        - [OnCompleted](#oncompleted)
    - [Methods](#-methods)
        - [IsCompleted()](#iscompleted)

---

## 🗂 Example of Usage

```csharp
// Assume we have an ICompleteSource instance
ICompleteSource completeSource = ...;

// Subscribe to the completion event
completeSource.OnCompleted += () =>
{
    Console.WriteLine("Source has completed!");
};

// Check if the source is completed
if (completeSource.IsCompleted())
{
    Console.WriteLine("Source is already completed.");
}
else
{
    Console.WriteLine("Source is still in progress.");
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface ICompleteSource
```

---

### ⚡ Events

#### `OnCompleted`

```csharp
public event Action OnCompleted;  
```

- **Description:** Invoked when the source has completed.

---

### 🏹 Methods

#### `IsCompleted()`

```csharp
public bool IsCompleted();  
```

- **Description:** Returns whether the source has completed.
    - **Returns:** `true` if completed; otherwise `false`.