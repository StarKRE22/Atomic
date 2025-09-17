# 🧩 DisposableComposite

Represents a **composite disposable container** that manages multiple `IDisposable` objects. Disposing the composite will automatically dispose all contained objects, making resource management easier and safer. It also works seamlessly with the [AddTo](Extensions.md/#addtoidisposable-disposablecomposite) extension method for chaining disposables into a composite.

---



## Properties

#### `Count`
```csharp
public int Count { get; }
```
- **Description:** Gets the number of disposables currently in the composite.
- **Remarks:** Useful to check how many disposables are managed before disposal.

---

## Constructors

#### `DisposableComposite(IEnumerable<IDisposable>)`
```csharp
public DisposableComposite(IEnumerable<IDisposable> disposables);
```
- **Description:** Initializes a new instance of `DisposableComposite` with a collection of disposables.
- **Parameter:** `disposables` — Initial disposables to add.
- **Remarks:** Copies all disposables from the provided collection into the internal list.

#### `DisposableComposite(params IDisposable[])`
```csharp
public DisposableComposite(params IDisposable[] disposables);
```
- **Description:** Initializes a new instance of `DisposableComposite` with a params array of disposables.
- **Parameter:** `disposables` — Initial disposables to add.
- **Remarks:** Provides a convenient way to pass multiple disposables inline.

---

## Methods

#### `Add(IDisposable)`
```csharp
public DisposableComposite Add(IDisposable disposable);
```
- **Description:** Adds a new `IDisposable` to the composite.
- **Parameter:** `disposable` — The disposable object to add. Cannot be `null`.
- **Returns:** The current `DisposableComposite` instance for chaining.
- **Exceptions:** Throws `ArgumentNullException` if `disposable` is `null`.
- **Remarks:** Use this method to dynamically add disposables after initialization.

#### `Dispose()`
```csharp
public void Dispose();
```
- **Description:** Disposes all contained `IDisposable` objects and clears the composite.
- **Remarks:** After calling this method, the internal list is empty. The composite can be reused by adding new disposables.

---
## 🗂 Example of Usage

Below is an example of using `DisposableComposite` in a weapon visual configuration via `WeaponViewInstaller` in the `Atomic.Entities` framework. This demonstrates the use of the [AddTo](Extensions.md/#addtoidisposable-disposablecomposite) extension method to chain disposables into the composite.

```csharp
public sealed class WeaponViewInstaller : SceneEntityInstaller
{
    [SerializeField] private ParticleSystem _fireVFX;
    [SerializeField] private AudioSource _fireSFX;
    [SerializeField] private Animator _animator;

    private readonly DisposableComposite _disposables = new();
    
    public void Install(IEntity entity)
    {
        ISignal fireEvent = entity.GetFireEvent();
        
        // Subscribe to the "Fire" event and automatically add to the composite
        fireEvent.Subscribe(_fireVFX.Play).AddTo(_disposables);
        fireEvent.Subscribe(_fireSFX.Play).AddTo(_disposables);
        fireEvent.Subscribe(() => _animator.SetTrigger("Fire")).AddTo(_disposables);
    }
    
    private void OnDestroy()
    {
        // Dispose all resources when the object is destroyed
        _disposables.Dispose();
    }
}
```

### 🔹 How it works

1. **Creating the composite:** `_disposables` stores all subscriptions and disposable objects.
2. **Subscribing with AddTo:** The `AddTo(_disposables)` method automatically adds each disposable to the composite.
3. **Resource cleanup:** When `WeaponViewInstaller` is destroyed, `_disposables.Dispose()` is called, ensuring all subscriptions are properly disposed and resources are released.
