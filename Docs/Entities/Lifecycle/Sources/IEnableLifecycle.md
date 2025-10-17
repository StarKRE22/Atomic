# 🧩 IEnableLifecycle

Represents a **runtime-controllable enable and disable contract** for entities or systems. Provides
events for enable and disable state changes, as well as methods to programmatically toggle the state.

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
          <li><a href="#onenabled">OnEnabled</a></li>
          <li><a href="#ondisabled">OnDisabled</a></li>
        </ul>
      </li>
      <li>
        <a href="#-properties">Properties</a>
        <ul>
          <li><a href="#enabled">Enabled</a></li>
        </ul>
      </li>
      <li>
        <a href="#-methods">Methods</a>
        <ul>
          <li><a href="#enable">Enable()</a></li>
          <li><a href="#disable">Disable()</a></li>
        </ul>
      </li>
    </ul>
  </li>
</ul>

---

## 🗂 Example of Usage

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

// Disable the object
enableSource.Disable();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEnableLifecycle
```

---

### ⚡ Events

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

### 🔑 Properties

#### `Enabled`

```csharp
public bool Enabled { get; }
```

- **Description:** Indicates whether the object is currently enabled.

---

### 🏹 Methods

#### `Enable()`

```csharp
public void Enable();
```

- **Description:** Enables the object, transitioning it into the **enabled state**.
- **Remarks:** Triggers `OnEnabled`.

#### `Disable()`

```csharp
public void Disable();
```

- **Description:** Disables the object, transitioning it into the **disabled state**.
- **Remarks:** Triggers `OnDisabled`.