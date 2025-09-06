# 🧩 IEnableSource

An interface used internally by the framework to represent a **runtime-controllable enable / disable contract**.  
Provides **events** for enable / disable state changes and methods to toggle state programmatically.

> [!NOTE]
> This is an **internal framework interface**. You should **not implement it manually** — it is used by the framework infrastructure.

---

## Overview
`IEnableSource` defines a standard way for objects to be **enabled** and **disabled** during runtime.  
It exposes events for observing state changes, a property to check the state, and methods to control it.
- Used by the framework for **lifecycle management**.
- Not intended for direct implementation in user code.

---

## IEnableSource
```csharp
public interface IEnableSource
```

---

## Members

### Events

#### OnEnabled
```csharp
event Action OnEnabled;
```
- Occurs when the object is enabled.

#### OnDisabled
```csharp
event Action OnDisabled;
```
- Occurs when the object is disabled.

---

### Properties

#### Enabled
```csharp
bool Enabled { get; }
```
- Gets a value indicating whether the object is currently enabled.

---

### Methods

#### Enable
```csharp
void Enable();
```
- Enables the object.
- Triggers `OnEnabled`.

#### Disable
```csharp
void Disable();
```
- Disables the object.
- Triggers `OnDisabled`.