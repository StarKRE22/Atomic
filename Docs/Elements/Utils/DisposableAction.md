# 🧩 DisposableAction

Represents a lightweight **disposable** implementation that invokes a specified action when disposed.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
    - [Dispose resources](#ex1)
    - [Event Unsubscription](#ex2)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [DisposableAction(Action)](#disposableactionaction)
    - [Methods](#-methods)
        - [Dispose()](#dispose)

---

## 🗂 Example of Usage

<div id="ex1"></div>

### 1️⃣ Dispose resources

```csharp
// Assume we load a heavy prefab
GameObject heavyPrefab = Resources.Load<GameObject>(somePath);

// Create an action that unloads this prefab
var disposable = new DisposableAction(() => 
{
    Resources.UnloadAsset(heavyPrefab);
});

// Later, unload the prefab
disposable.Dispose();
```

<div id="ex2"></div>

### 2️⃣ Event Unsubscription

```csharp
// Assume we have a some observable
ISignal signal = ...
    
// Assume we have a some event handler
Action handler = () => Console.WriteLine("Event fired.");

// Subscribe on event
signal.OnEvent += handler;

// Create an action that unsubscribes this handler
var disposable = new DisposableAction(() => 
{
    signal.OnEvent -= handler;
});

// Later, unsubscribe the handler
disposable.Dispose();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public readonly struct DisposableAction : IDisposable
```

- **Description:** Represents a lightweight **disposable** implementation that invokes a specified action when disposed.
- **Inheritance:** `IDisposable`
- **Note:** Ideal for inline or ad-hoc cleanup logic.

---

<div id="-constructors"></div>

### 🏗️ Constructors

#### `DisposableAction(Action)`

```csharp
public DisposableAction(Action action);
```

- **Description:** Initializes a new instance of `DisposableAction` with the specified action.
- **Parameter:** `action` — The action to invoke when `Dispose` is called.
- **Exceptions:** Throws `ArgumentNullException` if `action` is `null`.
- **Remarks:** Use this to create disposable behavior without defining a separate class.

---

### 🏹 Methods

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Invokes the action provided at construction.
- **Remarks:** Once called, the action executes. Useful for automatic cleanup in `using` blocks or when chaining
  disposables.