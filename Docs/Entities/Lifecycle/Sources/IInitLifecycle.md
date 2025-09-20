# 🧩 IInitLifecycle

An interface used internally by the framework to represent a **runtime-controllable initialization / disposal contract**.  
Provides **events** for initialization and disposal state changes, as well as a method to explicitly initialize the object.

> [!NOTE]
> This is an **internal framework interface**. You should **not implement it manually** — it is used by the framework infrastructure.

---

## Overview
`IInitSource` defines a standard way for objects to be **initialized** and **disposed** during runtime.  
It exposes events for observing lifecycle transitions, a property to check initialization state, and methods to control initialization.

- Used by the framework for **lifecycle management**.
- Not intended for direct implementation in user code.

---

## IInitSource
```csharp
public interface IInitSource : IDisposable
```

---

## Members

### Events

#### OnInitialized
```csharp
event Action OnInitialized;
```
- Occurs when the object has been successfully initialized.

#### OnDisposed
```csharp
event Action OnDisposed;
```
- Occurs when the object has been disposed and its resources released.

---

### Properties

#### Initialized
```csharp
bool Initialized { get; }
```
- Gets a value indicating whether the object is currently initialized.

---

### Methods

#### Init
```csharp
void Init();
```
- Initializes the object.
- Transitions it into the **initialized state**.
- Triggers `OnInitialized`.

#### Dispose
```csharp
void Dispose();
```
- Disposes the object and releases its resources.
- Transitions it into the **disposed state**.
- Triggers `OnDisposed`.  
