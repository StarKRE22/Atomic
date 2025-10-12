# 📌 Using Events with Entities

Below is an example of how to use [BaseEvent](BaseEvent.md) for triggering a **sound effect** together with the [Atomic.Entities](../../Entities/Manual.md)
framework:

---

#### 1. Create an entity with a `FireEvent` property

```csharp
var entity = new Entity("Character");
entity.AddValue("FireEvent", new BaseEvent());
```

#### 2. Use `FireEvent` through the `ISignal` interface

```csharp
[Serializable]
public class FireSFXBehaviour : IEntityInit, IEntityDispose
{
    [Serializable]
    private AudioClip _fireSFX;

    [Serializable]
    private AudioSource _audioSource;
    
    private ISignal _fireSignal; //IEvent
    
    public void Init(IEntity entity)
    {
        _fireSignal = entity.GetValue<ISignal>("FireEvent");
        _fireSignal.Subscribe(this.OnFire);
    }
    
    public void Dispose(IEntity entity)
    {
        _fireSignal.Unsubscribe(this.OnFire);
    }
    
    private void OnFire()
    {
        _audioSource.PlayOneShot(_fireSFX);
    }
}
```

#### 3. Invoke `FireEvent` through the `IAction` interface

```csharp
IAction fireEvent = entity.GetValue<IAction>("FireEvent");
fireEvent.Invoke();
```