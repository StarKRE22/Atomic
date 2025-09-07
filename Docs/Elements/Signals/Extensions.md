# 🧩  Extensions — Subscribing and Unsubscribing Helpers

This static partial class provides extension methods to simplify working with `IAction` and `ISignal` instances.

## Main Features

### Subscribe
Methods to subscribe an `IAction` to `ISignal` sources with 0–4 generic parameters.  
Returns a corresponding `Subscription` struct.

```csharp
var subscription = signal.Subscribe(myAction);
```

### Unsubscribe
Methods to unsubscribe an `IAction` from `ISignal` sources with 0–4 generic parameters.
```csharp
signal.Unsubscribe(myAction);
```
### SubscribeRange
Methods to subscribe a collection of `IAction` instances to a reactive source. Null actions are ignored.
```csharp
signal.SubscribeRange(new IAction<T>[] { action1, action2 });
```
### UnsubscribeRange
Methods to unsubscribe a collection of `IAction` instances from a reactive source. Null actions are ignored.
```csharp
signal.UnsubscribeRange(new IAction<T>[] { action1, action2 });
```

### Notes
- Works for non-generic signals and generic signals with 1–4 parameters.
- `Subscription` structs allow disposal and automatic unsubscription.