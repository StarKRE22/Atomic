# 🧩 ITickLifecycle

Represents a **runtime-controllable update contract** for entities or systems.  
Provides events and methods for subscribing to or triggering **Update**, **FixedUpdate**, and **LateUpdate**
callbacks.

> [!NOTE]
> This is a **core framework interface**. You should **not implement it manually** — it is used by the
> framework infrastructure.

---

## 📑 Table of Contents

<ul>
  <li><a href="#-example-of-usage">Example of Usage</a></li>
  <li>
    <a href="#-api-reference">API Reference</a>
    <ul>
      <li><a href="#-type">Type</a></li>
      <li>
        <a href="#-events">Events</a>
        <ul>
          <li><a href="#onticked">OnTicked</a></li>
          <li><a href="#onfixedticked">OnFixedTicked</a></li>
          <li><a href="#onlateticked">OnLateTicked</a></li>
        </ul>
      </li>
      <li>
        <a href="#-methods">Methods</a>
        <ul>
          <li><a href="#tick">Tick(float deltaTime)</a></li>
          <li><a href="#fixedtick">FixedTick(float deltaTime)</a></li>
          <li><a href="#latetick">LateTick(float deltaTime)</a></li>
        </ul>
      </li>
    </ul>
  </li>
</ul>


---

## 🗂 Example of Usage

```csharp
// Assume we have an instance of ITickLifecycle
ITickLifecycle tickSource = ...;

// Subscribe to events
tickSource.OnTicked += deltaTime => Console.WriteLine($"Update tick: {deltaTime:F3}s");
tickSource.OnFixedTicked += deltaTime => Console.WriteLine($"FixedUpdate tick: {deltaTime:F3}s");
tickSource.OnLateTicked += deltaTime => Console.WriteLine($"LateUpdate tick: {deltaTime:F3}s");

// Simulate update cycles
float deltaTime = 0.016f; // ~60 FPS

// Regular Update
tickSource.Tick(deltaTime);

// FixedUpdate
tickSource.FixedTick(0.02f);

// LateUpdate
tickSource.LateTick(deltaTime);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface ITickLifecycle
```

---

### ⚡ Events

#### `OnTicked`

```csharp
public event Action<float> OnTicked;
```

- **Description:** Triggered during the regular **Update** phase, once per frame.
- **Parameter:** `deltaTime` – The time in seconds since the last frame.

#### `OnFixedTicked`

```csharp
public event Action<float> OnFixedTicked;
```

- **Description:** Triggered during the **FixedUpdate** phase, used for physics updates.
- **Parameter:** `deltaTime` – The fixed time step used by the physics engine.

#### `OnLateTicked`

```csharp
public event Action<float> OnLateTicked;
```

- **Description:** Triggered during the **LateUpdate** phase, after all Update calls have been executed.
- **Parameter:** `deltaTime` – The time in seconds since the last frame.

---

### 🏹 Methods

#### `Tick()`

```csharp
public void Tick(float deltaTime);
```

- **Description:** Called once per frame during the **Update** phase.
- **Parameter:** `deltaTime` – The time in seconds since the last frame.
- **Remarks:** Invokes `OnTicked`.

#### `FixedTick()`

```csharp
public void FixedTick(float deltaTime);
```

- **Description:** Called during the **FixedUpdate** phase, typically used for physics calculations.
- **Parameter:** `deltaTime` – The fixed time step.
- **Remarks:** Invokes `OnFixedTicked`.

#### `LateTick()`

```csharp
public void LateTick(float deltaTime);
```

- **Description:** Called during the **LateUpdate** phase, after all Update calls.
- **Parameter:** `deltaTime` – The time in seconds since the last frame.
- **Remarks:** Invokes `OnLateTicked`.