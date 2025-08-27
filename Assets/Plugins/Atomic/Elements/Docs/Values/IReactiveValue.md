## 🧩 IReactiveValue<T>

`IReactiveValue<T>` represents a **reactive value** that combines:

- **Read-only access** through `IValue<T>`
- **Reactive observation** through `ISignal<T>`

It allows you to both **read the current value** and **subscribe to changes**.

---

#### Type Parameter

- `T` – The type of the value.

---

#### Inheritance

- `IValue<T>` – Provides read-only access via `Value` and `Invoke()`.
- `ISignal<T>` – Provides reactive subscriptions for when the value changes.

---

#### Description

- `IReactiveValue<T>` is useful in **reactive programming** or **UI binding scenarios**, where you want to **observe changes** to a value while keeping it **read-only**.
- It merges the benefits of a **value provider** and a **signal emitter** in a single interface.

---

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

You can use extension Observe and cache subscription handle
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