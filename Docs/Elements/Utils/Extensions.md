### AddTo(IDisposable, DisposableComposite)


## Extension Method

### `AddTo(this IDisposable it, DisposableComposite composite)`

Adds a disposable to a composite using an extension method for fluent syntax.

- **Parameters:**  
  `it` — the `IDisposable` to add.  
  `composite` — the `DisposableComposite` instance.

- **Usage:**  
  Allows chaining subscriptions and disposables directly into the composite:

```csharp
_fireEvent.Subscribe(_fireVFX.Play).AddTo(_disposables);
_fireEvent.Subscribe(_fireSFX.Play).AddTo(_disposables);
```
---