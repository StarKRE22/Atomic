# 📌 DisposeComposite in EntityInstallers

[DisposableComposite](../Elements/Utils/DisposableComposite.md) allows you to **manage multiple subscriptions or
disposables as a single unit**. Combined with
the [AddTo](../Elements/Utils/Extensions.md#addtoidisposable-disposablecomposite) extension method, it
ensures that all subscriptions are **cleanly disposed** when the object is destroyed, preventing memory leaks or
lingering event handlers.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [Conclusion](#-conclusion)
- [Benefits](#-benefits)

---

## 🗂 Example of Usage

This example demonstrates using `DisposableComposite` in a weapon visual configuration via `WeaponViewInstaller`:

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
         // Dispose all subscriptions and resources when the installer is removed
        _disposables.Dispose();
    }
}
```

> [!TIP]
> Using `AddTo(_disposables)` ties each subscription to the composite, so you don’t need to manually unsubscribe each
> one. This ensures **automatic cleanup** when the installer or MonoBehaviour is destroyed.

---

## 🏁 Conclusion

- `DisposableComposite` enables **centralized management of multiple subscriptions or disposables**.
- The `AddTo` extension method simplifies **chaining disposables into the composite**.
- This pattern guarantees **automatic cleanup** of event subscriptions, preventing memory leaks and unintended
  callbacks.
- Integrates seamlessly with [Atomic.Entities](../Entities/Manual.md), improving lifecycle management of entity-related
  events.
- Promotes **safe, modular, and maintainable reactive systems** in Unity projects.

---

## ✅ Benefits

- Centralizes **subscription disposal** for multiple events.
- Prevents **memory leaks** and dangling event handlers.
- Simplifies **installer and component cleanup**.
- Works with **Reactive events, ISignal, and other disposable patterns**.
- Encourages **modular, maintainable, and predictable lifecycle management**.  
