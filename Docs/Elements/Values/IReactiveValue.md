# 🧩 IReactiveValue&lt;T&gt;

`IReactiveValue<T>` represents a **reactive value** that combines:

- **Read-only access** through [IValue&lt;T&gt;](IValue.md)
- **Reactive observation** through [ISignal&lt;T&gt;](../Signals/ISignal.md)

It allows you to both **read the current value** and **subscribe to changes**.

---

## Type Parameter

- `T` – The type of the value.

---

## Properties
```csharp
T Value { get; }
```
- Description: Gets the current value.
- Access: Read-only

## Methods
### Invoke()
```csharp
T Invoke()
```
- Description: Invokes the function and returns the value.
  This is the default implementation from [IFunction&lt;R&gt;](../Functions/IFunction.md#invoke) and simply returns Value.
- Returns: The current value of type `T`.
---

### Subscribe(Action)
```csharp
Subscription<T> Subscribe(Action action)  
```
- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameters:**
  - `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription&lt;T&gt;](../Signals/Subscription.md#subscriptiont) struct representing the active subscription.

### Unsubscribe(Action)
```csharp
void Unsubscribe(Action action)  
```
- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:**
  - `action` – The delegate to remove from the subscription list.


## 🗂 Example of Usage
`IReactiveValue<T>` is useful in **reactive programming** or **UI binding scenarios**, where you want to **observe changes** to a value while keeping it **read-only**.

For example, rendering score text on a UI in Unity:
```csharp
using UnityEngine;
using UnityEngine.UI; // Required for Text
using Atomic.Elements;
using System;

public sealed class ScorePresenter : IDisposable
{
    private readonly IReactiveValue<int> _score;
    private readonly Text _scoreText;
    
    public ScorePresenter(IReactiveValue<int> score, Text scoreText)
    {
        _score = score;
        _scoreText = scoreText;

        // Subscribe to reactive updates
        _score.Subscribe(this.OnScoreChanged);

        // Initialize UI with current value
        this.OnScoreChanged(_score.Value);
    }
    
    public void Dispose()
    {
        // Unsubscribe when no longer needed
        _score.Unsubscribe(this.OnScoreChanged);
    }
    
    private void OnScoreChanged(int score)
    {
        // Update UI text
        _scoreText.text = "Score: " + score;
    }
}
```

We suppose to use [Observe](Extensions.md/#observe) extension method  and cache subscription handle
```csharp
using UnityEngine;
using UnityEngine.UI; // Required for Text
using Atomic.Elements;
using System;

public sealed class ScorePresenter : IDisposable
{
    private readonly IReactiveValue<int> _score;
    private readonly Text _scoreText;
    private readonly Subscription<int> _subscription; //struct
    
    public ScorePresenter(IReactiveValue<int> score, Text scoreText)
    {
        _score = score;
        _scoreText = scoreText;

        // Subscribe and initialize UI with current value
        _subscription = _score.Observe(this.OnScoreChanged);
    }
    
    public void Dispose()
    {
        // Unsubscribe to avoid memory leaks
        _subscription.Dispose();
    }
    
    private void OnScoreChanged(int score)
    {
        // Update UI text
        _scoreText.text = "Score: " + score;
    }
}
```