# 🧩 IInitLifecycle

Represents a **runtime-controllable initialization and disposal contract** for entities or systems.
Provides events for initialization and disposal state changes, as well as a method to explicitly initialize the
object.

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
          <li><a href="#oninitialized">OnInitialized</a></li>
          <li><a href="#ondisposed">OnDisposed</a></li>
        </ul>
      </li>
      <li>
        <a href="#-properties">Properties</a>
        <ul>
          <li><a href="#initialized">Initialized</a></li>
        </ul>
      </li>
      <li>
        <a href="#-methods">Methods</a>
        <ul>
          <li><a href="#init">Init()</a></li>
          <li><a href="#dispose">Dispose()</a></li>
        </ul>
      </li>
    </ul>
  </li>
</ul>


---

## 🗂 Example of Usage

```csharp
// Assume we have an instance of IInitLifecycle
IInitLifecycle initSource = ...;

// Subscribe to events
initSource.OnInitialized += () => Console.WriteLine("Object initialized");
initSource.OnDisposed += () => Console.WriteLine("Object disposed");

// Check initial state
Console.WriteLine($"Initialized: {initSource.Initialized}");

// Initialize the object
initSource.Init();

// Dispose the object
initSource.Dispose();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IInitLifecycle
```

- **Inheritance:** `IDisposable`

---

### ⚡ Events

#### `OnInitialized`

```csharp
public event Action OnInitialized;
```

- **Description:** Triggered when the object has been successfully initialized.

#### `OnDisposed`

```csharp
public event Action OnDisposed;
```

- **Description:** Triggered when the object has been disposed and its resources released.

---

### 🔑 Properties

#### `Initialized`

```csharp
public bool Initialized { get; }
```

- **Description:** Indicates whether the object is currently initialized.

---

### 🏹 Methods

#### `Init()`

```csharp
public void Init();
```

- **Description:** Initializes the object, transitioning it into the **initialized state**.
- **Remarks:** Triggers `OnInitialized`.

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Disposes the object and releases its resources, transitioning it into the **disposed state**.
- **Remarks:** Triggers `OnDisposed`.