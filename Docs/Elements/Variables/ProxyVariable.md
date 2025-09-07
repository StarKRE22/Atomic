# 🧩 ProxyVariable<T>

`ProxyVariable<T>` provides a **read-write variable** that delegates its value to **external getter and setter functions**.  
It implements `IVariable<T>`, allowing you to **wrap existing data sources** and expose them through a unified variable interface.

This is useful when you want to integrate third-party or existing fields/properties into systems expecting `IVariable<T>` without duplicating state.

---

## Type Parameter
- `T` – The type of the value being proxied.

---

## Constructors

```csharp
public ProxyVariable(Func<T> getter, Action<T> setter)
```
- getter – A function to retrieve the value.
- setter – An action to update the value.
- Throws: ArgumentNullException if either argument is null.

## Properties
```csharp
T Value { get; set; }
```
- Gets or sets the proxied value.
- Returns the default value if getter is not assigned.

## Builder API
ProxyVariable<T> also includes a fluent builder to simplify creation:

```csharp
var variable = ProxyVariable<int>
    .StartBuild()
    .WithGetter(() => myField)
    .WithSetter(v => myField = v)
    .Build();
```

## Example Usage
Wrapping a Transform’s Position

```csharp
using UnityEngine;
using Atomic.Elements;

public class Player
{
    private readonly IVariable<Vector3> _position;

    public Player(Transform transform)
    {
        _position = new ProxyVariable<Vector3>(
            getter: () => transform.position,
            setter: v => transform.position = v
        );
    }

    public void Move(Vector3 delta)
    {
        _position.Value += delta;
    }
}
```
## Using the Fluent Builder

```csharp
int health = 100;

IVariable<int> healthVariable = ProxyVariable<int>
    .StartBuild()
    .WithGetter(() => health)
    .WithSetter(v => health = v)
    .Build();

Debug.Log(healthVariable.Value); // Output: 100
healthVariable.Value = 80;
Debug.Log(health);               // Output: 80
```

## When to Use
- Integrating external or third-party APIs (e.g., Unity’s Transform, networking states).
- Adapting existing properties/fields to IVariable<T> without refactoring.
- Testing: Makes it easy to substitute mock getters/setters in unit tests.