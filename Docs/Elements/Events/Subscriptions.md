# 🧩 Subscriptions

Represent a **subscription** to a [ISignal](ISignals.md) instance. Disposing an instance will automatically **unsubscribe the
associated action** from the **signal**, ensuring proper cleanup of event handlers.

There are several subscriptions, depending on the number of arguments they take:

- [Subscription](Subscription.md) — Subscription without parameters.
- [Subscription&lt;T&gt;](Subscription%601.md) — Subscription that takes one argument.
- [Subscription&lt;T1, T2&gt;](Subscription%602.md) — Subscription that takes two arguments.
- [Subscription&lt;T1, T2, T3&gt;](Subscription%603.md) — Subscription that takes three arguments.
- [Subscription&lt;T1, T2, T3, T4&gt;](Subscription%604.md) — Subscription that takes four arguments.


---

## 📌 Best Practice

The following example demonstrates how to use `Subscription` together
with [DisposeComposite](../Utils/DisposableComposite.md) and `Atomic.Entities` framework to manage reactive event
lifecycles cleanly.

```csharp
//Visual installer of a weapon
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

Using the [AddTo](../Utils/Extensions.md#addtoidisposable-disposablecomposite) extension method ties each subscription
to a composite disposable for easy lifecycle management. This pattern ensures that all subscriptions are automatically
cleaned up when the object is destroyed, preventing memory
leaks or lingering event handlers.