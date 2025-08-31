# 🧩 DisposableComposite

`DisposableComposite` is a utility class that manages multiple `IDisposable` objects.  
Disposing the composite will automatically dispose all contained objects, making resource management easier and safer.  
It also works seamlessly with the `AddTo` extension method for chaining disposables into a composite.

---

## Overview

- Manages a collection of `IDisposable` objects.
- Allows adding disposables dynamically.
- Supports method chaining for convenient composition.
- Disposing the composite disposes all contained objects and clears the collection.
- Includes an extension method `AddTo` for adding disposables to a composite fluently.

---

## Constructors

### `DisposableComposite(IEnumerable<IDisposable> disposables)`

Initializes a new instance of `DisposableComposite` with a collection of disposables.

- **Parameters:**  
  `disposables` — a collection of objects to add to the composite.

---

### `DisposableComposite(params IDisposable[] disposables)`

Initializes a new instance of `DisposableComposite` with a params array of disposables.

- **Parameters:**  
  `disposables` — an array of objects to add to the composite.

---

## Properties

### `int Count { get; }`

Returns the number of disposables currently contained in the composite.

---

## Methods

### `DisposableComposite Add(IDisposable disposable)`

Adds a new disposable to the composite.

- **Parameters:**  
  `disposable` — the object to add (cannot be null).

- **Returns:**  
  The current `DisposableComposite` instance for method chaining.

- **Exceptions:**  
  Throws `ArgumentNullException` if `disposable` is null.

---

### `void Dispose()`

Disposes all contained disposables and clears the composite.

---

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
## Example Usage
```csharp
using System;
using Atomic.Elements;
using UnityEngine;

public class DisposableExample : MonoBehaviour
{
    private IEvent _fireEvent = new BaseEvent();
    private DisposableComposite _disposables = new DisposableComposite();
    
    [SerializeField]
    private ParticleSystem _fireVFX;
    
    [SerializeField]
    private AudioSource _fireSFX;
    
    private void Awake()
    {
        _fireEvent.Subscribe(_fireVFX.Play).AddTo(_disposables);
        _fireEvent.Subscribe(_fireSFX.Play).AddTo(_disposables);
    }
    
    private void OnDestroy()
    {
        _disposables.Dispose();
    }
}
```