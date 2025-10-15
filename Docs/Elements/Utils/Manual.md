# 🧩 Utilities

Provides a collection of **utility classes and components** that simplify common tasks in Unity and C# development. This
includes handling animation and collision events, trigger detection, disposable actions, optional references, and
various helper extensions. These utilities help reduce boilerplate code and make systems more modular and maintainable.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
  - [Reference\<T>](#ex1)
  - [Optional\<T>](#ex2)
  - [Atomic Utils](#ex3)
  - [Disposable Action](#ex4)
  - [Disposable Composite](#ex5)
  - [Animation Events](#ex6)
  - [Collision Events](#ex7)
  - [Trigger Events](#ex8)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Reference\<T>

The reference can serve as a lightweight container for `out` parameters in Unity coroutines or asynchronous tasks.
This allows coroutines or async methods to update a value that the caller can access after the operation completes.

```csharp
public class Example : MonoBehaviour
{
    private Reference<int> result = new();

    private void Start()
    {
        StartCoroutine(CalculateRoutine(result));
    }

    private IEnumerator CalculateRoutine(Reference<int> output)
    {
        yield return new WaitForSeconds(2f);
        output.Value = 42; // Set the result
    }
}
```

---

<div id="ex2"></div>

### 2️⃣ Optional\<T>

Below is an example of using optional type for weapon visualization:

```csharp
public sealed class WeaponView : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;

    [SerializeField] private Optional<ParticleSystem> _vfx;
    [SerializeField] private Optional<AudioSource> _sfx;
    [SerializeField] private Optional<Animation> _animation;

    private void OnEnable()
    {
        _weapon.OnFire += this.OnFire;
    }
    
    private void OnDisable()
    {
        _weapon.OnFire -= this.OnFire;
    }
    
    private void OnFire()
    {
        if (_vfx) _vfx.Play();
        if (_sfx) _sfx.Play();
        if (_animation) _animation.Play();
    }
}
```

---

<div id="ex3"></div>

### 3️⃣ Atomic Utils

You can check application environment: PlayMode or Editor Mode safely:

```csharp
// Check for Play Mode
if (AtomicUtils.IsPlayMode())
    Console.WriteLine("Application is running in Play Mode.");

// Check for Editor Mode
if (AtomicUtils.IsEditMode())
    Console.WriteLine("Application is in Edit Mode and not compiling.");
```

<div id="ex4"></div>

### 4️⃣ Disposable Action

You can create disposable wrappers for resource releasing and event unsubscription:

```csharp
// Assume we have a some observable
ISignal signal = ...
    
// Assume we have a some event handler
Action handler = () => Console.WriteLine("Event fired.");

// Subscribe on event
signal.OnEvent += handler;

// Create an action that unsubscribes this handler
var disposable = new DisposableAction(() => 
{
    signal.OnEvent -= handler;
});

// Later, unsubscribe the handler
disposable.Dispose();
```

---

<div id="ex5"></div>

### 5️⃣ Disposable Composite

You can store subscriptions and other disposables into special container.

Below is an non-generic version:

```csharp
//Assume we have some instance of signal
ISignal signal = ...

//Create a container of disposables 
var composite = new DisposableComposite();

//Subscribing to the signal
IDisposable disposable = signal.Subscribe(() => Console.WriteLine("Hi!"));

//Add subscription to the composite
composite.Add(disposable);

// Later, when disposing
composite.Dispose(); // All disposables including the subscription are disposed
```

Also, you can use generic version to avoid boxing to `IDisposable` when add struct disposable to the composite:

```csharp
// Assume we have multiple subscriptions
Subscription subscription1 = ...
Subscription subscription2 = ...

// Create a generic composite without boxing to IDisposable
var composite = new DisposableComposite<Subscription>(subscription1, subscription2);

// Add another disposable dynamically
IDisposable subscription3 = ...
composite.Add(subscription3);

// Later, unsubscribe all
composite.Dispose(); // All subscriptions are disposed and the internal list is cleared
```

---

<div id="ex6"></div>

### 6️⃣ Animation Events

Subscribe to facade with animation events without hardcoding special methods for `AnimationEvents` in the *
*MonoBehaviours**:

```csharp
public class Example : MonoBehaviour
{
    [SerializeField]
    private AnimationEvents _animEvents;

    private void OnEnable()
    {
        _animEvents.Subscribe("Hello", OnHello);
        _animEvents.OnEvent += OnAnimationEvent;
    }

    private void OnDisable()
    {
        _animEvents.Unsubscribe("Hello", OnHello);
        _animEvents.OnEvent -= OnAnimationEvent;
    }

    private void OnHello() => Debug.Log("Hello!");
    
    private void OnAnimationEvent(string evt) => Debug.Log($"Event triggered: {evt}");
}
```

---

<div id="ex7"></div>

### 7️⃣ Collision Events

Collision event aggregator:

```csharp
public class Example : MonoBehaviour
{
    [SerializeField]
    private CollisionEvents _collisionEvents;

    private void OnEnable()
    {
        _collisionEvents.OnEntered += HandleEnter;
        _collisionEvents.OnExited  += HandleExit;
        _collisionEvents.OnStay    += HandleStay;
    }

    private void OnDisable()
    {
        _collisionEvents.OnEntered -= HandleEnter;
        _collisionEvents.OnExited  -= HandleExit;
        _collisionEvents.OnStay    -= HandleStay;
    }

    private void HandleEnter(Collision collision)
        => Debug.Log($"Collision started with {collision.gameObject.name}");

    private void HandleExit(Collision collision)
        => Debug.Log($"Collision ended with {collision.gameObject.name}");

    private void HandleStay(Collision collision)
        => Debug.Log($"Still colliding with {collision.gameObject.name}");
}
```

Also, there is a `CollisionEvents2D` component for 2D Physics

---

<div id="ex8"></div>

### 8️⃣ Trigger Events

Trigger event aggregator:

```csharp
public class Example : MonoBehaviour
{
    [SerializeField]
    private TriggerEvents _triggerEvents;

    private void OnEnable()
    {
        _triggerEvents.OnEntered += HandleEnter;
        _triggerEvents.OnExited  += HandleExit;
        _triggerEvents.OnStay    += HandleStay;
    }

    private void OnDisable()
    {
        _triggerEvents.OnEntered -= HandleEnter;
        _triggerEvents.OnExited  -= HandleExit;
        _triggerEvents.OnStay    -= HandleStay;
    }

    private void HandleEnter(Collider other)
        => Debug.Log($"Trigger entered by {other.gameObject.name}");

    private void HandleExit(Collider other)
        => Debug.Log($"Trigger exited by {other.gameObject.name}");

    private void HandleStay(Collider other)
        => Debug.Log($"Still inside trigger: {other.gameObject.name}");
}
```

Also, there is a `TriggerEvents2D` component for 2D Physics

---

## 🔍 API Reference

- **Common**
    - [Reference](Reference.md) <!-- + -->
    - [Optional](Optional.md) <!-- + -->
    - [AtomicUtils](AtomicUtils.md)  <!-- + -->
- **Disposables**
    - [DisposableAction](DisposableAction.md) <!-- + -->
    - [DisposableComposite](DisposableComposite.md) <!-- + -->
    - [DisposableComposite\<T>](DisposableComposite%601.md) <!-- + -->
    - [Extensions](Extensions.md) <!-- + -->
- **UnityComponents**
    - [AnimationEvents](AnimationEvents.md) <!-- + -->
    - [CollisionEvents](CollisionEvents.md) <!-- + -->
    - [CollisionEvents2D](CollisionEvents2D.md) <!-- + -->
    - [TriggerEvents](TriggerEvents.md) <!-- + -->
    - [TriggerEvents2D](TriggerEvents2D.md) <!-- + -->

---

## 📌 Best Practices

Below are references to best practices for using these utilities with the [Atomic.Entities](../../Entities/Manual.md) framework:

- [Using Optional with EntityInstallers](../../BestPractices/UsingOptionalWithInstallers.md)
- [Using AtomicUtils for EntityInstallers](../../BestPractices/UsingUtilsForEntityInstallers.md)
- [Using DisposeComposite in EntityInstallers](../../BestPractices/UsingSubscriptionsWithDisposeComposite.md)

