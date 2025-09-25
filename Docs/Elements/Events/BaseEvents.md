# 🧩 BaseEvents

Provide **parameterless and generic reactive events** that can be **subscribed to, invoked,
and disposed**. They implement the corresponding [IEvent](IEvents.md) interfaces and allow both reactive tracking and
action-based invocation.

There are several implementations of events, depending on the number of arguments they take:

- [BaseEvent](BaseEvent.md) — Event without parameters.
- [BaseEvent&lt;T&gt;](BaseEvent%601.md) — Event that takes one argument.
- [BaseEvent&lt;T1, T2&gt;](BaseEvent%602.md) — Event that takes two arguments.
- [BaseEvent&lt;T1, T2, T3&gt;](BaseEvent%603.md) — Event that takes three arguments.
- [BaseEvent&lt;T1, T2, T3, T4&gt;](BaseEvent%604.md) — Event that takes four arguments.


---

## 🗂 Example of Usage

Below is an example of how to use `BaseEvent` for triggering a **sound effect** together with the `Atomic.Entities`
framework.

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