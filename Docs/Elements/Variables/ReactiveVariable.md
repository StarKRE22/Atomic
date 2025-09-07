# 🧩 ReactiveVariable<T>

`ReactiveVariable<T>` is a **serialized reactive variable** that raises events whenever its value changes.  
It implements `IReactiveVariable<T>` and `IDisposable`, allowing **reactive programming patterns** in Unity or pure C# environments.

> **Note:** For common types like `float`, `int`, `bool`, `Vector2`, `Vector3`, or `Quaternion`, there are **specialized reactive variants**:
> - `ReactiveFloat`, `ReactiveInt`, `ReactiveBool`
> - `ReactiveVector2`, `ReactiveVector3`
> - `ReactiveQuaternion`
  > These variants **do not require an `EqualityComparer`** and allow slightly faster `Value` assignment.
>
> Additionally, for projects using **Unity.Mathematics**, there are optimized reactive types:
> - `float3Reactive`
> - `quaternionReactive`
---

## Type Parameter

- `T` – The type of the stored value.

---

## Events

```csharp
event Action<T> OnValueChanged;
```
- Description: Triggered whenever the Value changes.
- Notes: Can be subscribed to via Subscribe for automatic unsubscription support.

## Properties
```csharp
- T Value { get; set; }
```
- Description: Gets or sets the current value.
- Behavior:
  - When a new value differs from the previous one, OnValueChanged is triggered.
  - Read-write access.

## Methods
```csharp
T Invoke()
```
- Returns the current value. Useful for functional-style invocation or delegate pointers.

```csharp
Subscription<T> Subscribe(Action<T> listener)
```
- Subscribes a listener to value changes. Returns a Subscription<T> for easy unsubscription.
```csharp
void Unsubscribe(Action<T> listener)
```
- Removes a previously subscribed listener.

```csharp
void Dispose()
```
- Clears all listeners and releases resources.

```csharp
public override string ToString()
```
- Returns a string representation of the current value.

## Constructors
```csharp
// Default constructor
public ReactiveVariable()

// Constructor with initial value
public ReactiveVariable(T value)
```
- Description:
  - ReactiveVariable() initializes with default(T).
  - ReactiveVariable(T value) initializes with the specified value.

## Implicit Conversion
```csharp
public static implicit operator ReactiveVariable<T>(T value)
```
- Allows assigning a plain value to a ReactiveVariable<T> directly.

## Example Usage
```csharp
using UnityEngine;
using Atomic.Elements;

public class Example : MonoBehaviour
{
    private ReactiveVariable<int> _score;

    void Start()
    {
        // Initialize with a starting value
        _score = new ReactiveVariable<int>(10);

        // Subscribe to changes
        _score.Subscribe(newScore =>
        {
            Debug.Log("Score updated: " + newScore);
        });

        // Change the value
        _score.Value = 20; // Triggers subscription callback

        // Functional-style invocation
        Debug.Log("Current Score: " + _score.Invoke());
    }

    void OnDestroy()
    {
        // Dispose to clear subscriptions
        _score.Dispose();
    }
}
```
