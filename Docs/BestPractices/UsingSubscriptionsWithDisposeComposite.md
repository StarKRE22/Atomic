# 📌 DisposeComposite in Using Entity Installers 

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



## 🗂 Example of Usage

Below is an example of using `DisposableComposite` in a weapon visual configuration via `WeaponViewInstaller` in the
`Atomic.Entities` framework. This demonstrates the use of
the [AddTo](Extensions.md/#addtoidisposable-disposablecomposite) extension method to chain disposables into the
composite.

```csharp
public sealed class WeaponViewInstaller : SceneEntityInstaller
{
    [SerializeField] private ParticleSystem _fireVFX;
    [SerializeField] private AudioSource _fireSFX;
    [SerializeField] private Animator _animator;

    private readonly DisposableComposite _disposables = new();
    
    public override void Install(IEntity entity)
    {
        ISignal fireEvent = entity.GetFireEvent();
        
        // Subscribe to the "Fire" event and automatically add to the composite
        fireEvent.Subscribe(_fireVFX.Play).AddTo(_disposables);
        fireEvent.Subscribe(_fireSFX.Play).AddTo(_disposables);
        fireEvent.Subscribe(() => _animator.SetTrigger("Fire")).AddTo(_disposables);
    }
    
    public override void Uninstall()
    {
         // Dispose all resources when the object is destroyed
        _disposables.Dispose();
    }
}
```