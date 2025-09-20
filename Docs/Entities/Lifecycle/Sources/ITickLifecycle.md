# 🧩 IUpdateSource

An interface used internally by the framework to represent a **runtime-controllable update contract**.  
Supports **regular**, **fixed**, and **late** update callbacks during the loop.

> [!NOTE]  
> This is an **internal framework interface**. You should **not implement it manually** — it is used by the framework infrastructure.

---

## Overview
`IUpdateSource` defines a standard way for objects to participate in the **update cycle** of the framework.  
It provides events for external observation and callback methods invoked by the loop system.
- Used by the framework for **frame updates**.
- Not intended for direct implementation in user code.

---

## IUpdateSource
```csharp
public interface IUpdateSource
```

---

## Members

### Events

#### OnUpdated
```csharp
event Action<float> OnUpdated;
```
- Occurs during the **regular Update phase**, once per frame.
- Receives `deltaTime` — time in seconds since the last frame.

#### OnFixedUpdated
```csharp
event Action<float> OnFixedUpdated;
```
- Occurs during the **FixedUpdate phase**, typically for physics updates.
- Receives `deltaTime` — fixed time step used by the physics engine.

#### OnLateUpdated
```csharp
event Action<float> OnLateUpdated;
```
- Occurs during the **LateUpdate phase**, after all Update calls.
- Receives `deltaTime` — time in seconds since the last frame.

---

### Methods

#### OnUpdate
```csharp
void OnUpdate(float deltaTime);
```
- Called once per frame during the **Update phase**.
- `deltaTime` — time in seconds since the last frame.

#### OnFixedUpdate
```csharp
void OnFixedUpdate(float deltaTime);
```
- Called during the **FixedUpdate phase**, used for physics calculations.
- `deltaTime` — fixed time step from the physics engine.

#### OnLateUpdate
```csharp
void OnLateUpdate(float deltaTime);
```
- Called during the **LateUpdate phase**, after all `OnUpdate` calls.
- `deltaTime` — time in seconds since the last frame.
