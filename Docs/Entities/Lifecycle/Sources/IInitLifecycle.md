# 🧩 IInitLifecycle

```csharp
public interface IInitLifecycle
```

- **Description:** Represents a **runtime-controllable initialization and disposal contract** for entities or systems.
  Provides events for initialization and disposal state changes, as well as a method to explicitly initialize the
  object.
- **Inheritance:** `IDisposable`
- **Note:** This is an **internal framework interface**. You should **not implement it manually** — it is used by the
  framework infrastructure.

---

## ⚡ Events

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

## 🔑 Properties

#### `Initialized`

```csharp
public bool Initialized { get; }
```

- **Description:** Indicates whether the object is currently initialized.

---

## 🏹 Methods

#### `Init`

```csharp
public void Init();
```

- **Description:** Initializes the object, transitioning it into the **initialized state**.
- **Remarks:** Triggers `OnInitialized`.

#### `Dispose`

```csharp
public void Dispose();
```

- **Description:** Disposes the object and releases its resources, transitioning it into the **disposed state**.
- **Remarks:** Triggers `OnDisposed`.