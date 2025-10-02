# 🧩 ITickLifecycle

```csharp
public interface ITickLifecycle
```

- **Description:** Represents a **runtime-controllable update contract** for entities or systems.  
  Provides events and methods for subscribing to or triggering **Update**, **FixedUpdate**, and **LateUpdate**
  callbacks.
- **Note:** This is an **internal framework interface**. You should **not implement it manually** — it is used by the
  framework infrastructure.

---

## ⚡ Events

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

## 🏹 Methods

#### `Tick`

```csharp
public void Tick(float deltaTime);
```

- **Description:** Called once per frame during the **Update** phase.
- **Parameter:** `deltaTime` – The time in seconds since the last frame.
- **Remarks:** Invokes `OnTicked`.

#### `FixedTick`

```csharp
public void FixedTick(float deltaTime);
```

- **Description:** Called during the **FixedUpdate** phase, typically used for physics calculations.
- **Parameter:** `deltaTime` – The fixed time step.
- **Remarks:** Invokes `OnFixedTicked`.

#### `LateTick`

```csharp
public void LateTick(float deltaTime);
```

- **Description:** Called during the **LateUpdate** phase, after all Update calls.
- **Parameter:** `deltaTime` – The time in seconds since the last frame.
- **Remarks:** Invokes `OnLateTicked`.