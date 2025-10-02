
## 📌 Using Uninstall Method for EntityInstallers 

**SceneEntityInstaller** also has an `Uninstall` method, which can be useful for unsubscribing or cleaning up when an
entity is destroyed or removed from the scene.

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