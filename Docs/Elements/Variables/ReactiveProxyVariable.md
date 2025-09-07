# 🧩 ReactiveProxyVariable<T>

`ReactiveProxyVariable<T>` is a **reactive proxy variable** that delegates reading, writing, and subscription operations to external handlers.  
This is useful when you need to **wrap an existing data source or event system** and expose it through the unified `IReactiveVariable<T>` interface.

---

## Type Parameter
- `T` – the value type.

---

## Constructors

```csharp
public ReactiveProxyVariable()
```
- Creates an empty instance. Delegates must be provided later using the builder.

```csharp
public ReactiveProxyVariable(
    Func<T> getter,
    Action<T> setter,
    Action<Action<T>> subscribe,
    Action<Action<T>> unsubscribe
)
```
- getter – function to retrieve the current value.
- setter – action to update the value.
- subscribe – handler for subscribing to change notifications.
- unsubscribe – handler for unsubscribing.
- Throws: ArgumentNullException if any argument is null.


## Properties
```csharp
T Value { get; set; }
```
- gets or sets the current value using delegated functions.

## Events
```csharp
event Action<T> OnValueChanged;
```
- invoked whenever the value changes.

## Methods
```csharp
//registers a listener and returns a subscription handle.
Subscription<T> Subscribe(Action<T> action)

//removes a previously registered listener.
void Unsubscribe(Action<T> action)
```

## Fluent Builder
ReactiveProxyVariable<T> provides a convenient fluent builder

```csharp
var variable = ReactiveProxyVariable<int>
    .StartBuild()
    .WithGetter(() => someInt)
    .WithSetter(v => someInt = v)
    .WithSubscribe(cb => myEvent += cb)
    .WithUnsubscribe(cb => myEvent -= cb)
    .Build();
```

## Example Usage
Wrapping an external field and event
```csharp
using System;
using Atomic.Elements;

public class Player
{
    private int _health;
    public event Action<int> HealthChanged;

    public IReactiveVariable<int> Health { get; }

    public Player()
    {
        Health = new ReactiveProxyVariable<int>(
            getter: () => _health,
            setter: v => { _health = v; HealthChanged?.Invoke(v); },
            subscribe: h => HealthChanged += h,
            unsubscribe: h => HealthChanged -= h
        );
    }
}
```

## Testing with the builder
```csharp
int score = 0;
event Action<int> scoreChanged;

var scoreVar = ReactiveProxyVariable<int>
    .StartBuild()
    .WithGetter(() => score)
    .WithSetter(v => { score = v; scoreChanged?.Invoke(v); })
    .WithSubscribe(cb => scoreChanged += cb)
    .WithUnsubscribe(cb => scoreChanged -= cb)
    .Build();

// Subscribe
scoreVar.Subscribe(v => Console.WriteLine($"Score updated: {v}"));

// Update value
scoreVar.Value = 100; // Output: "Score updated: 100"
```

## When to Use
- To integrate with external APIs or event systems.
- To adapt existing fields or properties into IReactiveVariable<T>.
- Testing: easily substitute mocks or test delegates.
