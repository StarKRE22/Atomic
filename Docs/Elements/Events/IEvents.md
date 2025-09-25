# 🧩 IEvents

Define a family of contracts for **reactive events** that can be both **observed** and **invoked**.
They combine the capabilities of [ISignal](ISignals.md) and [IAction](../Actions/IActions.md),
allowing subscription-based reactive tracking and direct action-based invocation.

There are several interfaces of events, depending on the number of arguments they take:

- [IEvent](IEvent.md) — Event without parameters.
- [IEvent&lt;T&gt;](IEvent%601.md) — Event that takes one argument.
- [IEvent&lt;T1, T2&gt;](IEvent%602.md) — Event that takes two arguments.
- [IEvent&lt;T1, T2, T3&gt;](IEvent%603.md) — Event that takes two arguments.
- [IEvent&lt;T1, T2, T3, T4&gt;](IEvent%604.md) — Event that takes two arguments.

---

## 🗂 Example of Usage

Below is an example of how to use `IEvent` for triggering a **sound effect** together with the `Atomic.Entities`
framework.

#### 1. Create an entity with a `FireEvent` property

```csharp
var entity = new Entity("Character");
entity.AddValue("FireEvent", new BaseEvent()); //IEvent
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