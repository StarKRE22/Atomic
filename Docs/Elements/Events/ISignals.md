# 🧩 ISignals

Define a family of contracts for **reactive event sources**. It provides a lightweight abstraction for subscribing to
notifications and reacting to events, optionally with arguments. When subscribing to a signal, the method returns
a [Subscription](Subscriptions.md) struct.

There are several interfaces of signals, depending on the number of arguments they take:

- [ISignal](ISignal.md) — Signal without parameters.
- [ISignal&lt;T&gt;](ISignal%601.md) — Signal that takes one argument.
- [ISignal&lt;T1, T2&gt;](ISignal%602.md) — Signal that takes two arguments.
- [ISignal&lt;T1, T2, T3&gt;](ISignal%603.md) — Signal that takes two arguments.
- [ISignal&lt;T1, T2, T3, T4&gt;](ISignal%604.md) — Signal that takes two arguments.

---

## 🗂 Example of Usage

Below is an example of how to use `ISignal` for triggering a **sound effect** together with the `Atomic.Entities`
framework.

```csharp
// Create an entity with a "FireEvent" signal property
var entity = new Entity("Character");
entity.AddValue("FireEvent", new BaseEvent()); //ISignal
```

```csharp
// Use "FireEvent" through the ISignal interface
[Serializable]
public class FireSFXBehaviour : IEntityInit, IEntityDispose
{
    [Serializable]
    private AudioClip _fireSFX;

    [Serializable]
    private AudioSource _audioSource;
    
    private ISignal _fireSignal;
    
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