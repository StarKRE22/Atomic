# 🧩 IEnableLifecycle

```csharp
//Assume we have an instance of IEnableLifecycle
IEnableLifecycle enableSource = ...;

// Subscribe to events
enableSource.OnEnabled += () => Console.WriteLine("Player enabled");
enableSource.OnDisabled += () => Console.WriteLine("Player disabled");

// Check initial state
Console.WriteLine($"Enabled: {enableSource.Enabled}");

// Enable the object
enableSource.Enable();
Console.WriteLine($"Enabled: {enableSource.Enabled}");

// Disable the object
enableSource.Disable();
Console.WriteLine($"Enabled: {enableSource.Enabled}");

// Try disabling again (no event will fire since it's already disabled)
enableSource.Disable();
```

---


```csharp
public interface IEnableLifecycle
```

- **Description:** Represents a **runtime-controllable enable and disable contract** for entities or systems. Provides
  events for enable and disable state changes, as well as methods to programmatically toggle the state.
- Note: This is an **internal framework interface**. You should **not implement it manually** — it is used by the
  framework infrastructure.

---

## ⚡ Events

#### `OnEnabled`

```csharp
public event Action OnEnabled;
```

- **Description:** Triggered when the object has been enabled.

#### `OnDisabled`

```csharp
public event Action OnDisabled;
```

- **Description:** Triggered when the object has been disabled.

---

## 🔑 Properties

#### `Enabled`

```csharp
public bool Enabled { get; }
```

- **Description:** Indicates whether the object is currently enabled.

---

## 🏹 Methods

#### `Enable`

```csharp
public void Enable();
```

- **Description:** Enables the object, transitioning it into the **enabled state**.
- **Remarks:** Triggers `OnEnabled`.

#### `Disable`

```csharp
public void Disable();
```

- **Description:** Disables the object, transitioning it into the **disabled state**.
- **Remarks:** Triggers `OnDisabled`.