# 🧩 IEvent

```csharp
public interface IEvent : ISignal, IAction
```
- **Description:** Represents a <b>parameterless event</b> that can be subscribed to and invoked.
- **Inheritance:** [ISignal](ISignal.md), [IAction](../Actions/IAction.md)

---

## 🏹 Methods

#### `Invoke()`

```csharp
public void Invoke();
```

- **Description:** Executes the event logic