# 🧩 Subscription Structs
The **Subscription** structs represent a **subscription** to a [Signal](ISignal.md). Disposing an instance will automatically **unsubscribe the associated action** from the **signal**, ensuring proper cleanup of event handlers.

---

## 🧩 Subscription
```csharp
public readonly struct Subscription : IDisposable
```
- **Description:** Represents a subscription to a **parameterless signal**.

### Constructors
#### `Subscription(ISignal, Action)`
```csharp
public Subscription(ISignal signal, Action action)
```
- **Description:** Initializes a new subscription for a parameterless signal.
- **Parameters:**
    - `signal` — The signal source.
    - `action` — The delegate to unsubscribe on disposal.

### Methods
#### `Dispose()`
```csharp
public void Dispose()
```
- **Description:** Unsubscribes the associated action from the signal source.

### 🗂 Example of Usage
```csharp
ISignal signal = ...
Subscription subscription = signal.Subscribe(lambda);

// Later, dispose to unsubscribe
subscription.Dispose();
```
---

## 🧩 Subscription&lt;T&gt;
```csharp
public readonly struct Subscription<T> : IDisposable
```
- **Description:** Represents a subscription to a **signal emitting one value**.
- **Type parameter:** `T` — The type of the emitted value.

### Constructors
#### `Subscription(ISignal<T>, Action<T>)`
```csharp
public Subscription(ISignal<T> signal, Action<T> action)
```
- **Description:** Initializes a new subscription for a signal emitting one value.
- **Parameters:**
    - `signal` — The signal source.
    - `action` — The delegate to unsubscribe on disposal.

### Methods
#### `Dispose()`
```csharp
public void Dispose()
```
- **Description:** Unsubscribes the associated action from the signal source.

### 🗂 Example of Usage
```csharp
ISignal<T> signal = ...
Subscription<T> subscription = signal.Subscribe<T>(lambda);

// Later, dispose to unsubscribe
subscription.Dispose();
```
---

## 🧩 Subscription<T1, T2>
```csharp
public readonly struct Subscription<T1, T2> : IDisposable
```
- **Description:** Represents a subscription to a **signal emitting two values**.
- **Type parameters:**
    - `T1` — The type of the first emitted value.
    - `T2` — The type of the second emitted value.

### Constructors
#### `Subscription(ISignal<T1, T2>, Action<T1, T2>)`
```csharp
public Subscription(ISignal<T1, T2> signal, Action<T1, T2> action)
```
- **Description:** Initializes a new subscription for a signal emitting two values.
- **Parameters:**
    - `signal` — The signal source.
    - `action` — The delegate to unsubscribe on disposal.

### Methods
#### `Dispose()`
```csharp
public void Dispose()
```
- **Description:** Unsubscribes the associated action from the signal source.

### 🗂 Example of Usage
```csharp
ISignal<T1, T2> signal = ...
Subscription<T1, T2> subscription = signal.Subscribe<T1, T2>(lambda);

// Later, dispose to unsubscribe
subscription.Dispose();
```
---

## 🧩 Subscription<T1, T2, T3>
```csharp
public readonly struct Subscription<T1, T2, T3> : IDisposable
```
- **Description:** Represents a subscription to a **signal emitting three values**.
- **Type parameters:**
    - `T1` — The type of the first emitted value.
    - `T2` — The type of the second emitted value.
    - `T3` — The type of the third emitted value.

### Constructors
#### `Subscription(ISignal<T1, T2, T3>, Action<T1, T2, T3>)`
```csharp
public Subscription(ISignal<T1, T2, T3> signal, Action<T1, T2, T3> action)
```
- **Description:** Initializes a new subscription for a signal emitting three values.
- **Parameters:**
    - `signal` — The signal source.
    - `action` — The delegate to unsubscribe on disposal.

### Methods
#### `Dispose()`
```csharp
public void Dispose()
```
- **Description:** Unsubscribes the associated action from the signal source.

### 🗂 Example of Usage
```csharp
ISignal<T1, T2, T3> signal = ...
Subscription<T1, T2, T3> subscription = signal.Subscribe<T1, T2, T3>(lambda);

// Later, dispose to unsubscribe
subscription.Dispose();
```
---

## 🧩 Subscription<T1, T2, T3, T4>
```csharp
public readonly struct Subscription<T1, T2, T3, T4> : IDisposable
```
- **Description:** Represents a subscription to a **signal emitting four values**.
- **Type parameters:**
    - `T1` — The type of the first emitted value.
    - `T2` — The type of the second emitted value.
    - `T3` — The type of the third emitted value.
    - `T4` — The type of the fourth emitted value.

### Constructors

#### `Subscription(ISignal<T1, T2, T3, T4>, Action<T1, T2, T3, T4>)`
```csharp
public Subscription(ISignal<T1, T2, T3, T4> signal, Action<T1, T2, T3, T4> action)
```
- **Description:** Initializes a new subscription for a signal emitting four values.
- **Parameters:**
    - `signal` — The signal source.
    - `action` — The delegate to unsubscribe on disposal.

### Methods
#### `Dispose()`
```csharp
public void Dispose()
```
- **Description:** Unsubscribes the associated action from the signal source.
### 🗂 Example of Usage
```csharp
ISignal<T1, T2, T3, T4> signal = ...
Subscription<T1, T2, T3, T4> subscription = signal.Subscribe<T1, T2, T3, T4>(lambda);

// Later, dispose to unsubscribe
subscription.Dispose();
```
---

## ✅ Practical Usage

The following example demonstrates how to use `Subscription` together with [DisposeComposite](../Utils/DisposableComposite.md) and `Atomic.Entities` framework to manage reactive event lifecycles cleanly.

```csharp
//Visual installer for Weapon Entity
public sealed class WeaponViewInstaller : SceneEntityInstaller
{
    [SerializeField] private ParticleSystem _fireVFX;
    [SerializeField] private AudioSource _fireSFX;
    [SerializeField] private Animator _animator;
    
    private readonly DisposableComposite _disposables = new();
    
    public void Install(IEntity entity)
    {
        // Retrieve the FireEvent signal from the entity
        ISignal fireEvent = entity.GetValue<ISignal>("FireEvent");
        
        // Subscribe multiple actions and add them to the disposables composite
        fireEvent.Subscribe(_fireVFX.Play).AddTo(_disposables);
        fireEvent.Subscribe(_fireSFX.Play).AddTo(_disposables);
        fireEvent.Subscribe(() => _animator.Play("Fire")).AddTo(_disposables);
    }
    
    private void OnDestroy()
    {
        // Dispose all subscriptions at once when the object is destroyed
        _disposables.Dispose();
    }
}



```

This pattern ensures that all subscriptions are automatically cleaned up when the object is destroyed, preventing memory leaks or lingering event handlers.

> [!NOTE]
> Using the [AddTo](../Utils/Extensions.md#addtoidisposable-disposablecomposite) extension method ties each subscription to a composite disposable for easy lifecycle management.
