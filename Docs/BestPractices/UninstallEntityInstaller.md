# 📌 Uninstall Method for EntityInstallers

## 📑 Table of Contents

- [Overview](#-overview)
- [Example of Usage](#-example-of-usage)
- [Notes](#-notes)

---

## 📖 Overview

[EntityInstallers](../Entities/Installers/Manual.md) provide an `Uninstall` method, which is useful for **unsubscribing
from events** or **cleaning up resources** when an entity is destroyed or removed from the scene.

---

## 🗂 Example of Usage

This example demonstrates subscribing to multiple events and cleaning them up automatically using `Uninstall`:

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

---

## 📝 Notes

- `Install` is called when the entity is created or added to the scene.
- `Uninstall` is called automatically when the entity is removed, ensuring **no memory leaks or dangling 
  subscriptions**.
- Using [DisposableComposite](../Elements/Utils/DisposableComposite.md) makes it easy to manage multiple subscriptions and resources in one place.
